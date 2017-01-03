using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MYHGTOOLS;
using Warehouse.Facroty;
using Warehouse.Model;

namespace Warehouse.Dal
{
    /// <summary>
    /// 商品类型
    /// </summary>
    public class GoodsTypeDAL
    {
        #region 公共

        /// <summary>
        /// 只读
        /// </summary>
        private SqliteReadOnly sqlRead = new SqliteReadOnly();
        /// <summary>
        /// 只写
        /// </summary>
        private SqliteWriteOnly sqlWrite = new SqliteWriteOnly();
        /// <summary>
        /// sql操作类
        /// </summary>
        private SQLProtectOperation<T_GoodsType_Tb> userSql = new SQLProtectOperation<T_GoodsType_Tb>();

        #endregion

        #region 方法

        /// <summary>
        /// 根据where查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable FindAllByWhere(string where)
        {
            try
            {
                DataTable dt = sqlRead.GetTableSet<T_GoodsType_Tb>(new T_GoodsType_Tb());
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="testM"></param>
        /// <returns></returns>
        public int AddData(T_GoodsType_Tb testM)
        {
            try
            {
                int result = sqlWrite.AddData<T_GoodsType_Tb>(testM);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="testM"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public int UpdateDataByWhere(T_GoodsType_Tb testM, string where)
        {
            try
            {
                //List<SqlParameter> para = null;
                //string sql = userSql.UpdateToModelSql(testM, where, ref para);
                //int result = sqlWrite.ExecuteNonQuery(sql, para);
                //return result;
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteData(int id)
        {
            try
            {
                int result = sqlWrite.RemData<T_GoodsType_Tb>(new T_GoodsType_Tb() { GTID = id.ToString() });
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
