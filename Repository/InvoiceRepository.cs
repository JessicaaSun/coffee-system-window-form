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
            var conn = Database.GetConnection();
            conn.Open();
            var cmd = new SqlCommand(
                "INSERT INTO invoices (UserId, InvoiceDate, TotalAmount) VALUES (@user, @date, @total)", conn);
            cmd.Parameters.AddWithValue("@user", invoice.UserId);
            cmd.Parameters.AddWithValue("@date", invoice.InvoiceDate);
            cmd.Parameters.AddWithValue("@total", invoice.TotalAmount);
            cmd.ExecuteNonQuery();
        }
    }
}
