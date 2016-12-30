using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Commnon;
using Warehouse.Data.Sqllite;
using Warehouse.Struct;

namespace Warehouse.Facroty
{
    public class SqliteReadOnly
    {
        /// <summary>
        /// 实例化只读数据库
        /// </summary>
        SqliteSupport helper = new SqliteSupport("MemberDB");


        #region 返回List实体

        /// <summary>
        /// 返回实体对象
        /// </summary>
        /// <returns>返回实体</returns>
        public List<T> GetEntitySet<T>(T Model) where T : new()
        {
            object obj = String.Empty;
            List<sParam> MyParams = EntityHandler<T>.ConvertToparam(Model);
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, SqliteSqlCreate<T>.Select(Model), MyParams);
            return EntityHandler<T>.FillModel(ds);
        }
        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="Where">条件</param>
        /// <param name="Model">实体</param>
        /// <returns>返回实体</returns>
        public List<T> GetEntitySet<T>(T Model, string Where) where T : new()
        {
            object obj = String.Empty;
            List<sParam> MyParams = EntityHandler<T>.ConvertToparam(Model);
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, SqliteSqlCreate<T>.Select(Model, Where), MyParams);
            return EntityHandler<T>.FillModel(ds);
        }
        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">正序OR倒叙</param>
        /// <param name="RecordCount">返回参数</param>
        /// <returns>返回实体</returns>
        public List<T> GetEntitySet<T>(T Model, string SortField, string Sort, ref int RecordCount) where T : new()
        {
            object obj = String.Empty;
            List<sParam> MyParams = EntityHandler<T>.ConvertToparam(Model);
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, SqliteSqlCreate<T>.SelectByPage(Model, SortField, Sort), MyParams);
            object rc = helper.ExecuteScalar(0, SqliteSqlCreate<T>.SelectByPage(Model), MyParams);
            RecordCount = Convert.ToInt32(rc);
            return EntityHandler<T>.FillModel(ds);
        }

        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">正序OR倒叙</param>
        /// <param name="RecordCount">返回参数</param>
        /// <param name="Where">条件</param>
        /// <returns>返回实体</returns>
        public List<T> GetEntitySet<T>(T Model, string SortField, string Sort, string Where, ref int RecordCount) where T : new()
        {
            object obj = String.Empty;
            List<sParam> MyParams = EntityHandler<T>.ConvertToparam(Model);
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, SqliteSqlCreate<T>.SelectByPage(Model, SortField, Sort, Where), MyParams);
            object rc = helper.ExecuteScalar(0, SqliteSqlCreate<T>.SelectByPage(Model, SortField, Sort, Where), MyParams);
            RecordCount = Convert.ToInt32(rc);
            return EntityHandler<T>.FillModel(ds);
        }

        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">正序OR倒叙</param>
        /// <param name="RecordCount">返回参数</param>
        /// <param name="GroupField">分组字段</param>
        /// <returns>返回实体</returns>
        public List<T> GetEntityGSet<T>(T Model, string GroupField, string SortField, string Sort, ref int RecordCount) where T : new()
        {
            object obj = String.Empty;
            List<sParam> MyParams = EntityHandler<T>.ConvertToparam(Model);
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, SqliteSqlCreate<T>.SelectByGPage(Model, GroupField, SortField, Sort), MyParams);
            object rc = helper.ExecuteScalar(0, SqliteSqlCreate<T>.SelectByGPage(Model, GroupField), MyParams);
            RecordCount = Convert.ToInt32(rc);
            return EntityHandler<T>.FillModel(ds);
        }

        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">正序OR倒叙</param>
        /// <param name="RecordCount">返回参数</param>
        /// <param name="GroupField">分组字段</param>
        /// <returns>返回实体</returns>
        public List<T> GetEntity<T>(string Sql, T Model, ref int RecordCount) where T : new()
        {
            object obj = String.Empty;
            List<sParam> MyParams = EntityHandler<T>.ConvertToparam(Model);
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, Sql, MyParams);
            object rc = helper.ExecuteScalar(0, Sql, MyParams);
            RecordCount = Convert.ToInt32(rc);
            return EntityHandler<T>.FillModel(ds);
        }




        #endregion

        #region 返回DataTable

        /// <summary>
        /// 返回实体对象
        /// </summary>
        /// <returns>返回实体</returns>
        public DataTable GetTableSet<T>(T Model) where T : new()
        {
            object obj = String.Empty;
            List<sParam> MyParams = EntityHandler<T>.ConvertToparam(Model);
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, SqliteSqlCreate<T>.Select(Model), MyParams);
            return ds.Tables[0];
        }
        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="Where">条件</param>
        /// <param name="Model">实体</param>
        /// <returns>返回实体</returns>
        public DataTable GetTableSet<T>(T Model, string Where) where T : new()
        {
            object obj = String.Empty;
            List<sParam> MyParams = EntityHandler<T>.ConvertToparam(Model);
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, SqliteSqlCreate<T>.Select(Model, Where), MyParams);
            return ds.Tables[0];
        }
        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">正序OR倒叙</param>
        /// <param name="RecordCount">返回参数</param>
        /// <returns>返回实体</returns>
        public DataTable GetTableSet<T>(T Model, string SortField, string Sort, ref int RecordCount) where T : new()
        {
            object obj = String.Empty;
            List<sParam> MyParams = EntityHandler<T>.ConvertToparam(Model);
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, SqliteSqlCreate<T>.SelectByPage(Model, SortField, Sort), MyParams);
            object rc = helper.ExecuteScalar(0, SqliteSqlCreate<T>.SelectByPage(Model), MyParams);
            RecordCount = Convert.ToInt32(rc);
            return ds.Tables[0];
        }

        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">正序OR倒叙</param>
        /// <param name="RecordCount">返回参数</param>
        /// <param name="Where">条件</param>
        /// <returns>返回实体</returns>
        public DataTable GetTableSet<T>(T Model, string SortField, string Sort, string Where, ref int RecordCount) where T : new()
        {
            object obj = String.Empty;
            List<sParam> MyParams = EntityHandler<T>.ConvertToparam(Model);
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, SqliteSqlCreate<T>.SelectByPage(Model, SortField, Sort, Where), MyParams);
            object rc = helper.ExecuteScalar(0, SqliteSqlCreate<T>.SelectByPage(Model, SortField, Sort, Where), MyParams);
            RecordCount = Convert.ToInt32(rc);
            return ds.Tables[0];
        }

        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">正序OR倒叙</param>
        /// <param name="RecordCount">返回参数</param>
        /// <param name="GroupField">分组字段</param>
        /// <returns>返回实体</returns>
        public DataTable GetDataGSet<T>(T Model, string GroupField, string SortField, string Sort, ref int RecordCount) where T : new()
        {
            object obj = String.Empty;
            List<sParam> MyParams = EntityHandler<T>.ConvertToparam(Model);
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, SqliteSqlCreate<T>.SelectByGPage(Model, GroupField, SortField, Sort), MyParams);
            object rc = helper.ExecuteScalar(0, SqliteSqlCreate<T>.SelectByGPage(Model, GroupField), MyParams);
            RecordCount = Convert.ToInt32(rc);
            return ds.Tables[0];
        }


        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">正序OR倒叙</param>
        /// <param name="RecordCount">返回参数</param>
        /// <param name="GroupField">分组字段</param>
        /// <returns>返回实体</returns>
        public DataTable GetTableSet(string sql, ref int RecordCount)
        {
            object obj = String.Empty;
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, sql, null);
            object rc = helper.ExecuteScalar(0, sql, null);
            RecordCount = Convert.ToInt32(rc);
            return ds.Tables[0];
        }

        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">正序OR倒叙</param>
        /// <param name="RecordCount">返回参数</param>
        /// <param name="GroupField">分组字段</param>
        /// <returns>返回实体</returns>
        public DataTable GetTableSet(string sql)
        {
            object obj = String.Empty;
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, sql, null);
            return ds.Tables[0];
        }

        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">正序OR倒叙</param>
        /// <param name="RecordCount">返回参数</param>
        /// <param name="GroupField">分组字段</param>
        /// <returns>返回实体</returns>
        public DataTable GetTableSet(string sql, List<sParam> para)
        {
            object obj = String.Empty;
            DataSet ds = helper.ReturnDataSetByDataAdapter(0, sql, para);
            return ds.Tables[0];
        }

        #endregion

        #region 执行方法

        /// <summary>
        /// 执行函数返回受影响行数
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQuery(string Sql)
        {
            try
            {

                int result = helper.ExecuteNonQuery(0, Sql, null);
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

                object result = helper.ExecuteScalar(0, Sql, null);
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
