using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FoodManagerService.Modele;
using MySql.Data.MySqlClient;

namespace FoodManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FoodManagerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FoodManagerService.svc or FoodManagerService.svc.cs at the Solution Explorer and start debugging.
    public class FoodManagerService : IFoodManagerService
    {
        readonly MySqlConnection _dbConnection = new DbConnection().GetConnection();
        public void Update(Food food)
        {
            throw new NotImplementedException();
        }

        public void Delete(Food food)
        {

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT login from users";
            //cmd.Parameters.AddWithValue("@test", "login");
            cmd.Connection = _dbConnection;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString("login"));
            }
            _dbConnection.Close();
            Console.ReadKey();
        }
    }
}
