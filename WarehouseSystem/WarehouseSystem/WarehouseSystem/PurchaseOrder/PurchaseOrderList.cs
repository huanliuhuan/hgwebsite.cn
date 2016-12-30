using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WarehouseSystem.PurchaseOrder
{
    public partial class PurchaseOrderList : UserControl
    {
           /// <summary>
        /// 本控件缓存
        /// </summary>
        private volatile static PurchaseOrderList _instance = null;
        //锁对象
        private static readonly object lockHelper = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        private PurchaseOrderList()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        public static PurchaseOrderList CreatePurchaseOrderList()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new PurchaseOrderList();
                }
            }
            return _instance;
        }
    }
}
