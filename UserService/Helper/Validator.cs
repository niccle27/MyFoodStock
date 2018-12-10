using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using MySql.Data.MySqlClient;
using UserService.Modele;

namespace UserService.Helper
{
    public class Validator
    {
        /// <summary>
        /// generate a valid token and make sure it's unique in the database
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="user"></param>
        /// <param name="hashGenerator"></param>
        /// <returns></returns>
        public string ValidUniqueToken( MySqlConnection connection, User user, HashGenerator hashGenerator)
        {
            string generatedToken = null;
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM users WHERE token=@token";
                MySqlDataReader reader;
                bool isAlreadyAttributed;

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
            }
            catch (MySqlException e)
            {
                throw new FaultException(e.Message);
            }
            return generatedToken;
        }
        /// <summary>
        /// make sure that the password used match with the hash in the database
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="user"></param>
        /// <param name="hashGenerator"></param>
        /// <returns></returns>
        public bool Authenticate(MySqlConnection connection,User user, HashGenerator hashGenerator)
        {
            bool isAuthenticate = false;
            try
            {
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
            }
            catch (MySqlException e)
            {
                throw new FaultException(e.Message);
            }

            return isAuthenticate;
        }
        /// <summary>
        /// check whether the user is already taken
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="user"></param>
        /// <returns>true if it already exist</returns>
        public bool CheckIfUsernameIsAlreadyTaken(MySqlConnection connection, User user)
        {
            bool alreadyExist = false;
            try
            {
                connection.Open();

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
            }
            catch (MySqlException e)
            {
                throw new FaultException(e.Message);
            }
            return alreadyExist;
        }
        /// <summary>
        /// check whether the token is still valid or not
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsTokenStillValid(MySqlConnection connection, User user)
        {
            bool connected = false;
            try
            {
                connection.Open();
                
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
            }

                catch (MySqlException e)
            {
                throw new FaultException(e.Message);
            }
            return connected;
        }
    }
}