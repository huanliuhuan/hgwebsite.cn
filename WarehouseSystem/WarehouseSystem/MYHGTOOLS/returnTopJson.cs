using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MYHGTOOLS
{
    /// <summary>
    /// 返回json
    /// </summary>
    public static class returnTopJson
    {
        /// <summary>
        /// 返回code:"",data:,message:""
        /// </summary>
        /// <param name="code">code</param>
        /// <param name="data">要求格式[{"":""}]</param>
        /// <param name="message">message</param>
        /// <returns></returns>
        public static string returnJson(string code, string data, string message)
        {
            System.Text.StringBuilder sbu = new System.Text.StringBuilder();
            sbu.Append("{");
            sbu.Append("\"code\":\"" + code + "\"");
            if (!data.Equals(""))
            {
                sbu.Append(",\"Data\":" + data);
            }
            sbu.Append(",\"message\":\"" + message + "\"");
            sbu.Append("}");
            return sbu.ToString();
        }

        /// <summary>
        /// 返回{"Stat":int,"Msg":msg,"Data":[]}
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string returnJson(int stat, string msg, string data)
        {
            System.Text.StringBuilder sbu = new System.Text.StringBuilder();
            sbu.Append("{");
            sbu.Append("\"Stat\":\"" + stat + "\"");
            sbu.Append(",\"Msg\":\"" + msg + "\"");
            if (!data.Equals(""))
            {
                sbu.Append(",\"Data\":" + data);
            }
            sbu.Append("}");
            return sbu.ToString();
        }

        /// <summary>
        /// 返回"A":"B"
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string returnJson(string key, string value)
        {
            System.Text.StringBuilder sbu = new System.Text.StringBuilder();
            sbu.Append("\"" + key + "\":\"" + value + "\"");
            return sbu.ToString();
        }
    }
}