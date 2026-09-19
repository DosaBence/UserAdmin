using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using UserAdmin.Models;

namespace UserAdmin.Services
{
    public class UserDbService
    {
        public string ConnectionString = "Server=localhost; Database=useradmin; User=root;Password=;";

        public void Delete(User user)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            string sql = "DELETE FROM `users` WHERE id = @id" ;
            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", user.Id);

            cmd.ExecuteNonQuery();

            connection.Close();



        }
        public void Update(User user)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            string sql = @"UPDATE `users` SET `username`=@username,`email`=@email,`password`=@password WHERE id = @id;";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@id", user.Id);

            cmd.ExecuteNonQuery();

            connection.Close();
        }


        public void Add(User user)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            string sql = @"INSERT INTO `users`(`username`, `email`, `password`, `registeredAt`)
            VALUES (@username,@Email,@Password,@RegisteredAt)";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@username",user.Username);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@RegisteredAt", user.RegisteredAt);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        //Megvizsgáljuk a függvénnyel, hogy van e ilyen jelszó és usernév páros
        public User FindByEmail(string email)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            string sql = @"SELECT  `username`, `email`, `password`, `registeredAt` FROM `users` WHERE email = @email";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@email", email);

            var reader = cmd.ExecuteReader(); //választó lekérdezést hoz létre
            if (reader.Read())
            {
                var user = new User
                {
                    Username = reader.GetString(0),
                    Email = reader.GetString(1),
                    Password = reader.GetString(2),
                    RegisteredAt = reader.GetDateTime(3)
                };

                connection.Close();
                return user;
            }
            else
            {
                connection.Close();
                return null;
            }

           
        }

        public List<User> GetAll() // Ez a függvénytábla lekérdezi a users összes tartalmát.
        {
            var users = new List<User>();
                 using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            string sql = @"SELECT `id`, `username`, `email`, `password`, `registeredAt` FROM `users` ORDER BY RegisteredAt";
            var cmd = new MySqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var user = new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3),
                    RegisteredAt = reader.GetDateTime(4)
                };

                users.Add(user);
            }
            connection.Close();
            return users;


        }
    }
}
