namespace sELedit.NOVO
{
    partial class ToolTip_addons
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
            this.components = new System.ComponentModel.Container();
            this.tmrHideMe = new System.Windows.Forms.Timer(this.components);
            this.listBox_adds = new System.Windows.Forms.ListBox();
            this.textAndImageColumn1 = new sELedit.configs.TextAndImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuspendLayout();
            // 
            // tmrHideMe
            // 
            this.tmrHideMe.Tick += new System.EventHandler(this.TmrHideMe_Tick);
            // 
            // listBox_adds
            // 
            this.listBox_adds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.listBox_adds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_adds.ForeColor = System.Drawing.Color.DodgerBlue;
            this.listBox_adds.FormattingEnabled = true;
            this.listBox_adds.Items.AddRange(new object[] {
            "(1) +25 nv de atk",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.listBox_adds.Location = new System.Drawing.Point(4, 4);
            this.listBox_adds.Margin = new System.Windows.Forms.Padding(0);
            this.listBox_adds.Name = "listBox_adds";
            this.listBox_adds.Size = new System.Drawing.Size(294, 13);
            this.listBox_adds.TabIndex = 0;
            // 
            // textAndImageColumn1
            // 
            this.textAndImageColumn1.HeaderText = "Column1";
            this.textAndImageColumn1.Image = null;
            this.textAndImageColumn1.Name = "textAndImageColumn1";
            this.textAndImageColumn1.ReadOnly = true;
            this.textAndImageColumn1.Width = 173;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 172;
            // 
            // ToolTip_addons
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.BackgroundImage = global::sELedit.Properties.Resources._base;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(304, 23);
            this.ControlBox = false;
            this.Controls.Add(this.listBox_adds);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolTip_addons";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ToolTip";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(22)))), ((int)(((byte)(29)))));
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmrHideMe;
        private configs.TextAndImageColumn textAndImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.ListBox listBox_adds;
    }

}