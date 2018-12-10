using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using UserService.Helper;
using UserService.Modele;

namespace UserService.Database
{
    public class UserDAO
    {
        private readonly MySqlConnection _connection;

        public UserDAO(MySqlConnection connection)
        {
            _connection = connection;
        }
        /// <summary>
        /// create a user in the database
        /// </summary>
        /// <param name="user">user Class</param>
        /// <param name="hashGenerator">used to hash the password</param>
        /// <param name="generatedToken"> used to generate a valid token</param>
        public void Create(User user, HashGenerator hashGenerator, string generatedToken)
        {
            _connection.Open();
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = _connection,
                CommandText =
                    "INSERT INTO users (login,password) " +
                    "VALUES (@login,@password)"
            };
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@login", user.Login);
            cmd.Parameters.AddWithValue("@password", hashGenerator.GenerateSHA512Hash(user.Password));
            cmd.ExecuteNonQuery();
            _connection.Close();
            UpdateToken(user,generatedToken);
        }
        /// <summary>
        /// update the token
        /// </summary>
        /// <param name="user">user Class</param>
        /// <param name="generatedToken">valid token</param>
        public void UpdateToken(User user,string generatedToken)
        {
            _connection.Open();
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = _connection,
                CommandText = "UPDATE users SET" +
                              " token = @token," +
                              " tokenValidTimeStamp = @tokenValidTimeStamp" +
                              " WHERE login = @login"
            };
            cmd.Parameters.AddWithValue("@token", generatedToken);
            cmd.Parameters.AddWithValue("@login", user.Login);
            cmd.Parameters.AddWithValue("@tokenValidTimeStamp", DateTime.Now.AddMinutes(20));
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
        /// <summary>
        /// change the password for a specific user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userId"></param>
        public void UpdatePassword(User user, int userId)
        {
            _connection.Open();
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = _connection,
                CommandText = "UPDATE users SET" +
                              " password = @password," +
                              " WHERE token = @token AND login = @login"
            };
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@token", user.Token);
            cmd.Parameters.AddWithValue("@login", user.Login);
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }
}