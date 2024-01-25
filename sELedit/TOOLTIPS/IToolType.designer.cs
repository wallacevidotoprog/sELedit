namespace sELedit
{
    partial class IToolType
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
            this.iconBox = new System.Windows.Forms.PictureBox();
            this.titleText = new System.Windows.Forms.Label();
            this.richTextBox_PreviewText = new sELedit.configs.AlphaRichTextBox();
            this.label_ID = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // iconBox
            // 
            this.iconBox.Location = new System.Drawing.Point(23, 11);
            this.iconBox.Name = "iconBox";
            this.iconBox.Size = new System.Drawing.Size(32, 32);
            this.iconBox.TabIndex = 6;
            this.iconBox.TabStop = false;
            // 
            // titleText
            // 
            this.titleText.AutoSize = true;
            this.titleText.BackColor = System.Drawing.Color.Transparent;
            this.titleText.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleText.ForeColor = System.Drawing.Color.White;
            this.titleText.Location = new System.Drawing.Point(61, 27);
            this.titleText.Name = "titleText";
            this.titleText.Size = new System.Drawing.Size(179, 16);
            this.titleText.TabIndex = 7;
            this.titleText.Text = "AAAAASDASDASDASDASD";
            // 
            // richTextBox_PreviewText
            // 
            this.richTextBox_PreviewText.AlphaAmount = 5;
            this.richTextBox_PreviewText.AlphaBackColor = System.Drawing.Color.Black;
            this.richTextBox_PreviewText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_PreviewText.BackColor = System.Drawing.Color.Transparent;
            this.richTextBox_PreviewText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_PreviewText.CaretColor = System.Drawing.Color.Empty;
            this.richTextBox_PreviewText.CausesValidation = false;
            this.richTextBox_PreviewText.Cursor = System.Windows.Forms.Cursors.No;
            this.richTextBox_PreviewText.Location = new System.Drawing.Point(6, 48);
            this.richTextBox_PreviewText.Name = "richTextBox_PreviewText";
            this.richTextBox_PreviewText.ReadOnly = true;
            this.richTextBox_PreviewText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox_PreviewText.Size = new System.Drawing.Size(298, 59);
            this.richTextBox_PreviewText.TabIndex = 42;
            this.richTextBox_PreviewText.TabStop = false;
            this.richTextBox_PreviewText.Text = "";
            this.richTextBox_PreviewText.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.rtb_ContentsResized);
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.BackColor = System.Drawing.Color.Transparent;
            this.label_ID.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ID.ForeColor = System.Drawing.Color.White;
            this.label_ID.Location = new System.Drawing.Point(61, 11);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(149, 14);
            this.label_ID.TabIndex = 7;
            this.label_ID.Text = "AAAAASDASDASDASDASD";
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.titleText);
            this.panel1.Controls.Add(this.richTextBox_PreviewText);
            this.panel1.Controls.Add(this.iconBox);
            this.panel1.Controls.Add(this.label_ID);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 119);
            this.panel1.TabIndex = 43;
            // 
            // IToolType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(312, 119);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "IToolType";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "IToolType";
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox iconBox;
        private System.Windows.Forms.Label titleText;
        private configs.AlphaRichTextBox richTextBox_PreviewText;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.Panel panel1;
    }
}