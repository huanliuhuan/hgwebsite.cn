using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Runtime.Serialization;
using System.Collections.Specialized;
using MYHGTOOLS.sqls;
namespace MYHGTOOLS
{
    /// <summary>
    /// SqlServer提供类
    /// </summary>
    public class SqlServerProvider : IDisposable
    {
        #region IDisposable 成员

        /// <summary>
        /// 回收垃圾 
        /// </summary>
        public void Dispose()
        {
            GC.Collect();//垃圾回收
        }

        #endregion

        #region 定义私有参数或保护参数


        /// <summary>
        /// 定义连接字符串键值对
        /// </summary>
        private readonly string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        /// <summary>
        /// 定义链接字符串
        /// </summary>
        protected SqlConnection con;

        #endregion

        #region 定义构造函数

        /// <summary>
        /// 构造函数实例化DBheler类库(不带参数)
        /// </summary>
        public SqlServerProvider()
        {
            con = new SqlConnection(ConString);
        }

        /// <summary>
        /// 构造函数实例化DBheler类库(带参数)
        /// </summary>
        public SqlServerProvider(string DBKey)
        {
            ConString = ConfigurationManager.ConnectionStrings[DBKey].ConnectionString;
            con = new SqlConnection(ConString);
        }

        #endregion

        #region 定义析构函数

        /// <summary>
        /// 析构函数用于回收资源
        /// </summary>
        ~SqlServerProvider()
        {
            try
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            catch
            {
                GC.Collect();//强制进行垃圾回收
            }
        }

        #endregion

        #region 定义公共方法
        /// <summary>
        /// 反射的方法给泛型赋值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dr">DataReader</param>
        /// <returns>返回泛型</returns>
        private T ExecuteReader<T>(SqlDataReader dr)
        {
            T obj = default(T);
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            int columnCount = dr.FieldCount;
            obj = Activator.CreateInstance<T>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    string columnName = dr.GetName(i);
                    if (string.Compare(columnName, propertyInfo.Name, true) == 0)
                    {
                        object value = dr.GetValue(i);
                        if (value != null && value != DBNull.Value)
                        {
                            propertyInfo.SetValue(obj, value, null);
                        }
                        break;
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// 打开方法
        /// </summary>
        private void Open()
        {
            if (con == null)
            {
                con = new SqlConnection(ConString);
            }
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
        }

        /// <summary>
        /// 关闭方法
        /// </summary>
        private void Close()
        {
            if (con != null)
            {
                con.Dispose();
                con = null;
            }
        }

        /// <summary>
        /// 创建SqlDataAdapter
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回SqlDataAdapter</returns>
        private SqlDataAdapter CreateDataAdapter(string ProName, SqlParameter[] Param, int Type)
        {
            //打开数据链接
            Open();
            //定义适配器
            SqlDataAdapter da = new SqlDataAdapter(ProName, con);
            //判断用存储过程还是sql命令
            if (Type == 1)
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                da.SelectCommand.CommandType = CommandType.Text;
            }
            //相适配器中添加参数
            if (Param != null)
            {
                foreach (SqlParameter p in Param)
                {
                    da.SelectCommand.Parameters.Add(p);
                }
            }
            //添加返回参数
            return da;
        }

        /// <summary>
        /// 定义sqlCommand对象
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回sqlCommand对象</returns>
        private SqlCommand CreateSqlCommand(string ProName, SqlParameter[] param, int Type)
        {
            //打开字符串
            Open();
            //定义命令对象
            SqlCommand cmd = new SqlCommand(ProName, con);
            if (Type == 1)
            { cmd.CommandType = CommandType.StoredProcedure; }
            else
            { cmd.CommandType = CommandType.Text; }
            //添加参数
            if (param != null)
            {
                foreach (SqlParameter p in param)
                {
                    if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && p.Value == null)
                    { p.Value = DBNull.Value; }
                    cmd.Parameters.Add(p);
                }
            }
            //添加返回参数
            return cmd;
        }

        /// <summary>
        /// 定义DataReader
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回DataReader</returns>
        private SqlDataReader CreateDataReader(string ProName, SqlParameter[] param, int Type,ref object obj)
        {
            //打开链接
            Open();
            SqlCommand cmd = CreateSqlCommand(ProName, param, Type);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            //获取返回参数
            if (param != null)
                foreach (SqlParameter p in param)
                {
                    if (p.Direction == ParameterDirection.Output)
                    {
                        obj = cmd.Parameters[p.ParameterName].Value;
                    }
                }
            
            return dr;
        }

