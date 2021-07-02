using DanfeSharp;
using DanfeSharp.Modelo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using InterRegraNegocio;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Techchop.Integracao;
using AutoUpdaterDotNET;

namespace InterEtiquetas
{
    public partial class Integrador_Etiquetas : MetroFramework.Forms.MetroForm
    {
        Properties.Settings oSett = new Properties.Settings();
        System.Threading.Timer TTimerNota = null;
        bool lRodandoTimer = false;

        public Integrador_Etiquetas()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public DataView GetDataView(GridControl gc)
        {
            DataView dv = null;

            if (gc.FocusedView != null && gc.FocusedView.DataSource != null)
            {
                var view = (ColumnView)gc.FocusedView;
                var currentList = ((DataTable)(gc.DataSource)).DefaultView; //(DataView)

                var filterExpression = GetFilterExpression(view);
                var sortExpression = GetSortExpression(view);

                var currentFilter = currentList.RowFilter;

                //create a new data view 
                dv = new DataView(currentList.Table) { Sort = sortExpression };

                if (filterExpression != String.Empty)
                {
                    if (currentFilter != String.Empty)
                    {
                        currentFilter += " AND ";
                    }
                    currentFilter += filterExpression;
                }
                dv.RowFilter = currentFilter;
            }
            return dv;
        }

        public string GetFilterExpression(ColumnView view)
        {
            var sExpressao = String.Empty;

            for (int i = 0; i < view.ActiveFilter.Count; i++)
            {
                if (sExpressao != string.Empty)
                {
                    sExpressao += " AND ";
                }

                string sTexto = view.ActiveFilter[i].Filter.GetValueDisplayText();

                int iColchete1 = sTexto.IndexOf('[') + 1;
                int iColchete2 = sTexto.IndexOf(']');

                string sCampo = sTexto.Substring(iColchete1, iColchete2 - iColchete1);

                int iAspa1 = sTexto.IndexOf('\'') + 1;
                int iAspa2 = sTexto.LastIndexOf('\'');
                string sValor = sTexto.Substring(iAspa1, iAspa2 - iAspa1);

                sExpressao += sCampo + "='" + sValor + "'";
            }


            if (view.ActiveFilter != null && view.ActiveFilterEnabled
                          && view.ActiveFilter.Expression != String.Empty)
            {
            }
            return sExpressao;
        }

        public string GetSortExpression(ColumnView view)
        {
            var expression = String.Empty;
            foreach (GridColumnSortInfo info in view.SortInfo)
            {
                expression += string.Format("[{0}]", info.Column.FieldName);

                if (info.SortOrder == DevExpress.Data.ColumnSortOrder.Descending)
                    expression += " DESC";
                else
                    expression += " ASC";
                expression += ", ";
            }
            return expression.TrimEnd(',', ' ');
        }

