using InterRegraNegocio;
using InterRegraNegocio.Bling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Relatorios
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGerarVendaCusto_Click(object sender, EventArgs e)
        {

            int i = 1;
            string sDataInicial = dateInicial.Value.ToString("dd/MM/yyyy");
            string sDataFinal = dateFinal.Value.ToString("dd/MM/yyyy");

            while (true)
            {
                var request = HttpWebRequest.Create(@"https://bling.com.br/Api/v2/pedidos/page=" + i.ToString() + "/json&apikey=" + ClasseParametros.sTokenEshopBling +"");

                //var request = HttpWebRequest.Create(@"https://bling.com.br/Api/v2/notasfiscais/page=" + i.ToString() + "/json&apikey=" + ClasseParametros.sTokenBling +
                // "&filters=dataEmissao[" + sDataInicial + " 00:00:00 TO " + sDataFinal + " 23:59:59]; situacao[6];loja[todas];tipo[S]");



                request.ContentType = "application/json";
                request.Method = "GET";
                string sNotas = "";
                var oNotas = "";

                string sSql = "";
                Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        return;
                        

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(content))
                            Console.Out.WriteLine("Empty Response");
                        else
                            sNotas = content;
                    }
                }

                PedidoJsonBling oListXML = Newtonsoft.Json.JsonConvert.DeserializeObject<PedidoJsonBling>(sNotas);

                barProgresso.Maximum = oListXML.retorno.pedidos.Length;
                barProgresso.Value = 0;
                lblPage.Text = i.ToString();

                barProgresso.Refresh();
                lblPage.Refresh();

                foreach (PedidoBling oPedido in oListXML.retorno.pedidos)
                {
                    DataTable dtbResult = null;
                    try
                    {
                        if (oPedido.pedido.nota == null)
                            continue;

                        int iNOTA = int.Parse(oPedido.pedido.nota.numero);
                        string sPEDIDO = oPedido.pedido.numero;
                        string sDATA = DateTime.Parse(oPedido.pedido.data).ToString("yyyy-MM-dd HH:mm:ss");
                        string sPEDIDOMARKETPLACE = oPedido.pedido.numeroPedidoLoja;
                        string sMARKETPLACE = oPedido.pedido.tipoIntegracao;
                        decimal eDESCONTO = decimal.Parse(oPedido.pedido.desconto.Replace(".", ","));
                        decimal eFRETE = decimal.Parse(oPedido.pedido.valorfrete.Replace(".", ","));
                        decimal eVALORPEDIDO = decimal.Parse(oPedido.pedido.totalvenda.Replace(".", ","));
                        decimal eVALORNOTA = decimal.Parse(oPedido.pedido.nota.valorNota.Replace(".", ","));
                        decimal eVALORPRODUTO = decimal.Parse(oPedido.pedido.totalprodutos.Replace(".", ","));
                        decimal eCUSTOPRODUTO = 0;
                        string sLOJACENTRAL = "ESHOP";

                        foreach (InterRegraNegocio.Bling.Iten oItem in oPedido.pedido.itens)
                        {
                            eCUSTOPRODUTO += decimal.Parse(oItem.item.precocusto.Replace(".", ","));
                        }

                        sSql = "SELECT * FROM RELATORIOVENDACUSTO WHERE NOTA = '" + iNOTA.ToString() + "' AND PEDIDO = '" + sPEDIDO + "' ";
                        dtbResult = ClasseParametros.ConsultaBancoMysql(sSql);
                        if (dtbResult.Rows.Count == 0)
                        {
                            sSql = "INSERT INTO RELATORIOVENDACUSTO(NOTA,PEDIDO,DATA,PEDIDOMARKETPLACE,MARKETPLACE,DESCONTO,FRETE,VALORPEDIDO,VALORNOTA,VALORPRODUTO,CUSTOPRODUTO,LOJACENTRAL) ";
                            sSql += " VALUES(@NOTA,@PEDIDO,@DATA,@PEDIDOMARKETPLACE,@MARKETPLACE,@DESCONTO,@FRETE,@VALORPEDIDO,@VALORNOTA,@VALORPRODUTO,@CUSTOPRODUTO,@LOJACENTRAL)";

                            ParametrosSQL.Add("NOTA", iNOTA);
                            ParametrosSQL.Add("PEDIDO", sPEDIDO);
                            ParametrosSQL.Add("DATA", sDATA);
                            ParametrosSQL.Add("PEDIDOMARKETPLACE", sPEDIDOMARKETPLACE);
                            ParametrosSQL.Add("MARKETPLACE", sMARKETPLACE);
                            ParametrosSQL.Add("DESCONTO", eDESCONTO);
                            ParametrosSQL.Add("FRETE", eFRETE);
                            ParametrosSQL.Add("VALORPEDIDO", eVALORPEDIDO);
                            ParametrosSQL.Add("VALORNOTA", eVALORNOTA);
                            ParametrosSQL.Add("VALORPRODUTO", eVALORPRODUTO);
                            ParametrosSQL.Add("CUSTOPRODUTO", eCUSTOPRODUTO);
                            ParametrosSQL.Add("LOJACENTRAL", sLOJACENTRAL);

                            ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);
                        }

                        lblLog.Text = "Pág: " + i.ToString() + " / " + "Nota: " + iNOTA.ToString();
                        lblLog.Refresh();






                    }
                    catch
                    {

                    }
                    finally
                    {
                        if (dtbResult != null)
                            dtbResult.Dispose();

                        barProgresso.Value++;
                    }
                }
                i++;
            }
        }

        public static Produto1 RetornaProdutoBling(string sProduto)
        {
            var request = HttpWebRequest.Create(@"https://bling.com.br/Api/v2/produtos/"+ sProduto + "/json&apikey="+ ClasseParametros.sTokenEshopBling);
            request.ContentType = "application/json";
            request.Method = "GET";
            string sProdutos = "";
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
                        sProdutos = content;
                }
            }


            Produto1 oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<Produto1>(sProdutos);
            return oProduto;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dlgArquivo.ShowDialog() == DialogResult.OK)
            {
                editArquivo.Text = dlgArquivo.FileName;
            }

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (editArquivo.Text == "")
                MessageBox.Show("Escolha um arquivo");


        }

        private void button3_Click(object sender, EventArgs e)
        {


















        }
    }
}
