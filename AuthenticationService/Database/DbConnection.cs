using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace AuthenticationService.Database
{
    /// <summary>
    /// This class proxy the MySqlConnection in order to make sure that the MySqlConnection instance remain
    /// unique while avoiding the use of the deprecated Singleton patter which make the code untestable
    /// using unit test => please use dependency injection instead
    /// </summary>
    public class DbConnection
    {

        private static MySqlConnection instance;
        /// <summary>
        /// build the MySqlConnection
        /// throw an exception if the instance is not null
        /// </summary>
        public DbConnection()
        {
            if (instance != null) throw new Exception("Connection instance are limited to 1 instance, please use dependency injection");
            instance = new MySqlConnection();
            MySqlConnectionStringBuilder mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder();
            mySqlConnectionStringBuilder.Server = "127.0.0.1";
            mySqlConnectionStringBuilder.UserID = "root";
            mySqlConnectionStringBuilder.Password = "";
            mySqlConnectionStringBuilder.Database = "my_food_stock";
            mySqlConnectionStringBuilder.SslMode = MySqlSslMode.None;
            try
            {
                instance.ConnectionString = mySqlConnectionStringBuilder.GetConnectionString(true);
                Console.WriteLine(instance.ConnectionString);
                instance.Open();
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
        }
        /// <summary>
        /// get static MySqlConnection instance
        /// </summary>
        /// <returns>connection to the database</returns>
        public MySqlConnection GetConnection()
        {
            return instance;
        }
    }
}