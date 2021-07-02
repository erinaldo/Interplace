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

namespace InterEtiquetas
{
    public partial class frmLogin : MetroFramework.Forms.MetroForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            DataTable dtbLogin = ClasseParametros.ConsultaBancoMysql("SELECT * FROM LOGIN WHERE USUARIO = '" + editUsuario.Text + "' AND SENHA = '" + editSenha.Text + "'");

            if (dtbLogin.Rows.Count > 0)
            {
                ClasseParametros.sUsuarioSistema = dtbLogin.Rows[0]["USUARIO"].ToString();
                ClasseParametros.iCodigoUsuarioSistema = int.Parse(dtbLogin.Rows[0]["CODIGO"].ToString());
                ClasseParametros.lPermiteReimpressao = int.Parse(dtbLogin.Rows[0]["PERMITEREIMPRESSAO"].ToString()) == 1;
                ClasseParametros.lImpressaoRomaneioRetrato = int.Parse(dtbLogin.Rows[0]["IMPRESSAORETRATOROMANEIO"].ToString()) == 1;
                ClasseParametros.lImpressaoPorSKU = int.Parse(dtbLogin.Rows[0]["IMPRESSAOEANSKU"].ToString()) == 1;

                DialogResult = DialogResult.OK;
            }
            else
            {
                lblAlerta.Text = "Login não encontrado";
            }
            dtbLogin.Dispose();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.No;

        }

        private void editSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                btnLogar.PerformClick();
        }
    }
}
