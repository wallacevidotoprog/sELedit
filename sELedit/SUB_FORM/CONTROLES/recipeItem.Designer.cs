namespace sELedit.NOVO
{
    partial class recipeItem
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_qtd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::sELedit.Properties.Resources.bloco_a;
            this.pictureBox1.Location = new System.Drawing.Point(22, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox_item_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox_item_MouseLeave);
            // 
            // button_qtd
            // 
            this.button_qtd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(42)))), ((int)(((byte)(27)))));
            this.button_qtd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_qtd.ForeColor = System.Drawing.Color.Goldenrod;
            this.button_qtd.Location = new System.Drawing.Point(3, 36);
            this.button_qtd.Name = "button_qtd";
            this.button_qtd.Size = new System.Drawing.Size(68, 23);
            this.button_qtd.TabIndex = 3;
            this.button_qtd.Text = "TXT";
            this.button_qtd.UseVisualStyleBackColor = false;
            // 
            // recipeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.button_qtd);
            this.Controls.Add(this.pictureBox1);
            this.Name = "recipeItem";
            this.Size = new System.Drawing.Size(74, 61);
            this.Load += new System.EventHandler(this.recipeItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_qtd;
    }
}
