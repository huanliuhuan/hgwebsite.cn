using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MYHGTOOLS.Login
{
    class CookieHandler
    {
         #region 基础方法
        /// <summary>
        /// Cookies赋值
        /// </summary>
        /// <param name="strName">主键</param>
        /// <param name="strValue">键值</param>
        /// <param name="strDay">有效天数</param>
        /// <returns></returns>
        public static bool setCookie(string strName, string strValue, int strDay, string strDomain)
        {
            try
            {
                remCookie(strName);
                HttpCookie Cookie = new HttpCookie(strName);
                Cookie.Domain = strDomain;//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                Cookie.Expires = DateTime.Now.AddDays(strDay);
                Cookie.Value = strValue;
                Cookie.Path = "/";
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Cookies赋值
        /// </summary>
        /// <param name="strName">主键</param>
        /// <param name="strValue">键值</param>
        /// <param name="strDay">有效天数</param>
        /// <returns></returns>
        public static bool setCookie(string strName, string strValue, int strDay)
        {
            try
            {
                remCookie(strName);
                HttpCookie Cookie = new HttpCookie(strName);
               // Cookie.Domain = strDomain;//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                Cookie.Expires = DateTime.Now.AddDays(strDay);
                Cookie.Value = strValue;
                Cookie.Path = "/";
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>

        public static string getCookie(string strName,string strDomain)
        {
            HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
            if (Cookie != null)
            {
                Cookie.Domain = strDomain;
                Cookie.Path = "/";
                return Cookie.Value.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>

        public static string getCookie(string strName)
        {
            HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
            if (Cookie != null)
            {
                //Cookie.Domain = strDomain;
                Cookie.Path = "/";
                return Cookie.Value.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>
        public static bool remCookie(string strName, string strDomain)
        {
            try
            {
                if (HttpContext.Current.Request.Cookies[strName] != null)
                {
                    HttpCookie Cookie = new HttpCookie(strName);
                    Cookie.Domain = strDomain;//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                    Cookie.Expires = DateTime.Now.AddDays(-1d);
                    System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                    //System.Web.HttpContext.Current.Response.Cookies["roleCheck"].Value = "";
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>
        public static bool remCookie(string strName)
        {
            try
            {
                if (HttpContext.Current.Request.Cookies[strName] != null)
                {
                    HttpCookie Cookie = new HttpCookie(strName);
                   // Cookie.Domain = strDomain;//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                    Cookie.Expires = DateTime.Now.AddDays(-1d);
                    System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                    //System.Web.HttpContext.Current.Response.Cookies["roleCheck"].Value = "";
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 多个cookie进行处理

        /// <summary>
        /// 同时绑定多个cookie
        /// </summary>
        /// <param name="strName">cookie文件的名称</param>
        /// <param name="lsKey">键</param>
        /// <param name="lsValue">值</param>
        /// <param name="strDay">过去天数</param>
        /// <param name="strDomain">cookie域</param>
        /// <returns>保存成功返回 ture</returns>
        public static bool setCookies(string strName, List<string> lsKey, List<string> lsValue, int strDay, string strDomain)
        {
            try
            {
                if(lsKey.Count!=lsValue.Count)return false;
                HttpCookie Cookies = new HttpCookie(strName);
                Cookies.Domain = strDomain;
                foreach (string item in lsKey)
                {
                    Cookies.Values.Add(item, lsValue[lsKey.IndexOf(item)]);//赋值
                }
                Cookies.Expires = DateTime.Now.AddDays(strDay);
                Cookies.Path = "/";
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookies);//添加cookie
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取多组键值对
        /// </summary>
        /// <param name="strName">cookie名称</param>
        /// <param name="strDomain">域名</param>
        /// <returns>获取多组键值对</returns>
        public static Dictionary<string,string> getCookies(string strName, string strDomain)
        {
            try
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
                Cookie.Path = "/";
                if (Cookie != null)
                {
                    foreach (string item in Cookie.Values.Keys)
                    {
                        dictionary.Add(item, Cookie.Values[item]);
                    }
                    return dictionary;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        #endregion
    }
}
