using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WarehouseSystem.CustomerFrom
{
    public partial class CustomerList : UserControl
    {
         /// <summary>
        /// 本控件缓存
        /// </summary>
        private volatile static CustomerList _instance = null;
        //锁对象
        private static readonly object lockHelper = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        private CustomerList()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        public static CustomerList CreateCustomerList()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new CustomerList();
                }
            }
            return _instance;
        }
    }
}
