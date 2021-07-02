using FastReport.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlusImpressaoEtiqueta
{
    public partial class frmSeparacaoProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtbSeparacao = Funcoes.ConsultaBancoMysql(@"
                    SELECT R.NOTA,R.CHAVENOTA,R.CNPJ,D.PRODUTO,R.SEPARADO
                    FROM NOTAMASTER R
                    LEFT OUTER JOIN NOTADETALHE D
                        ON R.NOTA = D.NOTA 
                    WHERE R.SEPARADO IS NULL OR R.SEPARADO = 0
                        ORDER BY NOTA ASC");
                gridSeparacao.DataSource = dtbSeparacao;
                gridSeparacao.DataBind();
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string sPedidos = "";

            foreach (GridViewRow row in gridSeparacao.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (((CheckBox)row.FindControl("chkRow")).Checked)
                    {
                        if (sPedidos != "")
                            sPedidos += ",";
                        sPedidos += row.Cells[0].Text;
                    }
                }
            }

            WebReport oRelatorio = new WebReport();
            oRelatorio.Report.Load("/Models/Separacao.frx");
            oRelatorio.Report.Prepare();






        }

    }
}