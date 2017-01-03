using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Warehouse.Bll;
using System.Windows.Forms;

namespace WarehouseSystem.ShoopingFrom
{
    public partial class ShoopingTypeList : Form
    {
        public ShoopingTypeList()
        {
            InitializeComponent();
        }

        #region 公共变量

        /// <summary>
        /// 商品类型Bll
        /// </summary>
        private GoodsTypeBLL gtBll = new GoodsTypeBLL();

        #endregion

        private void ShoopingTypeList_Load(object sender, EventArgs e)
        {
            //string file = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //skinUI1.SkinFile = file + "\\skins\\corona-CORONA12.skn";
            //skinUI1.Active = true;
        }

        private void ShoopingTypeList_Shown(object sender, EventArgs e)
        {
            Font boldFont = new Font(treeGridView1.DefaultCellStyle.Font, FontStyle.Bold);

            DataTable dt = gtBll.FindAllByWhere("");
            Bind(dt, "0");
        }

        #region 数据绑定
        /// <summary>
        /// 绑定子节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="datatable"></param>
        /// <param name="pid"></param>
        private void CreatChildNode(TreeGridNode parentNode, DataTable datatable, string pid)
        {
            DataRow[] rowList = datatable.Select("GTPID=" + pid);
            if (rowList != null && rowList.Length > 0)
                foreach (DataRow dr in rowList)
                {
                    TreeGridNode node = new TreeGridNode();
                    node = parentNode.Nodes.Add(dr["GTID"], dr["GTName"], dr["GTPID"]);

                    if (Convert.ToString(dr["GTPID"]) == "0")
                    {
                        node.DefaultCellStyle.BackColor = Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                    }

                    //实现递归
                    CreatChildNode(node, datatable, dr["GTID"].ToString());
                }
        }

        /// <summary>
        /// 绑定父节点
        /// </summary>
        /// <param name="Result"></param>
        public void Bind(DataTable Result, string mpid)
        {
            //清空tgv_m_MemberMessahe
            this.treeGridView1.Nodes.Clear();
            Font boldFont = new Font(treeGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            if (Result != null && Result.Rows.Count > 0)
            {
                DataRow[] rowList = Result.Select("GTPID=" + mpid);
                if (rowList != null && rowList.Length > 0) //如果不等于空
                    foreach (DataRow dr in rowList)
                    {
                        TreeGridNode nodep = new TreeGridNode();//实例化节点
                        nodep = treeGridView1.Nodes.Add(dr["GTID"], dr["GTName"], dr["GTPID"]);
                        nodep.DefaultCellStyle.BackColor = Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));

                        //实现递归
                        CreatChildNode(nodep, Result, dr["GTID"].ToString());
                    }

            }
        }

        #endregion
    }
}
