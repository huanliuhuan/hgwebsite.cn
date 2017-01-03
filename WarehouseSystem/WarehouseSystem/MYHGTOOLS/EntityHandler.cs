using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace MYHGTOOLS
{
    /// <summary>
    /// 实体转换类
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public class EntityHandler<T> where T : new()
    {

        #region DataTable转换成实体类

        /// <summary>
        /// 填充对象列表：用DataSet的第一个表填充实体类
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        public List<T> FillModel(DataSet ds)
        {
            if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return FillModel(ds.Tables[0]);
            }
        }

        /// <summary>  
        /// 填充对象列表：用DataSet的第index个表填充实体类
        /// </summary>  
        public List<T> FillModel(DataSet ds, int index)
        {
            if (ds == null || ds.Tables.Count <= index || ds.Tables[index].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return FillModel(ds.Tables[index]);
            }
        }

        /// <summary>  
        /// 填充对象列表：用DataTable填充实体类
        /// </summary>  
        public List<T> FillModel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            List<T> modelList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                //T model = (T)Activator.CreateInstance(typeof(T));  
                T model = new T();
                foreach (DataColumn item in dt.Columns)
                {
                    PropertyInfo propertyInfo = model.GetType().GetProperty(item.ColumnName);
                    if (propertyInfo != null && dr[item.ColumnName] != DBNull.Value)
                        propertyInfo.SetValue(model, dr[item.ColumnName], null);
                }
                modelList.Add(model);
            }
            return modelList;
        }

        /// <summary>  
        /// 填充对象：用DataRow填充实体类
        /// </summary>  
        public T FillModel(DataRow dr)
        {
            if (dr == null)
            {
                return default(T);
            }

            //T model = (T)Activator.CreateInstance(typeof(T));  
            T model = new T();
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                if (propertyInfo != null && dr[i] != DBNull.Value)
                    propertyInfo.SetValue(model, dr[i], null);
            }
            return model;
        }

        #endregion

        #region 实体类转换成DataTable

        /// <summary>
        /// 实体类转换成DataSet
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public DataSet FillDataSet(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            else
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(FillDataTable(modelList));
                return ds;
            }
        }

        /// <summary>
        /// 实体类转换成DataTable
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public DataTable FillDataTable(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            DataTable dt = CreateData(modelList[0]);

            foreach (T model in modelList)
            {
                DataRow dataRow = dt.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// 根据实体类得到表结构
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        private DataTable CreateData(T model)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                //dataTable.Columns.Add(new DataColumn(propertyInfo.Name,propertyInfo.PropertyType));
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name));
            }
            return dataTable;
        }
        #endregion

        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>返回字符串</returns>
        public string ConvertToString(T Model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();//实例化
                Type Ty = Model.GetType();//实例化实体
                foreach (PropertyInfo item in Ty.GetProperties())
                {
                    if (item == null) continue;//如果为空的话

                    object obj = item.GetValue(Model, null);

                    sb.Append(item.Name);
                    sb.Append(":");
                    if (obj == null)
                        sb.Append("");
                    else
                        sb.Append(obj.ToString());
                    sb.Append("々");
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将字符串转换成实体
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <returns>返回泛型实体</returns>
        public T ConvertToEntity(string Str)
        {
            try
            {
                if (string.IsNullOrEmpty(Str)) throw new Exception("传入的字符串不能为空！");
                T Model = new T();
                Type Ty = Model.GetType();//实例化实体

                

                foreach (string item in Str.Split('々'))
                {
                    if (string.IsNullOrEmpty(item)) continue;
                    string[] strs = item.Split(':');

                    PropertyInfo myproperty=Ty.GetProperty(strs[0]);
                    if(myproperty.PropertyType.IsGenericType) //泛型
                    {
                        //泛型Nullable<>
                        Type genericTypeDefinition = myproperty.PropertyType.GetGenericTypeDefinition();
                        if (genericTypeDefinition == typeof(Nullable<>))
                        {
                            myproperty.SetValue(Model, string.IsNullOrEmpty(strs[1]) ? null : Convert.ChangeType(strs[1], Nullable.GetUnderlyingType(myproperty.PropertyType)), null);
                        }
                    }
                    else
                    {
                        //非泛型
                        myproperty.SetValue(Model, string.IsNullOrEmpty(strs[1]) ? null : Convert.ChangeType(strs[1], myproperty.PropertyType), null);
                    }

                }
                return Model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
