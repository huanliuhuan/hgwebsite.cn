using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Dal;
using Warehouse.Model;

namespace Warehouse.Bll
{
    /// <summary>
    /// 创建：刘欢
    /// 时间：2015年5月25日 22:28:27
    /// 说明：逻辑访问层
    /// </summary>
    public class TestBLL
    {
        #region 公共

        /// <summary>
        /// 测试数据访问层
        /// </summary>
        private TestDAL teDal = new TestDAL();

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
                DataTable dt = teDal.FindAllByWhere(where);
                return dt;
            }
            catch { return null; }
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="testM"></param>
        /// <returns></returns>
        public bool AddData(Test testM)
        {
            try
            {
                int result = teDal.AddData(testM);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="testM"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool UpdateDataByWhere(Test testM, string where)
        {
            try
            {
                int result = teDal.UpdateDataByWhere(testM, where);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteData(int id)
        {
            try
            {
                int result = teDal.DeleteData(id);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }

        #endregion
    }
}
