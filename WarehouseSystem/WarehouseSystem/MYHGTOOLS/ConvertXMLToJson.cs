using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MYHGTOOLS
{
    /// <summary>
    /// 创建人：刘欢
    /// 创建时间：2015年1月8日 21:59:37
    /// 说明：将XML转换为json
    /// </summary>
    public static class ConvertXMLToJson
    {
        public static string toXmlNode(string xmlStr,out Exception exs)
        {
            try
            {
                //序列xml
                XmlDocument doc = new XmlDocument();
                //读取xml
                doc.LoadXml(xmlStr);
                //将xml转为json
                string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
                //示例一个异常
                exs = new Exception("ok");
                return json;
            }
            catch (Exception ex)
            {
                exs = ex;
                return "";
            }
        }
    }
}
