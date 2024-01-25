namespace sELedit.Previews
{
    partial class SkiViewerModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkiViewerModel));
            this.elementHost_SKI = new System.Windows.Forms.Integration.ElementHost();
            this.skiViewer1 = new sELedit.Previews.SkiViewer();
            this.SuspendLayout();
            // 
            // elementHost_SKI
            // 
            this.elementHost_SKI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost_SKI.Location = new System.Drawing.Point(0, 0);
            this.elementHost_SKI.Name = "elementHost_SKI";
            this.elementHost_SKI.Size = new System.Drawing.Size(906, 537);
            this.elementHost_SKI.TabIndex = 0;
            this.elementHost_SKI.Text = "elementHost1";
            this.elementHost_SKI.Child = this.skiViewer1;
            // 
            // SkiViewerModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 537);
            this.Controls.Add(this.elementHost_SKI);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SkiViewerModel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SkiViewerModel";
            this.Load += new System.EventHandler(this.SkiViewerModel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost_SKI;
        private SkiViewer skiViewer1;
    }
}