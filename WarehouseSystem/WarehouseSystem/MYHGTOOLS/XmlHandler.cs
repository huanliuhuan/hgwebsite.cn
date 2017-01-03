using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;

namespace MYHGTOOLS
{
    /// <summary>
    /// XML操作
    /// (外部资源，未经测试)
    /// </summary>
    public class XmlHandler
    {
        /*
       * CreateXML 创建XML文件和添加数据，便于之后的操作
       * AddItem 追加数据
       * ReadText 读取数据
       * UpdateText 更新数据
       * DelNode 删除数据
       * NodeCount 数据条数
       */
        /// <summary>
        /// xml名字
        /// </summary>
        private string _Name = "Default.xml";
        /// <summary>
        /// xml名字
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        /// <summary>
        /// 引用Xml类
        /// </summary>
        XmlDocument XmlDoc = new XmlDocument();
        /// <summary>
        /// 创建XML
        /// </summary>
        ///
        public void CreateXml()
        {
            //设置Xml
            XmlWriterSettings settings = new XmlWriterSettings();
            //设置格式（缩进元素）
            settings.Indent = true;
            settings.IndentChars = (" ");
            settings.Encoding = System.Text.Encoding.UTF8;
            //路径问题
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Config\\" + Name;
            if (!System.IO.Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Config"))//如果文件夹不存在
            {
                System.IO.Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "Config"); //创建文件夹
            }
            if (!File.Exists(path)) //如果文件不存在
            {
                using (XmlWriter Writer = XmlWriter.Create(path, settings))
                {
                    //写入standalone属性
                    //XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "GB2312", null);
                    // Writer.WriteStartDocument(true);

                    //写入根节点
                    Writer.WriteStartElement("Roots");
                    // Writer.WriteComment  写入备注
                    //写入属性和属性名字
                    Writer.WriteStartAttribute("SpaceType", "空间类型");
                    Writer.WriteStartAttribute("PinZhi", "优");
                    //写入创建人信息
                    Writer.WriteCData("创建时间：" + DateTime.Now.ToString() + "；创建者：易步科技。 文件编码：" + Guid.NewGuid().ToString());
                    //添加注释
                    Writer.WriteComment("作者：Async");
                    //关闭跟元素，并且写结束标签
                    Writer.WriteEndElement();
                    //将xml关闭并保存
                    Writer.Flush();
                    Writer.Close();
                }
            }
        }
        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="NodeAttr">节点属{"id:12","name:suchao"}</param>
        /// <param name="NodeDesc">节点描述</param>
        /// <param name="NodeValue">节点的值</param>
        public void AddItem(string NodeName, string[] NodeAttr, string NodeDesc, string NodeValue)
        {
            //路径问题
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Config\\" + Name;
            if (File.Exists(path))
            {
                XmlDoc.Load(path);
                //查找根节点
                XmlNode Root = XmlDoc.SelectSingleNode("Roots");
                XmlNodeList XmlList = Root.ChildNodes;
                //判断是否要添加
                bool B = true;
                //创建元素
                XmlElement XmlEle = XmlDoc.CreateElement(NodeName);
                //添加注释
                if (NodeDesc != null && NodeDesc != "")
                {
                    XmlComment XmlCom = XmlDoc.CreateComment(NodeDesc);
                }
                //写入属性
                if (NodeAttr != null && NodeAttr.Length > 0)
                {
                    foreach (string Str in NodeAttr)
                    {
                        string AttrName = "";
                        string Attr = "";
                        //开始截出属性
                        AttrName = Str.Split('=')[0];
                        Attr = Str.Split('=')[1];
                        //给节点赋属性
                        XmlEle.SetAttribute(AttrName, Attr);
                    }
                }
                //写入数值
                if (NodeValue != null && NodeValue != "")
                {
                    XmlEle.InnerText = NodeValue.Trim();
                }
                //插入节点
                Root.AppendChild(XmlEle);
                if (B)
                {
                    //保存数据
                    XmlDoc.Save(path);
                }

            }
        }
        /// <summary>
        /// 读取节点的值
        /// </summary>
        /// <returns>返回值得集合</returns>
        public string ReadText(string NodeName)
        {
            //路径问题
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Config\\" + Name;
            if (File.Exists(path))
            {
                XmlDoc.Load(path);
                XmlNode Root = XmlDoc.SelectSingleNode("Roots");
                //获得节点组
                XmlNodeList XmlList = Root.ChildNodes;
                //循环所有节点
                string Str = String.Empty;
                foreach (XmlNode Node in XmlList)
                {
                    if (Node.Name == NodeName)//读取节点内容
                    {
                        XmlElement XmlEle = (Node as XmlElement);
                        Str = XmlEle.InnerText;
                    }
                }
                return Str;
            }
            else
            {
                return "文件不存在，请确认文件是否存在。";
            }
        }
        /// <summary>
        /// 更新节点
        /// </summary>
        /// <param name="id">要更新的节点ID</param>
        /// <param name="Value">要更新的值</param>
        public void Update(string NodeName, string Value)
        {
            //路径问题
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Config\\" + Name;
            if (File.Exists(path))
            {
                XmlDoc.Load(path);
                XmlNode Root = XmlDoc.SelectSingleNode("Roots");
                XmlNodeList XmlList = Root.ChildNodes;
                //开始循环查找
                foreach (XmlNode Node in XmlList)
                {
                    if (Node.Name == NodeName)
                    {
                        XmlElement XmlEle = (Node as XmlElement);
                        //赋值
                        XmlEle.RemoveAttribute("edittime");
                        XmlEle.SetAttribute("edittime", DateTime.Now.ToString());
                        XmlEle.InnerText = Value;
                    }
                }
                XmlDoc.Save(path);
            }
            else
            {
                throw new Exception("您操作的文件不存在,请确认文件是否存在!");
            }
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="id">要删除的节点的Id</param>
        public void DelNode(string id)
        {
            //路径问题
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Config\\" + Name;
            if (File.Exists(path))
            {
                XmlDoc.Load(path);
                XmlNode Root = XmlDoc.SelectSingleNode("Roots");
                XmlNodeList XmlList = Root.ChildNodes;
                //开始循环
                foreach (XmlNode Node in XmlList)
                {
                    XmlElement XmlEle = (Node as XmlElement);
                    if (XmlEle != null)
                    {
                        if (XmlEle.GetAttribute("Id") == id)
                        {
                            //移除所有属性
                            XmlEle.RemoveAllAttributes();
                            //移除值
                            XmlEle.RemoveAll();
                            //移除节点本身
                            Root.RemoveChild((XmlEle as XmlNode));

                        }
                    }
                }
                XmlDoc.Save(path);
            }
            else
            {
                throw new Exception("您操作的文件不存在,请确认文件是否存在！");
            }
        }
        /// <summary>
        /// 计算数据的条数
        /// </summary>
        /// <returns>返回数据条数</returns>
        public int NodeCount()
        {
            //路径问题
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Config\\" + Name;
            if (File.Exists(path))
            {
                XmlDoc.Load(path);
                XmlNode Root = XmlDoc.SelectSingleNode("Roots");
                XmlNodeList XmlList = Root.ChildNodes;
                //计算行数
                int count = 0;
                //循环
                foreach (XmlNode Node in XmlList)
                {
                    XmlElement XmlEle = (Node as XmlElement);
                    if (XmlEle != null)
                    {
                        count += 1;
                    }
                }
                return count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 计算机返回数据
        /// </summary>
        /// <returns></returns>
        public DataTable ReturnTable()
        {
            try
            {
                DataTable result = new DataTable();
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "Config\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);
                    XmlNode Root = XmlDoc.SelectSingleNode("Roots");
                    XmlNodeList XmlList = Root.ChildNodes;
                    DataColumn dcId = new DataColumn("Id", typeof(Int32));
                    DataColumn dcName = new DataColumn("Name", typeof(String));
                    result.Columns.Add(dcId);
                    result.Columns.Add(dcName);
                    //开始循环查找
                    foreach (XmlNode Node in XmlList)
                    {
                        XmlElement XmlEle = (Node as XmlElement);
                        if (XmlEle != null)
                        {
                            DataRow dr = result.NewRow();
                            dr["Id"] = XmlEle.GetAttribute("Id");
                            dr["Name"] = XmlEle.InnerText;
                            result.Rows.Add(dr);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("转换成数据表的时候");
            }
        }

        #region 从XML读取数据

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllData()
        {
            try
            {
                DataTable result = new DataTable();
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "Config\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);
                    XmlNode Root = XmlDoc.SelectSingleNode("Roots");
                    XmlNodeList XmlList = Root.ChildNodes;
                    //添加数据列
                    foreach (XmlNode Node in XmlList)
                    {
                        if (result.Columns.Count <= 0)   //添加数据列
                        {
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                DataColumn dc = new DataColumn(item.Name);
                                dc.DataType = typeof(String);
                                result.Columns.Add(dc);
                            }
                            //添加文本字段
                            DataColumn DcText = new DataColumn();
                            DcText.ColumnName = "InnerText";
                            DcText.DataType = typeof(String);
                            result.Columns.Add(DcText);
                        }

                        //添加数据部分
                        XmlElement XmlEle = (Node as XmlElement);
                        if (XmlEle != null)
                        {
                            DataRow dr = result.NewRow();
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                dr[item.Name] = item.Value;
                            }
                            dr["InnerText"] = XmlEle.InnerText;
                            result.Rows.Add(dr);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("转换成数据表的时候");
            }
        }

        /// <summary>
        /// 获取需要的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetWhereData(string Attr, string obj)
        {
            try
            {
                DataTable result = new DataTable();
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "Config\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);
                    XmlNode Root = XmlDoc.SelectSingleNode("Roots");
                    XmlNodeList XmlList = Root.ChildNodes;
                    //添加数据列
                    foreach (XmlNode Node in XmlList)
                    {
                        if (result.Columns.Count <= 0)   //添加数据列
                        {
                            if (Node.Attributes == null) continue;
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                DataColumn dc = new DataColumn(item.Name);
                                dc.DataType = typeof(String);
                                result.Columns.Add(dc);
                            }
                            //添加文本字段
                            DataColumn DcText = new DataColumn();
                            DcText.ColumnName = "InnerText";
                            DcText.DataType = typeof(String);
                            result.Columns.Add(DcText);
                        }

                        //添加数据部分
                        XmlElement XmlEle = (Node as XmlElement);
                        if (XmlEle != null)
                        {
                            //加判断是否有满足条件
                            if (XmlEle.GetAttribute(Attr) != obj) continue;
                            DataRow dr = result.NewRow();
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                dr[item.Name] = item.Value;
                            }
                            dr["InnerText"] = XmlEle.InnerText;
                            result.Rows.Add(dr);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("转换成数据表的时候");
            }
        }

        /// <summary>
        /// 返回递归数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecursionData(string sonAttr, string ParAttr, string Obj)
        {
            try
            {
                DataTable result = new DataTable();
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "Config\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);
                    XmlNode Root = XmlDoc.SelectSingleNode("Roots");
                    XmlNodeList XmlList = Root.ChildNodes;
                    //添加数据列
                    foreach (XmlNode Node in XmlList)
                    {
                        if (result.Columns.Count <= 0)   //添加数据列
                        {
                            if (Node.Attributes == null) continue;
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                DataColumn dc = new DataColumn(item.Name);
                                dc.DataType = typeof(String);
                                result.Columns.Add(dc);
                            }
                            //添加文本字段
                            DataColumn DcText = new DataColumn();
                            DcText.ColumnName = "InnerText";
                            DcText.DataType = typeof(String);
                            result.Columns.Add(DcText);
                        }

                        //添加数据部分
                        XmlElement XmlEle = (Node as XmlElement);
                        if (XmlEle != null)
                        {
                            //加判断是否有满足条件
                            if (XmlEle.GetAttribute(ParAttr) != Obj) continue;
                            DataRow dr = result.NewRow();
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                dr[item.Name] = item.Value;
                            }
                            dr["InnerText"] = XmlEle.InnerText;
                            result.Rows.Add(dr);

                            //递归拿取数据
                            RecursionFunction(result, XmlList, ParAttr, sonAttr, XmlEle.GetAttribute(sonAttr));

                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("转换成数据表的时候");
            }
        }


        private void RecursionFunction(DataTable result, XmlNodeList XmlList, string ParAttr,string sonAttr, string Obj) 
        {
            try
            {
                //添加数据列
                foreach (XmlNode Node in XmlList)
                {
                    if (Node.Attributes == null) continue;
                    bool b = false;//默认属性里面不存在数据
                    foreach (XmlAttribute item in Node.Attributes) //判断数据是否存在
                    {
                        if (item.Name == ParAttr && item.Value == Obj)//数据有值
                        { b = true; break; }
                    }
                    if (b)//如果节点
                    {
                        //添加数据部分
                        XmlElement XmlEle = (Node as XmlElement);
                        if (XmlEle != null)
                        {
                            //加判断是否有满足条件
                            if (XmlEle.GetAttribute(ParAttr) != Obj) continue;
                            DataRow dr = result.NewRow();
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                dr[item.Name] = item.Value;
                            }
                            dr["InnerText"] = XmlEle.InnerText;
                            result.Rows.Add(dr);

                            //递归拿取数据
                            RecursionFunction(result, XmlList, ParAttr, sonAttr, XmlEle.GetAttribute(sonAttr));

                        } 
                    }
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region  根据ID操作XML信息

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="id"></param>
        public void DelNodes(string id)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "admin\\xml\\" + Name;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode rootElement = xmlDoc.SelectSingleNode("Roots");
            XmlNodeList xnl = xmlDoc.SelectSingleNode("Roots").ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {
                XmlElement xe = (XmlElement)xnl.Item(i);
                if (xe.GetAttribute("id") == id)
                {
                    rootElement.RemoveChild(xe);
                    if (i < xnl.Count) i = i - 1;
                }
            }
            xmlDoc.Save(path);
        }

        /// <summary>
        /// 更新XML文件中的指定节点内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeValue">更新内容</param>
        /// <returns>更新是否成功</returns>
        public bool UpdateNode(string filePath, string nodeName, string nodeValue, string[] NodeAttr)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = System.AppDomain.CurrentDomain.BaseDirectory + "admin\\xml\\" + Name;
            }
            bool flag = false;

            int Length = NodeAttr.Length;
          


            XmlDocument xml = new XmlDocument();
            xml.Load(filePath);
            foreach (XmlElement item in xml.GetElementsByTagName("id"))
            {
                string id = item.Attributes["id"].Value;
                if (id == nodeName)
                {
                    item.InnerText = nodeValue;

                    //添加属性
                    for (int i = 0; i < Length; i++)
                    {
                        string NodeName = NodeAttr[i].Split(',')[0].Split(':')[0];
                        string NodeValue = NodeAttr[i].Split(',')[0].Split(':')[1];
                        item.SetAttribute(NodeName, NodeValue);
                    }

                    xml.Save(filePath);
                    flag = true;
                }
            }

            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="nodeName"></param>
        /// <param name="NodeAttr"></param>
        /// <returns></returns>
        public bool AddNode(string filePath, string nodeName, string[] NodeAttr,string innertext)
        {
            bool flag = false;
            //路径问题
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = System.AppDomain.CurrentDomain.BaseDirectory + "admin\\xml\\" + Name;
            }

            //加载xml文档
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            //创建节点
            XmlElement xmlElement = doc.CreateElement(nodeName);

            int Length = NodeAttr.Length;
            //添加属性
            for (int i = 0; i < Length; i++)
            {
                string NodeName = NodeAttr[i].Split(',')[0].Split(':')[0];
                string NodeValue = NodeAttr[i].Split(',')[0].Split(':')[1];
                xmlElement.SetAttribute(NodeName, NodeValue);
            }
            //xmlElement.SetAttribute("ID", "21");
            //xmlElement.SetAttribute("Name", "王六");
            //将节点加入到指定的节点下
            xmlElement.InnerText = innertext;
            XmlNode xml = doc.DocumentElement.PrependChild(xmlElement);
            if (xml != null)
            {
                doc.Save(filePath);
                flag = true;
            }

            return flag;

        }
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDataByWhere()
        {
            try
            {
                DataTable result = new DataTable();
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "admin\\xml\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);
                    XmlNode Root = XmlDoc.SelectSingleNode("Roots");
                    XmlNodeList XmlList = Root.ChildNodes;
                    //添加数据列
                    foreach (XmlNode Node in XmlList)
                    {
                        if (result.Columns.Count <= 0)   //添加数据列
                        {
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                DataColumn dc = new DataColumn(item.Name);
                                dc.DataType = typeof(String);
                                result.Columns.Add(dc);
                            }
                            //添加文本字段
                            DataColumn DcText = new DataColumn();
                            DcText.ColumnName = "InnerText";
                            DcText.DataType = typeof(String);
                            result.Columns.Add(DcText);
                        }

                        //添加数据部分
                        XmlElement XmlEle = (Node as XmlElement);
                        if (XmlEle != null)
                        {
                            DataRow dr = result.NewRow();
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                dr[item.Name] = item.Value;
                            }
                            dr["InnerText"] = XmlEle.InnerText;
                            result.Rows.Add(dr);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("转换成数据表的时候");
            }
        }
        #endregion

    }
}
