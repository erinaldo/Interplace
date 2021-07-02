using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlusImpressaoEtiqueta
{
    public partial class frmImpressaoEtq : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UpdatePanel1.Update();
        }

        protected void btnIprime_Click(object sender, EventArgs e)
        {
            string sCNPJ = editCNPJ.Text;
            string sProduto = editProduto.Text;

            if (sProduto == "SEM")
                sProduto = "SEM GTIN";

            string sEtiqueta = Funcoes.RetornaEtiqueta(sCNPJ, sProduto);

            string sImpressora = Funcoes.ConsultaBancoMysql("SELECT IMPRESSORA FROM LOGIN WHERE USUARIO = '" + Session["login"].ToString() + "'").Rows[0]["IMPRESSORA"].ToString();

            sEtiqueta = sEtiqueta.Replace("\n", "").Replace("\r", "");

            editEtiqueta.Text = sEtiqueta;
            editImpressora.Text = sImpressora;

            editProduto.Text = "";
            editProduto.Focus();

            ScriptManager.RegisterClientScriptBlock(
    Page,
    Page.GetType(),
    "mensagem",
    "<script type='text/javascript'> print('" + sImpressora + "', '" + sEtiqueta + "') </script> ",
    false);


        }
    }
}