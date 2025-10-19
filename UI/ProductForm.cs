using CoffeeSystem.Models;
using CoffeeSystem.Repository;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CoffeeSystem.UI
{
    public partial class ProductForm : Form
    {
        private readonly ProductRepository productRepo;
        private readonly CategoryRepository categoryRepo;

        public ProductForm()
        {

            productRepo = new ProductRepository();
            categoryRepo = new CategoryRepository();
            InitializeComponent();
            LoadCategories();
            LoadProducts();
        }

        // Load categories into combo box
        private void LoadCategories()
        {
            var categories = categoryRepo.GetAll();
            categoryCambobox.DataSource = categories;
            categoryCambobox.DisplayMember = "Name";
            categoryCambobox.ValueMember = "Id";
            categoryCambobox.SelectedIndex = -1; // no default selection
        }

        // Load products into DataGridView
        private void LoadProducts()
        {
            productDataGridView.DataSource = null;
            productDataGridView.DataSource = productRepo.GetAll();

            // Make columns fit the grid
            productDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            productDataGridView.ClearSelection();
        }

        // Create new product
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || categoryCambobox.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in product name and select a category.");
                return;
            }

            var product = new Product
            {
                Name = txtProductName.Text,
                Price = decimal.Parse(txtPrice.Text),
                Qty = int.Parse(txtQty.Text),
                CategoryId = (int)categoryCambobox.SelectedValue
            };

            productRepo.Insert(product);
            LoadProducts();
            ClearInputs();
        }

        // Edit selected product
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (productDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Please select a product to edit.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Please enter product name.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            if (!int.TryParse(txtQty.Text, out int qty))
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            if (categoryCambobox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            int id = (int)productDataGridView.CurrentRow.Cells["Id"].Value;

            var product = new Product
            {
                Id = id,
                Name = txtProductName.Text,
                Price = price,
                Qty = qty,
                CategoryId = (int)categoryCambobox.SelectedValue
            };

            productRepo.Update(product);
            LoadProducts();
            ClearInputs();
        }


        // Delete selected product
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (productDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            int id = (int)productDataGridView.CurrentRow.Cells["Id"].Value;
            var confirm = MessageBox.Show("Are you sure you want to delete this product?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                productRepo.Delete(id);
                LoadProducts();
                ClearInputs();
            }
        }

        // When user clicks a row, populate the fields
        private void productDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (productDataGridView.CurrentRow != null)
            {
                txtProductName.Text = productDataGridView.CurrentRow.Cells["Name"].Value.ToString();
                txtPrice.Text = productDataGridView.CurrentRow.Cells["Price"].Value.ToString();
                txtQty.Text = productDataGridView.CurrentRow.Cells["Qty"].Value.ToString();
                int categoryId = (int)productDataGridView.CurrentRow.Cells["CategoryId"].Value;
                categoryCambobox.SelectedValue = categoryId;
            }
        }

        // Clear input fields
        private void ClearInputs()
        {
            txtProductName.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            categoryCambobox.SelectedIndex = -1;
        }

        private void categoryCambobox_SelectedIndexChanged(object sender, EventArgs e)
        {

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
    }
}
