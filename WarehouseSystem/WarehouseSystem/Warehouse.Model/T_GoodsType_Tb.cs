using System;
namespace Warehouse.Model
{
    /// <summary>
    /// GoodsTypeModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_GoodsType_Tb
    {
        public T_GoodsType_Tb()
        { }
        #region Model
        private string _gtid;
        private string _gtname;
        private string _backup;
        /// <summary>
        /// 
        /// </summary>
        public string GTID
        {
            set { _gtid = value; }
            get { return _gtid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GTName
        {
            set { _gtname = value; }
            get { return _gtname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BackUp
        {
            set { _backup = value; }
            get { return _backup; }
        }

        /// <summary>
        /// PID
        /// </summary>
        public string GTPID { get; set; }
        #endregion Model

    }
}

