namespace sELedit.NOVO
{
    partial class blocosS
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
            this.pictureBox_item = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_item)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_item
            // 
            this.pictureBox_item.BackgroundImage =  global::sELedit.Properties.Resources.bloco_a;
            this.pictureBox_item.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_item.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_item.Image = global::sELedit.Properties.Resources.bloco_a;
            this.pictureBox_item.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_item.Name = "pictureBox_item";
            this.pictureBox_item.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_item.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_item.TabIndex = 0;
            this.pictureBox_item.TabStop = false;
            this.pictureBox_item.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_item_MouseDown);
            this.pictureBox_item.MouseEnter += new System.EventHandler(this.pictureBox_item_MouseEnter);
            this.pictureBox_item.MouseLeave += new System.EventHandler(this.pictureBox_item_MouseLeave);
            // 
            // blocosS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_item);
            this.Name = "blocosS";
            this.Size = new System.Drawing.Size(35, 35);
            this.Load += new System.EventHandler(this.blocosS_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.blocosS_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_item)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_item;
    }
}
