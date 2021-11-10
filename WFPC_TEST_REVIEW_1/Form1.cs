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

namespace WFPC_TEST_REVIEW_1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Connection String   
            SqlConnection con = new SqlConnection(@"Data Source=localhost\mssqlserver01;Initial Catalog=ExamWinForm;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select * from Employees where EmployeeName=@EmployeeName", con);
            cmd.Parameters.AddWithValue("@EmployeeName", txtUser.Text);
            //cmd.Parameters.AddWithValue("@Password", txtPass.Text);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //Connection open here   
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Successfully loged in");
                //after successful it will redirect to next page .  
                Form2 settingsForm = new Form2();
                settingsForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please enter Correct Username and Password");
            }
        }
    }
}
