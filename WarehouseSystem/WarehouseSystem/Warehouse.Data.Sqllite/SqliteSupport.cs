using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Struct;

namespace Warehouse.Data.Sqllite
{
    /// <summary>
    /// sqlite辅助类
    /// </summary>
    public class SqliteSupport : AbsChangeParams<SQLiteParameter>
    {
        #region 实例化
        /// <summary>
        /// mysql提供类
        /// </summary>
        SqliteProvider provider;

        #endregion

        public SqliteSupport()
        {
            provider = new SqliteProvider();
        }

        public SqliteSupport(string BaseName)
        {
            provider = new SqliteProvider(BaseName);
        }

        #region 通讯方法

        /// <summary>
        /// 切换数据库名称
        /// </summary>
        /// <param name="BaseName">数据库名称</param>
        /// <returns>成功返回true失败返回false</returns>
        public bool ChangeDataBase(string BaseName)
        {
            provider.ChangeDataBase(BaseName);
            return true;
        }

        /// <summary>
        /// 给定连接的数据库用假设参数执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public int ExecuteNonQuery(int Type, string cmdText, List<sParam> commandParameters)
        {
            SQLiteParameter[] parms = null;
            if (commandParameters != null)
            {
                parms = ConvertToParameters(commandParameters.ToArray()).ToArray();
            }


            return provider.ExecuteNonQuery(cmdText, parms); //返回执行结果
        }

        /// <summary>
        /// 带事物的执行多天Sql语句返回受影响行数
        /// </summary>
        /// <param name="ProNames">sql语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回受影响行数</returns>
        public int TranExecuteNonQuery(List<string> ProNames, List<sParam[]> commandParameters)
        {
            List<SQLiteParameter[]> parms = null;
            if (commandParameters != null)
            {
                parms = ConvertToListParam(commandParameters);
            }
            return provider.TranExecuteNonQuery(ProNames, parms);
        }

        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的</returns>
        public object ExecuteScalar(int Type, string cmdText, List<sParam> commandParameters)
        {
            SQLiteParameter[] parms = null;
            if (commandParameters != null)
                parms = ConvertToParameters(commandParameters.ToArray()).ToArray();
            return provider.ExecuteScalar(cmdText, parms);
        }

        /// <summary>
        /// 带事物的执行多天Sql语句返回第一行第一列的值
        /// </summary>
        /// <param name="ProNames">sql语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回第一行第一列的值</returns>
        public object TranExecuteScalar(List<string> ProNames, List<sParam[]> commandParameters)
        {
            List<SQLiteParameter[]> parms = null;
            if (commandParameters != null)
                parms = ConvertToListParam(commandParameters);
            return provider.TranExecuteScalar(ProNames, parms);
        }

        /// <summary>
        /// 返回DataSet通过DataAdapter
        /// </summary>
        /// <param name="proName">存储过程名称或sql语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回DataSet</returns>
        public DataSet ReturnDataSetByDataAdapter(int Type, string proName, List<sParam> commandParameters)
        {
            SQLiteParameter[] parms = null;
            if (commandParameters != null)
                parms = ConvertToParameters(commandParameters.ToArray()).ToArray();
            return provider.ExecuteDataset(proName, parms);
        }

        /// <summary>
        /// 通过DataReader 读取数据
        /// </summary>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <param name="proName">类型（1=存储过程0=sql语句）</param>
        /// <param name="retValue">返回值(外部必须赋值,内部不一定赋值)—一般返回查询表的总行数</param>
        /// <param name="JsonStr">参数</param>
        /// <returns>返回DataSet</returns>
        public DataSet ReturnDataSetByDataReader(int Type, string proName, List<sParam> commandParameters)
        {
            SQLiteParameter[] parms = null;
            if (commandParameters != null)
                parms = ConvertToParameters(commandParameters.ToArray()).ToArray();
            return provider.TranExecuteDataset(proName, parms);
        }


        #endregion
    }
}
