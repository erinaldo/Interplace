using InterRegraNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterEtiquetas
{
    public partial class frmConfiguracao : MetroFramework.Forms.MetroForm
    {
        Properties.Settings oSett = new Properties.Settings();

        public frmConfiguracao()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            oSett.IPServidor = editIP.Text;
            oSett.Porta = int.Parse(editPorta.Text);
            oSett.PorArquivo = chkEmpresa.Checked;
            oSett.CNPJClientes = editCodigoEmpresa.Text;
            oSett.ImpressoraTermica = comboImpressoraTermica.Text;
            oSett.ImpressoraLaserJato = comboImpressoraLaserJato.Text;
            oSett.Save();

            int iRetratoPaisagem = 0;
            if (chkPaisagemRetrato.Checked)
                iRetratoPaisagem = 1;

            int iSKU = 0;
            if (chkSKU.Checked)
                iSKU = 1;

            string sSql = "UPDATE LOGIN SET IMPRESSAOEANSKU = " + iSKU.ToString() + ", IMPRESSAORETRATOROMANEIO = " + iRetratoPaisagem.ToString() + " WHERE CODIGO =" + ClasseParametros.iCodigoUsuarioSistema.ToString();

            ClasseParametros.ExecutabancoMySql(sSql);
            ClasseParametros.CarregaParametros();
        }

        private void FrmConfiguracao_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                string sImpressoras = PrinterSettings.InstalledPrinters[i];
                comboImpressoraTermica.Items.Add(sImpressoras);
                comboImpressoraLaserJato.Items.Add(sImpressoras);
            }

            comboImpressoraTermica.Text = oSett.ImpressoraTermica;
            comboImpressoraLaserJato.Text = oSett.ImpressoraLaserJato;
            editPorta.Text = oSett.Porta.ToString();
            editIP.Text = oSett.IPServidor;
            chkEmpresa.Checked = oSett.PorArquivo;
            editCodigoEmpresa.Text = oSett.CNPJClientes;

            DataTable dtbLogin = ClasseParametros.ConsultaBancoMysql("SELECT * FROM LOGIN WHERE CODIGO = " + ClasseParametros.iCodigoUsuarioSistema.ToString());
            if (dtbLogin.Rows.Count > 0)
            {
                chkSKU.Checked = dtbLogin.Rows[0]["IMPRESSAOEANSKU"].ToString() == "1";
                chkPaisagemRetrato.Checked = dtbLogin.Rows[0]["IMPRESSAORETRATOROMANEIO"].ToString() == "1";
            }
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
