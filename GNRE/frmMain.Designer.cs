
namespace GNRE
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
            this.toogleProducao = new DevExpress.XtraEditors.ToggleSwitch();
            ((System.ComponentModel.ISupportInitialize)(this.toogleProducao.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // toogleProducao
            // 
            this.toogleProducao.Location = new System.Drawing.Point(23, 743);
            this.toogleProducao.Name = "toogleProducao";
            this.toogleProducao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 25F);
            this.toogleProducao.Properties.Appearance.Options.UseFont = true;
            this.toogleProducao.Properties.OffText = "Produção";
            this.toogleProducao.Properties.OnText = "On";
            this.toogleProducao.Size = new System.Drawing.Size(295, 45);
            this.toogleProducao.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1435, 811);
            this.Controls.Add(this.toogleProducao);
            this.Name = "frmMain";
            this.Text = "GNRE - Gere suas GNRE";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.toogleProducao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ToggleSwitch toogleProducao;
    }
}

