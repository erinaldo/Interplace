using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GNRE
{
    public partial class frmMain : MetroFramework.Forms.MetroForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            object oServico = null;
            //if (toogleProducao.IsOn)
            //    ServiceReference2.GnreConfigUFSoapClient oServico = new ServiceReference2.GnreConfigUFSoapClient();

            //else
            //    ServiceReference1.GnreConfigUFSoapClient oServico = new ServiceReference1.GnreConfigUFSoapClient();

        }
    }
}
