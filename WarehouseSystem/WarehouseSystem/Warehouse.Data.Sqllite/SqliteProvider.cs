using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Data.Sqllite
{
    /// <summary>
    /// sqlite提供类
    /// </summary>
    public class SqliteProvider
    {
        #region 连接字符串

        /// <summary>
        /// 连接字符串
        /// </summary>
        private const string ConStr = "Data Source={0};Compress=True;Cache Size=3000;New=True";
        /// <summary>
        /// 连接字符串
        /// </summary>
        private SQLiteConnection _ConnString;

        //解密
        SelfSafety ss = new SelfSafety();



        #endregion

        #region 构造函数

        /// <summary>
        /// 不带参数的构造函数
        /// </summary>
        public SqliteProvider()
        {
            string MyPath = System.AppDomain.CurrentDomain.BaseDirectory + "SqliteDB";
            if (!Directory.Exists(MyPath))
                Directory.CreateDirectory(MyPath); //如果文件夹不存在创建文件夹
            MyPath = Path.Combine(MyPath, "Async.db");
            _ConnString = new SQLiteConnection(String.Format(ConStr, MyPath));
        }

        /// <summary>
        /// 不带参数的构造函数
        /// </summary>
        public SqliteProvider(string BaseName)
        {
            string MyPath = System.AppDomain.CurrentDomain.BaseDirectory + "SqliteDB";
            if (!Directory.Exists(MyPath))
                Directory.CreateDirectory(MyPath); //如果文件夹不存在创建文件夹
            MyPath = Path.Combine(MyPath, BaseName);
            _ConnString = new SQLiteConnection(String.Format(ConStr, MyPath));
        }

        /// <summary>
        /// 自己配置路径
        /// </summary>
        /// <param name="BasePath">文件夹路径</param>
        /// <param name="BaseName">文件名称</param>
        public SqliteProvider(string BasePath, string BaseName)
        {
            if (!Directory.Exists(BasePath))
                Directory.CreateDirectory(BasePath); //如果文件夹不存在创建文件夹
            string MyPath = Path.Combine(BasePath, BaseName);
            _ConnString = new SQLiteConnection(String.Format(ConStr, MyPath));
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 切换数据库
        /// </summary>
        /// <param name="BaseName"></param>
        public void ChangeDataBase(string BaseName)
        {
            _ConnString.ChangeDatabase(BaseName);
        }

        /// <summary>
        ///创建参数
        /// </summary>
        /// <param name="parameterName">参数的名字.</param>
        /// <param name="parameterType">参数的类型.</param>
        /// <param name="parameterValue">参数的值.</param>
        /// <returns>返回Sqllite参数</returns>
        public SQLiteParameter CreateParameter(string parameterName, System.Data.DbType parameterType, object parameterValue)
        {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.DbType = parameterType;
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            return parameter;
        }

        /// <summary>
        /// 创建命令
        /// </summary>
        /// <param name="CmdText">执行sql语句</param>
        /// <param name="lParams">参数集合</param>
        /// <returns>返回命令</returns>
        private SQLiteCommand CreateCommand(string CmdText, params SQLiteParameter[] lParams)
        {
            SQLiteCommand Cmd = new SQLiteCommand(_ConnString);
            Cmd.Parameters.Clear();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = CmdText;
            Cmd.CommandTimeout = 100;
            //参数不为空的时候
            if (lParams != null)
            {
                foreach (SQLiteParameter item in lParams)
                {
                    Cmd.Parameters.Add(item);
                }
            }
            return Cmd;
        }

        #endregion

        #region 通讯方法

        /// <summary>
        /// 返回结果集中的第一行第一列，忽略其他行或列
        /// </summary>
        /// <param name="CmdText">Sql</param>
        /// <param name="lParams">传入的参数</param>
        /// <returns>返回第一行第一列的值</returns>
        public object ExecuteScalar(string CmdText, params SQLiteParameter[] lParams)
        {
            try
            {

                string MyPwd = ConfigurationSettings.AppSettings["litepwd"] == null ? "" : ss.Decrypto(ConfigurationSettings.AppSettings["litepwd"]);
                _ConnString.SetPassword(MyPwd);

                _ConnString.Open();//打开连接字符串
                SQLiteCommand cmd = CreateCommand(CmdText, lParams);//创建命令
                object obj = cmd.ExecuteScalar();
                _ConnString.Close();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="CmdText">sql语句</param>
        /// <param name="lParams">传入的参数</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQuery(string CmdText, params SQLiteParameter[] lParams)
        {
            try
            {

                string MyPwd = ConfigurationSettings.AppSettings["litepwd"] == null ? "" : ss.Decrypto(ConfigurationSettings.AppSettings["litepwd"]);
                if (MyPwd != "")
                {
                    _ConnString.SetPassword(MyPwd);
                }
                _ConnString.Open();//打开连接字符串
                SQLiteCommand cmd = CreateCommand(CmdText, lParams);//创建命令
                int obj = cmd.ExecuteNonQuery();
                _ConnString.Close();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <param name="CmdText">sql语句</param>
        /// <param name="lParams">传入参数</param>
        /// <returns>返回查询的数据集</returns>
        public DataSet ExecuteDataset(string CmdText, params SQLiteParameter[] lParams)
        {
            try
            {
                string MyPwd = ConfigurationSettings.AppSettings["litepwd"] == null ? "" : ss.Decrypto(ConfigurationSettings.AppSettings["litepwd"]);
                _ConnString.SetPassword(MyPwd);

                _ConnString.Open();
                SQLiteCommand cmd = CreateCommand(CmdText, lParams);//创建命令
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                _ConnString.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回SqlDataReader对象
        /// </summary>
        /// <param name="CmdText"></param>
        /// <param name="lParams">传入的参数</param>
        /// <returns>返回DataReader</returns>
        public SQLiteDataReader ExecuteReader(string CmdText, params SQLiteParameter[] lParams)
        {
            try
            {

                string MyPwd = ConfigurationSettings.AppSettings["litepwd"] == null ? "" : ss.Decrypto(ConfigurationSettings.AppSettings["litepwd"]);
                _ConnString.SetPassword(MyPwd);

                _ConnString.Open();
                SQLiteCommand cmd = CreateCommand(CmdText, lParams);//创建命令
                SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_ConnString.State != ConnectionState.Closed)
                    _ConnString.Close();//如果不是关闭将连接字符串关闭
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过事务返回第一行第一列的值
        /// </summary>
        /// <param name="CmdTexts">sql语句集合</param>
        /// <param name="llParams">参数集合</param>
        /// <returns>返回第一行第一列的值</returns>
        public object TranExecuteScalar(List<string> CmdTexts, List<SQLiteParameter[]> llParams)
        {

            string MyPwd = ConfigurationSettings.AppSettings["litepwd"] == null ? "" : ss.Decrypto(ConfigurationSettings.AppSettings["litepwd"]);
            _ConnString.SetPassword(MyPwd);

            if (_ConnString.State != ConnectionState.Open) _ConnString.Open();
            SQLiteTransaction tran = _ConnString.BeginTransaction();
            try
            {
                object result = 0;
                if (tran.Connection.State != ConnectionState.Open) tran.Connection.Open();//打开连接字符串
                IDbCommand Cmd = tran.Connection.CreateCommand();//创建命令
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.Text;
                foreach (var item in CmdTexts)
                {
                    if (item == null || string.IsNullOrEmpty(item)) continue;
                    Cmd.CommandText = item;
                    Cmd.CommandTimeout = 100;
                    if (llParams != null)
                    {
                        if (CmdTexts.Count.Equals(llParams.Count))
                        {
                            Cmd.Parameters.Clear();
                            foreach (var myparams in llParams[CmdTexts.IndexOf(item)])
                            {
                                if (myparams != null)
                                {
                                    Cmd.Parameters.Add(myparams);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("参数对象的个数和sql语句的个数不相同！");
                        }
                    }
                    result = Cmd.ExecuteScalar();
                }
                tran.Commit();//提交事务
                return result;
            }
            catch (Exception ex)
            {
                tran.Rollback();//事务回滚
                throw ex;
            }
        }

        /// <summary>
        /// 通过事务返回受影响行数
        /// </summary>
        /// <param name="CmdTexts">sql语句集合</param>
        /// <param name="llParams">参数集合</param>
        /// <returns>返回受影响行数</returns>
        public int TranExecuteNonQuery(List<string> CmdTexts, List<SQLiteParameter[]> llParams)
        {

            string MyPwd = ConfigurationSettings.AppSettings["litepwd"] == null ? "" : ss.Decrypto(ConfigurationSettings.AppSettings["litepwd"]);
            _ConnString.SetPassword(MyPwd);

            if (_ConnString.State != ConnectionState.Open) _ConnString.Open();
            SQLiteTransaction tran = _ConnString.BeginTransaction();
            try
            {
                int result = 0;
                if (tran.Connection.State != ConnectionState.Open) tran.Connection.Open();//打开连接字符串

                IDbCommand Cmd = tran.Connection.CreateCommand();//创建命令
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.Text;
                foreach (var item in CmdTexts)
                {
                    if (item == null || string.IsNullOrEmpty(item)) continue;
                    Cmd.CommandText = item;
                    Cmd.CommandTimeout = 100;
                    if (llParams != null)
                    {
                        if (CmdTexts.Count.Equals(llParams.Count))
                        {
                            Cmd.Parameters.Clear();
                            foreach (var myparams in llParams[CmdTexts.IndexOf(item)])
                            {
                                if (myparams != null)
                                {
                                    Cmd.Parameters.Add(myparams);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("参数对象的个数和sql语句的个数不相同！");
                        }
                    }
                    result += Cmd.ExecuteNonQuery();
                }
                tran.Commit();//提交事务
                return result;
            }
            catch (Exception ex)
            {
                tran.Rollback();//事务回滚
                throw ex;
            }
        }

        /// <summary>
        /// 通过事务返回数据集
        /// </summary>
        /// <param name="CmdText">sql语句</param>
        /// <param name="lParams">传入参数</param>
        /// <returns>返回查询的数据集</returns>
        public DataSet TranExecuteDataset(string CmdText, params SQLiteParameter[] lParams)
        {

            string MyPwd = ConfigurationSettings.AppSettings["litepwd"] == null ? "" : ss.Decrypto(ConfigurationSettings.AppSettings["litepwd"]);
            _ConnString.SetPassword(MyPwd);

            if (_ConnString.State != ConnectionState.Open) _ConnString.Open();
            SQLiteTransaction tran = _ConnString.BeginTransaction();
            try
            {
                DataSet ds = new DataSet();
                if (tran.Connection.State != ConnectionState.Open) tran.Connection.Open();//打开连接字符串
                SQLiteCommand Cmd = tran.Connection.CreateCommand();//创建命令
                Cmd.Parameters.Clear();
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = CmdText;
                Cmd.CommandTimeout = 100;
                if (lParams != null)
                {
                    Cmd.Parameters.Add(lParams);
                }
                SQLiteDataAdapter da = new SQLiteDataAdapter(Cmd);
                da.Fill(ds);
                tran.Commit();//提交事务
                return ds;
            }
            catch (Exception ex)
            {
                tran.Rollback();//事务回滚
                throw ex;
            }
        }


        #endregion
    }
}
