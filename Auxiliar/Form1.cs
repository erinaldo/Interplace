using DanfeSharp.Modelo;
using interRegraNegocio.B2W;
using interRegraNegocio.FortePlus;
using interRegraNegocio.MercadoLivre;
using InterRegraNegocio;
using InterRegraNegocio.B2W;
using InterRegraNegocio.Bling;
using InterRegraNegocio.FortePlus;
using InterRegraNegocio.MagazineLuiza;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auxiliar
{
    public partial class Form1 : Form
    {

        //List<FortPlusXML> lstXML = ClasseFuncoes.RetornaListaXMLFortPlus();
        //List<ProdutoFortePlus> lstProdutoGerais = ClasseFuncoes.RetornaListProdutosFortPlus();
        List<FortPlusXML> lstXML = null;
        List<ProdutoFortePlus> lstProdutoGerais = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int iPagina = 1;
                int iPorPagina = 50;

                RestClient client = null;
                RestRequest request = null;
                IRestResponse oResposta = null;

                while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido");
                    request = new RestRequest(Method.GET);
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Accept", "*/*");
                    request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                    request.AddHeader("Content-Type", "application/json");

                    if (ClasseParametros.oJsonFortePluslogin == null)
                        ClasseFuncoes.ConectaForteplus(5);

                    request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                    oResposta = client.Execute(request);

                    if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        ClasseFuncoes.ConectaForteplus(5);
                    }
                    else if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        break;
                    }
                }

                List<Pedido> lstPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Pedido>>(oResposta.Content);
                List<FortPlusXML> lstXML = ClasseFuncoes.RetornaListaXMLFortPlus();

                lstPedido = lstPedido.Where(x => x.mvDmaEmissao != null && x.mvChaveAcesso == editJadlog.Text && x.mvEntidade == "NFE").ToList();

                foreach (Pedido oPedido in lstPedido)
                {
                    try
                    {
                        if (oPedido.mvChaveAcesso.Contains("20476"))
                        {

                        }

                        Console.WriteLine(" Requisição jadlog da nota " + oPedido.mvChaveAcesso);
                        ClasseFuncoes.GeraEtiquetaJADLOG(oPedido, 5, lstXML);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int iCliente = 5;

                if (ClasseParametros.iFilial == -1)
                {

                }

                if (ClasseParametros.oMeli == null)
                {
                    ClasseFuncoes.ConectaMercadoLivreAsync(iCliente);
                }

                int iOffset = 1;
                int ilimit = 50;

                IRestResponse oResposta = null;
                MercadoLivrePedido oPedidoMercadoLivre = null;


                while (oPedidoMercadoLivre == null || oPedidoMercadoLivre.results == null)
                {
                    try
                    {
                        List<Parameter> ps = new List<Parameter>();
                        Parameter p = new Parameter();
                        p.Name = "access_token";
                        p.Value = ClasseParametros.oMeli.AccessToken;
                        ps.Add(p);
                        p = new Parameter();
                        p.Name = "seller";
                        p.Value = ClasseParametros.oMeli.UserId;
                        ps.Add(p);

                        p = new Parameter();
                        p.Name = "q";
                        p.Value = editPedidoMercadoLivre.Text;
                        ps.Add(p);



                        var oRespostaPedido = ClasseParametros.oMeli.Get("/orders/search", ps);
                        oPedidoMercadoLivre = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivrePedido>(oRespostaPedido.Content);

                        if (oPedidoMercadoLivre.results.Length == 0 || oPedidoMercadoLivre.results[0].status == "shipped")
                            break;

                        if (oPedidoMercadoLivre.results[0].date_created < DateTime.Now.AddDays(-30))
                        {
                            break;
                        }



                        //Result[] aRessult = oPedidoMercadoLivre.results.Where(x => x.id.ToString() == "2538010356" || x.id.ToString() == "2531056580" || x.id.ToString() == "2530212967" || x.id.ToString() == "2526833324" || x.id.ToString() == "2535966174").ToArray();

                        foreach (Result oPedido in oPedidoMercadoLivre.results)
                        // foreach (Result oPedido in aRessult)
                        {

                            //if (oPedido.date_created < DateTime.Now.AddDays(-2))
                            //{
                            //    break;
                            //}

                            Console.WriteLine(" Integrando pedido " + oPedido.id.ToString());
                            string sCodigoUsado = oPedido.id.ToString();
                            string spacote = "";
                            if (oPedido.pack_id != null)
                            {
                                spacote = oPedido.pack_id.ToString();
                            }
                            try
                            {

                                if (oPedido.buyer.first_name.ToLower().Contains("adilio"))
                                {

                                }

                                if (spacote.ToString().Trim() == "2000000849143019")
                                {

                                }

                                if (sCodigoUsado.ToString().Trim() == "2614693635"
                                    || sCodigoUsado.ToString().Trim() == "2614456912"
                                    || sCodigoUsado.ToString().Trim() == "2614257443 "
                                    || sCodigoUsado.ToString().Trim() == "2614084672"
                                    || sCodigoUsado.ToString().Trim() == "2613984655"
                                    || sCodigoUsado.ToString().Trim() == "2613899261"
                                    || sCodigoUsado.ToString().Trim() == "2613615063"
                                    || sCodigoUsado.ToString().Trim() == "2613389954"
                                    || sCodigoUsado.ToString().Trim() == "2613235814"
                                    || sCodigoUsado.ToString().Trim() == "2612933107"
                                    )
                                {

                                }
                                if (oPedido.shipping.status == "delivered")
                                {
                                    continue;
                                }



                                if (oPedido.status == "paid" && (oPedido.shipping.status == "ready_to_ship" || oPedido.shipping.status == null))
                                {
                                    oResposta = null;
                                    while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                                    {
                                        RestClient client = null;
                                        if (spacote != "")
                                            client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/IdExterno/" + spacote.ToString().Trim());
                                        else
                                            client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/IdExterno/" + sCodigoUsado.ToString().Trim());

                                        RestRequest request = new RestRequest(Method.GET);
                                        request.AddHeader("Cache-Control", "no-cache");
                                        request.AddHeader("Accept", "*/*");
                                        request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                                        request.AddHeader("Content-Type", "application/json");

                                        if (ClasseParametros.oJsonFortePluslogin == null)
                                            ClasseFuncoes.ConectaForteplus(5);

                                        request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                                        oResposta = client.Execute(request);

                                        if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                        {
                                            ClasseFuncoes.ConectaForteplus(5);
                                        }
                                        else if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                                        {

                                            client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/IdExterno/" + sCodigoUsado.ToString().Trim());

                                            request = new RestRequest(Method.GET);
                                            request.AddHeader("Cache-Control", "no-cache");
                                            request.AddHeader("Accept", "*/*");
                                            request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                                            request.AddHeader("Content-Type", "application/json");

                                            if (ClasseParametros.oJsonFortePluslogin == null)
                                                ClasseFuncoes.ConectaForteplus(5);


                                            request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                                            oResposta = client.Execute(request);



                                            break;
                                        }
                                    }
                                    Pedido oPedidoFortPlusConsulta = Newtonsoft.Json.JsonConvert.DeserializeObject<Pedido>(oResposta.Content);

                                    #region Pega Itens do pedido
                                    bool lIntegrarNovamente = false;

                                    if (oPedidoFortPlusConsulta != null)
                                    {
                                        List<PedidoItemFortPlus> lstItem = ClasseFuncoes.RetornaItensPedido(oPedidoFortPlusConsulta.id);
                                        lIntegrarNovamente = lstItem.Count == 0;

                                        if (lIntegrarNovamente)
                                        {

                                        }
                                    }

                                    #endregion

                                    if (lIntegrarNovamente || oPedidoFortPlusConsulta == null)
                                    {
                                        bool lAtualiza = false;

                                        if (sCodigoUsado.ToString().Trim() == "2540493471")
                                        {

                                        }

                                        if (oPedido.status == "paid" && (oPedido.shipping.status == "ready_to_ship" || oPedido.shipping.status == null))
                                        {
                                            string sSql = "";

                                            if (spacote != "")
                                                sSql = "SELECT TRIM(PACKID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(PACKID) = '" + spacote.ToString().Trim() + "' AND STATUS = 0 AND MARKETPLACE = 'MERCADOLIVRE'";
                                            else
                                                sSql = "SELECT TRIM(ID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(ID) = '" + sCodigoUsado.ToString().Trim() + "' AND STATUS = 0 AND MARKETPLACE = 'MERCADOLIVRE'";
                                            DataTable d = ClasseParametros.ConsultaBancoMysql(sSql);
                                            if (d.Rows.Count == 0)
                                            {

                                                sSql = "INSERT INTO VENDAMARKETPLACE(ID,STATUS,MARKETPLACE, DATA,STATUSMENSAGEM,PACKID,USERID,EMAILML,PEDIDOML,SELLERID) " +
                                                    "VALUES('" + sCodigoUsado.ToString().Trim() + "',0,'MERCADOLIVRE', CURDATE(),0,'" + spacote + "', " +
                                                    "'" + oPedido.buyer.id.ToString() + "','" + oPedido.buyer.email + "','" + sCodigoUsado + "','" + oPedido.seller.id.ToString() + "')";
                                                ClasseParametros.ExecutabancoMySql(sSql);
                                            }
                                        }

                                        try
                                        {
                                            if (sCodigoUsado.ToString().Trim() == "2436237643")
                                            {

                                            }

                                            string sSql = "";
                                            if (spacote != "")
                                                sSql = "SELECT TRIM(PACKID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(PACKID) = '" + spacote.ToString().Trim() + "' AND STATUS = 0 AND MARKETPLACE = 'MERCADOLIVRE'";
                                            else
                                                sSql = "SELECT TRIM(ID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(ID) = '" + sCodigoUsado.ToString().Trim() + "' AND STATUS = 0 AND MARKETPLACE = 'MERCADOLIVRE'";
                                            DataTable d = ClasseParametros.ConsultaBancoMysql(sSql);
                                            if (lIntegrarNovamente || d.Rows.Count > 0)
                                            {
                                                if (oPedido.shipping.status == "ready_to_ship" || oPedido.shipping.status == null)
                                                {
                                                    oResposta = null;
                                                    MercadoLivreEntrega oEntrega = null;
                                                    while (oEntrega == null)
                                                    {

                                                        ps = new List<Parameter>();
                                                        p = new Parameter();
                                                        p.Name = "access_token";
                                                        p.Value = ClasseParametros.oMeli.AccessToken;
                                                        ps.Add(p);

                                                        //HttpParams oParametroEntretga = new HttpParams().Add("access_token", ClasseParametros.oMeli.AccessToken);

                                                        var oRespostaEntrega = ClasseParametros.oMeli.Get("/shipments/" + oPedido.shipping.id.ToString(), ps);


                                                        oEntrega = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreEntrega>(oRespostaEntrega.Content);
                                                    }
                                                    ClienteFortPlus oCliente = ClasseFuncoes.CadastraClienteFortPlus(oPedido, oEntrega);
                                                    string s = Newtonsoft.Json.JsonConvert.SerializeObject(oCliente);

                                                    if (oCliente == null)
                                                    {
                                                        ClasseFuncoes.EnviaMensagemTelegramAsync("Pedido não cadastrado pois o endereço está com erro!\n" + oPedido.buyer.first_name + " " + oPedido.buyer.last_name, "0,1", "Pedido não cadastrado", sCodigoUsado.ToString().Trim());

                                                        continue;
                                                    }

                                                    int iLocalEstoque = 32;
                                                    ClasseFuncoes.CarregaFiliais("34.036.601/0003-38 - 2ELETRO VAREJISTA");
                                                    //ClasseFuncoes.CarregaFiliais("34.036.601/0002-57 - 2ELETRO ATACADISTA");

                                                    //34036601000257 - 2ELETRO ATACADISTA	
                                                    //34036601000338 - 2ELETRO VAREJISTA


                                                    //34.036.601/0001-76 - 	2ELETRO MATRIZ	
                                                    //34.036.601/0002-57 - 2ELETRO ATACADISTA	
                                                    //34.036.601/0003-38 - 2ELETRO VAREJISTA

                                                    string sProduto = "";
                                                    try
                                                    {

                                                        foreach (Shipping_Items oItem in oEntrega.shipping_items)
                                                        {
                                                            ProdutoComplemento oProdutoComplemento = ClasseFuncoes.RetornaProdutoComplementoFortPlusPorIdExterno(oItem.id.Trim());
                                                            sProduto = "\nProduto: " + oItem.description + "\n";
                                                            sProduto += " MLB: " + oItem.id.Trim();
                                                            ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComplemento.cmIdProduto.ToString()).Content);
                                                            string sProdutoTemp = "";
                                                            //if (oProduto.prCodigo.ToString().Contains("MICFAC"))
                                                            //{
                                                            //    iLocalEstoque = 31;
                                                            //    break;
                                                            //}

                                                            if (oProduto.prCodigo.Contains("KIT"))
                                                            {
                                                                oProdutoComplemento = MAGALUClasseFuncoes.RetornaProdutoComplementoFortPlusPorSKU(oProduto.prCodigo);
                                                                oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComplemento.cmIdProduto.ToString()).Content);

                                                                oResposta = null;

                                                                while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                                                                {
                                                                    RestClient client = new RestClient(ClasseParametros.sURlFortPlus + "/api/ProdutoComposicao/Produto/" + oProduto.id.ToString());
                                                                    RestRequest request = new RestRequest(Method.GET);
                                                                    request.AddHeader("Cache-Control", "no-cache");
                                                                    request.AddHeader("Accept", "*/*");
                                                                    request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                                                                    request.AddHeader("Content-Type", "application/json");
                                                                    request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                                                                    oResposta = client.Execute(request);

                                                                    if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                                                    {
                                                                        ClasseFuncoes.ConectaForteplus(5);
                                                                    }
                                                                }

                                                                List<FortPlusProdutoComposicao> oListFormaPagamento = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FortPlusProdutoComposicao>>(oResposta.Content);
                                                                float? eValorTotalComposicao = 0;

                                                                foreach (FortPlusProdutoComposicao oProdutoComposicao in oListFormaPagamento)
                                                                {
                                                                    oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComposicao.pcIdProdutoComposicao.ToString()).Content);
                                                                    sProdutoTemp = oProduto.prCodigo;
                                                                    ProdutoComplemento oProdutoComplementoUsar = new ProdutoComplemento();
                                                                    int iLocalEstoque34 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "34");
                                                                    double eQtdEstoque = ClasseFuncoes.RetornaSeTemEstoque(oProduto.id.ToString(), iLocalEstoque34);

                                                                    if (oItem.quantity > eQtdEstoque)
                                                                    {
                                                                        iLocalEstoque = 33;
                                                                        break;
                                                                    }
                                                                    else
                                                                    {
                                                                        iLocalEstoque = 32;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {

                                                                int iLocalEstoque34 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "34");
                                                                double eQtdEstoque = ClasseFuncoes.RetornaSeTemEstoque(oProduto.id.ToString(), iLocalEstoque34);

                                                                if (oItem.quantity > eQtdEstoque)
                                                                {
                                                                    iLocalEstoque = 33;


                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    DataTable dtbProdutoOrla = ClasseParametros.ConsultaBancoMysql("SELECT * FROM PRODUTOSORLA WHERE SKU = '" + oProduto.prCodigo + "'");

                                                                    if (dtbProdutoOrla.Rows.Count > 0)
                                                                        iLocalEstoque = 32;
                                                                    else
                                                                        iLocalEstoque = 33;

                                                                    dtbProdutoOrla.Dispose();
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ClasseFuncoes.SalvaLogServicos(ex.Message);
                                                        ClasseFuncoes.EnviaMensagemTelegramAsync("Pedido com erro no item!\n" + sCodigoUsado + "\n" + oPedido.buyer.first_name + " " + oPedido.buyer.last_name + sProduto, "0,2", "Pedido com erro no item", sCodigoUsado.ToString());
                                                        continue;
                                                    }

                                                    Pedido oPedidoFortPlus = new Pedido();

                                                    Guid oGuid = Guid.NewGuid();
                                                    oPedidoFortPlus.id = 0;
                                                    oPedidoFortPlus.mvDocto = 0;
                                                    oPedidoFortPlus.mvIdPessoa = int.Parse(oCliente.id);
                                                    string sCNPJ = "";
                                                    oPedidoFortPlus.mvIdVendedor = ClasseFuncoes.RetornaVendedorFortPlus("MERCADO LIVRE");
                                                    oPedidoFortPlus.mvIdSerie = ClasseFuncoes.RetornaCodigoGlobal("SR", "1");
                                                    oPedidoFortPlus.mvIdModelo = ClasseFuncoes.RetornaCodigoGlobal("MD", "55");
                                                    oPedidoFortPlus.mvTipoMovimento = "1";
                                                    oPedidoFortPlus.mvTipoPedido = "P";
                                                    oPedidoFortPlus.mvIdTipoDocumento = ClasseFuncoes.RetornaCodigoGlobal("TD", "REC");

                                                    oPedidoFortPlus.mvIdTipoFrete = ClasseFuncoes.RetornaCodigoGlobal("TF", "9");
                                                    if (oEntrega.shipping_option.cost > 0.01)
                                                    {
                                                        oPedidoFortPlus.mvIdTipoFrete = ClasseFuncoes.RetornaCodigoGlobal("TF", "1");
                                                    }

                                                    oPedidoFortPlus.mvPreNota = "N";
                                                    oPedidoFortPlus.mvFinNf = "1";
                                                    oPedidoFortPlus.mvPresenca = ClasseFuncoes.RetornaCodigoParametro("_FPS_NFE_PRESENCA");
                                                    oPedidoFortPlus.mvIdNatureza = ClasseFuncoes.RetornaCodigoGlobal("NO", "01");
                                                    oPedidoFortPlus.mvIdParent = null;
                                                    oPedidoFortPlus.idFilial = ClasseParametros.iFilial;
                                                    oPedidoFortPlus.mvIdTransportadora = ClasseFuncoes.RetornaTransportadorFortPlus("MERCADO LIVRE");
                                                    int? eTotal = 0;
                                                    float? eTotalPago = 0;
                                                    float? eTotalValor = 0;
                                                    float? eTotalDesconto = 0;

                                                    foreach (Shipping_Items oItem in oEntrega.shipping_items)
                                                    {
                                                        ps = new List<Parameter>();
                                                        p = new Parameter();
                                                        p.Name = "access_token";
                                                        p.Value = ClasseParametros.oMeli.AccessToken;
                                                        ps.Add(p);

                                                        var oRespostaItem = ClasseParametros.oMeli.Get("/items/" + oItem.id.Trim(), ps);

                                                        MercadoLivreProduto oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreProduto>(oRespostaItem.Content);



                                                        var oRespostaEntrega = ClasseParametros.oMeli.Get("/shipments/" + oPedido.shipping.id.ToString(), ps);


                                                        oEntrega = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreEntrega>(oRespostaEntrega.Content);


                                                        eTotal += oItem.quantity;
                                                        eTotalValor += oItem.quantity * oProduto.price;
                                                        eTotalDesconto += oItem.quantity * oPedido.order_items[0].sale_fee;
                                                    }

                                                    s = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoFortPlus);
                                                    List<interRegraNegocio.MercadoLivre.Payment> lstPagamento = oPedido.payments.OfType<interRegraNegocio.MercadoLivre.Payment>().ToList();
                                                    foreach (interRegraNegocio.MercadoLivre.Payment oPagamento in lstPagamento)
                                                    {
                                                        if (oPagamento.status != "rejected")
                                                        {
                                                            eTotalPago += oPagamento.total_paid_amount;
                                                        }
                                                    }

                                                    oPedidoFortPlus.mvQuantidade = eTotal;
                                                    oPedidoFortPlus.mvPesoBruto = 0;
                                                    oPedidoFortPlus.mvPesoLiquido = 0;

                                                    oPedidoFortPlus.mvTpAmb = "1";
                                                    oPedidoFortPlus.mvTpEmis = "1";
                                                    oPedidoFortPlus.mvStatus = "0";
                                                    oPedidoFortPlus.mvEntidade = "PDV";
                                                    oPedidoFortPlus.ativo = "S";
                                                    oPedidoFortPlus.mvGuid = oGuid.ToString();
                                                    oPedidoFortPlus.dmaInclusao = DateTime.Now;
                                                    oPedidoFortPlus.dmaAlteracao = DateTime.Now;
                                                    oPedidoFortPlus.mvDmaEmissao = DateTime.Now;
                                                    oPedidoFortPlus.mvDmaEntradaSaida = DateTime.Now;

                                                    if (spacote != "")
                                                        oPedidoFortPlus.mvIdExterno = spacote;
                                                    else
                                                        oPedidoFortPlus.mvIdExterno = sCodigoUsado;


                                                    oPedidoFortPlus.mvValorFrete = oEntrega.shipping_option.cost;
                                                    oPedidoFortPlus.mvValorDesconto = 0;


                                                    oPedidoFortPlus.mvValorTotalProduto = eTotalValor;
                                                    //oPedidoFortPlus.valo = oPedido.total_amount;
                                                    oPedidoFortPlus.mvValorTotal = eTotalValor;
                                                    //mais de uma unidade
                                                    //04_02_2020
                                                    if (oPedido.buyer.billing_info.doc_type == "CPF")
                                                    {
                                                        oPedidoFortPlus.mvValorTotalProduto = eTotalValor - eTotalDesconto;
                                                        //oPedidoFortPlus.valo = oPedido.total_amount;
                                                        oPedidoFortPlus.mvValorTotal = eTotalValor - eTotalDesconto;

                                                        string smvValorTotalProduto = String.Format("{0:#,##0.00;(#,##0.00);Zero}", oPedidoFortPlus.mvValorTotalProduto);
                                                        string smvValorTotal = String.Format("{0:#,##0.00;(#,##0.00);Zero}", oPedidoFortPlus.mvValorTotal);


                                                        double eValorTemp = double.Parse(oPedidoFortPlus.mvValorTotalProduto.ToString());

                                                        oPedidoFortPlus.mvValorTotalProduto = float.Parse(smvValorTotalProduto);

                                                        eValorTemp = double.Parse(oPedidoFortPlus.mvValorTotal.ToString());

                                                        oPedidoFortPlus.mvValorTotal = float.Parse(smvValorTotal);

                                                    }

                                                    oPedidoFortPlus.mvVersao = ClasseFuncoes.RetornaCodigoParametro("_FPS_NFE_VERSAO");

                                                    //2322426297
                                                    oResposta = null;

                                                    while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                                                    {
                                                        RestClient client = null;
                                                        if (spacote != "")
                                                            client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/IdExterno/" + spacote.ToString().Trim());
                                                        else
                                                            client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/IdExterno/" + sCodigoUsado.ToString().Trim());
                                                        RestRequest request = new RestRequest(Method.GET);
                                                        request.AddHeader("Cache-Control", "no-cache");
                                                        request.AddHeader("Accept", "*/*");
                                                        request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                                                        request.AddHeader("Content-Type", "application/json");
                                                        if (ClasseParametros.oJsonFortePluslogin == null)
                                                            ClasseFuncoes.ConectaForteplus(5);
                                                        request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                                                        oResposta = client.Execute(request);

                                                        if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                                        {
                                                            ClasseFuncoes.ConectaForteplus(5);
                                                        }
                                                        else if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                                                        {
                                                            break;
                                                        }
                                                        else if (oResposta.StatusCode == System.Net.HttpStatusCode.OK)
                                                        {

                                                            oPedidoFortPlus = Newtonsoft.Json.JsonConvert.DeserializeObject<Pedido>(oResposta.Content);
                                                            break;
                                                        }

                                                    }

                                                    if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                                                        oPedidoFortPlus = ClasseFuncoes.CriaPedidoFortPlus(oPedidoFortPlus);


                                                    //int iLocalEstoque = 33;
                                                    //string sProduto = "";

                                                    //try
                                                    //{

                                                    //    foreach (Shipping_Items oItem in oEntrega.shipping_items)
                                                    //    {
                                                    //        ProdutoComplemento oProdutoComplemento = RetornaProdutoComplementoFortPlusPorIdExterno(oItem.id.Trim());
                                                    //        sProduto = "\nProduto: " + oItem.description + "\n";
                                                    //        sProduto += " MLB: " + oItem.id.Trim();
                                                    //        ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(RetornaProdutoFortPlus(oProdutoComplemento.cmIdProduto.ToString()).Content);

                                                    //        if (oProduto.prCodigo.ToString().Contains("MICFAC"))
                                                    //        {
                                                    //            iLocalEstoque = 31;
                                                    //            break;
                                                    //        }
                                                    //    }
                                                    //}
                                                    //catch (Exception ex)
                                                    //{
                                                    //    SalvaLogServicos(ex.Message);
                                                    //    EnviaMensagemTelegramAsync("Pedido com erro no item!\n" + sCodigoUsado + "\n" + oPedido.buyer.first_name + " " + oPedido.buyer.last_name + sProduto, "0,2", "Pedido com erro no item", sCodigoUsado.ToString());
                                                    //    return;
                                                    //}




                                                    List<ProdutoFortePlus> lstProduto = new List<ProdutoFortePlus>();
                                                    float? eValorFreteProduto = oPedidoFortPlus.mvValorFrete / oEntrega.shipping_items.Length;

                                                    foreach (Shipping_Items oItem in oEntrega.shipping_items)
                                                    {

                                                        ProdutoComplemento oProdutoComplemento = ClasseFuncoes.RetornaProdutoComplementoFortPlusPorIdExterno(oItem.id);
                                                        ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComplemento.cmIdProduto.ToString()).Content);
                                                        bool lTirarTarifa = oProduto.prPercentComissao == 1;

                                                        if (oProduto.prCodigo.Substring(0, 3).Trim() == "KIT")
                                                        {
                                                            oResposta = null;

                                                            while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                                                            {
                                                                RestClient client = new RestClient(ClasseParametros.sURlFortPlus + "/api/ProdutoComposicao/Produto/" + oProduto.id.ToString());
                                                                RestRequest request = new RestRequest(Method.GET);
                                                                request.AddHeader("Cache-Control", "no-cache");
                                                                request.AddHeader("Accept", "*/*");
                                                                request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                                                                request.AddHeader("Content-Type", "application/json");
                                                                if (ClasseParametros.oJsonFortePluslogin == null)
                                                                    ClasseFuncoes.ConectaForteplus(5);
                                                                request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                                                                oResposta = client.Execute(request);

                                                                if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                                                {
                                                                    ClasseFuncoes.ConectaForteplus(5);
                                                                }
                                                            }

                                                            List<FortPlusProdutoComposicao> oListFormaPagamento = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FortPlusProdutoComposicao>>(oResposta.Content);
                                                            float? eValorTotalComposicao = 0;

                                                            foreach (FortPlusProdutoComposicao oFormaPagamento in oListFormaPagamento)
                                                            {
                                                                try
                                                                {
                                                                    if (oFormaPagamento.pcIdProdutoComposicao != null)
                                                                    {
                                                                        ProdutoComplemento oProdutoComplementoUsar = new ProdutoComplemento();
                                                                        string sComposicao = ClasseFuncoes.RetornaProdutoComplementoFortPlus(oFormaPagamento.pcIdProdutoComposicao.ToString()).Content;
                                                                        List<ProdutoComplemento> oProdutoComplementoComposicao = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(sComposicao);
                                                                        foreach (ProdutoComplemento oPC in oProdutoComplementoComposicao)
                                                                        {
                                                                            string sMarketPlace = ClasseFuncoes.RetornaNomeGlobalMK("MK", oPC.cmIdMarketPlace);
                                                                            if (sMarketPlace.Contains("MERCADO LIVRE"))
                                                                            {
                                                                                oProdutoComplementoUsar = oPC;
                                                                                break;
                                                                            }
                                                                        }
                                                                        if (oProdutoComplementoUsar.cmPrecoDePor != null)
                                                                            eValorTotalComposicao += oProdutoComplementoUsar.cmPrecoDePor;
                                                                    }
                                                                }
                                                                catch
                                                                {

                                                                }
                                                            }

                                                            foreach (FortPlusProdutoComposicao oFormaPagamento in oListFormaPagamento)
                                                            {
                                                                PedidoItemFortPlus oItemPedido = new PedidoItemFortPlus();
                                                                oItemPedido.id = 0;
                                                                oItemPedido.mtIdNfOrigem = null;
                                                                oItemPedido.mtIdMovto = oPedidoFortPlus.id;
                                                                lstProduto.Add(oProduto);

                                                                float? eTotalSemTarifa = 0;
                                                                double? eQuantidade = 0;
                                                                float? eQtd = 0;

                                                                if (spacote != "")
                                                                {
                                                                    eTotalSemTarifa = (eTotalValor - eTotalDesconto) / oEntrega.shipping_items.Length;
                                                                    eQuantidade = oItem.quantity;
                                                                    eQtd = float.Parse(eQuantidade.ToString());

                                                                }
                                                                else
                                                                {
                                                                    eTotalSemTarifa = eTotalValor - eTotalDesconto;
                                                                    eQuantidade = oItem.quantity * oFormaPagamento.pcQtde;
                                                                    eQtd = float.Parse(eQuantidade.ToString());

                                                                }



                                                                if (oFormaPagamento.pcIdProdutoComposicao == null)
                                                                    continue;


                                                                List<ProdutoComplemento> oProdutoComplementoComposicao = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(ClasseFuncoes.RetornaProdutoComplementoFortPlus(oFormaPagamento.pcIdProdutoComposicao.ToString()).Content);
                                                                ProdutoComplemento oProdutoComplementoUsar = new ProdutoComplemento();


                                                                foreach (ProdutoComplemento oPC in oProdutoComplementoComposicao)
                                                                {
                                                                    string sMarketPlace = ClasseFuncoes.RetornaNomeGlobalMK("MK", oPC.cmIdMarketPlace);
                                                                    if (sMarketPlace.Contains("MERCADO LIVRE"))
                                                                    {
                                                                        oProdutoComplementoUsar = oPC;
                                                                        break;
                                                                    }
                                                                }
                                                                float? ePercentual = 0;
                                                                double? eValorProduto = 0;

                                                                if (spacote != "")
                                                                {
                                                                    ePercentual = ((oProdutoComplementoUsar.cmPrecoDePor * 100) / eValorTotalComposicao) / oEntrega.shipping_items.Length;
                                                                    eValorProduto = eTotalSemTarifa;

                                                                }
                                                                else
                                                                {
                                                                    ePercentual = (oProdutoComplementoUsar.cmPrecoDePor * 100) / eValorTotalComposicao;
                                                                    eValorProduto = (eTotalValor * ePercentual) / 100;

                                                                }


                                                                oItemPedido.mtIdProduto = oFormaPagamento.pcIdProdutoComposicao;
                                                                oItemPedido.mtQtde = eQtd;
                                                                oItemPedido.mtValorUnitario = float.Parse(eValorProduto.ToString());
                                                                oItemPedido.mtValor = oItemPedido.mtValorUnitario * eQtd;
                                                                if (ePercentual == 100)
                                                                {
                                                                    oItemPedido.mtValorUnitario = float.Parse(eValorTotalComposicao.ToString());
                                                                    oItemPedido.mtValor = oItemPedido.mtValorUnitario * eQtd;


                                                                    if (spacote != "")
                                                                    {
                                                                        eTotalSemTarifa = (eTotalValor - eTotalDesconto) / oEntrega.shipping_items.Length;
                                                                        eQuantidade = oItem.quantity;
                                                                        eQtd = float.Parse(eQuantidade.ToString());

                                                                    }
                                                                    else
                                                                    {
                                                                        eTotalSemTarifa = eTotalValor - eTotalDesconto;
                                                                        eQuantidade = oItem.quantity * oFormaPagamento.pcQtde;
                                                                        eQtd = float.Parse(eQuantidade.ToString());

                                                                    }

                                                                }

                                                                oItemPedido.mtValorTotal = oItemPedido.mtValor + eValorFreteProduto;

                                                                ////mais de uma unidade
                                                                ////04_02_2020
                                                                if (oPedido.buyer.billing_info.doc_type == "CPF" && spacote == "")
                                                                {
                                                                    ePercentual = (oProdutoComplementoUsar.cmPrecoDePor * 100) / eValorTotalComposicao;
                                                                    eValorProduto = (eTotalSemTarifa * ePercentual) / 100;

                                                                    if (oListFormaPagamento.Count == 1)
                                                                    {
                                                                        eValorProduto = eTotalSemTarifa / eQtd;
                                                                    }

                                                                    oItemPedido.mtValorUnitario = float.Parse(eValorProduto.ToString());
                                                                    oItemPedido.mtValor = oItemPedido.mtValorUnitario * eQtd;
                                                                    oItemPedido.mtValorTotal = oItemPedido.mtValor + eValorFreteProduto;
                                                                }

                                                                if (oItemPedido.mtQtde > 1)
                                                                {
                                                                    string sMensagem = "Foi vendido itens comuns(KIT'S), com mais de uma unidade\n" + "Pedido: " + sCodigoUsado + "\nNome: " + oPedido.buyer.first_name + " " + oPedido.buyer.last_name;
                                                                    ClasseFuncoes.EnviaMensagemTelegramAsync(sMensagem, "0,1", "Item com mais de um", sCodigoUsado.ToString());
                                                                }

                                                                oItemPedido.mtValorDesconto = 0;
                                                                oItemPedido.mtValorDescontoRateio = 0;
                                                                oItemPedido.mtPercDesconto = 0;
                                                                oItemPedido.mtValorFrete = eValorFreteProduto;
                                                                oItemPedido.mtValorSeguro = 0;
                                                                oItemPedido.mtValorOutrasDespesas = 0;
                                                                oItemPedido.mtCustoMedio = null;
                                                                oItemPedido.mtValorTabela = oProdutoComplementoUsar.cmPrecoVenda;
                                                                oItemPedido.mtPesoBruto = null;
                                                                oItemPedido.mtPesoLiquido = null;
                                                                oItemPedido.mtIdCfop = null;
                                                                oItemPedido.mtIdNcm = oProduto.prIdNcm;

                                                                oItemPedido.mtIdLocalEstoque = ClasseFuncoes.RetornaCodigoLocalEstoque("LE", iLocalEstoque.ToString());

                                                                oItemPedido.mtValorAproxImposto = null;
                                                                oItemPedido.mtValorTributoEstadual = null;
                                                                oItemPedido.mtValorTributoImportado = null;
                                                                oItemPedido.mtValorTributoMunicipal = null;
                                                                oItemPedido.mtValorTributoNacional = null;
                                                                oItemPedido.mtPercEstadual = null;
                                                                oItemPedido.mtPercImportado = null;
                                                                oItemPedido.mtPercMunicipal = null;
                                                                oItemPedido.mtPercNacional = null;
                                                                oItemPedido.mtVersaoIbpt = "";
                                                                oItemPedido.mtIdUnidade = oProduto.prIdUnidadePrincipal;
                                                                oItemPedido.mtOrdemCompra = "";
                                                                oItemPedido.mtOrdemItemCompra = "";
                                                                oItemPedido.mtReferencia = "";
                                                                oItemPedido.mtEntidade = "";
                                                                oItemPedido.mtModalidadeBcIcms = "";
                                                                oItemPedido.mtIdCstIcms = null;
                                                                oItemPedido.mtBaseIcms = null;
                                                                oItemPedido.mtAliquotaIcms = null;
                                                                oItemPedido.mtPercentReducaoBaseIcms = null;
                                                                oItemPedido.mtValorIcms = null;
                                                                oItemPedido.mtValorReducaoIcms = null;
                                                                oItemPedido.mtAliquotaIcmsCr = null;
                                                                oItemPedido.mtValorIcmsCr = null;
                                                                oItemPedido.mtBaseIcmsCr = null;
                                                                oItemPedido.mtModalidadeBcIcmsSt = "";
                                                                oItemPedido.mtPercentReducaoBaseIcmsSt = null;
                                                                oItemPedido.mtAliquotaIcmsSt = null;
                                                                oItemPedido.mtAliquotaMva = null;
                                                                oItemPedido.mtValorIcmsSt = null;
                                                                oItemPedido.mtValorReducaoIcmsSt = null;
                                                                oItemPedido.mtBaseIcmsSt = null;
                                                                oItemPedido.mtIdCstPis = null;
                                                                oItemPedido.mtBasePis = null;
                                                                oItemPedido.mtPercentReducaoBasePis = null;
                                                                oItemPedido.mtAliquotaPis = null;
                                                                oItemPedido.mtValorPis = null;
                                                                oItemPedido.mtIdCstCofins = null;
                                                                oItemPedido.mtBaseCofins = null;
                                                                oItemPedido.mtPercentReducaoBaseCofins = null;
                                                                oItemPedido.mtAliquotaCofins = null;
                                                                oItemPedido.mtValorCofins = null;
                                                                oItemPedido.mtIdCstIpi = null;
                                                                oItemPedido.mtBaseIpi = null;
                                                                oItemPedido.mtPercentReducaoBaseIpi = null;
                                                                oItemPedido.mtAliquotaIpi = null;
                                                                oItemPedido.mtValorIpi = null;
                                                                oItemPedido.mtIdCstIi = null;
                                                                oItemPedido.mtBaseIi = null;
                                                                oItemPedido.mtPercentReducaoBaseIi = null;
                                                                oItemPedido.mtValorIi = null;
                                                                oItemPedido.mtIdCstIssqn = null;
                                                                oItemPedido.mtBaseIssqn = null;
                                                                oItemPedido.mtPercentReducaoBaseIssqn = null;
                                                                oItemPedido.mtValorIssqn = null;
                                                                //oItemPedido.mtAliquotaInterEstadual = null;
                                                                oItemPedido.mtBaseDifal = null;
                                                                oItemPedido.mtAliquotaDifal = null;
                                                                oItemPedido.mtValorDifal = null;
                                                                oItemPedido.mtBaseFecp = null;
                                                                oItemPedido.mtAliquotaFecp = null;
                                                                oItemPedido.mtValorFecp = null;
                                                                oItemPedido.mtBaseFecpSt = null;
                                                                oItemPedido.mtAliquotaFecpSt = null;
                                                                oItemPedido.mtValorFecpSt = null;
                                                                oItemPedido.mtBaseFecpStRet = null;
                                                                oItemPedido.mtAliquotaFecpStRet = null;
                                                                oItemPedido.mtValorFecpStRet = null;
                                                                oItemPedido.mtValorIcmsPartUfOrigem = null;
                                                                oItemPedido.mtValorIcmsPartUfDestino = null;
                                                                oItemPedido.mtAliquotaIcmsPartUfOrigem = null;
                                                                oItemPedido.mtAliquotaIcmsPartUfDestino = null;
                                                                oItemPedido.mtBaseIcmsPartUfOrigem = null;
                                                                oItemPedido.mtBaseIcmsPartUfDestino = null;
                                                                oItemPedido.mtBaseCalculoKardex = null;
                                                                oItemPedido.mtPercentComissao = null;
                                                                oItemPedido.mtGuid = oPedidoFortPlus.mvGuid;
                                                                oItemPedido.mtObservacao = "";
                                                                oItemPedido.mtDmaItem = DateTime.Now;
                                                                oItemPedido.idFilial = ClasseParametros.iFilial;
                                                                oItemPedido.idIncluidoPor = null;
                                                                oItemPedido.idAltaradoPor = null;
                                                                oItemPedido.dmaInclusao = DateTime.Now;
                                                                oItemPedido.dmaAlteracao = DateTime.Now;
                                                                oItemPedido.ativo = "S";
                                                                oItemPedido.mtIdCest = oProduto.prIdCest;
                                                                oItemPedido.mtIdParent = null;
                                                                oItemPedido.mtQtdeLiberada = null;
                                                                oItemPedido.mtQtdeSaldo = null;
                                                                oItemPedido.mtVariacao = "";
                                                                oItemPedido.mtPrecoDePor = null;
                                                                oItemPedido.mtAjCusto = "";
                                                                oItemPedido.mtCEnqIpi = "";
                                                                oItemPedido.mtAgregaTotalFecp = "";
                                                                oItemPedido.mtIdLote = null;
                                                                oItemPedido.mtLote = "";
                                                                oItemPedido.mtDmaLote = DateTime.Now;
                                                                s = Newtonsoft.Json.JsonConvert.SerializeObject(oItemPedido);
                                                                ClasseFuncoes.InseriItensPedidoFortPlus(oItemPedido);

                                                            }
                                                        }
                                                        else
                                                        {

                                                            PedidoItemFortPlus oItemPedido = new PedidoItemFortPlus();
                                                            oItemPedido.id = 0;
                                                            oItemPedido.mtIdNfOrigem = null;
                                                            oItemPedido.mtIdMovto = oPedidoFortPlus.id;
                                                            oItemPedido.mtValorFrete = eValorFreteProduto;

                                                            lstProduto.Add(oProduto);

                                                            oItemPedido.mtIdProduto = oProdutoComplemento.cmIdProduto;
                                                            oItemPedido.mtQtde = oItem.quantity;
                                                            oItemPedido.mtValorUnitario = oProdutoComplemento.cmPrecoDePor;
                                                            oItemPedido.mtValor = (oProdutoComplemento.cmPrecoDePor * oItem.quantity);
                                                            oItemPedido.mtValorTotal = oItemPedido.mtValor + eValorFreteProduto;

                                                            //mais de uma unidade
                                                            //04_02_2020
                                                            float? eTotalSemTarifa = eTotalValor - eTotalDesconto;

                                                            if (oPedido.buyer.billing_info.doc_type == "CPF")
                                                            {
                                                                float? eValorProduto = eTotalSemTarifa / oItem.quantity;
                                                                oItemPedido.mtIdProduto = oProdutoComplemento.cmIdProduto;
                                                                oItemPedido.mtQtde = oItem.quantity;
                                                                oItemPedido.mtValorUnitario = eValorProduto;
                                                                oItemPedido.mtValor = (eValorProduto * oItem.quantity);
                                                                oItemPedido.mtValorTotal = oItemPedido.mtValor + eValorFreteProduto;
                                                            }

                                                            if (oItemPedido.mtQtde > 1)
                                                            {
                                                                string sMensagem = "Foi vendido itens comuns(que não são KIT'S), com mais de uma unidade\n" + "Pedido: " + sCodigoUsado + "\nNome: " + oPedido.buyer.first_name + " " + oPedido.buyer.last_name;
                                                                ClasseFuncoes.EnviaMensagemTelegramAsync(sMensagem, "0,1", "Item com mais de um", sCodigoUsado.ToString());
                                                            }

                                                            oItemPedido.mtValorDesconto = 0;
                                                            oItemPedido.mtValorDescontoRateio = 0;
                                                            oItemPedido.mtPercDesconto = 0;
                                                            oItemPedido.mtValorFrete = eValorFreteProduto;
                                                            oItemPedido.mtValorSeguro = 0;
                                                            oItemPedido.mtValorOutrasDespesas = 0;
                                                            oItemPedido.mtCustoMedio = null;
                                                            oItemPedido.mtValorTabela = oProdutoComplemento.cmPrecoVenda;
                                                            oItemPedido.mtPesoBruto = null;
                                                            oItemPedido.mtPesoLiquido = null;
                                                            oItemPedido.mtIdCfop = null;
                                                            oItemPedido.mtIdNcm = oProduto.prIdNcm;

                                                            oItemPedido.mtIdLocalEstoque = ClasseFuncoes.RetornaCodigoLocalEstoque("LE", iLocalEstoque.ToString());

                                                            oItemPedido.mtValorAproxImposto = null;
                                                            oItemPedido.mtValorTributoEstadual = null;
                                                            oItemPedido.mtValorTributoImportado = null;
                                                            oItemPedido.mtValorTributoMunicipal = null;
                                                            oItemPedido.mtValorTributoNacional = null;
                                                            oItemPedido.mtPercEstadual = null;
                                                            oItemPedido.mtPercImportado = null;
                                                            oItemPedido.mtPercMunicipal = null;
                                                            oItemPedido.mtPercNacional = null;
                                                            oItemPedido.mtVersaoIbpt = "";
                                                            oItemPedido.mtIdUnidade = oProduto.prIdUnidadePrincipal;
                                                            oItemPedido.mtOrdemCompra = "";
                                                            oItemPedido.mtOrdemItemCompra = "";
                                                            oItemPedido.mtReferencia = "";
                                                            oItemPedido.mtEntidade = "";
                                                            oItemPedido.mtModalidadeBcIcms = "";
                                                            oItemPedido.mtIdCstIcms = null;
                                                            oItemPedido.mtBaseIcms = null;
                                                            oItemPedido.mtAliquotaIcms = null;
                                                            oItemPedido.mtPercentReducaoBaseIcms = null;
                                                            oItemPedido.mtValorIcms = null;
                                                            oItemPedido.mtValorReducaoIcms = null;
                                                            oItemPedido.mtAliquotaIcmsCr = null;
                                                            oItemPedido.mtValorIcmsCr = null;
                                                            oItemPedido.mtBaseIcmsCr = null;
                                                            oItemPedido.mtModalidadeBcIcmsSt = "";
                                                            oItemPedido.mtPercentReducaoBaseIcmsSt = null;
                                                            oItemPedido.mtAliquotaIcmsSt = null;
                                                            oItemPedido.mtAliquotaMva = null;
                                                            oItemPedido.mtValorIcmsSt = null;
                                                            oItemPedido.mtValorReducaoIcmsSt = null;
                                                            oItemPedido.mtBaseIcmsSt = null;
                                                            oItemPedido.mtIdCstPis = null;
                                                            oItemPedido.mtBasePis = null;
                                                            oItemPedido.mtPercentReducaoBasePis = null;
                                                            oItemPedido.mtAliquotaPis = null;
                                                            oItemPedido.mtValorPis = null;
                                                            oItemPedido.mtIdCstCofins = null;
                                                            oItemPedido.mtBaseCofins = null;
                                                            oItemPedido.mtPercentReducaoBaseCofins = null;
                                                            oItemPedido.mtAliquotaCofins = null;
                                                            oItemPedido.mtValorCofins = null;
                                                            oItemPedido.mtIdCstIpi = null;
                                                            oItemPedido.mtBaseIpi = null;
                                                            oItemPedido.mtPercentReducaoBaseIpi = null;
                                                            oItemPedido.mtAliquotaIpi = null;
                                                            oItemPedido.mtValorIpi = null;
                                                            oItemPedido.mtIdCstIi = null;
                                                            oItemPedido.mtBaseIi = null;
                                                            oItemPedido.mtPercentReducaoBaseIi = null;
                                                            oItemPedido.mtValorIi = null;
                                                            oItemPedido.mtIdCstIssqn = null;
                                                            oItemPedido.mtBaseIssqn = null;
                                                            oItemPedido.mtPercentReducaoBaseIssqn = null;
                                                            oItemPedido.mtValorIssqn = null;
                                                            //oItemPedido.mtAliquotaInterEstadual = null;
                                                            oItemPedido.mtBaseDifal = null;
                                                            oItemPedido.mtAliquotaDifal = null;
                                                            oItemPedido.mtValorDifal = null;
                                                            oItemPedido.mtBaseFecp = null;
                                                            oItemPedido.mtAliquotaFecp = null;
                                                            oItemPedido.mtValorFecp = null;
                                                            oItemPedido.mtBaseFecpSt = null;
                                                            oItemPedido.mtAliquotaFecpSt = null;
                                                            oItemPedido.mtValorFecpSt = null;
                                                            oItemPedido.mtBaseFecpStRet = null;
                                                            oItemPedido.mtAliquotaFecpStRet = null;
                                                            oItemPedido.mtValorFecpStRet = null;
                                                            oItemPedido.mtValorIcmsPartUfOrigem = null;
                                                            oItemPedido.mtValorIcmsPartUfDestino = null;
                                                            oItemPedido.mtAliquotaIcmsPartUfOrigem = null;
                                                            oItemPedido.mtAliquotaIcmsPartUfDestino = null;
                                                            oItemPedido.mtBaseIcmsPartUfOrigem = null;
                                                            oItemPedido.mtBaseIcmsPartUfDestino = null;
                                                            oItemPedido.mtBaseCalculoKardex = null;
                                                            oItemPedido.mtPercentComissao = null;
                                                            oItemPedido.mtGuid = oPedidoFortPlus.mvGuid;
                                                            oItemPedido.mtObservacao = "";
                                                            oItemPedido.mtDmaItem = DateTime.Now;
                                                            oItemPedido.idFilial = ClasseParametros.iFilial;
                                                            oItemPedido.idIncluidoPor = null;
                                                            oItemPedido.idAltaradoPor = null;
                                                            oItemPedido.dmaInclusao = DateTime.Now;
                                                            oItemPedido.dmaAlteracao = DateTime.Now;
                                                            oItemPedido.ativo = "S";
                                                            oItemPedido.mtIdCest = oProduto.prIdCest;
                                                            oItemPedido.mtIdParent = null;
                                                            oItemPedido.mtQtdeLiberada = null;
                                                            oItemPedido.mtQtdeSaldo = null;
                                                            oItemPedido.mtVariacao = "";
                                                            oItemPedido.mtPrecoDePor = null;
                                                            oItemPedido.mtAjCusto = "";
                                                            oItemPedido.mtCEnqIpi = "";
                                                            oItemPedido.mtAgregaTotalFecp = "";
                                                            oItemPedido.mtIdLote = null;
                                                            oItemPedido.mtLote = "";
                                                            oItemPedido.mtDmaLote = DateTime.Now;
                                                            s = Newtonsoft.Json.JsonConvert.SerializeObject(oItemPedido);
                                                            ClasseFuncoes.InseriItensPedidoFortPlus(oItemPedido);
                                                        }
                                                    }


                                                    ClasseFuncoes.AtualizaDadosFortPlus(oPedidoFortPlus.id);

                                                    oResposta = null;
                                                    while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                                                    {
                                                        RestClient client = null;
                                                        if (spacote != "")
                                                            client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/IdExterno/" + spacote.ToString().Trim());
                                                        else
                                                            client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/IdExterno/" + sCodigoUsado.ToString().Trim());
                                                        RestRequest request = new RestRequest(Method.GET);
                                                        request.AddHeader("Cache-Control", "no-cache");
                                                        request.AddHeader("Accept", "*/*");
                                                        request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                                                        request.AddHeader("Content-Type", "application/json");
                                                        if (ClasseParametros.oJsonFortePluslogin == null)
                                                            ClasseFuncoes.ConectaForteplus(5);
                                                        request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                                                        oResposta = client.Execute(request);

                                                        if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                                        {
                                                            ClasseFuncoes.ConectaForteplus(5);
                                                        }
                                                        else if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                                                        {
                                                            break;
                                                        }
                                                    }


                                                    oPedidoFortPlus = Newtonsoft.Json.JsonConvert.DeserializeObject<Pedido>(oResposta.Content);

                                                    foreach (interRegraNegocio.MercadoLivre.Payment oPagamento in lstPagamento)
                                                    {
                                                        if (oPagamento.status != "rejected")
                                                        {
                                                            FortPlusFinanceiroReduzido oFinanceiro = new FortPlusFinanceiroReduzido();
                                                            oFinanceiro.email = "rodrigonunes@2eletro.com.br";
                                                            oFinanceiro.idFilial = oPedidoFortPlus.idFilial;
                                                            oFinanceiro.idMovto = oPedidoFortPlus.id;
                                                            oFinanceiro.idFormaPagamento = ClasseFuncoes.RetornaCodigoFormaPagamento(oPagamento.payment_type);
                                                            oFinanceiro.idCondicaoPagamento = ClasseFuncoes.RetornaCodigoCondicaoPagamento("À VISTA");
                                                            oFinanceiro.valor = Math.Round(double.Parse((eTotalPago + oPagamento.shipping_cost).ToString()), 3);

                                                            if (oPedido.buyer.billing_info.doc_type == "CPF")
                                                            {
                                                                oFinanceiro.valor = oPedidoFortPlus.mvValorTotal + oPedidoFortPlus.mvValorFrete;
                                                            }

                                                            oFinanceiro = ClasseFuncoes.CadastraFinanceiroReduzido(oFinanceiro);
                                                        }
                                                    }

                                                    if (spacote != "")
                                                        sSql = "UPDATE VENDAMARKETPLACE SET STATUS = 1, QUANTIDADEACIMA = 0 WHERE TRIM(PACKID) ='" + spacote.ToString().Trim() + "'";

                                                    else
                                                        sSql = "UPDATE VENDAMARKETPLACE SET STATUS = 1, QUANTIDADEACIMA = 0 WHERE TRIM(ID) ='" + sCodigoUsado.ToString().Trim() + "'";
                                                    ClasseParametros.ExecutabancoMySql(sSql);


                                                    foreach (ProdutoFortePlus oProduto in lstProduto)
                                                    {
                                                        ClasseFuncoes.EnviaProdutosMercadoLivreAsync(oProduto.prCodigo.ToString());
                                                    }


                                                    if (eTotal > 1)
                                                    {
                                                        if (spacote != "")
                                                            sSql = "UPDATE VENDAMARKETPLACE SET STATUSMENSAGEM = 3, QUANTIDADEACIMA = 1 WHERE TRIM(PACKID) ='" + spacote.ToString().Trim() + "'";

                                                        else
                                                            sSql = "UPDATE VENDAMARKETPLACE SET STATUSMENSAGEM = 3, QUANTIDADEACIMA = 1 WHERE TRIM(ID) ='" + sCodigoUsado.ToString().Trim() + "'";
                                                        ClasseParametros.ExecutabancoMySql(sSql);
                                                    }

                                                    string smensagem = string.Format(ClasseParametros.sMensagemAcabouComprar, oCliente.psNome);

                                                    string sPack = sCodigoUsado.ToString();
                                                    if (oPedido.pack_id != null)
                                                        sPack = oPedido.pack_id.ToString();

                                                    //EnviaMensagemMercadoLivre(sPack, oPedido.seller.id.ToString(), oPedido.buyer.id.ToString(), smensagem, "1", sCodigoUsado.ToString(), null, "STATUSMENSAGEMRETIRADA", "");

                                                    d.Dispose();
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            string sMensagem = "Pedido não integrado, erro genérico.\n" + "Pedido: " + sCodigoUsado + "\nNome: " + oPedido.buyer.first_name + " " + oPedido.buyer.last_name;
                                            ClasseFuncoes.EnviaMensagemTelegramAsync(sMensagem, "0,1", "Pedido não cadastrado", sCodigoUsado.ToString());
                                        }
                                    }
                                }
                                else if (oPedido.shipping.substatus != null && oPedido.shipping.substatus.ToString() == "waiting_for_withdrawal")
                                {
                                    string sSql = "SELECT * FROM VENDAMARKETPLACE WHERE ID = '" + sCodigoUsado + "' ";
                                    DataTable dtbVenda = ClasseParametros.ConsultaBancoMysql(sSql);
                                    if (dtbVenda.Rows.Count > 0)
                                        if (dtbVenda.Rows[0]["STATUSMENSAGEMRETIRADA"].ToString() == "")
                                        {
                                            string sMensagem = "Bom dia " + oPedido.buyer.first_name + " " + oPedido.buyer.last_name + ",\no produto esta aguardando a retirada nos correios perto de sua residência. Equipe 2ELETRO agradece";
                                            //EnviaMensagemMercadoLivre(sCodigoUsado.ToString(), ClasseParametros.oMeli.UserId, oPedido.buyer.id.ToString(), sMensagem, "1", sCodigoUsado.ToString(), null, "STATUSMENSAGEMRETIRADA", "");


                                        }
                                    //STATUSMENSAGEMRETIRADA
                                }
                            }
                            catch (Exception ex)
                            {


                            }
                        }
                        iOffset += 50;
                        oPedidoMercadoLivre = null;


                    }
                    catch (Exception ex)
                    {

                        if (oPedidoMercadoLivre == null || oPedidoMercadoLivre.results == null)
                            ClasseFuncoes.ConectaMercadoLivreAsync(5);
                    }

                    MessageBox.Show("Importado");
                    return;

                    Thread.Sleep(2000);
                }
            }
            catch
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
        }

        private void btnAmazon_Click(object sender, EventArgs e)
        {
            DataTable dtbJadlog = ClasseParametros.ConsultaBancoMysql("SELECT * FROM ETIQUETAJADLOG WHERE PEDIDOEXTERNO = '" + editPedidoAmazon.Text + "'");
            if (dtbJadlog.Rows.Count > 0)
            {
                editRastreio.Text = dtbJadlog.Rows[0]["CODIGOJADLOG"].ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int iCodigoCliente = 5;

                RestRequest oRequest = null;
                IRestResponse oResposta = null;
                RestClient client = null;
                RestRequest request = new RestRequest();
                DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM CLIENTE WHERE CODIGO = 5");


                string sKey = d.Rows[0]["KEYB2W"].ToString();
                string sUsuario = d.Rows[0]["USUARIOB2W"].ToString();
                string sAccount = "0MDxaksT8d";
                d.Dispose();

                int iPagina = 1;
                int iPorPagina = 50;

                while (true)
                {
                    oResposta = null;
                    while (oResposta == null)
                    {
                        client = new RestClient("http://api.skyhub.com.br/orders/" + editPedidoB2W.Text);

                        //client = new RestClient("http://api.skyhub.com.br/queues/orders");
                        //client = new RestClient("http://api.skyhub.com.br/orders?page=" + iPagina.ToString() + "&per_page=" + iPorPagina.ToString());
                        //client = new RestClient("http://api.skyhub.com.br/orders?page=0&per_page=50");
                        oRequest = new RestRequest(Method.GET);
                        oRequest.AddHeader("cache-control", "no-cache");
                        oRequest.AddHeader("Accept", "application/json");
                        oRequest.AddHeader("Content-Type", "application/json");
                        oRequest.AddHeader("x-Api-Key", sKey);
                        oRequest.AddHeader("X-User-Email", sUsuario);
                        oRequest.AddHeader("X-Accountmanager-Key", sAccount);
                        oResposta = client.Execute(oRequest);

                        if (oResposta.StatusCode == HttpStatusCode.BadGateway || oResposta.StatusCode == HttpStatusCode.GatewayTimeout || oResposta.StatusCode == HttpStatusCode.InternalServerError || oResposta.StatusCode == 0)
                        {
                            oResposta = null;
                        }
                    }
                    Thread.Sleep(2000);

                    #region etiqueta b2w
                    if (oResposta.Content == "Account  not found")
                    {
                        continue;
                    }
                    if (oResposta.Content.Contains("504 Gateway Time-ou"))
                    {
                        continue;
                    }

                    B2WPedido oPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<B2WPedido>(oResposta.Content);
                    if (oPedido == null)
                    {
                        break;
                    }



                    bool lAtualiza = false;

                    if (oPedido.code.Contains("976142107401"))
                    {

                    }

                    if (oPedido.status.type.ToUpper().Trim() == "APPROVED")
                    {
                        string sSql = "SELECT TRIM(ID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(ID) = '" + oPedido.code.ToString().Trim() + "' AND MARKETPLACE LIKE '%B2W%'";
                        d = ClasseParametros.ConsultaBancoMysql(sSql);
                        if (d.Rows.Count == 0)
                        {
                            string sPack = "";
                            if (oPedido.code != null)
                                sPack = oPedido.code.ToString();

                            sSql = "INSERT INTO VENDAMARKETPLACE(ID,STATUS,MARKETPLACE, DATA,STATUSMENSAGEM,PACKID,USERID,EMAILML,PEDIDOML,SELLERID) " +
                                "VALUES('" + oPedido.code.ToString().Trim() + "',0,'B2W - " + oPedido.code.Split('-')[0] + "' ,CURDATE(),0,'" + sPack + "', " +
                                "'" + iCodigoCliente.ToString() + "','" + oPedido.customer.email + "','" + oPedido.code + "','2ELETRO')";
                            ClasseParametros.ExecutabancoMySql(sSql);
                        }
                    }
                    else
                    {
                        // Atualiza pedido para MAGALU
                        //Thread.Sleep(1000);
                        //#region Marca pedido como processado no queue

                        //string sId = "[{\"Id\": \"" + oPedido.IdQueue.ToString() + "\"}]";

                        //client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/OrderQueue");
                        //request = new RestRequest(Method.PUT);
                        //request.AddHeader("cache-control", "no-cache");
                        //request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                        //request.AddParameter("application/json", sId, ParameterType.RequestBody);

                        //oResposta = client.Execute(request);

                        //#endregion
                    }



                    if (oPedido.code.Contains("976142107401"))
                    {

                    }

                    try
                    {
                        //if (oPedido.shipping_method == "Correios PAC" || oPedido.shipping_method == "Planilha")
                        //{
                        //    Pedido oPedidoFortePlus = RetornaPedidoFortePlusPorIdExterno(oPedido.code);
                        //    if (oPedidoFortePlus != null)
                        //    {
                        //        GeraEtiquetaJADLOG(oPedidoFortePlus, iCodigoCliente, lstXML);
                        //    }

                        //    continue;
                        //}

                        //if (oPedido.invoices[0].key == "32200634036601000338550010000149041001217216")
                        //{
                        //    //Pedido oPedidoFortePlus = RetornaPedidoFortePlusPorIdExterno(oPedido.code);

                        //    //GeraEtiquetaJADLOG(oPedidoFortePlus);
                        //}

                        if (oPedido.code.Contains("275530927201"))
                        {

                        }


                        int iOffset = 0;
                        int ilimit = 50;

                        //List<Result> lstPedidosMercadoLivre = new List<Result>();
                        bool lContinua = true;

                        Console.WriteLine(" Integrando pedido " + oPedido.code);
                        if (oPedido.code.ToString().Trim() == "2334214096")
                        {

                        }

                        string sSql = "SELECT TRIM(ID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(ID) = '" + oPedido.code.ToString().Trim() + "' AND STATUS = 0 AND MARKETPLACE LIKE '%B2W%'";
                        d = ClasseParametros.ConsultaBancoMysql(sSql);
                        if (d.Rows.Count > 0)
                        {
                            if (oPedido.status.type.ToUpper().Trim() == "APPROVED")
                            {
                                ClienteFortPlus oCliente = ClasseFuncoes.CadastraClienteB2WFortPlus(oPedido);
                                string s = Newtonsoft.Json.JsonConvert.SerializeObject(oCliente);

                                if (oCliente == null)
                                {
                                    continue;
                                }


                                int iLocalEstoque = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "33");
                                ClasseFuncoes.CarregaFiliais("34.036.601/0003-38 - 2ELETRO VAREJISTA");

                                //34036601000257 - 2ELETRO ATACADISTA	
                                //34036601000338 - 2ELETRO VAREJISTA

                                string sProduto = "";
                                //try
                                //{

                                //    foreach (Item1 oItem in oPedido.items)
                                //    {
                                //        string sLast = oItem.IdSku.Substring(oItem.IdSku.Length - 5);
                                //        string sInicio = oItem.IdSku.Replace(sLast, "");
                                //        string sProdutoTemp = "";

                                //        if (sLast.Substring(sLast.Length - 3, 3) == "VAR")
                                //        {
                                //            sLast = sLast.Replace("VAR", "");

                                //            string[] aProduto = sLast.Split('G');
                                //            sProdutoTemp = sInicio + aProduto[0];
                                //        }
                                //        else
                                //        {
                                //            sProdutoTemp = oItem.IdSku;
                                //        }

                                //        if (sProdutoTemp.Contains("KIT"))
                                //        {

                                //            ProdutoComplemento oProdutoComplemento = MAGALUClasseFuncoes.RetornaProdutoComplementoFortPlusPorSKU(sProdutoTemp);
                                //            ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComplemento.cmIdProduto.ToString()).Content);

                                //            oResposta = null;

                                //            while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                                //            {
                                //                client = new RestClient(ClasseParametros.sURlFortPlus + "/api/ProdutoComposicao/Produto/" + oProduto.id.ToString());
                                //                request = new RestRequest(Method.GET);
                                //                request.AddHeader("Cache-Control", "no-cache");
                                //                request.AddHeader("Accept", "*/*");
                                //                request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                                //                request.AddHeader("Content-Type", "application/json");
                                //                request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                                //                oResposta = client.Execute(request);

                                //                if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                //                {
                                //                    ClasseFuncoes.ConectaForteplus(5);
                                //                }
                                //            }

                                //            List<FortPlusProdutoComposicao> oListFormaPagamento = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FortPlusProdutoComposicao>>(oResposta.Content);
                                //            float? eValorTotalComposicao = 0;

                                //            foreach (FortPlusProdutoComposicao oProdutoComposicao in oListFormaPagamento)
                                //            {
                                //                ProdutoComplemento oProdutoComplementoUsar = new ProdutoComplemento();
                                //                int iLocalEstoque34 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "34");
                                //                double eQtdEstoque = ClasseFuncoes.RetornaSeTemEstoque(oProdutoComposicao.pcIdProdutoComposicao.ToString(), iLocalEstoque34);

                                //                if (oItem.Quantity > eQtdEstoque)
                                //                {
                                //                    iLocalEstoque = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "33");
                                //                    break;
                                //                }
                                //                else
                                //                {
                                //                    iLocalEstoque = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "32");
                                //                }
                                //            }
                                //        }
                                //        else
                                //        {
                                //            int iLocalEstoque34 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "34");
                                //            double eQtdEstoque = ClasseFuncoes.RetornaSeTemEstoque(sProdutoTemp, iLocalEstoque34);

                                //            if (oItem.qty > eQtdEstoque)
                                //            {
                                //                iLocalEstoque = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "33");
                                //                break;
                                //            }
                                //            else
                                //            {
                                //                if (sProdutoTemp == "MAQSIG44" || sProdutoTemp == "MAQSIG43")
                                //                    iLocalEstoque = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "32");
                                //                else
                                //                    iLocalEstoque = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "33");
                                //            }
                                //        }
                                //    }
                                //}
                                //catch (Exception ex)
                                //{
                                //    return;
                                //}

                                Pedido oPedidoFortPlus = new Pedido();

                                Guid oGuid = Guid.NewGuid();
                                oPedidoFortPlus.id = 0;
                                oPedidoFortPlus.mvDocto = 0;
                                oPedidoFortPlus.mvIdPessoa = int.Parse(oCliente.id);
                                string sCNPJ = "";
                                oPedidoFortPlus.mvIdVendedor = ClasseFuncoes.RetornaVendedorFortPlus("B2W");
                                oPedidoFortPlus.mvIdSerie = ClasseFuncoes.RetornaCodigoGlobal("SR", "1");
                                oPedidoFortPlus.mvIdModelo = ClasseFuncoes.RetornaCodigoGlobal("MD", "55");
                                oPedidoFortPlus.mvTipoMovimento = "1";
                                oPedidoFortPlus.mvTipoPedido = "P";
                                oPedidoFortPlus.mvIdTipoDocumento = ClasseFuncoes.RetornaCodigoGlobal("TD", "REC");

                                oPedidoFortPlus.mvIdTipoFrete = ClasseFuncoes.RetornaCodigoGlobal("TF", "9");
                                if (float.Parse(oPedido.shipping_cost.ToString()) > 0)
                                {
                                    oPedidoFortPlus.mvIdTipoFrete = ClasseFuncoes.RetornaCodigoGlobal("TF", "1");
                                }

                                oPedidoFortPlus.mvPreNota = "N";
                                oPedidoFortPlus.mvFinNf = "1";
                                oPedidoFortPlus.mvPresenca = ClasseFuncoes.RetornaCodigoParametro("_FPS_NFE_PRESENCA");
                                oPedidoFortPlus.mvIdNatureza = ClasseFuncoes.RetornaCodigoGlobal("NO", "01");
                                oPedidoFortPlus.mvIdParent = null;
                                oPedidoFortPlus.idFilial = ClasseParametros.iFilial;

                                if (oPedido.shipping_carrier == "Courier")
                                {
                                    oPedidoFortPlus.mvIdTransportadora = ClasseFuncoes.RetornaTransportadorFortPlus("Courier");
                                }
                                else if (oPedido.shipping_carrier == "PAC" || oPedido.shipping_carrier == "Correios PAC")
                                {
                                    oPedidoFortPlus.mvIdTransportadora = ClasseFuncoes.RetornaTransportadorFortPlus("PAC");

                                }
                                else if (oPedido.shipping_carrier == "JadLog Standard" || oPedido.shipping_carrier == "")
                                {
                                    oPedidoFortPlus.mvIdTransportadora = ClasseFuncoes.RetornaTransportadorFortPlus("JadLog Standard");
                                }
                                else if (oPedido.shipping_carrier == "Fedex Standard")
                                {
                                    oPedidoFortPlus.mvIdTransportadora = ClasseFuncoes.RetornaTransportadorFortPlus("RAPIDAO COMETA");
                                }
                                else
                                {
                                    ClasseFuncoes.EnviaMensagemTelegramAsync("Pedido B2W não cadastrado pois o transportador está com erro!\n" + oPedido.customer.name + "\nPedido: " + oPedido.code.ToString(), "0,1", "Pedido não cadastrado", oPedido.code.ToString().Trim());

                                    continue;
                                }



                                int? eTotal = 0;
                                float? eTotalPago = 0;
                                float? eTotalValor = 0;
                                float? eTotalDesconto = float.Parse(oPedido.shipping_cost.ToString());

                                foreach (Item1 o in oPedido.items)
                                {
                                    eTotal += o.qty;
                                    string sPreco = o.original_price.ToString();

                                    eTotalValor += (float)(o.qty * float.Parse(sPreco));
                                }


                                s = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoFortPlus);
                                //List<Payment> lstPagamento = oPedido.payment.OfType<Payment>().ToList();
                                //foreach (Payment oPagamento in lstPagamento)
                                //{
                                //    eTotalPago += (float)oPagamento.Amount;
                                //}

                                oPedidoFortPlus.mvQuantidade = eTotal;
                                oPedidoFortPlus.mvPesoBruto = 0;
                                oPedidoFortPlus.mvPesoLiquido = 0;

                                oPedidoFortPlus.mvTpAmb = "1";
                                oPedidoFortPlus.mvTpEmis = "1";
                                oPedidoFortPlus.mvStatus = "0";
                                oPedidoFortPlus.mvEntidade = "PDV";
                                oPedidoFortPlus.ativo = "S";
                                oPedidoFortPlus.mvGuid = oGuid.ToString();
                                oPedidoFortPlus.dmaInclusao = DateTime.Now;
                                oPedidoFortPlus.dmaAlteracao = DateTime.Now;
                                oPedidoFortPlus.mvDmaEmissao = DateTime.Now;
                                oPedidoFortPlus.mvDmaEntradaSaida = DateTime.Now;
                                oPedidoFortPlus.mvValorOutrasDespesasAcessoria = 0;//float.Parse(oPedido.seller_shipping_cost.ToString());

                                oPedidoFortPlus.mvIdExterno = oPedido.code;
                                if (oPedido.shipping_cost.ToString() != "")
                                    oPedidoFortPlus.mvValorFrete = float.Parse(oPedido.shipping_cost.ToString());
                                oPedidoFortPlus.mvValorDesconto = 0;

                                oPedidoFortPlus.mvValorTotalProduto = eTotalValor;
                                //oPedidoFortPlus.valo = oPedido.total_amount;
                                oPedidoFortPlus.mvValorTotal = oPedido.total_ordered;
                                //mais de uma unidade
                                //04_02_2020
                                //if (oPedido.CustomerPfCpf != null)
                                //{
                                //    oPedidoFortPlus.mvValorTotalProduto = eTotalValor - eTotalDesconto;
                                //    //oPedidoFortPlus.valo = oPedido.total_amount;
                                //    oPedidoFortPlus.mvValorTotal = eTotalPago - eTotalDesconto;
                                //}


                                oPedidoFortPlus.mvVersao = ClasseFuncoes.RetornaCodigoParametro("_FPS_NFE_VERSAO");

                                //2322426297

                                oResposta = null;

                                while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                                {
                                    client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/IdExterno/" + oPedido.code.ToString());
                                    request = new RestRequest(Method.GET);
                                    request.AddHeader("Cache-Control", "no-cache");
                                    request.AddHeader("Accept", "*/*");
                                    request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                                    request.AddHeader("Content-Type", "application/json");
                                    request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                                    oResposta = client.Execute(request);

                                    if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                    {
                                        ClasseFuncoes.ConectaForteplus(5);
                                    }
                                    else if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                                    {
                                        break;
                                    }
                                    else if (oResposta.StatusCode == System.Net.HttpStatusCode.OK)
                                    {

                                        oPedidoFortPlus = Newtonsoft.Json.JsonConvert.DeserializeObject<Pedido>(oResposta.Content);
                                        break;
                                    }

                                }

                                if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                                    oPedidoFortPlus = ClasseFuncoes.CriaPedidoFortPlus(oPedidoFortPlus);

                                //foreach (Payment oPagamento in lstPagamento)
                                //{

                                //}


                                List<ProdutoFortePlus> lstProduto = new List<ProdutoFortePlus>();
                                float? eValorFreteProduto = oPedidoFortPlus.mvValorFrete / oPedido.items.Length;

                                foreach (Item1 o in oPedido.items)
                                {

                                    string sLast = o.product_id.Substring(o.product_id.Length - 5);
                                    string sInicio = o.product_id.Replace(sLast, "");
                                    sProduto = "";
                                    if (sLast.Substring(sLast.Length - 3, 3) == "VAR")
                                    {
                                        sLast = sLast.Replace("VAR", "");

                                        string[] aProduto = sLast.Split('G');
                                        sProduto = sInicio + aProduto[0];
                                    }
                                    else
                                    {
                                        sProduto = o.product_id;
                                    }


                                    ProdutoComplemento oProdutoComplemento = MAGALUClasseFuncoes.RetornaProdutoComplementoFortPlusPorSKU(sProduto);
                                    ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComplemento.cmIdProduto.ToString()).Content);

                                    if (oProduto.prIdParent != null)
                                    {
                                        List<ProdutoFortePlus> oProdutoTemp = lstProdutoGerais.Where(x => x.prCodigo != null && x.prCodigo == sProduto).ToList();
                                        List<ProdutoFortePlus> oProdutoParent = lstProdutoGerais.Where(x => x.id == oProdutoTemp[0].prIdParent).ToList();
                                        oProdutoComplemento = MAGALUClasseFuncoes.RetornaProdutoComplementoFortPlusPorSKU(oProdutoParent[0].prCodigo);

                                        //ProdutoFortePlus oProdutoTemp = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(sProduto).Content);
                                    }

                                    List<PedidoItemFortPlus> lstPedidoItem = ClasseFuncoes.RetornaItensPedido(oPedidoFortPlus.id);

                                    if (oProduto.prCodigo.Substring(0, 3).Trim() == "KIT")
                                    {
                                        List<int?> lstSKU = ClasseFuncoes.RetornaProdutoKIT(oProduto.prCodigo, "B2W");
                                        lstPedidoItem = lstPedidoItem.Where(x => x.mtIdProduto == lstSKU[0]).ToList();
                                        if (lstPedidoItem.Count > 0)
                                        {
                                            continue;
                                        }

                                    }
                                    else

                                    {
                                        lstPedidoItem = lstPedidoItem.Where(x => x.mtIdProduto == oProduto.id).ToList();
                                        if (lstPedidoItem.Count > 0)
                                        {
                                            continue;
                                        }
                                    }




                                    bool lTirarTarifa = oProduto.prPercentComissao == 1;

                                    if (oProduto.prCodigo.Substring(0, 3).Trim() == "KIT")
                                    {
                                        oResposta = null;

                                        while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                                        {
                                            client = new RestClient(ClasseParametros.sURlFortPlus + "/api/ProdutoComposicao/Produto/" + oProduto.id.ToString());
                                            request = new RestRequest(Method.GET);
                                            request.AddHeader("Cache-Control", "no-cache");
                                            request.AddHeader("Accept", "*/*");
                                            request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                                            request.AddHeader("Content-Type", "application/json");
                                            request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                                            oResposta = client.Execute(request);

                                            if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                            {
                                                ClasseFuncoes.ConectaForteplus(5);
                                            }
                                        }

                                        List<FortPlusProdutoComposicao> oListFormaPagamento = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FortPlusProdutoComposicao>>(oResposta.Content);
                                        float? eValorTotalComposicao = 0;

                                        foreach (FortPlusProdutoComposicao oFormaPagamento in oListFormaPagamento)
                                        {
                                            ProdutoComplemento oProdutoComplementoUsar = new ProdutoComplemento();
                                            List<ProdutoComplemento> oProdutoComplementoComposicao = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(ClasseFuncoes.RetornaProdutoComplementoFortPlus(oFormaPagamento.pcIdProdutoComposicao.ToString()).Content);
                                            foreach (ProdutoComplemento oPC in oProdutoComplementoComposicao)
                                            {
                                                string sMarketPlace = ClasseFuncoes.RetornaNomeGlobalMK("MK", oPC.cmIdMarketPlace);
                                                if (sMarketPlace.Contains("B2W"))
                                                {
                                                    oProdutoComplementoUsar = oPC;
                                                    break;
                                                }
                                            }
                                            eValorTotalComposicao += oProdutoComplementoUsar.cmPrecoDePor;
                                        }

                                        foreach (FortPlusProdutoComposicao oFormaPagamento in oListFormaPagamento)
                                        {
                                            PedidoItemFortPlus oItemPedido = new PedidoItemFortPlus();
                                            oItemPedido.id = 0;
                                            oItemPedido.mtIdNfOrigem = null;
                                            oItemPedido.mtIdMovto = oPedidoFortPlus.id;
                                            lstProduto.Add(oProduto);

                                            float? eTotalSemTarifa = eTotalValor - eTotalDesconto;
                                            double? eQuantidade = o.qty * oFormaPagamento.pcQtde;
                                            float? eQtd = float.Parse(eQuantidade.ToString());
                                            List<ProdutoComplemento> oProdutoComplementoComposicao = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(ClasseFuncoes.RetornaProdutoComplementoFortPlus(oFormaPagamento.pcIdProdutoComposicao.ToString()).Content);
                                            ProdutoComplemento oProdutoComplementoUsar = new ProdutoComplemento();


                                            foreach (ProdutoComplemento oPC in oProdutoComplementoComposicao)
                                            {
                                                string sMarketPlace = ClasseFuncoes.RetornaNomeGlobalMK("MK", oPC.cmIdMarketPlace);
                                                if (sMarketPlace.Contains("B2W"))
                                                {
                                                    oProdutoComplementoUsar = oPC;
                                                    break;
                                                }
                                            }
                                            float? ePercentual = (oProdutoComplementoUsar.cmPrecoDePor * 100) / eValorTotalComposicao;
                                            double? eValorProduto = (eTotalValor * ePercentual) / 100;

                                            oItemPedido.mtIdProduto = oFormaPagamento.pcIdProdutoComposicao;
                                            oItemPedido.mtQtde = eQtd;
                                            oItemPedido.mtValorUnitario = float.Parse(eValorProduto.ToString());
                                            oItemPedido.mtValorTotal = oItemPedido.mtValorUnitario * eQtd;

                                            //////mais de uma unidade
                                            //////04_02_2020
                                            //if (oPedido.CustomerPfCpf != "CPF")
                                            //{
                                            //    ePercentual = (oProdutoComplementoUsar.cmPrecoDePor * 100) / eValorTotalComposicao;
                                            //    eValorProduto = (eTotalSemTarifa * ePercentual) / 100;

                                            //    if (oListFormaPagamento.Count == 1)
                                            //    {
                                            //        eValorProduto = eTotalSemTarifa / eQtd;
                                            //    }

                                            //    oItemPedido.mtValorUnitario = float.Parse(eValorProduto.ToString());
                                            //    oItemPedido.mtValorTotal = oItemPedido.mtValorUnitario * eQtd;
                                            //}

                                            oItemPedido.mtValorDesconto = 0;
                                            oItemPedido.mtValorDescontoRateio = 0;
                                            oItemPedido.mtPercDesconto = 0;
                                            oItemPedido.mtValor = 0;
                                            oItemPedido.mtValorFrete = eValorFreteProduto;
                                            oItemPedido.mtValorSeguro = 0;
                                            oItemPedido.mtValorOutrasDespesas = 0;
                                            oItemPedido.mtCustoMedio = null;
                                            oItemPedido.mtValorTabela = oProdutoComplementoUsar.cmPrecoVenda;
                                            oItemPedido.mtPesoBruto = null;
                                            oItemPedido.mtPesoLiquido = null;
                                            oItemPedido.mtIdCfop = null;
                                            oItemPedido.mtIdNcm = oProduto.prIdNcm;

                                            oItemPedido.mtIdLocalEstoque = iLocalEstoque;

                                            oItemPedido.mtValorAproxImposto = null;
                                            oItemPedido.mtValorTributoEstadual = null;
                                            oItemPedido.mtValorTributoImportado = null;
                                            oItemPedido.mtValorTributoMunicipal = null;
                                            oItemPedido.mtValorTributoNacional = null;
                                            oItemPedido.mtPercEstadual = null;
                                            oItemPedido.mtPercImportado = null;
                                            oItemPedido.mtPercMunicipal = null;
                                            oItemPedido.mtPercNacional = null;
                                            oItemPedido.mtVersaoIbpt = "";
                                            oItemPedido.mtIdUnidade = oProduto.prIdUnidadePrincipal;
                                            oItemPedido.mtOrdemCompra = "";
                                            oItemPedido.mtOrdemItemCompra = "";
                                            oItemPedido.mtReferencia = "";
                                            oItemPedido.mtEntidade = "";
                                            oItemPedido.mtModalidadeBcIcms = "";
                                            oItemPedido.mtIdCstIcms = null;
                                            oItemPedido.mtBaseIcms = null;
                                            oItemPedido.mtAliquotaIcms = null;
                                            oItemPedido.mtPercentReducaoBaseIcms = null;
                                            oItemPedido.mtValorIcms = null;
                                            oItemPedido.mtValorReducaoIcms = null;
                                            oItemPedido.mtAliquotaIcmsCr = null;
                                            oItemPedido.mtValorIcmsCr = null;
                                            oItemPedido.mtBaseIcmsCr = null;
                                            oItemPedido.mtModalidadeBcIcmsSt = "";
                                            oItemPedido.mtPercentReducaoBaseIcmsSt = null;
                                            oItemPedido.mtAliquotaIcmsSt = null;
                                            oItemPedido.mtAliquotaMva = null;
                                            oItemPedido.mtValorIcmsSt = null;
                                            oItemPedido.mtValorReducaoIcmsSt = null;
                                            oItemPedido.mtBaseIcmsSt = null;
                                            oItemPedido.mtIdCstPis = null;
                                            oItemPedido.mtBasePis = null;
                                            oItemPedido.mtPercentReducaoBasePis = null;
                                            oItemPedido.mtAliquotaPis = null;
                                            oItemPedido.mtValorPis = null;
                                            oItemPedido.mtIdCstCofins = null;
                                            oItemPedido.mtBaseCofins = null;
                                            oItemPedido.mtPercentReducaoBaseCofins = null;
                                            oItemPedido.mtAliquotaCofins = null;
                                            oItemPedido.mtValorCofins = null;
                                            oItemPedido.mtIdCstIpi = null;
                                            oItemPedido.mtBaseIpi = null;
                                            oItemPedido.mtPercentReducaoBaseIpi = null;
                                            oItemPedido.mtAliquotaIpi = null;
                                            oItemPedido.mtValorIpi = null;
                                            oItemPedido.mtIdCstIi = null;
                                            oItemPedido.mtBaseIi = null;
                                            oItemPedido.mtPercentReducaoBaseIi = null;
                                            oItemPedido.mtValorIi = null;
                                            oItemPedido.mtIdCstIssqn = null;
                                            oItemPedido.mtBaseIssqn = null;
                                            oItemPedido.mtPercentReducaoBaseIssqn = null;
                                            oItemPedido.mtValorIssqn = null;
                                            //oItemPedido.mtAliquotaInterEstadual = null;
                                            oItemPedido.mtBaseDifal = null;
                                            oItemPedido.mtAliquotaDifal = null;
                                            oItemPedido.mtValorDifal = null;
                                            oItemPedido.mtBaseFecp = null;
                                            oItemPedido.mtAliquotaFecp = null;
                                            oItemPedido.mtValorFecp = null;
                                            oItemPedido.mtBaseFecpSt = null;
                                            oItemPedido.mtAliquotaFecpSt = null;
                                            oItemPedido.mtValorFecpSt = null;
                                            oItemPedido.mtBaseFecpStRet = null;
                                            oItemPedido.mtAliquotaFecpStRet = null;
                                            oItemPedido.mtValorFecpStRet = null;
                                            oItemPedido.mtValorIcmsPartUfOrigem = null;
                                            oItemPedido.mtValorIcmsPartUfDestino = null;
                                            oItemPedido.mtAliquotaIcmsPartUfOrigem = null;
                                            oItemPedido.mtAliquotaIcmsPartUfDestino = null;
                                            oItemPedido.mtBaseIcmsPartUfOrigem = null;
                                            oItemPedido.mtBaseIcmsPartUfDestino = null;
                                            oItemPedido.mtBaseCalculoKardex = null;
                                            oItemPedido.mtPercentComissao = null;
                                            oItemPedido.mtGuid = oPedidoFortPlus.mvGuid;
                                            oItemPedido.mtObservacao = "";
                                            oItemPedido.mtDmaItem = DateTime.Now;
                                            oItemPedido.idFilial = ClasseParametros.iFilial;
                                            oItemPedido.idIncluidoPor = null;
                                            oItemPedido.idAltaradoPor = null;
                                            oItemPedido.dmaInclusao = DateTime.Now;
                                            oItemPedido.dmaAlteracao = DateTime.Now;
                                            oItemPedido.ativo = "S";
                                            oItemPedido.mtIdCest = oProduto.prIdCest;
                                            oItemPedido.mtIdParent = null;
                                            oItemPedido.mtQtdeLiberada = null;
                                            oItemPedido.mtQtdeSaldo = null;
                                            oItemPedido.mtVariacao = "";
                                            oItemPedido.mtPrecoDePor = null;
                                            oItemPedido.mtAjCusto = "";
                                            oItemPedido.mtCEnqIpi = "";
                                            oItemPedido.mtAgregaTotalFecp = "";
                                            oItemPedido.mtIdLote = null;
                                            oItemPedido.mtLote = "";
                                            oItemPedido.mtDmaLote = DateTime.Now;
                                            s = Newtonsoft.Json.JsonConvert.SerializeObject(oItemPedido);
                                            ClasseFuncoes.InseriItensPedidoFortPlus(oItemPedido);

                                        }
                                    }
                                    else
                                    {

                                        PedidoItemFortPlus oItemPedido = new PedidoItemFortPlus();
                                        oItemPedido.id = 0;
                                        oItemPedido.mtIdNfOrigem = null;
                                        oItemPedido.mtIdMovto = oPedidoFortPlus.id;
                                        oItemPedido.mtValorFrete = eValorFreteProduto;

                                        lstProduto.Add(oProduto);

                                        oItemPedido.mtIdProduto = oProdutoComplemento.cmIdProduto;
                                        oItemPedido.mtQtde = o.qty;

                                        string sPreco = o.original_price.ToString();

                                        oItemPedido.mtValorUnitario = float.Parse(sPreco);
                                        oItemPedido.mtValorTotal = (float)(o.qty * float.Parse(sPreco));

                                        //mais de uma unidade
                                        //04_02_2020
                                        //float? eTotalSemTarifa = eTotalValor - eTotalDesconto;

                                        //if (oPedido.CustomerPfCpf != null)
                                        //{
                                        //    float? eValorProduto = eTotalSemTarifa / o.Quantity;
                                        //    oItemPedido.mtIdProduto = oProdutoComplemento.cmIdProduto;
                                        //    oItemPedido.mtQtde = o.Quantity;
                                        //    oItemPedido.mtValorUnitario = eValorProduto;
                                        //    oItemPedido.mtValorTotal = eValorProduto * o.Quantity;
                                        //}

                                        oItemPedido.mtValorDesconto = 0;
                                        oItemPedido.mtValorDescontoRateio = 0;
                                        oItemPedido.mtPercDesconto = 0;
                                        oItemPedido.mtValor = 0;
                                        oItemPedido.mtValorFrete = eValorFreteProduto;
                                        oItemPedido.mtValorSeguro = 0;
                                        oItemPedido.mtValorOutrasDespesas = 0;
                                        oItemPedido.mtCustoMedio = null;
                                        oItemPedido.mtValorTabela = oProdutoComplemento.cmPrecoVenda;
                                        oItemPedido.mtPesoBruto = null;
                                        oItemPedido.mtPesoLiquido = null;
                                        oItemPedido.mtIdCfop = null;
                                        oItemPedido.mtIdNcm = oProduto.prIdNcm;

                                        oItemPedido.mtIdLocalEstoque = iLocalEstoque;

                                        oItemPedido.mtValorAproxImposto = null;
                                        oItemPedido.mtValorTributoEstadual = null;
                                        oItemPedido.mtValorTributoImportado = null;
                                        oItemPedido.mtValorTributoMunicipal = null;
                                        oItemPedido.mtValorTributoNacional = null;
                                        oItemPedido.mtPercEstadual = null;
                                        oItemPedido.mtPercImportado = null;
                                        oItemPedido.mtPercMunicipal = null;
                                        oItemPedido.mtPercNacional = null;
                                        oItemPedido.mtVersaoIbpt = "";
                                        oItemPedido.mtIdUnidade = oProduto.prIdUnidadePrincipal;
                                        oItemPedido.mtOrdemCompra = "";
                                        oItemPedido.mtOrdemItemCompra = "";
                                        oItemPedido.mtReferencia = "";
                                        oItemPedido.mtEntidade = "";
                                        oItemPedido.mtModalidadeBcIcms = "";
                                        oItemPedido.mtIdCstIcms = null;
                                        oItemPedido.mtBaseIcms = null;
                                        oItemPedido.mtAliquotaIcms = null;
                                        oItemPedido.mtPercentReducaoBaseIcms = null;
                                        oItemPedido.mtValorIcms = null;
                                        oItemPedido.mtValorReducaoIcms = null;
                                        oItemPedido.mtAliquotaIcmsCr = null;
                                        oItemPedido.mtValorIcmsCr = null;
                                        oItemPedido.mtBaseIcmsCr = null;
                                        oItemPedido.mtModalidadeBcIcmsSt = "";
                                        oItemPedido.mtPercentReducaoBaseIcmsSt = null;
                                        oItemPedido.mtAliquotaIcmsSt = null;
                                        oItemPedido.mtAliquotaMva = null;
                                        oItemPedido.mtValorIcmsSt = null;
                                        oItemPedido.mtValorReducaoIcmsSt = null;
                                        oItemPedido.mtBaseIcmsSt = null;
                                        oItemPedido.mtIdCstPis = null;
                                        oItemPedido.mtBasePis = null;
                                        oItemPedido.mtPercentReducaoBasePis = null;
                                        oItemPedido.mtAliquotaPis = null;
                                        oItemPedido.mtValorPis = null;
                                        oItemPedido.mtIdCstCofins = null;
                                        oItemPedido.mtBaseCofins = null;
                                        oItemPedido.mtPercentReducaoBaseCofins = null;
                                        oItemPedido.mtAliquotaCofins = null;
                                        oItemPedido.mtValorCofins = null;
                                        oItemPedido.mtIdCstIpi = null;
                                        oItemPedido.mtBaseIpi = null;
                                        oItemPedido.mtPercentReducaoBaseIpi = null;
                                        oItemPedido.mtAliquotaIpi = null;
                                        oItemPedido.mtValorIpi = null;
                                        oItemPedido.mtIdCstIi = null;
                                        oItemPedido.mtBaseIi = null;
                                        oItemPedido.mtPercentReducaoBaseIi = null;
                                        oItemPedido.mtValorIi = null;
                                        oItemPedido.mtIdCstIssqn = null;
                                        oItemPedido.mtBaseIssqn = null;
                                        oItemPedido.mtPercentReducaoBaseIssqn = null;
                                        oItemPedido.mtValorIssqn = null;
                                        //oItemPedido.mtAliquotaInterEstadual = null;
                                        oItemPedido.mtBaseDifal = null;
                                        oItemPedido.mtAliquotaDifal = null;
                                        oItemPedido.mtValorDifal = null;
                                        oItemPedido.mtBaseFecp = null;
                                        oItemPedido.mtAliquotaFecp = null;
                                        oItemPedido.mtValorFecp = null;
                                        oItemPedido.mtBaseFecpSt = null;
                                        oItemPedido.mtAliquotaFecpSt = null;
                                        oItemPedido.mtValorFecpSt = null;
                                        oItemPedido.mtBaseFecpStRet = null;
                                        oItemPedido.mtAliquotaFecpStRet = null;
                                        oItemPedido.mtValorFecpStRet = null;
                                        oItemPedido.mtValorIcmsPartUfOrigem = null;
                                        oItemPedido.mtValorIcmsPartUfDestino = null;
                                        oItemPedido.mtAliquotaIcmsPartUfOrigem = null;
                                        oItemPedido.mtAliquotaIcmsPartUfDestino = null;
                                        oItemPedido.mtBaseIcmsPartUfOrigem = null;
                                        oItemPedido.mtBaseIcmsPartUfDestino = null;
                                        oItemPedido.mtBaseCalculoKardex = null;
                                        oItemPedido.mtPercentComissao = null;
                                        oItemPedido.mtGuid = oPedidoFortPlus.mvGuid;
                                        oItemPedido.mtObservacao = "";
                                        oItemPedido.mtDmaItem = DateTime.Now;
                                        oItemPedido.idFilial = ClasseParametros.iFilial;
                                        oItemPedido.idIncluidoPor = null;
                                        oItemPedido.idAltaradoPor = null;
                                        oItemPedido.dmaInclusao = DateTime.Now;
                                        oItemPedido.dmaAlteracao = DateTime.Now;
                                        oItemPedido.ativo = "S";
                                        oItemPedido.mtIdCest = oProduto.prIdCest;
                                        oItemPedido.mtIdParent = null;
                                        oItemPedido.mtQtdeLiberada = null;
                                        oItemPedido.mtQtdeSaldo = null;
                                        oItemPedido.mtVariacao = "";
                                        oItemPedido.mtPrecoDePor = null;
                                        oItemPedido.mtAjCusto = "";
                                        oItemPedido.mtCEnqIpi = "";
                                        oItemPedido.mtAgregaTotalFecp = "";
                                        oItemPedido.mtIdLote = null;
                                        oItemPedido.mtLote = "";
                                        oItemPedido.mtDmaLote = DateTime.Now;
                                        s = Newtonsoft.Json.JsonConvert.SerializeObject(oItemPedido);
                                        ClasseFuncoes.InseriItensPedidoFortPlus(oItemPedido);
                                    }
                                }

                                sSql = "UPDATE VENDAMARKETPLACE SET STATUS = 1, QUANTIDADEACIMA = 0 WHERE TRIM(ID) ='" + oPedido.code.ToString().Trim() + "'";
                                ClasseParametros.ExecutabancoMySql(sSql);



                                oResposta = null;

                                while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                                {
                                    client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/IdExterno/" + oPedido.code.ToString());
                                    request = new RestRequest(Method.GET);
                                    request.AddHeader("Cache-Control", "no-cache");
                                    request.AddHeader("Accept", "*/*");
                                    request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                                    request.AddHeader("Content-Type", "application/json");
                                    request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                                    oResposta = client.Execute(request);

                                    if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                    {
                                        ClasseFuncoes.ConectaForteplus(5);
                                    }
                                    else if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                                    {
                                        break;
                                    }
                                    else if (oResposta.StatusCode == System.Net.HttpStatusCode.OK)
                                    {

                                        oPedidoFortPlus = Newtonsoft.Json.JsonConvert.DeserializeObject<Pedido>(oResposta.Content);
                                        break;
                                    }

                                }

                                FortPlusFinanceiroReduzido oFinanceiro = new FortPlusFinanceiroReduzido();
                                oFinanceiro.email = "rodrigonunes@2eletro.com.br";
                                oFinanceiro.idFilial = oPedidoFortPlus.idFilial;
                                oFinanceiro.idMovto = oPedidoFortPlus.id;
                                oFinanceiro.idFormaPagamento = ClasseFuncoes.RetornaCodigoFormaPagamento("CREDIT_CARD");
                                oFinanceiro.idCondicaoPagamento = ClasseFuncoes.RetornaCodigoCondicaoPagamento("À VISTA");
                                oFinanceiro.valor = (double)oPedidoFortPlus.mvValorTotal;
                                oFinanceiro = ClasseFuncoes.CadastraFinanceiroReduzido(oFinanceiro);

                                //foreach (ProdutoFortePlus oProduto in lstProduto)
                                //{
                                //    ClasseFuncoes.EnviaProdutosMercadoLivre(oProduto.id.ToString());
                                //}


                                if (eTotal > 1)
                                {
                                    sSql = "UPDATE VENDAMARKETPLACE SET STATUSMENSAGEM = 3, QUANTIDADEACIMA = 1 WHERE TRIM(ID) ='" + oPedido.code.ToString().Trim() + "'";
                                    ClasseParametros.ExecutabancoMySql(sSql);
                                }

                                string smensagem = string.Format(ClasseParametros.sMensagemAcabouComprar, oCliente.psNome);

                                string sPack = oPedido.code.ToString();
                                if (oPedido.code != null)
                                    sPack = oPedido.code.ToString();

                                // EnviaMensagemMercadoLivre(sPack, oPedido.seller.id.ToString(), oPedido.buyer.id.ToString(), smensagem, "1", oPedido.id.ToString());

                                d.Dispose();
                                ClasseFuncoes.AtualizaDadosFortPlus(oPedidoFortPlus.id);
                                ClasseFuncoes.AtualizaDadosFortPlus(oPedidoFortPlus.id);


                                foreach (Item1 o in oPedido.items)
                                {
                                    string sLast = o.product_id.Substring(o.product_id.Length - 5);
                                    string sInicio = o.product_id.Replace(sLast, "");
                                    sProduto = "";
                                    if (sLast.Substring(sLast.Length - 3, 3) == "VAR")
                                    {
                                        sLast = sLast.Replace("VAR", "");

                                        string[] aProduto = sLast.Split('G');
                                        sProduto = sInicio + aProduto[0];
                                    }
                                    else
                                    {
                                        sProduto = o.product_id;
                                    }
                                    ProdutoComplemento oProdutoComplemento = MAGALUClasseFuncoes.RetornaProdutoComplementoFortPlusPorSKU(sProduto);
                                    ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComplemento.cmIdProduto.ToString()).Content);

                                    if (oProduto.prIdParent != null)
                                    {
                                        List<ProdutoFortePlus> oProdutoTemp = lstProdutoGerais.Where(x => x.prCodigo != null && x.prCodigo == sProduto).ToList();
                                        List<ProdutoFortePlus> oProdutoParent = lstProdutoGerais.Where(x => x.id == oProdutoTemp[0].prIdParent).ToList();
                                        oProdutoComplemento = MAGALUClasseFuncoes.RetornaProdutoComplementoFortPlusPorSKU(oProdutoParent[0].prCodigo);

                                        //ProdutoFortePlus oProdutoTemp = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(sProduto).Content);
                                    }
                                    ClasseFuncoes.AtualizaProdutoB2W(oProdutoComplemento.cmCodigo, iCodigoCliente);
                                }

                                //sSql = "SELECT TRIM(ID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(ID) = '" + oPedido.code.ToString().Trim() + "' AND MARKETPLACE LIKE '%B2W%'";
                                //d = ClasseParametros.ConsultaBancoMysql(sSql);
                                //if (d.Rows.Count > 0)
                                //{
                                //    // Atualiza pedido para MAGALU
                                //    #region Marca pedido como processado
                                //    MAGALUPedidoProcessado oPedidoProcessado = new MAGALUPedidoProcessado();
                                //    oPedidoProcessado.IdOrder = oPedido.code;
                                //    oPedidoProcessado.OrderStatus = "PROCESSING";

                                //    string sJsonProcessado = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoProcessado);

                                //    client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order");
                                //    request = new RestRequest(Method.PUT);
                                //    request.AddHeader("cache-control", "no-cache");
                                //    request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                                //    request.AddParameter("application/json", sJsonProcessado, ParameterType.RequestBody);

                                //    IRestResponse response = client.Execute(request);
                                //    #endregion
                                //}
                                //d.Dispose();
                            }
                            else
                            {

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ClasseFuncoes.EnviaMensagemTelegramAsync("Pedido B2W não cadastrado pois está com erro!\n" + oPedido.customer.name, "0,1", "Pedido não cadastrado", oPedido.code.ToString().Trim());

                    }


                    MessageBox.Show("Importado");
                    return;
                    #endregion

                    iPagina += 1;

                }

            }
            catch
            {

            }
        }

        private void btnEnviaNotab2w_Click(object sender, EventArgs e)
        {
            try
            {
                RestRequest oRequest = null;
                IRestResponse oResposta = null;
                RestClient client = null;

                DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM CLIENTE WHERE CODIGO = 5");
                string sKey = d.Rows[0]["KEYB2W"].ToString();
                string sUsuario = d.Rows[0]["USUARIOB2W"].ToString();
                string sAccount = "0MDxaksT8d";
                d.Dispose();

                int iPagina = 1;
                int iPorPagina = 50;

                List<Pedido> oListaPedidoFortePlus = ClasseFuncoes.RetornaListaPedidoFortePlus();
                List<FortPlusXML> oListaXMLFortePlus = ClasseFuncoes.RetornaListaXMLFortPlus();
                List<ProdutoFortePlus> oListaProdutosFortePlus = ClasseFuncoes.RetornaListProdutosFortPlus();

                iPagina = 1;
                while (true)
                {
                    oResposta = null;
                    while (oResposta == null)
                    {
                        //client = new RestClient("http://api.skyhub.com.br/orders?filters[statuses][]=payment_received&page=" + iPagina.ToString() + "&per_page=" + iPorPagina.ToString());
                        //order_invoiced
                        client = new RestClient("http://api.skyhub.com.br/orders/" + editPedidoB2W.Text);
                        //client = new RestClient("http://api.skyhub.com.br/orders?filters[statuses][]=" + sStatus + "&page=" + iPagina.ToString() + "&per_page=" + iPorPagina.ToString());
                        //client = new RestClient("http://api.skyhub.com.br/orders?page=" + iPagina.ToString() + "&per_page=" + iPorPagina.ToString());


                        //client = new RestClient("http://api.skyhub.com.br/orders/Lojas Americanas-407890045");

                        ///
                        //client = new RestClient("http://api.skyhub.com.br/orders?page=" + iPagina.ToString() + "&per_page=" + iPorPagina.ToString());
                        //client = new RestClient("http://api.skyhub.com.br/orders?page=0&per_page=50");
                        oRequest = new RestRequest(Method.GET);
                        oRequest.AddHeader("cache-control", "no-cache");
                        oRequest.AddHeader("Accept", "application/json");
                        oRequest.AddHeader("Content-Type", "application/json");
                        oRequest.AddHeader("x-Api-Key", sKey);
                        oRequest.AddHeader("X-User-Email", sUsuario);
                        oRequest.AddHeader("X-Accountmanager-Key", sAccount);
                        oResposta = client.Execute(oRequest);

                        if (oResposta.StatusCode == HttpStatusCode.GatewayTimeout || oResposta.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            oResposta = null;

                        }
                        else if (oResposta.StatusCode == HttpStatusCode.BadGateway)
                        {
                            oResposta = null;

                        }
                    }
                    Thread.Sleep(2000);

                    if (oResposta.Content == "Account  not found")
                    {
                        continue;
                    }
                    if (oResposta.Content.Contains("504 Gateway Time-ou"))
                    {
                        continue;
                    }


                    B2WPedidos lstPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<B2WPedidos>(oResposta.Content);
                    if (lstPedido.orders == null || lstPedido.orders.Length == 0)
                    {
                        break;
                    }

                    foreach (B2WPedido oPedido in lstPedido.orders)
                    {
                        if (oPedido.code.Contains("276421954301"))
                        {

                        }

                        //if (oPedido.invoices.Length > 0)
                        //{
                        //    continue;
                        //}

                        //if(oPedido.code.Trim().Contains("Americanas Empresas"))
                        //{
                        //    oPedido.code = oPedido.code.Replace("Americanas Empresas", "Lojas Americanas");
                        //}

                        DataTable dtbMarketPlace = ClasseParametros.ConsultaBancoMysql("SELECT * FROM XMLVENDAS WHERE PEDIDO = '" + oPedido.code.Trim() + "'");
                        List<Pedido> lstPedidoFiltrado = oListaPedidoFortePlus.Where(x => x.mvIdExterno == oPedido.code.Trim() && x.mvEntidade == "NFE").ToList();
                        Pedido oPedidoFortePlus = null;
                        if (lstPedidoFiltrado.Count > 0)
                            oPedidoFortePlus = lstPedidoFiltrado[0];

                        if (oPedidoFortePlus == null)
                            continue;

                        Console.WriteLine("Envia nota do pedido " + oPedido.code.Trim());

                        DanfeViewModel oDanfe = null;
                        string sXML = "";
                        if (dtbMarketPlace.Rows.Count == 0)
                        {
                            List<FortPlusXML> oListXMLFiltrado = oListaXMLFortePlus.Where(x => x.trIdMovto == oPedidoFortePlus.id).ToList();
                            if (oListXMLFiltrado.Count == 0)
                                continue;
                            sXML = oListXMLFiltrado[0].trArquivoRetorno;
                        }
                        else
                        {
                            //sXML = System.Text.Encoding.UTF8.GetString(dtbMarketPlace.Rows[0]["XML"]);;

                            sXML = Encoding.ASCII.GetString((byte[])dtbMarketPlace.Rows[0]["XML"]);
                        }


                        if (sXML != null && sXML != "")
                        {
                            string sPasta = Directory.GetCurrentDirectory();
                            oDanfe = DanfeViewModelCreator.CriarDeStringXml(sXML);
                        }

                        if (oDanfe == null)
                        {
                            continue;
                        }

                        B2WInvoiced oFaturado = new B2WInvoiced();
                        oFaturado.status = "order_invoiced";
                        InvoiceInvoiced oNota = new InvoiceInvoiced();
                        oNota.issue_date = ((DateTime)oDanfe.DataHoraEmissao).ToString("yyyy-MM-ddTHH:mm:ss") + "-03:00";
                        oNota.key = oDanfe.ChaveAcesso;
                        oNota.volume_qty = int.Parse(oDanfe.Transportadora.QuantidadeVolumes.ToString());
                        oFaturado.invoice = oNota;

                        string sJSON = Newtonsoft.Json.JsonConvert.SerializeObject(oFaturado);
                        oResposta = null;
                        while (oResposta == null)
                        {
                            client = new RestClient("http://api.skyhub.com.br/orders/" + oPedido.code + "/invoice");
                            oRequest = new RestRequest(Method.POST);
                            oRequest.AddHeader("cache-control", "no-cache");
                            oRequest.AddHeader("Accept", "application/json");
                            oRequest.AddHeader("Content-Type", "application/json");
                            oRequest.AddHeader("x-Api-Key", sKey);
                            oRequest.AddHeader("X-User-Email", sUsuario);
                            oRequest.AddParameter("application/json", sJSON, ParameterType.RequestBody);
                            oResposta = client.Execute(oRequest);
                            if (oResposta.StatusCode == HttpStatusCode.Created)
                            {
                                break;
                            }
                        }

                        Thread.Sleep(1000);

                        // Atualiza status no forteplus
                        oPedidoFortePlus.mvIdStatus = 623;

                        string sJson = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoFortePlus);

                        oResposta = null;

                        while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/" + oPedidoFortePlus.id);
                            RestRequest request = new RestRequest(Method.PUT);
                            request.AddHeader("Cache-Control", "no-cache");
                            request.AddHeader("Accept", "*/*");
                            request.AddHeader("Content-Type", "application/json");
                            request.AddParameter("application/json", sJson, ParameterType.RequestBody);
                            if (ClasseParametros.oJsonFortePluslogin == null)
                                ClasseFuncoes.ConectaForteplus(5);
                            request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                            oResposta = client.Execute(request);

                            if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                ClasseFuncoes.ConectaForteplus(5);
                            }
                            else if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                            {
                                break;
                            }
                            else if (oResposta.StatusCode == System.Net.HttpStatusCode.NoContent)
                            {

                                break;
                            }

                        }

                        //if(oDanfe.Transportadora.RazaoSocial.Contains("jadlog"))
                        //{
                        //    B2WEnvioTracking oEnvioTracking = new B2WEnvioTracking();
                        //    oEnvioTracking.shipment= new ShipmentEnvioTracking();
                        //    oEnvioTracking.shipment.code = oPedido.code;
                        //    oEnvioTracking.shipment.delivered_carrier_date =DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T18:00:00");
                        //    List<PedidoItemFortPlus> oListaPedidoItemFortePlus = RetornaItensPedido(oPedidoFortePlus.id);

                        //    ItemEnvioTracking[] aItem = new ItemEnvioTracking[oListaPedidoItemFortePlus.Count];

                        //    int i = 0;
                        //    foreach(PedidoItemFortPlus oItem in oListaPedidoItemFortePlus)
                        //    {
                        //       List<ProdutoFortePlus> oListaProdutosFortePlusTemp


                        //        //aItem[i].sku = oItem.co
                        //    }







                        //    //oEnvioTracking.shipment.

                        //    InvoiceInvoiced oNota = new InvoiceInvoiced();
                        //    string dAgora = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
                        //    oNota.issue_date = DateTime.Parse(dAgora);
                        //    oNota.issue_date = oNota.issue_date.AddHours(3);
                        //    oNota.key = oDanfe.ChaveAcesso;
                        //    oNota.volume_qty = int.Parse(oDanfe.Transportadora.QuantidadeVolumes.ToString());
                        //    oFaturado.invoice = oNota;

                        //    string sJSON = Newtonsoft.Json.JsonConvert.SerializeObject(oFaturado);

                        //    client = new RestClient("http://api.skyhub.com.br/orders/" + oPedido.code + "/invoice");
                        //    oRequest = new RestRequest(Method.POST);
                        //    oRequest.AddHeader("cache-control", "no-cache");
                        //    oRequest.AddHeader("Accept", "application/json");
                        //    oRequest.AddHeader("Content-Type", "application/json");
                        //    oRequest.AddHeader("x-Api-Key", sKey);
                        //    oRequest.AddHeader("X-User-Email", sUsuario);
                        //    oRequest.AddParameter("application/json", sJSON, ParameterType.RequestBody);
                        //    oResposta = client.Execute(oRequest);










                        //}
                        ////}
                    }
                    iPagina += 1;

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void btnPedidosDuplicados_Click(object sender, EventArgs e)
        {
            List<Pedido> oListaPedidoFortePlus = ClasseFuncoes.RetornaListaPedidoFortePlus();
            List<FortPlusXML> oListaXMLFortePlus = ClasseFuncoes.RetornaListaXMLFortPlus();


            string sTexto = "";
            string sDuplicados = "";
            string[] sLinha = null;

            using (var reader = new StreamReader(@"C:\Users\rrgnu\Downloads\clientesmais1nota.csv"))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                int i = 0;
                while (!reader.EndOfStream)
                {
                    var line = "";
                    if (i == 0)
                    {
                        line = reader.ReadLine();
                        i++;
                        continue;
                    }



                    line = reader.ReadLine();
                    var values = line.Split(',');

                    List<FortPlusXML> oListaXMLDuplicado = oListaXMLFortePlus.Where(x => x.trIdCliente.ToString() == values[0]).ToList();
                    string sUltimo = "";
                    string sUltimoStatusNota = "";
                    foreach (FortPlusXML oXML in oListaXMLDuplicado)
                    {
                        List<Pedido> oPedido = oListaPedidoFortePlus.Where(x => x.mvDocto.ToString() == oXML.trDocto.ToString()).ToList();
                        if (sTexto != "")
                            sTexto += "\r\n";
                        if (oPedido[0].mvIdExterno == sUltimo && sUltimoStatusNota == "100")
                        {
                            sTexto += "DUPLICADO - ";
                        }
                        sUltimo = oPedido[0].mvIdExterno;
                        sUltimoStatusNota = oXML.trCstat;
                        sTexto += oXML.trDocto.ToString() + " - " + sUltimo;
                    }


                    i++;
                }

                File.WriteAllText(@"C:\Users\rrgnu\OneDrive\Documentos\2Eletros & Interplace\Backup\duplciados.txt", sTexto);





                sDuplicados = File.ReadAllText(@"C:\Users\rrgnu\OneDrive\Documentos\2Eletros & Interplace\Backup\duplciados.txt");


                sTexto = "";
                sLinha = sDuplicados.Split('\n');
                foreach (string sL in sLinha)
                {
                    if (sL.Contains("DUPLICADO"))
                    {
                        if (sTexto != "")
                        {
                            sTexto += "\r\n";
                        }
                        sTexto += sL;
                    }
                }







            }


            //using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(@"C:\Users\rrgnu\Downloads\clientes com mais de 1 nota fiscal.xlsx")))
            //{
            //    var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
            //    var totalRows = myWorksheet.Dimension.End.Row;
            //    var totalColumns = myWorksheet.Dimension.End.Column;

            //    var sb = new StringBuilder(); //this is your data
            //    for (int rowNum = 1; rowNum <= totalRows; rowNum++) //select starting row here
            //    {
            //        var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
            //        sb.AppendLine(string.Join(",", row));
            //    }
            //}

            //


            sDuplicados = File.ReadAllText(@"C:\InterplaceLog\duplicados\relDuplicados.txt");


            sTexto = "";
            sLinha = sDuplicados.Split('\n');
            foreach (string sL in sLinha)
            {

                if (sL.Contains("DUPLICADO"))
                {
                    if (sTexto != "")
                    {
                        sTexto += "\r\n";
                    }
                    if (sL == "")
                    {
                        continue;
                    }







                }
            }

        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            List<ClienteFortPlus> oCliente = ClasseFuncoes.RetornaClienteFortPlus();



            List<ClienteFortPlus> lstCliente = oCliente.Where(x => x.psCodigo == editCliente.Text).ToList();
            ClienteFortPlus oClienteTemp = lstCliente[0];

            IRestResponse oResposta = null;
            oResposta = null;
            int i = 0;
            RestClient client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Cliente/Id/" + oClienteTemp.id.ToString().Trim());
            RestRequest request = new RestRequest(Method.DELETE);
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            if (ClasseParametros.oJsonFortePluslogin == null)
                ClasseFuncoes.ConectaForteplus(5);
            request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);
            oResposta = client.Execute(request);

            if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                ClasseFuncoes.ConectaForteplus(5);
            }




















        }

        private void button4_Click(object sender, EventArgs e)
        {

            ExecuteGetFiscalDocumentsAsync();

        }

        public static async Task ExecuteGetFiscalDocumentsAsync()
        {
            string sDataOntem = DateTime.Now.AddDays(-4).ToString("dd/MM/yyyy");
            string sDataAgora = DateTime.Now.ToString("dd/MM/yyyy");

            int i = 1;

            while (true)
            {

                var request = HttpWebRequest.Create(@"https://bling.com.br/Api/v2/notasfiscais/page=" + i.ToString() + "/json&apikey=" + ClasseParametros.sTokenBling + "&filters=dataEmissao[" + sDataOntem + " 00:00:00 TO " + sDataAgora + " 23:59:59]; situacao[6]");
                request.ContentType = "application/json";
                request.Method = "GET";
                string sNotas = "";
                var oNotas = "";
                int iCodigoCliente = 5;
                string sSql = "";
                Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        Console.Out.WriteLine("Error. Server returned status code: {0}", response.StatusCode);

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(content))
                            Console.Out.WriteLine("Empty Response");
                        else
                            sNotas = content;
                    }
                }

                NotaFiscaljsonBling oListXML = Newtonsoft.Json.JsonConvert.DeserializeObject<NotaFiscaljsonBling>(sNotas);

                if (oListXML.retorno.notasfiscais == null)
                {
                    break;
                }

                foreach (Notasfiscai oNotaFiscal in oListXML.retorno.notasfiscais)
                {
                    if (int.Parse(oNotaFiscal.notafiscal.numero) < 597)
                    {
                        continue;
                    }


                    if (int.Parse(oNotaFiscal.notafiscal.numero) == 282)
                    {

                    }
                    Console.WriteLine(" Gerando Etiqueta do Pedido " + oNotaFiscal.notafiscal.numero + " - Referente ao Pedido Mercado Livre " + oNotaFiscal.notafiscal.numeroPedidoLoja);
                    DataTable d = null;
                    try
                    {
                        d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oNotaFiscal.notafiscal.chaveAcesso + "'");

                        if (d.Rows.Count == 0)
                        {
                            //https://www.bling.com.br/relatorios/nfe.xml.php&apikey=e61079a5031cc2b5065c5bc34b46b03a4983b34e694f7203f2f71b5cddaba2e9950bb934&chaveAcesso=32201034036601000338550020000001461303144558
                            request = HttpWebRequest.Create(@"https://www.bling.com.br/relatorios/nfe.xml.php?apikey=" + ClasseParametros.sTokenBling + "&chaveAcesso=" + oNotaFiscal.notafiscal.chaveAcesso);
                            request.ContentType = "application/json";
                            request.Method = "GET";

                            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                            {
                                if (response.StatusCode != HttpStatusCode.OK)
                                    Console.Out.WriteLine("Error. Server returned status code: {0}", response.StatusCode);

                                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                                {
                                    var content = reader.ReadToEnd();
                                    if (string.IsNullOrWhiteSpace(content))
                                        Console.Out.WriteLine("Empty Response");
                                    else
                                        sNotas = content;
                                }
                            }

                            string sPasta = Directory.GetCurrentDirectory() + "\\temp";
                            if (!Directory.Exists(sPasta))
                            {
                                Directory.CreateDirectory(sPasta);
                            }
                            string sArquivoXML = sPasta + "\\" + oNotaFiscal.notafiscal.chaveAcesso + ".xml";

                            File.WriteAllText(sPasta + "\\" + oNotaFiscal.notafiscal.chaveAcesso + ".xml", sNotas);


                            DanfeViewModel oDanfe = DanfeViewModelCreator.CriarDeStringXml(sNotas);

                            string sUsuarioFTP = "2eletro-varejo";
                            string sSenhaFTP = "#2eletro001#";
                            string sPastaFTP = "ftp://serrapark.dd.spiritlinux.com/04-EnvioSaida/";

                            using (var client = new WebClient())
                            {
                                client.Credentials = new NetworkCredential(sUsuarioFTP, sSenhaFTP);
                                client.UploadFile(sPastaFTP + oNotaFiscal.notafiscal.chaveAcesso + ".xml", WebRequestMethods.Ftp.UploadFile, sArquivoXML);
                            }

                            sSql = "INSERT INTO VENDAS(NOTAFISCAL) VALUES(@NOTAFISCAL)";

                            ParametrosSQL.Add("NOTAFISCAL", oNotaFiscal.notafiscal.chaveAcesso);




                            ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {

                        d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oNotaFiscal.notafiscal.chaveAcesso + "' AND ETIQUETATXT IS NOT NULL");

                        if (d.Rows.Count == 0)
                        {
                            if (oNotaFiscal.notafiscal.tipoIntegracao == "MercadoLivre")
                            {
                                if (ClasseParametros.oMeli == null)
                                    ClasseFuncoes.ConectaMercadoLivreAsync(iCodigoCliente);

                                IRestResponse oResposta = null;

                                //ClasseFuncoes.ConectaMercadoLivreAsync(iCodigoCliente);

                                List<Parameter> ps = new List<Parameter>();
                                Parameter p = new Parameter();
                                p.Name = "access_token";
                                p.Value = ClasseParametros.oMeli.AccessToken;
                                ps.Add(p);

                                MercadoLivreEntrega oEntrega = null;

                                while (oResposta == null)
                                {


                                    oResposta = ClasseParametros.oMeli.Get("orders/" + oNotaFiscal.notafiscal.numeroPedidoLoja, ps);

                                    Result oPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(oResposta.Content);



                                    if (oResposta.StatusCode == HttpStatusCode.Unauthorized)
                                    {
                                        ClasseFuncoes.ConectaMercadoLivreAsync(iCodigoCliente);
                                    }

                                    if (oResposta.StatusCode == HttpStatusCode.NotFound)
                                    {
                                        oResposta = ClasseParametros.oMeli.Get("orders/" + oNotaFiscal.notafiscal.numeroPedidoLoja, ps);

                                        oPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(oResposta.Content);
                                    }

                                    oResposta = ClasseParametros.oMeli.Get("/shipments/" + oPedido.shipping.id.ToString(), ps);


                                    oEntrega = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreEntrega>(oResposta.Content);
                                    if (oResposta.StatusCode == HttpStatusCode.Unauthorized)
                                    {
                                        ClasseFuncoes.ConectaMercadoLivreAsync(iCodigoCliente);

                                        oResposta = null;
                                    }
                                    else if (oResposta.StatusCode == 0)
                                    {
                                        ClasseFuncoes.ConectaMercadoLivreAsync(iCodigoCliente);


                                        oResposta = null;
                                    }
                                }

                                ps = new List<Parameter>();
                                p = new Parameter();
                                p.Name = "access_token";
                                p.Value = ClasseParametros.oMeli.AccessToken;
                                ps.Add(p);
                                p = new Parameter();
                                p.Name = "shipment_ids";
                                p.Value = oEntrega.id.ToString().Trim();
                                ps.Add(p);
                                p = new Parameter();
                                p.Name = "response_type";
                                p.Value = "zpl2";
                                ps.Add(p);

                                oResposta = null;
                                while (oResposta == null)
                                {
                                    oResposta = ClasseParametros.oMeli.Get("shipment_labels", ps);
                                    if (oResposta.Content.Contains("delivered") || oResposta.Content.Contains("shipped"))
                                    {
                                        ClasseParametros.ExecutabancoMySql("UPDATE VENDAMARKETPLACE SET STATUS = 2 WHERE ID = '" + oNotaFiscal.notafiscal.numeroPedidoLoja + "'");
                                    }
                                    else if (oResposta.StatusCode == HttpStatusCode.Unauthorized)
                                    {
                                        ClasseFuncoes.ConectaMercadoLivreAsync(iCodigoCliente);

                                        oResposta = null;

                                    }
                                }

                                /////shipment_labels?shipment_ids=21527708516&response_type=zpl2&access_token=$ACCESS_TOKEN"
                                if (oResposta.StatusCode == HttpStatusCode.OK)
                                {
                                    ClasseParametros.SalvaEtiqueta(oNotaFiscal.notafiscal.chaveAcesso, oResposta.RawBytes, 5, oNotaFiscal.notafiscal.numeroPedidoLoja, "MELI");
                                    sSql = "SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oNotaFiscal.notafiscal.chaveAcesso + "' AND ETIQUETATXT = ''";
                                    DataTable dtbVendas = ClasseParametros.ConsultaBancoMysql(sSql);
                                    if (dtbVendas.Rows.Count == 0)
                                    {
                                        ClasseParametros.ExecutabancoMySql("UPDATE XMLVENDAS SET STATUS = 1 WHERE NOTAFISCAL = '" + oNotaFiscal.notafiscal.chaveAcesso + "'");
                                        ClasseParametros.ExecutabancoMySql("UPDATE VENDAMARKETPLACE SET STATUS = 2 WHERE ID = '" + oNotaFiscal.notafiscal.numeroPedidoLoja + "'");
                                    }
                                    dtbVendas.Dispose();
                                    //break;
                                }

                            }
                            else if (oNotaFiscal.notafiscal.tipoIntegracao == "SkyHub")
                            {
                                Console.WriteLine("Gera etiqueta do pedido " + oNotaFiscal.notafiscal.numeroPedidoLoja);

                                try
                                {
                                    d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM CLIENTE WHERE CODIGO = 5");
                                    string sKey = d.Rows[0]["KEYB2W"].ToString();
                                    string sUsuario = d.Rows[0]["USUARIOB2W"].ToString();
                                    string sAccount = "0MDxaksT8d";
                                    d.Dispose();

                                    d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oNotaFiscal.notafiscal.chaveAcesso + "' AND ETIQUETATXT = ''");

                                    if (d.Rows.Count > 0)
                                    {
                                        continue;
                                    }


                                    RestClient client = new RestClient("http://api.skyhub.com.br/shipments/b2w");
                                    RestRequest oRequest = new RestRequest(Method.POST);
                                    oRequest.AddHeader("Content-Length", "52");
                                    oRequest.AddHeader("Cache-Control", "no-cache");
                                    oRequest.AddHeader("Accept", "application/json");
                                    oRequest.AddHeader("Content-Type", "application/json");
                                    oRequest.AddHeader("x-Api-Key", sKey);
                                    oRequest.AddHeader("X-User-Email", sUsuario);
                                    oRequest.AddHeader("X-Accountmanager-Key", sAccount);

                                    string sPost = "{\n  \"order_remote_codes\": [\n    \"" + String.Join("", System.Text.RegularExpressions.Regex.Split(oNotaFiscal.notafiscal.numeroPedidoLoja, @"[^\d]")) + "\"\n  ]\n}";
                                    oRequest.AddParameter("application/json", sPost, ParameterType.RequestBody);
                                    IRestResponse response = client.Execute(oRequest);

                                    if (response.Content.Contains("nao tem nota fiscal"))
                                    {
                                        continue;
                                    }

                                    if (response.Content.Contains("nao localizados"))
                                    {
                                        continue;
                                    }

                                    Thread.Sleep(1000);
                                    for (int j = 0; j < 12; j++)
                                    {
                                        if (response.StatusCode == HttpStatusCode.Created)
                                            break;

                                        if (response.StatusCode.ToString() == "422" || response.StatusCode.ToString() == "InternalServerError" || response.StatusCode == HttpStatusCode.GatewayTimeout || response.StatusCode == HttpStatusCode.ServiceUnavailable)
                                        {
                                            Thread.Sleep(10000);
                                            response = client.Execute(oRequest);
                                        }
                                        else if (response.StatusCode.ToString() == "429")
                                        {
                                            Thread.Sleep(20000);
                                            client = new RestClient("http://api.skyhub.com.br/shipments/b2w");
                                            oRequest = new RestRequest(Method.POST);
                                            oRequest.AddHeader("Content-Length", "52");
                                            oRequest.AddHeader("Cache-Control", "no-cache");
                                            oRequest.AddHeader("Accept", "application/json");
                                            oRequest.AddHeader("Content-Type", "application/json");
                                            oRequest.AddHeader("x-Api-Key", sKey);
                                            oRequest.AddHeader("X-User-Email", sUsuario);
                                            oRequest.AddHeader("X-Accountmanager-Key", sAccount);

                                            sPost = "{\n  \"order_remote_codes\": [\n    \"" + String.Join("", System.Text.RegularExpressions.Regex.Split(oNotaFiscal.notafiscal.numeroPedidoLoja, @"[^\d]")) + "\"\n  ]\n}";
                                            oRequest.AddParameter("application/json", sPost, ParameterType.RequestBody);
                                            response = client.Execute(oRequest);
                                            Thread.Sleep(3000);
                                        }
                                        else
                                            break;
                                    }

                                    bool lJaGerada = false;
                                    string sPLP = "";
                                    if (response.Content.Contains("ja agrupada"))
                                    {
                                        B2WResult oPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<B2WResult>(response.Content);
                                        sPLP = oPedido.message.Split(' ')[response.Content.Split(' ').Length - 1];
                                        lJaGerada = true;
                                    }


                                    Thread.Sleep(1000);


                                    if (response.StatusCode == HttpStatusCode.Created || lJaGerada)
                                    {
                                        //sPLP = String.Join("", System.Text.RegularExpressions.Regex.Split(response.Content, @"[^\d]"));
                                        response = null;
                                        while (response == null)
                                        {
                                            client = new RestClient("https://api.skyhub.com.br/shipments/b2w/view?plp_id=" + sPLP);
                                            oRequest = new RestRequest(Method.GET);
                                            oRequest.AddHeader("cache-control", "no-cache");
                                            oRequest.AddHeader("Accept", "application/json");
                                            oRequest.AddHeader("Content-Type", "application/json");
                                            oRequest.AddHeader("x-Api-Key", sKey);
                                            oRequest.AddHeader("X-User-Email", sUsuario);
                                            oRequest.AddHeader("X-Accountmanager-Key", sAccount);

                                            response = client.Execute(oRequest);

                                            if (response.Content.Contains("no Route matched"))
                                            {
                                                Thread.Sleep(3000);
                                                response = null;
                                            }
                                        }

                                        for (int j = 0; j < 12; j++)
                                        {
                                            if (response.StatusCode == HttpStatusCode.OK)
                                                break;

                                            if (response.StatusCode.ToString() == "422" || response.StatusCode.ToString() == "InternalServerError" || response.StatusCode == HttpStatusCode.GatewayTimeout)
                                            {
                                                Thread.Sleep(10000);
                                                response = client.Execute(oRequest);
                                            }

                                            else if (response.StatusCode.ToString() == "429" || response.StatusCode == System.Net.HttpStatusCode.BadGateway || response.StatusCode == System.Net.HttpStatusCode.Forbidden || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                                            {
                                                Thread.Sleep(20000);
                                                client = new RestClient("https://api.skyhub.com.br/shipments/b2w/view?plp_id=" + sPLP);
                                                oRequest = new RestRequest(Method.GET);
                                                oRequest.AddHeader("cache-control", "no-cache");
                                                oRequest.AddHeader("Accept", "application/json");
                                                oRequest.AddHeader("Content-Type", "application/json");
                                                oRequest.AddHeader("x-Api-Key", sKey);
                                                oRequest.AddHeader("X-User-Email", sUsuario);
                                                oRequest.AddHeader("X-Accountmanager-Key", sAccount);

                                                response = client.Execute(oRequest);
                                                Thread.Sleep(3000);
                                            }
                                            else
                                                break;
                                        }

                                        Thread.Sleep(3000);
                                        EtiquetaJSON oJsonB2W = Newtonsoft.Json.JsonConvert.DeserializeObject<EtiquetaJSON>(response.Content);
                                        if (oJsonB2W.docsExternos.Length > 0)
                                            if (oJsonB2W.plp != null)
                                            {
                                                string sEtiqueta = ClasseFuncoes.GeraEtiquetaB2W(oJsonB2W, oNotaFiscal.notafiscal.numeroPedidoLoja.Split('-')[0], "PAC");
                                                byte[] aEtiqueta = Encoding.UTF8.GetBytes(sEtiqueta);
                                                ClasseParametros.SalvaEtiquetaAnyMarket(oNotaFiscal.notafiscal.chaveAcesso, aEtiqueta, oNotaFiscal.notafiscal.numeroPedidoLoja.Split('-')[0], 5, oNotaFiscal.notafiscal.numeroPedidoLoja.Split('-')[0], oNotaFiscal.notafiscal.numeroPedidoLoja.Split('-')[1]);

                                                // salva json
                                                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\json"))
                                                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\json");

                                                File.WriteAllText(Directory.GetCurrentDirectory() + "\\json\\" + oNotaFiscal.notafiscal.chaveAcesso + ".json", response.Content); // Requires System.IO
                                            }

                                    }
                                    d.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);

                                }









                                //try
                                //{
                                //    d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM CLIENTE WHERE CODIGO = 5");
                                //    string sKey = d.Rows[0]["KEYB2W"].ToString();
                                //    string sUsuario = d.Rows[0]["USUARIOB2W"].ToString();
                                //    string sAccount = "0MDxaksT8d";
                                //    d.Dispose();

                                //    RestClient client = new RestClient("http://api.skyhub.com.br/shipments/b2w");
                                //    RestRequest oRequest = new RestRequest(Method.GET);
                                //    oRequest.AddHeader("Content-Length", "52");
                                //    oRequest.AddHeader("Cache-Control", "no-cache");
                                //    oRequest.AddHeader("Accept", "application/json");
                                //    oRequest.AddHeader("Content-Type", "application/json");
                                //    oRequest.AddHeader("x-Api-Key", sKey);
                                //    oRequest.AddHeader("X-User-Email", sUsuario);
                                //    oRequest.AddHeader("X-Accountmanager-Key", sAccount);
                                //    //string sPost = "{\n  \"order_remote_codes\": [\n    \"" + String.Join("", System.Text.RegularExpressions.Regex.Split(oNotaFiscal.notafiscal.numeroPedidoLoja, @"[^\d]")) + "\"\n  ]\n}";
                                //    //oRequest.AddParameter("application/json", sPost, ParameterType.RequestBody);
                                //    IRestResponse response = client.Execute(oRequest);

                                //    B2WPLP oPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<B2WPLP>(response.Content);

                                //    foreach (Plp oPLP in oPedido.plp)
                                //    {

                                //        string sPLP = String.Join("", System.Text.RegularExpressions.Regex.Split(response.Content, @"[^\d]"));
                                //        string sPedido = "";
                                //        response = null;
                                //        while (response == null)
                                //        {
                                //            client = new RestClient("https://api.skyhub.com.br/shipments/b2w/view?plp_id=" + sPLP);
                                //            oRequest = new RestRequest(Method.GET);
                                //            oRequest.AddHeader("cache-control", "no-cache");
                                //            oRequest.AddHeader("Accept", "application/json");
                                //            oRequest.AddHeader("Content-Type", "application/json");
                                //            oRequest.AddHeader("x-Api-Key", sKey);
                                //            oRequest.AddHeader("X-User-Email", sUsuario);
                                //            oRequest.AddHeader("X-Accountmanager-Key", sAccount);

                                //            response = client.Execute(oRequest);

                                //            if (response.Content.Contains("no Route matched"))
                                //            {
                                //                Thread.Sleep(3000);
                                //                response = null;
                                //            }
                                //        }

                                //        for (int j = 0; j < 12; i++)
                                //        {
                                //            if (response.StatusCode == HttpStatusCode.OK)
                                //                break;

                                //            if (response.StatusCode.ToString() == "422" || response.StatusCode.ToString() == "InternalServerError" || response.StatusCode == HttpStatusCode.GatewayTimeout)
                                //            {
                                //                Thread.Sleep(10000);
                                //                response = client.Execute(oRequest);
                                //            }

                                //            else if (response.StatusCode.ToString() == "429" || response.StatusCode == System.Net.HttpStatusCode.BadGateway || response.StatusCode == System.Net.HttpStatusCode.Forbidden || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                                //            {
                                //                Thread.Sleep(20000);
                                //                client = new RestClient("https://api.skyhub.com.br/shipments/b2w/view?plp_id=" + sPLP);
                                //                oRequest = new RestRequest(Method.GET);
                                //                oRequest.AddHeader("cache-control", "no-cache");
                                //                oRequest.AddHeader("Accept", "application/json");
                                //                oRequest.AddHeader("Content-Type", "application/json");
                                //                oRequest.AddHeader("x-Api-Key", sKey);
                                //                oRequest.AddHeader("X-User-Email", sUsuario);
                                //                oRequest.AddHeader("X-Accountmanager-Key", sAccount);

                                //                response = client.Execute(oRequest);
                                //                Thread.Sleep(3000);
                                //            }
                                //            else
                                //                break;
                                //        }

                                //        Thread.Sleep(3000);
                                //        EtiquetaJSON oJsonB2W = Newtonsoft.Json.JsonConvert.DeserializeObject<EtiquetaJSON>(response.Content);
                                //        if (oJsonB2W.docsExternos.Length > 0)
                                //            if (oJsonB2W.plp != null)
                                //            {
                                //                string sEtiqueta = ClasseFuncoes.GeraEtiquetaB2W(oJsonB2W, sPedido.Split('-')[0], "PAC");
                                //                byte[] aEtiqueta = Encoding.UTF8.GetBytes(sEtiqueta);
                                //                ClasseParametros.SalvaEtiquetaAnyMarket(oNotaFiscal.notafiscal.chaveAcesso, aEtiqueta, sPedido.Split('-')[0], 5, sPedido.Split('-')[0], sPedido.Split('-')[1]);

                                //                // salva json
                                //                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\json"))
                                //                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\json");

                                //                File.WriteAllText(Directory.GetCurrentDirectory() + "\\json\\" + oNotaFiscal.notafiscal.chaveAcesso + ".json", response.Content); // Requires System.IO
                                //            }
                                //    }
                                //}
                                //catch (Exception ex)
                                //{

                                //}








                            }
                            else if (oNotaFiscal.notafiscal.tipoIntegracao == "IntegraCommerce")
                            {
                                try
                                {
                                    ClasseFuncoes.RetornaCodigoMAGALU(iCodigoCliente);

                                    //Os possíveis status são: New, Approved, Processing, Invoiced, Shipped, Delivered, Canceled, ShipmentException.
                                    RestClient client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order/" + oNotaFiscal.notafiscal.numeroPedidoLoja);
                                    RestRequest oRequestMAGALU = new RestRequest(Method.GET);
                                    oRequestMAGALU.AddHeader("cache-control", "no-cache");
                                    oRequestMAGALU.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                                    IRestResponse oResposta = client.Execute(oRequestMAGALU);

                                    Thread.Sleep(3000);
                                    InterRegraNegocio.Order oPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<InterRegraNegocio.Order>(oResposta.Content);

                                    if (oPedido == null)
                                    {
                                        continue;
                                    }
                                    DataTable dtbChaves = ClasseParametros.ConsultaBancoMysql("SELECT USUARIOMAGALU,SENHAMAGALU FROM CLIENTE WHERE CODIGO = 5");


                                    Console.WriteLine("Gerando etiqueta do pedido " + oPedido.IdOrder);

                                    if (oPedido.IdOrder == "LU-8504500673851491")
                                    {

                                    }

                                    d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oPedido.InvoicedKey + "' AND ETIQUETATXT = ''");
                                    if (d.Rows.Count > 0)
                                        continue;

                                    try
                                    {
                                        // Gera tracking
                                        oPedido.CodigoCliente = 5;
                                        ClasseParametros.SalvaBancoPDFZPLMagalu(oPedido, dtbChaves.Rows[0]["USUARIOMAGALU"].ToString(), dtbChaves.Rows[0]["SENHAMAGALU"].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        ClasseParametros.MostraErro(ex.Message, ClasseParametros.iconApp);
                                    }

                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                    }






                    //if (d.Rows.Count > 0)
                    //{
                    //    sSql = "UPDATE VENDAS SET ETIQUETATXTTXT = @ETIQUETATXTTXT, ETIQUETATXT=@ETQ,LOJA=@LOJA,CODIGOCLIENTE=@CODIGOCLIENTE WHERE NOTAFISCAL = @NOTA";
                    //}
                    //else
                    //{
                    //    sSql = "INSERT INTO VENDAS(NOTAFISCAL,ETIQUETATXT,ETIQUETATXTTXT,LOJA,DATACRIADO,LOTE,CODIGOCLIENTE) VALUES(@NOTA,@ETQ,@ETIQUETATXTTXT,@LOJA,@DATACRIADO,@LOTE,@CODIGOCLIENTE)";
                    //    ParametrosSQL.Add("@DATACRIADO", DateTime.Now);
                    //    ParametrosSQL.Add("@LOTE", ClasseParametros.PegaLote("MELI", "5".ToString()));
                    //}
                    //d.Dispose();

                    //File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLETQ\\" + oNotaFiscal.notafiscal.chaveAcesso + ".TXT", aEtiqueta); // Requires System.IO

                    //ParametrosSQL.Add("@ETQ", aEtiqueta);
                    //ParametrosSQL.Add("@ETIQUETATXTTXT", sEtiqueta);
                    //ParametrosSQL.Add("@NOTA", Path.GetFileName(sArquivo).ToLower().Replace(".json", "").Replace(".txt", "").Replace(".pdf", "").Split('_')[0]);
                    //ParametrosSQL.Add("@LOJA", Path.GetFileName(sArquivo).ToLower().Replace(".json", "").Replace(".txt", "").Replace(".pdf", "").Split('_')[1]);
                    //ParametrosSQL.Add("@CODIGOCLIENTE", iCliente);
                    //ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);

                }



                i++;
            }

        }


    }
}
