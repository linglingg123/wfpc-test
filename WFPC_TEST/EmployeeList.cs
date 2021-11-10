using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFPC_TEST
{
    public partial class EmployeeList : Form
    {
        private SqlHelper _sql = new SqlHelper();
        public EmployeeList()
        {
            InitializeComponent();
        }

        private void LoadDataToTreeView()
        {
            string sql = "Select [DeptID], [DeptName] from [dbo].[Departments]";

        }

        private DataTable BindDataToDGV()
        {
            string sql = "select a.[EmployeeID],[EmployeeName],[DeptID],[Gender],[BirthDate],[Tel],[Address], b.DeptID as DeptId" +
                "from [dbo].[Employees] a" +
                "inner join [dbo].[Departments] b on a.DeptID=b.DeptID";
            DataSet ds = new DataSet();
            return _sql.GetDataSet(sql).Tables[0];
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            EmployeeDetail f2 = new EmployeeDetail();
            f2.Enabled = true;
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = $"Delete from [dbo].[Employees] where [EmployeeID]={dataGridView1..CellClick}";
            if (_sql.ExecNoneQuery(sql) > 0)
            {
                MessageBox.Show("success");
                dataGridView1.DataSource = BindDataToDGV();
            }else
            {
                MessageBox.Show("fail");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void EmployeeList_Load(object sender, EventArgs e)
        {

        }
    }
}
