using InterRegraNegocio;
using InterRegraNegocio.MagazineLuiza;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlusDespacharPedidoMagalu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Marca pedido magalu como despachado");
            int iCliente = 5;
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
            ClasseFuncoes.RetornaCodigoMAGALU(iCliente);
            int iPorPagina = 50;
            int iPagina = 1;

            //DESPACHADO
            while (true)
            {
                //Os possíveis status são: New, Approved, Processing, Invoiced, Shipped, Delivered, Canceled, ShipmentException.
                RestClient client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order?page=" + iPagina.ToString() + "&perPage=" + iPorPagina.ToString() + "&Status=INVOICED");
                RestRequest request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                IRestResponse oResposta = client.Execute(request);

                Thread.Sleep(1000);
                JsonPedidoMagalu oPedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPedidoMagalu>(oResposta.Content);

                if (oPedidos.Total == 0)
                {
                    break;
                }

                foreach (Order oPedido in oPedidos.Orders)
                {
                    Console.WriteLine(" Pedido: " + oPedido.IdOrder);

                    if (oPedido.IdOrder == "LU-8507500675115463")
                    {

                        #region despachado
                        MAGALUPedidoDespachado oPedidoDespachado = new MAGALUPedidoDespachado();
                        oPedidoDespachado.OrderStatus = "SHIPPED";
                        oPedidoDespachado.IdOrder = oPedido.IdOrder;
                        oPedidoDespachado.ShippedCarrierDate = DateTime.Parse("04/07/2020 17:00");
                        oPedidoDespachado.ShippedCarrierName = "PAC";
                        oPedidoDespachado.ShippedTrackingUrl = "https://www2.correios.com.br/sistemas/rastreamento/default.cfm";
                        oPedidoDespachado.ShippedTrackingProtocol = "DEVOLVIDO";
                        oPedidoDespachado.ShippedEstimatedDelivery = DateTime.Parse("04/07/2020 17:00").AddDays(10);

                        string sJsonProcessado = Newtonsoft.Json.JsonConvert.SerializeObject(oPedidoDespachado);

                        client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order");
                        request = new RestRequest(Method.PUT);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
                        request.AddParameter("application/json", sJsonProcessado, ParameterType.RequestBody);

                        oResposta = client.Execute(request);
                        #endregion

                    }

                    //DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oPedido.InvoicedKey + "'", null);

                    //if (d.Rows.Count > 0)
                    //{
                    //    continue;
                    //}

                    //SalvaBancoPDFZPLMagalu(oPedido);
                }
            }
        }
    }
}
