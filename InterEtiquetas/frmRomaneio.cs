using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using DanfeSharp.Modelo;
using DocumentFormat.OpenXml.Spreadsheet;
using InterRegraNegocio;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace InterEtiquetas
{
    public partial class frmRomaneio : MetroFramework.Forms.MetroForm
    {
        dstRomaneio oDstRomaneio = new dstRomaneio();
        public frmRomaneio()
        {
            InitializeComponent();
        }
        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void EditNota_KeyDown(object sender, KeyEventArgs e)
        {
            Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

            if (e.KeyCode == Keys.Return)
            {
                if (oDstRomaneio.Tables[0].Rows.Count == 50)
                {
                    MessageBox.Show("Máximo de 50 notas por romaneio");
                    return;
                }

                string sSql = @"SELECT * FROM NOTAMASTER WHERE ROMANEIO = 0 AND CHAVENOTA = @CHAVENOTA";
                ParametrosSQL.Add("CHAVENOTA", editNota.Text);
                DataTable dtbRomaneio = ClasseParametros.ConsultaBancoMysql(sSql, ParametrosSQL);
                if (dtbRomaneio.Rows.Count > 0)
                {
                    foreach (DataRow r in dtbRomaneio.Rows)
                    {
                        DanfeViewModel oModel = DanfeViewModelCreator.CriarDeStringXml(Encoding.ASCII.GetString((byte[])r["XML"]));

                        DataRow rRomaneio = oDstRomaneio.Tables[0].NewRow();
                        rRomaneio["CLIENTE"] = oModel.Emitente.RazaoSocial;
                        rRomaneio["TRANSPORTADORA"] = oModel.Transportadora.RazaoSocial;
                        rRomaneio["NUMERONOTA"] = oModel.NfNumero;
                        rRomaneio["DATAENTREGA"] = "Não consta";
                        rRomaneio["DESTINATARIO"] = oModel.Destinatario.RazaoSocial;
                        rRomaneio["CIDADE"] = oModel.Destinatario.Municipio;
                        rRomaneio["UF"] = oModel.Destinatario.EnderecoUf;
                        rRomaneio["CHAVENOTA"] = r["CHAVENOTA"].ToString();
                        rRomaneio["VOLUME"] = oModel.Transportadora.QuantidadeVolumes;
                        rRomaneio["VALOR"] = oModel.CalculoImposto.ValorTotalNota;

                        double eQtd = 0;
                        foreach (ProdutoViewModel oProduto in oModel.Produtos)
                        {
                            eQtd += oProduto.Quantidade;
                        }

                        rRomaneio["DATAEMISSAO"] = oModel.DataHoraEmissao;
                        rRomaneio["QTDPRODUTO"] = eQtd;

                        oDstRomaneio.Tables[0].Rows.Add(rRomaneio);
                        editNota.Text = "";
                        editNota.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Nota não encontrada ou já impressa o romaneio!");
                }
            }
        }

        private void EditNota_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmRomaneio_Load(object sender, EventArgs e)
        {
            oDstRomaneio.Tables[0].Columns.Add("DATAEMISSAO");
            oDstRomaneio.Tables[0].Columns.Add("QTDPRODUTO");
            oDstRomaneio.Tables[0].Columns.Add("VALOR");

            gridNota.DataSource = oDstRomaneio.Tables[0];
        }

        private void BtnConfigurar_Click(object sender, EventArgs e)
        {
            SqlConnection oConexaoInterno = null;
            Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

            Properties.Settings oSett = new Properties.Settings();

            string sPastaDataHoje = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;

            XLWorkbook oWorkbook = new XLWorkbook(Directory.GetCurrentDirectory() + "\\Arquivo\\romaneio.xlsx");
            var ws = oWorkbook.Worksheet(1);

            ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            if (ClasseParametros.lImpressaoRomaneioRetrato)
                ws.PageSetup.PageOrientation = XLPageOrientation.Portrait;

            Image returnImage = null;
            DataTable dtbEmpresa = ClasseParametros.ConsultaBancoMysql("SELECT * FROM CLIENTE WHERE CODIGO = " + ClasseParametros.ClienteSelecionado);
            if (dtbEmpresa.Rows[0]["LOGO"].ToString() != "")
            {
                byte[] aByte = (byte[])dtbEmpresa.Rows[0]["LOGO"];

                Stream ms = new MemoryStream((byte[])dtbEmpresa.Rows[0]["LOGO"]);
                returnImage = Image.FromStream(ms);

                ms.Position = 0;
                ws.AddPicture(ms, XLPictureFormat.Png, "Image11")
                    .MoveTo(ws.Cell(1, 1));

                //IXLPicture oImageExcel = ws.AddPicture(ms);
                //var picture = new IXLPicture()
                //{
                //    FilePath = filepath,
                //    WidthPx = 100,
                //    HeightPx = 100,
                //    Type = XLPictureType.Jpeg,
                //    CanUserChangeAspect = false,
                //};
                //picture.SetMarker(new IXLMarker()
                //{
                //    RowIndex = 12,
                //    ColumnIndex = 13,
                //    RowOffsetPx = 5,
                //    ColumnOffsetPx = 10,
                //});
                ////worksheet.Pictures.Add(picture);
                //ws.AddPicture(ms, XLPictureFormat.Png, "img")
                //    .WithPlacement(XLPicturePlacement.FreeFloating)
                //    .WithSize(300, 200);
                //    //.ScaleWidth(11)

            }

            // Change the background color of the headers
            //var rngHeaders = ws.Range("B3:F3");
            //rngHeaders.Style.Fill.BackgroundColor = XLColor.LightSalmon;

            // Change the date formats
            //var rngDates = ws.Range("A12:a");
            //rngDates.Style.DateFormat.Format = "MM/dd/yyyy";

            var oCelula = ws.Cell(5, "F");
            oCelula.Value = oDstRomaneio.Tables[0].Rows.Count.ToString();
            oCelula = ws.Cell(7, "G");
            oCelula.Value = "Dt. Mov.: " + DateTime.Now.ToString();
            oCelula = ws.Cell(1, "A");
            oCelula.Value = oDstRomaneio.Tables[0].Rows[0]["CLIENTE"].ToString() + "\r\n" + oDstRomaneio.Tables[0].Rows[0]["TRANSPORTADORA"].ToString();

            // Change the income values to text
            int iLinha = 12;
            int iContador = 1;
            foreach (DataRow rRomaneio in oDstRomaneio.Tables[0].Rows)
            {
                var rngNumbers = ws.Range("A" + iLinha + ":G" + iLinha);
                int iCell = 0;

                foreach (var cell in rngNumbers.Cells())
                {
                    switch (iCell)
                    {
                        case 0:
                            cell.Value = iContador.ToString();
                            break;
                        case 1:
                            cell.Value = rRomaneio["NUMERONOTA"].ToString();
                            break;
                        case 2:
                            cell.Value = rRomaneio["DATAEMISSAO"].ToString();
                            break;
                        case 3:
                            cell.Value = rRomaneio["QTDPRODUTO"].ToString();
                            break;
                        case 4:
                            cell.Value = rRomaneio["VOLUME"].ToString();
                            break;
                        case 5:
                            cell.Value = rRomaneio["VALOR"].ToString();
                            break;
                        case 6:
                            cell.Value = rRomaneio["DESTINATARIO"].ToString();
                            break;
                    }
                    iCell++;
                }
                iLinha++;
                iContador++;

            }

            iLinha += 4;
            oCelula = ws.Cell(iLinha, "A");
            oCelula.Value = "___________________________________";
            iLinha++;
            oCelula = ws.Cell(iLinha, "A");
            oCelula.Value = "CONFERENTE";

            iLinha--;
            oCelula = ws.Cell(iLinha, "G");
            oCelula.Value = "___________________________________";
            iLinha++;
            oCelula = ws.Cell(iLinha, "G");
            oCelula.Value = "ENCARREGADO";

            iLinha += 3;
            oCelula = ws.Cell(iLinha, "D");
            oCelula.Value = "___________________________________";
            iLinha++;
            oCelula = ws.Cell(iLinha, "D");
            oCelula.Value = "MOTORISTA";

            oCelula = ws.Cell(25, "C");
            oCelula.Value = "___________________________________";
            iLinha++;
            oCelula = ws.Cell(26, "C");
            oCelula.Value = oDstRomaneio.Tables[0].Rows[0]["TRANSPORTADORA"].ToString();

            string sPasta = Directory.GetCurrentDirectory() + "\\Romaneio\\";

            if (!Directory.Exists(sPasta))
            {
                Directory.CreateDirectory(sPasta);
            }

            //DataTable dt = oDstRomaneio.Tables[0];
            //wb.Worksheets.Add(dt, "Romaneio");
            //string sPAsta = Directory.GetCurrentDirectory() + "\\" + oDstRomaneio.Tables[0].Rows[0]["CLIENTE"].ToString() + "\\" + sPastaDataHoje;

            //if (!Directory.Exists(sPAsta))
            //{
            //    Directory.CreateDirectory(sPAsta);
            //}

            sPastaDataHoje += "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString();
            oWorkbook.SaveAs(sPasta + "\\romaneio" + sPastaDataHoje + ".xlsx");
            using (var pd = new PrintDialog())
            {
                pd.ShowDialog();
                var info = new ProcessStartInfo()
                {
                    Verb = "print",
                    CreateNoWindow = true,
                    FileName = sPasta + "\\romaneio" + sPastaDataHoje + ".xlsx",
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(info);
            }

            foreach (DataRow r in oDstRomaneio.Tables[0].Rows)
            {
                string sSql = @"UPDATE NOTAMASTER SET ROMANEIO = 1 WHERE CHAVENOTA = @CHAVENOTA";
                ParametrosSQL.Add("CHAVENOTA", r["CHAVENOTA"].ToString());
                ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);
                //DataTable dtbRomaneio = ClasseParametros.ConsultaBancoMysql(sSql, ParametrosSQL);
            }

            oDstRomaneio.Tables[0].Clear();
        }

        private void menuExcluirNota_Click(object sender, EventArgs e)
        {
            string sChaveNota = tabNota.GetRowCellValue(tabNota.FocusedRowHandle, "CHAVENOTA").ToString();

            for (int i = oDstRomaneio.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = oDstRomaneio.Tables[0].Rows[i];
                if (dr["CHAVENOTA"].ToString() == sChaveNota)
                    dr.Delete();
            }
            oDstRomaneio.Tables[0].AcceptChanges();
        }
    }
}
