using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WarehouseSystem.ShoopingFrom
{
    public partial class ShoopingMain : UserControl
    {
        /// <summary>
        /// 本控件缓存
        /// </summary>
        private volatile static ShoopingMain _instance = null;
        //锁对象
        private static readonly object lockHelper = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        private ShoopingMain()
        {

            InitializeComponent();

        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        public static ShoopingMain CreateShoopingMain()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ShoopingMain();
                }
            }
            return _instance;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShoopingTypeList stLFrom = new ShoopingTypeList();
            stLFrom.ShowDialog();
        }

    }
}
