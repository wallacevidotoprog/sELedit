using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LBLIBRARY;
using System.Windows.Forms;

namespace sELedit.gShop
{
	public class Item
	{
		public int Place { get; set; }
		public int Item_category { get; set; }
		public int Item_sub_category { get; set; }
		public string Icon { get; set; }
		public int Id { get; set; }
		public int Amount { get; set; }
		public List<Sale> Sales { get; set; }
		public int Status { get; set; }
		public string Explanation { get; set; }
		public string Name { get; set; }
		public int Gift_id { get; set; }
		public int Gift_amount { get; set; }
		public int Gift_time { get; set; }
		public int ILogPrice { get; set; }
		public int[] OwnerNpcs { get; set; }
		public int Period_limit { get; set; }
		public int Avail_frequency { get; set; }
		public int Class { get; set; }
		public Item Clone()
		{
			Item it = new Item()
			{
				Place = Place,
				Item_category = Item_category,
				Item_sub_category = Item_sub_category,
				Icon = Icon,
				Id = Id,
				Amount = Amount,
				Sales = new List<Sale> { Sales[0].Clone(), Sales[1].Clone(), Sales[2].Clone(), Sales[3].Clone() },
				Status = Status,
				Explanation = Explanation,
				Name = Name,
				Gift_id = Gift_id,
				Gift_amount = Gift_amount,
				Gift_time = Gift_time,
				ILogPrice = ILogPrice,
				OwnerNpcs = OwnerNpcs,
				Period_limit = Period_limit,
				Avail_frequency = Avail_frequency,
				Class = Class
			};
			return it;
		}
	}
	public class Categories
	{
		public string Category_name { get; set; }
		public int Amount { get; set; }
		public List<string> Sub_categories { get; set; }

	}
	public class Sale
	{
		public int Price { get; set; }
		public int Selling_end_time { get; set; }
		public int During { get; set; }
		public int Selling_start_time { get; set; }
		public int Control { get; set; }
		public int Day { get; set; }
		public int Status { get; set; }
		public int Flags { get; set; }
		public int Vip_lvl { get; set; }
		public Sale Clone()
		{
			Sale sl = new Sale()
			{
				Price = Price,
				Selling_end_time = Selling_end_time,
				During = During,
				Selling_start_time = Selling_start_time,
				Control = Control,
				Day = Day,
				Status = Status,
				Flags = Flags,
				Vip_lvl = Vip_lvl
			};
			return sl;
		}
	}
	public class FileGshop
	{
		public DateTime TimeStamp;
		public int Amount;
		public List<Categories> List_categories;
		public List<Item> List_items;
		public int Max_place = 0;
		public int Version;
		BinaryReader br;
		public void ReadFile(String Filepath, int Vers)
		{

			Version = Vers;
		Mark:
			br = new BinaryReader(File.Open(Filepath, FileMode.Open, FileAccess.Read));
			try
			{
				if (Version > 7)
				{
					List_categories = null;
					List_items = null;
					br.Close();
					return;
				}
				TimeStamp = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(br.ReadInt32());
				Amount = br.ReadInt32();
				List_items = new List<Item>();
				List_categories = new List<Categories>();
				for (int i = 0; i < Amount; i++)
				{
					Item it = new Item()
					{
						Place = br.ReadInt32()
					};
					Max_place = i;
					it.Item_category = br.ReadInt32();
					it.Item_sub_category = br.ReadInt32();
					it.Icon = Encoding.GetEncoding(936).GetString(br.ReadBytes(128)).Split('\0')[0];
					it.Id = br.ReadInt32();
					it.Amount = br.ReadInt32();
					it.Sales = new List<Sale>(4);
					for (int g = 0; g < 4; g++)
					{
						Sale sl = new Sale()
						{
							Price = br.ReadInt32(),
							Selling_end_time = br.ReadInt32(),
							During = br.ReadInt32(),
						};
						if (Version >= 1)
						{
							sl.Selling_start_time = br.ReadInt32();
							sl.Control = br.ReadInt32();
							sl.Day = br.ReadInt32();
							sl.Status = br.ReadInt32();
							sl.Flags = br.ReadInt32();
							if (Version >= 5)
							{
								sl.Vip_lvl = br.ReadInt32();
							}
						}
						else
						{
							if (g == 0)
							{
								sl.Control = -1;
							}
							else
							{
								sl.Control = 0;
							}
						}
						it.Sales.Add(sl);
					}
					if (Version == 0)
					{
						it.Status = br.ReadInt32();
					}
					it.Explanation = Encoding.Unicode.GetString(br.ReadBytes(1024)).TrimEnd('\0');
					it.Name = Encoding.Unicode.GetString(br.ReadBytes(64)).Split('\0')[0];
					if (Version >= 2)
					{
						it.Gift_id = br.ReadInt32();
						it.Gift_amount = br.ReadInt32();
						it.Gift_time = br.ReadInt32();
					}
					if (Version >= 3)
					{
						it.ILogPrice = br.ReadInt32();
					}
					#region OwnerNpcs
					it.OwnerNpcs = new int[8];
					if (Version >= 4)
					{
						for (int g = 0; g < 8; g++)
						{
							it.OwnerNpcs[g] = br.ReadInt32();
						}
					}
					#endregion
					if (Version >= 5)
					{
						it.Period_limit = br.ReadInt32();
						it.Avail_frequency = br.ReadInt32();
					}
					if (Version >= 6)
					{
						it.Class = br.ReadInt32();
					}
					if (it.Item_category > 7 || it.Item_sub_category > 8 || it.Place < 0)
					{
						throw new Exception();
					}
					it.Place = i;
					List_items.Add(it);
				}
				for (int j = 0; j < Amount; j++)
				{
					List_items[j].Place = j;
				}
				for (int b = 0; b < 8; b++)
				{
					ReadCategory();
				}
				if (br.BaseStream.Position != br.BaseStream.Length)
				{
					ReadCategory();
				}
			}
			catch
			{
				br.Close();
				Version = Version + 1;
				goto Mark;
			}
			br.Close();
		}
		public void ReadCategory()
		{
			Categories Ct = new Categories();
			Ct.Category_name = Encoding.Unicode.GetString(br.ReadBytes(128)).TrimEnd('\0').Split(new string[] { "\0" }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(); if (Ct.Category_name == null) Ct.Category_name = "";
			Ct.Amount = br.ReadInt32();
			if (Ct.Amount > 9)
				throw new Exception();
			Ct.Sub_categories = new List<string>(Ct.Amount);
			for (byte s = 0; s < Ct.Amount; s++)
			{
				Ct.Sub_categories.Add(Encoding.Unicode.GetString(br.ReadBytes(128)).Split(new string[] { "\0" }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault());
			}
			List_categories.Add(Ct);
		}
		public void WriteFile(BinaryWriter bw, int Vers)
		{
			bw.Write((Int32)(TimeStamp.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
			bw.Write(Amount);
			for (int i = 0; i < Amount; i++)
			{
				bw.Write(List_items[i].Place);
				bw.Write(List_items[i].Item_category);
				bw.Write(List_items[i].Item_sub_category);
				bw.Write(GetBytes(List_items[i].Icon, 128, Encoding.GetEncoding(936)));
				bw.Write(List_items[i].Id);
				bw.Write(List_items[i].Amount);
				for (int f = 0; f < 4; f++)
				{
					bw.Write(List_items[i].Sales[f].Price);
					bw.Write(List_items[i].Sales[f].Selling_end_time);
					bw.Write(List_items[i].Sales[f].During);
					if (Vers >= 1)
					{
						bw.Write(List_items[i].Sales[f].Selling_start_time);
						bw.Write(List_items[i].Sales[f].Control);
						bw.Write(List_items[i].Sales[f].Day);
						bw.Write(List_items[i].Sales[f].Status);
						bw.Write(List_items[i].Sales[f].Flags);
						if (Vers >= 5)
							bw.Write(List_items[i].Sales[f].Vip_lvl);
					}
				}
				if (Vers == 0)
					bw.Write(List_items[i].Status);
				bw.Write(GetBytes(List_items[i].Explanation, 1024, Encoding.Unicode));
				bw.Write(GetBytes(List_items[i].Name, 64, Encoding.Unicode));
				if (Vers >= 2)
				{
					bw.Write(List_items[i].Gift_id);
					bw.Write(List_items[i].Gift_amount);
					bw.Write(List_items[i].Gift_time);
				}
				if (Vers >= 3)
					bw.Write(List_items[i].ILogPrice);
				if (Vers >= 4)
					for (int f = 0; f < 8; f++)
					{
						bw.Write(List_items[i].OwnerNpcs[f]);
					}
				if (Vers >= 5)
				{
					bw.Write(List_items[i].Period_limit);
					bw.Write(List_items[i].Avail_frequency);
				}
				if (Vers >= 6)
					bw.Write(List_items[i].Class);
			}
			for (int d = 0; d < 8; d++)
			{
				bw.Write(GetBytes(List_categories[d].Category_name, 128, Encoding.Unicode));
				bw.Write(List_categories[d].Amount);
				for (int s = 0; s < List_categories[d].Amount; s++)
				{
					bw.Write(List_categories[d].Sub_categories[s].GetBytesFromString(128, Encoding.Unicode));
				}
			}
		}
		public void WriteSev(BinaryWriter bw, int Vers)
		{
			bw.Write((Int32)(TimeStamp.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
			bw.Write(Amount);
			for (int i = 0; i < Amount; i++)
			{
				bw.Write(List_items[i].Place);
				bw.Write(List_items[i].Item_category);
				bw.Write(List_items[i].Item_sub_category);
				bw.Write(List_items[i].Id);
				bw.Write(List_items[i].Amount);
				for (int f = 0; f < 4; f++)
				{
					bw.Write(List_items[i].Sales[f].Price);
					bw.Write(List_items[i].Sales[f].Selling_end_time);
					bw.Write(List_items[i].Sales[f].During);
					if (Vers >= 1)
					{
						bw.Write(List_items[i].Sales[f].Selling_start_time);
						bw.Write(List_items[i].Sales[f].Control);
						bw.Write(List_items[i].Sales[f].Day);
						bw.Write(List_items[i].Sales[f].Status);
						bw.Write(List_items[i].Sales[f].Flags);
						if (Vers >= 5)
							bw.Write(List_items[i].Sales[f].Vip_lvl);
					}
				}
				if (Vers == 0)
					bw.Write(List_items[i].Status);
				if (Vers >= 2)
				{
					bw.Write(List_items[i].Gift_id);
					bw.Write(List_items[i].Gift_amount);
					bw.Write(List_items[i].Gift_time);
				}
				if (Vers >= 3)
					bw.Write(List_items[i].ILogPrice);
				if (Vers >= 4)
					for (int f = 0; f < 8; f++)
					{
						bw.Write(List_items[i].OwnerNpcs[f]);
					}
				if (Vers >= 5)
				{
					bw.Write(List_items[i].Period_limit);
					bw.Write(List_items[i].Avail_frequency);
				}
				if (Vers >= 6)
					bw.Write(List_items[i].Class);
			}
		}
		public byte[] GetBytes(string str, int l, Encoding e)
		{
			str = str.Split('\0')[0];
			byte[] data = new byte[l];
			if (e.GetByteCount(str) > l)
			{
				Array.Copy(e.GetBytes(str), 0, data, 0, l);
			}
			else
			{
				Array.Copy(e.GetBytes(str), data, e.GetByteCount(str));
			}
			return data;
		}
	}

	public static class SalveShops
	{
		public static void SaveGshopData(FileGshop Gshop1, string FileSave)
		{            
            int Vrs = Gshop1.Version;
            if (Gshop1 != null)
            {
                if (Gshop1.List_categories != null && Gshop1.List_items != null)
                {
                    if (FileSave != null)
                    {
                        if (File.Exists(FileSave))
                        {
                            File.Delete(FileSave);
                        }
                        BinaryWriter bw = new BinaryWriter(File.Create(FileSave));
                        Gshop1.WriteFile(bw, Vrs);
                        bw.Close();                       
                        //MessageBox.Show("File has been saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                }
                else
                {
                    
                        MessageBox.Show("Loaded file has got wrong format!!!", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                
                    MessageBox.Show("Nothing to save!!!", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SaveGshopSevData(FileGshop Gshop1, string FileSave)
        {
            int  Verssev = Gshop1.Version;
            if (Verssev != 0)
            {

                if (FileSave != null)
                {
                    if (File.Exists(FileSave.Replace("gshop", "gshopsev")))
                    {
                        File.Delete(FileSave.Replace("gshop", "gshopsev"));
                    }


                    BinaryWriter bw = new BinaryWriter(File.Create(FileSave.Replace("gshop", "gshopsev")));
                    Gshop1.WriteSev(bw, Verssev);
                    bw.Close();
                    
                       // MessageBox.Show("gshopsev.data has been successfully saved.!!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                
                    MessageBox.Show("Use gshop.data for 1.3.6-1.4.2 versions", "Saving information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}