
namespace InterEtiquetas
{
    partial class frmProdutosAdicionais
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
            this.lblCNPJ = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.editEtiqueta = new System.Windows.Forms.TextBox();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.lblSKU = new System.Windows.Forms.Label();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.btnConferir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCNPJ
            // 
            this.lblCNPJ.AutoSize = true;
            this.lblCNPJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblCNPJ.Location = new System.Drawing.Point(60, 60);
            this.lblCNPJ.Name = "lblCNPJ";
            this.lblCNPJ.Size = new System.Drawing.Size(46, 20);
            this.lblCNPJ.TabIndex = 20;
            this.lblCNPJ.Text = "SKU:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(210, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "Código:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(22, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "Descrição:";
            // 
            // editEtiqueta
            // 
            this.editEtiqueta.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editEtiqueta.Location = new System.Drawing.Point(23, 148);
            this.editEtiqueta.Name = "editEtiqueta";
            this.editEtiqueta.Size = new System.Drawing.Size(336, 45);
            this.editEtiqueta.TabIndex = 23;
            this.editEtiqueta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editEtiqueta_KeyDown);
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.lblQuantidade.Location = new System.Drawing.Point(365, 151);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(40, 39);
            this.lblQuantidade.TabIndex = 24;
            this.lblQuantidade.Text = "X";
            // 
            // lblSKU
            // 
            this.lblSKU.AutoSize = true;
            this.lblSKU.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblSKU.Location = new System.Drawing.Point(102, 60);
            this.lblSKU.Name = "lblSKU";
            this.lblSKU.Size = new System.Drawing.Size(0, 20);
            this.lblSKU.TabIndex = 25;
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblDescricao.Location = new System.Drawing.Point(102, 89);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(0, 20);
            this.lblDescricao.TabIndex = 26;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblCodigo.Location = new System.Drawing.Point(270, 60);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(0, 20);
            this.lblCodigo.TabIndex = 27;
            // 
            // btnConferir
            // 
            this.btnConferir.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConferir.Location = new System.Drawing.Point(26, 199);
            this.btnConferir.Name = "btnConferir";
            this.btnConferir.Size = new System.Drawing.Size(327, 49);
            this.btnConferir.TabIndex = 28;
            this.btnConferir.Text = "Conferir";
            this.btnConferir.UseVisualStyleBackColor = true;
            this.btnConferir.Click += new System.EventHandler(this.btnConferir_Click);
            // 
            // frmProdutosAdicionais
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 254);
            this.Controls.Add(this.btnConferir);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.lblSKU);
            this.Controls.Add(this.lblQuantidade);
            this.Controls.Add(this.editEtiqueta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCNPJ);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProdutosAdicionais";
            this.Text = "Bipe os produtos adicionais";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmProdutosAdicionais_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCNPJ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editEtiqueta;
        public System.Windows.Forms.Label lblQuantidade;
        public System.Windows.Forms.Label lblSKU;
        public System.Windows.Forms.Label lblDescricao;
        public System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Button btnConferir;
    }
}