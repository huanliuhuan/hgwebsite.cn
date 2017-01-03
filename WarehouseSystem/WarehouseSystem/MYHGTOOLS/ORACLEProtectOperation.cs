﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Reflection;
using System.Data;

namespace MYHGTOOLS
{
    /// <summary>
    /// 创建人：刘欢
    /// 创建时间：2015年1月8日 22:31:35
    /// 说明：增删改从Oracle
    /// 注意：必须配置connStringOracle
    /// 配置方法：http://www.cnblogs.com/myblogslh/p/4212105.html
    /// </summary>
    public class ORACLEProtectOperation<T>
    {
        /// <summary>
        /// 判断是否存在sql关键字
        /// </summary>
        /// <param name="str">参数</param>
        /// <returns>返回数据</returns>
        public static bool ProtectIsShow(string str, out StringBuilder sbu)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                int index = 0;
                string sqlstr = "";
                string word = "and |exec |insert |select |delete |update |chr |mid |master |or |truncate |char |declare |join ";
                if (str == null)
                {
                    sb.Append("数据为空！");
                    sbu = sb;
                    return false;
                }
                foreach (string i in word.Split('|'))
                {
                    if (str.ToLower().Contains(i))
                    {
                        sqlstr += i.ToString();
                        index++;
                    }
                }
                sb.Append("当前存在" + index + "个关键字:" + sqlstr);
                sbu = sb;
                if (index > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                sbu = Common.ReturnException(ex);
                return false;
            }
        }

        /// <summary>
        /// insert
        /// </summary>
        /// <param name="Model">添加实体</param>
        /// <returns>返回成功ID失败则返回0</returns>
        public static string ReturnInsert(T Model)
        {
            try
            {
                List<StringBuilder> list = ValueFieldsAndParam(Model, "Add");
                StringBuilder sbu = new StringBuilder();
                StringBuilder IsOk = new StringBuilder();
                sbu.Append("insert into " + list[3] + "(");
                sbu.Append("" + list[0] + ")");
                sbu.Append(" values (");
                sbu.Append("" + list[1] + ")");
                sbu.Append(";select @@IDENTITY");

                OracleParameter[] para = GetParas(Model);
                object obj = DbHelperOra.GetSingle(sbu.ToString(), para);
                if (obj == null)
                {
                    IsOk.Append("0");
                    return IsOk.ToString();
                }
                else
                {
                    IsOk.Append(obj);
                    return IsOk.ToString();
                }
            }
            catch (Exception ex)
            {
                return Common.ReturnException(ex).ToString();
            }
        }


