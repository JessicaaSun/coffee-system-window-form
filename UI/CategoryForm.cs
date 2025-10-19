using CoffeeSystem.Models;
using CoffeeSystem.Repository;
using CoffeeSystem.UI;
using System;
using System.Windows.Forms;

namespace CoffeeSystem
{
    public partial class CategoryForm : Form
    {
        private readonly CategoryRepository repo = new CategoryRepository();

        public CategoryForm()
        {
            InitializeComponent();
            LoadCategories();
        }

        // Load data into the grid view
        private void LoadCategories()
        {
            categoryDataGridView.DataSource = null; // reset
            categoryDataGridView.DataSource = repo.GetAll();
            categoryDataGridView.ClearSelection();
        }

        // Create new category
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Please enter a category name.");
                return;
            }

            var category = new Category
            {
                Name = txtCategoryName.Text,
                Description = txtDescription.Text
            };

            repo.Insert(category);
            LoadCategories();
            txtCategoryName.Clear();
            txtDescription.Clear();
        }

        // Edit selected category
        private void button2_Click(object sender, EventArgs e)
        {
            if (categoryDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Please select a category to edit.");
                return;
            }

            int id = (int)categoryDataGridView.CurrentRow.Cells["Id"].Value;

            var category = new Category
            {
                Id = id,
                Name = txtCategoryName.Text,
                Description = txtDescription.Text
            };

            repo.Update(category);
            LoadCategories();
        }

        // Delete selected category
        private void button3_Click(object sender, EventArgs e)
        {
            if (categoryDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Please select a category to delete.");
                return;
            }

            int id = (int)categoryDataGridView.CurrentRow.Cells["Id"].Value;
            var confirm = MessageBox.Show("Are you sure you want to delete this category?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    repo.Delete(id);
                    LoadCategories();

                    MessageBox.Show("Category deleted successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        // When user clicks a row in the grid, load data into text boxes
        private void categoryDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (categoryDataGridView.CurrentRow != null)
            {
                txtCategoryName.Text = categoryDataGridView.CurrentRow.Cells["Name"].Value.ToString();
                txtDescription.Text = categoryDataGridView.CurrentRow.Cells["Description"].Value.ToString();
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
    }
}
