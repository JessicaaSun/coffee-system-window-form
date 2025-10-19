using CoffeeSystem.Data;
using CoffeeSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CoffeeSystem.Repository
{
    public class UserRepository
    {
        public List<User> GetAll()
        {
            List<User> users = new List<User>();
             var conn = Database.GetConnection();
            conn.Open();
            var cmd = new SqlCommand("SELECT id, name, email, password, role, created_at FROM users", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = (int)reader["id"],
                    Name = reader["name"].ToString(),
                    Email = reader["email"].ToString(),
                    Role = reader["role"].ToString(),
                    Password = reader["password"].ToString(),
                    CreatedAt = (DateTime)reader["created_at"]
                }); ;
            }
            return users;
        }

        public void Insert(User user)
        {
             var conn = Database.GetConnection();
            conn.Open();
            var cmd = new SqlCommand(
                "INSERT INTO users (name, email, password, role) VALUES (@name, @email, @password, @role)", conn);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@role", user.Role);
            cmd.ExecuteNonQuery();
        }

        public void Update(User user)
        {
             var conn = Database.GetConnection();
            conn.Open();
            var cmd = new SqlCommand(
                "UPDATE users SET name=@name, email=@email, password=@password, role=@role WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@role", user.Role);
            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
             var conn = Database.GetConnection();
            conn.Open();
            var cmd = new SqlCommand("DELETE FROM users WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
