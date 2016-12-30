using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Warehouse.Dal;
using Warehouse.Model;

namespace Warehouse.Bll
{
    /// <summary>
    /// 商品类型管理
    /// </summary>
    public class GoodsTypeBLL
    {
        #region 公共

        /// <summary>
        /// 测试数据访问层
        /// </summary>
        private GoodsTypeDAL teDal = new GoodsTypeDAL();

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
        public bool AddData(T_GoodsType_Tb testM)
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
        public bool UpdateDataByWhere(T_GoodsType_Tb testM, string where)
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
