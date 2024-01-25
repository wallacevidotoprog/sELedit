namespace sELedit
{
    partial class LoasdsProgreces
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
            this.label_read = new System.Windows.Forms.Label();
            this.progressBar_read = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label_read
            // 
            this.label_read.AutoSize = true;
            this.label_read.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_read.Location = new System.Drawing.Point(13, 15);
            this.label_read.Name = "label_read";
            this.label_read.Size = new System.Drawing.Size(90, 17);
            this.label_read.TabIndex = 3;
            this.label_read.Text = "READ (7/7)";
            // 
            // progressBar_read
            // 
            this.progressBar_read.Location = new System.Drawing.Point(115, 12);
            this.progressBar_read.Name = "progressBar_read";
            this.progressBar_read.Size = new System.Drawing.Size(315, 23);
            this.progressBar_read.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LoasdsProgreces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 63);
            this.Controls.Add(this.label_read);
            this.Controls.Add(this.progressBar_read);
            this.Name = "LoasdsProgreces";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_read;
        private System.Windows.Forms.ProgressBar progressBar_read;
        private System.Windows.Forms.Timer timer1;
    }
}