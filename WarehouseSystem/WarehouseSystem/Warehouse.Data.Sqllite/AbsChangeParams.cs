using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Struct;

namespace Warehouse.Data.Sqllite
{
    /// <summary>
    /// 转换抽象类
    /// </summary>
    public abstract class AbsChangeParams<T> where T : new()
    {
        #region 转换部分

        /// <summary>
        /// 转化成参数
        /// </summary>
        /// <param name="param">参数结构</param>
        /// <returns>数据库参数</returns>
        public virtual List<T> ConvertToParameters(sParam[] param)
        {
            if (param == null) return new List<T>();
            List<T> myTs = new List<T>();//实例化list对象
            foreach (sParam item in param)
            {
                T ty = new T();// 实例化泛型
                Type MyTy = ty.GetType(); //反射
                if (String.IsNullOrEmpty(item.ParamName)) continue;
                MyTy.GetProperty("ParameterName").SetValue(ty, "@" + item.ParamName, null);
                if (!String.IsNullOrEmpty(item.ParamType))
                    MyTy.GetProperty("DbType").SetValue(ty, ConvertToDbType(item.ParamType), null);
                if (item.ParamSize != 0)
                    MyTy.GetProperty("Size").SetValue(ty, item.ParamSize, null);
                if (item.ParamValue != null)
                    MyTy.GetProperty("Value").SetValue(ty, item.ParamValue, null);



                myTs.Add(ty);//添加到集合
            }
            return myTs;
        }

        /// <summary>
        /// 转换List参数对象
        /// </summary>
        /// <param name="Params">参数组</param>
        /// <returns>返回对象</returns>
        public virtual List<T[]> ConvertToListParam(List<sParam[]> Params)
        {
            List<T[]> SqlParams = new List<T[]>();
            if (Params == null) return null;
            foreach (sParam[] item in Params)
            {
                SqlParams.Add(ConvertToParameters(item).ToArray());
            }
            return SqlParams;
        }

        /// <summary>
        /// C#语言类型与数据库参数类型转换
        /// </summary>
        /// <param name="propertype">DbType的字符串形式</param>
        /// <returns>返回字符串类型</returns>
        public virtual DbType ConvertToDbType(string propertype)
        {
            DbType T = new DbType();
            switch (propertype)
            {

                case "System.AnsiString": T = DbType.AnsiString; break;
                case "System.AnsiStringFixedLength": T = DbType.AnsiStringFixedLength; break;
                case "System.Buffer": T = DbType.Binary; break;
                case "System.Boolean": T = DbType.Boolean; break;
                case "System.Byte": T = DbType.Byte; break;
                case "System.Currency": T = DbType.Currency; break;
                case "System.Date": T = DbType.Date; break;
                case "System.DateTime": T = DbType.DateTime; break;
                case "System.DateTime2": T = DbType.DateTime2; break;
                case "System.DateTimeOffset": T = DbType.DateTimeOffset; break;
                case "System.Decimal": T = DbType.Decimal; break;
                case "System.Double": T = DbType.Double; break;
                case "System.Guid": T = DbType.Guid; break;
                case "System.Int16": T = DbType.Int16; break;
                case "System.Int32": T = DbType.Int32; break;
                case "System.Int64": T = DbType.Int64; break;
                case "System.Object": T = DbType.Object; break;
                case "System.SByte": T = DbType.SByte; break;
                case "System.Single": T = DbType.Single; break;
                case "System.String": T = DbType.String; break;
                case "System.StringFixedLength": T = DbType.StringFixedLength; break;
                case "System.Timers": T = DbType.Time; break;
                case "System.UInt16": T = DbType.UInt16; break;
                case "System.UInt32": T = DbType.UInt32; break;
                case "System.UInt64": T = DbType.UInt64; break;
                case "System.VarNumeric": T = DbType.VarNumeric; break;
                case "System.Xml": T = DbType.Xml; break;
                default: throw new Exception("此类型不支持");
            }
            return T;
        }

        /// <summary>
        /// 通过实例去返回参数
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>返回 参数</returns>
        public virtual IList<sParam> ConvertToparam(T Model)
        {
            IList<sParam> Listp = new List<sParam>();
            Type _Type = Model.GetType();
            foreach (PropertyInfo item in _Type.GetProperties())
            {
                object obj = item.GetValue(Model, null);
                if (obj == null) continue;
                sParam param = new sParam();
                param.ParamName = item.Name;
                param.ParamValue = obj;
                Listp.Add(param);
            }
            return Listp;
        }

        #endregion
    }
}
