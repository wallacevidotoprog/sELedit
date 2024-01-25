namespace sELedit
{
    partial class Select_id
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Select_id));
            this.List_categories = new System.Windows.Forms.ListBox();
            this.Search_textbox = new System.Windows.Forms.TextBox();
            this.Items_grid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HideForm_button = new System.Windows.Forms.Button();
            this.AddNewItems_button = new System.Windows.Forms.Button();
            this.To_the_end_button = new System.Windows.Forms.Button();
            this.Continue_search = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.Items_grid)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // List_categories
            // 
            this.List_categories.AllowDrop = true;
            this.List_categories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.List_categories.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.List_categories.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.List_categories.ForeColor = System.Drawing.Color.Black;
            this.List_categories.FormattingEnabled = true;
            this.List_categories.ItemHeight = 16;
            this.List_categories.Location = new System.Drawing.Point(1, 0);
            this.List_categories.Name = "List_categories";
            this.List_categories.Size = new System.Drawing.Size(209, 498);
            this.List_categories.TabIndex = 0;
            this.List_categories.SelectedValueChanged += new System.EventHandler(this.List_categories_SelectedValueChanged);
            // 
            // Search_textbox
            // 
            this.Search_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Search_textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.Search_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Search_textbox.Location = new System.Drawing.Point(213, 1);
            this.Search_textbox.Name = "Search_textbox";
            this.Search_textbox.Size = new System.Drawing.Size(291, 24);
            this.Search_textbox.TabIndex = 1;
            this.Search_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Search_textbox.WordWrap = false;
            this.Search_textbox.TextChanged += new System.EventHandler(this.Search_textbox_TextChanged);
            // 
            // Items_grid
            // 
            this.Items_grid.AllowDrop = true;
            this.Items_grid.AllowUserToAddRows = false;
            this.Items_grid.AllowUserToDeleteRows = false;
            this.Items_grid.AllowUserToResizeColumns = false;
            this.Items_grid.AllowUserToResizeRows = false;
            this.Items_grid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Items_grid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.Items_grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.Items_grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Items_grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Items_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Items_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column2});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Items_grid.DefaultCellStyle = dataGridViewCellStyle4;
            this.Items_grid.EnableHeadersVisualStyles = false;
            this.Items_grid.GridColor = System.Drawing.Color.Silver;
            this.Items_grid.Location = new System.Drawing.Point(213, 26);
            this.Items_grid.Name = "Items_grid";
            this.Items_grid.ReadOnly = true;
            this.Items_grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Items_grid.RowHeadersVisible = false;
            this.Items_grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Items_grid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Items_grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Items_grid.ShowCellErrors = false;
            this.Items_grid.ShowCellToolTips = false;
            this.Items_grid.ShowEditingIcon = false;
            this.Items_grid.ShowRowErrors = false;
            this.Items_grid.Size = new System.Drawing.Size(352, 472);
            this.Items_grid.TabIndex = 13;
            this.Items_grid.DoubleClick += new System.EventHandler(this.Items_grid_DoubleClick);
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "GShopEditorByLuka.Properties.Resources.SmallQuestionMark";
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 23;
            // 
            // Column4
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Column4.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column4.HeaderText = "Name";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 257;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column2.Width = 70;
            // 
            // HideForm_button
            // 
            this.HideForm_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HideForm_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HideForm_button.Image = ((System.Drawing.Image)(resources.GetObject("HideForm_button.Image")));
            this.HideForm_button.Location = new System.Drawing.Point(283, 1);
            this.HideForm_button.Margin = new System.Windows.Forms.Padding(1);
            this.HideForm_button.Name = "HideForm_button";
            this.HideForm_button.Size = new System.Drawing.Size(281, 42);
            this.HideForm_button.TabIndex = 15;
            this.HideForm_button.Text = "Cancelar";
            this.HideForm_button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.HideForm_button.UseVisualStyleBackColor = true;
            this.HideForm_button.Click += new System.EventHandler(this.HideForm_button_Click);
            // 
            // AddNewItems_button
            // 
            this.AddNewItems_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddNewItems_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddNewItems_button.Image = ((System.Drawing.Image)(resources.GetObject("AddNewItems_button.Image")));
            this.AddNewItems_button.Location = new System.Drawing.Point(1, 1);
            this.AddNewItems_button.Margin = new System.Windows.Forms.Padding(1);
            this.AddNewItems_button.Name = "AddNewItems_button";
            this.AddNewItems_button.Size = new System.Drawing.Size(280, 42);
            this.AddNewItems_button.TabIndex = 14;
            this.AddNewItems_button.Text = "Salvar";
            this.AddNewItems_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddNewItems_button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.AddNewItems_button.UseVisualStyleBackColor = true;
            this.AddNewItems_button.Click += new System.EventHandler(this.AddNewItems_button_Click);
            // 
            // To_the_end_button
            // 
            this.To_the_end_button.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.To_the_end_button.FlatAppearance.BorderSize = 0;
            this.To_the_end_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.To_the_end_button.Image = ((System.Drawing.Image)(resources.GetObject("To_the_end_button.Image")));
            this.To_the_end_button.Location = new System.Drawing.Point(537, -1);
            this.To_the_end_button.Name = "To_the_end_button";
            this.To_the_end_button.Size = new System.Drawing.Size(29, 27);
            this.To_the_end_button.TabIndex = 3;
            this.To_the_end_button.UseVisualStyleBackColor = true;
            this.To_the_end_button.Click += new System.EventHandler(this.To_the_end_button_Click);
            // 
            // Continue_search
            // 
            this.Continue_search.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Continue_search.FlatAppearance.BorderSize = 0;
            this.Continue_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Continue_search.Image = ((System.Drawing.Image)(resources.GetObject("Continue_search.Image")));
            this.Continue_search.Location = new System.Drawing.Point(505, 0);
            this.Continue_search.Name = "Continue_search";
            this.Continue_search.Size = new System.Drawing.Size(32, 25);
            this.Continue_search.TabIndex = 2;
            this.Continue_search.UseVisualStyleBackColor = true;
            this.Continue_search.Click += new System.EventHandler(this.Continue_search_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.AddNewItems_button, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.HideForm_button, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 499);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(565, 44);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // Select_id
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(566, 542);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Items_grid);
            this.Controls.Add(this.To_the_end_button);
            this.Controls.Add(this.Continue_search);
            this.Controls.Add(this.Search_textbox);
            this.Controls.Add(this.List_categories);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Select_id";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Seleção de elementos com Elements.data";
            this.Load += new System.EventHandler(this.Select_id_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Items_grid)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Search_textbox;
        private System.Windows.Forms.Button Continue_search;
        private System.Windows.Forms.Button To_the_end_button;
        public System.Windows.Forms.ListBox List_categories;
        private System.Windows.Forms.Button AddNewItems_button;
        private System.Windows.Forms.Button HideForm_button;
        public System.Windows.Forms.DataGridView Items_grid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}