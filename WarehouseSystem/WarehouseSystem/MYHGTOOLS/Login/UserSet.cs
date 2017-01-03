using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MYHGTOOLS.Login
{
    /// <summary>
    /// 创建人：刘欢
    /// 创建时间：2015年1月18日 17:50:53
    /// 说明：登录类
    /// </summary>
    public class UserSet
    {
        #region 全局

        /// <summary>
        /// 定义私有变量
        /// </summary>
        private string _User = string.Empty;

        //定义安全加密类
        SafeHandler iSafe = new SafeHandler();


        private int _days = 0;
        /// <summary>
        /// cookie保存时间
        /// </summary>
        public int Days
        {
            set { _days = value; }
        }


        /// <summary>
        /// 给用户赋值
        /// </summary>
        public string sUser
        {
            set
            {
                _User = value;
                _User = iSafe.Encrypto(_User);
                SessionHandler.setSession("user", _User, 10000);
                string WebDomin = ConfigurationManager.AppSettings["Domain"].ToString();//cookie跨域访问的问题
                if (string.IsNullOrWhiteSpace(WebDomin))
                {
                    CookieHandler.setCookie("user", _User, _days == 0 ? 1 : _days);
                }
                else
                {
                    CookieHandler.setCookie("user", _User, _days == 0 ? 1 : _days, WebDomin);
                }

            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 退出的方法
        /// </summary>
        public void LoginOut()
        {
            //清空session
            SessionHandler.remSession("user");
            string webDomin = ConfigurationManager.AppSettings["Domin"].ToString();
            if (string.IsNullOrWhiteSpace(webDomin))
            {
                CookieHandler.remCookie("user");
            }
            else
            {
                CookieHandler.remCookie("user", webDomin);
            }
        }

        #endregion
    }
}