        [Obsolete]
        public void GerarEtiqueta(string ChaveEtiqueta)
        {
            Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

            string sPasta = "";

            if (oSett.PorArquivo)
            {
                string sSqlTemp = "SELECT * FROM CLIENTE WHERE CNPJCLIENTES LIKE ('%" + editEmpresa.Text + "%')";
                DataTable dtbCliente = ClasseParametros.ConsultaBancoMysql(sSqlTemp);
                string sPastaTemp = dtbCliente.Rows[0]["PASTA"].ToString();
                int iCodigo = int.Parse(dtbCliente.Rows[0]["CODIGO"].ToString());
                dtbCliente.Dispose();

                imgBoxProcessando.Visible = true;
                sPasta = Directory.GetCurrentDirectory();
                string sPastaRetorno = sPastaTemp + "\\13-RetornoOrla\\";

                bool lEtiquetaImpressa = false;
                bool lNotaImpressa = false;

                var data = GetDataView(gridEtiquetasNImpressas);

                data.Sort = "NOTA ASC";
                string sFiltro = "";
                if (data.RowFilter != "")
                {
                    sFiltro = data.RowFilter + " AND ";
                }

                if (ClasseParametros.lImpressaoPorSKU)
                    sFiltro += "SKU = '" + editEtiqueta.Text + "' AND CNPJ = '" + oSett.CNPJEmpresa + "'";

                else
                    sFiltro += "PRODUTO = '" + editEtiqueta.Text + "' AND CNPJ = '" + oSett.CNPJEmpresa + "'";

                data.RowFilter = sFiltro;
                DataTable dtbNotaEtiquetaFiltro = new DataTable();
                if (data.Count > 0)
                    dtbNotaEtiquetaFiltro = data.ToTable().AsEnumerable().Skip(0).Take(1).CopyToDataTable();

                if (dtbNotaEtiquetaFiltro.Rows.Count > 0)
                {
                    string sSql = @"SELECT M.NOTA, M.CHAVENOTA, M.FINALIZADO,M.DATA, M.CNPJ, D.PRODUTO, 
                                           V.ETIQUETATXTTXT,M.XML,V.LOJA, V.ETIQUETATXT, D.QUANTIDADE, D.SKU, 
                                           D.PRODUTO, D.DESCRICAO
                                    FROM NOTAMASTER M
                                    LEFT OUTER JOIN NOTADETALHE  D
                                    ON M.NOTA = D.NOTA AND M.CNPJ=D.CNPJ
                                    LEFT OUTER JOIN VENDAS V
                                    ON M.CHAVENOTA = V.NOTAFISCAL
                                    WHERE M.CNPJ = @CNPJ
                                        AND M.NOTA = @NOTA
                                    ORDER BY M.DATA ";

                    ParametrosSQL.Add("CNPJ", dtbNotaEtiquetaFiltro.Rows[0]["CNPJ"].ToString());
                    ParametrosSQL.Add("NOTA", dtbNotaEtiquetaFiltro.Rows[0]["NOTA"].ToString());
                    DataTable dtbNotaEtiqueta = ClasseParametros.ConsultaBancoMysql(sSql, ParametrosSQL);

                    List<ProdutoEtiqueta> lstFinalizaEtq = new List<ProdutoEtiqueta>();

                    foreach (DataRow rNotaEtiqeutas in dtbNotaEtiqueta.Rows)
                    {
                        ProdutoEtiqueta oProdutoEtq = new ProdutoEtiqueta();
                        oProdutoEtq.SKU = rNotaEtiqeutas["SKU"].ToString();
                        oProdutoEtq.Quantidade = int.Parse(dtbNotaEtiqueta.Rows[0]["QUANTIDADE"].ToString());
                        oProdutoEtq.FaltandoLer = int.Parse(dtbNotaEtiqueta.Rows[0]["QUANTIDADE"].ToString());
                        oProdutoEtq.Descricao = rNotaEtiqeutas["DESCRICAO"].ToString();
                        oProdutoEtq.Codigo = rNotaEtiqeutas["PRODUTO"].ToString();

                        if (rNotaEtiqeutas["PRODUTO"].ToString() == editEtiqueta.Text)
                            oProdutoEtq.FaltandoLer--;

                        lstFinalizaEtq.Add(oProdutoEtq);
                    }

                    foreach (ProdutoEtiqueta oProdutoEtq in lstFinalizaEtq)
                    {
                        if (oProdutoEtq.FaltandoLer > 0)
                        {
                            frmProdutosAdicionais oProdutoAdicionais = new frmProdutosAdicionais();
                            oProdutoAdicionais.lblSKU.Text = oProdutoEtq.SKU;
                            oProdutoAdicionais.lblQuantidade.Text = "X " + oProdutoEtq.FaltandoLer;
                            oProdutoAdicionais.lblDescricao.Text = oProdutoEtq.Descricao;
                            oProdutoAdicionais.lblCodigo.Text = oProdutoEtq.Codigo;
                            oProdutoAdicionais.iQtd = oProdutoEtq.FaltandoLer;

                            oProdutoAdicionais.ShowDialog();

                            if (oProdutoAdicionais.DialogResult != DialogResult.OK)
                            {
                                editEtiqueta.Text = "";
                                editEtiqueta.Focus();
                                oProdutoAdicionais.Dispose();
                                return;
                            }

                            oProdutoAdicionais.Dispose();
                            oProdutoEtq.FaltandoLer--;
                        }
                    }

                    //SO PODE PASSAR DAQUI DEPOIS QUE FEZ O CHECKUP DE TODOS OS PRODUTOS
                    if (dtbNotaEtiqueta.Rows[0]["XML"].ToString() == "")
                    {
                        MessageBox.Show("Nota não encontrada para este CNPJ!");
                        imgBoxProcessando.Visible = false;
                        return;
                    }

                    if (dtbNotaEtiqueta.Rows[0]["ETIQUETATXTTXT"].ToString() == "")
                    {
                        MessageBox.Show("Etiqueta não encontrada para este CNPJ!");
                        imgBoxProcessando.Visible = false;
                        return;
                    }

                    DanfeViewModel oModel = DanfeViewModelCreator.CriarDeStringXml(Encoding.ASCII.GetString((byte[])dtbNotaEtiqueta.Rows[0]["XML"]));
                    if (!Directory.Exists(sPasta + "\\PDFBACKUP\\"))
                    {
                        Directory.CreateDirectory(sPasta + "\\PDFBACKUP\\");
                    }

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
                        if (ClasseParametros.sBanco != "interplace_bd1")
                        {
                            sOperadotransportador = "Transportador:";

                        }

                        string sDANFE = @"^XA
^FXDados 
^FO30,50^GB290,135,3^FS
^CFA,20
^CF0,18
^FO50,70^FD1^FS
^FO145,105^FD{0}^FS
^FO145,140^FD{2}^FS
^FO295,107^FD{1}^FS
^FO170,230^FD{3}^FS
^FO170,290^FD{4} {5} {6}^FS

^CF0,25
^FO85,70^FD-^FS
^FO110,70^FDSaida^FS
^FO50,105^FDNumero^FS
^FO235,105^FDSerie^FS
^FO50,140^FDEmissao^FS

^CF0,30
^FO250,200^FDChave de acesso^FS
^FO170,260^FDProtocolo de autorizacao de uso^FS

^FXCodigo de barras.
^BY2,1,100
^FO680,50^A0R,30,30^FPR,10^BC^FD{3}^FS

^FO50,450^GB550,1,3^FS

^FXDados do Emitente.
^CFA,20
^FO180,470^FD{7}^FS
^FO115,500^FD{8}^FS
^FO230,525^FD{9}^FS
^FO430,500^FD{10}^FS
^CF0,25
^FO50,470^FDEmitente^FS
^FO50,500^FDCNPJ^FS
^FO50,525^FDIncricao Estadual^FS
^FO400,500^FDUF^FS

^FXDados do Destinatario.
^CFA,20
^FO180,600^FD{11}^FS
^FO160,630^FD{12}^FS
^FO230,660^FD{13}^FS
^FO430,630^FD{14}^FS

^CF0,25
^FO50,600^FDDestinatario^FS
^FO50,630^FDCPF/CNPJ^FS
^FO50,660^FDIncricao Estadual^FS
^FO400,630^FDUF^FS
^FO50,750^FDDanfe Simplificada^FS
^FXDados Adicionais.
^FO50,800^GB550,1,3^FS
^FO50,970^GB550,1,3^FS

^CF0,30
^FO50,1000^FDDados Adicionais^FS
^CFA,20
^FO250,1040^FD{15}^FS
^FO120,1070^FD{16}^FS
^FO180,1100^FD{17} - {19}^FS
^FO150,1130^FD{18}^FS
^FO270,1130^FD{20}^FS
^FO630,1130^FD{21}^FS
^FO110,1160^FD{22}^FS
^FO310,1160^FD{23}^FS
^FO400,1160^FD{24}^FS

^CF0,23
^FO60,1040^FD{25}^FS
    ^FO60,1070^FDCNPJ:^FS
    ^FO60,1100^FDEndereco:^FS
    ^FO60,1130^FDNumero:^FS
    ^FO200,1130^FDBairro:^FS
    ^FO550,1130^FDCidade:^FS
    ^FO60,1160^FDCEP:^FS
    ^FO220,1160^FDEstado:^FS
    ^FO350,1160^FDPais:^FS

    ^XZ";

                        string sRazaoSocial = "";
                        if (oModel.Transportadora.RazaoSocial != null)
                        {
                            sRazaoSocial = oModel.Transportadora.RazaoSocial;
                            if (sRazaoSocial.Length > 30)
                                sRazaoSocial = oModel.Transportadora.RazaoSocial.Substring(30);
                        }
                        //^FO650,1100^FDComplemento:^FS
                        if (ClasseParametros.sBanco != "interplace_bd1")
                        {
                            sDANFE = string.Format(sDANFE,
                            oModel.NfNumero.ToString(),
                            oModel.NfSerie.ToString(),
                            oModel.DataHoraEmissao.ToString(),
                            oModel.ChaveAcesso,
                            oModel.ProtocoloAutorizacao,
                           "",//oModel.DataHoraEmissao.ToString(),
                           "",//DateTime.Parse(oModel.DataHoraEmissao.ToString()).ToShortTimeString(),
                           oModel.Emitente.NomeFantasia,
                           oModel.Emitente.CnpjCpf,
                           oModel.Emitente.Ie,
                           oModel.Emitente.EnderecoUf,
                           oModel.Destinatario.NomeFantasia,
                           oModel.Destinatario.CnpjCpf,
                           oModel.Destinatario.Ie,
                           oModel.Destinatario.EnderecoUf,
                           sRazaoSocial,
                           oModel.Transportadora.CnpjCpf,
                           oModel.Transportadora.EnderecoLinha1,
                           oModel.Transportadora.EnderecoNumero,
                           oModel.Transportadora.EnderecoComplemento,
                           oModel.Transportadora.EnderecoBairro,
                           oModel.Transportadora.Municipio,
                           oModel.Transportadora.EnderecoCep,
                           oModel.Transportadora.EnderecoUf,

                           "BR",
                           sOperadotransportador);
                        }
                        else
                        {
                            sDANFE = string.Format(sDANFE,
                            oModel.NfNumero.ToString(),
                            oModel.NfSerie.ToString(),
                            oModel.DataHoraEmissao.ToString(),
                            oModel.ChaveAcesso,
                            oModel.ProtocoloAutorizacao,
                            DateTime.Parse(oModel.DataHoraEmissao.ToString()).ToShortDateString(),
                            DateTime.Parse(oModel.DataHoraEmissao.ToString()).ToShortTimeString(),
                            oModel.Emitente.NomeFantasia,
                            oModel.Emitente.CnpjCpf,
                            oModel.Emitente.Ie,
                            oModel.Emitente.EnderecoUf,
                            oModel.Destinatario.NomeFantasia,
                            oModel.Destinatario.CnpjCpf,
                            oModel.Destinatario.Ie,
                            oModel.Destinatario.EnderecoUf,
                            "Orla Logistica e Armazenagem",
                            oModel.Transportadora.CnpjCpf,
                            "Rod ES010",
                            "2594",
                            "KM2,60",
                            "JD Limoeiro",
                            "Serra",
                            "29164-140",
                            "ES",
                            "BR",
                            "");
                        }

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

                        File.WriteAllText(sPasta + "\\MINIDANFEBACKUP\\" + dtbNotaEtiqueta.Rows[0]["CHAVENOTA"].ToString() + ".txt", s);

                        GCHandle h = GCHandle.Alloc(oByte, GCHandleType.Pinned);
                        IntPtr pnt = h.AddrOfPinnedObject();
                        RawPrinterHelper.SendStringToPrinter(oSett.ImpressoraTermica, s);
                        //RawPrinterHelper.SendBytesToPrinter(comboImpressora.Text, pnt, aRetorno.Length);
                        h.Free();
                    }
                    else
                    {
                        PdfDocument pdfdocument = new PdfDocument();
                        pdfdocument.LoadFromFile(sPasta + "\\PDFBACKUP\\" + Path.GetFileName(dtbNotaEtiqueta.Rows[0]["CHAVENOTA"].ToString()) + ".pdf");
                        pdfdocument.PrinterName = oSett.ImpressoraLaserJato;
                        pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                        pdfdocument.PrintDocument.Print();
                        pdfdocument.Dispose();
                        lNotaImpressa = true;
                    }

                    string sEtiqueta = Encoding.ASCII.GetString((byte[])dtbNotaEtiqueta.Rows[0]["ETIQUETATXTTXT"]);
                    if (dtbNotaEtiqueta.Rows[0]["LOJA"].ToString().ToUpper() == "ME2")
                    {
                        MercadoLivreOrla oJson = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreOrla>(sEtiqueta);
                        sEtiqueta = oJson.content;
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

                        var size = Marshal.SizeOf(oByte[0]) * oByte.Length;
                        var pBytes = Marshal.AllocHGlobal(size);
                        GCHandle h = GCHandle.Alloc(oByte, GCHandleType.Pinned);

                        try
                        {
                            IntPtr pnt = h.AddrOfPinnedObject();
                            RawPrinterHelper.SendBytesToPrinter(oSett.ImpressoraTermica, pBytes, size);
                        }
                        finally
                        {
                            Marshal.FreeCoTaskMem(pBytes);
                        }


                        //RawPrinterHelper.SendBytesToPrinter((oSett.ImpressoraTermica, s);
                        //RawPrinterHelper.SendBytesToPrinter(comboImpressora.Text, pnt, aRetorno.Length);
                        h.Free();

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

                        GCHandle h = GCHandle.Alloc(oByte, GCHandleType.Pinned);
                        IntPtr pnt = h.AddrOfPinnedObject();
                        RawPrinterHelper.SendStringToPrinter(oSett.ImpressoraTermica, s);
                        //RawPrinterHelper.SendBytesToPrinter(comboImpressora.Text, pnt, aRetorno.Length);
                        h.Free();
                    }
                    lEtiquetaImpressa = true;

                    ClasseParametros.ExecutabancoMySql("UPDATE VENDAS SET IMPRESSOES = COALESCE(0,IMPRESSOES) + 1, DATAIMPRESSAO = CURRENT_TIMESTAMP WHERE NOTAFISCAL = '" + dtbNotaEtiqueta.Rows[0]["CHAVENOTA"].ToString() + "'");
                    ClasseParametros.ExecutabancoMySql("UPDATE NOTAMASTER SET FINALIZADO = 1 WHERE CHAVENOTA = '" + dtbNotaEtiqueta.Rows[0]["CHAVENOTA"].ToString() + "'");

                    dtbNotaEtiqueta.Dispose();

                }
                else
                {
                    MessageBox.Show("Não foi encontrado este produto para este CNPJ!");
                }


                //Retorno etiqueta lida





            }
            else
            {
                string sSql = "SELECT ETIQUETATXT,LOJA,ETIQUETAPDF, COALESCE(0, IMPRESSOES) AS IMPRESSOES FROM VENDAS WHERE NOTAFISCAL = '" + editEtiqueta.Text + "'";
                DataTable dtbNotaEtiqueta = ClasseParametros.ConsultaBancoMysql(sSql, ParametrosSQL);

                if (dtbNotaEtiqueta.Rows.Count > 0)
                {
                    int iImpressoes = int.Parse(dtbNotaEtiqueta.Rows[0]["IMPRESSOES"].ToString());
                    if (iImpressoes > 0)
                    {
                        lblMensagem.Text = "Etiqueta já gerada!";
                        lblMensagem.Refresh();
                    }

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

                    File.WriteAllText(sPasta + "\\ETQBACKUP\\" + editEtiqueta.Text + ".txt", s);

                    GCHandle h = GCHandle.Alloc(oByte, GCHandleType.Pinned);
                    IntPtr pnt = h.AddrOfPinnedObject();
                    RawPrinterHelper.SendStringToPrinter(oSett.ImpressoraTermica, s);
                    //RawPrinterHelper.SendBytesToPrinter(comboImpressora.Text, pnt, aRetorno.Length);
                    h.Free();

                    if (dtbNotaEtiqueta.Rows[0]["LOJA"].ToString() == "MAGAZINE_LUIZA")
                    {
                        oByte = (byte[])dtbNotaEtiqueta.Rows[0]["ETIQUETAPDF"];
                        IntPtr pUnmanagedBytes = Marshal.AllocCoTaskMem(oByte.Length);//Allocate unmanaged memory
                        Marshal.Copy(oByte, 0, pUnmanagedBytes, oByte.Length);//Copy bytes into unmanaged memory
                        bool retval = RawPrinterHelper.SendBytesToPrinter(oSett.ImpressoraTermica, pUnmanagedBytes, oByte.Length);//Send bytes to printer
                        Marshal.FreeCoTaskMem(pUnmanagedBytes);// Free the allocated unmanaged memory
                    }

                    ClasseParametros.ExecutabancoMySql("UPDATE VENDAS SET IMPRESSOES = COALESCE(0,IMPRESSOES) + 1, DATAIMPRESSAO = CURRENT_TIMESTAMP WHERE NOTAFISCAL = '" + editEtiqueta.Text + "'");
                }
                else
                {

                    sSql = "SELECT * FROM ETIQUETAJADLOG WHERE CHAVEACESSO = '" + editEtiqueta.Text + "'";
                    dtbNotaEtiqueta = ClasseParametros.ConsultaBancoMysql(sSql, ParametrosSQL);
                    if (dtbNotaEtiqueta.Rows.Count > 0)
                    {

                        MessageBox.Show("Nota será enviada pela coleta JADLOG");
                    }
                    else
                    {
                        lblMensagem.Text = "Não foi encontrado etiqueta para esta chave!";
                    }
                }

                dtbNotaEtiqueta.Dispose();
            }

