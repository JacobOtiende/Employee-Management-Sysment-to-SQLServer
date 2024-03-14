using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mod7EMpSystem
{
    public partial class Form1 : Form
    {
        CRUD crud;
        private object btnSumbit;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            crud = new CRUD();
            dataGridViewEmp.DataSource = crud.GetAll();
            dataGridViewEmp.Columns[4].Visible = false;
            dataGridViewEmp.Columns[5].Visible = false;
            btnSubmit.Enabled = false;
            btnUpdate.Enabled = false;
            foreach(var d in crud.GetDepartments())
            {
                comboDept.Items.Add(d.DeptName);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtID.Text = (crud.GetMaxId() + 1).ToString();
            txtID.ReadOnly = true;
            txtName.Clear();
            txtSalary.Clear();
            btnSubmit.Enabled = true;
        }
        private void Clear()
        {
            txtID.Clear();
            txtSalary.Clear();
            txtName.Clear();
            comboDept.SelectedIndex = -1;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtID.Text) && !string.IsNullOrEmpty(txtName.Text))
            {
                if(comboDept.SelectedIndex!=-1)
                {
                    var newEmp = new Employee();
                    newEmp.EmpId = int.Parse(txtID.Text);
                    newEmp.EmpName = txtName.Text;
                    newEmp.EmpSalary = decimal.Parse(txtSalary.Text);
                    newEmp.DeptId = comboDept.SelectedIndex + 1;
                    newEmp.DeptName = (from d in crud.GetDepartments()
                                       where d.DeptId == newEmp.DeptId
                                       select d.DeptName).FirstOrDefault();
                    crud.AddRecord(newEmp);
                    MessageBox.Show("New employee added");
                }
            }
            Clear();
            btnSubmit.Enabled = false;
            dataGridViewEmp.DataSource = crud.GetAll(); // to refresh the grid
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = dataGridViewEmp.CurrentRow.Cells[1].Value;
            var emptodel = crud.FindEmployee((int)id);
            crud.DeleteRecord(emptodel);
            MessageBox.Show("Record deleted permanently");
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var id = dataGridViewEmp.CurrentRow.Cells[1].Value;
            var emptoupdate = crud.FindEmployee((int)id);
            txtID.Text = emptoupdate.EmpId.ToString();
            txtID.ReadOnly = true;
            txtName.Text = emptoupdate.EmpName;
            txtSalary.Text = emptoupdate.EmpSalary.ToString();
            comboDept.SelectedItem= (from d in crud.GetDepartments()
                                       where d.DeptId == emptoupdate.DeptId
                                       select d.DeptName).FirstOrDefault();
            btnUpdate.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var id = int.Parse(txtID.Text);
            var emptoupdate = crud.FindEmployee(id);
            emptoupdate.EmpName = txtName.Text;
            emptoupdate.EmpSalary = decimal.Parse(txtSalary.Text);
            emptoupdate.DeptId = comboDept.SelectedIndex + 1;
            emptoupdate.DeptName = comboDept.Text;
            MessageBox.Show("Record updated");
            Clear();
            btnUpdate.Enabled = false;
            dataGridViewEmp.DataSource = crud.GetAll();
        }
    }
}
