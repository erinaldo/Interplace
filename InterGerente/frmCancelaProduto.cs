using InterRegraNegocio;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InterGerente
{
    public partial class frmCancelaProduto : InterRegraNegocio.frmGenericoAjuste
    {
        public frmCancelaProduto()
        {
            InitializeComponent();
        }

        private void pnlBottom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = new RestClient("https://api-integra.azurewebsites.net/api/SKU/" + textBox1.Text);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
            var response = client.Execute(request);


            InterRegraNegocio.MagazineLuiza.SKUProduto oProduto = Newtonsoft.Json.JsonConvert.DeserializeObject<InterRegraNegocio.MagazineLuiza.SKUProduto>(response.Content);
            oProduto.IdSku = textBox1.Text;
            oProduto.Status = false;

            oProduto.StockQuantity = 0;
            oProduto.MainImageUrl = "http://interplacelog.com.br/imagens/semimagem.jpg";
            // Imagens
            oProduto.UrlImages = new string[1];
            oProduto.UrlImages[0] = "http://interplacelog.com.br/imagens/semimagem.jpg";
            // Atributos
            //oProdutoMAGALUSKU.Attributes = new Attribute[1];
            InterRegraNegocio.MagazineLuiza.Attribute oAtributoSKU = new InterRegraNegocio.MagazineLuiza.Attribute();
            //if (lPar)
            //{
            //    oAtributoSKU.Name = "VOLTAGEM";
            //    oAtributoSKU.Value = "110V";
            //}

            //oProdutoMAGALUSKU.Attributes[0] = oAtributoSKU;
            string sJsonProduto = Newtonsoft.Json.JsonConvert.SerializeObject(oProduto);

            #region ENVIA SKU
            client = new RestClient("https://api-integra.azurewebsites.net/api/Sku");
            request = new RestRequest(Method.PUT);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic " + ClasseParametros.sTokenMAGALU);
            request.AddParameter("application/json", sJsonProduto, ParameterType.RequestBody);

            response = client.Execute(request);
            #endregion
        }

    }
}
