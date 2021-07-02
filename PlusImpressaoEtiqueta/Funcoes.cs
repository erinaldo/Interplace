using DanfeSharp;
using DanfeSharp.Modelo;
using MySql.Data.MySqlClient;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace PlusImpressaoEtiqueta
{
    public static class Funcoes
    {
        public static object CorrigeParametro(object oObject)
        {
            return oObject == null ? DBNull.Value : oObject;

        }

        public static bool ExecutabancoMySql(string sSql, Dictionary<string, object> dictSql = null)
        {
            int iRetorno = -1;
            bool lRetorno = false;
            MySqlConnection ConexaoMysql = null;

            ConexaoMysql = new MySqlConnection(@"server=187.45.196.174;uid=interplacelog; pwd=interplace2020;database= interplacelog");

            try
            {
                ConexaoMysql.Open();

                //TransacaoConexaoMysql = ConexaoMysql.BeginTransaction();
                MySqlCommand oCmd = new MySqlCommand();
                try
                {
                    oCmd.CommandText = sSql;
                    oCmd.Connection = ConexaoMysql;
                    //oCmd.Transaction = TransacaoConexaoMysql;
                    if (dictSql != null)
                        foreach (KeyValuePair<string, object> pairSql in dictSql)
                        {
                            MySqlParameter oParametro = new MySqlParameter();
                            oParametro.ParameterName = pairSql.Key;
                            oParametro.Value = CorrigeParametro(pairSql.Value);
                            if (pairSql.Key.Contains("DATA") && pairSql.Value == null)
                                oParametro.Value = DateTime.Parse("01/01/1900 00:00:00");
                            if (oParametro.Value == "")
                                oParametro.Value = string.Empty;
                            //oParametro.Value = oParametro.Value.ToString().Replace(",", ".");
                            //if (pairSql.Key.Contains("@"))
                            oCmd.Parameters.Add(oParametro);
                            //else
                            //oCmd.Parameters.Add("@" + pairSql.Key, pairSql.Value);
                        }
                    oCmd.ExecuteNonQuery();

                    //TransacaoConexaoMysql.Commit();
                    //if (sSql.Contains("INSERT"))
                    //{
                    //    // If has last inserted id, add a parameter to hold it.
                    //    if (oCmd. != null) oCmd.Parameters.Add(new MySqlParameter("newId", oCmd.LastInsertedId));

                    //    // Return the id of the new record. Convert from Int64 to Int32 (int).
                    //    iRetorno = Convert.ToInt32(oCmd.Parameters["@newId"].Value);
                    //}
                    lRetorno = true;
                }

                catch (Exception e)
                {

                }
                finally
                {
                    //TransacaoConexao.Dispose();
                    oCmd.Dispose();
                    if (ConexaoMysql.State == ConnectionState.Open)
                    {
                        ConexaoMysql.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            if (dictSql != null)
                dictSql.Clear();
            return lRetorno;
        }

        public static DataTable ConsultaBancoMysql(string sSql, Dictionary<string, object> dictSql = null)
        {
            DataTable dtbRetorno = new DataTable();
            MySqlCommand oCmd = new MySqlCommand();
            MySqlConnection ConexaoMysql = null;
            //ConexaoMysql = new MySqlConnection(@"server=interplace_bd1.mysql.dbaas.com.br;uid=interplace_bd1; pwd=interplace2020;database= interplace_bd1");
            ConexaoMysql = new MySqlConnection(@"server=187.45.196.174;uid=interplacelog; pwd=interplace2020;database= interplacelog");

            try
            {
                ConexaoMysql.Open();

                //if (oTrInterno == null)
                //    TransacaoConexaoMysql = ConexaoMysql.BeginTransaction();
                //else
                //    TransacaoConexaoMysql = oTrInterno;
                oCmd.CommandText = sSql;
                oCmd.Connection = ConexaoMysql;
                //oCmd.Transaction = TransacaoConexaoMysql;

                if (dictSql != null)
                    foreach (KeyValuePair<string, object> pairSql in dictSql)
                    {
                        MySqlParameter oParametro = new MySqlParameter();
                        oParametro.ParameterName = pairSql.Key;
                        oParametro.Value = pairSql.Value;
                        //if (pairSql.Key.Contains("@"))
                        oCmd.Parameters.Add(oParametro);
                        //else
                        //oCmd.Parameters.Add("@" + pairSql.Key, pairSql.Value);
                    }

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(oCmd);
                objAdapter.ResetFillLoadOption();
                objAdapter.Fill(dtbRetorno);
                objAdapter.Dispose();

                //if (oTrInterno == null)
                //    TransacaoConexaoMysql.Commit();
            }
            catch (Exception e)
            {
                try
                {

                    if (e.Message == "SqlConnection não dá suporte para transações paralelas.")
                    {
                        ConexaoMysql.Close();
                        ConexaoMysql.Open();
                    }

                }
                catch
                {

                }

                if (e.Message.Contains("fechada"))
                {
                    ConexaoMysql.Close();
                    ConexaoMysql.Open();
                }

            }
            finally
            {
                oCmd.Dispose();
                if (ConexaoMysql.State == ConnectionState.Open)
                {
                    ConexaoMysql.Close();
                }
            }
            if (dictSql != null)
                dictSql.Clear();

            return dtbRetorno;
        }

        public static string RetornaEtiqueta(string sCNPJ, string sProduto)
        {
            Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

            string sPasta = "";

            string sEtiqueta = "";

            string sSqlTemp = "SELECT * FROM CLIENTE WHERE CNPJCLIENTES LIKE ('%" + sCNPJ + "%')";
            DataTable dtbCliente = ConsultaBancoMysql(sSqlTemp);
            string sPastaTemp = dtbCliente.Rows[0]["PASTA"].ToString();
            int iCodigo = int.Parse(dtbCliente.Rows[0]["CODIGO"].ToString());
            dtbCliente.Dispose();

            sPasta = HttpContext.Current.Server.MapPath("~");
            string sPastaRetorno = sPastaTemp + "\\13-RetornoOrla\\";

            bool lEtiquetaImpressa = false;
            bool lNotaImpressa = false;

            string sSql = @"SELECT M.NOTA, M.CHAVENOTA, M.FINALIZADO,M.DATA, M.CNPJ, D.PRODUTO, V.ETIQUETATXTTXT,M.XML,V.LOJA, V.ETIQUETATXT
                                FROM NOTAMASTER M
                                LEFT OUTER JOIN NOTADETALHE  D
                                ON M.NOTA = D.NOTA
                                LEFT OUTER JOIN VENDAS V
                                ON M.CHAVENOTA = V.NOTAFISCAL
                                WHERE M.FINALIZADO = 0
                                    AND M.CNPJ = @CNPJ
                                    AND D.PRODUTO = @PRODUTO
                                ORDER BY M.DATA LIMIT 1 ";

            ParametrosSQL.Add("PRODUTO", sProduto);
            ParametrosSQL.Add("CNPJ", sCNPJ);
            DataTable dtbNotaEtiqueta = ConsultaBancoMysql(sSql, ParametrosSQL);

            if (dtbNotaEtiqueta.Rows.Count > 0)
            {
                if (dtbNotaEtiqueta.Rows[0]["XML"].ToString() == "")
                {
                    return "Nota não encontrada para este CNPJ!";
                }

                if (dtbNotaEtiqueta.Rows[0]["ETIQUETATXTTXT"].ToString() == "")
                {
                    return "Etiqueta não encontrada para este CNPJ!";
                }

                DanfeViewModel oModel = DanfeViewModelCreator.CriarDeStringXml(Encoding.ASCII.GetString((byte[])dtbNotaEtiqueta.Rows[0]["XML"]));

                string sMiniDanfe = "";

                if (iCodigo == 3)
                    oModel.Duplicatas.Clear();

                //Inicia o Danfe com o modelo criado
                using (Danfe danfe = new Danfe(oModel))
                {
                    danfe.Gerar();
                    danfe.Salvar(sPasta + "\\PDFBACKUP\\" + Path.GetFileName(dtbNotaEtiqueta.Rows[0]["CHAVENOTA"].ToString()) + ".pdf");
                }

                if (dtbCliente.Rows[0]["MINIDANFE"].ToString() == "1")
                {

                    string sOperadotransportador = "Operador Logistico:";
                    //if (ClasseParametros.sBanco != "interplace_bd1")
                    //{
                    //    sOperadotransportador = "Transportador:";

                    //}

                    int iX = 5;

                    string sDANFE = @"^XA ";
                    sDANFE += "^FXDados cabecalho.";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",50^GB290,135,3^FS";
                    sDANFE += "^CFA,20";
                    sDANFE += "^CF0,18";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",70^FD1^FS";
                    sDANFE += "^FO" + (145 + iX).ToString() + ",105^FD{0}^FS";
                    sDANFE += "^FO" + (145 + iX).ToString() + ",140^FD{1}^FS";
                    sDANFE += "^FO" + (295 + iX).ToString() + ",107^F{2}^FS";
                    sDANFE += "^FO" + (370 + iX).ToString() + ",100^FD{3}^FS";
                    sDANFE += "^FO" + (350 + iX).ToString() + ",170^FD{4} {5} {6}^FS";
                    sDANFE += "";
                    sDANFE += "^CF0,25";
                    sDANFE += "^FO" + (85 + iX).ToString() + ",70^FD-^FS";
                    sDANFE += "^FO" + (110 + iX).ToString() + ",70^FDSaida^FS";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",105^FDNumero^FS";
                    sDANFE += "^FO" + (235 + iX).ToString() + ",105^FDSerie^FS";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",140^FDEmissao^FS";
                    sDANFE += "";
                    sDANFE += "^CF0,30";
                    sDANFE += "^FO" + (450 + iX).ToString() + ",70^FDChave de acesso^FS";
                    sDANFE += "^FO" + (370 + iX).ToString() + ",130^FDProtocolo de autorizacao de uso^FS";
                    sDANFE += "";
                    sDANFE += "^FXCodigo de barras.";
                    sDANFE += "^BY1,3,100";
                    sDANFE += "^FO" + (140 + iX).ToString() + ",210^BC^FD{3}^FS";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",450^GB700,1,3^FS";
                    sDANFE += "";
                    sDANFE += "^FXDados do Emitente.";
                    sDANFE += "^CFA,20";
                    sDANFE += "^FO" + (180 + iX).ToString() + ",470^FD{7}^FS";
                    sDANFE += "^FO" + (115 + iX).ToString() + ",500^FD{8}^FS";
                    sDANFE += "^FO" + (30 + iX).ToString() + "0,500^FD{9}2^FS";
                    sDANFE += "^FO" + (720 + iX).ToString() + ",500^FD{10}^FS";
                    sDANFE += "^CF0,25";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",470^FDEmitente^FS";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",500^FDCNPJ^FS";
                    sDANFE += "^FO" + (315 + iX).ToString() + ",500^FDIncricao Estadual^FS";
                    sDANFE += "^FO" + (680 + iX).ToString() + ",500^FDUF^FS";
                    sDANFE += "";
                    sDANFE += "^FXDados do Destinatario.";
                    sDANFE += "^CFA,20";
                    sDANFE += "^FO" + (180 + iX).ToString() + ",550^FD{11}^FS";
                    sDANFE += "^FO" + (160 + iX).ToString() + ",580^FD{12}^FS";
                    sDANFE += "^FO" + (30 + iX).ToString() + "0,580^FD{13}^FS";
                    sDANFE += "^FO" + (720 + iX).ToString() + ",580^FD{14}^FS";
                    sDANFE += "";
                    sDANFE += "^CF0,25";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",550^FDDestinatario^FS";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",580^FDCPF/CNPJ^FS";
                    sDANFE += "^FO" + (315 + iX).ToString() + ",580^FDIncricao Estadual^FS";
                    sDANFE += "^FO" + (680 + iX).ToString() + ",580^FDUF^FS";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",610^FDDanfe Simplificada^FS";
                    sDANFE += "^FXDados Adicionais.";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",650^GB700,1,3^FS";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",770^GB700,1,3^FS";
                    sDANFE += "";
                    sDANFE += "^CF0,30";
                    sDANFE += "^FO" + (30 + iX).ToString() + ",740^FDDados Adicionais^FS";
                    sDANFE += "^CFA,20";
                    sDANFE += "^FO" + (250 + iX).ToString() + ",780^FD{15}^FS";
                    sDANFE += "^FO" + (120 + iX).ToString() + ",830^FD{16}^FS";
                    sDANFE += "^FO" + (180 + iX).ToString() + ",880^FD{17} - {19}^FS";
                    sDANFE += "^FO" + (150 + iX).ToString() + ",930^FD{18}^FS";
                    sDANFE += "^FO" + (270 + iX).ToString() + ",930^FD{20}^FS";
                    sDANFE += "^FO" + (630 + iX).ToString() + ",1030^FD{21}^FS";
                    sDANFE += "^FO" + (110 + iX).ToString() + ",1030^FD{22}^FS";
                    sDANFE += "^FO" + (310 + iX).ToString() + ",1030^FD{23}^FS";
                    sDANFE += "^FO" + (400 + iX).ToString() + ",1030^FD{24}^FS";
                    sDANFE += "";
                    sDANFE += "^CF0,23";
                    sDANFE += "^FO" + (60 + iX).ToString() + ",780^FD" + sOperadotransportador + "^FS";
                    sDANFE += "^FO" + (60 + iX).ToString() + ",830^FDCNPJ:^FS";
                    sDANFE += "^FO" + (60 + iX).ToString() + ",880^FDEndereco:^FS";
                    sDANFE += "^FO" + (60 + iX).ToString() + ",930^FDNumero:^FS";
                    sDANFE += "^FO" + (200 + iX).ToString() + ",930^FDBairro:^FS";
                    sDANFE += "^FO" + (550 + iX).ToString() + ",1030^FDCidade:^FS";
                    sDANFE += "^FO" + (60 + iX).ToString() + ",1030^FDCEP:^FS";
                    sDANFE += "^FO" + (220 + iX).ToString() + ",1030^FDEstado:^FS";
                    sDANFE += "^FO" + (350 + iX).ToString() + ",1030^FDPais:^FS";
                    sDANFE += "";
                    sDANFE += "^XZ";

                    //^FO650,1100^FDComplemento:^FS
                    sDANFE = string.Format(sDANFE,
                    oModel.NfNumero.ToString(),
                    oModel.NfSerie.ToString(),
                    oModel.DataHoraEmissao.ToString(),
                    oModel.ChaveAcesso,
                    oModel.ProtocoloAutorizacao,
                    "",
                   //DateTime.Parse(oModel.DataHoraEmissao.ToString()).ToShortDateString(),
                   //DateTime.Parse(oModel.DataHoraEmissao.ToString()).ToShortTimeString(),
                   "",
                   oModel.Emitente.NomeFantasia,
                   oModel.Emitente.CnpjCpf,
                   oModel.Emitente.Ie,
                   oModel.Emitente.EnderecoUf,
                   oModel.Destinatario.NomeFantasia,
                   oModel.Destinatario.CnpjCpf,
                   oModel.Destinatario.Ie,
                   oModel.Destinatario.EnderecoUf,
                   oModel.Transportadora.NomeFantasia,
                   oModel.Transportadora.CnpjCpf,
                   oModel.Transportadora.EnderecoLinha1,
                   oModel.Transportadora.EnderecoNumero,
                   oModel.Transportadora.EnderecoComplemento,
                   oModel.Transportadora.EnderecoBairro,
                   oModel.Transportadora.Municipio,
                   oModel.Transportadora.EnderecoCep,
                   oModel.Transportadora.EnderecoUf,
                   "BR",
                   "");

                    byte[] oByte = Encoding.ASCII.GetBytes(sDANFE);
                    string s = System.Text.Encoding.UTF8.GetString(oByte);
                    if (s.Substring(0, 1) == "\"")
                    {
                        s = s.Substring(1);
                        s = s.Substring(0, s.Length - 2);
                    }

                    if (!Directory.Exists(sPasta + "\\MINIDANFEBACKUP\\"))
                    {
                        Directory.CreateDirectory(sPasta + "\\MINIDANFEBACKUP\\");
                    }

                    sMiniDanfe = s;

                    File.WriteAllText(sPasta + "\\MINIDANFEBACKUP\\" + dtbNotaEtiqueta.Rows[0]["CHAVENOTA"].ToString() + ".txt", s);

                    //GCHandle h = GCHandle.Alloc(oByte, GCHandleType.Pinned);
                    //IntPtr pnt = h.AddrOfPinnedObject();
                    //RawPrinterHelper.SendStringToPrinter(oSett.ImpressoraTermica, s);
                    ////RawPrinterHelper.SendBytesToPrinter(comboImpressora.Text, pnt, aRetorno.Length);
                    //h.Free();
                }
                else
                {

                    //PdfDocument pdfdocument = new PdfDocument();
                    //pdfdocument.LoadFromFile(sPasta + "\\PDFBACKUP\\" + Path.GetFileName(dtbNotaEtiqueta.Rows[0]["CHAVENOTA"].ToString()) + ".pdf");
                    //pdfdocument.PrinterName = oSett.ImpressoraLaserJato;
                    //pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                    //pdfdocument.PrintDocument.Print();
                    //pdfdocument.Dispose();

                    lNotaImpressa = true;
                }

                sEtiqueta = Encoding.ASCII.GetString((byte[])dtbNotaEtiqueta.Rows[0]["ETIQUETATXTTXT"]);

                if (dtbCliente.Rows[0]["MINIDANFE"].ToString() == "1")
                {
                    sEtiqueta += sMiniDanfe;
                }

                if (dtbNotaEtiqueta.Rows[0]["LOJA"].ToString().ToUpper() == "ME2")
                {
                    //MercadoLivreOrla oJson = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreOrla>(sEtiqueta);
                    //sEtiqueta = oJson.content;
                }
                else if (dtbNotaEtiqueta.Rows[0]["LOJA"].ToString().ToUpper() == "TRANSPORTADORA")
                {
                    string ssEtiqueta = sEtiqueta.Substring(18);
                }

                if (sEtiqueta.Substring(0, 10).ToUpper().Contains("PDF")) // etiqueta pdf
                {
                    byte[] oByte = (byte[])dtbNotaEtiqueta.Rows[0]["ETIQUETATXT"];
                    string s = System.Text.Encoding.UTF8.GetString(oByte);
                    if (s.Substring(0, 1) == "\"")
                    {
                        s = s.Substring(1);
                        s = s.Substring(0, s.Length - 2);
                    }

                    if (!Directory.Exists(sPasta + "\\ETQBACKUP\\"))
                    {
                        Directory.CreateDirectory(sPasta + "\\ETQBACKUP\\");
                    }

                    File.WriteAllText(sPasta + "\\ETQBACKUP\\" + dtbNotaEtiqueta.Rows[0]["CHAVENOTA"].ToString() + ".txt", s);

                    //var size = Marshal.SizeOf(oByte[0]) * oByte.Length;
                    //var pBytes = Marshal.AllocHGlobal(size);
                    //GCHandle h = GCHandle.Alloc(oByte, GCHandleType.Pinned);

                    //try
                    //{
                    //    IntPtr pnt = h.AddrOfPinnedObject();
                    //    RawPrinterHelper.SendBytesToPrinter(oSett.ImpressoraTermica, pBytes, size);
                    //}
                    //finally
                    //{
                    //    Marshal.FreeCoTaskMem(pBytes);
                    //}


                    ////RawPrinterHelper.SendBytesToPrinter((oSett.ImpressoraTermica, s);
                    ////RawPrinterHelper.SendBytesToPrinter(comboImpressora.Text, pnt, aRetorno.Length);
                    //h.Free();

                }
                else
                {
                    byte[] oByte = Encoding.ASCII.GetBytes(sEtiqueta);
                    string s = System.Text.Encoding.UTF8.GetString(oByte);
                    if (s.Substring(0, 1) == "\"")
                    {
                        s = s.Substring(1);
                        s = s.Substring(0, s.Length - 2);
                    }

                    if (!Directory.Exists(sPasta + "\\ETQBACKUP\\"))
                    {
                        Directory.CreateDirectory(sPasta + "\\ETQBACKUP\\");
                    }

                    File.WriteAllText(sPasta + "\\ETQBACKUP\\" + dtbNotaEtiqueta.Rows[0]["CHAVENOTA"].ToString() + ".txt", s);

                    //GCHandle h = GCHandle.Alloc(oByte, GCHandleType.Pinned);
                    //IntPtr pnt = h.AddrOfPinnedObject();
                    //RawPrinterHelper.SendStringToPrinter(oSett.ImpressoraTermica, s);
                    ////RawPrinterHelper.SendBytesToPrinter(comboImpressora.Text, pnt, aRetorno.Length);
                    //h.Free();
                }
                lEtiquetaImpressa = true;

                ExecutabancoMySql("UPDATE VENDAS SET IMPRESSOES = COALESCE(0,IMPRESSOES) + 1, DATAIMPRESSAO = CURRENT_TIMESTAMP WHERE NOTAFISCAL = '" + dtbNotaEtiqueta.Rows[0]["CHAVENOTA"].ToString() + "'");
                ExecutabancoMySql("UPDATE NOTAMASTER SET FINALIZADO = 1 WHERE CHAVENOTA = '" + dtbNotaEtiqueta.Rows[0]["CHAVENOTA"].ToString() + "'");
            }
            else
            {
                return "Não foi encontrado este produto para este CNPJ!";
            }

            //Retorno etiqueta lida

            dtbNotaEtiqueta.Dispose();

            //Retorno Orla
            return sEtiqueta;
            //editEtiqueta.Text = "";
            //editEtiqueta.Focus();
            //imgBoxProcessando.Visible = false;
            //lblMensagem.Text = "";

            //CarregaGrids();
        }

    }
}