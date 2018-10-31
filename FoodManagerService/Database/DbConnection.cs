using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace FoodManagerService
{
    public static class DbConnection
    {
        private const string _server = "127.0.0.1";
        private const string _userId = "root";
        private const string _password = "";
        private const string _database = "my_food_stock";
        private const MySqlSslMode _sslMode = MySqlSslMode.None;
        /// <summary>
        /// get static MySqlConnection instance
        /// </summary>
        /// <returns>connection to the database</returns>
        public static MySqlConnection CreateConnection()
        {
            MySqlConnection instance = new MySqlConnection();
            MySqlConnectionStringBuilder mySqlConnectionStringBuilder =
                new MySqlConnectionStringBuilder
                {
                    Server = _server,
                    UserID = _userId,
                    Password = _password,
                    Database = _database,
                    SslMode = _sslMode
                };
            try
            {
                instance.ConnectionString = mySqlConnectionStringBuilder.GetConnectionString(true);
                Console.WriteLine(instance.ConnectionString);
                instance.Open();
                instance.Close();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        throw new Exception("Cannot connect to server.  Contact administrator");
                        break;
                    case 1045:
                        throw new Exception("Invalid username/password, please try again");
                        break;
                }
            }

            return instance;
        }
    }
}