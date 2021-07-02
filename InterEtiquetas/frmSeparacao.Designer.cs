namespace InterEtiquetas
{
    partial class frmSeparacao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.editFiltrar = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.editCNPJ = new System.Windows.Forms.TextBox();
            this.gridSeparacao = new DevExpress.XtraGrid.GridControl();
            this.tabSeparacao = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNOTA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCHAVENOTA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDATA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCNPJ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPRODUTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDESCRICAO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQUANTIDADE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOMECLIENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSKU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.gridMinhas = new DevExpress.XtraGrid.GridControl();
            this.tabMinhas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNOTA1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCNPJ1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCODIGO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.editCNPJMinhas = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.btnRomaneio = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.editSerieNota = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSeparacao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabSeparacao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMinhas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMinhas)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.editSerieNota);
            this.panel1.Controls.Add(this.editFiltrar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.editCNPJ);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1142, 136);
            this.panel1.TabIndex = 0;
            // 
            // editFiltrar
            // 
            this.editFiltrar.Location = new System.Drawing.Point(939, 67);
            this.editFiltrar.Name = "editFiltrar";
            this.editFiltrar.Size = new System.Drawing.Size(184, 53);
            this.editFiltrar.TabIndex = 4;
            this.editFiltrar.Text = "Filtrar";
            this.editFiltrar.Click += new System.EventHandler(this.editFiltrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "CNPJ";
            // 
            // editCNPJ
            // 
            this.editCNPJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.editCNPJ.Location = new System.Drawing.Point(16, 39);
            this.editCNPJ.Name = "editCNPJ";
            this.editCNPJ.Size = new System.Drawing.Size(265, 30);
            this.editCNPJ.TabIndex = 2;
            // 
            // gridSeparacao
            // 
            this.gridSeparacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSeparacao.Location = new System.Drawing.Point(0, 136);
            this.gridSeparacao.MainView = this.tabSeparacao;
            this.gridSeparacao.Name = "gridSeparacao";
            this.gridSeparacao.Size = new System.Drawing.Size(1142, 409);
            this.gridSeparacao.TabIndex = 2;
            this.gridSeparacao.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tabSeparacao});
            // 
            // tabSeparacao
            // 
            this.tabSeparacao.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNOTA,
            this.colCHAVENOTA,
            this.colDATA,
            this.colCNPJ,
            this.colPRODUTO,
            this.colDESCRICAO,
            this.colQUANTIDADE,
            this.colNOMECLIENTE,
            this.colSKU,
            this.colNotas});
            this.tabSeparacao.GridControl = this.gridSeparacao;
            this.tabSeparacao.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "NOTA", null, "")});
            this.tabSeparacao.Name = "tabSeparacao";
            this.tabSeparacao.OptionsBehavior.Editable = false;
            this.tabSeparacao.OptionsPrint.PrintSelectedRowsOnly = true;
            this.tabSeparacao.OptionsSelection.MultiSelect = true;
            this.tabSeparacao.OptionsView.ShowAutoFilterRow = true;
            this.tabSeparacao.OptionsView.ShowFooter = true;
            // 
            // colNOTA
            // 
            this.colNOTA.FieldName = "NOTA";
            this.colNOTA.Name = "colNOTA";
            // 
            // colCHAVENOTA
            // 
            this.colCHAVENOTA.FieldName = "CHAVENOTA";
            this.colCHAVENOTA.Name = "colCHAVENOTA";
            // 
            // colDATA
            // 
            this.colDATA.FieldName = "DATA";
            this.colDATA.Name = "colDATA";
            // 
            // colCNPJ
            // 
            this.colCNPJ.FieldName = "CNPJ";
            this.colCNPJ.Name = "colCNPJ";
            // 
            // colPRODUTO
            // 
            this.colPRODUTO.FieldName = "PRODUTO";
            this.colPRODUTO.Name = "colPRODUTO";
            this.colPRODUTO.Visible = true;
            this.colPRODUTO.VisibleIndex = 1;
            this.colPRODUTO.Width = 239;
            // 
            // colDESCRICAO
            // 
            this.colDESCRICAO.FieldName = "DESCRICAO";
            this.colDESCRICAO.Name = "colDESCRICAO";
            this.colDESCRICAO.Visible = true;
            this.colDESCRICAO.VisibleIndex = 2;
            this.colDESCRICAO.Width = 622;
            // 
            // colQUANTIDADE
            // 
            this.colQUANTIDADE.FieldName = "QUANTIDADE";
            this.colQUANTIDADE.Name = "colQUANTIDADE";
            this.colQUANTIDADE.Visible = true;
            this.colQUANTIDADE.VisibleIndex = 3;
            this.colQUANTIDADE.Width = 118;
            // 
            // colNOMECLIENTE
            // 
            this.colNOMECLIENTE.Caption = "Cliente";
            this.colNOMECLIENTE.FieldName = "NOMECLIENTE";
            this.colNOMECLIENTE.Name = "colNOMECLIENTE";
            // 
            // colSKU
            // 
            this.colSKU.Caption = "SKU";
            this.colSKU.FieldName = "SKU";
            this.colSKU.Name = "colSKU";
            this.colSKU.Visible = true;
            this.colSKU.VisibleIndex = 0;
            this.colSKU.Width = 138;
            // 
            // colNotas
            // 
            this.colNotas.Caption = "NOTAS";
            this.colNotas.FieldName = "NOTAS";
            this.colNotas.Name = "colNotas";
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(20, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.xtraTabPage1;
            this.tabControl.Size = new System.Drawing.Size(1144, 667);
            this.tabControl.TabIndex = 4;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gridSeparacao);
            this.xtraTabPage1.Controls.Add(this.panel2);
            this.xtraTabPage1.Controls.Add(this.panel1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1142, 642);
            this.xtraTabPage1.Text = "Separações";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.simpleButton2);
            this.panel2.Controls.Add(this.simpleButton1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 545);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1142, 97);
            this.panel2.TabIndex = 4;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButton2.Location = new System.Drawing.Point(0, 0);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(184, 97);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Sair";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButton1.Location = new System.Drawing.Point(958, 0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(184, 97);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Gerar Separação";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.gridMinhas);
            this.xtraTabPage2.Controls.Add(this.panel4);
            this.xtraTabPage2.Controls.Add(this.panel3);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1142, 642);
            this.xtraTabPage2.Text = "Minhas Separações";
            // 
            // gridMinhas
            // 
            this.gridMinhas.DataMember = "Query";
            this.gridMinhas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMinhas.Location = new System.Drawing.Point(0, 136);
            this.gridMinhas.MainView = this.tabMinhas;
            this.gridMinhas.Name = "gridMinhas";
            this.gridMinhas.Size = new System.Drawing.Size(1142, 409);
            this.gridMinhas.TabIndex = 7;
            this.gridMinhas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tabMinhas});
            // 
            // tabMinhas
            // 
            this.tabMinhas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNOTA1,
            this.colCNPJ1,
            this.colCODIGO});
            this.tabMinhas.GridControl = this.gridMinhas;
            this.tabMinhas.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "NOTA", null, "")});
            this.tabMinhas.Name = "tabMinhas";
            this.tabMinhas.OptionsBehavior.Editable = false;
            this.tabMinhas.OptionsPrint.PrintSelectedRowsOnly = true;
            this.tabMinhas.OptionsSelection.MultiSelect = true;
            this.tabMinhas.OptionsView.ShowAutoFilterRow = true;
            this.tabMinhas.OptionsView.ShowFooter = true;
            // 
            // colNOTA1
            // 
            this.colNOTA1.FieldName = "NOTA";
            this.colNOTA1.Name = "colNOTA1";
            this.colNOTA1.Visible = true;
            this.colNOTA1.VisibleIndex = 0;
            // 
            // colCNPJ1
            // 
            this.colCNPJ1.FieldName = "CNPJ";
            this.colCNPJ1.Name = "colCNPJ1";
            this.colCNPJ1.Visible = true;
            this.colCNPJ1.VisibleIndex = 1;
            // 
            // colCODIGO
            // 
            this.colCODIGO.FieldName = "CODIGO";
            this.colCODIGO.Name = "colCODIGO";
            this.colCODIGO.Visible = true;
            this.colCODIGO.VisibleIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.simpleButton4);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.editCNPJMinhas);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1142, 136);
            this.panel4.TabIndex = 6;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(939, 67);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(184, 53);
            this.simpleButton4.TabIndex = 4;
            this.simpleButton4.Text = "Filtrar";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(16, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "CNPJ";
            // 
            // editCNPJMinhas
            // 
            this.editCNPJMinhas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.editCNPJMinhas.Location = new System.Drawing.Point(16, 39);
            this.editCNPJMinhas.Name = "editCNPJMinhas";
            this.editCNPJMinhas.Size = new System.Drawing.Size(265, 30);
            this.editCNPJMinhas.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.simpleButton3);
            this.panel3.Controls.Add(this.btnRomaneio);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 545);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1142, 97);
            this.panel3.TabIndex = 5;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButton3.Location = new System.Drawing.Point(0, 0);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(184, 97);
            this.simpleButton3.TabIndex = 5;
            this.simpleButton3.Text = "Sair";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnRomaneio
            // 
            this.btnRomaneio.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRomaneio.Location = new System.Drawing.Point(958, 0);
            this.btnRomaneio.Name = "btnRomaneio";
            this.btnRomaneio.Size = new System.Drawing.Size(184, 97);
            this.btnRomaneio.TabIndex = 4;
            this.btnRomaneio.Text = "Gerar Romaneio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label3.Location = new System.Drawing.Point(287, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Série Nota";
            // 
            // editSerieNota
            // 
            this.editSerieNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.editSerieNota.Location = new System.Drawing.Point(287, 39);
            this.editSerieNota.Name = "editSerieNota";
            this.editSerieNota.Size = new System.Drawing.Size(104, 30);
            this.editSerieNota.TabIndex = 5;
            // 
            // frmSeparacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 747);
            this.Controls.Add(this.tabControl);
            this.Name = "frmSeparacao";
            this.Text = "Separação";
            this.Load += new System.EventHandler(this.frmSeparacao_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSeparacao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabSeparacao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMinhas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMinhas)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridSeparacao;
        private DevExpress.XtraGrid.Views.Grid.GridView tabSeparacao;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox editCNPJ;
        private DevExpress.XtraEditors.SimpleButton editFiltrar;
        private DevExpress.XtraGrid.Columns.GridColumn colNOTA;
        private DevExpress.XtraGrid.Columns.GridColumn colCHAVENOTA;
        private DevExpress.XtraGrid.Columns.GridColumn colDATA;
        private DevExpress.XtraGrid.Columns.GridColumn colCNPJ;
        private DevExpress.XtraGrid.Columns.GridColumn colPRODUTO;
        private DevExpress.XtraGrid.Columns.GridColumn colDESCRICAO;
        private DevExpress.XtraGrid.Columns.GridColumn colQUANTIDADE;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton btnRomaneio;
        private DevExpress.XtraGrid.GridControl gridMinhas;
        private DevExpress.XtraGrid.Views.Grid.GridView tabMinhas;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox editCNPJMinhas;
        private DevExpress.XtraGrid.Columns.GridColumn colNOTA1;
        private DevExpress.XtraGrid.Columns.GridColumn colCNPJ1;
        private DevExpress.XtraGrid.Columns.GridColumn colCODIGO;
        private DevExpress.XtraGrid.Columns.GridColumn colNOMECLIENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colSKU;
        private DevExpress.XtraGrid.Columns.GridColumn colNotas;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox editSerieNota;
    }
}