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
    public partial class DashboardForm : Form
    {

        private readonly InvoiceRepository invoiceRepo = new InvoiceRepository();

        public DashboardForm()
        {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // Total Invoice Count
            txt_total_invoice.Text = invoiceRepo.GetTotalInvoices().ToString();

            // Total Sale (format as money)
            txt_total_sale.Text = invoiceRepo.GetTotalSales().ToString("C"); // Example: $1,234.00

            // Total Product Count

            txt_today_sale.Text = invoiceRepo.GetTodaySales().ToString("C");
            txt_top_product.Text = invoiceRepo.GetTotalProducts().ToString();

        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txt_top_product_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void label8_Click(object sender, EventArgs e)
        {
            DashboardForm form = new DashboardForm();
            this.Hide();
            form.Show();
        }
    }
}
