using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;

namespace MYHGTOOLS.Login
{
    /// <summary>
    /// 创建人：刘欢
    /// 创建时间：2015年1月18日 18:15:31
    /// 说明：BaseLogin
    /// </summary>
    public class BaseLogin : System.Web.UI.Page, System.Web.SessionState.IRequiresSessionState
    {
        #region 全局

        /// <summary>
        /// 定义私有变量用户结构
        /// </summary>
        private string _User = string.Empty;

        //定义安全加密
        SafeHandler iSafe = new SafeHandler();

        /// <summary>
        /// 用户信息
        /// </summary>
        public string User
        {
            get
            {
                if (SessionHandler.getSession("user") != null)
                {
                    _User = iSafe.Decrypto(SessionHandler.getSession("user").ToString());
                }
                else
                {
                    string webDomin = ConfigurationManager.AppSettings["Domin"].ToString();
                    string cookiemsg = string.Empty;
                    if (string.IsNullOrWhiteSpace(webDomin))
                    {
                        cookiemsg = CookieHandler.getCookie("user");
                    }
                    else
                    {
                        cookiemsg = CookieHandler.getCookie("user", webDomin);
                    }
                    if (!string.IsNullOrWhiteSpace(cookiemsg))
                    {
                        _User = iSafe.Decrypto(cookiemsg);
                    }
                }
                return _User;
            }
        }


        #endregion

        #region 方法

        public override void ProcessRequest(System.Web.HttpContext context)
        {
            string webDomin = ConfigurationManager.AppSettings["Domin"];//cookie跨域访问的问题
            string loginUrl = ConfigurationManager.AppSettings["LoginUrl"].ToString();//登录的url
            //获取cookie缓存
            string cookiemsg = string.Empty;
            if (string.IsNullOrWhiteSpace(webDomin))
            {
                cookiemsg = CookieHandler.getCookie("user");
            }
            else
            {
                cookiemsg = CookieHandler.getCookie("user", webDomin);
            }
            if (SessionHandler.getSession("user") == null)
            {
                if (string.IsNullOrWhiteSpace(loginUrl))
                {
                    throw new Exception("请检查配置文件（Web.Config）的AppSettings节点里面配置键为\"LoginUrl\"的节点。");
                }
                else
                {
                    //跳转到登录页面
                    string script = loginUrl;
                    HttpContext.Current.Response.Write(HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Server.HtmlEncode("<script language=javascript>")));
                    HttpContext.Current.Response.Write(string.Format("top.location.href='{0}?LinkUrl={1}'", script, HttpUtility.UrlEncode(HttpContext.Current.Request.Url.AbsoluteUri)));
                    HttpContext.Current.Response.Write("</script>");
                    HttpContext.Current.Response.End();
                }
            }
            base.ProcessRequest(context);
        }

        #endregion
    }
}
