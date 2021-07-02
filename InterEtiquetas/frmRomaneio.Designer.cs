namespace InterEtiquetas
{
    partial class frmRomaneio
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
            this.components = new System.ComponentModel.Container();
            this.gridNota = new DevExpress.XtraGrid.GridControl();
            this.tabNota = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfigurar = new DevExpress.XtraEditors.SimpleButton();
            this.editNota = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuRomaneio = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuExcluirNota = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridNota)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabNota)).BeginInit();
            this.metroPanel1.SuspendLayout();
            this.menuRomaneio.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridNota
            // 
            this.gridNota.ContextMenuStrip = this.menuRomaneio;
            this.gridNota.Location = new System.Drawing.Point(20, 63);
            this.gridNota.MainView = this.tabNota;
            this.gridNota.Name = "gridNota";
            this.gridNota.Size = new System.Drawing.Size(890, 356);
            this.gridNota.TabIndex = 0;
            this.gridNota.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tabNota});
            // 
            // tabNota
            // 
            this.tabNota.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.tabNota.GridControl = this.gridNota;
            this.tabNota.Name = "tabNota";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Cliente";
            this.gridColumn1.FieldName = "CLIENTE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 312;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Nota";
            this.gridColumn2.FieldName = "NUMERONOTA";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 153;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Destinatário";
            this.gridColumn3.FieldName = "DESTINATARIO";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 407;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.simpleButton1);
            this.metroPanel1.Controls.Add(this.btnConfigurar);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 514);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(896, 61);
            this.metroPanel1.TabIndex = 30;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButton1.Location = new System.Drawing.Point(0, 0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.simpleButton1.Size = new System.Drawing.Size(128, 61);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Sair";
            this.simpleButton1.Click += new System.EventHandler(this.SimpleButton1_Click);
            // 
            // btnConfigurar
            // 
            this.btnConfigurar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfigurar.Location = new System.Drawing.Point(768, 0);
            this.btnConfigurar.Name = "btnConfigurar";
            this.btnConfigurar.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnConfigurar.Size = new System.Drawing.Size(128, 61);
            this.btnConfigurar.TabIndex = 2;
            this.btnConfigurar.Text = "Imprimir";
            this.btnConfigurar.Click += new System.EventHandler(this.BtnConfigurar_Click);
            // 
            // editNota
            // 
            this.editNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editNota.Location = new System.Drawing.Point(84, 455);
            this.editNota.Name = "editNota";
            this.editNota.Size = new System.Drawing.Size(763, 45);
            this.editNota.TabIndex = 31;
            this.editNota.TextChanged += new System.EventHandler(this.EditNota_TextChanged);
            this.editNota.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditNota_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(421, 432);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 32;
            this.label1.Text = "Nota Fiscal";
            // 
            // menuRomaneio
            // 
            this.menuRomaneio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExcluirNota});
            this.menuRomaneio.Name = "menuRomaneio";
            this.menuRomaneio.Size = new System.Drawing.Size(181, 48);
            // 
            // menuExcluirNota
            // 
            this.menuExcluirNota.Name = "menuExcluirNota";
            this.menuExcluirNota.Size = new System.Drawing.Size(180, 22);
            this.menuExcluirNota.Text = "Excluir Nota";
            this.menuExcluirNota.Click += new System.EventHandler(this.menuExcluirNota_Click);
            // 
            // frmRomaneio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 595);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editNota);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.gridNota);
            this.Name = "frmRomaneio";
            this.Text = "Impressão Romaneio";
            this.Load += new System.EventHandler(this.FrmRomaneio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridNota)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabNota)).EndInit();
            this.metroPanel1.ResumeLayout(false);
            this.menuRomaneio.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridNota;
        private DevExpress.XtraGrid.Views.Grid.GridView tabNota;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnConfigurar;
        private System.Windows.Forms.TextBox editNota;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private System.Windows.Forms.ContextMenuStrip menuRomaneio;
        private System.Windows.Forms.ToolStripMenuItem menuExcluirNota;
    }
}