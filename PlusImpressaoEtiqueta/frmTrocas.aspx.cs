using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlusImpressaoEtiqueta
{
    public partial class frmTrocas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sSql = "SELECT B.PEDIDO, B.DATAPEDIDO, B.CPF FROM TROCASB2W B ";
            if (IsPostBack)
            {
                string sDataInicial = dInicial.Text ;
                string sDataFinal = dFinal.Text;
                if (sDataFinal != "" && sDataInicial != "")
                {
                    sSql += "WHERE B.DATAPEDIDO > '" + DateTime.Parse(sDataInicial).AddDays(-1).ToString("yyyy-MM-dd") + "' AND B.DATAPEDIDO < '" + DateTime.Parse(sDataFinal).AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
            }
            DataTable dtbTrocas = Funcoes.ConsultaBancoMysql(sSql);
            gridTrocas.DataSource = dtbTrocas;
            gridTrocas.DataBind();
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {

        }
    }
}