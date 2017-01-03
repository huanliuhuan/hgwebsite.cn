using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MYHGTOOLS
{
    /// <summary>
    /// 创建人：刘欢
    /// 创建日期：2014-10-13
    /// 说明：关于cookie的操作
    /// </summary>
    public class CookieOperation
    {
        /// <summary>
        /// 创建cookie
        /// </summary>
        /// <param name="cookeName">cookie名称</param>
        /// <param name="cookieValue">cookie值</param>
        /// <param name="cookieTime">cookie存取时间</param>
        /// <param name="type">判断分钟数或天数0为分钟1为天</param>
        /// <returns></returns>
        public static StringBuilder CreateCookie(string cookeName, string cookieValue, int cookieTime,int type)
        {
            try
            {
                StringBuilder sbu = new StringBuilder();
                HttpCookie cookie = new HttpCookie(cookeName);
                if (type == 0)
                {
                    cookie.Expires = DateTime.Now.AddMinutes(cookieTime);
                }
                else
                {
                    cookie.Expires = DateTime.Now.AddDays(cookieTime);
                }
                cookie.Value = cookieValue;
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                sbu.AppendLine("ok");
                return sbu;
            }
            catch (Exception ex)
            {
                return Common.ReturnException(ex);
            }
        }

        /// <summary>
        /// 读取cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <returns></returns>
        public static StringBuilder ReadCookie(string cookieName)
        {
            try
            {
                StringBuilder sbu = new StringBuilder();
                HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];
                if (Cookie != null)
                {
                    sbu.Append(Cookie.Value.ToString());
                }
                else
                {
                    sbu.AppendLine("");
                }
                return sbu;
            }
            catch (Exception ex)
            {
                return Common.ReturnException(ex);
            }
        }

        /// <summary>
        /// 删除cookies
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <returns></returns>
        public static StringBuilder DeleteCookie(string cookieName)
        {
            try
            {
                StringBuilder sbu = new StringBuilder();
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                sbu.Append("ok");
                return sbu;
            }
            catch (Exception ex)
            {
                return Common.ReturnException(ex);
            }
        }

    }
}
