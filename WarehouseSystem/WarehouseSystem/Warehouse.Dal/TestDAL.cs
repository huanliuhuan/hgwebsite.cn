using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Facroty;
using System.Data;
using Warehouse.Model;
using MYHGTOOLS;
using System.Data.SqlClient;

namespace Warehouse.Dal
{
    /// <summary>
    /// 创建：刘欢
    /// 时间：2015年5月25日 22:01:52
    /// 说明：测试
    /// </summary>
    public class TestDAL
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
        private SQLProtectOperation<Test> userSql = new SQLProtectOperation<Test>();

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
                DataTable dt = sqlRead.GetTableSet<Test>(new Test());
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
        public int AddData(Test testM)
        {
            try
            {
                int result = sqlWrite.AddData<Test>(testM);
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
        public int UpdateDataByWhere(Test testM, string where)
        {
            try
            {
                List<SqlParameter> para = null;
                string sql = userSql.UpdateToModelSql(testM, where, ref para);
                int result = sqlWrite.ExecuteNonQuery(sql,para);
                return result;
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
                int result = sqlWrite.RemData<Test>(new Test() { ID = id.ToString() });
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
