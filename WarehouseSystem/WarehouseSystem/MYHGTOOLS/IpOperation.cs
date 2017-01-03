using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace MYHGTOOLS
{
    public class IpOperation
    {
        /// <summary>
        /// 获取公网IP
        /// </summary>
        /// <returns></returns>
        public static String GetPublicIP()
        {
            //读取网站的数据
            Uri uri = new Uri("http://iframe.ip138.com/city.asp");
            WebRequest wr = WebRequest.Create(uri);
            Stream s = wr.GetResponse().GetResponseStream();
            StreamReader sr = new StreamReader(s, System.Text.Encoding.Default);
            string all = sr.ReadToEnd();
            //all = "您的IP是：[123.120.81.183] 来自：北京市 联通";
            //找出ip
            int i = all.IndexOf("[") + 1;
            int j = all.IndexOf("]");
            string tempip = all.Substring(i, j - i);
            string ip = tempip.Replace(" ", "");

            sr.Close();
            s.Close();
            return ip;
        }

        /// <summary>
        /// 获取公网ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetInternetIp()
        {
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.kuainiu.com/tools/tool.php?t=ip");
                myHttpWebRequest.Referer = "";
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Stream receiveStream = myHttpWebResponse.GetResponseStream();
                Encoding encode = System.Text.Encoding.Default;
                StreamReader readStream = new StreamReader(receiveStream, encode);
                Char[] read = new Char[1024];
                int count = readStream.Read(read, 0, 1024);
                StringBuilder sb = new StringBuilder();
                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    sb.Append(str);
                    count = readStream.Read(read, 0, 1024);
                }
                myHttpWebResponse.Close();
                readStream.Close();
                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetSingelIP()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result))
            {
                return "127.0.0.1";
            }
            return result;
        }

        /// <summary>
        /// 通过ip获取城市
        /// </summary>
        /// <returns></returns>
        public static string GetPubLicCity(string ip)
        {
            try
            {
                string ipInfo = GetStrByUrl("http://whois.pconline.com.cn/ip.jsp?ip=" + ip, Encoding.Default);
                ipInfo = ipInfo.Replace(" ", "");
                return ipInfo;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        ///通过URL下载网页HTML,返回HTML代码
        /// </summary>
        /// <param name="url">要下载的网页的网址</param>
        /// <param name="encoding">要下载的网页的编码</param>
        /// <returns>网页内容</returns>
        public static string GetStrByUrl(string url, Encoding encoding)
        {
            try
            {
                string source = "";
                WebClient client = new WebClient();
                Stream sr = client.OpenRead(url);
                if (sr != null)
                {
                    StreamReader streamr = new StreamReader(sr, encoding);
                    source = streamr.ReadToEnd();
                    sr.Close();
                    streamr.Close();
                }
                return source.Replace("\n", "").Replace("\t", "").Replace("\r", "");
            }
            catch (System.InvalidOperationException ex)
            {
                return "";
                //throw ex;
            }
        }

        /// <summary>
        /// 获取浏览器版本
        /// </summary>
        /// <returns></returns>
        public string GetBrowerVersion()
        {
            return "浏览器：" + HttpContext.Current.Request.Browser.Browser + " 内核：" + HttpContext.Current.Request.Browser.Type;
        }
    }
}
