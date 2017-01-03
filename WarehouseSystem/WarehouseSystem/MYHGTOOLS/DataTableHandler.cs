using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;

namespace MYHGTOOLS
{
    /// <summary>
    /// Datatable操作类
    /// </summary>
    public class  DataTableHandler<T>
    {
        /// <summary>
        /// 将数据表转化成list对象
        /// </summary>
        /// <returns>返回list对象</returns>
        public static List<Dictionary<string, object>> GetList(DataTable dt)
        {
            //实例化list表
            List<Dictionary<string, object>> returnlist = new List<Dictionary<string, object>>();
            //第一行添加列名
            Dictionary<string, object> dcname = new Dictionary<string, object>();

            //添加数据
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                bool bl = false;//可以添加默认
                foreach (DataColumn dc in dt.Columns)
                {
                    object obj = dr[dc];
                    dic.Add(dc.ColumnName, obj);
                    if (obj != null && !string.IsNullOrEmpty(obj.ToString())) bl = true;
                }
                if (bl)
                    returnlist.Add(dic);
            }

            return returnlist;
        }

        //public static List<T> ToList(T Model,DataTable dt)
        //{
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        List<T> ModelList = new List<T>();
        //        List<StringBuilder> sbuList = ValueFieldsAndParam(Model);
        //        DataRow dr = dt.Rows[0];
        //        foreach (var item in sbuList[0].ToString().Split(','))
        //        {
        //            ModelList.Add(T item);
        //            string a = dr[item].ToString();
        //        }
        //    }
        //    return null;
        //}

        /// <summary>
        /// 将DataTable转换为list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public static List<T> TableToList<T>(DataTable tbl)
        {
            var list = new List<T>();
            foreach (DataRow row in tbl.Rows)
            {
                var stu = Activator.CreateInstance<T>();
                var props = typeof(T).GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    if (tbl.Columns.Contains(prop.Name) && !row.IsNull(prop.Name))
                    {
                        prop.SetValue(stu, row[prop.Name], null);
                    }
                }

                list.Add(stu);
            }
            return list;
        }

        /// <summary>
        /// 获取添加字段和添加参数
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static List<StringBuilder> ValueFieldsAndParam(T Model)
        {
            List<StringBuilder> lsb = new List<StringBuilder>();
            StringBuilder sbfield = new StringBuilder("");//字段
            StringBuilder sbparam = new StringBuilder("");//参数

            Type _Type = Model.GetType();//反射返回类型

            StringBuilder TableName = new StringBuilder();
            TableName.Append(_Type.Name);//以实体名作为表名
            int index = 0;
            StringBuilder idname = new StringBuilder();//第一个字段 一般为主键
            foreach (PropertyInfo Info in _Type.GetProperties())
            {

                if (index == 0)
                {
                    idname.Append(Info.Name);
                    index++;//索引加1；
                }
                object obj = Info.GetValue(Model, null);//获取值
                sbfield.Append(Info.Name);
                sbfield.Append(",");
               
            }
            if (sbfield.Length > 0) sbfield.Remove(sbfield.Length - 1, 1);
            if (sbparam.Length > 0) sbparam.Remove(sbparam.Length - 1, 1);
            lsb.Add(sbfield);//0 添加字段
            lsb.Add(sbparam);//1 添加参数
            lsb.Add(idname);//2 第一个字段 一般为主键
            lsb.Add(TableName);//3 以实体名作为表名
            return lsb;
        }


        /// <summary>
        /// 将list转换为DataTable
        /// 注意：该方法不允许有?值类型System.Nullable`1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aIList"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromIList<T>(List<T> aIList)
        {

            DataTable _returnTable = new DataTable();

            if (aIList.Count > 0)
            {

                //Creates the table structure looping in the in the first element of the list

                object _baseObj = aIList[0];

                Type objectType = _baseObj.GetType();

                PropertyInfo[] properties = objectType.GetProperties();

                DataColumn _col;

                foreach (PropertyInfo property in properties)
                {
                    if (!property.PropertyType.FullName.Equals("System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]"))
                    {
                        _col = new DataColumn();

                        _col.ColumnName = (string)property.Name;

                        _col.DataType = property.PropertyType;

                        _returnTable.Columns.Add(_col);
                    }

                }

                //Adds the rows to the table

                DataRow _row;

                foreach (object objItem in aIList)
                {

                    _row = _returnTable.NewRow();

                    foreach (PropertyInfo property in properties)
                    {
                        if (!property.PropertyType.FullName.Equals("System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]"))
                        {
                            _row[property.Name] = property.GetValue(objItem, null);
                        }
                    }

                    _returnTable.Rows.Add(_row);

                }

            }

            return _returnTable;

        }
     
    }
}