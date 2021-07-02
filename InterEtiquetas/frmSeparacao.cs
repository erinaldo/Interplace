using InterRegraNegocio;
using MetroFramework.Forms;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterEtiquetas
{
    public partial class frmSeparacao : MetroForm
    {
        public frmSeparacao()
        {
            InitializeComponent();
        }

        private void frmSeparacao_Load(object sender, EventArgs e)
        {

        }

        private void editFiltrar_Click(object sender, EventArgs e)
        {
            if (editCNPJ.Text.Trim() == "")
            {
                return;
            }

            Dictionary<string, object> dictSeparacao = new Dictionary<string, object>();
            dictSeparacao.Add("CNPJ", editCNPJ.Text);

            string sSql = @"SELECT NM.CNPJ, ND.PRODUTO, ND.DESCRICAO, ND.SKU, SUM(ND.QUANTIDADE) AS QUANTIDADE, GROUP_CONCAT(DISTINCT NM.NOTA SEPARATOR '-') AS NOTAS
                                    FROM NOTAMASTER NM
                                    LEFT OUTER JOIN NOTADETALHE  ND
                                      ON NM.NOTA = ND.NOTA AND NM.CNPJ = ND.CNPJ
                                    WHERE NM.SEPARADO = 0 AND NM.CNPJ = @CNPJ
                                    GROUP BY CNPJ, PRODUTO,DESCRICAO,SKU";
            if (editSerieNota.Text.Trim() != string.Empty)
            {
                sSql = @"SELECT NM.CNPJ, ND.PRODUTO, ND.DESCRICAO, ND.SKU, SUM(ND.QUANTIDADE) AS QUANTIDADE, GROUP_CONCAT(DISTINCT NM.NOTA SEPARATOR '-') AS NOTAS
                                    FROM NOTAMASTER NM
                                    LEFT OUTER JOIN NOTADETALHE  ND
                                      ON NM.NOTA = ND.NOTA AND NM.CNPJ = ND.CNPJ
                                    WHERE NM.SEPARADO = 0 AND NM.CNPJ = @CNPJ AND NM.NOTASERIE = @NOTASERIE
                                    GROUP BY CNPJ, PRODUTO,DESCRICAO,SKU";
                dictSeparacao.Add("NOTASERIE", editSerieNota.Text);
            }

            DataTable dtbSeparacao = ClasseParametros.ConsultaBancoMysql(sSql, dictSeparacao);
            dictSeparacao = null;

            gridSeparacao.DataSource = dtbSeparacao;

            DataTable dtbCliente = ClasseParametros.ConsultaBancoMysql(@"SELECT * FROM CLIENTE WHERE CNPJCLIENTES = '" + editCNPJ.Text + "'");
            ClasseParametros.ClienteSelecionado = dtbCliente.Rows[0]["CODIGO"].ToString();
            dtbCliente.Dispose();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ArrayList aLinhas = new ArrayList();

            // Add the selected rows to the list.
            Int32[] selectedRowHandles = tabSeparacao.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                    aLinhas.Add(tabSeparacao.GetDataRow(selectedRowHandle));
            }

            try
            {
                int iSeparacao = ClasseParametros.PegaCodigo("SEPARACAO", ClasseParametros.ClienteSelecionado);

                for (int i = 0; i < aLinhas.Count; i++)
                {
                    DataRow row = aLinhas[i] as DataRow;

                    string[] aNotas = row["Notas"].ToString().Split('-');

                    foreach (string sNota in aNotas)
                    {
                        string sSql = "UPDATE NOTAMASTER SET SEPARADO = 1, DATASEPARACAO='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                        sSql += " CODIGOSEPARADOR = " + ClasseParametros.iCodigoUsuarioSistema.ToString();
                        sSql += " WHERE CNPJ = '" + row["CNPJ"].ToString() + "' AND NOTA = '" + sNota + "'";

                        ClasseParametros.ExecutabancoMySql(sSql);

                        sSql = "INSERT INTO SEPARACAO(CODIGO,NOTA,CNPJ) VALUES('" + iSeparacao.ToString() + "','" + sNota + "','" + row["CNPJ"].ToString() + "')";
                        ClasseParametros.ExecutabancoMySql(sSql);
                    }
                }
            }
            finally
            {
                aLinhas = null;

                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\separacao"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\separacao");
                }

                string sFile = Directory.GetCurrentDirectory() + "\\separacao\\separacao" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".csv";
                gridSeparacao.ExportToCsv(sFile);
                System.Diagnostics.Process.Start(sFile);

                editFiltrar.PerformClick();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (editCNPJMinhas.Text.Trim() == "")
            {
                return;
            }

            DataTable dtbSeparacao = ClasseParametros.ConsultaBancoMysql(@"
                                    SELECT NM.NOTA,NM.CNPJ, SE.CODIGO
FROM NOTAMASTER NM
  LEFT OUTER JOIN SEPARACAO SE
    ON NM.NOTA=SE.NOTA AND NM.CNPJ = SE.CNPJ
WHERE NM.CODIGOSEPARADOR = " + ClasseParametros.iCodigoUsuarioSistema.ToString() + " AND NM.CNPJ = '" + editCNPJMinhas.Text + "'");
            gridMinhas.DataSource = dtbSeparacao;


            DataTable dtbCliente = ClasseParametros.ConsultaBancoMysql(@"SELECT * FROM CLIENTE WHERE CNPJCLIENTES = '" + editCNPJMinhas.Text + "'");
            ClasseParametros.ClienteSelecionado = dtbCliente.Rows[0]["CODIGO"].ToString();
            dtbCliente.Dispose();
        }
    }
}
