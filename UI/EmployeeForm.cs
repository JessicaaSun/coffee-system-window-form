using CoffeeSystem.Models;
using CoffeeSystem.Repository;
using System;
using System.Windows.Forms;

namespace CoffeeSystem.UI
{
    public partial class EmployeeForm : Form
    {
        private readonly UserRepository userRepo = new UserRepository();

        public EmployeeForm()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            var employees = userRepo.GetAll();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = employees;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ClearSelection();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullname.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            var user = new User
            {
                Name = txtFullname.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                Role = txtDescription.Text
            };

            userRepo.Insert(user);
            LoadEmployees();
            ClearInputs();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select an employee.");
                return;
            }

            int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;

            var user = new User
            {
                Id = id,
                Name = txtFullname.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                Role = txtDescription.Text
            };

            userRepo.Update(user);
            LoadEmployees();
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select an employee.");
                return;
            }

            int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
            var confirm = MessageBox.Show("Are you sure to delete this employee?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                userRepo.Delete(id);
                LoadEmployees();
                ClearInputs();
            }
        }

        private void ClearInputs()
        {
            txtFullname.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtDescription.Text = "";
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtFullname.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                txtEmail.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
                txtPassword.Text = dataGridView1.CurrentRow.Cells["Password"]?.Value.ToString();
                txtDescription.Text = dataGridView1.CurrentRow.Cells["Role"].Value.ToString();
            }
        }

        private void menu_Click(object sender, EventArgs e)
        {
            MenuForm form = new MenuForm();
            this.Hide();
            form.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ProductForm form = new ProductForm();
            this.Hide();
            form.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            CategoryForm form = new CategoryForm();
            this.Hide();
            form.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            InvoiceForm form = new InvoiceForm();
            this.Hide();
            form.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            EmployeeForm form = new EmployeeForm();
            this.Hide();
            form.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            DashboardForm form = new DashboardForm();
            this.Hide();
            form.Show();
        }
    }
}
