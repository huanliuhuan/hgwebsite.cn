using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Facroty
{
    public class SqlCreate<T>
    {

        #region 查找

        /// <summary>
        /// 生成条件
        /// </summary>
        /// <param name="sb">条件字符串</param>
        /// <param name="Info">属性</param>
        /// <param name="PageIndex">索引页面</param>
        /// <param name="PageSzie">页面大小</param>
        /// <returns>返回</returns>
        private static StringBuilder CreateWhere(T Model, ref int PageIndex, ref int PageSzie)
        {
            //定义条件
            StringBuilder sb = new StringBuilder(" WHERE ");

            if (Model == null) return new StringBuilder("");
            Type _Type = Model.GetType();
            foreach (PropertyInfo item in _Type.GetProperties())
            {
                object obj = item.GetValue(Model, null);
                if (obj == null) continue;
                if (!IsBase(_Type, item)) //如果不是基类的字段
                {
                    if (sb.Length > 7)
                        sb.Append("AND ");
                    sb.Append(item.Name);
                    sb.Append(" =?");
                    sb.Append(item.Name);
                    sb.Append("  ");
                }
                else
                {
                    if (item.Name == "PageIndex")
                    {
                        PageIndex = Convert.ToInt32(obj);
                    }
                    else if (item.Name == "PageSize")
                    {
                        PageSzie = Convert.ToInt32(obj);
                    }
                }
            }
            if (sb.Length <= 7) sb.Remove(0, sb.Length); //如果没有条件的话移除条件
            return sb;
        }

        /// <summary>
        /// 生成分组条件
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="GroupField">分组字段</param>
        /// <param name="PageIndex">索引页面</param>
        /// <param name="PageSzie">页面大小</param>
        /// <returns>返回</returns>
        private static StringBuilder CreateGroupWhere(T Model, string GroupField, ref int PageIndex, ref int PageSzie)
        {
            //定义条件
            StringBuilder sb = new StringBuilder(" WHERE ");

            if (Model == null) return new StringBuilder("");
            Type _Type = Model.GetType();
            foreach (PropertyInfo item in _Type.GetProperties())
            {
                object obj = item.GetValue(Model, null);
                if (obj == null) continue;
                if (!IsBase(_Type, item)) //如果不是基类的字段
                {
                    if (sb.Length > 7)
                        sb.Append("AND ");
                    sb.Append(item.Name);
                    sb.Append("=?");
                    sb.Append(item.Name);
                    sb.Append(" ");
                }
                else
                {
                    if (item.Name == "PageIndex")
                    {
                        PageIndex = Convert.ToInt32(obj);
                    }
                    else if (item.Name == "PageSize")
                    {
                        PageSzie = Convert.ToInt32(obj);
                    }
                }
            }
            if (sb.Length <= 7) sb.Remove(0, sb.Length); //如果没有条件的话移除条件
            if (GroupField != String.Empty)
            {
                sb.Append(" ");
                sb.Append(" GROUP BY ");
                sb.Append(GroupField);
            }
            return sb;
        }


        /// <summary>
        /// 是否属于基类
        /// </summary>
        /// <param name="Infos">公共属性集</param>
        /// <param name="info">单个公共属性</param>
        /// <returns>在基类里面返回true否则返回false</returns>
        private static bool IsBase(Type Type, PropertyInfo Info)
        {
            bool b = false;
            foreach (PropertyInfo item in Type.BaseType.GetProperties())
            {
                if (item.Name == Info.Name)
                {
                    b = true;
                }
            }
            return b;
        }

        /// <summary>
        /// 返回字段
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>返回字段</returns>
        private static StringBuilder CreateFields(T Model)
        {
            if (Model == null) return new StringBuilder(" * ");
            Type _Type = Model.GetType();//反射返回类型
            string result = String.Empty;
            StringBuilder fields = new StringBuilder();
            foreach (PropertyInfo Info in _Type.GetProperties())
            {
                object obj = Info.GetValue(Model, null);//获取值
                if (IsBase(_Type, Info)) continue;
                string name = Info.Name;
                fields.Append(name);
                fields.Append(",");
            }
            if (fields.Length > 0) fields.Remove(fields.Length - 1, 1);
            return fields;
        }

        /// <summary>
        /// 查询数据表
        /// </summary>
        /// <param name="Model">Model实体</param>
        /// <param name="Where">条件</param>
        /// <returns>返回 删除的字符串</returns>
        public static string Select(T Model)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = CreateWhere(Model, ref PageIndex, ref PageSzie);
            string result = String.Format(Sql.SqlSelect, CreateFields(Model), Model.GetType().Name, Where);
            return result;
        }

        /// <summary>
        /// 查询数据表
        /// </summary>
        /// <param name="Model">Model实体</param>
        /// <param name="Where">条件</param>
        /// <returns>返回 删除的字符串</returns>
        public static string Select(T Model, string Where)
        {
            string result = String.Format(Sql.SqlSelect, CreateFields(Model), Model.GetType().Name, Where);
            return result;
        }


        #region 分页
        /// <summary>
        /// 查询数据By分页
        /// </summary>
        /// <param name="Model">Model实体</param>
        /// <returns>返回字符串</returns>
        public static string SelectByPage(T Model, string SortField, string Sort)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = CreateWhere(Model, ref PageIndex, ref PageSzie);
            return String.Format(Sql.PageSel, CreateFields(Model).ToString(), SortField, Sort, Model.GetType().Name, Where, ((PageIndex - 1) * PageSzie).ToString(), PageSzie.ToString());
        }
        /// <summary>
        /// 总页数
        /// </summary>
        /// <param name="Model"></param>
        /// <returns>返回总页数</returns>
        public static string SelectByPage(T Model)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = CreateWhere(Model, ref PageIndex, ref PageSzie);
            return String.Format(Sql.RecordCount, Model.GetType().Name, Where);
        }

        /// <summary>
        /// 查询数据By分页
        /// </summary>
        /// <param name="Model">Model实体</param>
        /// <returns>返回字符串</returns>
        public static string SelectByPage(T Model, string SortField, string Sort, string Where)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            CreateWhere(Model, ref PageIndex, ref PageSzie);
            return String.Format(Sql.PageSel, CreateFields(Model).ToString(), SortField, Sort, Model.GetType().Name, Where, ((PageIndex - 1) * PageSzie).ToString(), PageSzie.ToString());
        }
        /// <summary>
        /// 总页数
        /// </summary>
        /// <param name="Model"></param>
        /// <returns>返回总页数</returns>
        public static string SelectByPage(T Model, string Where)
        {
            return String.Format(Sql.RecordCount, Model.GetType().Name, Where);
        }


        /// <summary>
        /// 查询数据By分页分组页面
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="GroupField">分组</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">排序</param>
        /// <returns>分组分页</returns>
        public static string SelectByGPage(T Model, string GroupField, string SortField, string Sort)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = CreateGroupWhere(Model, GroupField, ref PageIndex, ref PageSzie);
            return String.Format(Sql.GroupSelect, GroupField, Model.GetType().Name, SortField, SortField, Sort, ((PageIndex - 1) * PageSzie).ToString(), PageSzie.ToString());
        }


        /// <summary>
        /// 总页数
        /// </summary>
        /// <param name="Model"></param>
        /// <returns>返回总页数</returns>
        public static string SelectByGPage(T Model, string GroupField)
        {
            return String.Format(Sql.tempSelect, String.Format(Sql.SqlSelect, GroupField, Model.GetType().Name, " GROUP BY  " + GroupField + ""));
        }


        #endregion

        #endregion

        #region 添加数据

        /// <summary>
        /// 生成添加字段和数据参数
        /// </summary>
        /// <returns>返回字符串</returns>
        public static List<StringBuilder> ValueFieldsAndParam(T Model)
        {
            List<StringBuilder> lsb = new List<StringBuilder>();
            StringBuilder sbfield = new StringBuilder("");//字段
            StringBuilder sbparam = new StringBuilder("");//参数

            Type _Type = Model.GetType();//反射返回类型
            foreach (PropertyInfo Info in _Type.GetProperties())
            {
                object obj = Info.GetValue(Model, null);//获取值
                if (obj == null) continue;
                sbfield.Append(Info.Name);
                sbfield.Append(",");
                sbparam.Append("?");
                sbparam.Append(Info.Name);
                sbparam.Append(",");
            }
            if (sbfield.Length > 0) sbfield.Remove(sbfield.Length - 1, 1);
            if (sbparam.Length > 0) sbparam.Remove(sbparam.Length - 1, 1);
            lsb.Add(sbfield);//添加字段
            lsb.Add(sbparam);//添加参数
            return lsb;
        }
        /// <summary>
        /// 生成添加字符串
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>返回添加的字符串</returns>
        public static string Insert(T Model)
        {
            List<StringBuilder> lsb = ValueFieldsAndParam(Model);
            return String.Format(Sql.SqlInsert, Model.GetType().Name, lsb[0].ToString(), lsb[1].ToString());
        }

        #endregion

        #region 修改数据

        /// <summary>
        /// 返回更新的sql语句
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>添加</returns>
        public static StringBuilder UpDateSql(T Model)
        {
            StringBuilder sb = new StringBuilder();
            Type _Type = Model.GetType();//反射返回类型
            foreach (PropertyInfo Info in _Type.GetProperties())
            {
                object obj = Info.GetValue(Model, null);//获取值
                if (obj == null) continue;
                sb.Append(Info.Name);
                sb.Append("=");
                sb.Append("?");
                sb.Append(Info.Name);
                sb.Append(",");
            }
            if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
            return sb;
        }

        /// <summary>
        /// 更新sql语句
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="Where">条件</param>
        /// <returns>返回sql语句</returns>
        public static string Update(T Model, string Where)
        {
            return String.Format(Sql.SqlUpdate, Model.GetType().Name, UpDateSql(Model), Where);
        }

        #endregion

        #region 删除功能
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public static string Delete(T Model, string Where)
        {
            return String.Format(Sql.SqlDelete, Model.GetType().Name, Where);
        }

        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static string Delete(T Model)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = CreateWhere(Model, ref PageIndex, ref PageSzie);
            return String.Format(Sql.SqlDelete, Model.GetType().Name, Where);
        }

        #endregion

    }
}
