namespace sELedit.NOVO
{
    partial class item_ext_desc
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button_SETCOR = new System.Windows.Forms.Button();
            this.textBox_ColorCod = new System.Windows.Forms.TextBox();
            this.pictureBox_Color = new System.Windows.Forms.PictureBox();
            this.richTextBox_DESC_POS = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.richTextBox_DESC_PRE = new System.Windows.Forms.RichTextBox();
            this.pictureBox_color_Item = new System.Windows.Forms.PictureBox();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_color_Item)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.richTextBox_DESC_POS);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(800, 450);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DESCRIÇÃO";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button_SETCOR);
            this.groupBox5.Controls.Add(this.textBox_ColorCod);
            this.groupBox5.Controls.Add(this.pictureBox_Color);
            this.groupBox5.Location = new System.Drawing.Point(670, 322);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(118, 116);
            this.groupBox5.TabIndex = 317;
            this.groupBox5.TabStop = false;
            // 
            // button_SETCOR
            // 
            this.button_SETCOR.ForeColor = System.Drawing.Color.Black;
            this.button_SETCOR.Location = new System.Drawing.Point(34, 42);
            this.button_SETCOR.Name = "button_SETCOR";
            this.button_SETCOR.Size = new System.Drawing.Size(75, 23);
            this.button_SETCOR.TabIndex = 315;
            this.button_SETCOR.Text = "INSERIR";
            this.button_SETCOR.UseVisualStyleBackColor = true;
            this.button_SETCOR.Click += new System.EventHandler(this.button_SETCOR_Click);
            // 
            // textBox_ColorCod
            // 
            this.textBox_ColorCod.Location = new System.Drawing.Point(34, 16);
            this.textBox_ColorCod.Name = "textBox_ColorCod";
            this.textBox_ColorCod.ReadOnly = true;
            this.textBox_ColorCod.Size = new System.Drawing.Size(75, 20);
            this.textBox_ColorCod.TabIndex = 314;
            this.textBox_ColorCod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBox_Color
            // 
            this.pictureBox_Color.BackColor = System.Drawing.Color.White;
            this.pictureBox_Color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Color.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_Color.Location = new System.Drawing.Point(8, 16);
            this.pictureBox_Color.Name = "pictureBox_Color";
            this.pictureBox_Color.Size = new System.Drawing.Size(20, 49);
            this.pictureBox_Color.TabIndex = 313;
            this.pictureBox_Color.TabStop = false;
            this.pictureBox_Color.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // richTextBox_DESC_POS
            // 
            this.richTextBox_DESC_POS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_DESC_POS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.richTextBox_DESC_POS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_DESC_POS.ForeColor = System.Drawing.Color.White;
            this.richTextBox_DESC_POS.Location = new System.Drawing.Point(6, 157);
            this.richTextBox_DESC_POS.Name = "richTextBox_DESC_POS";
            this.richTextBox_DESC_POS.ReadOnly = true;
            this.richTextBox_DESC_POS.Size = new System.Drawing.Size(788, 281);
            this.richTextBox_DESC_POS.TabIndex = 4;
            this.richTextBox_DESC_POS.Text = "";
            this.richTextBox_DESC_POS.TextChanged += new System.EventHandler(this.richTextBox_DESC_POS_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.richTextBox_DESC_PRE);
            this.groupBox4.Controls.Add(this.pictureBox_color_Item);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(794, 135);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // richTextBox_DESC_PRE
            // 
            this.richTextBox_DESC_PRE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_DESC_PRE.BackColor = System.Drawing.Color.White;
            this.richTextBox_DESC_PRE.ForeColor = System.Drawing.Color.Black;
            this.richTextBox_DESC_PRE.Location = new System.Drawing.Point(9, 16);
            this.richTextBox_DESC_PRE.Name = "richTextBox_DESC_PRE";
            this.richTextBox_DESC_PRE.Size = new System.Drawing.Size(752, 113);
            this.richTextBox_DESC_PRE.TabIndex = 0;
            this.richTextBox_DESC_PRE.Text = "";
            this.richTextBox_DESC_PRE.TextChanged += new System.EventHandler(this.richTextBox_DESC_PRE_TextChanged);
            this.richTextBox_DESC_PRE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox_DESC_PRE_KeyPress);
            // 
            // pictureBox_color_Item
            // 
            this.pictureBox_color_Item.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_color_Item.Image = global::sELedit.Properties.Resources.InsertColor;
            this.pictureBox_color_Item.Location = new System.Drawing.Point(767, 16);
            this.pictureBox_color_Item.Name = "pictureBox_color_Item";
            this.pictureBox_color_Item.Size = new System.Drawing.Size(21, 20);
            this.pictureBox_color_Item.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_color_Item.TabIndex = 316;
            this.pictureBox_color_Item.TabStop = false;
            this.pictureBox_color_Item.Click += new System.EventHandler(this.pictureBox_color_Item_Click);
            // 
            // item_ext_desc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "item_ext_desc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ITEM EXT DESC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.item_ext_desc_FormClosing);
            this.Load += new System.EventHandler(this.item_ext_desc_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Color)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_color_Item)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox richTextBox_DESC_POS;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button_SETCOR;
        private System.Windows.Forms.TextBox textBox_ColorCod;
        private System.Windows.Forms.PictureBox pictureBox_Color;
        private System.Windows.Forms.RichTextBox richTextBox_DESC_PRE;
        private System.Windows.Forms.PictureBox pictureBox_color_Item;
    }
}