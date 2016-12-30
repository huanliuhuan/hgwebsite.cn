using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warehouse.Bll;

namespace WarehouseSystem.Test
{
    public partial class TestForm1 : Form
    {
        public TestForm1()
        {
            InitializeComponent();
        }

        #region 公共

        /// <summary>
        /// 测试逻辑
        /// </summary>
        private TestBLL tbll = new TestBLL();

        /// <summary>
        /// 选中的ID
        /// </summary>
        public string selectId { get; set; }

        #endregion

        private void TestForm1_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbll.AddData(new Warehouse.Model.Test() { Name = this.textBox1.Text.Trim() }))
            {
                BindData();
            }
            else
            {
                MessageBox.Show("失败！");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void BindData()
        {
            this.dataGridView1.DataSource = tbll.FindAllByWhere("");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbll.UpdateDataByWhere(new Warehouse.Model.Test() { Name = this.textBox2.Text.Trim() }, " Id = '" + selectId + "'"))
            {
                MessageBox.Show("成功！");
                BindData();
            }
            else
            {
                MessageBox.Show("失败！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tbll.DeleteData(Convert.ToInt32(this.selectId)))
            {
                MessageBox.Show("成功！");
                BindData();
            }
            else
            {
                MessageBox.Show("失败！");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectId = this.dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            this.textBox2.Text = this.dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
        }
    }
}