        /// <summary>
        /// update
        /// </summary>
        /// <param name="Model">修改实体</param>
        /// <param name="where">条件</param>
        /// <returns>成功为1失败为0</returns>
        public static string ReturnUpdateIsOK(T Model, string where)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (ProtectIsShow(where, out sb))
                {
                    sb.AppendLine("内含危险字符！");
                    return sb.ToString();
                }
                else
                {
                    List<StringBuilder> sbuList = ValueFieldsAndParam(Model, "Update");
                    OracleParameter[] para = GetParas(Model);
                    StringBuilder strSql = new StringBuilder();
                    StringBuilder sbu = new StringBuilder();
                    strSql.Append("update " + sbuList[3] + " set ");
                    strSql.Append(sbuList[0]);
                    if (!string.IsNullOrEmpty(where))
                    {
                        strSql.Append(" where " + where);
                    }
                    else
                    {
                        strSql.Append(sbuList[4]);
                    }
                    int rows = DbHelperOra.ExecuteSql(strSql.ToString(), para);
                    if (rows > 0)
                    {
                        sbu.Append("1");
                    }
                    else
                    {
                        sbu.Append("0");
                    }
                    return sbu.ToString();
                }
            }
            catch (Exception ex)
            {
                return Common.ReturnException(ex).ToString();
            }
        }

        /// <summary>
        /// Delete   
        /// </summary>
        /// <param name="Model">删除实体</param>
        /// <param name="Model">where</param>
        /// <returns>1为成功0为失败</returns>
        public static StringBuilder ReturnDeleteIsOK(T Model, string where)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (ProtectIsShow(where, out sb))
                {
                    sb.AppendLine("内含危险字符！");
                    return sb;
                }
                else
                {
                    List<StringBuilder> sbuList = ValueFieldsAndParam(Model, "Delete");
                    OracleParameter[] para = GetParas(Model);
                    StringBuilder sqlStr = new StringBuilder();
                    StringBuilder sbu = new StringBuilder();
                    sqlStr.Append("delete from " + sbuList[3]);
                    //sqlStr.Append(sbuList[4]);
                    if (!string.IsNullOrWhiteSpace(where))
                    {
                        sqlStr.Append(" where " + where);
                    }
                    //sqlStr.Append(" where ")
                    int result = DbHelperOra.ExecuteSql(sqlStr.ToString(), para);
                    if (result > 0)
                    {
                        sbu.Append("1");
                    }
                    else
                    {
                        sbu.Append("0");
                    }
                    return sbu;
                }
            }
            catch (Exception ex)
            {
                return Common.ReturnException(ex);
            }
        }

        /// <summary>
        /// Delete   
        /// </summary>
        /// <param name="Model">删除实体</param>
        /// <returns>1为成功0为失败</returns>
        public static string ReturnDeleteIsOK(T Model)
        {
            try
            {
                List<StringBuilder> sbuList = ValueFieldsAndParam(Model, "Delete");
                OracleParameter[] para = GetParas(Model);
                StringBuilder sqlStr = new StringBuilder();
                StringBuilder sbu = new StringBuilder();
                sqlStr.Append("delete from " + sbuList[3]);
                sqlStr.Append(sbuList[4]);
                int result = DbHelperOra.ExecuteSql(sqlStr.ToString(), para);
                if (result > 0)
                {
                    sbu.Append("1");
                }
                else
                {
                    sbu.Append("0");
                }
                return sbu.ToString();
            }
            catch (Exception ex)
            {
                return Common.ReturnException(ex).ToString();
            }
        }


        /// <summary> 
        /// 根据页数分页 
        /// </summary> 
        /// <param name="page"></param> 
        /// <param name="pagesize"></param> 
        /// <param name="where"></param> 
        /// <param name="column"></param> 
        /// <returns></returns> 
        public static DataSet GetListByPage(int page, int pagesize, string where, string column)
        {
            System.Text.StringBuilder sbu5 = new StringBuilder();
            int num1 = (page - 1) * pagesize;
            int num2 = page * pagesize;
            sbu5.Append("select " + column);
            sbu5.Append(" FROM(");
            sbu5.Append("SELECT A.*, ROWNUM RN FROM (SELECT * FROM tabel WHERE 1=1" + where + ") A WHERE 1=1" + where + " AND ROWNUM <= " + num2);
            sbu5.Append(")");
            sbu5.Append("where RN >" + num1);
            return DbHelperOra.Query(sbu5.ToString());
        }

        /// <summary> 
        ///  获得数据列表 
        /// </summary> 
        /// <param name="strWhere"></param> 
        /// <param name="column"></param> 
        /// <returns></returns> 
        public static DataSet GetList(string strWhere, string column)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + column);
            strSql.Append(" FROM table");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            return DbHelperOra.Query(strSql.ToString());
        }


        /// <summary>
        /// 获取添加字段和添加参数
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static List<StringBuilder> ValueFieldsAndParam(T Model, string type)
        {
            List<StringBuilder> lsb = new List<StringBuilder>();
            StringBuilder sbfield = new StringBuilder("");//字段
            StringBuilder sbparam = new StringBuilder("");//参数

            Type _Type = Model.GetType();//反射返回类型

            StringBuilder TableName = new StringBuilder();
            TableName.Append(_Type.Name);//以实体名作为表名
            int index = 0;
            StringBuilder idname = new StringBuilder();//第一个字段 一般为主键
            StringBuilder where = new StringBuilder();//更新时的where条件
            foreach (PropertyInfo Info in _Type.GetProperties())
            {

                if (index == 0)
                {
                    idname.Append(Info.Name);
                    index++;//索引加1；
                }
                object obj = Info.GetValue(Model, null);//获取值

                if (type == "Select")
                {
                    sbfield.Append(Info.Name);
                    sbfield.Append(",");
                }

                if (obj == null) continue;
                if (type == "Add")
                {
                    if (Info.Name != idname.ToString())
                    {
                        sbfield.Append(Info.Name);
                        sbfield.Append(",");
                        sbparam.Append("@");
                        sbparam.Append(Info.Name);
                        sbparam.Append(",");
                    }
                }
                else if (type == "Update")
                {

                    if (Info.Name != idname.ToString())
                    {
                        sbfield.Append(Info.Name);
                        sbfield.Append("=@");
                        sbfield.Append(Info.Name);
                        sbfield.Append(",");
                    }
                    else
                    {
                        where.Append(" where ");
                        where.Append(Info.Name);
                        where.Append("=@");
                        where.Append(Info.Name);
                    }
                }
                else if (type == "Delete")
                {
                    where.Append(" where ");
                    where.Append(Info.Name);
                    where.Append("=@");
                    where.Append(Info.Name);
                }
            }
            if (sbfield.Length > 0) sbfield.Remove(sbfield.Length - 1, 1);
            if (sbparam.Length > 0) sbparam.Remove(sbparam.Length - 1, 1);
            lsb.Add(sbfield);//0 添加字段
            lsb.Add(sbparam);//1 添加参数
            lsb.Add(idname);//2 第一个字段 一般为主键
            lsb.Add(TableName);//3 以实体名作为表名
            lsb.Add(where);//4 更新时的where条件
            return lsb;
        }

        /// <summary>
        /// 实体转换成参数数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static OracleParameter[] GetParas(object obj)
        {
            IDictionary<string, object> testDict = new Dictionary<string, object>();

            Type t = obj.GetType();
            PropertyInfo[] proInfos = t.GetProperties();
            OracleParameter[] paras = new OracleParameter[] { };
            IList<OracleParameter> list = new List<OracleParameter>();

            foreach (PropertyInfo item in proInfos)
            {
                OracleParameter para = new OracleParameter();
                object objValue = item.GetValue(obj, null);//获取值
                if (objValue == null) continue;
                para.Value = objValue;
                para.ParameterName = "@" + item.Name;
                list.Add(para);
            }
            paras = list.ToArray();
            return paras;
        }


        #region 创建Sql语句
        #region 查找

        /// <summary>
        /// 生成条件
        /// </summary>
        /// <param name="sb">条件字符串</param>
        /// <param name="Info">属性</param>
        /// <param name="PageIndex">索引页面</param>
        /// <param name="PageSzie">页面大小</param>
        /// <returns>返回</returns>
        private StringBuilder CreateWhere(T Model, ref int PageIndex, ref int PageSzie)
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
                        sb.Append("AND");
                    sb.Append(" ");
                    sb.Append(item.Name);
                    sb.Append("=@");
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
        private StringBuilder CreateGroupWhere(T Model, string GroupField, ref int PageIndex, ref int PageSzie)
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
                        sb.Append("AND");
                    sb.Append(" ");
                    sb.Append(item.Name);
                    sb.Append("=@");
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
        private bool IsBase(Type Type, PropertyInfo Info)
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
        private StringBuilder CreateFields(T Model, int Top = 0)
        {
            if (Model == null) return new StringBuilder(" * ");
            Type _Type = Model.GetType();//反射返回类型
            string result = String.Empty;
            StringBuilder fields = new StringBuilder();
            if (Top != 0) //添加top值
            {
                fields.Append(" TOP ");
                fields.Append(Top.ToString());
                fields.Append(" ");
            }
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
        public string Select(T Model, int Top = 0)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = CreateWhere(Model, ref PageIndex, ref PageSzie);
            string result = String.Format(CreateSql.SqlSelect, CreateFields(Model, Top), Model.GetType().Name, Where);
            return result;
        }

        /// <summary>
        /// 查询数据表
        /// </summary>
        /// <param name="Model">Model实体</param>
        /// <param name="Where">条件</param>
        /// <returns>返回 删除的字符串</returns>
        public string Select(T Model, string Where, int Top = 0)
        {
            string result = String.Format(CreateSql.SqlSelect, CreateFields(Model, Top), Model.GetType().Name, Where);
            return result;
        }


        #region 分页
        /// <summary>
        /// 查询数据By分页
        /// </summary>
        /// <param name="Model">Model实体</param>
        /// <returns>返回字符串</returns>
        public string SelectByPage(T Model, string SortField, string Sort, int Top = 0)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = CreateWhere(Model, ref PageIndex, ref PageSzie);
            return String.Format(CreateSql.PageSel, CreateFields(Model, Top).ToString(), SortField, Sort, Model.GetType().Name, Where, ((PageIndex - 1) * PageSzie + 1).ToString(), (PageIndex * PageSzie).ToString());
        }
        /// <summary>
        /// 总页数
        /// </summary>
        /// <param name="Model"></param>
        /// <returns>返回总页数</returns>
        public string SelectByPage(T Model)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = CreateWhere(Model, ref PageIndex, ref PageSzie);
            return String.Format(CreateSql.RecordCount, Model.GetType().Name, Where);
        }

        /// <summary>
        /// 查询数据By分页
        /// </summary>
        /// <param name="Model">Model实体</param>
        /// <returns>返回字符串</returns>
        public string SelectByPage(T Model, string SortField, string Sort, string Where, int Top = 0)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            CreateWhere(Model, ref PageIndex, ref PageSzie);
            return String.Format(CreateSql.PageSel, CreateFields(Model, Top).ToString(), SortField, Sort, Model.GetType().Name, Where, ((PageIndex - 1) * PageSzie).ToString(), PageSzie.ToString());
        }
        /// <summary>
        /// 总页数
        /// </summary>
        /// <param name="Model"></param>
        /// <returns>返回总页数</returns>
        public string SelectByPage(T Model, string Where)
        {
            return String.Format(CreateSql.RecordCount, Model.GetType().Name, Where);
        }


        /// <summary>
        /// 查询数据By分页分组页面
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="GroupField">分组</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">排序</param>
        /// <returns>分组分页</returns>
        public string SelectByGPage(T Model, string GroupField, string SortField, string Sort)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = CreateGroupWhere(Model, GroupField, ref PageIndex, ref PageSzie);
            return String.Format(CreateSql.GroupSelect, GroupField, Model.GetType().Name, SortField, SortField, Sort, ((PageIndex - 1) * PageSzie).ToString(), PageSzie.ToString());
        }

        /// <summary>
        /// 总页数
        /// </summary>
        /// <param name="Model"></param>
        /// <returns>返回总页数</returns>
        public string SelectByGPage(T Model, string GroupField)
        {
            return String.Format(CreateSql.tempSelect, String.Format(CreateSql.SqlSelect, GroupField, Model.GetType().Name, " GROUP BY  " + GroupField + ""));
        }


        #endregion

        #endregion

        #region 添加数据

        /// <summary>
        /// 生成添加字段和数据参数
        /// </summary>
        /// <returns>返回字符串</returns>
        public List<StringBuilder> ValueFieldsAndParam(T Model)
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
                sbparam.Append("@");
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
        public string Insert(T Model)
        {
            List<StringBuilder> lsb = ValueFieldsAndParam(Model);
            return String.Format(CreateSql.SqlInsert, Model.GetType().Name, lsb[0].ToString(), lsb[1].ToString());
        }

        #endregion

        #region 修改数据

        /// <summary>
        /// 返回更新的sql语句
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>添加</returns>
        public StringBuilder UpDateSql(T Model)
        {
            StringBuilder sb = new StringBuilder();
            Type _Type = Model.GetType();//反射返回类型
            foreach (PropertyInfo Info in _Type.GetProperties())
            {
                object obj = Info.GetValue(Model, null);//获取值
                if (obj == null) continue;
                sb.Append(Info.Name);
                sb.Append("=");
                sb.Append("@");
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
        public string Update(T Model, string Where)
        {
            return String.Format(CreateSql.SqlUpdate, Model.GetType().Name, UpDateSql(Model), Where);
        }

        #endregion

        #region 删除功能

        public string Delete(T Model, string Where)
        {
            return String.Format(CreateSql.SqlDelete, Model.GetType().Name, Where);
        }

        public string Delete(T Model)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = CreateWhere(Model, ref PageIndex, ref PageSzie);
            return String.Format(CreateSql.SqlDelete, Model.GetType().Name, Where);
        }

        #endregion
        #endregion
    }
}