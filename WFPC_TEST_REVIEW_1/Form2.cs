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
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=localhost\mssqlserver01;Initial Catalog=ExamWinForm;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        //ID variable used in Updating and Deleting Record  
        int ID = 0;

        public Form2()
        {
            InitializeComponent();
            //tree view display
            BindTreeView();
            //combo box data
            ComboBoxData();
        }

        public void ComboBoxData()
        {
            try
            {
                ExamWinFormEntities dc = new ExamWinFormEntities();
                var listDept = (from x in dc.Departments select x.DeptID).ToList();
                comDept.ValueMember = "Department";
                comDept.DataSource = listDept;

                var listGender = (from x in dc.Employees select x.Gender).ToList();
                comGender.ValueMember = "Gender";
                comGender.DataSource = listGender;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void BindTreeView()
        {
            ExamWinFormEntities entity = new ExamWinFormEntities();
            var node = entity.Departments.ToList();
            foreach(var item in node)
            {
                TreeNode rootNode = new TreeNode(item.DeptName);
                Department dept = item as Department;
                var childNode = entity.Departments.Where(a => a.DeptID == dept.DeptID);
                foreach(var child in childNode)
                {
                    TreeNode childNo = new TreeNode(child.DeptName);
                    rootNode.Nodes.Add(childNo);
                }
                treeView1.Nodes.Add(rootNode);
            }
        }

        //Display Data in DataGridView  
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Employees", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        //Clear Data from all txtboxes
        private void ClearData()
        {
            txtAddress.Text = "";
            //txtBirthdate.Text = "";
            txtName.Text = "";
            txtTel.Text = "";
            ID = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAddress.Text != "" /*&& txtBirthdate.Text != ""*/ && txtName.Text != "" && txtTel.Text != "" && comDept.SelectedValue!=null && comGender.SelectedValue!=null)
            {
                cmd = new SqlCommand("insert into Employees(EmployeeName,BirthDate,Tel,Address,DeptID,Gender) values(@name,@birth,@tel,@address,@deptid,@gender)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@birth", dateBirthdayPicker.Value);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@tel", txtTel.Text);
                cmd.Parameters.AddWithValue("@deptid", comDept.SelectedItem);
                cmd.Parameters.AddWithValue("@gender", comGender.SelectedItem);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("delete Employees where EmployeeID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        //add data button
        private void btnQuery_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        //update data button
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtAddress.Text != "" /*&& txtBirthdate.Text != ""*/ && txtName.Text != "" && txtTel.Text != "" && comDept.SelectedValue != null && comGender.SelectedValue != null)
            {
                cmd = new SqlCommand("update Employees set EmployeeName=@name,BirthDate=@birth,Tel=@tel,Address=@address,DeptID=@deptid,Gender=@gender where EmployeeID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@birth", dateBirthdayPicker.Value.ToShortDateString());
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@tel", txtTel.Text);
                cmd.Parameters.AddWithValue("@deptid", comDept.SelectedItem);
                cmd.Parameters.AddWithValue("@gender", comGender.SelectedItem);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                con.Close();
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }
    }
}
