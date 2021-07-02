using InterRegraNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterGerente
{
    public partial class frmManutencaoGeral : Form
    {
        public frmManutencaoGeral()
        {
            InitializeComponent();
        }

        private void btnRetiraDuplicadoProduto_Click(object sender, EventArgs e)
        {
            DataTable d = ClasseParametros.ConsultaBancoMysql(@"SELECT P.CODIGO
                                                                FROM PRODUTO P
                                                                GROUP BY P.CODIGO
                                                                HAVING COUNT(*) > 1");
            int i = 1;

            ClasseParametros.barProgress.Maximum = d.Rows.Count;
            ClasseParametros.barProgress.Value = 0;

            foreach (DataRow r in d.Rows)
            {
                string sSql = "DELETE FROM VENDAS WHERE NOTAFISCAL = @NOTAFISCAL";
                Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();
                ParametrosSQL.Add("NOTAFISCAL", r["NOTAFISCAL"].ToString());
                ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);
                ClasseParametros.barProgress.Value = i;
                i++;
                ClasseParametros.barProgress.Refresh();
                ClasseParametros.barProgress.Refresh();
            }


            MessageBox.Show("acabou");
        }
    }
}
