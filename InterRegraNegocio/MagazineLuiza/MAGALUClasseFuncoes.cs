using DanfeSharp.Modelo;
using interRegraNegocio.FortePlus;
using InterRegraNegocio.FortePlus;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace InterRegraNegocio.MagazineLuiza
{
    public class MAGALUClasseFuncoes
    {
        public static ProdutoComplemento RetornaProdutoComplementoFortPlusPorSKU(string sSKU, int? iMarketplace = -1)
        {
            ProdutoComplemento oRetonro = null;
            IRestResponse oResposta = null;
            while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
            {
                RestClient client = new RestClient(ClasseParametros.sURlFortPlus + "/api/ProdutoComplemento");
                RestRequest request = new RestRequest(Method.GET);
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

            List<ProdutoComplemento> oListFormaPagamento = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(oResposta.Content);
            List<ProdutoComplemento> oListFormaPagamentoFiltrado = new List<ProdutoComplemento>();

            try
            {
                if (iMarketplace > -1)
                    oListFormaPagamentoFiltrado = oListFormaPagamento.Where(x => x.cmCodigo == sSKU && x.cmIdMarketPlace == iMarketplace).ToList();
                else
                    oListFormaPagamentoFiltrado = oListFormaPagamento.Where(x => x.cmCodigo == sSKU).ToList();

                foreach (ProdutoComplemento oProduto in oListFormaPagamentoFiltrado)
                {
                    //string sMarketplace = ClasseFuncoes.RetornaNomeGlobalMK("MK", oProduto.cmIdMarketPlace);
                    //if (sMarketplace == "MAGAZINE LUIZA")
                    //{
                    oListFormaPagamentoFiltrado = oListFormaPagamento.Where(x => x.id == oProduto.id).ToList();
                    //}
                }

            }
            catch (Exception ex)
            {

            }

            if (oListFormaPagamentoFiltrado.Count > 0)
                oRetonro = oListFormaPagamentoFiltrado[0];

            return oRetonro;
        }


        public static void IntegraManual(string sPedido)
        {
            ClasseFuncoes.SalvaLogServicos("Integra pedidos MAGALU");
            IRestResponse oResposta = null;
            List<RetornoPedido> lstRetornoPedidoslidos = new List<RetornoPedido>();

            List<Order> lstPedidoIntegracao = new List<Order>();
            var client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order/" + sPedido);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
            oResposta = client.Execute(request);
            Thread.Sleep(1000);
            Order oPedidoORder = Newtonsoft.Json.JsonConvert.DeserializeObject<Order>(oResposta.Content);
            lstPedidoIntegracao.Add(oPedidoORder);

            int? iMarketplace = ClasseFuncoes.RetornaCodigoGlobal("MK", "MAGAZINE LUIZA");


            int iOffset = 0;
            int ilimit = 50;

            //List<Result> lstPedidosMercadoLivre = new List<Result>();
            bool lContinua = true;


            if (lstPedidoIntegracao.Count > 0)
            {
                bool lAtualiza = false;
                foreach (Order oPedido in lstPedidoIntegracao)
                {
                    if (oPedido.IdOrder.ToString().Trim() == "2341088442")
                    {

                    }

                    if (oPedido.OrderStatus.ToUpper().Trim() == "APPROVED")
                    {
                        string sSql = "SELECT TRIM(ID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(ID) = '" + oPedido.IdOrder.ToString().Trim() + "' AND MARKETPLACE = 'MAGAZINE LUIZA'";
                        DataTable d = ClasseParametros.ConsultaBancoMysql(sSql);
                        if (d.Rows.Count == 0)
                        {
                            string sPack = "";
                            if (oPedido.IdOrder != null)
                                sPack = oPedido.IdOrder.ToString();

                            sSql = "INSERT INTO VENDAMARKETPLACE(ID,STATUS,MARKETPLACE, DATA,STATUSMENSAGEM,PACKID,USERID,EMAILML,PEDIDOML,SELLERID) " +
                                "VALUES('" + oPedido.IdOrder.ToString().Trim() + "',0,'MAGAZINE LUIZA',CURDATE(),0,'" + sPack + "', " +
                                "'" + oPedido.CodigoCliente.ToString() + "','" + oPedido.CustomerMail + "','" + oPedido.IdOrder + "','2ELETRO')";
                            ClasseParametros.ExecutabancoMySql(sSql);
                        }
                    }
                }

                foreach (Order oPedido in lstPedidoIntegracao)
                {
                    if (oPedido.IdOrder.ToString().Trim() == "2334214096")
                    {

                    }

                    string sSql = "SELECT TRIM(ID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(ID) = '" + oPedido.IdOrder.ToString().Trim() + "' AND STATUS = 0 AND MARKETPLACE = 'MAGAZINE LUIZA'";
                    DataTable d = ClasseParametros.ConsultaBancoMysql(sSql);
                    if (d.Rows.Count > 0)
                    {
                        if (oPedido.OrderStatus.ToUpper().Trim() == "APPROVED")
                        {
                            ClienteFortPlus oCliente = ClasseFuncoes.CadastraClienteMAGALUFortPlus(oPedido);
                            string s = Newtonsoft.Json.JsonConvert.SerializeObject(oCliente);

                            if (oCliente == null)
                            {
                                continue;
                            }

                            Pedido oPedidoFortPlus = new Pedido();

                            Guid oGuid = Guid.NewGuid();
                            oPedidoFortPlus.id = 0;
                            oPedidoFortPlus.mvDocto = 0;
                            oPedidoFortPlus.mvIdPessoa = int.Parse(oCliente.id);
                            string sCNPJ = "";
                            oPedidoFortPlus.mvIdVendedor = ClasseFuncoes.RetornaVendedorFortPlus("MAGAZINE LUIZA");
                            oPedidoFortPlus.mvIdSerie = ClasseFuncoes.RetornaCodigoGlobal("SR", "1");
                            oPedidoFortPlus.mvIdModelo = ClasseFuncoes.RetornaCodigoGlobal("MD", "55");
                            oPedidoFortPlus.mvTipoMovimento = "1";
                            oPedidoFortPlus.mvTipoPedido = "P";
                            oPedidoFortPlus.mvIdTipoDocumento = ClasseFuncoes.RetornaCodigoGlobal("TD", "REC");

                            oPedidoFortPlus.mvIdTipoFrete = ClasseFuncoes.RetornaCodigoGlobal("TF", "9");
                            if (float.Parse(oPedido.TotalFreight) > 0)
                            {
                                oPedidoFortPlus.mvIdTipoFrete = ClasseFuncoes.RetornaCodigoGlobal("TF", "1");
                            }

                            oPedidoFortPlus.mvPreNota = "N";
                            oPedidoFortPlus.mvFinNf = "1";
                            oPedidoFortPlus.mvPresenca = ClasseFuncoes.RetornaCodigoParametro("_FPS_NFE_PRESENCA");
                            oPedidoFortPlus.mvIdNatureza = ClasseFuncoes.RetornaCodigoGlobal("NO", "01");
                            oPedidoFortPlus.mvIdParent = null;
                            oPedidoFortPlus.idFilial = ClasseParametros.iFilial;
                            oPedidoFortPlus.mvIdTransportadora = ClasseFuncoes.RetornaTransportadorFortPlus("MAGAZINE LUIZA");

                            int? eTotal = 0;
                            float? eTotalPago = 0;
                            float? eTotalValor = 0;
                            float? eTotalDesconto = float.Parse(oPedido.TotalTax);

                            foreach (ProductMagalu o in oPedido.Products)
                            {
                                eTotal += o.Quantity;
                                string sPreco = o.Price.ToString().Insert(o.Price.ToString().Length - 2, ",");

                                eTotalValor += (float)(o.Quantity * float.Parse(sPreco));
                            }


                            s = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoFortPlus);
                            List<Payment> lstPagamento = oPedido.Payments.OfType<Payment>().ToList();
                            foreach (Payment oPagamento in lstPagamento)
                            {
                                eTotalPago += (float)oPagamento.Amount;
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
                            oPedidoFortPlus.mvValorOutrasDespesasAcessoria = float.Parse(oPedido.TotalTax);

                            oPedidoFortPlus.mvIdExterno = oPedido.IdOrder.ToString();
                            if (oPedido.TotalFreight != "")
                                oPedidoFortPlus.mvValorFrete = float.Parse(oPedido.TotalFreight);
                            oPedidoFortPlus.mvValorDesconto = 0;

                            oPedidoFortPlus.mvValorTotalProduto = eTotalValor;
                            //oPedidoFortPlus.valo = oPedido.total_amount;
                            oPedidoFortPlus.mvValorTotal = eTotalPago;
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
                                client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/IdExterno/" + oPedido.IdOrder.ToString());
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

                            foreach (Payment oPagamento in lstPagamento)
                            {

                                FortPlusFinanceiroReduzido oFinanceiro = new FortPlusFinanceiroReduzido();
                                oFinanceiro.email = "rodrigonunes@2eletro.com.br";
                                oFinanceiro.idFilial = oPedidoFortPlus.idFilial;
                                oFinanceiro.idMovto = oPedidoFortPlus.id;
                                oFinanceiro.idFormaPagamento = ClasseFuncoes.RetornaCodigoFormaPagamento(oPagamento.Name);
                                oFinanceiro.idCondicaoPagamento = ClasseFuncoes.RetornaCodigoCondicaoPagamento("À VISTA");
                                oFinanceiro.valor = (double)oPagamento.Amount;
                                oFinanceiro = ClasseFuncoes.CadastraFinanceiroReduzido(oFinanceiro);

                            }

                            int iLocalEstoque = 33;

                            try
                            {
                                foreach (ProductMagalu o in oPedido.Products)
                                {
                                    string sLast = o.IdSku.Substring(o.IdSku.Length - 5);
                                    string sInicio = o.IdSku.Replace(sLast, "");
                                    string sProduto = "";
                                    if (sLast.Substring(sLast.Length - 3, 3) == "VAR")
                                    {
                                        sLast = sLast.Replace("VAR", "");

                                        string[] aProduto = sLast.Split('G');
                                        sProduto = sInicio + aProduto[0];
                                    }
                                    else
                                    {
                                        sProduto = o.IdSku;
                                    }


                                    ProdutoComplemento oProdutoComplemento = RetornaProdutoComplementoFortPlusPorSKU(sProduto, iMarketplace);

                                    ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComplemento.cmIdProduto.ToString()).Content);

                                    if (oProduto.prCodigo.ToString().Contains("MICFAC"))
                                    {
                                        iLocalEstoque = 31;
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ClasseFuncoes.SalvaLogServicos(ex.Message);
                                return;
                            }

                            List<ProdutoFortePlus> lstProduto = new List<ProdutoFortePlus>();
                            float? eValorFreteProduto = oPedidoFortPlus.mvValorFrete / oPedido.Products.Length;

                            foreach (ProductMagalu o in oPedido.Products)
                            {
                                string sLast = o.IdSku.Substring(o.IdSku.Length - 5);
                                string sInicio = o.IdSku.Replace(sLast, "");
                                string sProduto = "";
                                if (sLast.Substring(sLast.Length - 3, 3) == "VAR")
                                {
                                    sLast = sLast.Replace("VAR", "");

                                    string[] aProduto = sLast.Split('G');
                                    sProduto = sInicio + aProduto[0];
                                }
                                else
                                {
                                    sProduto = o.IdSku;
                                }


                                ProdutoComplemento oProdutoComplemento = RetornaProdutoComplementoFortPlusPorSKU(sProduto);
                                ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComplemento.cmIdProduto.ToString()).Content);
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
                                            if (sMarketPlace.Contains("MAGAZINE LUIZA"))
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
                                        double? eQuantidade = o.Quantity * oFormaPagamento.pcQtde;
                                        float? eQtd = float.Parse(eQuantidade.ToString());
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
                                        float? ePercentual = (oProdutoComplementoUsar.cmPrecoDePor * 100) / eValorTotalComposicao;
                                        double? eValorProduto = (eTotalValor * ePercentual) / 100;

                                        oItemPedido.mtIdProduto = oFormaPagamento.pcIdProdutoComposicao;
                                        oItemPedido.mtQtde = eQtd;
                                        oItemPedido.mtValorUnitario = float.Parse(eValorProduto.ToString());
                                        oItemPedido.mtValorTotal = oItemPedido.mtValorUnitario * eQtd;

                                        ////mais de uma unidade
                                        ////04_02_2020
                                        if (oPedido.CustomerPfCpf != "CPF")
                                        {
                                            ePercentual = (oProdutoComplementoUsar.cmPrecoDePor * 100) / eValorTotalComposicao;
                                            eValorProduto = (eTotalSemTarifa * ePercentual) / 100;

                                            if (oListFormaPagamento.Count == 1)
                                            {
                                                eValorProduto = eTotalSemTarifa / eQtd;
                                            }

                                            oItemPedido.mtValorUnitario = float.Parse(eValorProduto.ToString());
                                            oItemPedido.mtValorTotal = oItemPedido.mtValorUnitario * eQtd;
                                        }

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
                                    oItemPedido.mtQtde = o.Quantity;

                                    string sPreco = o.Price.ToString().Insert(o.Price.ToString().Length - 2, ",");

                                    oItemPedido.mtValorUnitario = float.Parse(sPreco);
                                    oItemPedido.mtValorTotal = (float)(o.Quantity * float.Parse(sPreco));

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
                            sSql = "UPDATE VENDAMARKETPLACE SET STATUS = 1, QUANTIDADEACIMA = 0 WHERE TRIM(ID) ='" + oPedido.IdOrder.ToString().Trim() + "'";
                            ClasseParametros.ExecutabancoMySql(sSql);


                            //foreach (ProdutoFortePlus oProduto in lstProduto)
                            //{
                            //    ClasseFuncoes.EnviaProdutosMercadoLivre(oProduto.id.ToString());
                            //}


                            if (eTotal > 1)
                            {
                                sSql = "UPDATE VENDAMARKETPLACE SET STATUSMENSAGEM = 3, QUANTIDADEACIMA = 1 WHERE TRIM(ID) ='" + oPedido.IdOrder.ToString().Trim() + "'";
                                ClasseParametros.ExecutabancoMySql(sSql);
                            }

                            string smensagem = string.Format(ClasseParametros.sMensagemAcabouComprar, oCliente.psNome);

                            string sPack = oPedido.IdOrder.ToString();
                            if (oPedido.IdOrder != null)
                                sPack = oPedido.IdOrder.ToString();

                            // EnviaMensagemMercadoLivre(sPack, oPedido.seller.id.ToString(), oPedido.buyer.id.ToString(), smensagem, "1", oPedido.id.ToString());

                            d.Dispose();
                            ClasseFuncoes.AtualizaDadosFortPlus(oPedidoFortPlus.id);
                            ClasseFuncoes.AtualizaDadosFortPlus(oPedidoFortPlus.id);

                            sSql = "SELECT TRIM(ID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(ID) = '" + oPedido.IdOrder.ToString().Trim() + "' AND MARKETPLACE = 'MAGAZINE LUIZA'";
                            d = ClasseParametros.ConsultaBancoMysql(sSql);
                            if (d.Rows.Count > 0)
                            {
                                // Atualiza pedido para MAGALU
                                #region Marca pedido como processado
                                MAGALUPedidoProcessado oPedidoProcessado = new MAGALUPedidoProcessado();
                                oPedidoProcessado.IdOrder = oPedido.IdOrder;
                                oPedidoProcessado.OrderStatus = "PROCESSING";

                                string sJsonProcessado = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoProcessado);

                                client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order");
                                request = new RestRequest(Method.PUT);
                                request.AddHeader("cache-control", "no-cache");
                                request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                                request.AddParameter("application/json", sJsonProcessado, ParameterType.RequestBody);

                                IRestResponse response = client.Execute(request);
                                #endregion
                            }
                            d.Dispose();
                        }
                    }
                }
            }
        }

        public static void RecebePedidoMAGALUEnviaFortPlus(int iCodigoCliente)
        {
            try
            {
                ClasseFuncoes.SalvaLogServicos("Integra pedidos MAGALU");


                int iLocalEstoque32 = -1;
                //var client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/OrderQueue?Status=APPROVED");
                var client = new RestClient("https://in.integracommerce.com.br/api/OrderQueue?Status=APPROVED");
                //var client = new RestClient(ClasseParametros.sEnderecoMagalu+ "/api/Order?page=1&perPage=50&status=APPROVED&startDate=2020-06-15%2000%3A00%3A00&endDate=2020-06-17%2000%3A00%3A00");

                if (ClasseParametros.sTokenMAGALU == "")
                    ClasseFuncoes.RetornaCodigoMAGALU(iCodigoCliente);

                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                IRestResponse oResposta = client.Execute(request);
                Thread.Sleep(1000);
                MAGALUOrderQueue oNovosPedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<MAGALUOrderQueue>(oResposta.Content);
                List<RetornoPedido> lstRetornoPedidoslidos = new List<RetornoPedido>();
                int? iMarketplace = ClasseFuncoes.RetornaCodigoGlobal("MK", "MAG");

                #region add pedidos lidos, excluir depois de integrado

                //oNovosPedidos.OrderQueues = new Orderqueue[8];

                //Orderqueue oP0 = new Orderqueue();
                //oP0.IdOrder = "PEDIDO-TESTE03073127";
                //oP0.IdOrderMarketplace = "PEDIDO-TESTE03073127";
                //oNovosPedidos.OrderQueues[0] = oP0;

                //Orderqueue oP1 = new Orderqueue();
                //oP1.IdOrder = "PEDIDO-TESTE03073129";
                //oP1.IdOrderMarketplace = "PEDIDO-TESTE03073129";
                //oNovosPedidos.OrderQueues[1] = oP1;

                //Orderqueue oP2 = new Orderqueue();
                //oP2.IdOrder = "PEDIDO-TESTE03073128";
                //oP2.IdOrderMarketplace = "PEDIDO-TESTE03073128";
                //oNovosPedidos.OrderQueues[2] = oP2;

                //Orderqueue oP3 = new Orderqueue();
                //oP3.IdOrder = "PEDIDO-TESTE03073126";
                //oP3.IdOrderMarketplace = "PEDIDO-TESTE03073126";
                //oNovosPedidos.OrderQueues[3] = oP3;

                //Orderqueue oP4 = new Orderqueue();
                //oP4.IdOrder = "PEDIDO-TESTE03073125";
                //oP4.IdOrderMarketplace = "PEDIDO-TESTE03073125";
                //oNovosPedidos.OrderQueues[4] = oP4;

                //Orderqueue oP5 = new Orderqueue();
                //oP5.IdOrder = "PEDIDO-TESTE03073124";
                //oP5.IdOrderMarketplace = "PEDIDO-TESTE03073124";
                //oNovosPedidos.OrderQueues[5] = oP5;

                //Orderqueue oP6 = new Orderqueue();
                //oP6.IdOrder = "PEDIDO-TESTE03073122";
                //oP6.IdOrderMarketplace = "PEDIDO-TESTE03073122";
                //oNovosPedidos.OrderQueues[6] = oP6;

                //Orderqueue oP7 = new Orderqueue();
                //oP7.IdOrder = "PEDIDO-TESTE03073121";
                //oP7.IdOrderMarketplace = "PEDIDO-TESTE03073121";
                //oNovosPedidos.OrderQueues[7] = oP7;

                //oNovosPedidos.Total = 8;
                #endregion

                //var client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order/" + oNovosPedidos.OrderQueues[i].IdOrder);

                //MAGALUOrderQueue oNovosPedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<MAGALUOrderQueue>(oResposta.Content);
                //oNovosPedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPedidoMagalu>(oResposta.Content);
                List<Order> lstPedidoIntegracao = new List<Order>();
                string sPedido = "LU-8553500689741475";

                for (int i = 0; i < oNovosPedidos.Total; i++)
                {
                    try
                    {
                        //client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order/" + oNovosPedidos.OrderQueues[i].IdOrder);
                        sPedido = oNovosPedidos.OrderQueues[i].IdOrder;
                        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order/" + sPedido);
                        request = new RestRequest(Method.GET);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                        oResposta = client.Execute(request);
                        Thread.Sleep(1000);
                        Order oPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<Order>(oResposta.Content);
                        oPedido.IdQueue = oNovosPedidos.OrderQueues[i].Id;
                        lstPedidoIntegracao.Add(oPedido);
                    }
                    catch { }
                }

                int iOffset = 0;
                int ilimit = 50;

                //List<Result> lstPedidosMercadoLivre = new List<Result>();
                bool lContinua = true;

                Console.WriteLine(" Salvando pedidos no banco");
                if (lstPedidoIntegracao.Count > 0)
                {
                    bool lAtualiza = false;
                    foreach (Order oPedido in lstPedidoIntegracao)
                    {


                        if (oPedido.IdOrder.ToString().Trim() == "2341088442")
                        {

                        }

                        if (oPedido.OrderStatus.ToUpper().Trim() == "APPROVED")
                        {
                            string sSql = "SELECT TRIM(ID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(ID) = '" + oPedido.IdOrder.ToString().Trim() + "' AND MARKETPLACE = 'MAGAZINE LUIZA'";
                            DataTable d = ClasseParametros.ConsultaBancoMysql(sSql);
                            if (d.Rows.Count == 0)
                            {
                                string sPack = "";
                                if (oPedido.IdOrder != null)
                                    sPack = oPedido.IdOrder.ToString();

                                sSql = "INSERT INTO VENDAMARKETPLACE(ID,STATUS,MARKETPLACE, DATA,STATUSMENSAGEM,PACKID,USERID,EMAILML,PEDIDOML,SELLERID) " +
                                    "VALUES('" + oPedido.IdOrder.ToString().Trim() + "',0,'MAGAZINE LUIZA',CURDATE(),0,'" + sPack + "', " +
                                    "'" + oPedido.CodigoCliente.ToString() + "','" + oPedido.CustomerMail + "','" + oPedido.IdOrder + "','2ELETRO')";
                                ClasseParametros.ExecutabancoMySql(sSql);
                            }
                        }
                        else
                        {
                            // Atualiza pedido para MAGALU
                            Thread.Sleep(1000);
                            #region Marca pedido como processado no queue

                            string sId = "[{\"Id\": \"" + oPedido.IdQueue.ToString() + "\"}]";

                            client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/OrderQueue");
                            request = new RestRequest(Method.PUT);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                            request.AddParameter("application/json", sId, ParameterType.RequestBody);

                            oResposta = client.Execute(request);

                            #endregion
                        }
                    }

                    foreach (Order oPedido in lstPedidoIntegracao)
                    {
                        try
                        {
                            Console.WriteLine(" Integrando pedido " + oPedido.IdOrder);
                            if (oPedido.IdOrder.ToString().Trim() == "2334214096")
                            {

                            }

                            string sSql = "SELECT TRIM(ID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(ID) = '" + oPedido.IdOrder.ToString().Trim() + "' AND STATUS = 0 AND MARKETPLACE = 'MAGAZINE LUIZA'";
                            DataTable d = ClasseParametros.ConsultaBancoMysql(sSql);
                            if (d.Rows.Count > 0)
                            {
                                if (oPedido.OrderStatus.ToUpper().Trim() == "APPROVED")
                                {
                                    ClienteFortPlus oCliente = ClasseFuncoes.CadastraClienteMAGALUFortPlus(oPedido);
                                    string s = Newtonsoft.Json.JsonConvert.SerializeObject(oCliente);

                                    if (oCliente == null)
                                    {
                                        //EnviaMensagemTelegramAsync("Pedido MAGALU não cadastrado pois o endereço está com erro!\n" + oPedido.buyer.first_name + " " + oPedido.buyer.last_name, "0,1", "Pedido não cadastrado", sCodigoUsado.ToString().Trim());

                                        continue;
                                    }


                                    int iLocalEstoque = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "32");
                                    ClasseFuncoes.CarregaFiliais("34.036.601/0003-38 - 2ELETRO VAREJISTA");

                                    //34036601000257 - 2ELETRO ATACADISTA	
                                    //34036601000338 - 2ELETRO VAREJISTA

                                    string sProduto = "";
                                    try
                                    {

                                        foreach (ProductMagalu oItem in oPedido.Products)
                                        {
                                            string sLast = oItem.IdSku.Substring(oItem.IdSku.Length - 5);
                                            string sInicio = oItem.IdSku.Replace(sLast, "");
                                            string sProdutoTemp = "";

                                            if (sLast.Substring(sLast.Length - 3, 3) == "VAR")
                                            {
                                                sLast = sLast.Replace("VAR", "");

                                                string[] aProduto = sLast.Split('G');
                                                sProdutoTemp = sInicio + aProduto[0];
                                            }
                                            else
                                            {
                                                sProdutoTemp = oItem.IdSku;
                                            }

                                            ProdutoComplemento oProdutoComplemento = RetornaProdutoComplementoFortPlusPorSKU(sProdutoTemp, iMarketplace);
                                            ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComplemento.cmIdProduto.ToString()).Content);

                                            if (sProdutoTemp.Contains("KIT"))
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

                                                foreach (FortPlusProdutoComposicao oProdutoComposicao in oListFormaPagamento)
                                                {
                                                    ProdutoFortePlus oProdutoTemp = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComposicao.pcIdProdutoComposicao.ToString()).Content);


                                                    ProdutoComplemento oProdutoComplementoUsar = new ProdutoComplemento();
                                                    int iLocalEstoque34 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "34");
                                                    iLocalEstoque32 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "32");
                                                    double eQtdEstoque = ClasseFuncoes.RetornaSeTemEstoque(oProdutoComposicao.pcIdProdutoComposicao.ToString(), iLocalEstoque34);
                                                    eQtdEstoque += ClasseFuncoes.RetornaSeTemEstoque(oProdutoComposicao.pcIdProdutoComposicao.ToString(), iLocalEstoque32);

                                                    if (oItem.Quantity > eQtdEstoque)
                                                    {
                                                        iLocalEstoque = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "33");
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        iLocalEstoque = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "32");
                                                    }
                                                }
                                            }
                                            else
                                            {

                                                int iLocalEstoque34 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "34");
                                                iLocalEstoque32 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "32");
                                                double eQtdEstoque = ClasseFuncoes.RetornaSeTemEstoque(oProduto.id.ToString(), iLocalEstoque34);
                                                eQtdEstoque += ClasseFuncoes.RetornaSeTemEstoque(oProduto.id.ToString(), iLocalEstoque32);

                                                if (oItem.Quantity > eQtdEstoque)
                                                {
                                                    iLocalEstoque = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "33");
                                                    break;
                                                }
                                                else
                                                {
                                                    DataTable dtbProdutoOrla = ClasseParametros.ConsultaBancoMysql("SELECT * FROM PRODUTOSORLA WHERE SKU = '" + sProdutoTemp + "'");

                                                    if (dtbProdutoOrla.Rows.Count > 0)
                                                        iLocalEstoque = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "32");
                                                    else
                                                        iLocalEstoque = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "33");

                                                    dtbProdutoOrla.Dispose();
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ClasseFuncoes.EnviaMensagemTelegramAsync("Pedido com erro no item!\n" + oPedido.IdOrder.ToString() + "\n" + oPedido.CustomerPfName + " - " + sProduto, "0,2", "Pedido com erro no item", oPedido.IdOrder.ToString());

                                        continue;
                                    }

                                    Pedido oPedidoFortPlus = new Pedido();

                                    Guid oGuid = Guid.NewGuid();
                                    oPedidoFortPlus.id = 0;
                                    oPedidoFortPlus.mvDocto = 0;
                                    oPedidoFortPlus.mvIdPessoa = int.Parse(oCliente.id);
                                    string sCNPJ = "";
                                    oPedidoFortPlus.mvIdVendedor = ClasseFuncoes.RetornaVendedorFortPlus("MAGAZINE LUIZA");
                                    oPedidoFortPlus.mvIdSerie = ClasseFuncoes.RetornaCodigoGlobal("SR", "1");
                                    oPedidoFortPlus.mvIdModelo = ClasseFuncoes.RetornaCodigoGlobal("MD", "55");
                                    oPedidoFortPlus.mvTipoMovimento = "1";
                                    oPedidoFortPlus.mvTipoPedido = "P";
                                    oPedidoFortPlus.mvIdTipoDocumento = ClasseFuncoes.RetornaCodigoGlobal("TD", "REC");

                                    oPedidoFortPlus.mvIdTipoFrete = ClasseFuncoes.RetornaCodigoGlobal("TF", "9");
                                    if (float.Parse(oPedido.TotalFreight) > 0)
                                    {
                                        oPedidoFortPlus.mvIdTipoFrete = ClasseFuncoes.RetornaCodigoGlobal("TF", "1");
                                    }

                                    oPedidoFortPlus.mvPreNota = "N";
                                    oPedidoFortPlus.mvFinNf = "1";
                                    oPedidoFortPlus.mvPresenca = ClasseFuncoes.RetornaCodigoParametro("_FPS_NFE_PRESENCA");
                                    oPedidoFortPlus.mvIdNatureza = ClasseFuncoes.RetornaCodigoGlobal("NO", "01");
                                    oPedidoFortPlus.mvIdParent = null;
                                    oPedidoFortPlus.idFilial = ClasseParametros.iFilial;

                                    if (oPedido.ShippedCarrierName == "Magalu Entregas")
                                    {
                                        oPedidoFortPlus.mvIdTransportadora = ClasseFuncoes.RetornaTransportadorFortPlus("MAGAZINE LUIZA");
                                    }
                                    else if (oPedido.ShippedCarrierName == "PAC")
                                    {
                                        oPedidoFortPlus.mvIdTransportadora = ClasseFuncoes.RetornaTransportadorFortPlus("PAC");

                                    }


                                    int? eTotal = 0;
                                    float? eTotalPago = 0;
                                    float? eTotalValor = 0;
                                    float? eTotalDesconto = float.Parse(oPedido.TotalTax);

                                    foreach (ProductMagalu o in oPedido.Products)
                                    {
                                        eTotal += o.Quantity;
                                        string sPreco = o.Price.ToString().Insert(o.Price.ToString().Length - 2, ",");

                                        eTotalValor += (float)(o.Quantity * float.Parse(sPreco));
                                    }


                                    s = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoFortPlus);
                                    List<Payment> lstPagamento = oPedido.Payments.OfType<Payment>().ToList();
                                    foreach (Payment oPagamento in lstPagamento)
                                    {
                                        eTotalPago += (float)oPagamento.Amount;
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
                                    oPedidoFortPlus.mvValorOutrasDespesasAcessoria = float.Parse(oPedido.TotalTax);

                                    oPedidoFortPlus.mvIdExterno = oPedido.IdOrder.ToString();
                                    if (oPedido.TotalFreight != "")
                                        oPedidoFortPlus.mvValorFrete = float.Parse(oPedido.TotalFreight);
                                    oPedidoFortPlus.mvValorDesconto = float.Parse(oPedido.TotalDiscount);

                                    oPedidoFortPlus.mvValorTotalProduto = eTotalValor;
                                    //oPedidoFortPlus.valo = oPedido.total_amount;
                                    oPedidoFortPlus.mvValorTotal = eTotalPago;
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
                                        client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/IdExterno/" + oPedido.IdOrder.ToString());
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

                                    foreach (Payment oPagamento in lstPagamento)
                                    {

                                        FortPlusFinanceiroReduzido oFinanceiro = new FortPlusFinanceiroReduzido();
                                        oFinanceiro.email = "rodrigonunes@2eletro.com.br";
                                        oFinanceiro.idFilial = oPedidoFortPlus.idFilial;
                                        oFinanceiro.idMovto = oPedidoFortPlus.id;
                                        oFinanceiro.idFormaPagamento = ClasseFuncoes.RetornaCodigoFormaPagamento(oPagamento.Name);
                                        oFinanceiro.idCondicaoPagamento = ClasseFuncoes.RetornaCodigoCondicaoPagamento("À VISTA");
                                        oFinanceiro.valor = (double)oPagamento.Amount;
                                        oFinanceiro = ClasseFuncoes.CadastraFinanceiroReduzido(oFinanceiro);

                                    }


                                    List<ProdutoFortePlus> lstProduto = new List<ProdutoFortePlus>();
                                    float? eValorFreteProduto = oPedidoFortPlus.mvValorFrete / oPedido.Products.Length;

                                    foreach (ProductMagalu o in oPedido.Products)
                                    {
                                        string sLast = o.IdSku.Substring(o.IdSku.Length - 5);
                                        string sInicio = o.IdSku.Replace(sLast, "");
                                        sProduto = "";
                                        if (sLast.Substring(sLast.Length - 3, 3) == "VAR")
                                        {
                                            sLast = sLast.Replace("VAR", "");

                                            string[] aProduto = sLast.Split('G');
                                            sProduto = sInicio + aProduto[0];
                                        }
                                        else
                                        {
                                            sProduto = o.IdSku;
                                        }


                                        ProdutoComplemento oProdutoComplemento = RetornaProdutoComplementoFortPlusPorSKU(sProduto, iMarketplace);
                                        ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComplemento.cmIdProduto.ToString()).Content);
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
                                                if (oProdutoComplementoComposicao.Count > 0)
                                                {
                                                    foreach (ProdutoComplemento oPC in oProdutoComplementoComposicao)
                                                    {
                                                        string sMarketPlace = ClasseFuncoes.RetornaNomeGlobalMK("MK", oPC.cmIdMarketPlace);
                                                        if (sMarketPlace.Contains("MAGAZINE LUIZA"))
                                                        {
                                                            oProdutoComplementoUsar = oPC;
                                                            break;
                                                        }
                                                    }
                                                    eValorTotalComposicao += oProdutoComplementoUsar.cmPrecoDePor;
                                                }
                                            }

                                            foreach (FortPlusProdutoComposicao oFormaPagamento in oListFormaPagamento)
                                            {
                                                PedidoItemFortPlus oItemPedido = new PedidoItemFortPlus();
                                                oItemPedido.id = 0;
                                                oItemPedido.mtIdNfOrigem = null;
                                                oItemPedido.mtIdMovto = oPedidoFortPlus.id;
                                                lstProduto.Add(oProduto);

                                                float? eTotalSemTarifa = eTotalValor - eTotalDesconto;
                                                double? eQuantidade = o.Quantity * oFormaPagamento.pcQtde;
                                                float? eQtd = float.Parse(eQuantidade.ToString());
                                                List<ProdutoComplemento> oProdutoComplementoComposicao = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(ClasseFuncoes.RetornaProdutoComplementoFortPlus(oFormaPagamento.pcIdProdutoComposicao.ToString()).Content);
                                                ProdutoComplemento oProdutoComplementoUsar = new ProdutoComplemento();


                                                foreach (ProdutoComplemento oPC in oProdutoComplementoComposicao)
                                                {
                                                    string sMarketPlace = ClasseFuncoes.RetornaNomeGlobalMK("MK", oPC.cmIdMarketPlace);
                                                    if (sMarketPlace.Contains("MAGAZINE"))
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
                                            oItemPedido.mtQtde = o.Quantity;

                                            string sPreco = o.Price.ToString().Insert(o.Price.ToString().Length - 2, ",");

                                            oItemPedido.mtValorUnitario = float.Parse(sPreco);
                                            oItemPedido.mtValorTotal = (float)(o.Quantity * float.Parse(sPreco));

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
                                    sSql = "UPDATE VENDAMARKETPLACE SET STATUS = 1, QUANTIDADEACIMA = 0 WHERE TRIM(ID) ='" + oPedido.IdOrder.ToString().Trim() + "'";
                                    ClasseParametros.ExecutabancoMySql(sSql);


                                    //foreach (ProdutoFortePlus oProduto in lstProduto)
                                    //{
                                    //    ClasseFuncoes.EnviaProdutosMercadoLivre(oProduto.id.ToString());
                                    //}


                                    if (eTotal > 1)
                                    {
                                        sSql = "UPDATE VENDAMARKETPLACE SET STATUSMENSAGEM = 3, QUANTIDADEACIMA = 1 WHERE TRIM(ID) ='" + oPedido.IdOrder.ToString().Trim() + "'";
                                        ClasseParametros.ExecutabancoMySql(sSql);
                                    }

                                    string smensagem = string.Format(ClasseParametros.sMensagemAcabouComprar, oCliente.psNome);

                                    string sPack = oPedido.IdOrder.ToString();
                                    if (oPedido.IdOrder != null)
                                        sPack = oPedido.IdOrder.ToString();

                                    // EnviaMensagemMercadoLivre(sPack, oPedido.seller.id.ToString(), oPedido.buyer.id.ToString(), smensagem, "1", oPedido.id.ToString());

                                    d.Dispose();
                                    ClasseFuncoes.AtualizaDadosFortPlus(oPedidoFortPlus.id);
                                    ClasseFuncoes.AtualizaDadosFortPlus(oPedidoFortPlus.id);

                                    sSql = "SELECT TRIM(ID) AS ID FROM VENDAMARKETPLACE WHERE TRIM(ID) = '" + oPedido.IdOrder.ToString().Trim() + "' AND MARKETPLACE = 'MAGAZINE LUIZA'";
                                    d = ClasseParametros.ConsultaBancoMysql(sSql);
                                    if (d.Rows.Count > 0)
                                    {
                                        // Atualiza pedido para MAGALU
                                        #region Marca pedido como processado
                                        MAGALUPedidoProcessado oPedidoProcessado = new MAGALUPedidoProcessado();
                                        oPedidoProcessado.IdOrder = oPedido.IdOrder;
                                        oPedidoProcessado.OrderStatus = "PROCESSING";

                                        string sJsonProcessado = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoProcessado);

                                        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order");
                                        request = new RestRequest(Method.PUT);
                                        request.AddHeader("cache-control", "no-cache");
                                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                                        request.AddParameter("application/json", sJsonProcessado, ParameterType.RequestBody);

                                        IRestResponse response = client.Execute(request);
                                        #endregion
                                    }
                                    d.Dispose();
                                }
                                else
                                {

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro: " + ex.Message);
                        }

                    }
                }

            }
            catch
            {

            }
        }

        public static void AtualizaEstoqueProdutos(int iCodigoCliente)
        {
            #region MAGAZINELUIZA
            int iPagina = 1;
            int iPorpagina = 50;

            List<ProdutoFortePlus> lstProdutoGerais = ClasseFuncoes.RetornaListProdutosFortPlus();

            while (true)
            {
                try
                {
                    ClasseFuncoes.RetornaCodigoMAGALU(iCodigoCliente);
                    var client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Product/?page=" + iPagina.ToString() + "&perPage=" + iPorpagina.ToString());
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                    IRestResponse response = client.Execute(request);
                    MAGALUProduto oProdutos = Newtonsoft.Json.JsonConvert.DeserializeObject<MAGALUProduto>(response.Content);
                    int? iMarketplace = ClasseFuncoes.RetornaCodigoGlobal("MK", "MAG");

                    if (oProdutos.Products.Length == 0)
                        break;

                    foreach (MagazineLuiza.Product oProduto in oProdutos.Products)
                    {
                        Console.WriteLine("Atualizando Produtos Magalu - " + oProduto.IdProduct);


                        if (oProduto.IdProduct != "LAVLAV01"
                            && oProduto.IdProduct != "LAVLAV02"
                            && oProduto.IdProduct != "LAVLAV03"
                            && oProduto.IdProduct != "LAVLAV04")
                        {
                            continue;
                        }

                        IRestResponse oResposta = ClasseFuncoes.RetornaProdutoComplementoFortPlus(ClasseFuncoes.RetornaCodigoProdutoFortPlusPorSKU(oProduto.IdProduct));
                        List<ProdutoComplemento> oJsonProdutoComplementoFortePlus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(oResposta.Content);

                        List<MagaluEstoque> oEstoques = new List<MagaluEstoque>();


                        foreach (ProdutoComplemento oProdutoMarketplace in oJsonProdutoComplementoFortePlus)
                        {
                            string sMarketplace = ClasseFuncoes.RetornaNomeGlobalMK("MK", oProdutoMarketplace.cmIdMarketPlace);
                            if (sMarketplace.Contains("MAGALU") || sMarketplace.Contains("MAGAZINE"))
                            {
                                if (oProduto.IdProduct != "ASPMON01")
                                {
                                }

                                ProdutoComplemento oProdutoComplemento = MAGALUClasseFuncoes.RetornaProdutoComplementoFortPlusPorSKU(oProduto.IdProduct);
                                ProdutoFortePlus oProdutoFortplus = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(oProdutoComplemento.cmIdProduto.ToString()).Content);

                                if (oProdutoFortplus.prIdParent != null)
                                {
                                    List<ProdutoFortePlus> oProdutoTemp1 = lstProdutoGerais.Where(x => x.prCodigo != null && x.prCodigo == oProduto.IdProduct).ToList();
                                    List<ProdutoFortePlus> oProdutoParent1 = lstProdutoGerais.Where(x => x.id == oProdutoTemp1[0].prIdParent).ToList();
                                    oProdutoComplemento = MAGALUClasseFuncoes.RetornaProdutoComplementoFortPlusPorSKU(oProdutoParent1[0].prCodigo);

                                    //ProdutoFortePlus oProdutoTemp = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(ClasseFuncoes.RetornaProdutoFortPlus(sProduto).Content);
                                }


                                int iLocalEstoque34 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "34");
                                double eQtdEstoque = ClasseFuncoes.RetornaSeTemEstoque(oProdutoComplemento.cmIdProduto.ToString(), iLocalEstoque34);

                                iLocalEstoque34 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "33");
                                eQtdEstoque = eQtdEstoque + ClasseFuncoes.RetornaSeTemEstoque(oProdutoComplemento.cmIdProduto.ToString(), iLocalEstoque34);

                                int iLocalEstoque32 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "32");
                                eQtdEstoque = eQtdEstoque + ClasseFuncoes.RetornaSeTemEstoque(oProdutoComplemento.cmIdProduto.ToString(), iLocalEstoque32);


                                if (oProduto.Code.Contains("KIT"))
                                {

                                    oResposta = null;

                                    while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                                    {
                                        client = new RestClient(ClasseParametros.sURlFortPlus + "/api/ProdutoComposicao/Produto/" + oProdutoFortplus.id.ToString());
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
                                    }

                                    List<FortPlusProdutoComposicao> oListFormaPagamento = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FortPlusProdutoComposicao>>(oResposta.Content);
                                    float? eValorTotalComposicao = 0;
                                    double eEstoquetemp = -06011988;
                                    foreach (FortPlusProdutoComposicao oProdutoComposicao in oListFormaPagamento)
                                    {
                                        iLocalEstoque34 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "34");
                                        eQtdEstoque = ClasseFuncoes.RetornaSeTemEstoque(oProdutoComposicao.pcIdProdutoComposicao.ToString(), iLocalEstoque34);

                                        iLocalEstoque34 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "33");
                                        eQtdEstoque = eQtdEstoque + ClasseFuncoes.RetornaSeTemEstoque(oProdutoComposicao.pcIdProdutoComposicao.ToString(), iLocalEstoque34);

                                        iLocalEstoque32 = (int)ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "32");
                                        eQtdEstoque = eQtdEstoque + ClasseFuncoes.RetornaSeTemEstoque(oProdutoComposicao.pcIdProdutoComposicao.ToString(), iLocalEstoque32);

                                        if (eEstoquetemp == -06011988)
                                            eEstoquetemp = eQtdEstoque;
                                        else if (eQtdEstoque < eEstoquetemp)
                                            eEstoquetemp = eQtdEstoque;




                                    }

                                    eQtdEstoque = eEstoquetemp;
                                }


                                if (ClasseFuncoes.RetornaSeTemEstoque(oProduto.IdProduct, iLocalEstoque32) != 0)
                                {

                                }

                                double iEstoqueAtual = eQtdEstoque;
                                double ePercentual = 0;
                                if (oProdutoMarketplace.cmPercentual != null)
                                    ePercentual = double.Parse(oProdutoMarketplace.cmPercentual.ToString());
                                else
                                    ePercentual = 30;

                                double eResultado = Math.Floor(iEstoqueAtual * (ePercentual / 100));

                                MagaluEstoque oEstoque = new MagaluEstoque();
                                oEstoque.IdSku = oProduto.IdProduct;
                                oEstoque.Quantity = (int)eResultado;
                                oEstoques.Add(oEstoque);

                                string sJson = Newtonsoft.Json.JsonConvert.SerializeObject(oEstoques);

                                client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Stock");
                                request = new RestRequest(Method.PUT);
                                request.AddHeader("cache-control", "no-cache");
                                request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                                request.AddParameter("application/json", sJson, ParameterType.RequestBody);

                                response = client.Execute(request);
                                request = null;
                                client = null;

                                Thread.Sleep(1000);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                iPagina++;
            }
            #endregion
        }

        internal static void AtualizaProdutos(TextBox editLog, string sSKU)
        {
            int? iCodigo = ClasseFuncoes.RetornaCodigoProdutoFortPlusPorSKU(sSKU.Trim());

            IRestResponse oResposta = ClasseFuncoes.RetornaProdutoFortPlus(iCodigo.ToString());
            ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(oResposta.Content);

            oResposta = ClasseFuncoes.RetornaProdutoComplementoFortPlus(oProduto.id);
            List<ProdutoComplemento> oJsonProdutoComplementoFortePlus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(oResposta.Content);

            Category[] aCategoria = new Category[1];
            Attribute[] aAttributes = new Attribute[1];
            Category oCategoria = new Category();
            Attribute oAtributo = new Attribute();

            if (sSKU == "MAQSIG09")
            {

            }

            string sJsonProduto = "";

            foreach (ProdutoComplemento oProdutoMarketplace in oJsonProdutoComplementoFortePlus)
            {
                double iEstoqueAtual = 0;
                bool lEnviar = false;
                string sMarketplace = ClasseFuncoes.RetornaNomeGlobalMK("MK", oProdutoMarketplace.cmIdMarketPlace);
                if (sMarketplace.Contains("MAGALU") || sMarketplace.Contains("MAGAZINE"))
                {

                    iEstoqueAtual = ClasseFuncoes.RetornaEstoque(oProduto.prCodigo);
                    double ePercentual = double.Parse(oProdutoMarketplace.cmPercentual.ToString());
                    double eResultado = Math.Floor(iEstoqueAtual * (ePercentual / 100));



                    bool lAnuncioCriado = false;
                    while (!lAnuncioCriado)
                    {
                        // Consulta se tem produto
                        var client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Product/" + oProduto.prCodigo.ToString());
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                        IRestResponse response = client.Execute(request);

                        Product oProdutoMAGALU = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(response.Content);

                        if (oProdutoMAGALU.IdProduct.Substring(oProdutoMAGALU.IdProduct.Length - 5).Contains("G"))
                        {
                            string sFinal = oProdutoMAGALU.IdProduct.Substring(oProdutoMAGALU.IdProduct.Length - 5);
                            string sSku = oProdutoMAGALU.IdProduct.Replace(sFinal, "");
                            string[] sSkuTemp = sFinal.Split('G');
                            aAttributes = new Attribute[sSkuTemp.Length];
                            int i = 0;
                            foreach (string sSKUAtual in sSkuTemp)
                            {
                                string sSKUOriginal = sSku + sSKUAtual;
                                Attribute oAtributoSKU = new Attribute();

                                oAtributoSKU.Name = "VOLTAGEM";
                                oAtributoSKU.Value = (i + 1).ToString() + (i + 1).ToString() + "0V";
                                aAttributes[i] = oAtributoSKU;
                                i++;

                            }
                            oProdutoMAGALU.Attributes = aAttributes;
                        }

                        oProdutoMAGALU.Categories = aCategoria;
                        oProdutoMAGALU.Attributes = aAttributes;

                        oProdutoMAGALU.Active = eResultado > 0;
                        sJsonProduto = Newtonsoft.Json.JsonConvert.SerializeObject(oProdutoMAGALU);

                        #region ENVIA PRODUTO
                        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Product");
                        request = new RestRequest(Method.PUT);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                        request.AddParameter("application/json", sJsonProduto, ParameterType.RequestBody);

                        response = client.Execute(request);
                        #endregion

                        // Consulta se tem SKU
                        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Sku/" + oProdutoMAGALU.IdProduct.ToString());
                        request = new RestRequest(Method.GET);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                        response = client.Execute(request);

                        // SKU
                        SKUProduto oProdutoMAGALUSKU = new SKUProduto();
                        oProdutoMAGALUSKU.IdSku = oProduto.prCodigo;
                        oProdutoMAGALUSKU.IdSkuErp = oProduto.prCodigo;
                        oProdutoMAGALUSKU.IdProduct = oProduto.prCodigo.ToString();
                        oProdutoMAGALUSKU.Name = oProduto.prNome;
                        oProdutoMAGALUSKU.Description = oProduto.prNome;
                        oProdutoMAGALUSKU.Height = (float)oProduto.prAltura / 100;
                        oProdutoMAGALUSKU.Width = (float)oProduto.prLargura / 100;
                        oProdutoMAGALUSKU.Length = (float)oProduto.prComprimento / 100;
                        oProdutoMAGALUSKU.Weight = (float)oProduto.prPesoBruto;
                        oProdutoMAGALUSKU.CodeEan = oProduto.prEan.ToString();
                        oProdutoMAGALUSKU.CodeNcm = "";
                        oProdutoMAGALUSKU.CodeIsbn = "";
                        oProdutoMAGALUSKU.CodeNbm = "";
                        oProdutoMAGALUSKU.Variation = "";
                        oProdutoMAGALUSKU.Status = true;

                        // Preco
                        Price oPreco = new Price();
                        oPreco.ListPrice = (float?)oProdutoMarketplace.cmPrecoDePor;
                        oPreco.SalePrice = (float?)oProdutoMarketplace.cmPrecoDePor;

                        oProdutoMAGALUSKU.Price = oPreco;

                        // Estoque

                        //if (!lPar)
                        //    eResultado = 0;

                        oProdutoMAGALUSKU.StockQuantity = (int)eResultado;
                        oProdutoMAGALUSKU.MainImageUrl = "http://interplacelog.com.br/imagens/semimagem.jpg";
                        // Imagens
                        oProdutoMAGALUSKU.UrlImages = new string[1];
                        oProdutoMAGALUSKU.UrlImages[0] = "http://interplacelog.com.br/imagens/semimagem.jpg";
                        // Atributos
                        //oProdutoMAGALUSKU.Attributes = new Attribute[1];

                        if (oProdutoMAGALUSKU.IdSku.Substring(oProdutoMAGALUSKU.IdSku.Length - 5).Contains("G"))
                        {
                            string sFinal = oProdutoMAGALUSKU.IdSku.Substring(oProdutoMAGALUSKU.IdSku.Length - 5);
                            string sSku = oProdutoMAGALUSKU.IdSku.Replace(sFinal, "");
                            string[] sSkuTemp = sFinal.Split('G');
                            aAttributes = new Attribute[sSkuTemp.Length];
                            int i = 0;
                            foreach (string sSKUAtual in sSkuTemp)
                            {
                                string sSKUOriginal = sSku + sSKUAtual;
                                Attribute oAtributoSKU = new Attribute();

                                oAtributoSKU.Name = "VOLTAGEM";
                                oAtributoSKU.Value = (i + 1).ToString() + (i + 1).ToString() + "0V";
                                aAttributes[i] = oAtributoSKU;
                                i++;

                            }
                            oProdutoMAGALUSKU.Attributes = aAttributes;
                        }



                        //if (lPar)
                        //{
                        //    oAtributoSKU.Name = "VOLTAGEM";
                        //    oAtributoSKU.Value = "110V";
                        //}

                        //oProdutoMAGALUSKU.Attributes[0] = oAtributoSKU;
                        sJsonProduto = Newtonsoft.Json.JsonConvert.SerializeObject(oProdutoMAGALUSKU);

                        #region ENVIA SKU
                        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Sku");
                        request = new RestRequest(Method.PUT);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                        request.AddParameter("application/json", sJsonProduto, ParameterType.RequestBody);

                        response = client.Execute(request);

                        #endregion



                        string sSql = "UPDATE PRODUTO SET ATUALIZADO = 1 WHERE CODIGO = '" + sSKU.Trim() + "' AND MARKETPLACE = '" + sMarketplace + "'";
                        ClasseParametros.ExecutabancoMySql(sSql);
                        lAnuncioCriado = true;

                    }
                }
            }



        }

        public static void EnviaProdutosNovos(TextBox editLog, string sSKU)
        {
            int? iCodigo = ClasseFuncoes.RetornaCodigoProdutoFortPlusPorSKU(sSKU.Trim());

            IRestResponse oResposta = ClasseFuncoes.RetornaProdutoFortPlus(iCodigo.ToString());
            ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(oResposta.Content);

            oResposta = ClasseFuncoes.RetornaProdutoComplementoFortPlus(oProduto.id);
            List<ProdutoComplemento> oJsonProdutoComplementoFortePlus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(oResposta.Content);

            Category[] aCategoria = new Category[1];
            Attribute[] aAttributes = new Attribute[1];
            Category oCategoria = new Category();
            Attribute oAtributo = new Attribute();


            string sJsonProduto = "";

            foreach (ProdutoComplemento oProdutoMarketplace in oJsonProdutoComplementoFortePlus)
            {
                double iEstoqueAtual = 0;
                bool lEnviar = false;
                string sMarketplace = ClasseFuncoes.RetornaNomeGlobalMK("MK", oProdutoMarketplace.cmIdMarketPlace);
                if (sMarketplace.Contains("MAGALU") || sMarketplace.Contains("MAGAZINE"))
                {
                    bool lAnuncioCriado = false;
                    while (!lAnuncioCriado)
                    {
                        // Consulta se tem produto
                        var client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Product/" + oProduto.prCodigo.ToString());
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            Product oProdutoMAGALU = new Product();
                            oProdutoMAGALU.Name = oProduto.prNome;
                            oProdutoMAGALU.IdProduct = oProduto.prCodigo;
                            if (oProduto.prIdMarca == null)
                            {
                                ClasseFuncoes.EnviaMensagemTelegramAsync("Item " + oProduto.prCodigo + " com erro no cadastro, falta a marca", "2,0", "Pedido com erro no item", oProduto.prCodigo);
                                break;
                            }
                            oProdutoMAGALU.Brand = ClasseFuncoes.RetornaNomeGlobalMK("MR", oProduto.prIdMarca);
                            oProdutoMAGALU.Code = oProduto.prCodigo;
                            oProdutoMAGALU.NbmOrigin = "0";
                            oProdutoMAGALU.WarrantyTime = "12";

                            string sGrupo = ClasseFuncoes.RetornaNomeGlobalMK("GP", oProduto.prIdGrupo);

                            oCategoria.Name = sGrupo;
                            oCategoria.Id = "1";
                            aCategoria[0] = oCategoria;
                            aAttributes = new Attribute[1];

                            //if (r["TIPOANUNCIO"].ToString() != "")
                            //{
                            //    // Pega variações e joga nos atributos
                            //    List<GlobalFortPlus> oListaGlobal = ClasseFuncoes.RetornaVariacoes("VP", "Voltagem");
                            //    string[] aVoltagem = r["TIPOANUNCIO"].ToString().Trim().Split(',');
                            //    aAttributes = new Attribute[aVoltagem.Length];
                            //    int i = 0;
                            //    foreach (string sVoltagem in aVoltagem)
                            //    {

                            //        List<GlobalFortPlus> oListaTemp = oListaGlobal.Where(x => x.id.ToString() == sVoltagem).ToList();
                            //        oAtributo = new Attribute();
                            //        oAtributo.Name = "Voltagem";
                            //        oAtributo.Value = oListaTemp[0].glNome;

                            //        aAttributes[i] = oAtributo;
                            //        i++;
                            //    }
                            //}
                            if (oProdutoMAGALU.IdProduct.Substring(oProdutoMAGALU.IdProduct.Length - 5).Contains("G"))
                            {
                                string sFinal = oProdutoMAGALU.IdProduct.Substring(oProdutoMAGALU.IdProduct.Length - 5);
                                string sSku = oProdutoMAGALU.IdProduct.Replace(sFinal, "");
                                string[] sSkuTemp = sFinal.Split('G');
                                aAttributes = new Attribute[sSkuTemp.Length];
                                int i = 0;
                                foreach (string sSKUAtual in sSkuTemp)
                                {
                                    string sSKUOriginal = sSku + sSKUAtual;
                                    Attribute oAtributoSKU = new Attribute();

                                    oAtributoSKU.Name = "VOLTAGEM";
                                    oAtributoSKU.Value = (i + 1).ToString() + (i + 1).ToString() + "0V";
                                    aAttributes[i] = oAtributoSKU;
                                    i++;

                                }
                                oProdutoMAGALU.Attributes = aAttributes;
                            }

                            oProdutoMAGALU.Categories = aCategoria;
                            oProdutoMAGALU.Attributes = aAttributes;

                            oProdutoMAGALU.Active = true;
                            sJsonProduto = Newtonsoft.Json.JsonConvert.SerializeObject(oProdutoMAGALU);

                            #region ENVIA PRODUTO
                            client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Product");
                            request = new RestRequest(Method.POST);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                            request.AddParameter("application/json", sJsonProduto, ParameterType.RequestBody);

                            response = client.Execute(request);
                            #endregion
                        }

                        if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            // Consulta se tem SKU
                            client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Sku/" + oProduto.id.ToString());
                            request = new RestRequest(Method.GET);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                            response = client.Execute(request);
                            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                            {
                                // SKU
                                SKUProduto oProdutoMAGALUSKU = new SKUProduto();
                                oProdutoMAGALUSKU.IdSku = oProduto.prCodigo;
                                oProdutoMAGALUSKU.IdSkuErp = oProduto.prCodigo;
                                oProdutoMAGALUSKU.IdProduct = oProduto.prCodigo.ToString();
                                oProdutoMAGALUSKU.Name = oProduto.prNome;
                                oProdutoMAGALUSKU.Description = oProduto.prNome;
                                oProdutoMAGALUSKU.Height = (float)oProduto.prAltura / 100;
                                oProdutoMAGALUSKU.Width = (float)oProduto.prLargura / 100;
                                oProdutoMAGALUSKU.Length = (float)oProduto.prComprimento / 100;
                                oProdutoMAGALUSKU.Weight = (float)oProduto.prPesoBruto;
                                if (oProduto.prEan == null)
                                {
                                    ClasseFuncoes.EnviaMensagemTelegramAsync("Item " + oProduto.prCodigo + " com erro no cadastro, falta EAN(Código da Barra)", "2,0", "Pedido com erro no item", oProduto.prCodigo);
                                    break;
                                }
                                oProdutoMAGALUSKU.CodeEan = oProduto.prEan.ToString();
                                oProdutoMAGALUSKU.CodeNcm = "";
                                oProdutoMAGALUSKU.CodeIsbn = "";
                                oProdutoMAGALUSKU.CodeNbm = "";
                                oProdutoMAGALUSKU.Variation = "";
                                oProdutoMAGALUSKU.Status = true;

                                // Preco
                                Price oPreco = new Price();
                                oPreco.ListPrice = (float?)oProdutoMarketplace.cmPrecoDePor;
                                oPreco.SalePrice = (float?)oProdutoMarketplace.cmPrecoDePor;

                                oProdutoMAGALUSKU.Price = oPreco;

                                // Estoque
                                iEstoqueAtual = ClasseFuncoes.RetornaEstoque(oProduto.prCodigo);


                                double ePercentual = double.Parse(oProdutoMarketplace.cmPercentual.ToString());
                                double eResultado = Math.Floor(iEstoqueAtual * (ePercentual / 100));
                                //if (!lPar)
                                //    eResultado = 0;

                                oProdutoMAGALUSKU.StockQuantity = (int)eResultado;
                                oProdutoMAGALUSKU.MainImageUrl = "http://interplacelog.com.br/imagens/semimagem.jpg";
                                // Imagens
                                oProdutoMAGALUSKU.UrlImages = new string[1];
                                oProdutoMAGALUSKU.UrlImages[0] = "http://interplacelog.com.br/imagens/semimagem.jpg";
                                // Atributos
                                //oProdutoMAGALUSKU.Attributes = new Attribute[1];

                                if (oProdutoMAGALUSKU.IdSku.Substring(oProdutoMAGALUSKU.IdSku.Length - 5).Contains("G"))
                                {
                                    string sFinal = oProdutoMAGALUSKU.IdSku.Substring(oProdutoMAGALUSKU.IdSku.Length - 5);
                                    string sSku = oProdutoMAGALUSKU.IdSku.Replace(sFinal, "");
                                    string[] sSkuTemp = sFinal.Split('G');
                                    aAttributes = new Attribute[sSkuTemp.Length];
                                    int i = 0;
                                    foreach (string sSKUAtual in sSkuTemp)
                                    {
                                        string sSKUOriginal = sSku + sSKUAtual;
                                        Attribute oAtributoSKU = new Attribute();

                                        oAtributoSKU.Name = "VOLTAGEM";
                                        oAtributoSKU.Value = (i + 1).ToString() + (i + 1).ToString() + "0V";
                                        aAttributes[i] = oAtributoSKU;
                                        i++;

                                    }
                                    oProdutoMAGALUSKU.Attributes = aAttributes;
                                }



                                //if (lPar)
                                //{
                                //    oAtributoSKU.Name = "VOLTAGEM";
                                //    oAtributoSKU.Value = "110V";
                                //}

                                //oProdutoMAGALUSKU.Attributes[0] = oAtributoSKU;
                                sJsonProduto = Newtonsoft.Json.JsonConvert.SerializeObject(oProdutoMAGALUSKU);

                                #region ENVIA SKU
                                client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Sku");
                                request = new RestRequest(Method.POST);
                                request.AddHeader("cache-control", "no-cache");
                                request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                                request.AddParameter("application/json", sJsonProduto, ParameterType.RequestBody);

                                response = client.Execute(request);
                                if (response.Content.Contains("Já existe um sku com o IdSku informado"))
                                {
                                    client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Sku");
                                    request = new RestRequest(Method.PUT);
                                    request.AddHeader("cache-control", "no-cache");
                                    request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                                    request.AddParameter("application/json", sJsonProduto, ParameterType.RequestBody);

                                    response = client.Execute(request);
                                }


                                #endregion
                            }


                            string sSql = "UPDATE PRODUTO SET ATUALIZADO = 1 WHERE CODIGO = '" + sSKU.Trim() + "' AND MARKETPLACE = '" + sMarketplace + "'";
                            ClasseParametros.ExecutabancoMySql(sSql);
                            lAnuncioCriado = true;
                        }
                        else if (response.Content.Contains("Não existe uma categoria com Id informado"))
                        {
                            sJsonProduto = Newtonsoft.Json.JsonConvert.SerializeObject(oCategoria);

                            client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Category");
                            request = new RestRequest(Method.POST);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                            request.AddParameter("application/json", sJsonProduto, ParameterType.RequestBody);

                            response = client.Execute(request);
                        }
                    }
                }
            }

        }

        public static void EnviaProdutoMAGALU(int iCodigoCliente)
        {
            try
            {
                //ClasseFuncoes.SalvaLogServicos("Envia produtos para MAGALU");

                DataTable dtbProduto = ClasseParametros.ConsultaBancoMysql("SELECT * FROM PRODUTO WHERE ATUALIZADO = 0");

                bool lPar = true;

                foreach (DataRow r in dtbProduto.Rows)
                {
                    try
                    {
                        Console.WriteLine("Enviando produto " + r["CODIGO"].ToString());

                        if (!r["MARKETPLACE"].ToString().Contains("MAGAZINE LUIZA"))
                        {
                            continue;
                        }

                        IRestResponse oResposta = ClasseFuncoes.RetornaProdutoFortPlus(r["CODIGOINTERNO"].ToString().Trim());
                        ProdutoFortePlus oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProdutoFortePlus>(oResposta.Content);

                        oResposta = ClasseFuncoes.RetornaProdutoComplementoFortPlus(oProduto.id);
                        List<ProdutoComplemento> oJsonProdutoComplementoFortePlus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(oResposta.Content);

                        Category[] aCategoria = new Category[1];
                        Attribute[] aAttributes = new Attribute[1];
                        Category oCategoria = new Category();
                        Attribute oAtributo = new Attribute();


                        string sJsonProduto = "";

                        foreach (ProdutoComplemento oProdutoMarketplace in oJsonProdutoComplementoFortePlus)
                        {
                            double iEstoqueAtual = 0;
                            bool lEnviar = false;
                            string sMarketplace = ClasseFuncoes.RetornaNomeGlobalMK("MK", oProdutoMarketplace.cmIdMarketPlace);
                            if (sMarketplace.Contains("MAGALU") || sMarketplace.Contains("MAGAZINE"))
                            {
                                Console.WriteLine("     Produto " + oProduto.prNome);
                                bool lAnuncioCriado = false;
                                while (!lAnuncioCriado)
                                {

                                    ClasseFuncoes.RetornaCodigoMAGALU(iCodigoCliente);

                                    // Consulta se tem produto
                                    var client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Product/" + oProduto.prCodigo.ToString());
                                    var request = new RestRequest(Method.GET);
                                    request.AddHeader("cache-control", "no-cache");
                                    request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                                    IRestResponse response = client.Execute(request);

                                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                                    {
                                        Product oProdutoMAGALU = new Product();
                                        oProdutoMAGALU.Name = oProduto.prNome;
                                        oProdutoMAGALU.IdProduct = oProduto.prCodigo;
                                        oProdutoMAGALU.Brand = ClasseFuncoes.RetornaNomeGlobalMK("MR", oProduto.prIdMarca);
                                        oProdutoMAGALU.Code = oProduto.prCodigo;
                                        oProdutoMAGALU.NbmOrigin = "0";
                                        oProdutoMAGALU.WarrantyTime = "12";

                                        string sGrupo = ClasseFuncoes.RetornaNomeGlobalMK("GP", oProduto.prIdGrupo);

                                        oCategoria.Name = sGrupo;
                                        oCategoria.Id = "1";
                                        aCategoria[0] = oCategoria;
                                        aAttributes = new Attribute[1];

                                        if (r["TIPOANUNCIO"].ToString() != "")
                                        {
                                            // Pega variações e joga nos atributos
                                            List<GlobalFortPlus> oListaGlobal = ClasseFuncoes.RetornaVariacoes("VP", "Voltagem");
                                            string[] aVoltagem = r["TIPOANUNCIO"].ToString().Trim().Split(',');
                                            aAttributes = new Attribute[aVoltagem.Length];
                                            int i = 0;
                                            foreach (string sVoltagem in aVoltagem)
                                            {

                                                List<GlobalFortPlus> oListaTemp = oListaGlobal.Where(x => x.id.ToString() == sVoltagem).ToList();
                                                oAtributo = new Attribute();
                                                oAtributo.Name = "Voltagem";
                                                oAtributo.Value = oListaTemp[0].glNome;

                                                aAttributes[i] = oAtributo;
                                                i++;
                                            }
                                        }

                                        oProdutoMAGALU.Categories = aCategoria;
                                        oProdutoMAGALU.Attributes = aAttributes;

                                        oProdutoMAGALU.Active = true;
                                        sJsonProduto = Newtonsoft.Json.JsonConvert.SerializeObject(oProdutoMAGALU);

                                        #region ENVIA PRODUTO
                                        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Product");
                                        request = new RestRequest(Method.POST);
                                        request.AddHeader("cache-control", "no-cache");
                                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                                        request.AddParameter("application/json", sJsonProduto, ParameterType.RequestBody);

                                        response = client.Execute(request);
                                        if (response.Content.Contains("O campo Brand é obrigatório"))
                                        {
                                            Console.WriteLine("Erro: " + response.Content);
                                            break;
                                        }
                                        #endregion
                                    }

                                    if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        // Consulta se tem SKU
                                        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Sku/" + oProduto.prCodigo.ToString());
                                        request = new RestRequest(Method.GET);
                                        request.AddHeader("cache-control", "no-cache");
                                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                                        response = client.Execute(request);
                                        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                                        {
                                            // SKU
                                            SKUProduto oProdutoMAGALUSKU = new SKUProduto();
                                            oProdutoMAGALUSKU.IdSku = oProduto.prCodigo;
                                            oProdutoMAGALUSKU.IdSkuErp = oProduto.prCodigo;
                                            oProdutoMAGALUSKU.IdProduct = oProduto.prCodigo.ToString();
                                            oProdutoMAGALUSKU.Name = oProduto.prNome;
                                            oProdutoMAGALUSKU.Description = oProduto.prNome;
                                            oProdutoMAGALUSKU.Height = (float)oProduto.prAltura / 100;
                                            oProdutoMAGALUSKU.Width = (float)oProduto.prLargura / 100;
                                            oProdutoMAGALUSKU.Length = (float)oProduto.prComprimento / 100;
                                            oProdutoMAGALUSKU.Weight = (float)oProduto.prPesoBruto;
                                            oProdutoMAGALUSKU.CodeEan = oProduto.prEan.ToString();
                                            oProdutoMAGALUSKU.CodeNcm = "";
                                            oProdutoMAGALUSKU.CodeIsbn = "";
                                            oProdutoMAGALUSKU.CodeNbm = "";
                                            oProdutoMAGALUSKU.Variation = "";
                                            oProdutoMAGALUSKU.Status = true;

                                            // Preco
                                            Price oPreco = new Price();
                                            oPreco.ListPrice = (float?)oProdutoMarketplace.cmPrecoDePor;
                                            oPreco.SalePrice = (float?)oProdutoMarketplace.cmPrecoDePor;

                                            oProdutoMAGALUSKU.Price = oPreco;

                                            // Estoque
                                            iEstoqueAtual = ClasseFuncoes.RetornaEstoque(oProduto.prCodigo);
                                            double ePercentual = 0;
                                            if (oProdutoMarketplace.cmPercentual != null)
                                                ePercentual = double.Parse(oProdutoMarketplace.cmPercentual.ToString());
                                            else
                                                ePercentual = 30;

                                            double eResultado = Math.Floor(iEstoqueAtual * (ePercentual / 100));
                                            //if (!lPar)
                                            //    eResultado = 0;

                                            oProdutoMAGALUSKU.StockQuantity = (int)eResultado;
                                            oProdutoMAGALUSKU.MainImageUrl = "http://interplacelog.com.br/imagens/semimagem.jpg";
                                            // Imagens
                                            oProdutoMAGALUSKU.UrlImages = new string[1];
                                            oProdutoMAGALUSKU.UrlImages[0] = "http://interplacelog.com.br/imagens/semimagem.jpg";
                                            // Atributos
                                            //oProdutoMAGALUSKU.Attributes = new Attribute[1];
                                            Attribute oAtributoSKU = new Attribute();
                                            //if (lPar)
                                            //{
                                            //    oAtributoSKU.Name = "VOLTAGEM";
                                            //    oAtributoSKU.Value = "110V";
                                            //}

                                            //oProdutoMAGALUSKU.Attributes[0] = oAtributoSKU;
                                            sJsonProduto = Newtonsoft.Json.JsonConvert.SerializeObject(oProdutoMAGALUSKU);

                                            #region ENVIA SKU
                                            client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Sku");
                                            request = new RestRequest(Method.POST);
                                            request.AddHeader("cache-control", "no-cache");
                                            request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                                            request.AddParameter("application/json", sJsonProduto, ParameterType.RequestBody);

                                            response = client.Execute(request);
                                            #endregion
                                        }


                                        string sSql = "UPDATE PRODUTO SET ATUALIZADO = 1 WHERE CODIGOINTERNO = " + r["CODIGOINTERNO"].ToString().Trim() + " AND MARKETPLACE = '" + sMarketplace + "'";
                                        ClasseParametros.ExecutabancoMySql(sSql);
                                        lAnuncioCriado = true;
                                    }
                                    else if (response.Content.Contains("Não existe uma categoria com Id informado"))
                                    {
                                        sJsonProduto = Newtonsoft.Json.JsonConvert.SerializeObject(oCategoria);

                                        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Category");
                                        request = new RestRequest(Method.POST);
                                        request.AddHeader("cache-control", "no-cache");
                                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                                        request.AddParameter("application/json", sJsonProduto, ParameterType.RequestBody);

                                        response = client.Execute(request);
                                    }
                                }
                            }
                            else if (sMarketplace.Contains("B2W"))
                            {





                            }
                        }
                        lPar = !lPar;
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
            catch { }
        }
        public static void EnviaNotasFaturadasMAGALU(int iCodigoCliente)
        {
            try
            {
                ClasseFuncoes.SalvaLogServicos("Envia Notas MAGALU!");

                RestClient client = null;
                RestRequest request = null;
                IRestResponse oResposta = null;
                IRestResponse response = null;
                ClasseFuncoes.RetornaCodigoMAGALU(iCodigoCliente);
                #region add pedidos lidos, excluir depois de integrado

                //RestClient client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/OrderQueue?status=APPROVED");
                //RestRequest request = new RestRequest(Method.GET);
                //request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                //IRestResponse response = client.Execute(request);
                //MAGALUOrderQueue oPedidoQueue = Newtonsoft.Json.JsonConvert.DeserializeObject<MAGALUOrderQueue>(response.Content);

                //List<RetornoPedido> oRetorno = new List<RetornoPedido>();
                //for (int i = 0; i < oPedidoQueue.Total; i++)
                //{
                //    RetornoPedido oTemp = new RetornoPedido();
                //    oTemp.Id = oPedidoQueue.OrderQueues[i].Id.ToString();

                //    oRetorno.Add(oTemp);
                //}

                //string sJsonProduto = Newtonsoft.Json.JsonConvert.SerializeObject(oRetorno);

                //client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/OrderQueue");
                //request = new RestRequest(Method.PUT);
                //request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                //request.AddParameter("application/json", sJsonProduto, ParameterType.RequestBody);
                //IRestResponse oResposta = client.Execute(request);

                #endregion


                int iPagina = 1;
                int iPorPagina = 50;

                //PROCESSANDO
                //while (true)
                //{
                //    //Os possíveis status são: New, Approved, Processing, Invoiced, Shipped, Delivered, Canceled, ShipmentException.
                //    client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order?page=" + iPagina.ToString() + "&perPage=" + iPorPagina.ToString() + "&Status=APPROVED");
                //    request = new RestRequest(Method.GET);
                //    request.AddHeader("cache-control", "no-cache");
                //    request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                //    oResposta = client.Execute(request);

                //    Thread.Sleep(1000);
                //    JsonPedidoMagalu oPedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPedidoMagalu>(oResposta.Content);

                //    if (oPedidos.Total == 0)
                //    {
                //        break;
                //    }

                //    foreach (Order oPedido in oPedidos.Orders)
                //    {
                //        #region processado
                //        MAGALUPedidoProcessado oPedidoProcessado = new MAGALUPedidoProcessado();
                //        oPedidoProcessado.OrderStatus = "PROCESSING";
                //        oPedidoProcessado.IdOrder = oPedido.IdOrder;

                //        string sJsonProcessado = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoProcessado);

                //        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order");
                //        request = new RestRequest(Method.PUT);
                //        request.AddHeader("cache-control", "no-cache");
                //        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                //        request.AddParameter("application/json", sJsonProcessado, ParameterType.RequestBody);

                //        response = client.Execute(request);
                //        #endregion
                //    }
                //    iPagina++;
                //}


                oResposta = null;

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

                oResposta = null;
                while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Transmissao/");
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
                List<FortPlusXML> oListXML = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FortPlusXML>>(oResposta.Content);

                //Faturado
                while (true)
                {
                    try
                    {
                        ////Os possíveis status são: New, Approved, Processing, Invoiced, Shipped, Delivered, Canceled, ShipmentException.
                        //string sDataInicial = DateTime.Now.AddDays(-8).ToShortDateString() + " 00:00:00";
                        //string sDataFinal = DateTime.Now.ToShortDateString() + " 23:59:59";
                        //string sURL = "https://in.integracommerce.com.br/api/Order?page="+iPagina.ToString()+"&perPage="+iPorPagina.ToString()+
                        //    "&status=PROCESSING&startDate="+ sDataInicial + "&endDate="+ sDataFinal + "&api_key=" + ClasseParametros.sTokenMAGALU;
                        //XmlTextReader reader = new XmlTextReader(sURL);

                        client = new RestClient("https://in.integracommerce.com.br/api/Order?page=" + iPagina.ToString() + "&perPage=" + iPorPagina.ToString() + "&Status=PROCESSING");
                        request = new RestRequest(Method.GET);
                        request.AddHeader("cache-control", "no-cache");

                        request.AddHeader("Content-Type", "application/json");

                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                        oResposta = client.Execute(request);

                        Thread.Sleep(1000);
                        JsonPedidoMagalu oPedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPedidoMagalu>(oResposta.Content);

                        if (oPedidos.Orders == null)
                        {
                            break;
                        }



                        foreach (Order oPedido in oPedidos.Orders)
                        {
                            Console.WriteLine("Enviando nota do pedido " + oPedido.IdOrder);
                            DataTable dtbMarketPlace = ClasseParametros.ConsultaBancoMysql("SELECT * FROM XMLVENDAS WHERE PEDIDO = '" + oPedido.IdOrder.Trim() + "'");
                            List<Pedido> lstPedidoFiltrado = lstPedido.Where(x => x.mvIdExterno == oPedido.IdOrder && x.mvEntidade == "NFE").ToList();
                            if (lstPedidoFiltrado.Count == 0)
                                continue;
                            Pedido oPedidoFortePlus = lstPedidoFiltrado[0];


                            DanfeViewModel oDanfe = null;
                            string sXML = "";
                            if (dtbMarketPlace.Rows.Count == 0)
                            {

                                List<FortPlusXML> oListXMLFiltrado = oListXML.Where(x => x.trIdMovto != null && x.trIdMovto == oPedidoFortePlus.id).ToList();
                                if (oListXMLFiltrado.Count == 0)
                                    return;
                                sXML = oListXMLFiltrado[0].trArquivoRetorno;
                            }
                            else
                            {
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


                            #region faturado
                            Thread.Sleep(3000);
                            MAGALUPedidoFaturado oPedidoFaturado = new MAGALUPedidoFaturado();
                            oPedidoFaturado.OrderStatus = "INVOICED";
                            oPedidoFaturado.IdOrder = oPedido.IdOrder;
                            oPedidoFaturado.InvoicedDanfeXml = sXML;
                            oPedidoFaturado.InvoicedIssueDate = oDanfe.DataHoraEmissao;
                            oPedidoFaturado.InvoicedKey = oDanfe.ChaveAcesso;
                            oPedidoFaturado.InvoicedNumber = oDanfe.NfNumero.ToString();

                            string sJsonProcessado = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoFaturado);

                            client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order");
                            request = new RestRequest(Method.PUT);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                            request.AddParameter("application/json", sJsonProcessado, ParameterType.RequestBody);

                            response = client.Execute(request);
                            #endregion
                        }
                    }
                    catch
                    {

                    }
                    iPagina++;
                }

                Thread.Sleep(500);


                iPagina = 1;
                // Gera Etiquetas
                while (true)
                {
                    try
                    {
                        //Os possíveis status são: New, Approved, Processing, Invoiced, Shipped, Delivered, Canceled, ShipmentException.
                        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order?page=" + iPagina.ToString() + "&perPage=" + iPorPagina.ToString() + "&Status=INVOICED");
                        request = new RestRequest(Method.GET);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                        oResposta = client.Execute(request);

                        Thread.Sleep(3000);
                        JsonPedidoMagalu oPedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPedidoMagalu>(oResposta.Content);

                        if (oPedidos.Orders == null)
                        {
                            break;
                        }
                        DataTable dtbChaves = ClasseParametros.ConsultaBancoMysql("SELECT USUARIOMAGALU,SENHAMAGALU FROM CLIENTE WHERE CODIGO = 5");

                        foreach (Order oPedido in oPedidos.Orders)
                        {
                            Console.WriteLine("Gerando etiqueta do pedido " + oPedido.IdOrder);

                            if (oPedido.IdOrder == "LU-8504500673851491")
                            {

                            }

                            DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oPedido.InvoicedKey + "'");
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


                        //Order oJson = null;
                        //foreach (DataRow rChaves in dtbChaves.Rows)
                        //{
                        //    if (rChaves["USUARIOMAGALU"].ToString() != "")
                        //    {
                        //        string sAPI = ClasseFuncoes.Base64Encode(rChaves["USUARIOMAGALU"].ToString() + ":" + rChaves["SENHAMAGALU"].ToString());
                        //        //var client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order?page=1&perPage=50&Status=INVOICED");
                        //        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order/" + oPedido.IdOrder.Trim());
                        //        request = new RestRequest(Method.GET);
                        //        request.AddHeader("cache-control", "no-cache");
                        //        request.AddHeader("authorization", "Basic " + sAPI);
                        //        //request.AddHeader("authorization", "Basic " + Base64Encode("lojamegastoreapi:LyJymDIY4gwX"));
                        //        response = client.Execute(request);
                        //        if (response.StatusCode == HttpStatusCode.OK)
                        //        {
                        //            oJson = Newtonsoft.Json.JsonConvert.DeserializeObject<Order>(response.Content);
                        //            //decimal ePagina = Math.Ceiling(decimal.Divide(oJson.Total, 50));
                        //            try
                        //            {
                        //                // Gera tracking
                        //                oJson.CodigoCliente = 5;
                        //                ClasseParametros.SalvaBancoPDFZPLMagalu(oJson, rChaves["USUARIOMAGALU"].ToString(), rChaves["SENHAMAGALU"].ToString());

                        //            }
                        //            catch (Exception ex)
                        //            {
                        //                ClasseParametros.MostraErro(ex.Message, ClasseParametros.iconApp);
                        //            }
                        //            break;
                        //        }
                        //    }
                        //}

                    }
                    catch
                    {

                    }
                    iPagina++;
                }

                //DESPACHADO
                //while (true)
                //{
                //    //Os possíveis status são: New, Approved, Processing, Invoiced, Shipped, Delivered, Canceled, ShipmentException.
                //    client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order?page=" + iPagina.ToString() + "&perPage=" + iPorPagina.ToString() + "&Status=INVOICED");
                //    request = new RestRequest(Method.GET);
                //    request.AddHeader("cache-control", "no-cache");
                //    request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                //    oResposta = client.Execute(request);

                //    Thread.Sleep(1000);
                //    JsonPedidoMagalu oPedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPedidoMagalu>(oResposta.Content);

                //    if (oPedidos.Total == 0)
                //    {
                //        break;
                //    }

                //    foreach (Order oPedido in oPedidos.Orders)
                //    {

                //        #region despachado
                //        MAGALUPedidoDespachado oPedidoDespachado = new MAGALUPedidoDespachado();
                //        oPedidoDespachado.OrderStatus = "SHIPPED";
                //        oPedidoDespachado.IdOrder = oPedido.IdOrder;
                //        oPedidoDespachado.ShippedCarrierDate = DateTime.Now;
                //        oPedidoDespachado.ShippedCarrierName = "magalu_entregas";
                //        oPedidoDespachado.ShippedTrackingUrl = "http://teste";
                //        oPedidoDespachado.ShippedTrackingProtocol = "BR1234564";
                //        oPedidoDespachado.ShippedEstimatedDelivery = DateTime.Now.AddDays(10);

                //        string sJsonProcessado = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoDespachado);

                //        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order");
                //        request = new RestRequest(Method.PUT);
                //        request.AddHeader("cache-control", "no-cache");
                //        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                //        request.AddParameter("application/json", sJsonProcessado, ParameterType.RequestBody);

                //        response = client.Execute(request);
                //        #endregion

                //        //DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oPedido.InvoicedKey + "'", null);

                //        //if (d.Rows.Count > 0)
                //        //{
                //        //    continue;
                //        //}

                //        //SalvaBancoPDFZPLMagalu(oPedido);
                //    }
                //}

                //ENTREGUE
                //while (true)
                //{
                //    //Os possíveis status são: New, Approved, Processing, Invoiced, Shipped, Delivered, Canceled, ShipmentException.
                //    client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order?page=" + iPagina.ToString() + "&perPage=" + iPorPagina.ToString() + "&Status=SHIPPED");
                //    request = new RestRequest(Method.GET);
                //    request.AddHeader("cache-control", "no-cache");
                //    request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                //    oResposta = client.Execute(request);

                //    Thread.Sleep(1000);
                //    JsonPedidoMagalu oPedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPedidoMagalu>(oResposta.Content);

                //    if (oPedidos.Total == 0)
                //    {
                //        break;
                //    }

                //    foreach (Order oPedido in oPedidos.Orders)
                //    {

                //        #region despachado
                //        MAGALUPedidoEntregue oPedidoEntregue = new MAGALUPedidoEntregue();
                //        oPedidoEntregue.OrderStatus = "DELIVERED";
                //        oPedidoEntregue.IdOrder = oPedido.IdOrder;
                //        oPedidoEntregue.DeliveredDate = DateTime.Now.AddDays(10);

                //        string sJsonProcessado = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoEntregue);

                //        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order");
                //        request = new RestRequest(Method.PUT);
                //        request.AddHeader("cache-control", "no-cache");
                //        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                //        request.AddParameter("application/json", sJsonProcessado, ParameterType.RequestBody);

                //        response = client.Execute(request);
                //        #endregion

                //        //DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oPedido.InvoicedKey + "'", null);

                //        //if (d.Rows.Count > 0)
                //        //{
                //        //    continue;
                //        //}

                //        //SalvaBancoPDFZPLMagalu(oPedido);
                //    }
                //}
                iPagina++;
            }
            catch
            {

            }
        }


        public static void SalvaBancoPDFZPLMagalu(Order oPedido)
        {
            try
            {
                EnvioEtiquetaMAGALU oEtiqueta = new EnvioEtiquetaMAGALU();
                oEtiqueta.Format = "ZPL";
                oEtiqueta.Orders = new string[] { oPedido.IdOrder };


                string sBody = Newtonsoft.Json.JsonConvert.SerializeObject(oEtiqueta);
                var client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order/ShippingLabels");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", sBody, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.Content.Contains("startIndex cannot be larger"))
                {
                    return;
                }
                //EtiquetaMAGALU oJson = Newtonsoft.Json.JsonConvert.DeserializeObject<EtiquetaMAGALU>(response.Content);
                string[] aJson = response.Content.Split('"');
                string sSite = aJson[3];
                using (var clientWeb = new WebClient())
                {
                    clientWeb.DownloadFile(sSite, Directory.GetCurrentDirectory() + "\\tempmagalu.zip");
                }
                DescampactaSalvaEtiquetaMAGALU(oPedido);
            }
            catch (Exception ex)
            {
                ClasseFuncoes.SalvaLogServicos(ex.Message + " - MAGAZINE_LUIZA");

            }
        }

        private static void DescampactaSalvaEtiquetaMAGALU(Order oPedido)
        {

            try
            {
                //File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\temp.zip", aEtiqueta); // Requires System.IO
                string sPastaDescompactado = Directory.GetCurrentDirectory() + "\\temp";

                if (!Directory.Exists(sPastaDescompactado))
                {
                    Directory.CreateDirectory(sPastaDescompactado);
                }

                string sArquivoZIP = Directory.GetCurrentDirectory() + "\\tempmagalu.zip";

                File.Delete(sPastaDescompactado + "\\plp.pdf");

                File.Delete(sPastaDescompactado + "\\etiquetas.zpl");

                ZipFile.ExtractToDirectory(sArquivoZIP, sPastaDescompactado, Encoding.UTF8);

                byte[] aArquivoZPL = { };
                if (File.Exists(sPastaDescompactado + "\\etiquetas.zpl"))
                {
                    aArquivoZPL = File.ReadAllBytes(sPastaDescompactado + "\\etiquetas.zpl");
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLETQ\\"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLETQ\\");
                    }
                    File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLETQ\\" + oPedido.InvoicedKey + ".zpl", aArquivoZPL); // Requires System.IO
                }

                byte[] aArquivoPDF = { };
                if (File.Exists(sPastaDescompactado + "\\plp.pdf"))
                {
                    aArquivoPDF = File.ReadAllBytes(sPastaDescompactado + "\\plp.pdf");
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLPDF\\"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLPDF\\");
                    }
                    File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLPDF\\" + oPedido.InvoicedKey + ".pdf", aArquivoPDF); // Requires System.IO
                }

                try
                {
                    DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oPedido.InvoicedKey + "'");
                    Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();
                    ParametrosSQL.Add("@ETQ", aArquivoZPL);
                    ParametrosSQL.Add("@ETIQUETATXTTXT", Encoding.UTF8.GetString(aArquivoZPL));
                    ParametrosSQL.Add("@ETIQUETAPDF", aArquivoPDF);
                    ParametrosSQL.Add("@NOTA", oPedido.InvoicedKey);
                    ParametrosSQL.Add("@LOJA", "MAGAZINE_LUIZA");
                    ParametrosSQL.Add("@CODIGOCLIENTE", oPedido.CodigoCliente);
                    ParametrosSQL.Add("@MARKETPLACE", oPedido.MarketplaceName);
                    ParametrosSQL.Add("@PEDIDO", oPedido.IdOrder);
                    string sSql = "";

                    if (d.Rows.Count > 0)
                    {
                        sSql = "UPDATE VENDAS SET ETIQUETATXT=@ETQ,ETIQUETATXTTXT=@ETIQUETATXTTXT,ETIQUETAPDF=@ETIQUETAPDF,LOJA=@LOJA,CODIGOCLIENTE=@CODIGOCLIENTE,MARKETPLACE=@MARKETPLACE,PEDIDO=@PEDIDO WHERE NOTAFISCAL = @NOTA";
                    }
                    else
                    {
                        sSql = "INSERT INTO VENDAS(NOTAFISCAL,ETIQUETATXT,LOJA,DATACRIADO,LOTE,CODIGOCLIENTE,MARKETPLACE,PEDIDO,ETIQUETATXTTXT,ETIQUETAPDF) VALUES(@NOTA,@ETQ,@LOJA,@DATACRIADO,@LOTE,@CODIGOCLIENTE,@MARKETPLACE,@PEDIDO,@ETIQUETATXTTXT,@ETIQUETAPDF)";

                        ParametrosSQL.Add("@DATACRIADO", DateTime.Now);
                        ParametrosSQL.Add("@LOTE", ClasseParametros.PegaLote("MAGAZINE_LUIZA", oPedido.CodigoCliente.ToString()));

                    }
                    d.Dispose();

                    ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);

                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLETQMAGAZINELUIZA\\"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLETQMAGAZINELUIZA\\");
                    }

                    if (aArquivoPDF == null || aArquivoZPL == null)
                        return;

                }
                catch (Exception e)
                {
                    ClasseFuncoes.SalvaLogServicos(e.Message);

                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                ClasseFuncoes.SalvaLogServicos(ex.Message);

            }


        }
    }
}
