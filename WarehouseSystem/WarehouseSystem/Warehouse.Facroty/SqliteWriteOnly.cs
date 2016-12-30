using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Commnon;
using Warehouse.Data.Sqllite;
using Warehouse.Struct;

namespace Warehouse.Facroty
{
    public class SqliteWriteOnly
    {
        /// <summary>
        /// 实例化只读数据库
        /// </summary>
        SqliteSupport Helper = new SqliteSupport("MemberDB");



        #region 辅助方法

        /// <summary>
        /// 处理键值
        /// </summary>
        /// <param name="KeyDB">键值</param>
        /// <param name="strKey">返回参数</param>
        /// <returns>是否包含（true=包含，false=不包含）</returns>
        private bool ContainKey(ref string strKey, params string[] KeyDB)
        {
            try
            {
                if (KeyDB == null) return false;
                //初始化
                strKey = String.Empty; //键值
                bool b = false;
                if (KeyDB.Length > 0)
                {
                    foreach (string item in KeyDB)
                    {
                        strKey = item;
                    }
                    b = true;
                }
                else
                {
                    b = false;
                }
                return b;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region HttpLinkDataBase通信方法

        /// <summary>
        /// 切换数据库名称
        /// </summary>
        /// <param name="BaseName">数据库名称</param>
        /// <returns>成功返回true失败返回false</returns>
        public bool ChangeDataBase(string BaseName)
        {
            return Helper.ChangeDataBase(BaseName);
        }

        #region 添加部分

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>返回受影响行数</returns>
        public int AddData<T>(T Model) where T : new()
        {
            try
            {

                List<sParam> listparam = EntityHandler<T>.ConvertToparam(Model);
                int result = Helper.ExecuteNonQuery(0, SqliteSqlCreate<T>.Insert(Model), listparam);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加数据（同步下载数据时使用）
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>返回受影响行数</returns>
        public int AddDataSync<T>(T Model) where T : new()
        {
            try
            {

                List<sParam> listparam = EntityHandler<T>.ConvertToparam(Model);
                int result = Helper.ExecuteNonQuery(0, SqliteSqlCreate<T>.InsertSync(Model), listparam);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="lists">字符串对象</param>
        /// <returns>返回受影响行数</returns>
        public int TranAddData(List<string> lists)
        {
            try
            {
                int result = Helper.TranExecuteNonQuery(lists, null);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="lists">字符串对象</param>
        /// <returns>返回受影响行数</returns>
        public int TranAddData(List<string> lists, List<sParam[]> valuesList)
        {
            try
            {
                int result = Helper.TranExecuteNonQuery(lists, valuesList);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 修改部分

        /// <summary>
        /// 修改部分
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="Where">条件</param>
        /// <returns>返回受影响行数</returns>
        public int ModData<T>(T Model, string Where) where T : new()
        {
            try
            {
                List<sParam> listparam = EntityHandler<T>.ConvertToparam(Model);
                int result = Helper.ExecuteNonQuery(0, SqliteSqlCreate<T>.Update(Model, Where), listparam);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 删除部分

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>返回受影响行数</returns>
        public int RemData<T>(T Model) where T : new()
        {
            try
            {
                List<sParam> listparam = EntityHandler<T>.ConvertToparam(Model);
                int result = Helper.ExecuteNonQuery(0, SqliteSqlCreate<T>.Delete(Model), listparam);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="Where">条件</param>
        /// <returns>返回受影响行数</returns>
        public int RemData<T>(T Model, string Where) where T : new()
        {
            try
            {
                List<sParam> listparam = EntityHandler<T>.ConvertToparam(Model);
                int result = Helper.ExecuteNonQuery(0, SqliteSqlCreate<T>.Delete(Model, Where), listparam);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion


        #region 执行方法

        /// <summary>
        /// 执行函数返回受影响行数
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQuery(string Sql, List<SqlParameter> para)
        {
            try
            {
                int index = 0;
                SQLiteParameter[] sqlitepara = new SQLiteParameter[para.Count];
                foreach (var item in para)
                {
                    sqlitepara[index] = new SQLiteParameter() { ParameterName = item.ParameterName, Value = item.Value };
                    index++;
                }
                int result = DbHelperSQLite.ExecuteSql(Sql, sqlitepara);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行函数返回第一行第一列的值
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>返回受影响行数</returns>
        public object ExecuteScalar(string Sql)
        {
            try
            {
                object result = Helper.ExecuteScalar(0, Sql, null);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

    }
}
