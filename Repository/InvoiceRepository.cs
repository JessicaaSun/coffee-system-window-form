using CoffeeSystem.Data;
using CoffeeSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CoffeeSystem.Repository
{
    public class InvoiceRepository
    {
        public List<Invoice> GetAll()
        {
            List<Invoice> list = new List<Invoice>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM invoices", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Invoice
                    {
                        Id = (int)reader["Id"],
                        UserId = (int)reader["user_id"],        // match SQL column
                        InvoiceDate = (DateTime)reader["invoice_date"],
                        TotalAmount = (decimal)reader["total_amount"]
                     });
                }
            }
            return list;
        }

        public void Insert(Invoice invoice)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO invoices (user_id, invoice_date, total_amount) VALUES (@user, @date, @total)", conn);
                cmd.Parameters.AddWithValue("@user", invoice.UserId);
                cmd.Parameters.AddWithValue("@date", invoice.InvoiceDate);
                cmd.Parameters.AddWithValue("@total", invoice.TotalAmount);
                cmd.ExecuteNonQuery();
            }
        }

        public decimal GetTotalSales()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT SUM(total_amount) FROM invoices", conn);
                var result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

        public int GetTotalInvoices()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT COUNT(*) FROM invoices", conn);
                return (int)cmd.ExecuteScalar();
            }
        }

        public int GetTotalProducts()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT COUNT(*) FROM products", conn);
                return (int)cmd.ExecuteScalar();
            }
        }

        public decimal GetTodaySales()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT SUM(total_amount) FROM invoices WHERE CAST(invoice_date AS DATE) = CAST(GETDATE() AS DATE)",
                    conn
                );
                var result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }





    }
}
