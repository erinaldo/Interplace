using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlusImpressaoEtiqueta
{
    public partial class frmLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            DataTable dtbLogin = Funcoes.ConsultaBancoMysql("SELECT * FROM LOGIN WHERE USUARIO = '" + editLogin.Text + "' AND SENHA = '" + editSenha.Text + "' ");
            if (dtbLogin.Rows.Count > 0)
            {
                Session["login"] = editLogin.Text;
                Response.Redirect("frmInicio.aspx");
            }
            else
            {
                lblStatus.Text = "Login não encontrado";
            }



        }
    }
}