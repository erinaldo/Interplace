namespace Auxiliar
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
            this.button1 = new System.Windows.Forms.Button();
            this.editJadlog = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.editPedidoMercadoLivre = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.editRastreio = new System.Windows.Forms.TextBox();
            this.btnAmazon = new System.Windows.Forms.Button();
            this.editPedidoAmazon = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnEnviaNotab2w = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.editPedidoB2W = new System.Windows.Forms.TextBox();
            this.btnPedidosDuplicados = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnDeletar = new System.Windows.Forms.Button();
            this.editCliente = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.editJadlog);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cria uma etiqueta jadlog";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(307, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Gerar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // editJadlog
            // 
            this.editJadlog.Location = new System.Drawing.Point(6, 19);
            this.editJadlog.Name = "editJadlog";
            this.editJadlog.Size = new System.Drawing.Size(376, 20);
            this.editJadlog.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.editPedidoMercadoLivre);
            this.groupBox2.Location = new System.Drawing.Point(13, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(185, 79);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Integra 1 Pedido Mercado Livre";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(90, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Gerar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // editPedidoMercadoLivre
            // 
            this.editPedidoMercadoLivre.Location = new System.Drawing.Point(6, 19);
            this.editPedidoMercadoLivre.Name = "editPedidoMercadoLivre";
            this.editPedidoMercadoLivre.Size = new System.Drawing.Size(159, 20);
            this.editPedidoMercadoLivre.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.editRastreio);
            this.groupBox3.Controls.Add(this.btnAmazon);
            this.groupBox3.Controls.Add(this.editPedidoAmazon);
            this.groupBox3.Location = new System.Drawing.Point(409, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(390, 79);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Retorna Rastreio Amazon";
            // 
            // editRastreio
            // 
            this.editRastreio.Location = new System.Drawing.Point(6, 50);
            this.editRastreio.Name = "editRastreio";
            this.editRastreio.ReadOnly = true;
            this.editRastreio.Size = new System.Drawing.Size(200, 20);
            this.editRastreio.TabIndex = 2;
            // 
            // btnAmazon
            // 
            this.btnAmazon.Location = new System.Drawing.Point(212, 17);
            this.btnAmazon.Name = "btnAmazon";
            this.btnAmazon.Size = new System.Drawing.Size(75, 23);
            this.btnAmazon.TabIndex = 1;
            this.btnAmazon.Text = "Gerar";
            this.btnAmazon.UseVisualStyleBackColor = true;
            this.btnAmazon.Click += new System.EventHandler(this.btnAmazon_Click);
            // 
            // editPedidoAmazon
            // 
            this.editPedidoAmazon.Location = new System.Drawing.Point(6, 19);
            this.editPedidoAmazon.Name = "editPedidoAmazon";
            this.editPedidoAmazon.Size = new System.Drawing.Size(200, 20);
            this.editPedidoAmazon.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnEnviaNotab2w);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.editPedidoB2W);
            this.groupBox4.Location = new System.Drawing.Point(218, 98);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(287, 79);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Integra 1 Pedido B2W";
            // 
            // btnEnviaNotab2w
            // 
            this.btnEnviaNotab2w.Location = new System.Drawing.Point(125, 50);
            this.btnEnviaNotab2w.Name = "btnEnviaNotab2w";
            this.btnEnviaNotab2w.Size = new System.Drawing.Size(75, 23);
            this.btnEnviaNotab2w.TabIndex = 1;
            this.btnEnviaNotab2w.Text = "Enviar Nota";
            this.btnEnviaNotab2w.UseVisualStyleBackColor = true;
            this.btnEnviaNotab2w.Click += new System.EventHandler(this.btnEnviaNotab2w_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(206, 50);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Integrar Pedido";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // editPedidoB2W
            // 
            this.editPedidoB2W.Location = new System.Drawing.Point(6, 19);
            this.editPedidoB2W.Name = "editPedidoB2W";
            this.editPedidoB2W.Size = new System.Drawing.Size(275, 20);
            this.editPedidoB2W.TabIndex = 0;
            // 
            // btnPedidosDuplicados
            // 
            this.btnPedidosDuplicados.Location = new System.Drawing.Point(1043, 12);
            this.btnPedidosDuplicados.Name = "btnPedidosDuplicados";
            this.btnPedidosDuplicados.Size = new System.Drawing.Size(132, 71);
            this.btnPedidosDuplicados.TabIndex = 4;
            this.btnPedidosDuplicados.Text = "Ver Pedidos Duplicados";
            this.btnPedidosDuplicados.UseVisualStyleBackColor = true;
            this.btnPedidosDuplicados.Click += new System.EventHandler(this.btnPedidosDuplicados_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnDeletar);
            this.groupBox5.Controls.Add(this.editCliente);
            this.groupBox5.Location = new System.Drawing.Point(511, 98);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(185, 79);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Apaga 1 cliente";
            // 
            // btnDeletar
            // 
            this.btnDeletar.Location = new System.Drawing.Point(90, 50);
            this.btnDeletar.Name = "btnDeletar";
            this.btnDeletar.Size = new System.Drawing.Size(75, 23);
            this.btnDeletar.TabIndex = 1;
            this.btnDeletar.Text = "Deletar";
            this.btnDeletar.UseVisualStyleBackColor = true;
            this.btnDeletar.Click += new System.EventHandler(this.btnDeletar_Click);
            // 
            // editCliente
            // 
            this.editCliente.Location = new System.Drawing.Point(6, 19);
            this.editCliente.Name = "editCliente";
            this.editCliente.Size = new System.Drawing.Size(159, 20);
            this.editCliente.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(702, 148);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(94, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Bling baixa nota";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1187, 192);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnPedidosDuplicados);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Auxiliar";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox editJadlog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox editPedidoMercadoLivre;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAmazon;
        private System.Windows.Forms.TextBox editPedidoAmazon;
        private System.Windows.Forms.TextBox editRastreio;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox editPedidoB2W;
        private System.Windows.Forms.Button btnEnviaNotab2w;
        private System.Windows.Forms.Button btnPedidosDuplicados;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnDeletar;
        private System.Windows.Forms.TextBox editCliente;
        private System.Windows.Forms.Button button4;
    }
}

