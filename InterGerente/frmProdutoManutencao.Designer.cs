namespace InterGerente
{
    partial class frmProdutoManutencao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProdutoManutencao));
            this.editProduto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.editCaracteristicasTecnicas = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.editCodigo1 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboTipoAnuncio = new System.Windows.Forms.ComboBox();
            this.dlgImagem = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            this.lblTopo.Size = new System.Drawing.Size(170, 24);
            this.lblTopo.Text = "Produtos Ajuste";
            // 
            this.editProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editProduto.Location = new System.Drawing.Point(156, 50);
            this.editProduto.Name = "editProduto";
            this.editProduto.Size = new System.Drawing.Size(681, 26);
            this.editProduto.TabIndex = 127;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(95, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 128;
            this.label2.Text = "Nome:";
            // 
            // editCodigo
            // 
            this.editCodigo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCodigo.Location = new System.Drawing.Point(156, 25);
            this.editCodigo.Name = "editCodigo";
            this.editCodigo.ReadOnly = true;
            this.editCodigo.Size = new System.Drawing.Size(84, 19);
            this.editCodigo.TabIndex = 125;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(87, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 126;
            this.label1.Text = "Código:";
            // 
            // editCaracteristicasTecnicas
            // 
            this.editCaracteristicasTecnicas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCaracteristicasTecnicas.Location = new System.Drawing.Point(156, 82);
            this.editCaracteristicasTecnicas.Multiline = true;
            this.editCaracteristicasTecnicas.Name = "editCaracteristicasTecnicas";
            this.editCaracteristicasTecnicas.Size = new System.Drawing.Size(681, 129);
            this.editCaracteristicasTecnicas.TabIndex = 131;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(66, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 132;
            this.label3.Text = "Descrição:";
            // 
            // editCodigo1
            // 
            this.editCodigo1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editCodigo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editCodigo1.Location = new System.Drawing.Point(246, 25);
            this.editCodigo1.Name = "editCodigo1";
            this.editCodigo1.ReadOnly = true;
            this.editCodigo1.Size = new System.Drawing.Size(104, 19);
            this.editCodigo1.TabIndex = 133;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(156, 584);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(681, 26);
            this.textBox1.TabIndex = 134;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(103, 587);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 20);
            this.label4.TabIndex = 135;
            this.label4.Text = "Guid:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 20);
            this.label5.TabIndex = 136;
            this.label5.Text = "Tipo de Anúncio:";
            // 
            // comboTipoAnuncio
            // 
            this.comboTipoAnuncio.FormattingEnabled = true;
            this.comboTipoAnuncio.Location = new System.Drawing.Point(157, 222);
            this.comboTipoAnuncio.Name = "comboTipoAnuncio";
            this.comboTipoAnuncio.Size = new System.Drawing.Size(211, 21);
            this.comboTipoAnuncio.TabIndex = 137;
            // 
            // btnAdicionarImagem
            // 
            // 
            // dlgImagem
            // 
            this.dlgImagem.Filter = "Imagens|*.png|Imagens|*.jpg";
            // 
            // lstImagem
            // 
            // 
            // frmProdutoManutencao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 802);
            this.Name = "frmProdutoManutencao";
            this.Text = "frmProdutoManutencao";
            this.Load += new System.EventHandler(this.FrmProdutoManutencao_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox editCaracteristicasTecnicas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox editProduto;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox editCodigo;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox editCodigo1;
        private System.Windows.Forms.ComboBox comboTipoAnuncio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog dlgImagem;
    }
}