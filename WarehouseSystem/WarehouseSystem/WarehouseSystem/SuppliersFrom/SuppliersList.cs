using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WarehouseSystem.SuppliersFrom
{
    public partial class SuppliersList : UserControl
    {
        /// <summary>
        /// 本控件缓存
        /// </summary>
        private volatile static SuppliersList _instance = null;
        //锁对象
        private static readonly object lockHelper = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        private SuppliersList()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        public static SuppliersList CreateSuppliersList()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new SuppliersList();
                }
            }
            return _instance;
        }

        private void SuppliersList_Load(object sender, EventArgs e)
        {

        }
    }
}
