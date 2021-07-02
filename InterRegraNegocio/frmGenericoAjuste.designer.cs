namespace InterRegraNegocio
{
    partial class frmGenericoAjuste
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
            this.lblTopo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlTopo
            // 
            // 
            // lblTopo
            // 
            this.lblTopo.AutoSize = true;
            this.lblTopo.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTopo.Location = new System.Drawing.Point(100, 27);
            this.lblTopo.Name = "lblTopo";
            this.lblTopo.Size = new System.Drawing.Size(72, 24);
            this.lblTopo.TabIndex = 1;
            this.lblTopo.Text = "label1";
            // 
            // imgTopo
            // 
            // 
            // pnlBottom
            // 
            // 
            // btnSalvar
            // 
            // 
            // btnSair
            // 
            // pnlConteudo
            // 
            // 
            // frmGenericoAjuste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.DisplayHeader = false;
            this.KeyPreview = true;
            this.Name = "frmGenericoAjuste";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmGenericoAjuste";
            this.Activated += new System.EventHandler(this.frmGenericoAjuste_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmGenericoAjuste_FormClosing);
            this.Load += new System.EventHandler(this.frmGenericoAjuste_Load);
            this.Shown += new System.EventHandler(this.frmGenericoAjuste_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGenericoAjuste_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Label lblTopo;

    }
}