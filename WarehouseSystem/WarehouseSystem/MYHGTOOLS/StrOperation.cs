using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Web;

namespace MYHGTOOLS
{
    /// <summary>
    /// 创建者：刘欢
    /// 创建日期：2014年10月14日 16:15:18
    /// 说明：关于字符串的一些操作
    /// </summary>
    public class StrOperation
    {
        /// <summary>
        /// 截取字符串优化版
        /// </summary>
        /// <param name="stringToSub">所要截取的字符</param>
        /// <param name="length">长度</param>
        /// <returns>截取后的字符</returns>
        public static StringBuilder GetFirstString(string stringToSub, int length)
        {
            try
            {
                Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
                char[] stringChar = stringToSub.ToCharArray();
                StringBuilder sbu = new StringBuilder();
                int nlength = 0;
                //字符是否足够截取
                bool isCut = false;
                for (int i = 0; i < stringChar.Length; i++)
                {
                    if (regex.IsMatch((stringChar[i]).ToString()))
                    {
                        sbu.Append(stringChar[i]);
                        nlength += 2;
                    }
                    else
                    {
                        sbu.Append(stringChar[i]);
                        nlength = nlength + 1;
                    }
                    if (nlength > length)
                    {
                        isCut = true;
                        break;
                    }
                }
                if (isCut)
                {
                    sbu.Append("...");
                    return sbu;
                }
                else
                {
                    return sbu;
                }
            }
            catch (Exception ex)
            {
                return Common.ReturnException(ex);
            }
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        private string GenerateCheckCode()
        {
            #region
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }

            HttpContext.Current.Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));

            return checkCode;
            #endregion
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        public String CreateCheckCodeImage()
        {
            #region
            string checkCode = GenerateCheckCode();
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return null;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(Color.White);

                //画图片的背景噪音线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 150; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Gif";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
            return checkCode;
            #endregion
        }

        private static char[] constant =   
          {   
            '2','3','4','5','6','7','8','9',   
            'a','b','c','d','e','f','g','h','j','k','m','n','p','q','r','s','t','u','v','w','x','y','z',   
            'A','B','C','D','E','F','G','H','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'   
          };

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="Length">长度</param>
        /// <returns>随机数</returns>
        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(56);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(56)]);
            }
            return newRandom.ToString();
        }

        /// <summary>
        /// 获取汉字第一个拼音
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string getSpells(string input)
        {
            #region
            int len = input.Length;
            string reVal = "";
            for (int i = 0; i < len; i++)
            {
                reVal += getSpell(input.Substring(i, 1));
            }
            return reVal;
            #endregion
        }

        static private string getSpell(string cn)
        {
            #region
            byte[] arrCN = Encoding.Default.GetBytes(cn);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "?";
            }
            else return cn;
            #endregion
        }

        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="QJstr"></param>
        /// <returns></returns>
        static public string GetBanJiao(string QJstr)
        {
            #region
            char[] c = QJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            string strNew = new string(c);
            return strNew;
            #endregion
        }

        /// <summary>
        /// 对传递的参数字符串进行处理，防止注入式攻击
        /// </summary>
        /// <param name="str">传递的参数字符串</param>
        /// <returns>String</returns>
        public static string ConvertSql(string str)
        {
            #region
            str = str.Trim();
            str = str.Replace("'", "");
            str = str.Replace(";--", "");
            str = str.Replace(" or ", "");
            str = str.Replace(" and ", "");

            return str;
            #endregion
        }

        /// <summary>
        /// 将指定字符串中的汉字转换为拼音首字母的缩写，其中非汉字保留为原字符
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertSpellFirst(string text)
        {
            #region
            char pinyin;
            byte[] array;
            StringBuilder sb = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                pinyin = c;
                array = Encoding.Default.GetBytes(new char[] { c });

                if (array.Length == 2)
                {
                    int i = array[0] * 0x100 + array[1];

                    #region 条件匹配
                    if (i < 0xB0A1) pinyin = c;
                    else
                        if (i < 0xB0C5) pinyin = 'a';
                        else
                            if (i < 0xB2C1) pinyin = 'b';
                            else
                                if (i < 0xB4EE) pinyin = 'c';
                                else
                                    if (i < 0xB6EA) pinyin = 'd';
                                    else
                                        if (i < 0xB7A2) pinyin = 'e';
                                        else
                                            if (i < 0xB8C1) pinyin = 'f';
                                            else
                                                if (i < 0xB9FE) pinyin = 'g';
                                                else
                                                    if (i < 0xBBF7) pinyin = 'h';
                                                    else
                                                        if (i < 0xBFA6) pinyin = 'g';
                                                        else
                                                            if (i < 0xC0AC) pinyin = 'k';
                                                            else
                                                                if (i < 0xC2E8) pinyin = 'l';
                                                                else
                                                                    if (i < 0xC4C3) pinyin = 'm';
                                                                    else
                                                                        if (i < 0xC5B6) pinyin = 'n';
                                                                        else
                                                                            if (i < 0xC5BE) pinyin = 'o';
                                                                            else
                                                                                if (i < 0xC6DA) pinyin = 'p';
                                                                                else
                                                                                    if (i < 0xC8BB) pinyin = 'q';
                                                                                    else
                                                                                        if (i < 0xC8F6) pinyin = 'r';
                                                                                        else
                                                                                            if (i < 0xCBFA) pinyin = 's';
                                                                                            else
                                                                                                if (i < 0xCDDA) pinyin = 't';
                                                                                                else
                                                                                                    if (i < 0xCEF4) pinyin = 'w';
                                                                                                    else
                                                                                                        if (i < 0xD1B9) pinyin = 'x';
                                                                                                        else
                                                                                                            if (i < 0xD4D1) pinyin = 'y';
                                                                                                            else
                                                                                                                if (i < 0xD7FA) pinyin = 'z';
                    #endregion
                }

                sb.Append(pinyin);
            }

            return sb.ToString();
            #endregion
        }

        #region 获取指定时间和现在的间隔时间
        /// <summary>
        /// 获取指定时间和现在的间隔时间
        /// </summary>
        /// <param name="postTime"></param>
        /// <returns></returns>
        public static string GetPassTime(DateTime postTime)
        {
            TimeSpan ts = DateTime.Now.Subtract(postTime);
            string returnVal = string.Empty;
            if (ts.TotalSeconds < 60)
            {
                returnVal = string.Format("{0}秒前", (int)ts.TotalSeconds);
            }
            else if (ts.TotalMinutes < 60)
            {
                returnVal = string.Format("{0}分前", (int)ts.TotalMinutes);
            }
            else if (ts.TotalHours < 24)
            {
                returnVal = string.Format("{0}小时前", (int)ts.TotalHours);
            }
            else if ((int)ts.TotalDays == 1)
            {
                returnVal = "昨天";
            }
            else if ((int)ts.TotalDays == 2)
            {
                returnVal = "前天";
            }
            else if (ts.TotalDays < 7)
            {
                returnVal = string.Format("{0}前天", (int)ts.TotalDays);
            }
            else
            {
                returnVal = postTime.ToString("yyyy-MM-dd");
            }

            return returnVal;
        }
        #endregion

    }
}
