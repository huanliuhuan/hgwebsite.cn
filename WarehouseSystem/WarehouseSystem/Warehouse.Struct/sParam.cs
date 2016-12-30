using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Enum;

namespace Warehouse.Struct
{
    /// <summary>
    /// 参数结构传输形式
    /// </summary>
    public struct sParam
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        private string _ParamName;
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParamName
        {
            get { return _ParamName; }
            set { _ParamName = value; }
        }

        /// <summary>
        /// 参数类型
        /// </summary>
        private string _ParamType;
        /// <summary>
        /// 参数类型
        /// </summary>
        public string ParamType
        {
            get { return _ParamType; }
            set { _ParamType = value; }
        }

        /// <summary>
        /// 参数大小
        /// </summary>
        private int _ParamSize;
        /// <summary>
        /// 参数大小
        /// </summary>
        public int ParamSize
        {
            get { return _ParamSize; }
            set { _ParamSize = value; }
        }

        /// <summary>
        /// 参数的数值
        /// </summary>
        private object _ParamValue;
        /// <summary>
        /// 参数数值
        /// </summary>
        public object ParamValue
        {
            get { return _ParamValue; }
            set { _ParamValue = value; }
        }

        /// <summary>
        /// 参数方向
        /// </summary>
        private eParamDirection _Direction;
        /// <summary>
        /// 参数方向
        /// </summary>
        public eParamDirection Direction
        {
            get { return _Direction; }
            set { _Direction = value; }
        }
    }
}
