using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MySql.Data.MySqlClient;
using UserService.Database;
namespace UserService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        static readonly MySqlConnection DbConnection = new DbConnection().GetConnection();
        #region ServiceMethod
        /// <summary>
        /// try to connect with a login and password. This verify whether they match
        /// or not and in case of match, it generates and return the token
        /// </summary>
        /// <param name="userLogin">user in database</param>
        /// <param name="userPassword">password that match the user</param>
        /// <returns>token in case of success, otherwise null</returns>
        public string Connect(string userLogin, string userPassword)
        {
            string generatedToken = null;
            
            if (Authenticate(userLogin,userPassword))
            {
                generatedToken=UpdateToken(userLogin);
            }
            return generatedToken;
        }
        /// <summary>
        /// Create a user entry in the database. This function check whether the
        /// login is already taken or not. Return token in case of success
        /// </summary>
        /// <param name="userLogin">user in database</param>
        /// <param name="userPassword">password that match the user</param>
        /// <returns>token in case of success, otherwise null</returns>
        public string Register(string userLogin, string userPassword)
        {
            string generatedToken = null;
            if (!CheckIfUsernameIsAlreadyTaken(userLogin))
            {
                generatedToken = CreateUserAccount(userLogin, userPassword);
            }
            return generatedToken;
        }
        /// <summary>
        /// verify whether the token match the login and is still valid
        /// </summary>
        /// <param name="userLogin">login in database</param>
        /// <param name="userToken">token in database</param>
        /// <returns>true in case of success , false otherwise</returns>
        public bool IsTokenStillValid(string userLogin, string userToken)//todo complete token validation
        {
            DbConnection.Open();
            bool connected = false;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM users WHERE login = @login";
            cmd.Parameters.AddWithValue("@login", userLogin);
            cmd.Connection = DbConnection;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if ((reader.GetDateTime("tokenValidTimeStamp") > DateTime.Now) &&
                    reader.GetString("token") == userToken) 
                {
                    connected = true;
                }
            }
            DbConnection.Close();
            return connected;
        }

        #endregion

        #region privateStaticFunction
        /// <summary>
        /// create the SHA512 hash from the specified string password
        /// </summary>
        /// <param name="password">password to hash</param>
        /// <returns>hash of the password</returns>
        static String HashPassword(String password)
        {
            Byte[] output;
            using (SHA512 hashingUnit = new SHA512Managed())
            {
                output = hashingUnit.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < output.Length; i++)
            {
                sb.Append(output[i].ToString("X2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// generate a token from a random number and the username
        /// </summary>
        /// <param name="username">username from the database</param>
        /// <returns>the generated token hash</returns>
        static String GenerateTokenHash(String username)
        {
            Byte[] output;
            using (MD5 hashingUnit = MD5.Create())
            {
                Random rand = new Random();
                output = hashingUnit.ComputeHash(Encoding.UTF8.GetBytes(username + rand.Next(99999).ToString()));
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < output.Length; i++)
            {
                sb.Append(output[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Evalue whether the password math with the hash in the database for the specified login
        /// </summary>
        /// <param name="userLogin">login in the database</param>
        /// <param name="userPassword">password matching the login</param>
        /// <returns>true if the password match</returns>
        static bool Authenticate(string userLogin, string userPassword)
        {
            bool isAuthenticate = false;
            DbConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM users WHERE login = @login and password=@password";
            cmd.Parameters.AddWithValue("@login", userLogin);
            cmd.Parameters.AddWithValue("@password", HashPassword(userPassword));
            cmd.Connection = DbConnection;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                isAuthenticate = true;
            }
            DbConnection.Close();
            return isAuthenticate;
        }
        /// <summary>
        /// generate a token and verify that the token does not match any other login
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>the generated token</returns>
        static string UpdateToken(string userLogin)
        {   DbConnection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DbConnection;
            cmd.CommandText = "SELECT * FROM users WHERE token=@token";
            MySqlDataReader reader;
            bool isAlreadyAttributed;
            string generatedToken = null;
            do
            {
                generatedToken = GenerateTokenHash(userLogin);
                isAlreadyAttributed = false;
                cmd.Parameters.AddWithValue("@token", generatedToken);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isAlreadyAttributed = true;
                }
            } while (isAlreadyAttributed);
            reader.Close();
            cmd.CommandText = "UPDATE users SET token = @token, tokenValidTimeStamp = @tokenValidTimeStamp  where login=@login";
            //cmd.Parameters.AddWithValue("@token", generatedToken);//was already defined, and we still use the same object to save memory
            cmd.Parameters.AddWithValue("@login", userLogin);
            cmd.Parameters.AddWithValue("@tokenValidTimeStamp", DateTime.Now.AddMinutes(20));
            cmd.Connection = DbConnection;
            cmd.ExecuteNonQuery();
            DbConnection.Close();
            return generatedToken;
        }
        /// <summary>
        /// Query the database to see it there is the login is already taken
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>true if the login is already taken</returns>
        static bool CheckIfUsernameIsAlreadyTaken(string userLogin)
        {
            DbConnection.Open();
            bool alreadyExist = false;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM users WHERE login = @login";
            cmd.Parameters.AddWithValue("@login", userLogin);
            cmd.Connection = DbConnection;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                alreadyExist = true;
            }
            DbConnection.Close();
            return alreadyExist;
        }
        /// <summary>
        /// create an account by inserting the database with login, hashpassword and generated token
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="userPassword"></param>
        /// <returns>generated token</returns>
        static string CreateUserAccount(string userLogin, string userPassword)
        {
            DbConnection.Open();
            string generatedToken = GenerateTokenHash(userLogin);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DbConnection;
            cmd.CommandText = "INSERT INTO users (login,password) VALUES (@login,@password)";
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@login", userLogin);
            cmd.Parameters.AddWithValue("@password", HashPassword(userPassword));
            //cmd.Parameters.AddWithValue("@token", generatedToken);
            cmd.ExecuteNonQuery();
            DbConnection.Close();
            return UpdateToken(userLogin);
        }

        #endregion

    }
}
