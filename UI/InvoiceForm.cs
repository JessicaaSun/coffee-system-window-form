using CoffeeSystem.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeSystem.UI
{
    public partial class InvoiceForm : Form
    {
        private readonly InvoiceRepository invoiceRepo ;
        private readonly InvoiceItemRepository itemRepo;
        public InvoiceForm()
        {
            invoiceRepo = new InvoiceRepository();
            itemRepo = new InvoiceItemRepository();

            InitializeComponent();
            LoadInvoices();
        }

        private void invoiceGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadInvoices()
        {
            var invoices = invoiceRepo.GetAll();
            invoiceGridView.DataSource = null;
            invoiceGridView.DataSource = invoices;
            invoiceGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            invoiceGridView.ClearSelection();
        }

        private void invoiceGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (invoiceGridView.CurrentRow != null)
            {
                int invoiceId = (int)invoiceGridView.CurrentRow.Cells["Id"].Value;
                var items = itemRepo.GetByInvoiceId(invoiceId);

                // You can show invoice items in another grid, e.g. invoiceItemsGridView
                // invoiceItemsGridView.DataSource = items;
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

        private void label5_Click(object sender, EventArgs e)
        {
            EmployeeForm form = new EmployeeForm();
            this.Hide();
            form.Show();
        }
    }
}
