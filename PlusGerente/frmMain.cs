using DanfeSharp;
using DanfeSharp.Modelo;
using Ghostscript.NET.Rasterizer;
using interRegraNegocio;
using interRegraNegocio.B2W;
using interRegraNegocio.FortePlus;
using interRegraNegocio.MercadoLivre;
using InterRegraNegocio;
using InterRegraNegocio.CorreiosLocal;
using InterRegraNegocio.MagazineLuiza;
using InterRegraNegocio.MercadoLivre;
using InterRegraNegocio.ORLA;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PdfSharp.Pdf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace PlusGerente
{
    public partial class frmMain : MetroFramework.Forms.MetroForm
    {
        bool lAberto = true;
        bool lRodandoTimer = false;
        bool lRodandoTimerFinalizaEtiqueta = false;
        Properties.Settings oSett = new Properties.Settings();
        TcpListener oServidor = null;
        TcpListener oServidorConsulta = null;

        frmClientes oFrmCliente = new frmClientes();


        System.Threading.Timer TTimerIniciaProcesso = null;

        public frmMain()
        {
            InitializeComponent();
            ClasseParametros.iconApp = iconApp;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        public void SalvaNota(int iCodigo, string sPastaParametro)
        {
            Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();


            //#if DEBUG
            //            string sPastaXML = "C:\\Users\\Rodrigo\\Documents\\XML";
            //            string sPastaEtiqueta = "C:\\Users\\Rodrigo\\Documents\\Etiqueta";
            //#endif
            string sPastaXML = sPastaParametro + "\\07-EnvioSaidaNF\\";
            string sPastaEtiqueta = sPastaParametro + "\\12-Etiquetas\\";

            if (!Directory.Exists(sPastaXML))
            {
                sPastaXML = "C:\\CLIENTES\\ORLA\\07-EnvioSaidaNF\\";
                sPastaEtiqueta = "C:\\CLIENTES\\ORLA\\12-Etiquetas\\";
            }

            string[] aArquivos = Directory.GetFiles(sPastaXML, "*.xml");

            foreach (string sArquivo in aArquivos)
            {
                try
                {
                    bool lLivre = false;
                    while (!lLivre)
                    {
                        FileStream stream = null;
                        try
                        {
                            stream = File.Open(sArquivo, FileMode.Open, FileAccess.Read, FileShare.None);
                            lLivre = true;
                        }
                        catch (IOException)
                        {
                            //the file is unavailable because it is:
                            //still being written to
                            //or being processed by another thread
                            //or does not exist (has already been processed)
                            lLivre = false;
                        }
                        finally
                        {
                            if (stream != null)
                                stream.Close();
                        }

                        //file is not locked
                    }

                    string sPasta = Directory.GetCurrentDirectory();
                    string sPastaPDFBACKUP = sPastaXML + "PDFBACKUP\\";
                    if (!Directory.Exists(sPastaPDFBACKUP))
                    {
                        Directory.CreateDirectory(sPastaPDFBACKUP);
                    }

                    string sPastaDataHoje = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "\\";
                    if (!Directory.Exists(sPastaPDFBACKUP + sPastaDataHoje))
                    {
                        Directory.CreateDirectory(sPastaPDFBACKUP + sPastaDataHoje);
                    }

                    string sPastaSalvaDANFE = sPastaPDFBACKUP + sPastaDataHoje;
                    string sTextoXML = File.ReadAllText(sArquivo);
                    DanfeSharp.Modelo.DanfeViewModel oModel = DanfeSharp.Modelo.DanfeViewModelCreator.CriarDeArquivoXml(sArquivo);
                    if (iCodigo == 3)
                        oModel.Duplicatas.Clear();

                    //Inicia o Danfe com o modelo criado
                    using (Danfe danfe = new Danfe(oModel))
                    {
                        danfe.Gerar();
                        danfe.Salvar(sPastaSalvaDANFE + Path.GetFileName(sArquivo.Split('.')[0] + ".pdf"));
                    }

                    string sChaveNota = oModel.ChaveAcesso;
                    string sNota = oModel.NfNumero.ToString();
                    string sDataEmissao = oModel.DataHoraEmissao.ToString();
                    string sCNPJ = oModel.Emitente.CnpjCpf;

                    DateTime dEmissao = DateTime.Parse(sDataEmissao);

                    byte[] oPDFStream = File.ReadAllBytes(sPastaSalvaDANFE + Path.GetFileName(sArquivo.Split('.')[0] + ".pdf"));
                    string sSql = "";
                    sSql = "INSERT INTO NOTAMASTER(NOTA,CHAVENOTA,DATA,FINALIZADO,CNPJ,XML,PDF) VALUES(@NOTA,@CHAVENOTA,@DATA,@FINALIZADO,@CNPJ,@XML,@PDF)";
                    ParametrosSQL.Add("NOTA", int.Parse(sNota).ToString());
                    ParametrosSQL.Add("CHAVENOTA", sChaveNota);
                    ParametrosSQL.Add("DATA", dEmissao.ToString("yyyy-MM-dd HH:mm:ss"));
                    ParametrosSQL.Add("FINALIZADO", "0");
                    ParametrosSQL.Add("CNPJ", sCNPJ);
                    ParametrosSQL.Add("XML", sTextoXML);
                    ParametrosSQL.Add("PDF", oPDFStream);
                    ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);

                    foreach (ProdutoViewModel oProduto in oModel.Produtos)
                    {
                        string sEAN = oProduto.EAN;
                        sSql = "INSERT INTO NOTADETALHE(NOTA,PRODUTO,CNPJ) VALUES(@NOTA,@PRODUTO,@CNPJ)";
                        ParametrosSQL.Clear();
                        ParametrosSQL.Add("NOTA", int.Parse(sNota).ToString());
                        ParametrosSQL.Add("PRODUTO", sEAN);
                        ParametrosSQL.Add("CNPJ", sCNPJ);
                        ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);
                    }
                    oModel = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    string sPastaXMLBACKUP = sPastaXML + "XMLBACKUP\\";

                    if (!Directory.Exists(sPastaXMLBACKUP))
                    {
                        Directory.CreateDirectory(sPastaXMLBACKUP);
                    }

                    if (!Directory.Exists(sPastaXMLBACKUP + sPastaDataHoje))
                    {
                        Directory.CreateDirectory(sPastaXMLBACKUP + sPastaDataHoje);
                    }

                    File.Copy(sArquivo, sPastaXMLBACKUP + sPastaDataHoje + Path.GetFileName(sArquivo), true);
                    File.Delete(sArquivo);
                }

                catch (Exception ex)
                {
                    ClasseParametros.MostraErro(ex.Message, iconApp);
                }
            }

            // salva etiquetas
            IEnumerable<String> aEtiquetas = Directory.EnumerateFiles(sPastaEtiqueta, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.ToLower().EndsWith(".json") || s.ToLower().EndsWith(".txt") || s.ToLower().EndsWith(".xml"));
            foreach (string sArquivo in aEtiquetas)
            {
                try
                {
                    bool lLivre = false;
                    while (!lLivre)
                    {
                        FileStream stream = null;
                        try
                        {
                            stream = File.Open(sArquivo, FileMode.Open, FileAccess.Read, FileShare.None);
                            lLivre = true;
                        }
                        catch (IOException)
                        {
                            //the file is unavailable because it is:
                            //still being written to
                            //or being processed by another thread
                            //or does not exist (has already been processed)
                            lLivre = false;
                        }
                        finally
                        {
                            if (stream != null)
                                stream.Close();
                        }
                        //file is not locked
                    }

                    byte[] aEtiqueta = null;
                    string sTexto = File.ReadAllText(sArquivo);
                    string sSql = "";
                    string sEtiqueta = "";
                    if (sArquivo.Split('_')[1].Split('.')[0] == "MELI")
                    {
                        ClasseOrlaMercadoLivre oJsonB2W = Newtonsoft.Json.JsonConvert.DeserializeObject<ClasseOrlaMercadoLivre>(sTexto);
                        sEtiqueta = oJsonB2W.content;
                        aEtiqueta = Encoding.UTF8.GetBytes(sEtiqueta);
                    }
                    else if (sArquivo.Split('_')[1].Split('.')[0] == "B2W")
                    {
                        EtiquetaJSON oJsonB2W = Newtonsoft.Json.JsonConvert.DeserializeObject<EtiquetaJSON>(sTexto);
                        if (oJsonB2W.plp != null)
                        {
                            sSql = "";
                            sEtiqueta = GeraEtiquetaB2W(oJsonB2W, "", "");
                            aEtiqueta = Encoding.UTF8.GetBytes(sEtiqueta);
                        }
                    }
                    else if (sArquivo.Split('_')[1].Split('.')[0].ToUpper() == "CORREIOS")
                    {
                        //A chave de acesso da NF-e é um número de 44 dígitos
                        //calculado de acordo com o manual de integração do
                        //contribuinte(disponibilizado no portal do NF - e).
                        //(02 dígitos) ***Código do Estado(UF) do emitente
                        //(04 dígitos) * **Ano e mês da emissão da NF - e(no formato AAMM)
                        //(14 dígitos) * **CNPJ do emitente da NF - e(CNPJ da sua Empresa)
                        //(02 dígitos) * **Modelo do NF - e
                        //(03 dígitos) * **Série do NF - e
                        //(09 dígitos) * **Número da NF - e
                        //(09 dígitos) * **Código da NF-e - Número gerado pelo sistema
                        //(01 dígitos) * **Dígito verificador - DV(Cálculo no módulo 11).
                        //Ex.de chave de acesso do NF - e:
                        //51080701212344000127550010000000981364112281
                        //51 * 0807 * 01212344000127 * 55 * 001 * 000000098 * 136411228 * 1
                        //Parte do Código Significado
                        //51 = Código do Estado
                        //0807 = Ano e mês da emissão da NF-e
                        //01212344000127 = CNPJ do Emitente
                        //55 = Modelo da NF-e
                        //001 = Série da NF-e
                        //000000098 = Número da NF-e
                        //136411778 = Código da NF-e
                        //1 = DV(digito verificador)
                        string sExtensao = sArquivo.Split('.')[1];
                        if (sExtensao.ToLower() == "txt")
                        {
                            sEtiqueta = sTexto;

                            aEtiqueta = Encoding.UTF8.GetBytes(sTexto);
                        }
                        else
                        {
                            string sTextoXMl = File.ReadAllText(sArquivo);
                            var sr = new System.IO.StringReader(sTextoXMl);
                            var xs = new XmlSerializer(typeof(correioslog));

                            InterRegraNegocio.CorreiosLocal.correioslog oResult = (InterRegraNegocio.CorreiosLocal.correioslog)xs.Deserialize(sr);


                            foreach (correioslogObjeto_postal oObjetoPostal in oResult.objeto_postal)
                            {
                                DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM NOTAMASTER WHERE NOTA = '" + oObjetoPostal.nacional.numero_nota_fiscal.ToString() + "'");
                                if (d.Rows.Count > 0)
                                {
                                    string sChaveNota = d.Rows[0]["CHAVENOTA"].ToString();
                                    // ClasseCorreiosLocal.GeraEtiquetaCorreios(oResult, oObjetoPostal, sChaveNota, iCodigo.ToString(), sPastaParametro);
                                }
                            }
                            continue;
                        }
                    }
                    else if ((sArquivo.Split('_')[1].Split('.')[0].Contains("MAGA")) || sArquivo.Split('_')[1].Split('.')[0].Contains("NET"))
                    {
                        List<string> lstZPL = ZplFromPdf(new MemoryStream(Convert.FromBase64String(sArquivo)), new Size(0, 0), 300);

                        aEtiqueta = Encoding.UTF8.GetBytes(sTexto);
                    }
                    else
                    {
                        sEtiqueta = sTexto;

                        aEtiqueta = Encoding.UTF8.GetBytes(sTexto);
                    }

                    if (aEtiqueta != null)
                    {
                        DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + Path.GetFileName(sArquivo).ToLower().Replace(".json", "").Replace(".txt", "").Replace(".pdf", "").Split('_')[0] + "'");
                        if (d.Rows.Count > 0)
                        {
                            sSql = "UPDATE VENDAS SET ETIQUETATXTTXT = @ETIQUETATXTTXT, ETIQUETATXT=@ETQ,LOJA=@LOJA,CODIGOCLIENTE=@CODIGOCLIENTE WHERE NOTAFISCAL = @NOTA";
                        }
                        else
                        {
                            sSql = "INSERT INTO VENDAS(NOTAFISCAL,ETIQUETATXT,ETIQUETATXTTXT,LOJA,DATACRIADO,LOTE,CODIGOCLIENTE) VALUES(@NOTA,@ETQ,@ETIQUETATXTTXT,@LOJA,@DATACRIADO,@LOTE,@CODIGOCLIENTE)";
                            ParametrosSQL.Add("@DATACRIADO", DateTime.Now);
                            ParametrosSQL.Add("@LOTE", ClasseParametros.PegaLote(Path.GetFileName(sArquivo).ToLower().Replace(".json", "").Replace(".txt", "").Replace(".pdf", "").Split('_')[1], iCodigo.ToString()));
                        }
                        d.Dispose();

                        File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLETQ\\" + Path.GetFileName(sArquivo).ToLower().Replace(".json", "").Replace(".txt", "").Replace(".pdf", "").Split('_')[0] + ".TXT", aEtiqueta); // Requires System.IO

                        ParametrosSQL.Add("@ETQ", aEtiqueta);
                        ParametrosSQL.Add("@ETIQUETATXTTXT", sEtiqueta);
                        ParametrosSQL.Add("@NOTA", Path.GetFileName(sArquivo).ToLower().Replace(".json", "").Replace(".txt", "").Replace(".pdf", "").Split('_')[0]);
                        ParametrosSQL.Add("@LOJA", Path.GetFileName(sArquivo).ToLower().Replace(".json", "").Replace(".txt", "").Replace(".pdf", "").Split('_')[1]);
                        ParametrosSQL.Add("@CODIGOCLIENTE", iCodigo);
                        ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);


                        if (!Directory.Exists(sPastaEtiqueta + "\\JSONTXTBACKUP\\"))
                        {
                            Directory.CreateDirectory(sPastaEtiqueta + "\\JSONTXTBACKUP\\");
                        }

                        string sPastaEtiquetaBackup = sPastaEtiqueta + "\\JSONTXTBACKUP\\";
                        string sPastaDataHoje = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "\\";
                        if (!Directory.Exists(sPastaEtiquetaBackup + sPastaDataHoje))
                        {
                            Directory.CreateDirectory(sPastaEtiquetaBackup + sPastaDataHoje);
                        }


                        File.Copy(sArquivo, sPastaEtiquetaBackup + sPastaDataHoje + Path.GetFileName(sArquivo), true);
                        File.Delete(sArquivo);
                    }
                }

                catch (Exception ex)
                {
                    ClasseParametros.MostraErro(ex.Message, iconApp);
                }
            }



        }

        public static List<string> ZplFromPdf(Stream pdf, Size size, int dpi = 300)
        {
            var zpls = new List<string>();

            if (size == new Size(0, 0))
            {
                size = new Size(812, 1218);
            }

            using (var rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(pdf);

                var images = new List<Image>();
                for (int pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
                {
                    var bmp = new Bitmap(rasterizer.GetPage(dpi, dpi, pageNumber), size.Width, size.Height);

                    var zpl = new StringBuilder();
                    zpl.Append(ZPLHelper.GetGrfStoreCommand("R:LBLRA2.GRF", bmp));
                    zpl.Append("^XA^FO0,0^XGR:LBLRA2.GRF,1,1^FS^XZ");
                    zpl.Append("^XA^IDR:LBLRA2.GRF^FS^XZ");

                    zpls.Add(zpl.ToString());
                }
                return zpls;
            }
        }

        private bool VerificarManutencaoAberto()
        {
            bool lFormAberto = false;
            FormCollection aForms = Application.OpenForms;
            foreach (Form oForm in aForms)
            {
                if (oForm.Name == "frmClienteManutencao" || oForm.Name == "frmClientes")
                {
                    lFormAberto = true;
                }
            }

            return lFormAberto;
        }


        private string GeraEtiquetaMAGALU(Order oJsonMAGALU, Content oAnymarket)
        {
            string sImagemMAGALU = "^FO50,50^GFA,5200,5200,40,,::::::::::::::::::iK0FC,L01LF8R0FCL07EgO0FC,L01LFCR0FEL0FEgO0FC,L01LFCR0FFK01FEgO0FC,gL0FFK03FEgO0FC,gL0FF8J03FEgO0FC,gL0FFCJ07FEgO0FC,gL0FFCJ0FFEgO0FC,gL0FFEJ0FFEgO0FC,gL0FFEI01FFEgO0FC,gL0IFI01FFEgO0FC,gL0IF8003FFEJ07F8K01FEM07FCJ0FC,gL0IF8007F7EI07IFK07FF81F8007IF8I0FC01F8I0FC,gL0F8FC007E7E003JFCI01IFE1F801JFEI0FC01F8I0FC,gL0F8FE00FC7E003JFEI03JF1F801KFI0FC01F8I0FC,gL0F87E01FC7E001KFI07FE1IF801KF800FC01F8I0FC,gL0F83F01F87E001E00FF800FF003FF800E007F800FC01F8I0FC,gL0F83F83F07EL03F801FE001FF8K01FC00FC01F8I0FC,gL0F81F87F07EL01FC01FCI0FF8L0FC00FC01F8I0FC,R0ES0F80FC7E07EM0FC03F8I07F8L0FC00FC01F8I0FC,Q01FS0F80FEFC07EM0FC03FJ03F8L07E00FC01F8I0FC,Q03F8R0F807FFC07EM0FC03FJ03F8L07E00FC01F8I0FC,Q03FCR0F803FF807EM0FC03FJ01F8L07E00FC01F8I0FC,Q03FER0F803FF007EM07C07EJ01F8L07E00FC01F8I0FC,Q03FFR0F801FF007EI03FF8FC07EJ01F8001FFC7E00FC01F8I0FC,Q01FF8Q0F800FE007EI0KFC07EJ01F800KFE00FC01F8I0FC,R0FFCQ0F800FC007E003KFC07EJ01F801KFE00FC01F8I0FC,R07FEQ0F80078007E007F803FC03FJ01F803F801FE00FC01F8I0FC,R03FFQ0F80038007E00FEI0FC03FJ03F807FI07E00FC01F8I0FC,R01FF8P0F8003I07E00FCI07C03FJ03F80FEI07E00FC01F8I0FC,S0FFCK0EJ0F8L07E01F8I07C03F8I07F80FCI07E00FC01F8I0FC,S07FFK0EJ0F8L07E01F8I07C01FCI0FF80FCI07E00FC01FCI0FC,S03FF8J0EJ0F8L07E01F8I0FC01FE001FF80F8I07E00FC01FC001FC,S01FFCJ0EJ0F8L07E01F8I0FC00FF003FF80F8I07E00FC01FC001FC,T0FFEJ0EJ0F8L07E01F8I0FC007FF3F9F80FCI0FE00FC01FE003FC,T07FFJ0EJ0F8L07E01FC001FC003JF1F80FC001FE00FC00FF007FC,T07FF8I0EJ0F8L07E00FC003FC001IFE1F80FE003FE00FC00FF80FFC,O07MFCI0EJ0F8L07E00FF00FFCI07FF81F807F007FE00FC007IFEFC,N01NFCI0EJ0F8L07E007IFEFCJ0FE01F807JF7E00FC007IFCFC,N03NFEI0EJ0F8L07E003IFCFCM01F003IFC7E00FC003IF8FC,N03NFEI0EJ0F8L07E001IF07CM03FI0IF87E00FCI0IF0FC,N03NFEI0EW03FCP03FI03FCP03F8,N01NFCI0EgP07F,O0NF8I0EgP07E,T0IFJ0EgK04J0FE,T07FEJ0EgK0F8001FC,T0FFCJ0EgK0FF00FF8,S01FFCJ0EgK0LF,S01FF8J0EgK07JFE,J018M03FFK0EgK01JF8,J018M07FEK0EgL03FFC,J018M0FFCK0E,J018L01FF8K0E,J018L03FFL0E,J018L07FEL0E,J018L0FFCL0E,J018K01FF8L0E,J018K03FFM0E,J018K03FEM0E,J018K03FCM0ET04,J018K01F8M0ET04,J018K01FN0ET04,J018U0ET04,:J018U0EJ01880105C03FE061C021I0310C01FC0033,J018U0EJ04060100300400630080400C00CI0300C,J018U0EJ08030180100400640100601I0CI01808,J018U0EI010010180180400700300203001CJ0808,J018U0EI01I08180080400700200102I0CJ0818,J018U0EI03I081I080400700600106I0CJ0808,J018U0EI02I081I080400600400104I0CJ0806,J018U0EI03I081I080400600600104I0C01008038,J018U0EI03J01I0804006007J04I0C04008003,J018U0EI03J01I0804006006J06I0C08008I0C,J01CT01EI02J01I0804006006J02I0C18008I06,J01CT01CI01J01I0804006002J03001C18008I02,K0ET03CI01J01I0804006002J01800C18008I02,K07KFCL03FF8J08I01I0804006001K0C00C08018I02,K03UFK040101I0806006I0802001E0C0C0081004,R0NFCK020601I0803006I0404K08060080408,gM07V0EL08004J04,hQ08,hP018,hP03,hN0804,hN03F,,::::::::::::::::::::::::^FS";

            string sResult = "";
            string sEtiqueta = @"^XA
^CI28
^FO50,50^GFA,5200,5200,40,,::::::::::::::::::iK0FC,L01LF8R0FCL07EgO0FC,L01LFCR0FEL0FEgO0FC,L01LFCR0FFK01FEgO0FC,gL0FFK03FEgO0FC,gL0FF8J03FEgO0FC,gL0FFCJ07FEgO0FC,gL0FFCJ0FFEgO0FC,gL0FFEJ0FFEgO0FC,gL0FFEI01FFEgO0FC,gL0IFI01FFEgO0FC,gL0IF8003FFEJ07F8K01FEM07FCJ0FC,gL0IF8007F7EI07IFK07FF81F8007IF8I0FC01F8I0FC,gL0F8FC007E7E003JFCI01IFE1F801JFEI0FC01F8I0FC,gL0F8FE00FC7E003JFEI03JF1F801KFI0FC01F8I0FC,gL0F87E01FC7E001KFI07FE1IF801KF800FC01F8I0FC,gL0F83F01F87E001E00FF800FF003FF800E007F800FC01F8I0FC,gL0F83F83F07EL03F801FE001FF8K01FC00FC01F8I0FC,gL0F81F87F07EL01FC01FCI0FF8L0FC00FC01F8I0FC,R0ES0F80FC7E07EM0FC03F8I07F8L0FC00FC01F8I0FC,Q01FS0F80FEFC07EM0FC03FJ03F8L07E00FC01F8I0FC,Q03F8R0F807FFC07EM0FC03FJ03F8L07E00FC01F8I0FC,Q03FCR0F803FF807EM0FC03FJ01F8L07E00FC01F8I0FC,Q03FER0F803FF007EM07C07EJ01F8L07E00FC01F8I0FC,Q03FFR0F801FF007EI03FF8FC07EJ01F8001FFC7E00FC01F8I0FC,Q01FF8Q0F800FE007EI0KFC07EJ01F800KFE00FC01F8I0FC,R0FFCQ0F800FC007E003KFC07EJ01F801KFE00FC01F8I0FC,R07FEQ0F80078007E007F803FC03FJ01F803F801FE00FC01F8I0FC,R03FFQ0F80038007E00FEI0FC03FJ03F807FI07E00FC01F8I0FC,R01FF8P0F8003I07E00FCI07C03FJ03F80FEI07E00FC01F8I0FC,S0FFCK0EJ0F8L07E01F8I07C03F8I07F80FCI07E00FC01F8I0FC,S07FFK0EJ0F8L07E01F8I07C01FCI0FF80FCI07E00FC01FCI0FC,S03FF8J0EJ0F8L07E01F8I0FC01FE001FF80F8I07E00FC01FC001FC,S01FFCJ0EJ0F8L07E01F8I0FC00FF003FF80F8I07E00FC01FC001FC,T0FFEJ0EJ0F8L07E01F8I0FC007FF3F9F80FCI0FE00FC01FE003FC,T07FFJ0EJ0F8L07E01FC001FC003JF1F80FC001FE00FC00FF007FC,T07FF8I0EJ0F8L07E00FC003FC001IFE1F80FE003FE00FC00FF80FFC,O07MFCI0EJ0F8L07E00FF00FFCI07FF81F807F007FE00FC007IFEFC,N01NFCI0EJ0F8L07E007IFEFCJ0FE01F807JF7E00FC007IFCFC,N03NFEI0EJ0F8L07E003IFCFCM01F003IFC7E00FC003IF8FC,N03NFEI0EJ0F8L07E001IF07CM03FI0IF87E00FCI0IF0FC,N03NFEI0EW03FCP03FI03FCP03F8,N01NFCI0EgP07F,O0NF8I0EgP07E,T0IFJ0EgK04J0FE,T07FEJ0EgK0F8001FC,T0FFCJ0EgK0FF00FF8,S01FFCJ0EgK0LF,S01FF8J0EgK07JFE,J018M03FFK0EgK01JF8,J018M07FEK0EgL03FFC,J018M0FFCK0E,J018L01FF8K0E,J018L03FFL0E,J018L07FEL0E,J018L0FFCL0E,J018K01FF8L0E,J018K03FFM0E,J018K03FEM0E,J018K03FCM0ET04,J018K01F8M0ET04,J018K01FN0ET04,J018U0ET04,:J018U0EJ01880105C03FE061C021I0310C01FC0033,J018U0EJ04060100300400630080400C00CI0300C,J018U0EJ08030180100400640100601I0CI01808,J018U0EI010010180180400700300203001CJ0808,J018U0EI01I08180080400700200102I0CJ0818,J018U0EI03I081I080400700600106I0CJ0808,J018U0EI02I081I080400600400104I0CJ0806,J018U0EI03I081I080400600600104I0C01008038,J018U0EI03J01I0804006007J04I0C04008003,J018U0EI03J01I0804006006J06I0C08008I0C,J01CT01EI02J01I0804006006J02I0C18008I06,J01CT01CI01J01I0804006002J03001C18008I02,K0ET03CI01J01I0804006002J01800C18008I02,K07KFCL03FF8J08I01I0804006001K0C00C08018I02,K03UFK040101I0806006I0802001E0C0C0081004,R0NFCK020601I0803006I0404K08060080408,gM07V0EL08004J04,hQ08,hP018,hP03,hN0804,hN03F,,::::::::::::::::::::::::^FS


^CFA,20
^FO40,200^FDNota Fiscal: {NotaFiscal}^FS
^CFA,40

^BY3,2,150^FD^FS


^FO40,250^BC^FD{Barra}^FS

^CFA,20
^FO40,450^FDRecebedor: _______________________________________________^FS
^FO40,480^FDAssinatura: ________________ Documento: __________________^FS


^FO40,510^GB700,1,3^FS

^CFA,40
^FO40,530^FDDESTINATARIO^FS

^FO600,530^GFA,1144,1144,22,,:::L07IF,L07IF8,L03IFC,L01IFE,M0JF,:M07IF8,M03IFC,M01IFE,N0IFE,I03IF0IFE,I0JF1IFE003FC,001JF3IFC00FFE,001IFE3IFC01FFE,003IFC7IF803E0C01C,007IF8JF007CI07F803E1F0FF01C0FF01FE,00JF1IFE00F8001FFC0FE7F1FFC1C1FF83FF,00JF3IFE00FI03FFE0FC7E3FFE1C3FFC3FE,01IFE3IFC00EI03C1F1F0F07C1E1C7C3E38,03IFC7IF800EI0780F9E0F0780F1CF01F78,03IF87FFEI0EI070079E0F0F00F1CF00F3C,03IFCM0EI070039E0F0JF9CE0073F8,03IFCM0EI0F0039E0F0JF9CE0071FE,01IFEM0EI0F0039E0F0JF9CE0070FF,00JFM0FI070079E0F0JF1CE00700F,007IF8L0780078079E0F07I01CF00F0078,003IF8L07C00780F1E0F078001C781E0078,003IFCL03F1C3E3F1E0F07C301C7C7E38F8,001IFEL01FFE1FFE1E0F03FF81C3FFC3FF,I0JFM0FFE0FFC1E0F01FF81C1FF83FE,I07IFM03F807F01C0E007F01C07E00FC,I03IFN0F001E00C06001E01803C0078,,:::::::::::::::^FS


^CFA,30
^FO40,570^FD{NomeDestinatario}^FS
^FO40,600^FD{EnderecoDestinatario}^FS
^FO40,630^FD{BairroDestinatario}^FS
^FO40,660^FD{ComplementoDestinatario}^FS
^FO40,690^FD{Cidade/UFDestinatario}^FS
^FO40,720^FDCEP:{Cep} ^FS
^CFA,20
^FO40,900^FDPedido:{Pedido}
 ^FS

^FO400,800^BC^FD{Cep}^FS

^FO40,1000^GB700,1,3^FS
^CFA,40
^FO40,1010^FDREMETENTE^FS

^CFA,30
^FO40,1040^FD{NomeRemetente}^FS
^FO40,1070^F{EnderecoRemetente}^FS
^FO40,1100^FD^FS
^FO40,1130^FD^FS
^FO40,1160^FD{CepRemetente/Cidade/UFRemetente}^FS


^XZ ";
            string sImagem = sImagemMAGALU;


            sResult = sEtiqueta.Replace("{NotaFiscal}", oJsonMAGALU.InvoicedNumber)
                               .Replace("{Volume}", "")
                               .Replace("{Imagem}", sImagem)
                               .Replace("{Direct}", "")
                               .Replace("{Pedido}", oJsonMAGALU.IdOrder.Split('-')[1])
                               .Replace("{Data}", "")
                               .Replace("{Barra}", oJsonMAGALU.ShippedTrackingProtocol)
                               .Replace("{NomeDestinatario}", oJsonMAGALU.CustomerPfName)
                               .Replace("{EnderecoDestinatario}", oJsonMAGALU.DeliveryAddressStreet + "-" + oJsonMAGALU.DeliveryAddressNumber)
                               .Replace("{BairroDestinatario}", oJsonMAGALU.DeliveryAddressNeighborhood)
                               .Replace("{ComplementoDestinatario}", oJsonMAGALU.DeliveryAddressAdditionalInfo)
                               .Replace("{Cidade/UFDestinatario}", oJsonMAGALU.DeliveryAddressCity + "/" + oJsonMAGALU.DeliveryAddressState)
                               .Replace("{Cep}", oJsonMAGALU.DeliveryAddressZipcode)
                               .Replace("{NomeRemetente}", oAnymarket.NomeRemetente)
                               .Replace("{EnderecoRemetente}", oAnymarket.EnderecoRemetente)
                               //.Replace("{Serie}", oAnymarket.docsExternos[0].megaRota)
                               //.Replace("{Rota}", oAnymarket.docsExternos[0].rota)
                               //.Replace("{BairroRemetente}", r["REMETENTE"].ToString())
                               //.Replace("{ComplementoRemetente}", r["REMETENTE"].ToString())
                               .Replace("{CepRemetente/Cidade/UFRemetente}", oAnymarket.CEPRemetente + "-" + oAnymarket.CidadeRemetente + "-" + oAnymarket.UFRemetente);

            return sResult;
        }

        private string GeraEtiquetaB2W(EtiquetaJSON oAnymarket, string sNomeMarketplace, string sTipoEnvio)
        {
            string sImagemAmericanas = "^FO50,50^GFA,3712,3712,29,,::::::::::::::::::::::::::gN01,gN038,,X07006180E0080300C00C0701CI018,W01FC7FFE3F9FB8FC3F1FE1FC7FI078,W018E73CE71CFB9CE739E739CE3I0OF8,W010E618660CE3986619C330CEI03OF8,X01E618660DC3B800398301CF8007OF8,W01FE61867FFC3B803F9831FC7F00PF8,W038E6186601C3B807198338C1F00PF8,W030E6186600C3986E198330C03807OF8,W038E618671CC39CEE398331CC3801OF8,W03FE61863F9C39FC7F9833FCFFI0OF8,W01E661861F081078399831C43EI038,hK018,,:::X01FE,:X01FEgN07C,X01FEgM0IFE,I0IFM0FF8003FEK07E003FFCK07FCK03JF8,003IFE003FC3FFE03JF1FE0FE00JF8I01IF1FF007JFE,00KF003FCJF83JF1FE1FE03JFCI07IF9FF00LF,01KFC03FCJFC3JF1FE3FE07KFI0JFDFF01LF,03KFE03FDJFC3JF1FE7FE0LF801MF01LF8,07KFE03LFE3JF1JFE1LFC01MF03FF81FF8,0MF03LFE3JF1JFE3LFC03MF03FE00FF8,0FFC03FF83FFC0FFE03FE01JFE3FF00FFE07FF83IF03FC007FC,1FF801FF83FF003FF01FE01IFC07FE003FE07FE00IF03FC007FC,1FFI0FFC3FF003FF01FE01FFE007FC001FF07FC007FF03F8007FC,3FEI07FC3FE001FF01FE01FFC007F8001FF0FFC003FFL07FC,3FEI07FC3FE001FF01FE01FF800FF8I0FF0FF8003FFL0FFC,3FCI03FC3FC001FF01FE01FF800FF8I0FF8FF8001FFJ01IFC,3FCI03FE3FC001FF01FE01FFI0FFJ0FF8FF8001FFI07JFC,3MFE3FC001FF01FE01FFI0NF9FFI01FF003KFC,3MFE3FC001FF01FE01FFI0NF9FFI01FF00LFC,7MFE3FC001FF01FE01FFI0NF9FFI01FF01LFC,7MFE3FC001FF01FE01FFI0NF9FFI01FF03IF87FC,7MFE3FC001FF01FE01FFI0NF9FFI01FF07FF007FC,7MFE3FC001FF01FE01FFI0NF9FFI01FF07FC007FC,3FCL03FC001FF01FE01FFI0FFL01FFI01FF07F8007FC,3FCL03FC001FF01FE01FFI0FFM0FF8001FF0FF8007FC,:3FEL03FC001FF01FE01FFI0FF8L0FF8003FF0FF8007FC,3FEL03FC001FF01FE01FFI0FF8L0FFC007FF0FF800FFC,1FFI07FC3FC001FF01FE01FFI07FC001FF07FE007FF0FF801FFC,1FF800FFC3FC001FF01FE01FFI07FE003FF07FF01IF07FC03FFC,1FFC01FF83FC001FF01FE01FFI03FF007FE07MF07FF1IFC,0IF8IF83FC001FF01IF1FFI03FFE3FFE03MF07LFC,07LF03FC001FF01IF1FFI01LFC01MF03JF3FC,03KFE03FC001FF01IF1FFJ0LF800JFDFF01IFE3FC,01KFC03FC001FF01IF1FFJ07KFI07IF9FF00IFC3FE,00KF803FC001FF00IF1FFJ03JFEI03IF1FF007FFI02,003IFE007FC001FF007FF1FFK0JF8J0FFE1FF,I0IF8S0FFN03FFEN01FF,hH01FF,:hH01FE,gW07F8003FE,gW0FFC003FE,gW0FFE007FEN01,gW07FF01FFCN018,gW07LFC3NFE,gW03LF83OF,gW01LF03OFC,gX0KFE03OFE,gX03JF803PF,gY07FFC003OFE,hK03OF8,hK03OF,hS03C,hS018,hS01,,::::::::::::::::::::::::^FS";
            string sImagemSubmarino = "^FO50,50^GFA,05760,05760,00036,:Z64:eJztlcFqhDAQhpNAQHLaHnKXnBafInvYewTnfYaeQp9CepI8ZWeiS3VrG6FSKOTfVVRmv/3zz6hCVFVVVVVV/b2aWK6xwzn/BVCuafAA5yw/B2qakzhH9A85TYxYKNEA4E9ww5xQKsKIpYFmzhkDZA9wKJ5Y8nOEU5aUzAEpf6hB5Hhoj9+WcDizfmNJevjU4+Jz7zCHM+thaNyW2NuK4xd2cQZI1+2pvq844RvOHM78wXzjN2IS+QDzl37zxAE/gIa7liBs73enU7m2dcoppZxwrnUuc1YYClpDAGC05YOwRBbXEuZ17BKmt0QHyYxT2uPcAtCmByvDoL23e35E29GmWqNa2tP5V44Vw8Acb2UfZNAzZxUP9cvE6apwMtjFcVSjSficD3Fg4QAQx+8+/jkf9sPhkJ85n6d+USyLH+LofuFgXM+PoabPfoSYKB/DfvRmfi60joXD76Jb2MmnNYL9XFUOiiyZnM+GM+ie/QBx+sBLe3BW8zOpNJKf9IYmjd079SsPkFjfF4PO64K71+D1fTefVrgcDrWJkqF81Hxdvjx0ERd6UF8kNSs/scmV3sl5u0gsVLBRX6xxXbFEHnnnm6lco8t2qqqqqqqqqg7qAxEowaE=:CA3C";
            string sImagemShoptime = "^FO50,50^GFA,05760,05760,00036,:Z64:eJztl9GKm0AUhs9IJUUo5KLSK6HkKvgUFvRewcGb+C5z3aeQXsk8RfYNXOhCLzrv0v+MiRmNbSwJXZZ6lolk0G///c8/J1mitdZaa623XOK1Baz1puv+/KRpmmVZejdH2iofoCcD5349RcF68kfoybLyb/VEVzvX/sQLOOG1nkyWMnX1LOFc67H2FMUD9CA9i/WEL4hs2IVR+GX7juqORC23AlfHn1iTp+I20Fo170m35GmNd63LkVWyyapyg2v+0ZeSNnj2k6yIbHqsHq0b76vWlhODQYHlaHXBCGM63xgTGvP0bD4Yc4yM+Y51pN4e5MdrgwZPWw5e21jFSqtAuRw/EbkvSbI/Mg8TPymxx/tk08N6PEWt18Id+KObgLxmz3u4OvYc6affoVsRjOki8rsDFvEa/AmYo04cFeP51jJGnIRK/O6Q9ZT48fNBj00P63E5LGuOEyk6+Mdezw+qSTwfSECPsHoKnLF5Dk310FiPgB5x9qc86fm2czm7Hfsz1WPM4M+BanTv0Hvj+oMuOxykwPqM64WDpJSbQU8lK+jZJJwmsunJZjkx/tZgxIleZOn4g3lTFzVniPWwPX2/yPXHkiec0Gbo4g92kGnOEfHhSrN0juPp3u9Rv8jxhzmQUk/zM9WjaMIJkys9WNLmx9ozy+mXy4E3dPGHhxCvXk9hJzT1Z2DgBAMncDk5bS96oC6RVo9MyNrD/nhDv7Rq2eMGg4TGPoshP+bpQDjvHXpFp36d58+ZEzAH1KbvV+tw6Jwfv+KTztNISqzC6sl6ToA5RrQDgvb9VOOJpOmzwxGnASyktT0ngYb1b/5Urse/K7Hks28Jp1vAaW/f8hg9+VjP7Pef/Sg787Xku0E8ys58YQbdusWe05uFKK/1z2v9/2ute2rNz/9avwBu5THV:C551";
            string sDirectSEDEX = "^FO600,50^GFA,02048,02048,00016,:Z64:eJzlVEEOwyAMqzjyCrTTxCt5StXTlFduKVAyyQ7bKvUyc7Hkuk5Im2X5M5Q3kIeyyOq8I8oLzKt2RfLtzgvyJnpoBdUuDyKH3ODH0wJy10kBXSYFHPGkgCNeBOu1ez2wQOOH+ojHIxh23MBZ3eTfgRyMH11AkNH/9ovf5KMBzPM/0EsbA8lPSnh+uzbaf6kskv6PWyf5qVOSXzqNOB9Ro5uhQz0NDvPL4BHphF+KQP7NDmf77HZn+yhm+/Om3/7qxnsFBHf9zffvWd3bPrvurtf5/p/5v8ETe7zP1w==:C098";
            string sDirectPAC = "^FO600,50^GFA,02048,02048,00016,:Z64:eJztVDluwzAQpBQTBtIL/oNesU9QIYbfUGH2eorKgK+0zGt3CJpOkSJFVo0Wg+HMLA+l/mTZ9SLbT+8P0Q7GmFX0oz9r535yZ23nD8X+CftvoJ+FdLHAzYai3M8RLw4inR34VGV5Fz9cvgjoxDdUwoFAkrd2AfmSINOzAe/BwOBKhfxjhRf5NAGBB4NXW4rAXjI4MX9p4i7nNxvYTwFMxa9xlrcrxgsBBsFv4hzf3anPNxg/4B9C3zbwLn9/jpfzuybe5197+j/ga5Hf0e/rv5vf0MvfmF+9P1rIfzVweX7q/Q/np97/ucLfnb8b67fPN/Pr+xHuj+b81LpfYgDt+4vxBf7i/s8VPuX4m0KDh8IHgBQ+ALtCA0qBgfKAyukJgSP3WqQXArlNExAP+AzLpzNI3I9Aj3uwyF4JmOD7r349AHy0+xI=:016D";

            string sResult = "";
            string sEtiqueta = @"^XA 
^CI28
{Imagem}
{Direct}

^CFA,20
^FO40,200^FDNota Fiscal: {Notafiscal}^FS
^FO600,200^FDVolume: {Volume}^FS
^FO40,220^FDPedido: {Pedido}^FS
^FO40,240^FDPLP: {PLP}^FS
^CFA,40

^BY3,2,150^FD^FS


^FO140,290^BC^FD{Barra}^FS

^CFA,20
^FO40,500^FDRecebedor: _______________________________________________^FS
^FO40,540^FDAssinatura: ________________ Documento: __________________^FS


^FO40,590^GB700,1,3^FS

^CFA,40
^FO40,605^FDDESTINATARIO^FS

^CFA,30
^FO40,640^FD{NomeDestinatario}^FS
^FO40,670^FD{EnderecoDestinatario}^FS
^FO40,700^FD{BairroDestinatario}^FS
^FO40,730^FD{ComplementoDestinatario}^FS
^FO40,760^FD{CepDestinatario/Cidade/UFDestinatario}^FS

^BY3,2,100^FD^FS
^FO40,850^BC^FD{Cep}^FS
^FO200,800^FD{Serie}^FS

^FWB
^FO 700,820^AD,27^FH^FD{Rota}^FS 
^FWN

^FO40,1000^GB700,1,3^FS
^CFA,40
^FO40,1010^FDREMETENTE^FS

^CFA,30
^FO40,1040^FD{NomeRemetente}^FS
^FO40,1070^FD{EnderecoRemetente}^FS
^FO40,1100^FD{BairroRemetente}^FS
^FO40,1130^FD^FS
^FO40,1160^FD{CepRemetente/Cidade/UFRemetente}^FS


^XZ"; ;
            string sImagem = "";
            string sImagemDirect = "";
            if (sNomeMarketplace == "Lojas Americanas")
                sImagem = sImagemAmericanas;
            else if (sNomeMarketplace == "Shoptime")
                sImagem = sImagemShoptime;
            else if (sNomeMarketplace == "Submarino")
                sImagem = sImagemSubmarino;
            if (sTipoEnvio.ToUpper() == "SEDEX")
                sImagemDirect = sDirectSEDEX;
            else if (sTipoEnvio.ToUpper() == "PAC" || sTipoEnvio.ToUpper() == "NORMAL")
                sImagemDirect = sDirectPAC;

            sResult = sEtiqueta.Replace("{Notafiscal}", oAnymarket.docsExternos[0].numNotaFiscal)
                               .Replace("{Volume}", oAnymarket.docsExternos[0].qtVolumes.ToString())
                               .Replace("{Imagem}", sImagem)
                               .Replace("{Direct}", sImagemDirect)
                               .Replace("{Pedido}", oAnymarket.docsExternos[0].docExterno)
                               .Replace("{PLP}", oAnymarket.plp.id.ToString())
                               .Replace("{Data}", "")
                               .Replace("{Barra}", oAnymarket.docsExternos[0].awbs[0].codigoAwb)
                               .Replace("{NomeDestinatario}", oAnymarket.docsExternos[0].destinatario.nome)
                               .Replace("{EnderecoDestinatario}", oAnymarket.docsExternos[0].destinatario.enderecoLogradouro + "-" + oAnymarket.docsExternos[0].destinatario.enderecoNumero)
                               .Replace("{BairroDestinatario}", oAnymarket.docsExternos[0].destinatario.enderecoBairro)
                               .Replace("{ComplementoDestinatario}", oAnymarket.docsExternos[0].destinatario.enderecoComplemento)
                               .Replace("{CepDestinatario/Cidade/UFDestinatario}", oAnymarket.docsExternos[0].destinatario.enderecoCep + "-" + oAnymarket.docsExternos[0].destinatario.enderecoCidade + "/" + oAnymarket.docsExternos[0].destinatario.enderecoUf)
                               .Replace("{Cep}", oAnymarket.docsExternos[0].destinatario.enderecoCep.PadLeft(8, '0'))
                               .Replace("{NomeRemetente}", oAnymarket.docsExternos[0].remetente.nome)
                               .Replace("{EnderecoRemetente}", oAnymarket.docsExternos[0].remetente.enderecoLogradouro + " Nº: " + oAnymarket.docsExternos[0].remetente.enderecoNumero)
                               .Replace("{Serie}", oAnymarket.docsExternos[0].megaRota)
                               .Replace("{Rota}", oAnymarket.docsExternos[0].rota)
                               .Replace("{BairroRemetente}", oAnymarket.docsExternos[0].remetente.enderecoBairro)
                               //.Replace("{ComplementoRemetente}", r["REMETENTE"].ToString())
                               .Replace("{CepRemetente/Cidade/UFRemetente}", oAnymarket.docsExternos[0].remetente.enderecoCep.PadLeft(8, '0') + "-" + oAnymarket.docsExternos[0].remetente.enderecoCidade + "-" + oAnymarket.docsExternos[0].remetente.enderecoUf);

            return sResult;
        }

        private void ExportJpegImage(PdfDictionary image, ref int count)
        {
            // Fortunately JPEG has native support in PDF and exporting an image is just writing the stream to a file.
            byte[] stream = image.Stream.Value;
            FileStream fs = new FileStream(String.Format("Image{0}.jpeg", count++), FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(stream);
            bw.Close();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {

        }



        private void BtnClientes_Click(object sender, EventArgs e)
        {
            lAberto = false;
            if (oServidor != null)
                oServidor.Stop();
            if (oServidorConsulta != null)
                oServidorConsulta.Stop();

            frmClientes oFrm = new frmClientes();
            oFrm.Show();
        }

        public void SalvaNota(string sArquivo, int iCliente)
        {
            Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

            try
            {
                bool lLivre = false;
                while (!lLivre)
                {
                    FileStream stream = null;
                    try
                    {
                        stream = File.Open(sArquivo, FileMode.Open, FileAccess.Read, FileShare.None);
                        lLivre = true;
                    }
                    catch (IOException)
                    {
                        //the file is unavailable because it is:
                        //still being written to
                        //or being processed by another thread
                        //or does not exist (has already been processed)
                        lLivre = false;
                    }
                    finally
                    {
                        if (stream != null)
                            stream.Close();
                    }

                    //file is not locked
                }

                string sPasta = Directory.GetCurrentDirectory();
                string sPastaXML = Path.GetDirectoryName(sArquivo);
                string sPastaPDFBACKUP = sPastaXML + "PDFBACKUP\\";
                if (!Directory.Exists(sPastaPDFBACKUP))
                {
                    Directory.CreateDirectory(sPastaPDFBACKUP);
                }

                string sPastaDataHoje = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "\\";
                if (!Directory.Exists(sPastaPDFBACKUP + sPastaDataHoje))
                {
                    Directory.CreateDirectory(sPastaPDFBACKUP + sPastaDataHoje);
                }

                string sPastaSalvaDANFE = sPastaPDFBACKUP + sPastaDataHoje;
                string sTextoXML = File.ReadAllText(sArquivo);
                DanfeViewModel oModel = DanfeViewModelCreator.CriarDeArquivoXml(sArquivo);
                if (iCliente == 3)
                    oModel.Duplicatas.Clear();

                //Inicia o Danfe com o modelo criado
                Danfe danfe = new Danfe(oModel);
                danfe.Gerar();
                danfe.Salvar(sPastaSalvaDANFE + Path.GetFileName(sArquivo.Split('.')[0] + ".pdf"));
                danfe.Dispose();

                string sChaveNota = oModel.ChaveAcesso;
                string sNota = oModel.NfNumero.ToString();
                string sDataEmissao = oModel.DataHoraEmissao.ToString();
                string sCNPJ = oModel.Emitente.CnpjCpf;

                DateTime dEmissao = DateTime.Parse(sDataEmissao);

                byte[] oPDFStream = File.ReadAllBytes(sPastaSalvaDANFE + Path.GetFileName(sArquivo.Split('.')[0] + ".pdf"));
                string sSql = "";
                sSql = "INSERT INTO NOTAMASTER(NOTA,CHAVENOTA,DATA,FINALIZADO,CNPJ,XML,PDF) VALUES(@NOTA,@CHAVENOTA,@DATA,@FINALIZADO,@CNPJ,@XML,@PDF)";
                ParametrosSQL.Add("NOTA", int.Parse(sNota).ToString());
                ParametrosSQL.Add("CHAVENOTA", sChaveNota);
                ParametrosSQL.Add("DATA", dEmissao.ToString("yyyy-MM-dd HH:mm:ss"));
                ParametrosSQL.Add("FINALIZADO", "0");
                ParametrosSQL.Add("CNPJ", sCNPJ);
                ParametrosSQL.Add("XML", sTextoXML);
                ParametrosSQL.Add("PDF", oPDFStream);
                ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);


                foreach (ProdutoViewModel oProduto in oModel.Produtos)
                {
                    string sEAN = oProduto.EAN;
                    sSql = "INSERT INTO NOTADETALHE(NOTA,PRODUTO,CNPJ) VALUES(@NOTA,@PRODUTO,@CNPJ)";
                    ParametrosSQL.Clear();
                    ParametrosSQL.Add("NOTA", int.Parse(sNota).ToString());
                    ParametrosSQL.Add("PRODUTO", sEAN);
                    ParametrosSQL.Add("CNPJ", sCNPJ);
                    ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);
                }

                oModel = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                string sPastaXMLBACKUP = sPastaXML + "XMLBACKUP\\";

                if (!Directory.Exists(sPastaXMLBACKUP))
                {
                    Directory.CreateDirectory(sPastaXMLBACKUP);
                }

                if (!Directory.Exists(sPastaXMLBACKUP + sPastaDataHoje))
                {
                    Directory.CreateDirectory(sPastaXMLBACKUP + sPastaDataHoje);
                }

                File.Copy(sArquivo, sPastaXMLBACKUP + sPastaDataHoje + Path.GetFileName(sArquivo), true);
                File.Delete(sArquivo);
            }

            catch (Exception ex)
            {
                ClasseParametros.MostraErro(ex.Message, iconApp);
            }
        }

        private void SalvaB2W(string sCodigo)
        {
            //try
            //{
            //    DataTable dtbCliente = ClasseParametros.ConsultaBancoMysql(@"SELECT PORANYMARKET, USUARIOMAGALU, SENHAMAGALU, PORARQUIVO, CODIGO, CLIENTE, IDMERCADOLIVRE, SENHAMERCADOLIVRE, CODEMERCADOLIVRE, ACCESSTOKENMERCADOLIVRE,
            //                                                                   REFRESHTOKENMERCADOLIVRE, KEYB2W, USUARIOB2W,ACCOUNTMANAGERB2W, REMETENTE, ENDERECO, CEP, CIDADE, UF, GUMGATOKENANYMARKET, PASTA, PASTAXML, ACCOUNTMANAGERB2W
            //                                                        FROM CLIENTE WHERE CODIGO = " + sCodigo + " ORDER BY PORARQUIVO DESC");
            //    InterPlaceModel oBase = new InterPlaceModel();
            //    DbSet<CLIENTE> oCliente = oBase.CLIENTE;
            //    List<Content> lstContent = new List<Content>();
            //    Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

            //    RestRequest oRequest = null;
            //    IRestResponse oResposta = null;
            //    RestClient client = null;

            //    int iTotalEtq = 0;

            //    foreach (DataRow r in dtbCliente.Rows)
            //    {
            //        try
            //        {
            //            int iCodigoCliente = int.Parse(r["CODIGO"].ToString());
            //            if (VerificarManutencaoAberto())
            //            {
            //                return;
            //            }

            //            int iPorArquivo = 0;
            //            if (r["PORARQUIVO"].ToString() != "")
            //                iPorArquivo = int.Parse(r["PORARQUIVO"].ToString());

            //            if (iPorArquivo == 1)
            //            {
            //                SalvaNota(int.Parse(r["CODIGO"].ToString()), r["PASTA"].ToString());
            //                continue;
            //            }

            //            lblAPICount.BeginInvoke((MethodInvoker)delegate ()
            //            {
            //                lblAPICount.Text = iTotalEtq.ToString();
            //            });

            //            Application.DoEvents();
            //            if (r["PASTA"].ToString() == "")
            //            {

            //                #region B2W
            //                DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM CLIENTE WHERE CODIGO = " + r["CODIGO"].ToString());
            //                string sKey = d.Rows[0]["KEYB2W"].ToString();
            //                string sUsuario = d.Rows[0]["USUARIOB2W"].ToString();
            //                string sAccount = d.Rows[0]["ACCOUNTMANAGERB2W"].ToString();
            //                d.Dispose();

            //                int iPagina = 1;
            //                int iPorPagina = 50;
            //                int iTotal = 0;

            //                while (true)
            //                {
            //                    oResposta = null;
            //                    while (oResposta == null)
            //                    {
            //                        client = new RestClient("http://api.skyhub.com.br/orders?filters[statuses][]=order_invoiced&page=" + iPagina.ToString() + "&per_page=" + iPorPagina.ToString());
            //                        //client = new RestClient("http://api.skyhub.com.br/orders?page=" + iPagina.ToString() + "&per_page=" + iPorPagina.ToString());
            //                        //client = new RestClient("http://api.skyhub.com.br/orders?page=0&per_page=50");
            //                        oRequest = new RestRequest(Method.GET);
            //                        oRequest.AddHeader("cache-control", "no-cache");
            //                        oRequest.AddHeader("Accept", "application/json");
            //                        oRequest.AddHeader("Content-Type", "application/json");
            //                        oRequest.AddHeader("x-Api-Key", sKey);
            //                        oRequest.AddHeader("X-User-Email", sUsuario);
            //                        oRequest.AddHeader("X-Accountmanager-Key", sAccount);
            //                        oResposta = client.Execute(oRequest);

            //                        if (oResposta.StatusCode == HttpStatusCode.BadGateway || oResposta.StatusCode == HttpStatusCode.GatewayTimeout || oResposta.StatusCode == HttpStatusCode.InternalServerError || oResposta.StatusCode == 0)
            //                        {
            //                            oResposta = null;
            //                        }
            //                    }
            //                    Thread.Sleep(200);

            //                    if (oResposta.Content == "Account  not found")
            //                    {
            //                        continue;
            //                    }
            //                    if (oResposta.Content.Contains("504 Gateway Time-ou"))
            //                    {
            //                        continue;
            //                    }

            //                    B2WPedidos lstPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<B2WPedidos>(oResposta.Content);
            //                    if (lstPedido.orders == null || lstPedido.orders.Length == 0)
            //                    {
            //                        break;
            //                    }

            //                    foreach (B2WPedido oPedido in lstPedido.orders)
            //                    {

            //                        if (oPedido.invoices[0].key == "35200508355660000105550010000807741348889004")
            //                        {

            //                        }


            //                        Content oItemAnymarket = new Content();
            //                        InterRegraNegocio.Invoice oInvoice = new InterRegraNegocio.Invoice();
            //                        oInvoice.accessKey = oPedido.invoices[0].key;
            //                        oItemAnymarket.marketPlaceId = oPedido.code;
            //                        oItemAnymarket.invoice = oInvoice;
            //                        oItemAnymarket.CodigoCliente = iCodigoCliente;
            //                        oItemAnymarket.marketPlace = "B2W";
            //                        oItemAnymarket.subChannel = "";
            //                        lstContent.Add(oItemAnymarket);
            //                    }

            //                    iTotal += lstPedido.orders.Length;
            //                    lblAPICount.BeginInvoke((MethodInvoker)delegate ()
            //                    {
            //                        lblAPICount.Text = "Add " + iTotal.ToString() + "";
            //                    });
            //                    Application.DoEvents();

            //                    iPagina += 1;
            //                }
            //                #endregion

            //                #region MERCADOLIVRE
            //                //if (ClasseParametros.oMeli == null)
            //                //    ClasseFuncoes.ConectaMercadoLivre(int.Parse(r["CODIGO"].ToString()));

            //                //List<Parameter> ps = new List<Parameter>();
            //                //int iOffset = 0;
            //                //int ilimit = 50;
            //                //List<Result> lstPedidosMercadoLivre = new List<Result>();
            //                //bool lContinua = true;

            //                //ps = new List<Parameter>();
            //                //Parameter p = new Parameter();
            //                //p.Name = "access_token";
            //                //p.Value = ClasseParametros.oMeli.AccessToken;
            //                //ps.Add(p);


            //                //if (ClasseParametros.oUsuarioMercadoLivre == null)
            //                //{
            //                //    oResposta = ClasseParametros.oMeli.Get("/users/me", ps);
            //                //    if (oResposta.Content.Contains("invalid_token"))
            //                //    {
            //                //        ClasseParametros.oMeli.refreshToken(ClasseParametros.oMeli.RefreshToken);
            //                //        oResposta = ClasseParametros.oMeli.Get("/users/me", ps);
            //                //    }
            //                //    ClasseParametros.oUsuarioMercadoLivre = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreUsuario>(oResposta.Content);
            //                //}

            //                //while (lContinua)
            //                //{
            //                //    //offset=3&limit=3
            //                //    MercadoLivrePedido oPedidoMercadoLivre = null;

            //                //    while (oPedidoMercadoLivre == null || oPedidoMercadoLivre.results == null)
            //                //    {
            //                //        try
            //                //        {
            //                //            ps = new List<Parameter>();
            //                //            p = new Parameter();
            //                //            p.Name = "access_token";
            //                //            p.Value = ClasseParametros.oMeli.AccessToken;
            //                //            ps.Add(p);
            //                //            p = new Parameter();
            //                //            p.Name = "seller";
            //                //            p.Value = ClasseParametros.oUsuarioMercadoLivre.id;
            //                //            ps.Add(p);
            //                //            p = new Parameter();
            //                //            p.Name = "order.status";
            //                //            p.Value = "paid";
            //                //            ps.Add(p);
            //                //            p = new Parameter();
            //                //            p.Name = "offset";
            //                //            p.Value = iOffset;
            //                //            ps.Add(p);
            //                //            p = new Parameter();
            //                //            p.Name = "limit";
            //                //            p.Value = ilimit;
            //                //            ps.Add(p);
            //                //            p = new Parameter();
            //                //            p.Name = "sort";
            //                //            p.Value = "date_desc";
            //                //            ps.Add(p);

            //                //            oResposta = ClasseParametros.oMeli.Get("/orders/search", ps);
            //                //            oPedidoMercadoLivre = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivrePedido>(oResposta.Content);

            //                //            foreach (Result oPedidoAtual in oPedidoMercadoLivre.results)
            //                //            {
            //                //                string sSql = "SELECT * FROM VENDAS WHERE PEDIDO = '" + oPedidoAtual.id.ToString().Trim() + "'";
            //                //                DataTable dtbVendasTemp = ClasseParametros.ConsultaBancoMysql(sSql);
            //                //                int iTotalTemp = dtbVendasTemp.Rows.Count;
            //                //                dtbVendasTemp.Dispose();

            //                //                if (iTotalTemp > 0)
            //                //                    continue;

            //                //                if (oPedidoAtual.order_request == null)
            //                //                {
            //                //                    continue;
            //                //                }
            //                //                ps = new List<Parameter>();
            //                //                p = new Parameter();
            //                //                p.Name = "access_token";
            //                //                p.Value = ClasseParametros.oMeli.AccessToken;
            //                //                ps.Add(p);
            //                //                oResposta = null;
            //                //                MercadoLivreEntrega oEntrega = null;

            //                //                while (oResposta == null)
            //                //                {
            //                //                    oResposta = ClasseParametros.oMeli.Get("orders/" + oPedidoAtual.id.ToString().Trim() + "/shipments", ps);
            //                //                    oEntrega = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreEntrega>(oResposta.Content);
            //                //                    if (oResposta.StatusCode == HttpStatusCode.Unauthorized)
            //                //                    {
            //                //                        ClasseFuncoes.ConectaMercadoLivre(5);
            //                //                        ps = new List<Parameter>();
            //                //                        p = new Parameter();
            //                //                        p.Name = "access_token";
            //                //                        p.Value = ClasseParametros.oMeli.AccessToken;
            //                //                        ps.Add(p);


            //                //                        oResposta = null;
            //                //                    }
            //                //                    else if (oResposta.StatusCode == 0)
            //                //                    {
            //                //                        break;
            //                //                    }
            //                //                }


            //                //                if (oEntrega.status == "delivered" || oEntrega.status == "pending")
            //                //                {
            //                //                    lContinua = false;
            //                //                    break;
            //                //                }

            //                //                var p1 = new Parameter();
            //                //                p1.Name = "shipment_ids";
            //                //                p1.Value = oEntrega.id.ToString().Trim();
            //                //                ps.Add(p1);

            //                //                var p2 = new Parameter();
            //                //                p2.Name = "response_type";
            //                //                p2.Value = "zpl2";
            //                //                ps.Add(p2);

            //                //                oResposta = null;
            //                //                while (oResposta == null)
            //                //                {
            //                //                    oResposta = ClasseParametros.oMeli.Get("shipment_labels", ps);
            //                //                    if (oResposta.Content.Contains("delivered"))
            //                //                    {
            //                //                        ClasseParametros.ExecutabancoMySql("UPDATE VENDAMARKETPLACE SET STATUS = 2 WHERE ID = '" + r["ID"].ToString() + "'");
            //                //                    }
            //                //                    else if (oResposta.StatusCode == HttpStatusCode.Unauthorized)
            //                //                    {
            //                //                        ClasseFuncoes.ConectaMercadoLivre(5);
            //                //                        oResposta = null;

            //                //                    }
            //                //                }
            //                //                /////shipment_labels?shipment_ids=21527708516&response_type=zpl2&access_token=$ACCESS_TOKEN"
            //                //                if (oResposta.StatusCode == HttpStatusCode.OK)
            //                //                {
            //                //                    #region Pega a nota ML

            //                //                    ps = new List<Parameter>();
            //                //                    p = new Parameter();
            //                //                    p.Name = "access_token";
            //                //                    p.Value = ClasseParametros.oMeli.AccessToken;
            //                //                    ps.Add(p);
            //                //                    oResposta = null;
            //                //                    MercadoLivreNotaFiscal oNota = null;

            //                //                    while (oResposta == null)
            //                //                    {
            //                //                        oResposta = ClasseParametros.oMeli.Get("/users/" + ClasseParametros.oMeli.UserId.ToString() + "/invoices/orders/" + oPedidoAtual.id.ToString(), ps);
            //                //                        oNota = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreNotaFiscal>(oResposta.Content);
            //                //                        if (oResposta.StatusCode == HttpStatusCode.Unauthorized)
            //                //                        {
            //                //                            ClasseFuncoes.ConectaMercadoLivre(5);
            //                //                            ps = new List<Parameter>();
            //                //                            p = new Parameter();
            //                //                            p.Name = "access_token";
            //                //                            p.Value = ClasseParametros.oMeli.AccessToken;
            //                //                            ps.Add(p);


            //                //                            oResposta = null;
            //                //                        }
            //                //                        else if (oResposta.StatusCode == 0)
            //                //                        {
            //                //                            break;
            //                //                        }
            //                //                    }

            //                //                    #endregion





            //                //                    //ClasseParametros.SalvaEtiqueta(oPedidoAtual., oResposta.RawBytes, 5, oPedidoAtual.id, "MELI");
            //                //                    // sSql = "SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oPedidoAtual.mvChaveAcesso + "' AND ETIQUETATXT = ''";
            //                //                    //DataTable dtbVendas = ClasseParametros.ConsultaBancoMysql(sSql);
            //                //                    //if (dtbVendas.Rows.Count == 0)
            //                //                    //{
            //                //                    //    ClasseParametros.ExecutabancoMySql("UPDATE XMLVENDAS SET STATUS = 1 WHERE NOTAFISCAL = '" + oPedidoAtual.mvChaveAcesso + "'");
            //                //                    //    ClasseParametros.ExecutabancoMySql("UPDATE VENDAMARKETPLACE SET STATUS = 2 WHERE ID = '" + r["ID"].ToString() + "'");
            //                //                    //}
            //                //                    //dtbVendas.Dispose();
            //                //                    break;
            //                //                }
            //                //            }
            //                //        }
            //                //        catch (Exception ex)
            //                //        {

            //                //            if (oPedidoMercadoLivre == null || oPedidoMercadoLivre.results == null)
            //                //                ClasseFuncoes.ConectaMercadoLivre(5);
            //                //        }
            //                //        Thread.Sleep(2000);
            //                //        iOffset++;

            //                //    }
            //                //}

            //                #endregion

            //                #region MAGAZINE LUIZA
            //                string sSenha = r["SENHAMAGALU"].ToString();
            //                sUsuario = r["USUARIOMAGALU"].ToString();

            //                if (sUsuario == "")
            //                    continue;
            //                iPagina = 1;
            //                iPorPagina = 50;

            //                //Faturado
            //                while (true)
            //                {
            //                    ClasseParametros.sTokenMAGALU = ClasseFuncoes.Base64Encode(sUsuario + ":" + sSenha);
            //                    //Os possíveis status são: New, Approved, Processing, Invoiced, Shipped, Delivered, Canceled, ShipmentException.
            //                    client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order?page=" + iPagina.ToString() + "&perPage=" + iPorPagina.ToString() + "&Status=INVOICED");
            //                    RestRequest request = new RestRequest(Method.GET);
            //                    request.AddHeader("cache-control", "no-cache");
            //                    request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
            //                    oResposta = client.Execute(request);

            //                    Thread.Sleep(1000);
            //                    JsonPedidoMagalu oPedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPedidoMagalu>(oResposta.Content);

            //                    if (oPedidos.Total == 0)
            //                    {
            //                        break;
            //                    }

            //                    foreach (Order oPedido in oPedidos.Orders)
            //                    {
            //                        if (oPedido.InvoicedKey.Contains("35200608355660000105550010000845311395776907"))
            //                        {

            //                        }


            //                        oPedido.CodigoCliente = int.Parse(r["CODIGO"].ToString());
            //                        DataTable dtbetiqueta = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oPedido.InvoicedKey + "'");
            //                        if (dtbetiqueta.Rows.Count > 0)
            //                            continue;
            //                        MAGALUClasseFuncoes.SalvaBancoPDFZPLMagalu(oPedido);

            //                        //string sPastaXML = dtbCliente.Rows[0]["PASTAXML"].ToString();
            //                        //sPastaXML = @"C:\CLIENTES\ORLA\07-EnvioSaidaNF\";


            //                        //string[] aArquivos = Directory.GetFiles(sPastaXML, "*.xml");

            //                        //foreach (string sArquivo in aArquivos)
            //                        //{
            //                        //    string s = @"C:\Users\rrgnu\OneDrive\Documentos\n\N.F. 80430.xml";
            //                        //    DanfeViewModel oDanfe = DanfeViewModelCreator.CriarDeArquivoXml(sArquivo);
            //                        //    string sXML = "";

            //                        //    if (oDanfe == null)
            //                        //    {
            //                        //        continue;
            //                        //    }

            //                        //    if (oDanfe.ChaveAcesso == oPedido.InvoicedKey)
            //                        //    {

            //                        //        oPedido.CodigoCliente = int.Parse(r["CODIGO"].ToString());



            //                        //    }

            //                        //}










            //                    }
            //                    request = null;

            //                    iPagina++;
            //                }
            //                #endregion

            //            }
            //            else
            //            {
            //                string sPastaParametro = r["PASTA"].ToString();

            //                //Processo XML
            //                #region processarNOta
            //                //string sPastaXML = sPastaParametro + "\\07-EnvioSaidaNF\\";
            //                //string sPastaEtiqueta = sPastaParametro + "\\12-Etiquetas\\";

            //                //if (!Directory.Exists(sPastaXML))
            //                //{

            //                //}

            //                //string[] aArquivos = Directory.GetFiles(sPastaXML, "*.xml");

            //                //foreach (string sArquivo in aArquivos)
            //                //{
            //                //    try
            //                //    {
            //                //        bool lLivre = false;
            //                //        while (!lLivre)
            //                //        {
            //                //            FileStream stream = null;
            //                //            try
            //                //            {
            //                //                stream = File.Open(sArquivo, FileMode.Open, FileAccess.Read, FileShare.None);
            //                //                lLivre = true;
            //                //            }
            //                //            catch (IOException)
            //                //            {
            //                //                //the file is unavailable because it is:
            //                //                //still being written to
            //                //                //or being processed by another thread
            //                //                //or does not exist (has already been processed)
            //                //                lLivre = false;
            //                //            }
            //                //            finally
            //                //            {
            //                //                if (stream != null)
            //                //                    stream.Close();
            //                //            }

            //                //            //file is not locked
            //                //        }

            //                //        string sTexto = File.ReadAllText(sArquivo);
            //                //        XmlDocument doc = new XmlDocument();
            //                //        doc.LoadXml(sTexto);

            //                //        // Add a price element.
            //                //        XmlNodeList newElem = doc.GetElementsByTagName("infNFe");
            //                //        string sChaveNota = newElem[0].Attributes["Id"].InnerText.Replace("NFe", "");
            //                //        string sNota = sChaveNota.Substring(26, 8);
            //                //        newElem = doc.GetElementsByTagName("dhEmi");
            //                //        string sDataEmissao = newElem[0].InnerText;
            //                //        newElem = doc.GetElementsByTagName("CNPJ");
            //                //        string sCNPJ = newElem[0].InnerText;


            //                //        DateTime dEmissao = DateTime.Parse(sDataEmissao);

            //                //        string sPasta = Directory.GetCurrentDirectory();
            //                //        string sPastaPDFBACKUP = sPastaXML + "PDFBACKUP\\";
            //                //        if (!Directory.Exists(sPastaPDFBACKUP))
            //                //        {
            //                //            Directory.CreateDirectory(sPastaPDFBACKUP);
            //                //        }

            //                //        string sPastaDataHoje = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "\\";
            //                //        if (!Directory.Exists(sPastaPDFBACKUP + sPastaDataHoje))
            //                //        {
            //                //            Directory.CreateDirectory(sPastaPDFBACKUP + sPastaDataHoje);
            //                //        }

            //                //        string sPastaSalvaDANFE = sPastaPDFBACKUP + sPastaDataHoje;

            //                //        DanfeViewModel oModel = DanfeViewModelCreator.CriarDeArquivoXml(sArquivo);
            //                //        oModel.Duplicatas.Clear();
            //                //        //Inicia o Danfe com o modelo criado
            //                //        using (Danfe danfe = new Danfe(oModel))
            //                //        {
            //                //            danfe.Gerar();
            //                //            danfe.Salvar(sPastaSalvaDANFE + Path.GetFileName(sArquivo.Split('.')[0] + ".pdf"));
            //                //        }

            //                //        byte[] oPDFStream = File.ReadAllBytes(sPastaSalvaDANFE + Path.GetFileName(sArquivo.Split('.')[0] + ".pdf"));
            //                //        string sSql = "";
            //                //        sSql = "INSERT INTO NOTAMASTER(NOTA,CHAVENOTA,DATA,FINALIZADO,CNPJ,XML,PDF) VALUES(@NOTA,@CHAVENOTA,@DATA,@FINALIZADO,@CNPJ,@XML,@PDF)";
            //                //        ParametrosSQL.Add("NOTA", int.Parse(sNota).ToString());
            //                //        ParametrosSQL.Add("CHAVENOTA", sChaveNota);
            //                //        ParametrosSQL.Add("DATA", dEmissao.ToString("yyyy-MM-dd HH:mm:ss"));
            //                //        ParametrosSQL.Add("FINALIZADO", "0");
            //                //        ParametrosSQL.Add("CNPJ", sCNPJ);
            //                //        ParametrosSQL.Add("XML", sTexto);
            //                //        ParametrosSQL.Add("PDF", oPDFStream);
            //                //        ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);


            //                //        newElem = doc.GetElementsByTagName("det");

            //                //        for (int i = 0; i < newElem.Count; i++)
            //                //        {
            //                //            XmlNode eleTemp = newElem[i];
            //                //            string sEAN = eleTemp.ChildNodes[0].ChildNodes[1].InnerText;
            //                //            sSql = "INSERT INTO NOTADETALHE(NOTA,PRODUTO,CNPJ) VALUES(@NOTA,@PRODUTO,@CNPJ)";
            //                //            ParametrosSQL.Clear();
            //                //            ParametrosSQL.Add("NOTA", int.Parse(sNota).ToString());
            //                //            ParametrosSQL.Add("PRODUTO", sEAN);
            //                //            ParametrosSQL.Add("CNPJ", sCNPJ);
            //                //            ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);
            //                //        }

            //                //        newElem = null;
            //                //        doc = null;
            //                //        oModel = null;
            //                //        GC.Collect();
            //                //        GC.WaitForPendingFinalizers();

            //                //        string sPastaXMLBACKUP = sPastaXML + "XMLBACKUP\\";

            //                //        if (!Directory.Exists(sPastaXMLBACKUP))
            //                //        {
            //                //            Directory.CreateDirectory(sPastaXMLBACKUP);
            //                //        }

            //                //        if (!Directory.Exists(sPastaXMLBACKUP + sPastaDataHoje))
            //                //        {
            //                //            Directory.CreateDirectory(sPastaXMLBACKUP + sPastaDataHoje);
            //                //        }

            //                //        File.Copy(sArquivo, sPastaXMLBACKUP + sPastaDataHoje + Path.GetFileName(sArquivo), true);
            //                //        File.Delete(sArquivo);
            //                //    }

            //                //    catch (Exception ex)
            //                //    {
            //                //        ClasseParametros.MostraErro(ex.Message, iconApp);
            //                //    }
            //                //}

            //                #endregion

            //                string sPastaXML = r["PASTAXML"].ToString();

            //                if (!Directory.Exists(sPastaXML))
            //                {
            //                    sPastaXML = @"C:\CLIENTES\ORLA\07-EnvioSaidaNF\";
            //                }

            //                string[] aArquivos = Directory.GetFiles(sPastaXML, "*.xml");


            //                foreach (string sArquivo in aArquivos)
            //                {
            //                    string s = sArquivo;
            //                    DanfeViewModel oModel = DanfeViewModel.CreateFromXmlFile(s);

            //                    string sXML = System.IO.File.ReadAllText(s);
            //                    string sTemp = "";
            //                    try
            //                    {
            //                        int iECOMMERCE = oModel.InformacoesComplementares.IndexOf("E-COMMERCE");
            //                        if (iECOMMERCE > -1)
            //                        {
            //                            string sComplemento = oModel.InformacoesComplementares.Substring(iECOMMERCE);

            //                            sTemp = sComplemento.Substring(sComplemento.IndexOf("[") + 1, sComplemento.IndexOf("]") - sComplemento.IndexOf("["));
            //                            sTemp = sTemp.Replace("]", "");
            //                        }
            //                        else
            //                        {
            //                            int iPedido = oModel.InformacoesComplementares.IndexOf("Pedido");
            //                            if (iPedido > -1)
            //                            {
            //                                string sComplemento = oModel.InformacoesComplementares.Substring(iPedido);
            //                                sTemp = sComplemento.Split(':')[1].Trim();
            //                            }
            //                        }
            //                    }
            //                    catch
            //                    {

            //                    }

            //                    string sPedido = sTemp.Trim();

            //                    if (r["IDMERCADOLIVRE"].ToString() != "")
            //                    {
            //                        #region mercado livre
            //                        //string sXML = System.IO.File.ReadAllText(sArquivo);


            //                        //if (ClasseParametros.oMeli == null)
            //                        //    ClasseFuncoes.ConectaMercadoLivreAsync(int.Parse(dtbCliente.Rows[0]["CODIGO"].ToString()));

            //                        //var p = new Parameter();
            //                        //p.Name = "access_token";
            //                        //p.Value = ClasseParametros.oMeli.AccessToken;
            //                        //var ps = new List<Parameter>();
            //                        //ps.Add(p);
            //                        //oResposta = null;

            //                        //if (ClasseParametros.oUsuarioMercadoLivre == null)
            //                        //{
            //                        //    oResposta = ClasseParametros.oMeli.Get("/users/me", ps);
            //                        //    if (oResposta.Content.Contains("invalid_token"))
            //                        //    {
            //                        //        ClasseParametros.oMeli.refreshToken(ClasseParametros.oMeli.RefreshToken);
            //                        //        oResposta = ClasseParametros.oMeli.Get("/users/me", ps);

            //                        //    }
            //                        //    ClasseParametros.oUsuarioMercadoLivre = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreUsuario>(oResposta.Content);
            //                        //}

            //                        int iOffset = 0;
            //                        int ilimit = 50;
            //                        List<Result> lstPedidosMercadoLivre = new List<Result>();
            //                        bool lContinua = true;
            //                        Parameter pToken = new Parameter();

            //                        while (lContinua)
            //                        {
            //                            //offset=3&limit=3
            //                            MercadoLivrePedido oPedidoMercadoLivre = null;
            //                            bool lEncontrado = false;


            //                            while (oPedidoMercadoLivre == null || oPedidoMercadoLivre.results == null)
            //                            {
            //                                try
            //                                {
            //                                    ps = new List<Parameter>();
            //                                    pToken.Name = "access_token";
            //                                    pToken.Value = ClasseParametros.oMeli.AccessToken;
            //                                    ps = new List<Parameter>();
            //                                    ps.Add(pToken);
            //                                    p = new Parameter();
            //                                    p.Name = "seller";
            //                                    p.Value = ClasseParametros.oUsuarioMercadoLivre.id;
            //                                    ps.Add(p);
            //                                    p = new Parameter();

            //                                    p = new Parameter();
            //                                    p.Name = "q";
            //                                    p.Value = sPedido;
            //                                    ps.Add(p);


            //                                    oResposta = ClasseParametros.oMeli.Get("/orders/search", ps);
            //                                    oPedidoMercadoLivre = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivrePedido>(oResposta.Content);


            //                                    if (oPedidoMercadoLivre.results.Length == 0)
            //                                    {

            //                                        ps = new List<Parameter>();
            //                                        pToken.Name = "access_token";
            //                                        pToken.Value = ClasseParametros.oMeli.AccessToken;
            //                                        ps = new List<Parameter>();
            //                                        ps.Add(pToken);
            //                                        p = new Parameter();
            //                                        p.Name = "seller";
            //                                        p.Value = ClasseParametros.oUsuarioMercadoLivre.id;
            //                                        ps.Add(p);
            //                                        p = new Parameter();
            //                                        p.Name = "order.status";
            //                                        p.Value = "paid";
            //                                        ps.Add(p);
            //                                        p = new Parameter();
            //                                        p.Name = "date_created";
            //                                        p.Value = DateTime.Now.AddDays(-2).ToShortDateString();
            //                                        ps.Add(p);
            //                                        p = new Parameter();
            //                                        p.Name = "offset";
            //                                        p.Value = iOffset;
            //                                        ps.Add(p);
            //                                        p = new Parameter();
            //                                        p.Name = "limit";
            //                                        p.Value = ilimit;
            //                                        ps.Add(p);

            //                                        p.Name = "sort";
            //                                        p.Value = "date_desc";
            //                                        ps.Add(p);

            //                                        oResposta = ClasseParametros.oMeli.Get("/orders/search", ps);
            //                                        oPedidoMercadoLivre = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivrePedido>(oResposta.Content);

            //                                        Result[] lstResult = oPedidoMercadoLivre.results.Where(x => x.pack_id != null && x.pack_id.ToString() == sPedido).ToArray();

            //                                        if (lstResult.Length == 0)
            //                                        {
            //                                            iOffset += 50;
            //                                        }
            //                                        else
            //                                        {
            //                                            oPedidoMercadoLivre.results = lstResult;
            //                                            break;
            //                                        }
            //                                    }
            //                                }
            //                                catch (Exception ex)
            //                                {

            //                                    if (oPedidoMercadoLivre == null || oPedidoMercadoLivre.results == null)
            //                                        ClasseFuncoes.ConectaMercadoLivreAsync(5);
            //                                }
            //                                Thread.Sleep(2000);
            //                            }



            //                            foreach (Result oPedido in oPedidoMercadoLivre.results)
            //                            {
            //                                try
            //                                {

            //                                    if (oPedido.status == "paid" && (oPedido.shipping.status == "ready_to_ship" || oPedido.shipping.status == null))
            //                                    {
            //                                        ps = new List<Parameter>();
            //                                        p = new Parameter();
            //                                        p.Name = "access_token";
            //                                        p.Value = ClasseParametros.oMeli.AccessToken;
            //                                        ps.Add(p);
            //                                        oResposta = null;
            //                                        MercadoLivreEntrega oEntrega = null;


            //                                        while (oResposta == null)
            //                                        {
            //                                            oResposta = ClasseParametros.oMeli.Get("orders/" + oPedido.id.ToString() + "/shipments", ps);

            //                                            if (oResposta.StatusCode == HttpStatusCode.NotFound)
            //                                            {
            //                                                oResposta = ClasseParametros.oMeli.Get("orders/" + r["ID"].ToString() + "/shipments", ps);
            //                                            }

            //                                            oEntrega = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreEntrega>(oResposta.Content);
            //                                            if (oResposta.StatusCode == HttpStatusCode.Unauthorized)
            //                                            {
            //                                                ClasseFuncoes.ConectaMercadoLivreAsync(5);
            //                                                ps = new List<Parameter>();
            //                                                p = new Parameter();
            //                                                p.Name = "access_token";
            //                                                p.Value = ClasseParametros.oMeli.AccessToken;
            //                                                ps.Add(p);


            //                                                oResposta = null;
            //                                            }
            //                                            else if (oResposta.StatusCode == 0)
            //                                            {
            //                                                ClasseFuncoes.ConectaMercadoLivreAsync(5);
            //                                                ps = new List<Parameter>();
            //                                                p = new Parameter();
            //                                                p.Name = "access_token";
            //                                                p.Value = ClasseParametros.oMeli.AccessToken;
            //                                                ps.Add(p);


            //                                                oResposta = null;
            //                                            }
            //                                        }

            //                                        var p1 = new Parameter();
            //                                        p1.Name = "shipment_ids";
            //                                        p1.Value = oEntrega.id.ToString().Trim();
            //                                        ps.Add(p1);

            //                                        var p2 = new Parameter();
            //                                        p2.Name = "response_type";
            //                                        p2.Value = "zpl2";
            //                                        ps.Add(p2);

            //                                        oResposta = null;
            //                                        while (oResposta == null)
            //                                        {
            //                                            oResposta = ClasseParametros.oMeli.Get("shipment_labels", ps);
            //                                            if (oResposta.Content.Contains("delivered") || oResposta.Content.Contains("shipped"))
            //                                            {
            //                                                ClasseParametros.ExecutabancoMySql("UPDATE VENDAMARKETPLACE SET STATUS = 2 WHERE ID = '" + oPedido.id.ToString() + "'");
            //                                            }
            //                                            else if (oResposta.StatusCode == HttpStatusCode.Unauthorized)
            //                                            {
            //                                                ClasseFuncoes.ConectaMercadoLivreAsync(5);
            //                                                oResposta = null;

            //                                            }
            //                                        }
            //                                        /////shipment_labels?shipment_ids=21527708516&response_type=zpl2&access_token=$ACCESS_TOKEN"
            //                                        if (oResposta.StatusCode == HttpStatusCode.OK)
            //                                        {
            //                                            ClasseParametros.SalvaEtiqueta(oModel.ChaveAcesso, oResposta.RawBytes, iCodigoCliente, oPedido.id.ToString(), "MELI");

            //                                            lEncontrado = true;

            //                                            SalvaNota(sArquivo, iCodigoCliente);
            //                                            break;
            //                                        }
            //                                        break;
            //                                    }
            //                                    else
            //                                    {
            //                                        lEncontrado = true;

            //                                    }
            //                                }
            //                                catch (Exception ex)
            //                                {


            //                                }
            //                            }
            //                            iOffset += 50;

            //                            if (lEncontrado)
            //                                break;
            //                        }

            //                        #endregion

            //                    }

            //                    if (r["KEYB2W"].ToString() != "")
            //                    {
            //                        #region B2W
            //                        string sKey = r["KEYB2W"].ToString();
            //                        string sUsuario = r["USUARIOB2W"].ToString();
            //                        string sAccount = r["ACCOUNTMANAGERB2W"].ToString();

            //                        int iPagina = 1;
            //                        int iPorPagina = 50;
            //                        int iTotal = 0;

            //                        while (true)
            //                        {
            //                            oResposta = null;
            //                            while (oResposta == null)
            //                            {
            //                                client = new RestClient("http://api.skyhub.com.br/orders?filters[statuses][]=order_invoiced&page=" + iPagina.ToString() + "&per_page=" + iPorPagina.ToString());
            //                                //client = new RestClient("http://api.skyhub.com.br/orders?page=" + iPagina.ToString() + "&per_page=" + iPorPagina.ToString());
            //                                //client = new RestClient("http://api.skyhub.com.br/orders?page=0&per_page=50");
            //                                oRequest = new RestRequest(Method.GET);
            //                                oRequest.AddHeader("cache-control", "no-cache");
            //                                oRequest.AddHeader("Accept", "application/json");
            //                                oRequest.AddHeader("Content-Type", "application/json");
            //                                oRequest.AddHeader("x-Api-Key", sKey);
            //                                oRequest.AddHeader("X-User-Email", sUsuario);
            //                                oRequest.AddHeader("X-Accountmanager-Key", sAccount);
            //                                oResposta = client.Execute(oRequest);

            //                                if (oResposta.StatusCode == HttpStatusCode.BadGateway || oResposta.StatusCode == HttpStatusCode.GatewayTimeout || oResposta.StatusCode == HttpStatusCode.InternalServerError || oResposta.StatusCode == 0)
            //                                {
            //                                    oResposta = null;
            //                                }
            //                            }
            //                            Thread.Sleep(200);

            //                            if (oResposta.Content == "Account  not found")
            //                            {
            //                                continue;
            //                            }
            //                            if (oResposta.Content.Contains("504 Gateway Time-ou"))
            //                            {
            //                                continue;
            //                            }

            //                            B2WPedidos lstPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<B2WPedidos>(oResposta.Content);
            //                            if (lstPedido.orders == null || lstPedido.orders.Length == 0)
            //                            {
            //                                break;
            //                            }

            //                            List<B2WPedido> lstB2WPedido = lstPedido.orders.Where(x => x.invoices.Length > 0).ToList();

            //                            foreach (B2WPedido oPedido in lstB2WPedido)
            //                            {

            //                                if (oPedido.invoices[0].key == "32200629079030000117550010000262721008887626")
            //                                {

            //                                }
            //                                bool lOk = false;

            //                                client = new RestClient("http://api.skyhub.com.br/shipments/b2w");
            //                                var request = new RestRequest(Method.POST);
            //                                request.AddHeader("Content-Length", "52");
            //                                request.AddHeader("Cache-Control", "no-cache");
            //                                request.AddHeader("Accept", "application/json");
            //                                request.AddHeader("Content-Type", "application/json");
            //                                request.AddHeader("x-Api-Key", sKey);
            //                                request.AddHeader("X-User-Email", sUsuario);
            //                                oRequest.AddHeader("X-Accountmanager-Key", sAccount);

            //                                string sPost = "{\n  \"order_remote_codes\": [\n    \"" + oPedido.code.Split('-')[1] + "\"\n  ]\n}";
            //                                request.AddParameter("application/json", sPost, ParameterType.RequestBody);
            //                                IRestResponse response = client.Execute(request);
            //                                if (response.Content.Contains("nao encontrada") || response.Content.Contains("nao localizados"))
            //                                {
            //                                    continue;
            //                                }

            //                                Thread.Sleep(100);
            //                                for (int i = 0; i < 12; i++)
            //                                {
            //                                    if (response.StatusCode.ToString() == "422")
            //                                    {
            //                                        response = client.Execute(request);
            //                                    }
            //                                    else if (response.StatusCode.ToString() == "429")
            //                                    {
            //                                        Thread.Sleep(15000);

            //                                        client.ClearHandlers();
            //                                        client = null;
            //                                        request = null;
            //                                        client = new RestClient("http://api.skyhub.com.br/shipments/b2w");
            //                                        request = new RestRequest(Method.POST);
            //                                        request.AddHeader("Content-Length", "52");
            //                                        request.AddHeader("Cache-Control", "no-cache");
            //                                        request.AddHeader("Accept", "application/json");
            //                                        request.AddHeader("Content-Type", "application/json");
            //                                        request.AddHeader("x-Api-Key", r["KEYB2W"].ToString());
            //                                        request.AddHeader("X-User-Email", r["USUARIOB2W"].ToString());
            //                                        sPost = "{\n  \"order_remote_codes\": [\n    \"" + oPedido.code.Split('-')[1] + "\"\n  ]\n}";
            //                                        request.AddParameter("application/json", sPost, ParameterType.RequestBody);
            //                                        response = client.Execute(request);
            //                                        Thread.Sleep(200);

            //                                    }
            //                                    else
            //                                        break;
            //                                }

            //                                if (response.StatusCode == HttpStatusCode.Created)
            //                                {
            //                                    string sPLP = String.Join("", System.Text.RegularExpressions.Regex.Split(response.Content, @"[^\d]"));
            //                                    client = new RestClient("https://api.skyhub.com.br/shipments/b2w/view?plp_id=" + sPLP);
            //                                    request = new RestRequest(Method.GET);
            //                                    request.AddHeader("cache-control", "no-cache");
            //                                    request.AddHeader("Accept", "application/json");
            //                                    request.AddHeader("Content-Type", "application/json");
            //                                    request.AddHeader("x-Api-Key", r["KEYB2W"].ToString());
            //                                    request.AddHeader("X-User-Email", r["USUARIOB2W"].ToString());
            //                                    response = client.Execute(request);
            //                                    Thread.Sleep(100);

            //                                    EtiquetaJSON oJsonB2W = Newtonsoft.Json.JsonConvert.DeserializeObject<EtiquetaJSON>(response.Content);
            //                                    if (oJsonB2W.plp != null)
            //                                    {
            //                                        string sEtiqueta = GeraEtiquetaB2W(oJsonB2W, oPedido.code.Split('-')[1], oJsonB2W.docsExternos[0].tpEntrega);
            //                                        byte[] aEtiqueta = Encoding.UTF8.GetBytes(sEtiqueta);
            //                                        ClasseParametros.SalvaEtiquetaAnyMarket(oPedido.invoices[0].key, aEtiqueta, oPedido.code.Split('-')[0], int.Parse(r["CODIGO"].ToString()), oPedido.code.Split('-')[0], oPedido.code);
            //                                        // salva json

            //                                        if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\json"))
            //                                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\json");

            //                                        File.WriteAllText(Directory.GetCurrentDirectory() + "\\json\\" + oPedido.invoices[0].key + ".json", response.Content); // Requires System.IO
            //                                    }
            //                                    lOk = true;
            //                                    break;
            //                                }
            //                                if (!lOk)
            //                                {

            //                                }
            //                            }

            //                            iTotal += lstPedido.orders.Length;
            //                            lblAPICount.BeginInvoke((MethodInvoker)delegate ()
            //                            {
            //                                lblAPICount.Text = "Add " + iTotal.ToString() + "";
            //                            });
            //                            Application.DoEvents();

            //                            iPagina += 1;
            //                        }
            //                        #endregion
            //                    }
            //                }

            //                if (r["USUARIOMAGALU"].ToString() != "")
            //                {
            //                    #region MAGALU

            //                    string sSenha = r["SENHAMAGALU"].ToString();
            //                    string sUsuario = r["USUARIOMAGALU"].ToString();

            //                    if (sUsuario == "")
            //                        continue;
            //                    int iPagina = 1;
            //                    int iPorPagina = 50;

            //                    //Faturado
            //                    while (true)
            //                    {
            //                        try
            //                        {
            //                            ClasseParametros.sTokenMAGALU = ClasseFuncoes.Base64Encode(sUsuario + ":" + sSenha);
            //                            //Os possíveis status são: New, Approved, Processing, Invoiced, Shipped, Delivered, Canceled, ShipmentException.
            //                            client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order?page=" + iPagina.ToString() + "&perPage=" + iPorPagina.ToString() + "&Status=INVOICED");
            //                            RestRequest request = new RestRequest(Method.GET);
            //                            request.AddHeader("cache-control", "no-cache");
            //                            request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
            //                            oResposta = client.Execute(request);

            //                            Thread.Sleep(1000);
            //                            JsonPedidoMagalu oPedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPedidoMagalu>(oResposta.Content);

            //                            if (oPedidos.Orders == null)
            //                            {
            //                                break;
            //                            }

            //                            foreach (Order oPedido in oPedidos.Orders)
            //                            {
            //                                if (oPedido.InvoicedKey.Contains("35200608355660000105550010000845311395776907"))
            //                                {

            //                                }

            //                                oPedido.CodigoCliente = int.Parse(r["CODIGO"].ToString());
            //                                DataTable dtbetiqueta = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oPedido.InvoicedKey + "'");
            //                                if (dtbetiqueta.Rows.Count > 0)
            //                                    continue;
            //                                MAGALUClasseFuncoes.SalvaBancoPDFZPLMagalu(oPedido);
            //                            }
            //                            request = null;
            //                        }
            //                        catch (Exception ex)
            //                        {

            //                        }

            //                        iPagina++;
            //                    }



            //                    #endregion
            //                }

            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            if (ClasseParametros.oMeli == null)
            //                ClasseFuncoes.ConectaMercadoLivreAsync(int.Parse(dtbCliente.Rows[0]["CODIGO"].ToString()));
            //        }

            //    }

            //    editNota.BeginInvoke((MethodInvoker)delegate ()
            //    {
            //        editNota.Text = "Finalizado add " + lstContent.Count.ToString() + " ao list";
            //    });
            //    Application.DoEvents();

            //    progressBarra.BeginInvoke((MethodInvoker)delegate ()
            //    {
            //        progressBarra.Maximum = lstContent.Count;
            //        progressBarra.Value = 0;
            //    });

            //    lblAPICount.BeginInvoke((MethodInvoker)delegate ()
            //    {
            //        lblAPICount.Text = "Total B2W: " + lstContent.Count.ToString() + " ";
            //    });

            //    int iProgresso = 0;


            //    foreach (Content oAnymarket in lstContent)
            //    {
            //        try
            //        {
            //            DataTable dtbNota = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oAnymarket.invoice.accessKey.ToString() + "' ");

            //            if (dtbNota.Rows.Count > 0)
            //            {
            //                dtbNota.Dispose();

            //                editNota.BeginInvoke((MethodInvoker)delegate ()
            //                {
            //                    if (oAnymarket.invoice != null)
            //                        editNota.Text = "(" + oAnymarket.marketPlace + ",(JÁ GERADA)) " + oAnymarket.invoice.accessKey;
            //                });


            //                continue;
            //            }
            //            dtbNota.Dispose();

            //            string sEtiqueta = "";

            //            if (oAnymarket.invoice.accessKey == "35200508355660000105550010000807741348889004")
            //            {

            //            }

            //            if (oAnymarket.marketPlace.Contains("B2W"))
            //            {
            //                bool lOk = false;
            //                foreach (DataRow r in dtbCliente.Rows)
            //                {
            //                    DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM CLIENTE WHERE CODIGO = " + r["CODIGO"].ToString());
            //                    string sKey = d.Rows[0]["KEYB2W"].ToString();
            //                    string sUsuario = d.Rows[0]["USUARIOB2W"].ToString();
            //                    string sAccount = d.Rows[0]["ACCOUNTMANAGERB2W"].ToString();
            //                    d.Dispose();


            //                    client = new RestClient("http://api.skyhub.com.br/shipments/b2w");
            //                    var request = new RestRequest(Method.POST);
            //                    request.AddHeader("Content-Length", "52");
            //                    request.AddHeader("Cache-Control", "no-cache");
            //                    request.AddHeader("Accept", "application/json");
            //                    request.AddHeader("Content-Type", "application/json");
            //                    request.AddHeader("x-Api-Key", sKey);
            //                    request.AddHeader("X-User-Email", sUsuario);
            //                    oRequest.AddHeader("X-Accountmanager-Key", sAccount);

            //                    string sPost = "{\n  \"order_remote_codes\": [\n    \"" + String.Join("", System.Text.RegularExpressions.Regex.Split(oAnymarket.marketPlaceId, @"[^\d]")) + "\"\n  ]\n}";
            //                    request.AddParameter("application/json", sPost, ParameterType.RequestBody);
            //                    IRestResponse response = client.Execute(request);
            //                    if (response.Content.Contains("nao encontrada"))
            //                    {
            //                        continue;
            //                    }

            //                    Thread.Sleep(100);
            //                    for (int i = 0; i < 12; i++)
            //                    {
            //                        if (response.StatusCode.ToString() == "422")
            //                        {
            //                            response = client.Execute(request);
            //                        }
            //                        else if (response.StatusCode.ToString() == "429")
            //                        {
            //                            Thread.Sleep(15000);

            //                            client.ClearHandlers();
            //                            client = null;
            //                            request = null;
            //                            client = new RestClient("http://api.skyhub.com.br/shipments/b2w");
            //                            request = new RestRequest(Method.POST);
            //                            request.AddHeader("Content-Length", "52");
            //                            request.AddHeader("Cache-Control", "no-cache");
            //                            request.AddHeader("Accept", "application/json");
            //                            request.AddHeader("Content-Type", "application/json");
            //                            request.AddHeader("x-Api-Key", r["KEYB2W"].ToString());
            //                            request.AddHeader("X-User-Email", r["USUARIOB2W"].ToString());
            //                            sPost = "{\n  \"order_remote_codes\": [\n    \"" + String.Join("", System.Text.RegularExpressions.Regex.Split(oAnymarket.marketPlaceId, @"[^\d]")) + "\"\n  ]\n}";
            //                            request.AddParameter("application/json", sPost, ParameterType.RequestBody);
            //                            response = client.Execute(request);
            //                            Thread.Sleep(200);

            //                        }
            //                        else
            //                            break;
            //                    }

            //                    if (response.StatusCode == HttpStatusCode.Created)
            //                    {
            //                        string sPLP = String.Join("", System.Text.RegularExpressions.Regex.Split(response.Content, @"[^\d]"));
            //                        client = new RestClient("https://api.skyhub.com.br/shipments/b2w/view?plp_id=" + sPLP);
            //                        request = new RestRequest(Method.GET);
            //                        request.AddHeader("cache-control", "no-cache");
            //                        request.AddHeader("Accept", "application/json");
            //                        request.AddHeader("Content-Type", "application/json");
            //                        request.AddHeader("x-Api-Key", r["KEYB2W"].ToString());
            //                        request.AddHeader("X-User-Email", r["USUARIOB2W"].ToString());
            //                        response = client.Execute(request);
            //                        Thread.Sleep(100);

            //                        EtiquetaJSON oJsonB2W = Newtonsoft.Json.JsonConvert.DeserializeObject<EtiquetaJSON>(response.Content);
            //                        if (oJsonB2W.plp != null)
            //                        {
            //                            sEtiqueta = GeraEtiquetaB2W(oJsonB2W, oAnymarket.marketPlaceId.Split('-')[0], oJsonB2W.docsExternos[0].tpEntrega);
            //                            byte[] aEtiqueta = Encoding.UTF8.GetBytes(sEtiqueta);
            //                            ClasseParametros.SalvaEtiquetaAnyMarket(oAnymarket.invoice.accessKey, aEtiqueta, oAnymarket.marketPlace, oAnymarket.CodigoCliente, oAnymarket.marketPlaceId.Split('-')[0], oAnymarket.marketPlaceId);
            //                            // salva json

            //                            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\json"))
            //                                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\json");

            //                            File.WriteAllText(Directory.GetCurrentDirectory() + "\\json\\" + oAnymarket.invoice.accessKey + ".json", response.Content); // Requires System.IO
            //                        }
            //                        lOk = true;
            //                        break;
            //                    }
            //                }
            //                if (!lOk)
            //                {

            //                }
            //            }
            //            else if (oAnymarket.marketPlace == "MERCADO_LIVRE")
            //            {
            //                client = new RestClient("http://api.anymarket.com.br/v2/printtag/zpl2");
            //                var request = new RestRequest(Method.POST);
            //                request.AddHeader("cache-control", "no-cache");
            //                request.AddHeader("Accept-Encoding", "gzip, deflate");
            //                request.AddHeader("Host", "api.anymarket.com.br");
            //                request.AddHeader("Cache-Control", "no-cache");
            //                request.AddHeader("Accept", "application/json");
            //                request.AddHeader("Content-Type", "application/json");
            //                request.AddHeader("gumgaToken", oAnymarket.TokenCliente);
            //                request.AddParameter("application/json", "{\"orders\":[" + oAnymarket.id + "]}", ParameterType.RequestBody);
            //                IRestResponse response = client.Execute(request);
            //                if (response.StatusCode != HttpStatusCode.BadRequest)
            //                {
            //                    ClasseParametros.SalvaEtiqueta(oAnymarket.invoice.accessKey, response.RawBytes, oAnymarket.CodigoCliente, "BaseInfo", "MELI");
            //                }
            //            }
            //            else if (oAnymarket.marketPlace == "MAGAZINE_LUIZA")
            //            {
            //                if (oAnymarket.UsuarioMAGALU != "")
            //                {
            //                    client = new RestClient("http://api.anymarket.com.br/v2/printtag/zpl");
            //                    var request = new RestRequest(Method.POST);
            //                    request.AddHeader("cache-control", "no-cache");
            //                    request.AddHeader("Accept-Encoding", "gzip, deflate");
            //                    request.AddHeader("Cache-Control", "no-cache");
            //                    request.AddHeader("Accept", "application/zpl");
            //                    request.AddHeader("Content-Type", "application/zpl");
            //                    request.AddHeader("gumgaToken", oAnymarket.TokenCliente);
            //                    request.AddParameter("application/json", "{\"orders\":[" + oAnymarket.id + "]}", ParameterType.RequestBody);
            //                    IRestResponse response = client.Execute(request);
            //                    if (response.StatusCode != HttpStatusCode.BadRequest)
            //                    {
            //                        ClasseParametros.SalvaEtiqueta(oAnymarket.invoice.accessKey, response.RawBytes, oAnymarket.CodigoCliente, "BaseInfo", "MAGAZINE_LUIZA");
            //                    }


            //                    #region Funciona 100% magazine luiza
            //                    //DataTable dtbChaves = ClasseParametros.ConsultaBanco("SELECT PORANYMARKET,USUARIOMAGALU,SENHAMAGALU,PORARQUIVO, CODIGO, CLIENTE, IDMERCADOLIVRE, SENHAMERCADOLIVRE, CODEMERCADOLIVRE, ACCESSTOKENMERCADOLIVRE,REFRESHTOKENMERCADOLIVRE,KEYB2W,USUARIOB2W,REMETENTE,ENDERECO,CEP,CIDADE,UF,GUMGATOKENANYMARKET FROM CLIENTE WHERE CODIGO IN (1,2,4)");
            //                    //Order oJson = null;
            //                    //foreach (DataRow rChaves in dtbChaves.Rows)
            //                    //{
            //                    //    if (rChaves["USUARIOMAGALU"].ToString() != "")
            //                    //    {
            //                    //        string sAPI = ClasseFuncoes.Base64Encode(rChaves["USUARIOMAGALU"].ToString() + ":" + rChaves["SENHAMAGALU"].ToString());
            //                    //        //var client = new RestClient("https://api-integra.azurewebsites.net/api/Order?page=1&perPage=50&Status=INVOICED");
            //                    //        var client = new RestClient("https://api-integra.azurewebsites.net/api/Order/" + oAnymarket.marketPlaceId);
            //                    //        var request = new RestRequest(Method.GET);
            //                    //        request.AddHeader("cache-control", "no-cache");
            //                    //        request.AddHeader("authorization", "Basic " + sAPI);
            //                    //        //request.AddHeader("authorization", "Basic " + Base64Encode("lojamegastoreapi:LyJymDIY4gwX"));
            //                    //        IRestResponse response = client.Execute(request);
            //                    //        if (response.StatusCode == HttpStatusCode.OK)
            //                    //        {
            //                    //            oJson = Newtonsoft.Json.JsonConvert.DeserializeObject<Order>(response.Content);
            //                    //            //decimal ePagina = Math.Ceiling(decimal.Divide(oJson.Total, 50));
            //                    //            try
            //                    //            {
            //                    //                // Gera tracking
            //                    //                oJson.CodigoCliente = oAnymarket.CodigoCliente;
            //                    //                ClasseParametros.SalvaBancoPDFZPLMagalu(oJson, rChaves["USUARIOMAGALU"].ToString(), rChaves["SENHAMAGALU"].ToString());

            //                    //            }
            //                    //            catch (Exception ex)
            //                    //            {
            //                    //                ClasseParametros.MostraErro(ex.Message, ClasseParametros.iconApp);
            //                    //            }
            //                    //            break;
            //                    //        }
            //                    //    }
            //                    //}
            //                    #endregion 

            //                    //if (ePagina > 1)
            //                    //    for (int i = 2; i <= ePagina; i++)
            //                    //    {
            //                    //        client = new RestClient("https://api-integra.azurewebsites.net/api/Order?page=" + i + "&perPage=50&Status=INVOICED");
            //                    //        request = new RestRequest(Method.GET);
            //                    //        request.AddHeader("cache-control", "no-cache");
            //                    //        request.AddHeader("authorization", "Basic " + ClasseFuncoes.Base64Encode(oAnymarket.UsuarioMAGALU + ":" + oAnymarket.SenhaMAGALU));
            //                    //        //request.AddHeader("authorization", "Basic " + Base64Encode("lojamegastoreapi:LyJymDIY4gwX"));
            //                    //        response = client.Execute(request);
            //                    //        oJson = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPedidoMagalu>(response.Content);
            //                    //        foreach (Order oPedido in oJson.Orders)
            //                    //        {
            //                    //            try
            //                    //            {
            //                    //                //gera tracking
            //                    //                oPedido.CodigoCliente = oAnymarket.CodigoCliente;
            //                    //                ClasseParametros.SalvaBancoPDFZPLMagalu(oPedido, oAnymarket);
            //                    //            }
            //                    //            catch (Exception ex)
            //                    //            {
            //                    //                ClasseParametros.MostraErro(ex.Message, ClasseParametros.iconApp);
            //                    //            }
            //                    //        }
            //                    //    }
            //                }
            //                else
            //                {
            //                    ClasseParametros.MostraErro("Solicitar chave magalu cliente " + oAnymarket.CodigoCliente.ToString(), iconApp);
            //                }
            //            }
            //            else
            //            {
            //                byte[] aEtiqueta = null;
            //                //ClasseParametros.SalvaEtiquetaAnyMarket(oAnymarket.invoice.accessKey, aEtiqueta, oAnymarket.marketPlace, oAnymarket.CodigoCliente, oAnymarket.subChannel, oAnymarket.marketPlaceId);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            ClasseParametros.MostraErro(ex.Message, iconApp);
            //            ClasseFuncoes.SalvaLogServicos(ex.Message);

            //        }
            //        //ClasseParametros.Executabanco("UPDATE VENDAS SET FINALIZADO = 1 WHERE NOTAFISCAL = '" + oItemJsonB2W.invoices[0].key + "'");
            //        editNota.BeginInvoke((MethodInvoker)delegate ()
            //        {
            //            if (oAnymarket.invoice != null)
            //                editNota.Text = "(" + oAnymarket.marketPlace + ") " + oAnymarket.invoice.accessKey;
            //        });
            //        progressBarra.BeginInvoke((MethodInvoker)delegate ()
            //        {
            //            try
            //            {
            //                progressBarra.Value = iProgresso;
            //            }
            //            catch
            //            {

            //            }
            //        });



            //        Application.DoEvents();
            //        iProgresso++;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ClasseParametros.MostraErro(ex.Message, iconApp);
            //    ClasseFuncoes.SalvaLogServicos(ex.Message);

            //}

        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            //ClasseParametros.Conexao = new SqlConnection(@"Server=RODRIGO-PC\SQLEXPRESS;Database=BaseInfo;Trusted_Connection=True;MultipleActiveResultSets=true;");
            //ClasseParametros.ConexaoCliente = new SqlConnection(@"Server=RODRIGO-PC\SQLEXPRESS;Database=BaseInfo;Trusted_Connection=True;MultipleActiveResultSets=true;");
            //ClasseParametros.Conexao = new SqlConnection(@"Server=191.252.61.62;Database=BaseInfo;User=sa;Password=#BanLoc#InterPlace#2019#;MultipleActiveResultSets=true;");
            //ClasseParametros.ConexaoCliente = new SqlConnection(@"Server=191.252.61.62;Database=BaseInfo;User=sa;Password=#BanLoc#InterPlace#2019#;MultipleActiveResultSets=true;");
            //ClasseFinalizaEtiqueta.ConexaoFinalizaEtiqueta = new SqlConnection(@"Server=191.252.61.62;Database=BaseInfo;User=sa;Password=#BanLoc#InterPlace#2019#;MultipleActiveResultSets=true;");

            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");

            ClasseParametros.iconApp = iconApp;
            ClasseParametros.oEditLog = editLog;


            TTimerIniciaProcesso = new System.Threading.Timer(
                                   new TimerCallback(TimerIniciaProcesso),
                                   null,
                                   1000,
                                   5000);
        }

        private void IniciaTudo()
        {
            if (!lRodandoTimer)
            {
                lRodandoTimer = true;

                DataTable dtbCliente = ClasseParametros.ConsultaBancoMysql("SELECT PORANYMARKET,USUARIOMAGALU,SENHAMAGALU,PORARQUIVO, CODIGO, CLIENTE, IDMERCADOLIVRE, SENHAMERCADOLIVRE, CODEMERCADOLIVRE, " +
                    "                                                         ACCESSTOKENMERCADOLIVRE,REFRESHTOKENMERCADOLIVRE,KEYB2W,USUARIOB2W,REMETENTE,ENDERECO,CEP,CIDADE,UF,GUMGATOKENANYMARKET," +
                    "                                                         PASTA, MATRIZ " +
                    "                                                  FROM CLIENTE WHERE MATRIZ = 5 ORDER BY PORARQUIVO DESC");
                foreach (DataRow r in dtbCliente.Rows)
                {
                    try
                    {
                        //ClasseFuncoes.ConectaMercadoLivre(int.Parse(r["CODIGO"].ToString()));
                        SalvaB2W(r["CODIGO"].ToString());
                        if (r["PASTA"].ToString() != "")
                            ClasseFinalizaEtiqueta.FinalizaEtiqueta(r["PASTA"].ToString());
                    }
                    catch
                    {

                    }
                    //ThreadStart oThreadStartAtualizaProdutos = delegate
                    //{
                    //};
                    //Thread oThreadAtualizaProdutos = new Thread(oThreadStartAtualizaProdutos);
                    //oThreadAtualizaProdutos.Start();


                    //int iPorArquivo = 0;
                    //if (r["PORARQUIVO"].ToString() != "")
                    //    iPorArquivo = int.Parse(r["PORARQUIVO"].ToString());

                    //if (iPorArquivo == 1)
                    //    if (!lRodandoTimerFinalizaEtiqueta)
                    //    {
                    //        ThreadStart oThreadStartFinalizaEtiqueta = delegate
                    //        {
                    //            lRodandoTimerFinalizaEtiqueta = true;
                    //            lRodandoTimerFinalizaEtiqueta = false;
                    //        };
                    //        Thread oThreadFinalizaEtiqueta = new Thread(oThreadStartFinalizaEtiqueta);
                    //        oThreadFinalizaEtiqueta.Start();
                    //    }
                }

                lRodandoTimer = false;

            }

        }

        private void TimerIniciaProcesso(object state)
        {


            IniciaTudo();


        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            lAberto = false;
            if (oServidor != null)
            {
                oServidor.Stop();
                oServidorConsulta.Stop();
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (editPorta.Text != "")
            {
                oSett.Porta = int.Parse(editPorta.Text);
                oSett.PortaConsulta = int.Parse(editPortaConsulta.Text);
                oSett.Save();
            }
        }
    }
}
