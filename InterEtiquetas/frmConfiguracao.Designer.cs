namespace InterEtiquetas
{
    partial class frmConfiguracao
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
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvar = new DevExpress.XtraEditors.SimpleButton();
            this.label6 = new System.Windows.Forms.Label();
            this.editCodigoEmpresa = new System.Windows.Forms.TextBox();
            this.chkEmpresa = new System.Windows.Forms.CheckBox();
            this.comboImpressoraLaserJato = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.editPorta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.editIP = new System.Windows.Forms.TextBox();
            this.comboImpressoraTermica = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkPaisagemRetrato = new System.Windows.Forms.CheckBox();
            this.chkSKU = new System.Windows.Forms.CheckBox();
            this.metroPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.simpleButton1);
            this.metroPanel1.Controls.Add(this.btnSalvar);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 369);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(408, 61);
            this.metroPanel1.TabIndex = 19;
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
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Sair";
            this.simpleButton1.Click += new System.EventHandler(this.SimpleButton1_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSalvar.Location = new System.Drawing.Point(280, 0);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSalvar.Size = new System.Drawing.Size(128, 61);
            this.btnSalvar.TabIndex = 2;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "CNPJ Empresa (Separados por \',\')";
            this.label6.Visible = false;
            // 
            // editCodigoEmpresa
            // 
            this.editCodigoEmpresa.Location = new System.Drawing.Point(8, 119);
            this.editCodigoEmpresa.Name = "editCodigoEmpresa";
            this.editCodigoEmpresa.Size = new System.Drawing.Size(287, 20);
            this.editCodigoEmpresa.TabIndex = 38;
            this.editCodigoEmpresa.Visible = false;
            // 
            // chkEmpresa
            // 
            this.chkEmpresa.AutoSize = true;
            this.chkEmpresa.Location = new System.Drawing.Point(308, 31);
            this.chkEmpresa.Name = "chkEmpresa";
            this.chkEmpresa.Size = new System.Drawing.Size(72, 17);
            this.chkEmpresa.TabIndex = 37;
            this.chkEmpresa.Text = "Por CNPJ";
            this.chkEmpresa.UseVisualStyleBackColor = true;
            // 
            // comboImpressoraLaserJato
            // 
            this.comboImpressoraLaserJato.FormattingEnabled = true;
            this.comboImpressoraLaserJato.Location = new System.Drawing.Point(8, 75);
            this.comboImpressoraLaserJato.Name = "comboImpressoraLaserJato";
            this.comboImpressoraLaserJato.Size = new System.Drawing.Size(287, 21);
            this.comboImpressoraLaserJato.TabIndex = 35;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Impressora Laser/Jato:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(116, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Porta";
            this.label4.Visible = false;
            // 
            // editPorta
            // 
            this.editPorta.Location = new System.Drawing.Point(116, 178);
            this.editPorta.Name = "editPorta";
            this.editPorta.Size = new System.Drawing.Size(100, 20);
            this.editPorta.TabIndex = 33;
            this.editPorta.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "IP Servidor";
            this.label3.Visible = false;
            // 
            // editIP
            // 
            this.editIP.Location = new System.Drawing.Point(8, 178);
            this.editIP.Name = "editIP";
            this.editIP.Size = new System.Drawing.Size(100, 20);
            this.editIP.TabIndex = 31;
            this.editIP.Visible = false;
            // 
            // comboImpressoraTermica
            // 
            this.comboImpressoraTermica.FormattingEnabled = true;
            this.comboImpressoraTermica.Location = new System.Drawing.Point(8, 31);
            this.comboImpressoraTermica.Name = "comboImpressoraTermica";
            this.comboImpressoraTermica.Size = new System.Drawing.Size(287, 21);
            this.comboImpressoraTermica.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Impressora Térmica:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(20, 63);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(405, 283);
            this.tabControl1.TabIndex = 40;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.comboImpressoraTermica);
            this.tabPage1.Controls.Add(this.editCodigoEmpresa);
            this.tabPage1.Controls.Add(this.editIP);
            this.tabPage1.Controls.Add(this.chkEmpresa);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.comboImpressoraLaserJato);
            this.tabPage1.Controls.Add(this.editPorta);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(397, 257);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Impressora";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkSKU);
            this.tabPage2.Controls.Add(this.chkPaisagemRetrato);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(397, 257);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Geral";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkPaisagemRetrato
            // 
            this.chkPaisagemRetrato.AutoSize = true;
            this.chkPaisagemRetrato.Location = new System.Drawing.Point(16, 15);
            this.chkPaisagemRetrato.Name = "chkPaisagemRetrato";
            this.chkPaisagemRetrato.Size = new System.Drawing.Size(172, 17);
            this.chkPaisagemRetrato.TabIndex = 0;
            this.chkPaisagemRetrato.Text = "Impressão Retrato do Romanio";
            this.chkPaisagemRetrato.UseVisualStyleBackColor = true;
            // 
            // chkSKU
            // 
            this.chkSKU.AutoSize = true;
            this.chkSKU.Location = new System.Drawing.Point(16, 38);
            this.chkSKU.Name = "chkSKU";
            this.chkSKU.Size = new System.Drawing.Size(118, 17);
            this.chkSKU.TabIndex = 1;
            this.chkSKU.Text = "Impressão Por SKU";
            this.chkSKU.UseVisualStyleBackColor = true;
            // 
            // frmConfiguracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.metroPanel1);
            this.Name = "frmConfiguracao";
            this.Text = "Configuração";
            this.Load += new System.EventHandler(this.FrmConfiguracao_Load);
            this.metroPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private DevExpress.XtraEditors.SimpleButton btnSalvar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox editCodigoEmpresa;
        private System.Windows.Forms.CheckBox chkEmpresa;
        private System.Windows.Forms.ComboBox comboImpressoraLaserJato;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox editPorta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox editIP;
        private System.Windows.Forms.ComboBox comboImpressoraTermica;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chkPaisagemRetrato;
        private System.Windows.Forms.CheckBox chkSKU;
    }
}