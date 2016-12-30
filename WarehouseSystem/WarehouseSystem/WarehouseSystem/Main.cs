using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WarehouseSystem.CustomerFrom;
using WarehouseSystem.PurchaseOrder;
using WarehouseSystem.SalesFrom;
using WarehouseSystem.ShoopingFrom;
using WarehouseSystem.SuppliersFrom;

namespace WarehouseSystem
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);//自绘
            this.SetStyle(ControlStyles.DoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            //this.SetStyle(ControlStyles.Opaque, true);//如果为真，控件将绘制为不透明，不绘制背景
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);   //透明效果

            ShoopingMain sm = ShoopingMain.CreateShoopingMain();
            pal_DisplayContent.Controls.Add(sm);
            sm.Dock = DockStyle.Fill;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string file = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            skinUI1.SkinFile = file+"\\skins\\corona-CORONA12.skn";
            skinUI1.Active = true;
        }

        private void 商品管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShoopingMain MF = ShoopingMain.CreateShoopingMain();
            if (!pal_DisplayContent.Contains(MF))
            {
                pal_DisplayContent.Controls.Add(MF);
                MF.Dock = DockStyle.Fill;
            }
            MF.BringToFront();
        }

        private void 供应商管理GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuppliersList MF = SuppliersList.CreateSuppliersList();
            if (!pal_DisplayContent.Contains(MF))
            {
                pal_DisplayContent.Controls.Add(MF);
                MF.Dock = DockStyle.Fill;
            }
            MF.BringToFront();
        }

        private void 大客户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerList MF = CustomerList.CreateCustomerList();
            if (!pal_DisplayContent.Contains(MF))
            {
                pal_DisplayContent.Controls.Add(MF);
                MF.Dock = DockStyle.Fill;
            }
            MF.BringToFront();
        }

        private void 采购单管理RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PurchaseOrderList MF = PurchaseOrderList.CreatePurchaseOrderList();
            if (!pal_DisplayContent.Contains(MF))
            {
                pal_DisplayContent.Controls.Add(MF);
                MF.Dock = DockStyle.Fill;
            }
            MF.BringToFront();
        }

        private void 销售单管理FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesList MF = SalesList.CreateSalesList();
            if (!pal_DisplayContent.Contains(MF))
            {
                pal_DisplayContent.Controls.Add(MF);
                MF.Dock = DockStyle.Fill;
            }
            MF.BringToFront();
        }
    }
}
