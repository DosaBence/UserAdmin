using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using UserAdmin.Models;

namespace UserAdmin.Services
{
    internal class UserDbService
    {
        public string ConnectionString = "Server=localhost; Database=useradmin; User=root;Password=;";

        public void Add(User user)
        {
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            string sql = @"INSERT INTO `users`(`username`, `email`, `password`, `registeredAt`)
            VALUES (@username,@Email,@Password,@RegisteredAt)";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@username",user.Username);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@RegisteredAt", user.RegisteredAt);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
