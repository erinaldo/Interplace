using InterRegraNegocio;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterGerente
{
    public partial class frmProdutoManutencao : InterRegraNegocio.frmGenericoAjuste
    {
        public frmProdutoManutencao()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {

            if (editProduto.Text == "")
            {
                MessageBox.Show(this, "Digite um produto!", ClasseParametros.NomeProjeto);
                return;

            }
            string sSql = "";

            sSql = "UPDATE PRODUTO SET DESCRICAO=@DESCRICAO, CARACTERISTICASTEC=@CARACTERISTICASTEC, ATUALIZADO=@ATUALIZADO,ATUALIZADOMAGALU=@ATUALIZADOMAGALU, TIPOANUNCIO=@TIPOANUNCIO WHERE ID = @ID";
            Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

            ParametrosSQL.Add("ID", editCodigo.Text);
            ParametrosSQL.Add("DESCRICAO", editProduto.Text);
            ParametrosSQL.Add("CARACTERISTICASTEC", editCaracteristicasTecnicas.Text);

            //if (((SimpleButton)(sender)).Name == "BtnSalvar")
            //    ParametrosSQL.Add("ATUALIZADO", "S");
            //else
            //    ParametrosSQL.Add("ATUALIZADO", "N");

            //if (((SimpleButton)(sender)).Name == "BtnSalvar")
            //    ParametrosSQL.Add("ATUALIZADOMAGALU", "S");
            //else
                ParametrosSQL.Add("ATUALIZADOMAGALU", "N");

            ParametrosSQL.Add("TIPOANUNCIO", comboTipoAnuncio.Text.Split('-')[0].Trim());
            ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);

            sSql = "DELETE FROM PRODUTOIMAGEM WHERE ID = '" + editCodigo.Text + "'";
            ClasseParametros.ExecutabancoMySql(sSql);

            //if (lstImagem.Images.Count > 0)
            //{
            //    int iImagem = 0;
            //    foreach (Bitmap oImage in lstImagem.Images)
            //    {
            //        ClasseFuncoes.EnviaImagemFTP(editCodigo1.Text, editCodigo.Text, oImage, iImagem);
            //        iImagem++;
            //    }

            //}

            Close();
        }

        private void FrmProdutoManutencao_Load(object sender, EventArgs e)
        {
            DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM TIPOANUNCIO");

            foreach (DataRow r in d.Rows)
            {
                comboTipoAnuncio.Items.Add(r["ID"].ToString() + " - " + r["NOME"].ToString());
            }

            d.Dispose();

            if (editCodigo.Text != "")
            {
                string sSql = "SELECT DESCRICAO, CARACTERISTICASTEC, ID,CODIGO FROM PRODUTO WHERE ID = '" + editCodigo.Text + "'";
                DataTable dtbCliente = ClasseParametros.ConsultaBancoMysql(sSql);
                if (dtbCliente.Rows.Count > 0)
                {
                    editCodigo.Text = dtbCliente.Rows[0]["ID"].ToString().Trim();
                    editCodigo1.Text = dtbCliente.Rows[0]["CODIGO"].ToString();
                    editProduto.Text = dtbCliente.Rows[0]["DESCRICAO"].ToString();
                    editCaracteristicasTecnicas.Text = dtbCliente.Rows[0]["CARACTERISTICASTEC"].ToString();
                }
            }

            this.ActiveControl = editProduto;
            editProduto.Focus();
            //CarregaImagens do FTP
            //lstImagem.Images.Clear();
            //List<Image> lstImagens = ClasseFuncoes.DownloadFileFromFTP(editCodigo1.Text);
            //foreach (Image oimage in lstImagens)
            //{
            //    lstImagem.Images.Add(oimage);
            //}
        }

        private void BtnAdicionarImagem_Click(object sender, EventArgs e)
        {
            if (dlgImagem.ShowDialog() == DialogResult.OK)
            {
                Bitmap oBitMap = new Bitmap(dlgImagem.FileName);
                //lstImagem.Images.Add(oBitMap);
            }
        }

        private void BtnDeletarImagem_Click(object sender, EventArgs e)
        {
        }
    }
}
