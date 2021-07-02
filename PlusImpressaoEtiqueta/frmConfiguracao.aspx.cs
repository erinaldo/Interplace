using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlusImpressaoEtiqueta
{
    public partial class frmConfiguracao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string sUsuario = Session["login"].ToString();
            string sImpressora = Request.QueryString["installedPrinterName"];
            string sSql = "UPDATE LOGIN SET IMPRESSORA = '" + sImpressora + "' WHERE USUARIO = '" + sUsuario + "'";

            Funcoes.ExecutabancoMySql(sSql);
        }
    }
}