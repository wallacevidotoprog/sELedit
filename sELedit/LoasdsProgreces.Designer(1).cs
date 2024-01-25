namespace sELedit
{
    partial class LoasdsProgreces
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

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progressBar_read = new System.Windows.Forms.ProgressBar();
            this.label_read = new System.Windows.Forms.Label();
            this.timerload = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // progressBar_read
            // 
            this.progressBar_read.Location = new System.Drawing.Point(109, 5);
            this.progressBar_read.Name = "progressBar_read";
            this.progressBar_read.Size = new System.Drawing.Size(315, 23);
            this.progressBar_read.TabIndex = 0;
            // 
            // label_read
            // 
            this.label_read.AutoSize = true;
            this.label_read.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_read.Location = new System.Drawing.Point(7, 8);
            this.label_read.Name = "label_read";
            this.label_read.Size = new System.Drawing.Size(90, 17);
            this.label_read.TabIndex = 1;
            this.label_read.Text = "READ (7/7)";
            // 
            // timerload
            // 
            this.timerload.Tick += new System.EventHandler(this.timerload_Tick);
            // 
            // LoasdsProgreces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_read);
            this.Controls.Add(this.progressBar_read);
            this.Name = "LoasdsProgreces";
            this.Size = new System.Drawing.Size(427, 35);
            this.Load += new System.EventHandler(this.LoasdsProgreces_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar_read;
        private System.Windows.Forms.Label label_read;
        private System.Windows.Forms.Timer timerload;
    }
}
