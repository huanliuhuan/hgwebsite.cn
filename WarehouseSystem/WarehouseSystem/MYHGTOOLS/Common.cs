using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MYHGTOOLS
{
    /// <summary>
    /// 创建人：刘欢
    /// 创建日期：2014-10-13
    /// 说明：公共方法
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static StringBuilder ReturnException(Exception ex)
        {
            StringBuilder sbu = new StringBuilder();
            sbu.AppendLine("错误发生时间：" + ex.Data);
            sbu.AppendLine("异常链接：" + ex.HelpLink);
            sbu.AppendLine("异常信息：" + ex.Message);
            sbu.AppendLine("应用程序对象名称：" + ex.Source);
            sbu.AppendLine("引发异常的方法：" + ex.TargetSite);
            return sbu;
        }
    }
}
