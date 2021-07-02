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
    public partial class frmProduto : InterRegraNegocio.frmGenerico
    {
        public frmProduto()
        {
            InitializeComponent();
            _sComandoSQL = "SELECT ID, CODIGO, DESCRICAO FROM PRODUTO ";
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            frmProdutoManutencao oFrm = new frmProdutoManutencao();
            oFrm.ShowDialog();
            oFrm.Dispose();
            CarregaDados();
        }

        private void BtnDeletar_Click(object sender, EventArgs e)
        {

        }

        private void FrmProduto_Load(object sender, EventArgs e)
        {
            CarregaDados();
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
           // ClasseFuncoes.AtualizaProdutos();
            CarregaDados();
        }

        private void BtnEditar_Click_1(object sender, EventArgs e)
        {
            frmProdutoManutencao oFrm = new frmProdutoManutencao();
            //oFrm.editCodigo.Text = tabGeral.GetFocusedRowCellDisplayText("ID");
            oFrm.ShowDialog();
            CarregaDados();
        }
    }
}
