using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;

namespace MYHGTOOLS
{
    /// <summary>
    /// 字符串操作
    /// </summary>
    public static class StringOperation
    {
       

        #region string类型转换
        /// <summary>
        /// 去掉字符串的最后一位
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DelLastOne(string value)
        {
            string str = "";
            if (value.Length > 0)
            {
                str = value.Substring(0, value.Length - 1);
            }
            else
            {
                str = value;
            }
            return str;
        }

        /// <summary>
        /// 以‘,’分割字符串转成int数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int[] ToIntArray(this string value)
        {
            string[] ids = value.Split(',');
            int[] ids2 = Array.ConvertAll(ids, id => Convert.ToInt32(id));
            return ids2;
        }

        /// <summary>
        /// 把数组转化成字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string arrayToString(string[] str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (i == str.Length - 1)
                {
                    sb.Append(str[i]);
                }
                else
                {
                    sb.Append(str[i]);
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// String转Int类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch { return 0; }
        }
        /// <summary>
        /// String转Decimal类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch { return 0; }
        }
        /// <summary>
        /// String转DateTime类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch { return DateTime.Now; }
        }
        #endregion

        #region 去除HTML标记
        ///<summary>   
        ///去除HTML标记   
        ///</summary>   
        ///<param name="NoHTML">包括HTML的源码</param>   
        ///<returns>已经去除后的文字</returns>   
        public static string NoHTML(string Htmlstring)
        {
            if (Htmlstring != null)
            {
                //Regex myReg = new Regex(@"(\<.[^\<]*\>)", RegexOptions.IgnoreCase);
                //Htmlstring = myReg.Replace(Htmlstring, "");
                //myReg = new Regex(@"(\<\/[^\<]*\>)", RegexOptions.IgnoreCase);
                //Htmlstring = myReg.Replace(Htmlstring, "");
                //return Htmlstring;

                //删除脚本
                Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                //删除HTML
                Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

                Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&ldquo;", "“", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&rdquo;", "”", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

                Htmlstring = Htmlstring.Replace("<", "&lt;");
                Htmlstring = Htmlstring.Replace(">", "&gt;");
            }
            else
            {
                Htmlstring = "";
            }
            return Htmlstring;
        }
        #endregion

        /// <summary>
        /// 替换html中的特殊字符
        /// </summary>
        /// <param name="theString">需要进行替换的文本。</param>
        /// <returns>替换完的文本。</returns>
        public static string HtmlEncode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace("  ", " &nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("'", "&#39;");
            theString = theString.Replace("\r\n", "<br/> ");
            return theString;
        }

        /// <summary>
        /// 恢复html中的特殊字符
        /// </summary>
        /// <param name="theString">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public static string HtmlDecode(string theString)
        {
            if (theString == "" || theString == null)
                return "";
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace(" &nbsp;", "  ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "'");
            theString = theString.Replace("<br/> ", "\r\n");
            return theString;
        }

        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="value"></param>
        /// <param name="code">加密后截取长度16或者32</param>
        /// <returns></returns>
        public static string MD5(this string value, int code)
        {
            try
            {
                if (code == 16)
                {
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(value, "MD5").ToLower().Substring(8, 16);
                }

                if (code == 32)
                {
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(value, "MD5");
                }
            }
            catch
            {

            }
            return "00000000000000000000000000000000";
        }
        /// <summary>
        /// MD5加密 32位
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MD5(this string value)
        {
            return MD5(value, 32);
        }
        #endregion

        #region 获取指定长度的字符串
        public static string CutString(string source, int len, bool isAddDot)
        {
            if (source.Length <= len)
            {
                return source;
            }
            string strTemp = "";
            int intTemp = 0;
            int intPos = 0;
            while ((intTemp < len * 2) && (intPos < source.Length))
            {
                if (((int)source[intPos]) < 128)
                {
                    //单字节
                    strTemp += source[intPos].ToString();
                    intTemp++;
                }
                else if (intTemp == (len * 2 - 1))
                {
                    //双字节
                    break;
                }
                else
                {
                    strTemp += source[intPos].ToString();
                    intTemp += 2;
                }

                intPos++;
            }
            if (isAddDot)
            {
                strTemp += "...";
            }
            return strTemp;
        }
        #endregion

        /// <summary>
        /// *字符替换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MobileReplaceX(this string str)
        {
            return str.ReplaceX(3, "****");
        }
        /// <summary>
        /// *字符替换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pre"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        public static string EmailReplaceX(this string str)
        {
            string q = str;//@前部分
            string h = "";//@后部分 
            if (str.Contains("@"))
            {
                q = str.Substring(0, str.LastIndexOf('@'));
                h = str.Substring(str.LastIndexOf('@'));
            }
            return q.ReplaceX(3, "****") + h;
        }
        /// <summary>
        /// 替换为*
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Pre"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceX(this string str, int Pre, string replace)
        {
            string q = "";//前部分
            string h = "";//后部分  
            if (str.Length > Pre)
            {
                q = str.Substring(0, Pre);
            }
            if (str.Length - 1 > Pre)
            {
                h = str.Substring(str.Length - 1);
            }
            return q + replace + h; ;
        }

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string SetNumber(string formcode)
        {
            formcode += DateTime.Now.Year.ToString();
            formcode += DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            formcode += DateTime.Now.Day.ToString().Length == 1 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
            formcode += DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            formcode += DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
            formcode += DateTime.Now.Second.ToString().Length == 1 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString();
            if (DateTime.Now.Millisecond.ToString().Length == 1)
            {
                formcode += "00" + DateTime.Now.Millisecond.ToString();
            }
            else if (DateTime.Now.Millisecond.ToString().Length == 2)
            {
                formcode += "0" + DateTime.Now.Millisecond.ToString();
            }
            else
            {
                formcode += DateTime.Now.Millisecond.ToString();
            }
            return formcode;
        }
        /// <summary>
        /// DEC 加密过程
        /// </summary>
        /// <param name="pToEncrypt">被加密的字符串</param>
        /// <param name="sKey">密钥（只支持8个字节的密钥）</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string pToEncrypt, string sKey)
        {
            //访问数据加密标准(DES)算法的加密服务提供程序 (CSP) 版本的包装对象
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);　//建立加密对象的密钥和偏移量
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);　 //原文使用ASCIIEncoding.ASCII方法的GetBytes方法

            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);//把字符串放到byte数组中

            MemoryStream ms = new MemoryStream();//创建其支持存储区为内存的流　
            //定义将数据流链接到加密转换的流
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //上面已经完成了把加密后的结果放到内存中去

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// DEC 解密过程
        /// </summary>
        /// <param name="pToDecrypt">被解密的字符串</param>
        /// <param name="sKey">密钥（只支持8个字节的密钥，同前面的加密密钥相同）</param>
        /// <returns>返回被解密的字符串</returns>
        public static string Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);　//建立加密对象的密钥和偏移量，此值重要，不能修改
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //建立StringBuild对象，createDecrypt使用的是流对象，必须把解密后的文本变成流对象
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
    }
}
