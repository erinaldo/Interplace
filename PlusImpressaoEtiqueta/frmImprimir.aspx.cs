using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlusImpressaoEtiqueta
{
    public partial class frmImprimir : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sCNPJ = Request.QueryString["cnpj"];
            string sProduto = Request.QueryString["produto"];

            if (sProduto == "SEM")
                sProduto = "SEM GTIN";

            string sEtiqueta = Funcoes.RetornaEtiqueta(sCNPJ, sProduto);

            string sImpressora = Funcoes.ConsultaBancoMysql("SELECT IMPRESSORA FROM LOGIN WHERE USUARIO = '"+Session["login"].ToString()+"'").Rows[0]["IMPRESSORA"].ToString();

            sEtiqueta = sEtiqueta.Replace("\n", "").Replace("\r", "");

            ScriptManager.RegisterClientScriptBlock(
                Page,
                Page.GetType(),
                "mensagem",
                "<script type='text/javascript'> \n" + 
                "JSPM.JSPrintManager.auto_reconnect = true;\n" +
                "JSPM.JSPrintManager.start();\n" +
                "JSPM.JSPrintManager.WS.onStatusChanged = function() {\n" +
                "           if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Open)\n" +
                "           {\n" +
                "print('" + sImpressora + "', '" + sEtiqueta + "'); "+"}"+"};\n" +
                "function jspmWSStatus()\n" +
                "{\n" +
                "    if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Open)\n" +
                "           return true;\n" +
                "       else if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Closed)\n" +
                "       {\n" +
                "           alert('JSPrintManager (JSPM) is not installed or not running! Download JSPM Client App from https://neodynamic.com/downloads/jspm');\n" +
                "           return false;\n" +
                "       }\n" +
                "       else if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Blocked)\n" +
                "       {\n" +
                "           alert('JSPM has blocked this website!');\n" +
                "           return false;\n" +
                "       }\n" +
                "   }\n" +
                "\n" +
                "   function print(sImpressora, sEtiqueta)\n" +
                "   {\n" +
                "       if (jspmWSStatus())\n" +
                "       {\n" +
                "           var cpj = new JSPM.ClientPrintJob();\n" +
                "           cpj.clientPrinter = new JSPM.InstalledPrinter(sImpressora);\n" +
                "           var cmds = sEtiqueta;\n" +
                "           cpj.printerCommands = cmds;\n" +
                "           cpj.sendToClient();\n" +
                "       }\n" +
                "   }</script>", false);
        }
    }
}