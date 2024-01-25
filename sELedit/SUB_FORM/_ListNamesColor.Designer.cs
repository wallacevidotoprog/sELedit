namespace sELedit.NOVO
{
    partial class _ListNamesColor
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
            this.listBox_names = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox_names
            // 
            this.listBox_names.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_names.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox_names.FormattingEnabled = true;
            this.listBox_names.Location = new System.Drawing.Point(0, 0);
            this.listBox_names.Margin = new System.Windows.Forms.Padding(4);
            this.listBox_names.Name = "listBox_names";
            this.listBox_names.Size = new System.Drawing.Size(376, 152);
            this.listBox_names.TabIndex = 0;
            this.listBox_names.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_names_DrawItem);
            this.listBox_names.SelectedIndexChanged += new System.EventHandler(this.listBox_names_SelectedIndexChanged);
            this.listBox_names.MouseLeave += new System.EventHandler(this.listBox_names_MouseLeave);
            // 
            // _ListNamesColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 152);
            this.Controls.Add(this.listBox_names);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_ListNamesColor";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.load);
            this.MouseLeave += new System.EventHandler(this.listBox_names_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_names;
    }
}
