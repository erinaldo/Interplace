using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterEtiquetas
{
    public partial class frmProdutosAdicionais : MetroFramework.Forms.MetroForm
    {

        public int iQtd = -1;
        public frmProdutosAdicionais()
        {
            InitializeComponent();
        }

        private void btnConferir_Click(object sender, EventArgs e)
        {
            if (editEtiqueta.Text == lblCodigo.Text)
            {
                iQtd--;
                lblQuantidade.Text = "X " + iQtd.ToString();
                editEtiqueta.Text = "";
                editEtiqueta.Focus();

            }

            if (iQtd == 0)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void frmProdutosAdicionais_Load(object sender, EventArgs e)
        {
            editEtiqueta.Focus();
        }

        private void editEtiqueta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                btnConferir.PerformClick();
            }
        }
    }
}
