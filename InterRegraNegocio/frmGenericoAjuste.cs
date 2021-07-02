
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace InterRegraNegocio
{
    public partial class frmGenericoAjuste : MetroForm
    {
        public Dictionary<string, object> oDict = new Dictionary<string, object>();
        public Properties.Settings Configuracao = new Properties.Settings();
        public int iRetorno = -1;
        public bool lEdicao = false;
        public bool lPulaCampo = true;

        public frmGenericoAjuste()
        {
            InitializeComponent();
            //(new TabOrderManager(this)).SetTabOrder(TabOrderManager.TabScheme.AcrossFirst);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (lEdicao)
            {
                // VALIDA SE O USUAIOR QUER MESMO SALVAR
                if (MessageBox.Show(this, "Deseja realmente sair sem salvar?", ClasseParametros.NomeProjeto, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        private void frmGenericoAjuste_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if(lPulaCampo)
        //        this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
        //        lPulaCampo = true;
        //    }
        //    else if (e.KeyData == Keys.F6)
        //    {
        //        btnSalvar.PerformClick();
        //    }

        //    else if (e.KeyData == Keys.Escape)
        //    {
        //        if (lEdicao)
        //        {
        //            // VALIDA SE O USUAIOR QUER MESMO SALVAR
        //            if (MessageBox.Show(this, "Deseja realmente sair?",
        //ClasseParametros.NomeProjeto, System.Windows.Forms.MessageBoxButtons.YesNo,
        //System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
        //            {
        //                btnSair.PerformClick();
        //            }
        //        }
        //        else
        //        {
        //            btnSair.PerformClick();
        //        }
        //    }
        }

        private void frmGenericoAjuste_Activated(object sender, EventArgs e)
        {
            try
            {
                Control oControle = ActiveControl;
                ActiveControl = oControle;
                oControle.Focus();
            }
            catch
            {

            }

        }

        private void frmGenericoAjuste_Shown(object sender, EventArgs e)
        {

        }

        private void frmGenericoAjuste_Load(object sender, EventArgs e)
        {

        }

        private void FrmGenericoAjuste_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dictionary<int, Control> lstControle = new Dictionary<int, Control>();
            ClasseParametros.PegaControles(this, ref lstControle);
            foreach (KeyValuePair<int, Control> oKeyValue in lstControle)
            {
                //if (oKeyValue.Value.GetType() == typeof(GridControl))
                //{
                //    if (Directory.Exists(Directory.GetCurrentDirectory() + "\\XML\\" + ((Control)(oKeyValue.Value)).Parent.Name + "_" + ((Control)(oKeyValue.Value)).Name))
                //    {
                //        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XML\\" + ((Control)(oKeyValue.Value)).Parent.Name + "_" + ((Control)(oKeyValue.Value)).Name);
                //    }


                //}
            }
            lstControle.Clear();
        }


    }
}
