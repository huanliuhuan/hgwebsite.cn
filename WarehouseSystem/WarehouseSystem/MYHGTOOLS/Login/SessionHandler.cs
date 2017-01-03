using System;
using System.Collections.Generic;
using System.Text;

namespace MYHGTOOLS.Login
{
    class SessionHandler
    {
        #region 基本操作方法

        /// <summary>
        /// 添加session缓存
        /// </summary>
        /// <param name="strKey">键</param>
        /// <param name="objValue">值</param>
        /// <param name="strTimeout">超时</param>
        /// <returns>成功返回true</returns>
        public static bool setSession(string strKey, object objValue, int strTimeout)
        {
            try
            {
                if (System.Web.HttpContext.Current.Session[strKey] != null)
                {
                    System.Web.HttpContext.Current.Session.Remove(strKey);
                }
                System.Web.HttpContext.Current.Session[strKey] = objValue;
                System.Web.HttpContext.Current.Session.Timeout = strTimeout;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取Session的值
        /// </summary>
        /// <param name="strKey">键</param>
        /// <returns>返回值</returns>
        public static object getSession(string strKey)
        {
            if (System.Web.HttpContext.Current.Session[strKey] != null)
            {
                return System.Web.HttpContext.Current.Session[strKey];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除Session
        /// </summary>
        /// <param name="strKey">键</param>
        /// <returns>成功返回 true</returns>
        public static bool remSession(string strKey)
        {
            try
            {
                if (System.Web.HttpContext.Current.Session[strKey] != null)
                {
                    System.Web.HttpContext.Current.Session.Remove(strKey);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 多条Session添加方法

        /// <summary>
        /// 添加多条
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="strTimeout">超时</param>
        /// <returns>成功返回true</returns>
        public static bool setSessions(Dictionary<string, object> dic, int strTimeout)
        {
            try
            {
                foreach (string strKey in dic.Keys)
                {
                    if (System.Web.HttpContext.Current.Session[strKey] != null)
                    {
                        System.Web.HttpContext.Current.Session.Remove(strKey);
                    }
                    System.Web.HttpContext.Current.Session[strKey] = dic[strKey];
                    System.Web.HttpContext.Current.Session.Timeout = strTimeout;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取session值
        /// </summary>
        /// <param name="strKeys">键组</param>
        /// <returns>返回字典</returns>
        public static Dictionary<string, object> getSessions(List<string> strKeys)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (string item in strKeys)
            {
                dic.Add(item, System.Web.HttpContext.Current.Session[item]);
            }
            return dic;
        }

        /// <summary>
        /// 删除Session
        /// </summary>
        public static bool remSessions(List<string> strKeys)
        {
            foreach (string strKey in strKeys)
            {
                if (System.Web.HttpContext.Current.Session[strKey] != null)
                {
                    System.Web.HttpContext.Current.Session.Remove(strKey);
                }
            }
            return true;
        }

        #endregion
    }
}
