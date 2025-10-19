using CoffeeSystem.Data;
using CoffeeSystem.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CoffeeSystem.Repository
{
    public class InvoiceItemRepository
    {
        public List<InvoiceItem> GetByInvoiceId(int invoiceId)
        {
            List<InvoiceItem> list = new List<InvoiceItem>();
             var conn = Database.GetConnection();
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM invoice_items WHERE InvoiceId=@id", conn);
            cmd.Parameters.AddWithValue("@id", invoiceId);
             var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new InvoiceItem
                {
                    Id = (int)reader["Id"],
                    InvoiceId = (int)reader["InvoiceId"],
                    ProductId = (int)reader["ProductId"],
                    Quantity = (int)reader["Quantity"],
                    UnitPrice = (decimal)reader["UnitPrice"],
                    Subtotal = (decimal)reader["Subtotal"]
                });
            }
            return list;
        }

        public void Insert(InvoiceItem item)
        {
             var conn = Database.GetConnection();
            conn.Open();
            var cmd = new SqlCommand(
                "INSERT INTO invoice_items (InvoiceId, ProductId, Quantity, UnitPrice, Subtotal) VALUES (@inv,@prod,@qty,@price,@sub)", conn);
            cmd.Parameters.AddWithValue("@inv", item.InvoiceId);
            cmd.Parameters.AddWithValue("@prod", item.ProductId);
            cmd.Parameters.AddWithValue("@qty", item.Quantity);
            cmd.Parameters.AddWithValue("@price", item.UnitPrice);
            cmd.Parameters.AddWithValue("@sub", item.Subtotal);
            cmd.ExecuteNonQuery();
        }
    }
}
