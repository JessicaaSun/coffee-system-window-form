using CoffeeSystem.Data;
using CoffeeSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CoffeeSystem.Repository
{
    public class CategoryRepository
    {
        // Get all categories
        public List<Category> GetAll()
        {
            List<Category> list = new List<Category>();
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM categories", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Category
                    {
                        Id = (int)reader["id"],
                        Name = reader["name"].ToString() ?? "",
                        Description = reader["description"].ToString() ?? ""
                    });
                }
            }
            return list;
        }

        // Insert new category
        public void Insert(Category category)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO categories (name, description) VALUES (@name, @desc)", conn);
                cmd.Parameters.AddWithValue("@name", category.Name);
                cmd.Parameters.AddWithValue("@desc", category.Description);
                cmd.ExecuteNonQuery();
            }
        }

        // Update existing category
        public void Update(Category category)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE categories SET name=@name, description=@desc WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", category.Id);
                cmd.Parameters.AddWithValue("@name", category.Name);
                cmd.Parameters.AddWithValue("@desc", category.Description);
                cmd.ExecuteNonQuery();
            }
        }

        // Delete category by ID
        public void Delete(int id)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                // Check if any products are using this category
                SqlCommand checkCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM products WHERE category_id = @id", conn);
                checkCmd.Parameters.AddWithValue("@id", id);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    throw new Exception("Cannot delete this category because it has related products.");
                }

                SqlCommand cmd = new SqlCommand("DELETE FROM categories WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
