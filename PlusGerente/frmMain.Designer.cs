namespace PlusGerente
{
    partial class frmMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnClientes = new MetroFramework.Controls.MetroButton();
            this.lblStatus = new System.Windows.Forms.Label();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.iconApp = new System.Windows.Forms.NotifyIcon(this.components);
            this.progressBarra = new System.Windows.Forms.ProgressBar();
            this.editNota = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.editPorta = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.editPortaConsulta = new System.Windows.Forms.TextBox();
            this.lblAPICount = new System.Windows.Forms.Label();
            this.editLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClientes
            // 
            this.btnClientes.Location = new System.Drawing.Point(52, 63);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(158, 58);
            this.btnClientes.TabIndex = 2;
            this.btnClientes.Text = "Clientes";
            this.btnClientes.UseSelectable = true;
            this.btnClientes.Click += new System.EventHandler(this.BtnClientes_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(111, 612);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(52, 551);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(158, 58);
            this.metroButton1.TabIndex = 4;
            this.metroButton1.Text = "Sair";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.MetroButton1_Click);
            // 
            // iconApp
            // 
            this.iconApp.Text = "notifyIcon1";
            this.iconApp.Visible = true;
            // 
            // progressBarra
            // 
            this.progressBarra.Location = new System.Drawing.Point(231, 625);
            this.progressBarra.Name = "progressBarra";
            this.progressBarra.Size = new System.Drawing.Size(546, 23);
            this.progressBarra.TabIndex = 5;
            // 
            // editNota
            // 
            this.editNota.Location = new System.Drawing.Point(267, 654);
            this.editNota.Name = "editNota";
            this.editNota.Size = new System.Drawing.Size(396, 20);
            this.editNota.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 658);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nota:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 642);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Porta Etiqueta:";
            // 
            // editPorta
            // 
            this.editPorta.Location = new System.Drawing.Point(110, 639);
            this.editPorta.Name = "editPorta";
            this.editPorta.Size = new System.Drawing.Size(100, 20);
            this.editPorta.TabIndex = 24;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(90, 685);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 20);
            this.btnSalvar.TabIndex = 26;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 663);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Porta Consulta:";
            // 
            // editPortaConsulta
            // 
            this.editPortaConsulta.Location = new System.Drawing.Point(110, 660);
            this.editPortaConsulta.Name = "editPortaConsulta";
            this.editPortaConsulta.Size = new System.Drawing.Size(100, 20);
            this.editPortaConsulta.TabIndex = 27;
            // 
            // lblAPICount
            // 
            this.lblAPICount.AutoSize = true;
            this.lblAPICount.Location = new System.Drawing.Point(228, 689);
            this.lblAPICount.Name = "lblAPICount";
            this.lblAPICount.Size = new System.Drawing.Size(13, 13);
            this.lblAPICount.TabIndex = 29;
            this.lblAPICount.Text = "0";
            // 
            // editLog
            // 
            this.editLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.editLog.Location = new System.Drawing.Point(1073, 60);
            this.editLog.Multiline = true;
            this.editLog.Name = "editLog";
            this.editLog.Size = new System.Drawing.Size(349, 647);
            this.editLog.TabIndex = 30;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 727);
            this.Controls.Add(this.editLog);
            this.Controls.Add(this.lblAPICount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.editPortaConsulta);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.editPorta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editNota);
            this.Controls.Add(this.progressBarra);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnClientes);
            this.Name = "frmMain";
            this.Text = "Gerente";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroButton btnClientes;
        private System.Windows.Forms.Label lblStatus;
        private MetroFramework.Controls.MetroButton metroButton1;
        private System.Windows.Forms.NotifyIcon iconApp;
        private System.Windows.Forms.ProgressBar progressBarra;
        private System.Windows.Forms.TextBox editNota;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox editPorta;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editPortaConsulta;
        private System.Windows.Forms.Label lblAPICount;
        private System.Windows.Forms.TextBox editLog;
    }
}

