using InterRegraNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InterGerente
{
    public partial class frmConfiguracao : InterRegraNegocio.frmGenericoAjuste
    {
        public frmConfiguracao()
        {
            InitializeComponent();
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            ClasseFuncoes.AtualizaTipoAnuncioAsync();
        }
    }
}
