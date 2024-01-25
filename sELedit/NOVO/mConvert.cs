using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace sELedit.NOVO
{
    class mConvert
    {
        public static string SecondsToString(int time, int Type = 0)
        {
            if (Type == 1 && (uint)time > 0x80000000)
            {
                int tm_year = Convert.ToInt32(((time & 0x7FFFFFFFu) >> 24) + 100);
                int tm_mon = ((time & 0xFF0000) >> 16) - 1;
                int tm_mday = (time & 0xFF00) >> 8;
                int tm_hour = (byte)time;
                DateTime origin = new DateTime(tm_year + 1900, tm_mon + 1, tm_mday, tm_hour, 0, 0, 0);
                return origin.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                uint utime = (uint)time;
                uint days = utime / 86400;
                utime = utime - (days * 86400);
                uint hours = utime / 3600;
                utime = utime - (hours * 3600);
                uint minutes = utime / 60;
                uint seconds = utime - (minutes * 60);
                return (days.ToString("D2") + "-" + hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2"));
            }
        }
        public static int StringToSeconds(string time)
        {
            if (!UInt32.TryParse(time, out uint value))
            {
                if (DateTime.TryParse(time, out DateTime date))
                {
                    return BitConverter.ToInt32(new byte[] { Convert.ToByte(date.Hour), Convert.ToByte(date.Day), Convert.ToByte(date.Month), Convert.ToByte(date.Year - 1872) }, 0);
                }
                else
                {
                    char[] chArray = new char[]
                    {
                        '-', ':'
                    };
                    string[] times = time.Split(chArray);
                    return (int)((86400 * Convert.ToUInt32(times[0])) + (3600 * Convert.ToUInt32(times[1])) + (60 * Convert.ToUInt32(times[2])) + Convert.ToUInt32(times[3]));
                }
            }
            else
                return (int)Convert.ToUInt32(time);
        }

        public static string TimestampToString(uint timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            origin = origin.AddSeconds(timestamp);
            return origin.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ServerXToClientX(float x)
        {
            double cx = 400 + Math.Truncate(x * 0.1);
            return cx.ToString();
        }
        public static string ServerYToClientY(float y)
        {
            double cy = Math.Truncate(y * 0.1);
            return cy.ToString();
        }
        public static string ServerZToClientZ(float z)
        {
            double cz = 550 + Math.Truncate(z * 0.1);
            return cz.ToString();
        }

        //public static string Int32ToString(int value)
        //{
        //    return value.ToString(GlobalProgramData.ValueFormat, CultureInfo.CreateSpecificCulture("ru-RU"));
        //}
        //public static string UInt32ToString(int value)
        //{
        //    return ((uint)value).ToString(GlobalProgramData.ValueFormat, CultureInfo.CreateSpecificCulture("ru-RU"));
        //}

        public static int DigitNumberToInt32(object value)
        {
            string result = Convert.ToString(value).Replace("" + (char)160, "").Replace("" + (char)32, "");
            return Convert.ToInt32(result);
        }
        public static int DigitNumberToUInt32(object value)
        {
            string result = Convert.ToString(value).Replace("" + (char)160, "").Replace("" + (char)32, "");
            return (int)Convert.ToUInt32(result);
        }

        //public static string Int32ToSilver(int value)
        //{
        //    string result = "";
        //    if (Settings.Fields.EnableShowDigits)
        //    {
        //        if (value < 100)
        //            result = value.ToString();
        //        else
        //        {
        //            string s = value.ToString();
        //            string tmp1 = s.Substring(0, s.Length - 2);
        //            string tmp2 = s.Substring(s.Length - 2, 2);
        //            result = tmp1 + " " + tmp2;
        //        }
        //    }
        //    else
        //        result = value.ToString();
        //    return result;
        //}
        //public static string UInt32ToSilver(int value)
        //{
        //    string result = "";
        //    if (Settings.Fields.EnableShowDigits)
        //    {
        //        if ((uint)value < 100)
        //            result = ((uint)value).ToString();
        //        else
        //        {
        //            string s = ((uint)value).ToString();
        //            string tmp1 = s.Substring(0, s.Length - 2);
        //            string tmp2 = s.Substring(s.Length - 2, 2);
        //            result = tmp1 + " " + tmp2;
        //        }
        //    }
        //    else
        //        result = ((uint)value).ToString();
        //    return result;
        //}

        public static float PercentNumberToSingle(object value, bool EnableShowPercents)
        {
            if (EnableShowPercents)
            {
                float result = Convert.ToSingle(Convert.ToString(value).Replace("%", ""));
                return Convert.ToSingle(result * 0.01);
            }
            else
            {
                float result = Convert.ToSingle(value);
                return result;
            }
        }

        //public static string ItemPropsSecondsToString(uint time)
        //{
        //    string result = "";
        //    uint time1 = time;
        //    uint days = time / 86400;
        //    time = time - (days * 86400);
        //    uint hours = time / 3600;
        //    time = time - (hours * 3600);
        //    uint minutes = time / 60;
        //    uint seconds = time - (minutes * 60);
        //    if (time1 == 60) seconds = 60;
        //    if (time1 == 3600) minutes = 60;
        //    if (time1 == 86400) hours = 24;
        //    if (time1 <= 60) result = seconds.ToString() + GlobalProgramData.GetLocalization(7091);
        //    if (time1 > 60 && time1 <= 3600) result = minutes.ToString() + GlobalProgramData.GetLocalization(7092) + " " + seconds.ToString() + GlobalProgramData.GetLocalization(7091);
        //    if (time1 > 3600 && time1 <= 86400) result = hours.ToString() + GlobalProgramData.GetLocalization(7093) + " " + minutes.ToString() + GlobalProgramData.GetLocalization(7092);
        //    if (time1 > 86400) result = days.ToString() + GlobalProgramData.GetLocalization(7094) + " " + hours.ToString() + GlobalProgramData.GetLocalization(7093);
        //    return result;
        //}

        //public static string ItemPropsSecondsToString2(uint time)
        //{
        //    string result = "";
        //    uint time1 = time;
        //    uint days = time / 86400;
        //    time = time - (days * 86400);
        //    uint hours = time / 3600;
        //    time = time - (hours * 3600);
        //    uint minutes = time / 60;
        //    uint seconds = time - (minutes * 60);
        //    if (time1 == 60) seconds = 60;
        //    if (time1 == 3600) minutes = 60;
        //    if (time1 == 86400) hours = 24;
        //    if (time1 <= 60) result = seconds.ToString() + GlobalProgramData.GetLocalization(7114);
        //    if (time1 > 60 && time1 <= 3600) result = minutes.ToString() + GlobalProgramData.GetLocalization(7115) + " " + seconds.ToString() + GlobalProgramData.GetLocalization(7114);
        //    if (time1 > 3600 && time1 <= 86400) result = hours.ToString() + GlobalProgramData.GetLocalization(7116) + " " + minutes.ToString() + GlobalProgramData.GetLocalization(7115);
        //    if (time1 > 86400) result = days.ToString() + GlobalProgramData.GetLocalization(7117) + " " + hours.ToString() + GlobalProgramData.GetLocalization(7116);
        //    return result;
        //}

        public static string ColorToString(Color color)
        {
            return String.Format("^{0:x2}{1:x2}{2:x2}", color.R, color.G, color.B).ToUpper();
        }
        public static Color StringToColor(string String, int StartIndex)
        {
            Color result = Color.FromArgb(1, 0, 0, 0);
            if (String.Length >= 6 + StartIndex && Int32.TryParse("FF" + String.Substring(StartIndex, 6), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int color_int))
                return Color.FromArgb(color_int);
            else
                return result;
        }

        //public static string BoolToString(bool value)
        //{
        //    if (value)
        //        return GlobalProgramData.GetLocalization(2311);
        //    else
        //        return GlobalProgramData.GetLocalization(2310);
        //}

        public static string StringToRTF(string String, Color DefaultColor, Font Font)
        {
            String = String.Replace(@"\", @"\\");
            String = String.Replace(@"{", @"\{");
            String = String.Replace(@"}", @"\}");
            string RTFText_string = "";
            string RTFText_colortbl = "{\\colortbl ;";
            List<Color> ColorTable = new List<Color>();
            ColorTable.Add(DefaultColor);
            RTFText_colortbl += "\\red" + ColorTable[0].R + "\\green" + ColorTable[0].G + "\\blue" + ColorTable[0].B + ";";
            Color CurColor = DefaultColor;
            bool AddColor = true;
            string[] blocks = (ColorToString(DefaultColor) + String).Split(new char[] { '^' });
            for (int i = 1; i < blocks.Length; i++)
            {
                if (blocks[i] != "")
                {
                    try
                    {
                        Color color = StringToColor(blocks[i], 0);
                        if (color.A != 1)
                        {
                            if (color.Name == "ffffff")
                                CurColor = DefaultColor;
                            else
                                CurColor = color;
                            AddColor = true;
                            for (int i1 = 0; i1 < ColorTable.Count; i1++)
                            {
                                if (ColorTable[i1] == CurColor)
                                {
                                    RTFText_string += ConvertStringRTF("\\cf" + (i1 + 1) + " " + blocks[i].Substring(6));
                                    AddColor = false;
                                    break;
                                }
                            }
                            if (AddColor)
                            {
                                ColorTable.Add(CurColor);
                                RTFText_colortbl += "\\red" + ColorTable[ColorTable.Count - 1].R + "\\green" + ColorTable[ColorTable.Count - 1].G + "\\blue" + ColorTable[ColorTable.Count - 1].B + ";";
                                RTFText_string += ConvertStringRTF("\\cf" + ColorTable.Count + " " + blocks[i].Substring(6));
                            }
                        }
                        else
                            RTFText_string += ConvertStringRTF("^" + blocks[i]);
                    }
                    catch
                    {
                        RTFText_string += ConvertStringRTF("^" + blocks[i]);
                    }
                }
            }
            RTFText_colortbl += "}\r\n";
            string result = "{\\rtf1\\ansi\\ansicpg936\\deff0\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fnil\\fcharset0 " + Font.Name + ";}}\r\n" + RTFText_colortbl + "\\viewkind4\\uc1\\pard\\lang2052";
            if (Font.Bold)
                result += "\\b";
            result += "\\f0\\fs" + Math.Ceiling(Font.Size * 2) + " " + RTFText_string;
            return result;
        }

        private static string ConvertStringRTF(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char character in input.Replace("\r\n", "\\par\r\n"))
            {
                if (character <= 0x7f)
                    sb.Append(character);
                else
                    sb.Append("\\u" + Convert.ToUInt32(character) + "?");
            }
            return sb.ToString();
        }
    }
}