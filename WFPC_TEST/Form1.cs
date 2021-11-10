using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFPC_TEST
{
    public partial class EmployeeDetail : Form
    {
        private SqlHelper _sql = new SqlHelper();
        public string _connString = "Data Source=ADMIN;Initial Catalog=ExamWinForm;Integrated Security=True";
        public SqlConnection _conn;
        public EmployeeDetail()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LoadDataToComboBox()
        {
            string sql = "Select [DeptID], [DeptName] from [dbo].[Departments]";
            comboBox1.DataSource = _sql.GetDataSet(sql).Tables[0];
            comboBox1.DisplayMember = "DeptName";
            comboBox1.ValueMember = "DeptID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int gencheck = 0;
            if (male.Checked==true)
            {
                gencheck = 1;
            }else if (female.Checked == true)
            {
                gencheck = 0;
            }
            string sql = "insert into [dbo].[Employees]([EmployeeName],[DeptID],[Gender],[BirthDate],[Tel],[Address])" +
                "values('" + textBox1.Text + "'," + comboBox1.SelectedValue + ",'" + gencheck + "','" + dateTimePicker1.Text + "','" + textBox2.Text + "','" + textBox2.Text + "')"; ;
            if (textBox1.Text.Length <= 5 || textBox2.Text.Length<=6)
            {
                MessageBox.Show("invalid input");
            }
            else {
                SqlCommand con = new SqlCommand(sql, _conn);
                con.Parameters.AddWithValue("@EmployeeName", textBox1.Text);
                con.Parameters.AddWithValue("@DeptID", comboBox1.SelectedValue);
                con.Parameters.AddWithValue("@Gender", gencheck);
                con.Parameters.AddWithValue("@BirthDate", dateTimePicker1.Text);
                con.Parameters.AddWithValue("@Tel", textBox2.Text);
                con.Parameters.AddWithValue("@Address", textBox3.Text);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
