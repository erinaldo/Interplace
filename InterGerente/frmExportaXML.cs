using interRegraNegocio.FortePlus;
using InterRegraNegocio;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterGerente
{
    public partial class frmExportaXML : Form
    {
        public frmExportaXML()
        {
            InitializeComponent();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            IRestResponse oResposta = null;

            while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
            {
                RestClient client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Transmissao");
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
            List<FortPlusXML> oListXML = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FortPlusXML>>(oResposta.Content);
            List<FortPlusXML> oListXMLFiltrado = oListXML.Where(x => x.dmaInclusao > DateTime.Parse(dataInicial.Value.ToString("dd/MM/yyyy") + " 00:00:00")
            && x.dmaInclusao < DateTime.Parse(dataFinal.Value.ToString("dd/MM/yyyy") + " 23:59:59")
            && x.trTipo.Trim() == "A"
            && x.trStatus.Trim() == "1").ToList();

            string sExportado = Directory.GetCurrentDirectory() + "\\XMLExportado";

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLExportado"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLExportado");
            }

            string sPastaHoje = sExportado + "\\" + DateTime.Now.ToShortDateString().Replace("/", "_");
            if (!Directory.Exists(sPastaHoje))
            {
                Directory.CreateDirectory(sPastaHoje);
            }

            foreach (FortPlusXML oXML in oListXMLFiltrado)
            {
                File.WriteAllText(sPastaHoje + "\\" + oXML.trChaveAcesso + ".xml", oXML.trArquivoRetorno);




            }



            MessageBox.Show("Finalizado");













        }

        private void button1_Click(object sender, EventArgs e)
        {

            IRestResponse oResposta = null;

            while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
            {
                RestClient client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Transmissao");
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
            List<FortPlusXML> oListXML = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FortPlusXML>>(oResposta.Content);

            DataTable dtbNotasDIFAL = ClasseParametros.ConsultaBancoMysql("SELECT * FROM DIFAL D WHERE D.STATUS = 1");



            string sExportado = Directory.GetCurrentDirectory() + "\\XMLExportadodifal";

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLExportadodifal"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLExportadodifal");
            }

            string sPastaHoje = sExportado + "\\" + DateTime.Now.ToShortDateString().Replace("/", "_");
            if (!Directory.Exists(sPastaHoje))
            {
                Directory.CreateDirectory(sPastaHoje);
            }

            foreach (DataRow r in dtbNotasDIFAL.Rows)
            {
                List<FortPlusXML> oListXMLFiltrado = oListXML.Where(x => x.trDocto.ToString()==r["CODIGO"].ToString()).ToList();
                if(oListXMLFiltrado.Count==1)
                {
                    File.WriteAllText(sPastaHoje + "\\" + oListXMLFiltrado[0].trChaveAcesso + ".xml", oListXMLFiltrado[0].trArquivoRetorno);



                }







            }










            MessageBox.Show("Finalizado");




        }
    }
}
