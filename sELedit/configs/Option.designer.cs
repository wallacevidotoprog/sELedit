namespace sELedit
{
    partial class Option
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Option));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Configs_search = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Configs_path = new System.Windows.Forms.TextBox();
            this.Surfaces_pck_search = new System.Windows.Forms.Button();
            this.Elements_data_search = new System.Windows.Forms.Button();
            this.Surfaces_path_textbox = new System.Windows.Forms.TextBox();
            this.Elements_path_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Dialog_elements = new System.Windows.Forms.OpenFileDialog();
            this.Dialog_surfaces = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Dialog_configs = new System.Windows.Forms.OpenFileDialog();
            this.Reload_checkbox = new System.Windows.Forms.CheckBox();
            this.Exit_button = new System.Windows.Forms.Button();
            this.Accept_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.Configs_search);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Configs_path);
            this.groupBox1.Controls.Add(this.Surfaces_pck_search);
            this.groupBox1.Controls.Add(this.Elements_data_search);
            this.groupBox1.Controls.Add(this.Surfaces_path_textbox);
            this.groupBox1.Controls.Add(this.Elements_path_textbox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(1, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(422, 97);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // Configs_search
            // 
            this.Configs_search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Configs_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Configs_search.Image = ((System.Drawing.Image)(resources.GetObject("Configs_search.Image")));
            this.Configs_search.Location = new System.Drawing.Point(401, 73);
            this.Configs_search.Name = "Configs_search";
            this.Configs_search.Size = new System.Drawing.Size(20, 20);
            this.Configs_search.TabIndex = 8;
            this.Configs_search.UseVisualStyleBackColor = false;
            this.Configs_search.Click += new System.EventHandler(this.Configs_search_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(17, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Configs.pck:";
            // 
            // Configs_path
            // 
            this.Configs_path.Location = new System.Drawing.Point(109, 73);
            this.Configs_path.Name = "Configs_path";
            this.Configs_path.Size = new System.Drawing.Size(292, 20);
            this.Configs_path.TabIndex = 6;
            // 
            // Surfaces_pck_search
            // 
            this.Surfaces_pck_search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Surfaces_pck_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Surfaces_pck_search.Image = ((System.Drawing.Image)(resources.GetObject("Surfaces_pck_search.Image")));
            this.Surfaces_pck_search.Location = new System.Drawing.Point(401, 43);
            this.Surfaces_pck_search.Name = "Surfaces_pck_search";
            this.Surfaces_pck_search.Size = new System.Drawing.Size(20, 20);
            this.Surfaces_pck_search.TabIndex = 5;
            this.Surfaces_pck_search.UseVisualStyleBackColor = false;
            this.Surfaces_pck_search.Click += new System.EventHandler(this.Surfaces_pck_search_Click);
            // 
            // Elements_data_search
            // 
            this.Elements_data_search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Elements_data_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Elements_data_search.Image = ((System.Drawing.Image)(resources.GetObject("Elements_data_search.Image")));
            this.Elements_data_search.Location = new System.Drawing.Point(401, 13);
            this.Elements_data_search.Name = "Elements_data_search";
            this.Elements_data_search.Size = new System.Drawing.Size(20, 20);
            this.Elements_data_search.TabIndex = 4;
            this.Elements_data_search.UseVisualStyleBackColor = false;
            this.Elements_data_search.Click += new System.EventHandler(this.Elements_data_search_Click);
            // 
            // Surfaces_path_textbox
            // 
            this.Surfaces_path_textbox.Location = new System.Drawing.Point(109, 43);
            this.Surfaces_path_textbox.Name = "Surfaces_path_textbox";
            this.Surfaces_path_textbox.Size = new System.Drawing.Size(292, 20);
            this.Surfaces_path_textbox.TabIndex = 3;
            // 
            // Elements_path_textbox
            // 
            this.Elements_path_textbox.Location = new System.Drawing.Point(109, 13);
            this.Elements_path_textbox.Name = "Elements_path_textbox";
            this.Elements_path_textbox.Size = new System.Drawing.Size(292, 20);
            this.Elements_path_textbox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(11, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Surfaces.pck:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Elements.data:";
            // 
            // Dialog_elements
            // 
            this.Dialog_elements.FileName = "Elements.data";
            this.Dialog_elements.Filter = "Data|*.data|All Files|*.*";
            // 
            // Dialog_surfaces
            // 
            this.Dialog_surfaces.FileName = "Surfaces.pck";
            this.Dialog_surfaces.Filter = "Anjelica Engine|*.pck";
            // 
            // Dialog_configs
            // 
            this.Dialog_configs.FileName = "Configs.pck";
            this.Dialog_configs.Filter = "Anjelica Engine|*.pck";
            // 
            // Reload_checkbox
            // 
            this.Reload_checkbox.AutoSize = true;
            this.Reload_checkbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Reload_checkbox.Checked = true;
            this.Reload_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Reload_checkbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Reload_checkbox.ForeColor = System.Drawing.Color.Black;
            this.Reload_checkbox.Location = new System.Drawing.Point(0, 1);
            this.Reload_checkbox.Name = "Reload_checkbox";
            this.Reload_checkbox.Size = new System.Drawing.Size(153, 20);
            this.Reload_checkbox.TabIndex = 3;
            this.Reload_checkbox.Text = "Reload Surfaces.pck";
            this.Reload_checkbox.UseVisualStyleBackColor = true;
            // 
            // Exit_button
            // 
            this.Exit_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Exit_button.Image = ((System.Drawing.Image)(resources.GetObject("Exit_button.Image")));
            this.Exit_button.Location = new System.Drawing.Point(213, 115);
            this.Exit_button.Name = "Exit_button";
            this.Exit_button.Size = new System.Drawing.Size(210, 40);
            this.Exit_button.TabIndex = 2;
            this.Exit_button.Text = "Cancelar";
            this.Exit_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Exit_button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Exit_button.UseVisualStyleBackColor = true;
            this.Exit_button.Click += new System.EventHandler(this.Exit_options_button);
            // 
            // Accept_button
            // 
            this.Accept_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Accept_button.Image = ((System.Drawing.Image)(resources.GetObject("Accept_button.Image")));
            this.Accept_button.Location = new System.Drawing.Point(1, 115);
            this.Accept_button.Name = "Accept_button";
            this.Accept_button.Size = new System.Drawing.Size(210, 40);
            this.Accept_button.TabIndex = 1;
            this.Accept_button.Text = "Aceitar";
            this.Accept_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Accept_button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Accept_button.UseVisualStyleBackColor = true;
            this.Accept_button.Click += new System.EventHandler(this.Accept_button_Click);
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(424, 156);
            this.Controls.Add(this.Reload_checkbox);
            this.Controls.Add(this.Exit_button);
            this.Controls.Add(this.Accept_button);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Option";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configurações";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Surfaces_pck_search;
        private System.Windows.Forms.Button Elements_data_search;
        private System.Windows.Forms.TextBox Surfaces_path_textbox;
        private System.Windows.Forms.TextBox Elements_path_textbox;
        private System.Windows.Forms.Button Accept_button;
        private System.Windows.Forms.Button Exit_button;
        private System.Windows.Forms.OpenFileDialog Dialog_elements;
        private System.Windows.Forms.OpenFileDialog Dialog_surfaces;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button Configs_search;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Configs_path;
        private System.Windows.Forms.OpenFileDialog Dialog_configs;
        private System.Windows.Forms.CheckBox Reload_checkbox;
    }
}