namespace sELedit.NOVO
{
	partial class ProbabilityEditorWindow
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridView_Elements = new System.Windows.Forms.DataGridView();
			this.Column_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column_Icon = new System.Windows.Forms.DataGridViewImageColumn();
			this.Column_Name = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Column_Progress = new sELedit.configs.DataGridViewProgressColumn();
			this.Column_Fixed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.numericUpDown_Probability = new System.Windows.Forms.NumericUpDown();
			this.trackBar_Probability = new System.Windows.Forms.TrackBar();
			this.button_Ok = new System.Windows.Forms.Button();
			this.button_Cancel = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.numericUpDown_Prob0Items = new System.Windows.Forms.NumericUpDown();
			this.checkBox_UseProb0Items = new System.Windows.Forms.CheckBox();
			this.checkBox_FixedProb0Items = new System.Windows.Forms.CheckBox();
			this.groupBox_Prob0Items = new System.Windows.Forms.GroupBox();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Elements)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Probability)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_Probability)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Prob0Items)).BeginInit();
			this.groupBox_Prob0Items.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridView_Elements
			// 
			this.dataGridView_Elements.AllowUserToAddRows = false;
			this.dataGridView_Elements.AllowUserToDeleteRows = false;
			this.dataGridView_Elements.AllowUserToResizeColumns = false;
			this.dataGridView_Elements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView_Elements.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
			this.dataGridView_Elements.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView_Elements.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView_Elements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_Elements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Id,
            this.Column_Icon,
            this.Column_Name,
            this.Column_Progress,
            this.Column_Fixed});
			this.dataGridView_Elements.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGridView_Elements.Location = new System.Drawing.Point(12, 12);
			this.dataGridView_Elements.MultiSelect = false;
			this.dataGridView_Elements.Name = "dataGridView_Elements";
			this.dataGridView_Elements.ReadOnly = true;
			this.dataGridView_Elements.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView_Elements.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView_Elements.RowHeadersWidth = 52;
			this.dataGridView_Elements.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView_Elements.Size = new System.Drawing.Size(560, 250);
			this.dataGridView_Elements.TabIndex = 0;
			this.dataGridView_Elements.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Elements_CellValueChanged);
			this.dataGridView_Elements.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView_Elements_Scroll);
			this.dataGridView_Elements.SelectionChanged += new System.EventHandler(this.dataGridView_Elements_SelectionChanged);
			this.dataGridView_Elements.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridView_Elements_MouseMove);
			// 
			// Column_Id
			// 
			this.Column_Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.Column_Id.DefaultCellStyle = dataGridViewCellStyle2;
			this.Column_Id.HeaderText = "ID";
			this.Column_Id.Name = "Column_Id";
			this.Column_Id.ReadOnly = true;
			this.Column_Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column_Id.Width = 24;
			// 
			// Column_Icon
			// 
			this.Column_Icon.HeaderText = "";
			this.Column_Icon.Name = "Column_Icon";
			this.Column_Icon.ReadOnly = true;
			this.Column_Icon.Width = 33;
			// 
			// Column_Name
			// 
			this.Column_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Column_Name.HeaderText = "Name";
			this.Column_Name.Name = "Column_Name";
			this.Column_Name.ReadOnly = true;
			this.Column_Name.Width = 41;
			// 
			// Column_Progress
			// 
			this.Column_Progress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Column_Progress.HeaderText = "";
			this.Column_Progress.Name = "Column_Progress";
			this.Column_Progress.ReadOnly = true;
			this.Column_Progress.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// Column_Fixed
			// 
			this.Column_Fixed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Column_Fixed.HeaderText = "Fixed";
			this.Column_Fixed.Name = "Column_Fixed";
			this.Column_Fixed.ReadOnly = true;
			this.Column_Fixed.Visible = false;
			// 
			// numericUpDown_Probability
			// 
			this.numericUpDown_Probability.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.numericUpDown_Probability.DecimalPlaces = 6;
			this.numericUpDown_Probability.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.numericUpDown_Probability.Location = new System.Drawing.Point(12, 316);
			this.numericUpDown_Probability.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown_Probability.Name = "numericUpDown_Probability";
			this.numericUpDown_Probability.Size = new System.Drawing.Size(70, 20);
			this.numericUpDown_Probability.TabIndex = 1;
			this.numericUpDown_Probability.ValueChanged += new System.EventHandler(this.numericUpDown_Probability_ValueChanged);
			// 
			// trackBar_Probability
			// 
			this.trackBar_Probability.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar_Probability.LargeChange = 1;
			this.trackBar_Probability.Location = new System.Drawing.Point(89, 314);
			this.trackBar_Probability.Maximum = 100;
			this.trackBar_Probability.Name = "trackBar_Probability";
			this.trackBar_Probability.Size = new System.Drawing.Size(418, 45);
			this.trackBar_Probability.TabIndex = 2;
			this.trackBar_Probability.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBar_Probability.ValueChanged += new System.EventHandler(this.trackBar_Probability_ValueChanged);
			// 
			// button_Ok
			// 
			this.button_Ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.button_Ok.ForeColor = System.Drawing.Color.Black;
			this.button_Ok.Location = new System.Drawing.Point(11, 343);
			this.button_Ok.Name = "button_Ok";
			this.button_Ok.Size = new System.Drawing.Size(274, 23);
			this.button_Ok.TabIndex = 3;
			this.button_Ok.Text = "Ok";
			this.button_Ok.UseVisualStyleBackColor = true;
			this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
			// 
			// button_Cancel
			// 
			this.button_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.button_Cancel.ForeColor = System.Drawing.Color.Black;
			this.button_Cancel.Location = new System.Drawing.Point(299, 343);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(274, 23);
			this.button_Cancel.TabIndex = 4;
			this.button_Cancel.Text = "Cancel";
			this.button_Cancel.UseVisualStyleBackColor = true;
			this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.ForeColor = System.Drawing.Color.Black;
			this.button1.Location = new System.Drawing.Point(513, 315);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(60, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "Ʃ 100%";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 90000;
			this.toolTip.InitialDelay = 500;
			this.toolTip.ReshowDelay = 100;
			this.toolTip.UseAnimation = false;
			this.toolTip.UseFading = false;
			// 
			// numericUpDown_Prob0Items
			// 
			this.numericUpDown_Prob0Items.DecimalPlaces = 6;
			this.numericUpDown_Prob0Items.Enabled = false;
			this.numericUpDown_Prob0Items.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.numericUpDown_Prob0Items.Location = new System.Drawing.Point(9, 15);
			this.numericUpDown_Prob0Items.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown_Prob0Items.Name = "numericUpDown_Prob0Items";
			this.numericUpDown_Prob0Items.Size = new System.Drawing.Size(70, 20);
			this.numericUpDown_Prob0Items.TabIndex = 6;
			this.numericUpDown_Prob0Items.ValueChanged += new System.EventHandler(this.numericUpDown_Prob0Items_ValueChanged);
			// 
			// checkBox_UseProb0Items
			// 
			this.checkBox_UseProb0Items.AutoSize = true;
			this.checkBox_UseProb0Items.Location = new System.Drawing.Point(9, -1);
			this.checkBox_UseProb0Items.Name = "checkBox_UseProb0Items";
			this.checkBox_UseProb0Items.Size = new System.Drawing.Size(133, 17);
			this.checkBox_UseProb0Items.TabIndex = 7;
			this.checkBox_UseProb0Items.Text = "Use Probability 0 Items";
			this.checkBox_UseProb0Items.UseVisualStyleBackColor = true;
			this.checkBox_UseProb0Items.CheckedChanged += new System.EventHandler(this.checkBox_UseProb0Items_CheckedChanged);
			// 
			// checkBox_FixedProb0Items
			// 
			this.checkBox_FixedProb0Items.AutoSize = true;
			this.checkBox_FixedProb0Items.Enabled = false;
			this.checkBox_FixedProb0Items.Location = new System.Drawing.Point(85, 18);
			this.checkBox_FixedProb0Items.Name = "checkBox_FixedProb0Items";
			this.checkBox_FixedProb0Items.Size = new System.Drawing.Size(139, 17);
			this.checkBox_FixedProb0Items.TabIndex = 8;
			this.checkBox_FixedProb0Items.Text = "Fixed Probability 0 Items";
			this.checkBox_FixedProb0Items.UseVisualStyleBackColor = true;
			// 
			// groupBox_Prob0Items
			// 
			this.groupBox_Prob0Items.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox_Prob0Items.Controls.Add(this.numericUpDown_Prob0Items);
			this.groupBox_Prob0Items.Controls.Add(this.checkBox_FixedProb0Items);
			this.groupBox_Prob0Items.Controls.Add(this.checkBox_UseProb0Items);
			this.groupBox_Prob0Items.ForeColor = System.Drawing.Color.White;
			this.groupBox_Prob0Items.Location = new System.Drawing.Point(12, 264);
			this.groupBox_Prob0Items.Name = "groupBox_Prob0Items";
			this.groupBox_Prob0Items.Size = new System.Drawing.Size(560, 44);
			this.groupBox_Prob0Items.TabIndex = 9;
			this.groupBox_Prob0Items.TabStop = false;
			this.groupBox_Prob0Items.Text = "Probability 0 Items";
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewTextBoxColumn1.HeaderText = "ID";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// dataGridViewImageColumn1
			// 
			this.dataGridViewImageColumn1.HeaderText = "";
			this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
			this.dataGridViewImageColumn1.Width = 33;
			// 
			// ProbabilityEditorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
			this.ClientSize = new System.Drawing.Size(584, 377);
			this.Controls.Add(this.groupBox_Prob0Items);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button_Cancel);
			this.Controls.Add(this.button_Ok);
			this.Controls.Add(this.trackBar_Probability);
			this.Controls.Add(this.numericUpDown_Probability);
			this.Controls.Add(this.dataGridView_Elements);
			this.ForeColor = System.Drawing.Color.Black;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(600, 416);
			this.Name = "ProbabilityEditorWindow";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ProbabilityEditorWindow";
			this.Load += new System.EventHandler(this.ProbabilityEditorWindow_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Elements)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Probability)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_Probability)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Prob0Items)).EndInit();
			this.groupBox_Prob0Items.ResumeLayout(false);
			this.groupBox_Prob0Items.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView_Elements;
		private System.Windows.Forms.NumericUpDown numericUpDown_Probability;
		private System.Windows.Forms.TrackBar trackBar_Probability;
		private System.Windows.Forms.Button button_Ok;
		private System.Windows.Forms.Button button_Cancel;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.NumericUpDown numericUpDown_Prob0Items;
		private System.Windows.Forms.CheckBox checkBox_UseProb0Items;
		private System.Windows.Forms.CheckBox checkBox_FixedProb0Items;
		private System.Windows.Forms.GroupBox groupBox_Prob0Items;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column_Id;
		private System.Windows.Forms.DataGridViewImageColumn Column_Icon;
		private System.Windows.Forms.DataGridViewButtonColumn Column_Name;
		private configs.DataGridViewProgressColumn Column_Progress;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Column_Fixed;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
	}
}