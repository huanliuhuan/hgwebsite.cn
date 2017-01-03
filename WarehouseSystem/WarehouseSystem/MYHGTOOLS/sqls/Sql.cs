using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MYHGTOOLS.sqls
{
    /// <summary>
    /// 公共Sql语句的提供者
    /// </summary>
    public class Sql
    {
        #region SqlServerProvider公共方法部分

        /// <summary>
        /// 数据库中某表是否存在否列
        /// </summary>
        protected internal const string ColumnExists = "select count(1) from syscolumns where [id]=object_id('{0}') and [name]='{1}'";
        /// <summary>
        /// 判断某张表是否存在
        /// </summary>
        protected internal const string TableExists = "select count(*) from sysobjects where id = object_id(N'[{0}]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
        /// <summary>
        /// 获取某表中最大的ID值
        /// </summary>
        protected internal const string MaxID = "select max({0})+1 from   {1}";

        #endregion

        #region 获取存储过程参数

        /// <summary>
        ///  获取存储过程参数
        /// </summary>
        protected internal const string GetProcParams = "SELECT  param.name AS [Name],ISNULL(baset.name, N'') AS [SystemType],CAST(CASE WHEN baset.name IN (N'nchar', N'nvarchar') AND param.max_length <> -1 THEN param.max_length/2 ELSE param.max_length END AS int) AS [Length] FROM sys.all_objects AS sp INNER JOIN sys.all_parameters AS param ON param.object_id=sp.object_id LEFT OUTER JOIN sys.types AS baset ON baset.user_type_id = param.system_type_id and baset.user_type_id = baset.system_type_id WHERE (sp.type = N'P' OR sp.type = N'RF' OR sp.type='PC')and(sp.name= N'{0}' and SCHEMA_NAME(sp.schema_id)=N'{1}') ORDER BY param.parameter_id ASC";

        #endregion

        #region 维护部分数据库

        /// <summary>
        /// 备份数据库
        /// </summary>
        protected internal const string backup = "BACKUP DATABASE {0} TO DISK ='{1}'";

        /// <summary>
        /// 还原数据库
        /// </summary>
        protected internal const string restore = "use master RESTORE DATABASE {0} from disk='{1}'";

        #endregion
    }
}
