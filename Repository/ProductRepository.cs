using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CoffeeSystem.Models;
using CoffeeSystem.Data;

namespace CoffeeSystem.Repository
{
    public class ProductRepository
    {
        public List<Product> GetAll()
        {
            var list = new List<Product>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM products", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Product
                    {
                        Id = (int)reader["id"],
                        Name = reader["name"].ToString() ?? "",
                        CategoryId = (int)reader["category_id"],
                        Price = (decimal)reader["price"],
                        Qty = (int)reader["qty"]
                    });
                }
            }
            return list;
        }

        public void Insert(Product p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO products (name, category_id, price, qty) VALUES (@name, @cat, @price, @qty)", conn);
                cmd.Parameters.AddWithValue("@name", p.Name);
                cmd.Parameters.AddWithValue("@cat", p.CategoryId);
                cmd.Parameters.AddWithValue("@price", p.Price);
                cmd.Parameters.AddWithValue("@qty", p.Qty);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Product p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE products SET name=@name, category_id=@cat, price=@price, qty=@qty WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@name", p.Name);
                cmd.Parameters.AddWithValue("@cat", p.CategoryId);
                cmd.Parameters.AddWithValue("@price", p.Price);
                cmd.Parameters.AddWithValue("@qty", p.Qty);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM products WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