            //Retorno Orla
            System.Threading.Thread.Sleep(3000);
            editEtiqueta.Text = "";
            editEtiqueta.Focus();
            imgBoxProcessando.Visible = false;
            lblMensagem.Text = "";

            CarregaGrids();
        }

        [Obsolete]
        private void BtnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensagem.Text = "Processando Aguarde!";
                Application.DoEvents();
                GerarEtiqueta(editEtiqueta.Text);
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Etiqueta não encontrada!\n" + ex.Message;

                imgBoxProcessando.Visible = false;
            }
        }

        private void EditEtiqueta_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GerarEtiqueta(editEtiqueta.Text);
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Erro a Ler Etiqueta, favor entrar em contato com TI Interplace";

                MessageBox.Show("Erro Tratado" + ex.Message + ex.StackTrace);
            }
        }

        private void Integrador_Etiquetas_Load(object sender, EventArgs e)
        {
            AutoUpdater.Start("http://191.252.61.26/update/update.xml");

            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");

            editEmpresa.Text = oSett.CNPJEmpresa;
            lblCNPJ.Visible = oSett.PorArquivo;
            editEmpresa.Visible = oSett.PorArquivo;

            TTimerNota = new System.Threading.Timer(
                            new TimerCallback(TickTimerNota),
                            null,
                            1000,
                            100000);

            frmLogin oLogin = new frmLogin();

            DialogResult oRestul = oLogin.ShowDialog();

            if (oRestul != DialogResult.OK)
            {
                this.BeginInvoke(new MethodInvoker(this.Close));
            }

            oLogin.Dispose();

            CarregaGrids();

            lblUsuario.Text = "Logado como " + ClasseParametros.sUsuarioSistema;

            if (ClasseParametros.lPermiteReimpressao)
                menuReimpressao.Enabled = true;


        }

        private void TickTimerNota(object state)
        {
            if (lRodandoTimer)
                return;
            lRodandoTimer = true;

            CarregaGrids();

            lRodandoTimer = false;
        }

        private void CarregaGrids()
        {
            if (editEmpresa.Text != "")
            {
                string sSql = @"SELECT N.CNPJ, N.NOTA,N.NOTASERIE, D.PRODUTO,N.DATA,V.LOJA, IF(N.XML IS NULL,0,1) AS ND, IF(V.ETIQUETATXTTXT IS NULL,0,1) AS ED,D.SKU
                                FROM NOTAMASTER N 
                                LEFT OUTER JOIN NOTADETALHE D
                                  ON N.NOTA = D.NOTA AND N.CNPJ = D.CNPJ
                                LEFT OUTER JOIN VENDAS V
                                  ON N.CHAVENOTA = V.NOTAFISCAL
                                WHERE N.FINALIZADO = 0
                                  AND N.CNPJ='" + editEmpresa.Text + "'" +
                                "ORDER BY N.DATA";
                DataTable dtbNImpressoes = ClasseParametros.ConsultaBancoMysql(sSql);

                gridEtiquetasNImpressas.BeginInvoke((MethodInvoker)delegate ()
                                {
                                    gridEtiquetasNImpressas.DataSource = dtbNImpressoes;
                                });

                sSql = @"SELECT  NM.CNPJ,NM.NOTA,ND.PRODUTO
                            FROM NOTAMASTER NM
                                LEFT OUTER JOIN NOTADETALHE  ND
                                ON NM.NOTA = ND.NOTA AND NM.CNPJ = ND.CNPJ
                            WHERE NM.FINALIZADO = 1
                              AND NM.CNPJ='" + editEmpresa.Text + "'" +
                            "ORDER BY NM.DATA DESC ";

                DataTable dtbImpressoes = ClasseParametros.ConsultaBancoMysql(sSql);
                gridImpressoes.BeginInvoke((MethodInvoker)delegate ()
                {
                    gridImpressoes.DataSource = dtbImpressoes;
                });

                dtbImpressoes.Dispose();

                ClasseParametros.CarregaParametros();

                colSKU.Visible = ClasseParametros.lImpressaoPorSKU;
                colProduto.Visible = !ClasseParametros.lImpressaoPorSKU;
            }

            //else
            //{
            //    string[] aCNPJ = oSett.CNPJClientes.Split(',');
            //    string sCnpjClientes = "";
            //    foreach (string sCNPJ in aCNPJ)
            //    {
            //        if (sCnpjClientes != "")
            //            sCnpjClientes += ",";

            //        sCnpjClientes += "'" + sCNPJ + "'";

            //    }



            //    string sSql = @"SELECT '' AS CNPJ,V.NOTAFISCAL AS NOTA,V.DATACRIADO AS DATA,V.LOJA, IF(V.ETIQUETATXTTXT IS NULL,0,1) AS ED
            //                FROM  VENDAS V
            //                WHERE V.IMPRESSOES IS NULL
            //                  AND V.CODIGOCLIENTE IN (" + sCnpjClientes + ") " +
            //                "ORDER BY V.DATAIMPRESSAO";
            //    DataTable dtbNImpressoes = ClasseParametros.ConsultaBancoMysql(sSql);
            //    gridEtiquetasNImpressas.BeginInvoke((MethodInvoker)delegate ()
            //    {
            //        gridEtiquetasNImpressas.DataSource = dtbNImpressoes;
            //    });
            //    sSql = @"SELECT '' AS CNPJ,V.NOTAFISCAL AS NOTA, V.DATAIMPRESSAO AS DATA
            //                FROM VENDAS V
            //                WHERE V.IMPRESSOES > 0 AND V.DATAIMPRESSAO BETWEEN '" + DateTime.Now.AddDays(-2).ToString("yyyyMMdd") + "' AND '" + DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "' " +
            //                "AND V.CODIGOCLIENTE IN (" + sCnpjClientes + ") " +
            //                "ORDER BY V.DATAIMPRESSAO";

            //    DataTable dtbImpressoes = ClasseParametros.ConsultaBancoMysql(sSql);
            //    gridImpressoes.BeginInvoke((MethodInvoker)delegate ()
            //    {
            //        gridImpressoes.DataSource = dtbImpressoes;
            //    });


            //    dtbImpressoes.Dispose();
            //}
        }

        private void ComboImpressora_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Integrador_Etiquetas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.M)
            {
                //SoundPlayer snd = new SoundPlayer(Properties.Resources.smw_coin);
                //snd.Play();
                BtnConfigurar_Click(null, null);
            }
            else if (e.KeyCode == Keys.F5)
            {
                CarregaGrids();
            }
        }

        private void ComboImpressoraLaserJato_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EditEmpresa_TextChanged(object sender, EventArgs e)
        {
  
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {

        }

        private void BtnConfigurar_Click(object sender, EventArgs e)
        {
            frmConfiguracao oFrm = new frmConfiguracao();
            oFrm.ShowDialog();
            lblCNPJ.Visible = oSett.PorArquivo;
            editEmpresa.Visible = oSett.PorArquivo;
            oSett.Reload();
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
        }

        private void AnularNotaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

            string sNota = tabEtiquetasNImpressas.GetFocusedRowCellDisplayText("NOTA");
            string sCNPJ = tabEtiquetasNImpressas.GetFocusedRowCellDisplayText("CNPJ");

            string sSql = "UPDATE NOTAMASTER SET FINALIZADO = 1 WHERE CNPJ = @CNPJ AND NOTA = @NOTA";
            ParametrosSQL.Add("CNPJ", sCNPJ);
            ParametrosSQL.Add("NOTA", sNota);

            ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);

            MessageBox.Show("Nota " + sNota + " do CNPJ " + sCNPJ + " anulada com sucesso!");
            CarregaGrids();

        }

        private void Button1_Click(object sender, EventArgs e)
        {




        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

            string sNota = tabImpressoes.GetFocusedRowCellDisplayText("NOTA");
            string sCNPJ = tabImpressoes.GetFocusedRowCellDisplayText("CNPJ");

            string sSql = "UPDATE NOTAMASTER SET FINALIZADO = 0, DATAREIMPRESSAO = @DATAREIMPRESSAO WHERE CNPJ = @CNPJ AND NOTA = @NOTA";

            ParametrosSQL.Add("CNPJ", sCNPJ);
            ParametrosSQL.Add("NOTA", sNota);
            ParametrosSQL.Add("DATAREIMPRESSAO", DateTime.Now);

            ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);

            MessageBox.Show("Nota " + sNota + " do CNPJ " + sCNPJ + " pode imprimir novamente com sucesso!");
            CarregaGrids();
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            frmRomaneio oFrm = new frmRomaneio();
            oFrm.ShowDialog();
        }

        private void gridEtiquetasNImpressas_Click(object sender, EventArgs e)
        {

        }

        private void btnSeparacao_Click(object sender, EventArgs e)
        {
            frmSeparacao oFrm = new frmSeparacao();
            oFrm.editCNPJ.Text = editEmpresa.Text;
            oFrm.editCNPJMinhas.Text = editEmpresa.Text;
            oFrm.ShowDialog();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void imgLogo_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog imgLogoDialog = new OpenFileDialog();

            if (imgLogoDialog.ShowDialog() == DialogResult.OK)
            {
                imgLogo.Image = Image.FromFile(imgLogoDialog.FileName);
                byte[] oIMGStream = File.ReadAllBytes(imgLogoDialog.FileName);

                Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

                string sSql = "UPDATE CLIENTE SET LOGO = @LOGO WHERE CODIGO =" + ClasseParametros.ClienteSelecionado;
                ParametrosSQL.Add("LOGO", oIMGStream);

                ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);

                ParametrosSQL = null;
            }
        }

        private void editEmpresa_Leave(object sender, EventArgs e)
        {
            oSett.CNPJEmpresa = editEmpresa.Text;
            oSett.Save();

            DataTable dtbCliente = ClasseParametros.ConsultaBancoMysql(@"SELECT * FROM CLIENTE WHERE CNPJCLIENTES = '" + editEmpresa.Text + "'");
            if (dtbCliente.Rows.Count > 0)
                ClasseParametros.ClienteSelecionado = dtbCliente.Rows[0]["CODIGO"].ToString();
            dtbCliente.Dispose();

            //carrega informações primarias do sistema
            DataTable dtbEmpresa = ClasseParametros.ConsultaBancoMysql("SELECT * FROM CLIENTE WHERE CODIGO = " + ClasseParametros.ClienteSelecionado);

            if(dtbEmpresa.Rows.Count==0)
            {
                MessageBox.Show("Cnpj não encontrado!");
                return;
            }

            if (dtbEmpresa.Rows[0]["LOGO"].ToString() != "")
            {
                byte[] aByte = (byte[])dtbEmpresa.Rows[0]["LOGO"];

                MemoryStream ms = new MemoryStream((byte[])dtbEmpresa.Rows[0]["LOGO"]);
                Image returnImage = Image.FromStream(ms);
                imgLogo.Image = returnImage;
            }

            CarregaGrids();
        }
    }
}