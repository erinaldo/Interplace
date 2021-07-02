using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace InterRegraNegocio
{
    public partial class frmGenerico : MetroForm
    {
        public string _sComandoSQL = "";
        DataTable dtbGeral = new DataTable();
        public Properties.Settings Configuracao = new Properties.Settings();
        public Dictionary<string, object> oDict = new Dictionary<string, object>();
        public int iRetorno = -1;
        public int iLinhaSelecionada = -1;

        public frmGenerico()
        {
            InitializeComponent();
        }

        public void CarregaColunas()
        {
            //foreach (DataColumn oColuna in dtbGeral.Columns)
            //{
            //    GridColumn oColunaGrid = new GridColumn();
            //    oColunaGrid.Caption = oColuna.ColumnName;
            //    oColunaGrid.FieldName = oColuna.ColumnName;

            //    if (oColuna.DataType == typeof(Int32))
            //    {
            //        oColunaGrid.Width = 150;
            //    }
            //    else if (oColuna.DataType == typeof(Double))
            //    {
            //        oColunaGrid.DisplayFormat.FormatType = FormatType.Numeric;
            //        oColunaGrid.DisplayFormat.FormatString = "c";
            //    }

            //    tabGeral.Columns.Add(oColunaGrid);
            //}

            //tabGeral.BestFitColumns();

        }

        public void CarregaForm()
        {
            CarregaDados();

            try
            {
                ProcessControls(this, true);

            }
            catch
            {

            };
        }

        private void frmGenerico_Load(object sender, EventArgs e)
        {
            DisplayHeader = false;
            Width = 907;
            Height = 622;
        }

        public void CarregaDados()
        {
            //    dtbGeral = ClasseParametros.ConsultaBancoMysql(_sComandoSQL);
            //    gridGeral.DataSource = dtbGeral;
            //    CarregaColunas();
            //    tabGeral.OptionsView.ColumnAutoWidth = false;
            //    tabGeral.BestFitColumns();
        }

        private void ProcessControls(Control ctrlContainer, bool lAbrindo)
        {
            //    try
            //    {
            //        if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XML\\"))
            //        {
            //            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XML\\");
            //        }

            //        foreach (Control x in ctrlContainer.Controls)
            //        {
            //            //if (x.GetType() == typeof(GridControl))
            //            //{
            //            //    string sGrid = Directory.GetCurrentDirectory() + "\\XML\\" + x.Parent.Name + "_" + x.Name + ".xml";

            //            //    GridView oView = (GridView)((GridControl)x).DefaultView;

            //            //    OptionsLayoutGrid oLayout = new OptionsLayoutGrid();
            //            //    oLayout.StoreAllOptions = true;
            //            //    oLayout.StoreAppearance = true;
            //            //    oLayout.StoreVisualOptions = true;

            //            //    if (lAbrindo)
            //            //    {
            //            //        if (File.Exists(sGrid))
            //            //        {
            //            //            oView.RestoreLayoutFromXml(sGrid, oLayout);
            //            //        }
            //            //    }
            //            //    else
            //            //    {
            //            //        oView.SaveLayoutToXml(sGrid, oLayout);
            //            //    }


            //                //oView.OptionsView.ShowFooter = true;
            //                //oView.OptionsView.ShowAutoFilterRow = true;
            //                //OptionsLayoutGrid oLayout = new OptionsLayoutGrid();
            //                //oLayout.Columns.AddNewColumns = true;
            //                //oView.RestoreLayoutFromXml(sGrid, oLayout);
            //                x.DoubleClick += X_DoubleClick;
            //            }

            //            if (x.HasChildren)
            //                ProcessControls(x, lAbrindo);
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    };
        }

        //private void X_DoubleClick(object sender, EventArgs e)
        //{
        //    GridControl oGrid = (GridControl)sender;
        //    GridView oView = (GridView)((GridControl)oGrid).DefaultView;
        //}

        //private void frmGenerico_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    ProcessControls(this, false);
        //}

        //private void btnSair_Click(object sender, EventArgs e)
        //{
        //    Close();
        //}

        //private void pnlTopo_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void lblTopo_Click(object sender, EventArgs e)
        //{

        //}

        //private void imgTopo_EditValueChanged(object sender, EventArgs e)
        //{

        //}

        //private void pnlBottom_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void simpleButton3_Click(object sender, EventArgs e)
        //{

        //}

        //private void simpleButton2_Click(object sender, EventArgs e)
        //{

        //}

        //private void simpleButton1_Click(object sender, EventArgs e)
        //{

        //}

        //private void gridGeral_Click(object sender, EventArgs e)
        //{

        //}

        //private void pnlConteudo_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void frmGenerico_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyData == Keys.F2)
        //    {
        //        btnAdicionar.PerformClick();
        //    }
        //    else if (e.KeyData == Keys.F3)
        //    {
        //        btnDeletar.PerformClick();
        //    }
        //    else if (e.KeyData == Keys.F4)
        //    {
        //        btnEditar.PerformClick();
        //    }
        //    else if (e.KeyData == Keys.F5)
        //    {
        //        CarregaDados();
        //    }
        //    else if (e.KeyData == Keys.Escape)
        //    {
        //        //            // VALIDA SE O USUAIOR QUER MESMO SALVAR
        //        //            if (MessageBox.Show(this, "Deseja realmente sair?",
        //        //Parametros.NomeProjeto, System.Windows.Forms.MessageBoxButtons.YesNo,
        //        //System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
        //        //            {
        //        Close();
        //        //            }

        //    }
        //}

        //private void MenuGrid_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    iLinhaSelecionada = tabGeral.FocusedRowHandle;
        //}

        //private void TabGeral_DoubleClick(object sender, EventArgs e)
        //{
        //    btnEditar.PerformClick();
        //}
    }
}
