namespace Relatorios
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnGerarVendaCusto = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateFinal = new System.Windows.Forms.DateTimePicker();
            this.dateInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.editArquivo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dlgArquivo = new System.Windows.Forms.OpenFileDialog();
            this.barProgresso = new System.Windows.Forms.ProgressBar();
            this.lblPage = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLog);
            this.groupBox1.Controls.Add(this.btnGerarVendaCusto);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateFinal);
            this.groupBox1.Controls.Add(this.dateInicial);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vendas com Custo - ESHOP";
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(20, 67);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(0, 13);
            this.lblLog.TabIndex = 4;
            // 
            // btnGerarVendaCusto
            // 
            this.btnGerarVendaCusto.Location = new System.Drawing.Point(143, 67);
            this.btnGerarVendaCusto.Name = "btnGerarVendaCusto";
            this.btnGerarVendaCusto.Size = new System.Drawing.Size(75, 23);
            this.btnGerarVendaCusto.TabIndex = 3;
            this.btnGerarVendaCusto.Text = "Gerar";
            this.btnGerarVendaCusto.UseVisualStyleBackColor = true;
            this.btnGerarVendaCusto.Click += new System.EventHandler(this.btnGerarVendaCusto_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Periodo";
            // 
            // dateFinal
            // 
            this.dateFinal.CustomFormat = "dd/MM/yyyy";
            this.dateFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFinal.Location = new System.Drawing.Point(122, 41);
            this.dateFinal.Name = "dateFinal";
            this.dateFinal.Size = new System.Drawing.Size(96, 20);
            this.dateFinal.TabIndex = 1;
            // 
            // dateInicial
            // 
            this.dateInicial.CustomFormat = "dd/MM/yyyy";
            this.dateInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateInicial.Location = new System.Drawing.Point(20, 41);
            this.dateInicial.Name = "dateInicial";
            this.dateInicial.Size = new System.Drawing.Size(96, 20);
            this.dateInicial.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.editArquivo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(252, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gera Excel - ESHOP";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(184, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 20);
            this.button2.TabIndex = 7;
            this.button2.Text = "P";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Arquivo";
            // 
            // editArquivo
            // 
            this.editArquivo.Location = new System.Drawing.Point(7, 41);
            this.editArquivo.Name = "editArquivo";
            this.editArquivo.Size = new System.Drawing.Size(172, 20);
            this.editArquivo.TabIndex = 5;
            this.editArquivo.Text = "C:\\Users\\Rodrigo Nunes\\Downloads\\MODELO RELATORIO RODRIGO ML BLING.xlsx";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(143, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Gerar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dlgArquivo
            // 
            this.dlgArquivo.FileName = "openFileDialog1";
            // 
            // barProgresso
            // 
            this.barProgresso.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barProgresso.Location = new System.Drawing.Point(0, 427);
            this.barProgresso.Name = "barProgresso";
            this.barProgresso.Size = new System.Drawing.Size(800, 23);
            this.barProgresso.TabIndex = 2;
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Location = new System.Drawing.Point(0, 408);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(0, 13);
            this.lblPage.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(574, 325);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(214, 96);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.barProgresso);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Relatórios em Geral";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGerarVendaCusto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateFinal;
        private System.Windows.Forms.DateTimePicker dateInicial;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox editArquivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog dlgArquivo;
        private System.Windows.Forms.ProgressBar barProgresso;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Button button3;
    }
}

