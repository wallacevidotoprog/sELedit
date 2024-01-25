//(c)Rey35
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace sELedit.NOVO
{
	public partial class ProbabilityEditorWindow : Form
	{
		public ElementProbability[] Elements;
		private int ElenetType;
		private int LeaveIndex = 0;
		private float Prob0Items = 0;
		private bool FormLoaded = false;
		private bool EnableTrackBarValueChange = false;
		private bool EnableNumericUpDownValueChange = false;
		private bool EnableNumericUpDown_Prob0ItemsValueChange = false;
		private bool EnableDataGridViewCellValueChange = false;
		private bool Scrolling = false;

		public ProbabilityEditorWindow(ElementProbability[] elements, int element_type = 0)
		{
			InitializeComponent();
			//if (MainWindow.W_ProbabilityEditorSize.Width > 0 || MainWindow.W_ProbabilityEditorSize.Height > 0)
			//    Size = MainWindow.W_ProbabilityEditorSize;
			Elements = elements;
			ElenetType = element_type;
			this.SetLocalization();
			float ProbAllitems = 0;
			for (int i = 0; i < Elements.Length; i++)
				if (Elements[i].Probability != 1)
					ProbAllitems += Elements[i].Probability;
			if (ProbAllitems > 1)
			{
				float tmp = ProbAllitems - 1;
				ProbAllitems = ProbAllitems - tmp;
			}
			if (ProbAllitems != 1 && ProbAllitems != 0)
			{
				Prob0Items = 1.0f - ProbAllitems;
				checkBox_UseProb0Items.Checked = true;
			}
			//if (Settings.Fields.EnableShowPercents)
			// {
			numericUpDown_Probability.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
			numericUpDown_Probability.Increment = new decimal(new int[] { 1, 0, 0, 0 });
			numericUpDown_Probability.DecimalPlaces = 4;
			numericUpDown_Prob0Items.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
			numericUpDown_Prob0Items.Increment = new decimal(new int[] { 1, 0, 0, 0 });
			numericUpDown_Prob0Items.DecimalPlaces = 4;
			// }
			//// if (Settings.Fields.EnableShowPercents)
			numericUpDown_Prob0Items.Value = Convert.ToDecimal(Prob0Items * 100);
			//  else
			//     numericUpDown_Prob0Items.Value = Convert.ToDecimal(Prob0Items);
		}

		private void ProbabilityEditorWindow_Load(object sender, EventArgs e)
		{
			switch (ElenetType)
			{
				case 0:					
					dataGridView_Elements.Columns[1].Visible = true;
					for (int i = 0; i < Elements.Length; i++)
					{
						dataGridView_Elements.Rows.Add(new object[] { Elements[i].Id, Properties.Resources.back, "nuul", Elements[i].Probability.ToString(), false });
						dataGridView_Elements.Rows[i].HeaderCell.Value = (i + 1).ToString();
					}
					//dataGridView_Elements_SizeChanged(null, null);
					break;
				case 1:
					dataGridView_Elements.Columns[1].Visible = false;
					for (int i = 0; i < Elements.Length; i++)
					{
						dataGridView_Elements.Rows.Add(new object[] { Elements[i].Id, null, EQUIPMENT_ADDON.GetAddon(Elements[i].Id.ToString()), Elements[i].Probability.ToString(), null, false });
						dataGridView_Elements.Rows[i].HeaderCell.Value = (i + 1).ToString();
					}
					//dataGridView_Elements_SizeChanged(null, null);
					break;
			}
			EnableTrackBarValueChange = true;
			EnableNumericUpDownValueChange = true;
			EnableNumericUpDown_Prob0ItemsValueChange = true;
			EnableDataGridViewCellValueChange = true;
			FormLoaded = true;
			dataGridView_Elements_SelectionChanged(null, null);
		}

		private void dataGridView_Elements_SelectionChanged(object sender, EventArgs e)
		{
			if (dataGridView_Elements.CurrentRow.Index > -1 && FormLoaded)
			{
				EnableTrackBarValueChange = false;
				EnableNumericUpDownValueChange = false;
				EnableNumericUpDown_Prob0ItemsValueChange = false;
				EnableDataGridViewCellValueChange = false;
				if (true)
					numericUpDown_Probability.Value = Convert.ToDecimal(Elements[dataGridView_Elements.CurrentRow.Index].Probability);
				else
					numericUpDown_Probability.Value = Convert.ToDecimal(Elements[dataGridView_Elements.CurrentRow.Index].Probability);
				trackBar_Probability.Value = Convert.ToInt32(Elements[dataGridView_Elements.CurrentRow.Index].Probability );
				EnableTrackBarValueChange = true;
				EnableNumericUpDownValueChange = true;
				EnableNumericUpDown_Prob0ItemsValueChange = true;
				EnableDataGridViewCellValueChange = true;
			}
		}

		private void dataGridView_Elements_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (EnableDataGridViewCellValueChange && FormLoaded)
			{
				EnableDataGridViewCellValueChange = false;
				switch (dataGridView_Elements.CurrentCell.ColumnIndex)
				{
					case 3:
						Elements[dataGridView_Elements.CurrentRow.Index].Probability = mConvert.PercentNumberToSingle(dataGridView_Elements.Rows[dataGridView_Elements.CurrentRow.Index].Cells[3].Value,true);
						//dataGridView_Elements_SizeChanged(null, null);
						break;
				}
				EnableDataGridViewCellValueChange = true;
			}
		}

		private void trackBar_Probability_ValueChanged(object sender, EventArgs e)
		{
			if (EnableTrackBarValueChange)
			{
				EnableTrackBarValueChange = false;
				EnableNumericUpDownValueChange = false;
				EnableNumericUpDown_Prob0ItemsValueChange = false;
				EnableDataGridViewCellValueChange = false;
				RecalculateProbabilities();
				//if (Settings.Fields.EnableShowPercents)
				numericUpDown_Probability.Value = Convert.ToDecimal(Elements[dataGridView_Elements.CurrentRow.Index].Probability);
				//else
				//numericUpDown_Probability.Value = Convert.ToDecimal(Elements[dataGridView_Elements.CurrentRow.Index].Probability);
				//dataGridView_Elements_SizeChanged(null, null);
				EnableTrackBarValueChange = true;
				EnableNumericUpDownValueChange = true;
				EnableNumericUpDown_Prob0ItemsValueChange = true;
				EnableDataGridViewCellValueChange = true;
			}
		}

		private void numericUpDown_Probability_ValueChanged(object sender, EventArgs e)
		{
			if (EnableNumericUpDownValueChange)
			{
				EnableNumericUpDownValueChange = false;
				// if (Settings.Fields.EnableShowPercents)
				trackBar_Probability.Value = Convert.ToInt32(numericUpDown_Probability.Value);
				// else
				///   trackBar_Probability.Value = Convert.ToInt32(numericUpDown_Probability.Value * 100);
				EnableNumericUpDownValueChange = true;
			}
		}

		private void RecalculateProbabilities()
		{
			if (dataGridView_Elements.RowCount == 0/* || dataGridView_Elements.SelectedRows.Count == 0*/)
				return;
			int changed_index = /*dataGridView_Elements.SelectedRows[0].Index*/dataGridView_Elements.CurrentRow.Index; //индекс измененной вероятности
			float new_prob = ((float)trackBar_Probability.Value / 100);
			if (new_prob < 0.000001f)
				new_prob = 0.000000f;
			else if (new_prob > 1.000000f)
				new_prob = 1.000000f;

			List<int> HundredPercentElementIndexes = new List<int>();
			for (int i = 0; i < Elements.Length; i++)
				if (Elements[i].Probability == 1 && i != changed_index)
					HundredPercentElementIndexes.Add(i);

			float new_balance = 1.000000f; //сколько всего единиц будет доступно для распределения вероятностей
			float old_balance = 0.000000f; //текущее кол-во доступных единиц (используется для вычисления коэффициентов)
			int fix_count = 0; //кол-во закрепленных вероятностей

			float sum = 0.000000f;
			for (int i = 0; i < Elements.Length; i++)
			{
				if (i == changed_index || CheckHundredPercentElement(HundredPercentElementIndexes, i)) //исключаю изменившуюся вероятность
					continue;
				sum += Elements[i].Probability;
				bool bFixed = (bool)dataGridView_Elements[4, i].Value;
				if (bFixed)
				{
					new_balance -= Elements[i].Probability;
					fix_count++;
				}
				else
				{
					old_balance += Elements[i].Probability;
				}
			}
			if (checkBox_UseProb0Items.Checked)
			{
				sum += Prob0Items;
				if (checkBox_FixedProb0Items.Checked)
				{
					new_balance -= Prob0Items;
					fix_count++;
				}
				else
				{
					old_balance += Prob0Items;
				}
			}

			bool bProbsIsGood = (Math.Abs(1.000000f - sum) < 0.000005 && Elements[changed_index].Probability > 0.000005);
			if (!bProbsIsGood)
			{
				if (new_balance - new_prob < 0.000000f && old_balance > 0.000000f)
					new_prob = (Elements[changed_index].Probability + old_balance);
				new_balance -= new_prob;
				if (checkBox_UseProb0Items.Checked)
				{
					if (new_balance < 0.000000f || (old_balance == 0.000000f && fix_count >= Elements.Length))
					{ //если шансы станут неправильными
						trackBar_Probability.Value = Convert.ToInt32(Elements[changed_index].Probability);
						return;
					}
				}
				else
				{
					if (new_balance < 0.000000f || (old_balance == 0.000000f && fix_count >= Elements.Length - 1))
					{ //если шансы станут неправильными
						trackBar_Probability.Value = Convert.ToInt32(Elements[changed_index].Probability);
						return;
					}
				}

				Elements[changed_index].Probability = new_prob;
				dataGridView_Elements[3, changed_index].Value = Elements[changed_index].Probability.ToString();

				#region Пересчитывание остальных вероятностей
				for (int i = 0; i < Elements.Length; i++)
				{
					if ((bool)dataGridView_Elements[4, i].Value || i == changed_index || CheckHundredPercentElement(HundredPercentElementIndexes, i))
						continue;
					float x; //коэффициент
					if (old_balance > 0.000000f)
					{
						x = Elements[i].Probability / old_balance;
					}
					else
					{
						if (checkBox_UseProb0Items.Checked)
							x = 1.0000000f / (Elements.Length + 1);
						else
							x = 1.0000000f / Elements.Length;
					}
					if (new_balance <= 0.000000f) x = 0.000000f;
					Elements[i].Probability = new_balance * x;
					dataGridView_Elements[3, i].Value = Elements[i].Probability.ToString();
				}
				if (checkBox_UseProb0Items.Checked && !checkBox_FixedProb0Items.Checked)
				{
					float x;
					if (old_balance > 0.000000f)
					{
						x = Prob0Items / old_balance;
					}
					else
					{
						x = 1.0000000f	 / (Elements.Length + 1);
					}
					if (new_balance <= 0.000000f) x = 0.000000f;
					Prob0Items = new_balance * x;
					if (true)
						numericUpDown_Prob0Items.Value = Convert.ToDecimal(Prob0Items * 100);
					else
						numericUpDown_Prob0Items.Value = Convert.ToDecimal(Prob0Items);
				}
				#endregion
				#region Контрольная проверка вероятностей
				sum = 0.0f;
				for (int i = 0; i < Elements.Length; i++)
				{
					if (!CheckHundredPercentElement(HundredPercentElementIndexes, i))
						sum += Elements[i].Probability;
				}
				if (checkBox_UseProb0Items.Checked)
				{
					sum += Prob0Items;
				}
			}
			if (sum != 1.0000000f)
			{
				int index = GetMaxValueIndex(Elements, HundredPercentElementIndexes);
				if (checkBox_UseProb0Items.Checked)
				{
					int result = -1;
					float max_value = 0;
					max_value = Prob0Items;
					for (int i = 0; i < Elements.Length; i++)
					{
						if (!CheckHundredPercentElement(HundredPercentElementIndexes, i))
						{
							if (Elements[i].Probability > max_value)
							{
								result = i;
								max_value = Elements[i].Probability;
							}
						}
					}
					if (result == -1)
					{
						float value = Prob0Items;
						value += 1.0000000f - sum;
						if (value < 0.000000f)
							value = 0.000000f;
						if (true)
							numericUpDown_Prob0Items.Value = Convert.ToDecimal(value * 100);
						else
							numericUpDown_Prob0Items.Value = Convert.ToDecimal(value);
					}
					else
					{
						Elements[index].Probability += 1.0000000f - sum;
						if (Elements[index].Probability < 0.000000f)
							Elements[index].Probability = 0.000000f;
						dataGridView_Elements[3, index].Value = Elements[index].Probability.ToString();
					}
				}
				else
				{
					Elements[index].Probability += 1.0000000f - sum;
					if (Elements[index].Probability < 0.000000f)
						Elements[index].Probability = 0.000000f;
					dataGridView_Elements[3, index].Value = Elements[index].Probability.ToString();
				}
			}
			#endregion
		}

		private void numericUpDown_Prob0Items_ValueChanged(object sender, EventArgs e)
		{
			if (EnableNumericUpDown_Prob0ItemsValueChange)
			{
				EnableTrackBarValueChange = false;
				EnableNumericUpDownValueChange = false;
				EnableNumericUpDown_Prob0ItemsValueChange = false;
				EnableDataGridViewCellValueChange = false;
				if (dataGridView_Elements.RowCount == 0/* || dataGridView_Elements.SelectedRows.Count == 0*/)
					return;
				int changed_index = -1; //индекс измененной вероятности
				float new_prob = 0;
				//if (true)
				//{
				//	new_prob = ((float)numericUpDown_Prob0Items.Value / 1000);
				//	new_prob = new_prob * 10;
				//}
				//else
					new_prob = ((float)numericUpDown_Prob0Items.Value);
				if (new_prob < 0.000001f)
					new_prob = 0.000000f;
				else if (new_prob > 1.0000000f)
					new_prob = 1.0f;

				List<int> HundredPercentElementIndexes = new List<int>();
				for (int i = 0; i < Elements.Length; i++)
					if (Elements[i].Probability == 1 && i != changed_index)
						HundredPercentElementIndexes.Add(i);

				float new_balance = 1.0000000f; //сколько всего единиц будет доступно для распределения вероятностей
				float old_balance = 0.000000f; //текущее кол-во доступных единиц (используется для вычисления коэффициентов)
				int fix_count = 0; //кол-во закрепленных вероятностей

				float sum = 0.000000f;
				for (int i = 0; i < Elements.Length; i++)
				{
					if (i == changed_index || CheckHundredPercentElement(HundredPercentElementIndexes, i)) //исключаю изменившуюся вероятность
						continue;
					sum += Elements[i].Probability;
					bool bFixed = (bool)dataGridView_Elements[4, i].Value;
					if (bFixed)
					{
						new_balance -= Elements[i].Probability;
						fix_count++;
					}
					else
					{
						old_balance += Elements[i].Probability;
					}
				}

				bool bProbsIsGood = (Math.Abs(1.0000000f - sum) < 0.000005 && Prob0Items > 0.000005);
				if (!bProbsIsGood)
				{
					if (new_balance - new_prob < 0.0f && old_balance > 0.0f)
						new_prob = Prob0Items + old_balance;
					new_balance -= new_prob;
					if (new_balance < 0.0f || (old_balance == 0.0f && fix_count >= Elements.Length))
					{ //если шансы станут неправильными

						if (true)
							numericUpDown_Prob0Items.Value = Convert.ToDecimal(Prob0Items * 100.0f);
						else
							numericUpDown_Prob0Items.Value = Convert.ToDecimal(Prob0Items);
						return;
					}

					Prob0Items = new_prob;
					if (true)
						numericUpDown_Prob0Items.Value = Convert.ToDecimal(Prob0Items * 100.0f);
					else
						numericUpDown_Prob0Items.Value = Convert.ToDecimal(Prob0Items);

					#region Пересчитывание остальных вероятностей
					for (int i = 0; i < Elements.Length; i++)
					{
						if ((bool)dataGridView_Elements[4, i].Value || i == changed_index || CheckHundredPercentElement(HundredPercentElementIndexes, i))
							continue;
						float x; //коэффициент
						if (old_balance > 0.000000f)
						{
							x = Elements[i].Probability / old_balance;
						}
						else
						{
							if (checkBox_UseProb0Items.Checked)
								x = 1.000000f / (Elements.Length + 1);
							else
								x = 1.000000f / Elements.Length;
						}
						if (new_balance <= 0.000000f) x = 0.000000f;
						Elements[i].Probability = new_balance * x;
						dataGridView_Elements[3, i].Value = Elements[i].Probability.ToString();
					}
					#endregion
					#region Контрольная проверка вероятностей
					sum = 0.000000f;
					for (int i = 0; i < Elements.Length; i++)
					{
						if (!CheckHundredPercentElement(HundredPercentElementIndexes, i))
							sum += Elements[i].Probability;
					}
					if (checkBox_UseProb0Items.Checked)
					{
						sum += Prob0Items;
					}
				}
				if (sum != 1.000000f)
				{
					int index = GetMaxValueIndex(Elements, HundredPercentElementIndexes);
					if (checkBox_UseProb0Items.Checked)
					{
						int result = -1;
						float max_value = 0;
						max_value = Prob0Items;
						for (int i = 0; i < Elements.Length; i++)
						{
							if (!CheckHundredPercentElement(HundredPercentElementIndexes, i))
							{
								if (Elements[i].Probability > max_value)
								{
									result = i;
									max_value = Elements[i].Probability;
								}
							}
						}
						if (result == -1)
						{
							float value = Prob0Items;
							value += 1.0000000f - sum;
							if (value < 0.000000f)
								value = 0.000000f;
							if (true)
								numericUpDown_Prob0Items.Value = Convert.ToDecimal(value * 100);
							else
								numericUpDown_Prob0Items.Value = Convert.ToDecimal(value);
						}
						else
						{
							Elements[index].Probability += 1.0000000f - sum;
							if (Elements[index].Probability < 0.000000f)
								Elements[index].Probability = 0.000000f;
							dataGridView_Elements[3, index].Value = Elements[index].Probability.ToString();
						}
					}
					else
					{
						Elements[index].Probability += 1.0000000f - sum;
						if (Elements[index].Probability < 0.000000f)
							Elements[index].Probability = 0.000000f;
						dataGridView_Elements[3, index].Value = Elements[index].Probability.ToString();
					}
				}
				#endregion
				EnableTrackBarValueChange = true;
				EnableNumericUpDownValueChange = true;
				EnableNumericUpDown_Prob0ItemsValueChange = true;
				EnableDataGridViewCellValueChange = true;
			}
		}

		bool CheckHundredPercentElement(List<int> Indexes, int ElementIndex)
		{
			for (int i = 0; i < Indexes.Count; i++)
				if (ElementIndex == Indexes[i])
					return true;
			return false;
		}

		int GetMaxValueIndex(ElementProbability[] Elements, List<int> HundredPercentElementIndexes)
		{
			int result = 0;
			float max_value = 0;
			for (int i = 0; i < Elements.Length; i++)
			{
				if (!CheckHundredPercentElement(HundredPercentElementIndexes, i))
				{
					if (Elements[i].Probability > max_value)
					{
						result = i;
						max_value = Elements[i].Probability;
					}
				}
			}
			return result;
		}

		//private void dataGridView_Elements_SizeChanged(object sender, EventArgs e)
		//{
		//	EnableDataGridViewCellValueChange = false;
		//	for (int i = 0; i < dataGridView_Elements.Rows.Count; i++)
		//		dataGridView_Elements.Rows[i].Cells[4].Value = DrawProgress(Elements[i].Probability, dataGridView_Elements.Columns[4].Width, dataGridView_Elements.Rows[0].Height + 1);
		//	// MainWindow.W_ProbabilityEditorSize = Size;
		//	EnableDataGridViewCellValueChange = true;
		//}

		//Image DrawProgress(float value, int width, int height)
		//{
		//	height -= 2;
		//	Image result = new Bitmap(width, height);
		//	Graphics g = Graphics.FromImage(result);
		//	if (value < 0.0f)
		//		value = 0.0f;
		//	else if (value > 1.0f)
		//		value = 1.0f;

		//	g.FillRectangle(new SolidBrush(Color.LightGray), 0, 0, width, height);
		//	g.DrawRectangle(new Pen(Color.LightGray/*Gray*/, 2.0f), 0, 0, width, height);
		//	if (value > 0.000001f)
		//	{
		//		int prog_width = (int)(value * width/* - 2*/);
		//		if (prog_width < 1)
		//			prog_width = 1;
		//		g.FillRectangle(new SolidBrush(Color.DarkGreen), /*1, 1*/0, 0, prog_width, height/* - 2*/);
		//	}
		//	return result;
		//}

		private void button1_Click(object sender, EventArgs e)
		{
			int changed_elements_count = 0;
			for (int i = 0; i < Elements.Length; i++)
				if (!(bool)dataGridView_Elements.Rows[i].Cells[4].Value)
					changed_elements_count++;
			if (checkBox_UseProb0Items.Checked && !checkBox_FixedProb0Items.Checked)
				changed_elements_count++;
			float Value = 1f / changed_elements_count;
			for (int i = 0; i < Elements.Length; i++)
			{
				if (!(bool)dataGridView_Elements.Rows[i].Cells[4].Value)
				{
					Elements[i].Probability = Value;
					dataGridView_Elements.Rows[i].Cells[3].Value = Elements[i].Probability.ToString();
				}
			}
			if (checkBox_UseProb0Items.Checked && !checkBox_FixedProb0Items.Checked)
			{
				Prob0Items = Value;
				// if (Settings.Fields.EnableShowPercents)
				//numericUpDown_Prob0Items.Value = Convert.ToDecimal(Prob0Items * 100);
				//else
				    numericUpDown_Prob0Items.Value = Convert.ToDecimal(Prob0Items);
			}
			dataGridView_Elements_SelectionChanged(null, null);
			//dataGridView_Elements_SizeChanged(null, null);
		}

		//private void dataGridView_Elements_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
		//{
		//    if (!Scrolling && e.ColumnIndex >= 0 && e.ColumnIndex < 3 && e.RowIndex > -1 && (!Settings.Fields.UseUdE || !UdE.API.IsElementsEditorRunning) && Settings.Fields.EnableShowInfo)
		//    {
		//        try
		//        {
		//            int Id = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value);
		//            if (Id > 0)
		//            {
		//                string text = "";
		//                ((DataGridView)sender).ShowCellToolTips = false;
		//                if (MainWindow.LoadedElements)
		//                {
		//                    switch (ElenetType)
		//                    {
		//                        case 0:
		//                            text = tasks.GlobalFunctions.ColorClean(GlobalProgramData.GetItemProps(Id, 0));
		//                            toolTip.ToolTipTitle = tasks.GlobalFunctions.ColorClean(GlobalProgramData.GetItemName(Id));
		//                            break;
		//                        case 1:
		//                            text = tasks.GlobalFunctions.ColorClean(GlobalProgramData.GetMonsterNPCMineProps(Id)) + "\n";
		//                            toolTip.ToolTipTitle = tasks.GlobalFunctions.ColorClean(GlobalProgramData.GetMonsterNPCMineName(Id));
		//                            break;
		//                    }
		//                }
		//                if (ElenetType == 0)
		//                    text += tasks.GlobalFunctions.ColorClean(GlobalProgramData.GetItemDesc(Id)) + "\n";
		//                Point point = Extensions.SetToolTipPos((Control)sender);
		//                toolTip.Show(text, (Control)sender, point.X, point.Y);
		//            }

		//        }
		//        catch
		//        {
		//            ((DataGridView)sender).ShowCellToolTips = true;
		//        }
		//    }
		//    else
		//        ((DataGridView)sender).ShowCellToolTips = true;
		//}

		private void dataGridView_Elements_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			toolTip.Hide((Control)sender);
			/*if (Settings.Fields.UseUdE && UdE.API.IsElementsEditorRunning)
				TaskToolTip.ShowTaskHint(new Control(), null, 0);*/
			if (ModifierKeys == Keys.Alt && MouseButtons == MouseButtons.Right)
				LeaveIndex = e.RowIndex;
		}

		//private void dataGridView_Elements_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
		//{
		//    if (Settings.Fields.UseUdE && UdE.API.IsElementsEditorRunning)
		//    {
		//        switch (ElenetType)
		//        {
		//            case 0:
		//                UdE.API.ShowToolTipUnderCell(sender, e, 0, UdE.ItemType.Matter);
		//                break;
		//            case 1:
		//                UdE.API.ShowToolTipUnderCell(sender, e, 0, UdE.ItemType.NpcOrMobOrMine);
		//                break;
		//        }
		//    }
		//}

		private void dataGridView_Elements_MouseMove(object sender, MouseEventArgs e)
		{
			if (Scrolling)
				Scrolling = false;
		}

		private void dataGridView_Elements_Scroll(object sender, ScrollEventArgs e)
		{
			Scrolling = true;
		}

		private void checkBox_UseProb0Items_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox_UseProb0Items.Checked)
			{
				numericUpDown_Prob0Items.Enabled = true;
				checkBox_FixedProb0Items.Enabled = true;
			}
			else
			{
				numericUpDown_Prob0Items.Enabled = false;
				checkBox_FixedProb0Items.Enabled = false;
			}
		}

		private void button_Ok_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		public void SetLocalization()
		{
			//Text = GlobalProgramData.GetLocalization(6134);
			//Column_Id.HeaderText = GlobalProgramData.GetLocalization(6135);
			//Column_Name.HeaderText = GlobalProgramData.GetLocalization(6136);
			//Column_Probability.HeaderText = GlobalProgramData.GetLocalization(6137);
			//Column_Fixed.HeaderText = GlobalProgramData.GetLocalization(6138);
			//checkBox_UseProb0Items.Text = GlobalProgramData.GetLocalization(6139);
			//checkBox_FixedProb0Items.Text = GlobalProgramData.GetLocalization(6140);
			//button_Ok.Text = GlobalProgramData.GetLocalization(6001);
			//button_Cancel.Text = GlobalProgramData.GetLocalization(6002);
		}
	}
}
//(c)Rey35