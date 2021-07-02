using interRegraNegocio.FortePlus;
using interRegraNegocio.MercadoLivre;
using InterRegraNegocio;
using InterRegraNegocio.FortePlus;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InterGerente
{
    public partial class frmRelatorios : InterRegraNegocio.frmGenericoAjuste
    {
        public frmRelatorios()
        {
            InitializeComponent();
        }

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            //if (tabControleEstoque.SelectedTabPageIndex == 0) // Estoque
            //{
            //    DataTable dtbProduto = new DataTable();

            //    dtbProduto.Columns.Add("PRODUTO");
            //    dtbProduto.Columns.Add("IDEXTERNO");
            //    dtbProduto.Columns.Add("QUANTIDADEFORTPLUS");
            //    dtbProduto.Columns.Add("QUANTIDADEMERCADOLIVRE");
            //    dtbProduto.Columns.Add("STATUS");

            //    int? iLocalEstoque31 = ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "31");
            //    int? iLocalEstoque33 = ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "33");
            //    double iEstoqueAtual = 0;

            //    IRestResponse oResposta = ClasseFuncoes.RetornaProdutosFortPlus();
            //    List<InterRegraNegocio.FortePlus.ProdutoFortePlus> lstProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InterRegraNegocio.FortePlus.ProdutoFortePlus>>(oResposta.Content);

            //    barProgresso.Maximum = lstProduto.Count;
            //    barProgresso.Value = 0;
            //    int iValue = 0;

            //    foreach (InterRegraNegocio.FortePlus.ProdutoFortePlus oProduto in lstProduto)
            //    {
            //        iEstoqueAtual = 0;
            //        oResposta = ClasseFuncoes.RetornaProdutoEstoqueFortPlus(oProduto.prCodigo);
            //        List<InterRegraNegocio.FortePlus.FortPlusEstoque> oJsonProdutoEstoqueFortePlus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InterRegraNegocio.FortePlus.FortPlusEstoque>>(oResposta.Content);
            //        foreach (InterRegraNegocio.FortePlus.FortPlusEstoque oEstoque in oJsonProdutoEstoqueFortePlus)
            //        {
            //            if (oEstoque.idLocalEstoque == iLocalEstoque31.ToString() || oEstoque.idLocalEstoque == iLocalEstoque33.ToString())
            //                if (oEstoque.codigo == oProduto.prCodigo)
            //                    if (double.Parse(oEstoque.qtdeAtual) > 0)
            //                    {
            //                        iEstoqueAtual = double.Parse(oEstoque.qtdeAtual);
            //                        break;
            //                    }
            //        }

            //        oResposta = ClasseFuncoes.RetornaProdutoComplementoFortPlus(oProduto.id);
            //        List<ProdutoComplemento> lstProdutoComplementosFortePlus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(oResposta.Content);

            //        foreach (ProdutoComplemento oComplemento in lstProdutoComplementosFortePlus)
            //        {

            //            if (oComplemento.cmIdExterno == "MLB1399884553")
            //            {

            //            }

            //            double eResultado = 0;
            //            List<Parameter> ps = new List<Parameter>();
            //            Parameter p1 = new Parameter();
            //            p1.Name = "access_token";
            //            p1.Value = ClasseParametros.oMeli.AccessToken;
            //            ps.Add(p1);

            //            oResposta = ClasseParametros.oMeli.Get("/items/" + oComplemento.cmIdExterno, ps);

            //            MercadoLivreProdutoRetorno oMLProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreProdutoRetorno>(oResposta.Content);

            //            if (eResultado != oMLProduto.available_quantity)
            //            {
            //                DataRow r = dtbProduto.NewRow();
            //                r["PRODUTO"] = oProduto.prNome;
            //                r["IDEXTERNO"] = oComplemento.cmIdExterno;
            //                double ePorcento = 0;
            //                if (oComplemento.cmPercentual != null)
            //                    ePorcento = double.Parse(oComplemento.cmPercentual.ToString());

            //                double ePercentual = double.Parse(ePorcento.ToString());
            //                eResultado = Math.Floor(iEstoqueAtual * (ePercentual / 100));

            //                r["QUANTIDADEFORTPLUS"] = eResultado;

            //                r["QUANTIDADEMERCADOLIVRE"] = oMLProduto.available_quantity;
            //                r["STATUS"] = oMLProduto.status;
            //                dtbProduto.Rows.Add(r);
            //            }




            //        }
            //        iValue++;
            //        barProgresso.Value = iValue;
            //        barProgresso.Refresh();
            //    }
            //    //gridEstoque.DataSource = dtbProduto;
            //    StreamWriter sw = new StreamWriter(@"C:\Users\rrgnu\OneDrive\Documentos\Interplace/relatorio.txt");
            //    string line = "";


            //    foreach (DataRow dr in dtbProduto.Rows)
            //    {

            //        foreach (DataColumn dc in dtbProduto.Columns)
            //        {
            //            line += dc.ColumnName + ":" + dr[dc].ToString() + ";";
            //        }
            //        line += "\r\n";
            //    }
            //    sw.WriteLine(line);
            //    sw.Close();
            //}
            //else if (tabControleEstoque.SelectedTabPageIndex == 1)// Percentual
            //{
            //    DataTable dtbProduto = new DataTable();

            //    dtbProduto.Columns.Add("PRODUTO");
            //    dtbProduto.Columns.Add("SKU");
            //    dtbProduto.Columns.Add("PRODUTOCOMPLEMENTO");
            //    dtbProduto.Columns.Add("PERCENTUAL");
            //    dtbProduto.Columns.Add("MARKETPLACE");
            //    dtbProduto.Columns.Add("STATUS");

            //    IRestResponse oResposta = null;
            //    while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
            //    {
            //        RestClient client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Produto");
            //        RestRequest request = new RestRequest(Method.GET);
            //        request.AddHeader("Cache-Control", "no-cache");
            //        request.AddHeader("Accept", "*/*");
            //        request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
            //        request.AddHeader("Content-Type", "application/json");
            //        request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

            //        oResposta = client.Execute(request);

            //        if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            //        {
            //            ClasseFuncoes.ConectaForteplus(5);
            //        }
            //        else if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
            //        {
            //            break;
            //        }
            //    }
            //    List<ProdutoFortePlus> oProdutoFortPlusConsulta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoFortePlus>>(oResposta.Content);
            //    barProgresso.Maximum = oProdutoFortPlusConsulta.Count;
            //    barProgresso.Value = 0;
            //    int iValue = 0;
            //    foreach (ProdutoFortePlus oProduto in oProdutoFortPlusConsulta)
            //    {
            //        List<ProdutoComplemento> lstComplemento = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(ClasseFuncoes.RetornaProdutoComplementoFortPlus(oProduto.id).Content);
            //        foreach (ProdutoComplemento oComplemento in lstComplemento)
            //        {
            //            if (lstComplemento != null && lstComplemento.Count > 0)
            //            {
            //                // Status mercado livre
            //                List<Parameter> ps = new List<Parameter>();
            //                Parameter p1 = new Parameter();
            //                p1.Name = "access_token";
            //                p1.Value = ClasseParametros.oMeli.AccessToken;
            //                ps.Add(p1);

            //                oResposta = ClasseParametros.oMeli.Get("/items/" + oComplemento.cmIdExterno, ps);

            //                MercadoLivreProdutoRetorno oMLProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreProdutoRetorno>(oResposta.Content);

            //                DataRow oRow = dtbProduto.NewRow();
            //                oRow["PRODUTO"] = oProduto.prNome;
            //                oRow["PRODUTOCOMPLEMENTO"] = oComplemento.cmTitulo;
            //                oRow["PERCENTUAL"] = oComplemento.cmPercentual;
            //                oRow["MARKETPLACE"] = ClasseFuncoes.RetornaDescricaoGlobal("MK", oComplemento.cmIdMarketPlace.ToString());
            //                oRow["STATUS"] = oMLProduto.status;
            //                oRow["SKU"] = oProduto.prCodigo;
            //                dtbProduto.Rows.Add(oRow);
            //            }
            //        }
            //        iValue++;
            //        barProgresso.Value = iValue;
            //        barProgresso.Refresh();
            //    }
            //    gridPercente.DataSource = dtbProduto;
            //}
            //else if (tabControleEstoque.SelectedTabPageIndex == 2)// Duplicado
            //{
            //    DataTable dtbPedidoTemp = new DataTable();
            //    DataTable dtbPedidoTemp1 = new DataTable();
            //    DataTable dtbPedido = new DataTable();

            //    dtbPedido.Columns.Add("NOME");
            //    dtbPedido.Columns.Add("PEDIDO");
            //    dtbPedidoTemp.Columns.Add("PEDIDO");
            //    dtbPedidoTemp.Columns.Add("NOME");
            //    dtbPedidoTemp1.Columns.Add("PEDIDO");
            //    dtbPedidoTemp1.Columns.Add("NOME");


            //    IRestResponse oResposta = null;

            //    while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
            //    {
            //        RestClient client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido");
            //        RestRequest request = new RestRequest(Method.GET);
            //        request.AddHeader("Cache-Control", "no-cache");
            //        request.AddHeader("Accept", "*/*");
            //        request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
            //        request.AddHeader("Content-Type", "application/json");
            //        request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

            //        oResposta = client.Execute(request);

            //        if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            //        {
            //            ClasseFuncoes.ConectaForteplus(5);
            //        }
            //    }

            //    List<Pedido> lstPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Pedido>>(oResposta.Content);

            //    foreach (Pedido oPedido in lstPedido)
            //    {
            //        if (oPedido.mvIdExterno != null)
            //        {
            //            DataRow r = dtbPedidoTemp.NewRow();
            //            r["PEDIDO"] = oPedido.mvIdExterno;
            //            r["NOME"] = oPedido.mvChaveAcesso;
            //            dtbPedidoTemp.Rows.Add(r);
            //        }
            //    }

            //    foreach (Pedido oPedido in lstPedido)
            //    {
            //        if (oPedido.mvIdExterno != null)
            //        {
            //            DataRow r = dtbPedidoTemp1.NewRow();
            //            r["PEDIDO"] = oPedido.mvIdExterno;
            //            r["NOME"] = oPedido.mvChaveAcesso;
            //            dtbPedidoTemp1.Rows.Add(r);
            //        }
            //    }


            //    foreach (DataRow r in dtbPedidoTemp.Rows)
            //    {
            //        int i = 0;
            //        foreach (DataRow r1 in dtbPedidoTemp1.Rows)
            //        {
            //            if (r["PEDIDO"].ToString() == r1["PEDIDO"].ToString())
            //            {
            //                i++;
            //            }
            //        }
            //        if (i > 1)
            //        {
            //            DataRow oRow = dtbPedido.NewRow();
            //            oRow["PEDIDO"] = r["PEDIDO"].ToString();
            //            dtbPedido.Rows.Add(oRow);
            //        }
            //    }
            //}
        }

        private void PnlConteudo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            //    if (tabControleEstoque.SelectedTabPageIndex == 0)// Estoque
            //        gridView1.ExportToPdf(Directory.GetCurrentDirectory() + "//EstoqueExportado.pdf");
            //    else if (tabControleEstoque.SelectedTabPageIndex == 1)// Percentual
            //        tabPercente.ExportToPdf(Directory.GetCurrentDirectory() + "//PercentualExportado.pdf");

        }
    }
}
