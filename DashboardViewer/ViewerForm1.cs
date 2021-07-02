using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraMap;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace DashboardViewer
{
    public partial class ViewerForm1 : XtraForm
    {
        public ViewerForm1()
        {
            InitializeComponent();
        }

        private void ViewerForm1_Load(object sender, EventArgs e)
        {
            string sFile = Directory.GetCurrentDirectory() + "\\Dashboard\\Acompanhar Impressões.xml";
            dashboardViewer.LoadDashboard(sFile);
        }
    }
}