        #endregion

        #region 方法转换类

        /// <summary>
        /// DataSet和DataReader转换
        /// </summary>
        /// <param name="DataReader">读取数据</param>
        /// <returns>返回数据集</returns>
        public DataSet ReaderTable(SqlDataReader DataReader)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //循环dataReader/动态添加表的数据列
            for (int i = 0; i < DataReader.FieldCount; i++)
            {
                DataColumn dc = new DataColumn();
                dc.DataType = DataReader.GetFieldType(i);
                dc.ColumnName = DataReader.GetName(i);
                dt.Columns.Add(dc);
            }
            //向表里面添加数据
            while (DataReader.Read())
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < DataReader.FieldCount; j++)
                {
                    dr[j] = DataReader[j].ToString();
                }
                dt.Rows.Add(dr);
            }
            //关闭dataReader读取器
            DataReader.Close();
            ds.Tables.Add(dt);
            return ds;
        }

        #endregion

        #region 数据通讯方法

        /// <summary>
        /// 切换数据库名称
        /// </summary>
        /// <param name="BaseName">数据库名称</param>
        /// <returns>成功返回true失败返回false</returns>
        public bool ChangeDataBase(string BaseName)
        {
            bool b = false;
            try
            {
                if (con != null)
                {
                    Open();
                    con.ChangeDatabase(BaseName); //转换数据库名称
                    b = true;
                    Close();
                }
            }
            catch(Exception ex)
            {
                b = false;
                throw ex;
            }
            return b;
        }

        /// <summary>
        /// 受影响行数
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQuery(string proName, int Type, params SqlParameter[] Param)
        {
            //打开数据库
            Open();
            int num = 0;
            //命令对象
            SqlCommand cmd = CreateSqlCommand(proName, Param, Type);
            try
            {
                num = cmd.ExecuteNonQuery();//返回收影响行数
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                GC.Collect();//垃圾回收
                throw ex;
            }
            finally {

                //关闭数据库
                Close();
            }
            return num;
        }

        /// <summary>
        /// 执行多条sql语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns>受影响行数</returns>
        public int TranExecuteNonQuerys(List<string> Sqls, List<SqlParameter[]> Prams)
        {
            using (con)
            {
                int cnt = 0;
                Open();
                SqlCommand cmd = new SqlCommand(); //定义命令对象
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                SqlTransaction sqlaTran = con.BeginTransaction(); //定义事务
                cmd.Transaction = sqlaTran;
                try
                {
                    for (int i = 0; i < Sqls.Count; i++)
                    {
                        string ExcuSql = Sqls[i].Trim();

                        if (ExcuSql == null || ExcuSql.Length <= 0) continue;
                        cmd.CommandText = ExcuSql; //执行的sql语句
                        if (Prams != null)//如果不等于空循环
                        {
                            if (Sqls.Count.Equals(Prams.Count))
                            {
                                cmd.Parameters.Clear();
                                foreach (SqlParameter item in Prams[i])
                                {
                                    if (item != null)
                                    {
                                        cmd.Parameters.Add(item);
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("参数对象的个数和sql语句的个数不相同！");
                            }
                        }
                        cnt += cmd.ExecuteNonQuery();
                    }
                    sqlaTran.Commit();
                    return cnt;
                }
                catch(Exception ex)
                {
                    sqlaTran.Rollback();
                    return 0;
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    Close();
                }

            }
        }


        /// <summary>
        /// 最后第一行第一列的值
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回最后第一行第一列的值</returns>
        public object ExecuteScalar(string proName, int Type, params SqlParameter[] Param)
        {
            //打开数据库
            Open();
            //定义命令对象
            SqlCommand cmd = CreateSqlCommand(proName, Param, Type);
            //返回对象
            object obj = null;
            try
            {
                obj = cmd.ExecuteScalar();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                GC.Collect();
                throw ex;
            }
            finally
            {
                Close();
            }
            return obj;
        }

        /// <summary>
        /// 最后第一行第一列的值
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回最后第一行第一列的值</returns>
        public object TranExecuteScalar(List<string> Sqls, List<SqlParameter[]> Prams)
        {
            using (con)
            {
                object cnt = 0;
                Open();
                SqlCommand cmd = new SqlCommand(); //定义命令对象
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                SqlTransaction sqlaTran = con.BeginTransaction(); //定义事务
                cmd.Transaction = sqlaTran;
                try
                {
                    for (int i = 0; i < Sqls.Count; i++)
                    {
                        string ExcuSql = Sqls[i].Trim();
                        if (ExcuSql == null || ExcuSql.Length <= 0) continue;
                        cmd.CommandText = ExcuSql; //执行的sql语句
                        if (Prams != null)
                        {
                            if (Sqls.Count.Equals(Prams.Count))
                            {
                                cmd.Parameters.Clear();
                                foreach (SqlParameter item in Prams[i])
                                {
                                    if (item != null)
                                    {
                                        cmd.Parameters.Add(item);
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("参数对象的个数和sql语句的个数不相同！");
                            }
                        }
                        cnt = cmd.ExecuteScalar();
                    }
                    sqlaTran.Commit();
                    return cnt;
                }
                catch(Exception ex)
                {
                    sqlaTran.Rollback();
                    return 0;
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    Close();
                }

            }
        }

        /// <summary>
        /// 返回DataSet通过DataAdapter
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="tableName">表名</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <param name="retValue">返回值(外部必须赋值,内部不一定赋值)—一般返回查询表的总行数</param>
        /// <returns>返回DataSet</returns>
        public DataSet ReturnDataSetByDataAdapter(string proName, string tableName, int Type, ref object retValue, params SqlParameter[] Param)
        {
            //打开数据库
            Open();
            //定义Command命令
            try
            {
                SqlCommand cmd = CreateSqlCommand(proName, Param, Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                int num = da.Fill(ds, tableName);

                //获取返回参数
                if (Param != null)
                    foreach (SqlParameter p in Param)
                    {
                        if (p.Direction == ParameterDirection.Output)
                        {
                            retValue = cmd.Parameters[p.ParameterName].Value;
                        }
                    }
                Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 返回DataSet通过DataAdapter
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <param name="retValue">返回值(外部必须赋值,内部不一定赋值)—一般返回查询表的总行数</param>
        /// <returns>返回DataSet</returns>
        public DataSet ReturnDataSetByDataAdapter(string proName, int Type, ref object retValue, params SqlParameter[] Param)
        {
            try
            {
                //打开数据库
                Open();
                //定义Command命令
                SqlCommand cmd = CreateSqlCommand(proName, Param, Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                int num = da.Fill(ds);

                //获取返回参数
                if (Param != null)
                    foreach (SqlParameter p in Param)
                    {
                        if (p.Direction == ParameterDirection.Output)
                        {
                            retValue = cmd.Parameters[p.ParameterName].Value;
                        }
                    }
                Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 通过DataReader返回数据集
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="obj">返回参数</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>通过DataReader返回数据集</returns>
        public DataSet ReturnDataSetByDataReader(string ProName, int Type, ref object obj, params SqlParameter[] param)
        {
            SqlDataReader ReaderResult = CreateDataReader(ProName, param, Type, ref obj);
            try
            {
                DataSet ds = ReaderTable(ReaderResult);
                ReaderResult.Dispose();//释放DataReader
                Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ReaderResult.Dispose();
            }
        }

        /// <summary>
        /// 分页重载函数，返回当前页
        /// </summary>
        /// <param name="ProName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="TableName">表名</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageSize">页面大小</param>
        /// <returns>分页重载函数，返回当前页</returns>
        public DataSet PageDataSet(string ProName, string TableName, int Type, int CurrentPage, int PageSize, params SqlParameter[] Param)
        {
            try
            {
                //打开数据库
                Open();
                //定义命令
                SqlCommand cmd = CreateSqlCommand(ProName, Param, Type);
                DataSet ds = new DataSet();//实例化DataSet
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, CurrentPage, PageSize, TableName);
                Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 执行DataReader
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="ProName">sql语句或存储过程名称</param>
        /// <param name="Param">参数</param>
        /// <param name="Type">sql语句还是存储过程</param>
        /// <param name="obj">返回参数</param>
        /// <returns>List泛型对象</returns>
        public List<T> ReaderList<T>(string ProName, SqlParameter[] Param, int Type,ref object obj)
        {
            try
            {
                List<T> list = new List<T>();//实例化List对象

                //打开数据库
                Open();
                //定义命令
                SqlCommand cmd = CreateSqlCommand(ProName, Param, Type);
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        T item = ExecuteReader<T>(dr);
                        list.Add(item);
                    }
                }

                //获取返回参数
                if (Param != null)
                    foreach (SqlParameter p in Param)
                    {
                        if (p.Direction == ParameterDirection.Output)
                        {
                            obj = cmd.Parameters[p.ParameterName].Value;
                        }
                    }


                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        #endregion

        #region 维护数据库

        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        public bool ColumnExists(string tableName, string columnName)
        {
            try
            {
                string sql = String.Format(Sql.ColumnExists, tableName, columnName);
                object res = ExecuteScalar(sql, 0, null);
                if (res == null)
                {
                    return false;
                }
                return Convert.ToInt32(res) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns>是返回true否则返回false</returns>
        public bool TabExists(string TableName)
        {
            try
            {
                string strsql = String.Format(Sql.TableExists, TableName);
                object obj = ExecuteScalar(strsql, 0, null);
                int cmdresult;
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = int.Parse(obj.ToString());
                }
                if (cmdresult == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回存储过程参数
        /// </summary>
        /// <param name="ProcName">sql语句</param>
        /// <param name="tableName">表名</param>
        /// <param name="Framework">架构</param>
        /// <returns>存储过程参数数据集</returns>
        public DataSet GetProcParams(string ProcName, string tableName, string Framework = "dbo")
        {
            try
            {
                object TotleCount = 0;
                string strsql = String.Format(Sql.GetProcParams, ProcName, Framework);
                return ReturnDataSetByDataAdapter(strsql, tableName, 0, ref TotleCount, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="DataBaseName">数据库名称</param>
        /// <param name="SqlPath">数据库路径</param>
        /// <returns>执行结果（true执行成功，false执行失败）</returns>
        public bool BACKUP(string DataBaseName, string SqlPath)
        {
            try
            {
                ExecuteScalar(String.Format(Sql.backup, DataBaseName, SqlPath), 0, null);
                return true;//执行完成
            }
            catch(Exception ex)
            {
                GC.Collect();
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// 恢复数据库
        /// </summary>
        /// <param name="DataBaseName">数据库名称</param>
        /// <param name="SqlPath">数据库路径</param>
        /// <returns>处理结果（true执行成功，false执行失败）</returns>
        public bool RESTORE(string DataBaseName, string SqlPath)
        {
            try
            {
                ExecuteScalar(String.Format(Sql.restore, DataBaseName, SqlPath), 0, null);
                return true;//执行完成
            }
            catch(Exception ex)
            {
                GC.Collect();
                return false;
                throw ex;
            }
        }

        #endregion

        #region 序列化的表

        /// <summary>
        /// 返回序列化的DataSet
        /// </summary>
        /// <param name="proName">存储过程名称或sql语句</param>
        /// <param name="Param">参数</param>
        /// <param name="tableName">表名</param>
        /// <param name="Type">类型（1=存储过程0=sql语句）</param>
        /// <returns>返回序列化的DataSet</returns>
        public object ReturnDataSetBySerializer(string proName, SqlParameter[] Param, string tableName, int Type, ref object retValue)
        {
            try
            {
                DataSet ds = ReturnDataSetByDataAdapter(proName, Type, ref retValue, Param);
                IFormatter formatter = new BinaryFormatter();//定义BinaryFormatter以序列化object对象  

                MemoryStream ms = new MemoryStream();//创建内存流对象  

                formatter.Serialize(ms, ds);//把object对象序列化到内存流  

                byte[] buffer = ms.ToArray();//把内存流对象写入字节数组  

                ms.Close();//关闭内存流对象  

                ms.Dispose();//释放资源  

                MemoryStream msNew = new MemoryStream();

                GZipStream gzipStream = new GZipStream(msNew, CompressionMode.Compress, true);//创建压缩对象  

                gzipStream.Write(buffer, 0, buffer.Length);//把压缩后的数据写入文件  

                gzipStream.Close();//关闭压缩流,这里要注意：一定要关闭，要不然解压缩的时候会出现小于4K的文件读取不到数据，大于4K的文件读取不完整              

                gzipStream.Dispose();//释放对象  

                msNew.Close();

                msNew.Dispose();

                return msNew.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }

        }

        #endregion

        
    }
}
