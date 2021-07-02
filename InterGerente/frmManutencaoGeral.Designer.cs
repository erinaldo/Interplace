namespace InterGerente
{
    partial class frmManutencaoGeral
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
            this.btnRetiraDuplicadoProduto = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRetiraDuplicadoProduto
            // 
            this.btnRetiraDuplicadoProduto.Location = new System.Drawing.Point(12, 12);
            this.btnRetiraDuplicadoProduto.Name = "btnRetiraDuplicadoProduto";
            this.btnRetiraDuplicadoProduto.Size = new System.Drawing.Size(265, 100);
            this.btnRetiraDuplicadoProduto.TabIndex = 0;
            this.btnRetiraDuplicadoProduto.Text = "Retira Duplicado Produto";
            this.btnRetiraDuplicadoProduto.UseVisualStyleBackColor = true;
            this.btnRetiraDuplicadoProduto.Click += new System.EventHandler(this.btnRetiraDuplicadoProduto_Click);
            // 
            // frmManutencaoGeral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1187, 668);
            this.Controls.Add(this.btnRetiraDuplicadoProduto);
            this.Name = "frmManutencaoGeral";
            this.Text = "frmManutencaoGeral";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRetiraDuplicadoProduto;
    }
}