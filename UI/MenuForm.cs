using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CoffeeSystem.Repository;
using CoffeeSystem.Models;

namespace CoffeeSystem.UI
{
    public partial class MenuForm : Form
    {
        private readonly ProductRepository productRepo = new ProductRepository();
        private readonly CategoryRepository categoryRepo = new CategoryRepository();
        private readonly InvoiceRepository invoiceRepo = new InvoiceRepository();

        private List<Product> products = new List<Product>();
        private List<Category> categories = new List<Category>();
        private List<Product> orderList = new List<Product>();

        public MenuForm()
        {
            InitializeComponent();
            panel4.AutoScroll = true;
            panel5.AutoScroll = true;
   
            UpdatelabeltxtTotal();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadProducts();
            RenderProducts(products);
        }

        private void LoadCategories()
        {
            categories = categoryRepo.GetAll();
            categories.Insert(0, new Category { Id = 0, Name = "All" });

            categoryComboBox.DisplayMember = "Name";
            categoryComboBox.ValueMember = "Id";
            categoryComboBox.DataSource = categories;
        }

        private void LoadProducts()
        {
            products = productRepo.GetAll();
        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryComboBox.SelectedValue is int selectedCatId)
            {
                var filtered = selectedCatId == 0
                    ? products
                    : products.Where(p => p.CategoryId == selectedCatId).ToList();
                RenderProducts(filtered);
            }
        }

        private void RenderProducts(List<Product> productList)
        {
            panel4.Controls.Clear();
            int x = 10, y = 10;
            int cardWidth = 210;
            int cardHeight = 120;
            int padding = 15;
            int count = 0;

            foreach (var p in productList)
            {
                Panel card = new Panel
                {
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Size = new Size(cardWidth, cardHeight),
                    Location = new Point(x, y),
                    Cursor = Cursors.Hand
                };

                Label nameLbl = new Label
                {
                    Text = p.Name,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                Label priceLbl = new Label
                {
                    Text = $"${p.Price:F2}",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.DimGray,
                    Location = new Point(10, 40),
                    AutoSize = true
                };

                Button addBtn = new Button
                {
                    Text = "Add",
                    BackColor = Color.LightGreen,
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(card.Width - 70, card.Height - 40),
                    Size = new Size(60, 25)
                };
                addBtn.FlatAppearance.BorderSize = 0;

                addBtn.Click += (s, e) => AddToOrder(p);

                card.Controls.Add(nameLbl);
                card.Controls.Add(priceLbl);
                card.Controls.Add(addBtn);
                panel4.Controls.Add(card);

                // Layout: 2 columns
                count++;
                x += cardWidth + padding;
                if (count % 2 == 0)
                {
                    x = 10;
                    y += cardHeight + padding;
                }
            }
        }

        private void AddToOrder(Product p)
        {
            var existing = orderList.FirstOrDefault(x => x.Id == p.Id);
            if (existing != null)
            {
                existing.Qty++;
            }
            else
            {
                orderList.Add(new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Qty = 1
                });
            }
            RenderOrderList();
            UpdatelabeltxtTotal();   // ← Add this

        }

        private void RenderOrderList()
        {
            panel5.Controls.Clear();
            int y = 10;
            int cardHeight = 80;
            int padding = 10;

            foreach (var item in orderList)
            {
                Panel card = new Panel
                {
                    BackColor = Color.WhiteSmoke,
                    BorderStyle = BorderStyle.FixedSingle,
                    Size = new Size(panel5.Width - 40, cardHeight),
                    Location = new Point(10, y)
                };

                Label nameLbl = new Label
                {
                    Text = item.Name,
                    Font = new Font("Segoe UI", 8),
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                Label priceLbl = new Label
                {
                    Text = $"${item.Price * item.Qty:F2}",
                    Font = new Font("Segoe UI", 9),
                    Location = new Point(10, 30),
                    AutoSize = true
                };

                Label qtyLbl = new Label
                {
                    Text = item.Qty.ToString(),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(card.Width - 87, 55),
                    AutoSize = true
                };

                Button minusBtn = new Button
                {
                    Text = "–",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Size = new Size(25, 25),
                    Location = new Point(card.Width - 120, 50),
                    BackColor = Color.LightCoral,
                    FlatStyle = FlatStyle.Flat
                };
                minusBtn.FlatAppearance.BorderSize = 0;
                minusBtn.Click += (s, e) => DecreaseQty(item);

                Button plusBtn = new Button
                {
                    Text = "+",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Size = new Size(25, 25),
                    Location = new Point(card.Width - 55, 50),
                    BackColor = Color.LightGreen,
                    FlatStyle = FlatStyle.Flat
                };
                plusBtn.FlatAppearance.BorderSize = 0;
                plusBtn.Click += (s, e) => IncreaseQty(item);

                card.Controls.Add(nameLbl);
                card.Controls.Add(priceLbl);
                card.Controls.Add(minusBtn);
                card.Controls.Add(qtyLbl);
                card.Controls.Add(plusBtn);
                panel5.Controls.Add(card);

                y += cardHeight + padding;
            }
        }

        private void IncreaseQty(Product p)
        {
            p.Qty++;
            RenderOrderList();
            UpdatelabeltxtTotal();
        }

        private void DecreaseQty(Product p)
        {
            p.Qty--;
            if (p.Qty <= 0)
                orderList.Remove(p);
            RenderOrderList();
            UpdatelabeltxtTotal();
        }



        private void UpdatelabeltxtTotal()
        {
            decimal total = orderList.Sum(p => p.Price * p.Qty);
            txt_total.Text = $"${total:F2}";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (!orderList.Any())
            {
                MessageBox.Show("No items in order.");
                return;
            }

            decimal total = orderList.Sum(p => p.Price * p.Qty);
            var invoice = new Invoice
            {
                UserId = 1, // Example: replace with logged-in user
                InvoiceDate = DateTime.Now,
                TotalAmount = total
            };

            invoiceRepo.Insert(invoice);
            MessageBox.Show("Checkout successful!");

            orderList.Clear();
            RenderOrderList();
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

        private void label8_Click(object sender, EventArgs e)
        {
            DashboardForm form = new DashboardForm();
            this.Hide();
            form.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
