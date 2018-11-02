using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using UserService.Modele;

namespace UserService.Helper
{
    public class Validator
    {
        public string ValidUniqueToken( MySqlConnection connection, User user, HashGenerator hashGenerator)
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM users WHERE token=@token";
            MySqlDataReader reader;
            bool isAlreadyAttributed;
            string generatedToken = null;
            do
            {
                generatedToken = hashGenerator.GenerateTokenHash(user.Login);
                isAlreadyAttributed = false;
                cmd.Parameters.AddWithValue("@token", generatedToken);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isAlreadyAttributed = true;
                }
            } while (isAlreadyAttributed);
            connection.Close();
            return generatedToken;
        }

        public bool Authenticate(MySqlConnection connection,User user, HashGenerator hashGenerator)
        {
            bool isAuthenticate = false;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand
            {
                CommandText = "SELECT * FROM users WHERE login = @login and password=@password",
                Connection = connection
            };
            cmd.Parameters.AddWithValue("@login", user.Login);
            cmd.Parameters.AddWithValue("@password", hashGenerator.GenerateSHA512Hash(user.Password));
            
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                isAuthenticate = true;
            }
            connection.Close();
            return isAuthenticate;
        }

        public bool CheckIfUsernameIsAlreadyTaken(MySqlConnection connection, User user)
        {
            connection.Open();
            bool alreadyExist = false;
            MySqlCommand cmd = new MySqlCommand
            {
                CommandText = "SELECT * FROM users WHERE login = @login",
                Connection = connection
            };
            cmd.Parameters.AddWithValue("@login", user.Login);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                alreadyExist = true;
            }
            connection.Close();
            return alreadyExist;
        }

        public bool IsTokenStillValid(MySqlConnection connection, User user)//todo complete token validation
        {
            connection.Open();
            bool connected = false;
            MySqlCommand cmd = new MySqlCommand
            {
                CommandText = "SELECT * FROM users WHERE login = @login",
                Connection = connection
            };
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@login", user.Login);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if ((reader.GetDateTime("tokenValidTimeStamp") > DateTime.Now) &&
                    reader.GetString("token") == user.Token)
                {
                    connected = true;
                }
            }
            connection.Close();
            return connected;
        }
    }
}