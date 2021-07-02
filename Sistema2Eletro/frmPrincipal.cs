using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sistema2Eletro
{
    public partial class frmPrincipal : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            if (!containerGeral.Controls.Contains(ucBradescoGNRE.instance))
            {
                containerGeral.Controls.Add(ucBradescoGNRE.instance);
                ucBradescoGNRE.instance.Dock = DockStyle.Fill;
                ucBradescoGNRE.instance.BringToFront();
            }
            ucBradescoGNRE.instance.BringToFront();
        }
    }
}
