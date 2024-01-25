namespace sELedit
{
    partial class IToolType_REC
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
            this.dataGridView_item = new System.Windows.Forms.DataGridView();
            this.ICON = new sELedit.configs.TextAndImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_item)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_item
            // 
            this.dataGridView_item.AllowUserToAddRows = false;
            this.dataGridView_item.AllowUserToDeleteRows = false;
            this.dataGridView_item.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_item.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView_item.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_item.ColumnHeadersVisible = false;
            this.dataGridView_item.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ICON});
            this.dataGridView_item.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_item.EnableHeadersVisualStyles = false;
            this.dataGridView_item.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView_item.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_item.MultiSelect = false;
            this.dataGridView_item.Name = "dataGridView_item";
            this.dataGridView_item.RowHeadersVisible = false;
            this.dataGridView_item.Size = new System.Drawing.Size(249, 230);
            this.dataGridView_item.TabIndex = 0;
            // 
            // ICON
            // 
            this.ICON.HeaderText = "ICON";
            this.ICON.Image = null;
            this.ICON.Name = "ICON";
            this.ICON.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // IToolType_REC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(249, 230);
            this.Controls.Add(this.dataGridView_item);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "IToolType_REC";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "IToolType";
            this.Load += new System.EventHandler(this.IToolType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_item)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_item;
        private configs.TextAndImageColumn ICON;
    }
}