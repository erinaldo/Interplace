namespace InterGerente
{
    partial class frmRelatorios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelatorios));
            this.btnAtualizar = new MetroFramework.Controls.MetroButton();
            this.barProgresso = new MetroFramework.Controls.MetroProgressBar();
            this.SuspendLayout();
            
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAtualizar.Location = new System.Drawing.Point(918, 2);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(119, 52);
            this.btnAtualizar.TabIndex = 2;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseSelectable = true;
            this.btnAtualizar.Click += new System.EventHandler(this.BtnAtualizar_Click);
            // 
            // barProgresso
            // 
            this.barProgresso.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barProgresso.Location = new System.Drawing.Point(20, 609);
            this.barProgresso.Name = "barProgresso";
            this.barProgresso.Size = new System.Drawing.Size(1039, 23);
            this.barProgresso.TabIndex = 6;
            // 
            // tabControleEstoque
            // 
            // 
            // frmRelatorios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1079, 708);
            this.Controls.Add(this.barProgresso);
            this.Name = "frmRelatorios";
            this.Controls.SetChildIndex(this.barProgresso, 0);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton btnAtualizar;
        private MetroFramework.Controls.MetroProgressBar barProgresso;

    }
}
