using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodManagerService.Modele;
using MySql.Data.MySqlClient;

namespace FoodManagerService.Database
{
    public class FoodDAO:IDao<Food>
    {
        private  readonly MySqlConnection _connection;

        public FoodDAO(MySqlConnection connection)
        {
            _connection = connection;
        }

        public List<Food> GetList(int userId)
        {
            List<Food> outputList = new List<Food>();
            _connection.Open();
            MySqlCommand cmd = new MySqlCommand()
            {
                Connection = _connection,
                CommandText = "SELECT * FROM foodstuffs WHERE id_user = @id_user",
            };
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@id_user", userId);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                outputList.Add(new Food()
                {
                    Id = reader.GetInt32("id"),
                    ExpirationDate = reader.GetDateTime("expiration_date"),
                    IdCategory = reader.GetInt32("id_category"),
                    Name = reader.GetString("name"),
                    Unit = reader.GetString("unit"),
                    Quantity = reader.GetInt32("quantity"),
                    Price = reader.GetInt32("price")
                });
            }
            _connection.Close();
            return outputList;
        }

        public int Create(Food food, int userId)
        {
            _connection.Open();
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = _connection,
                CommandText =
                    "INSERT INTO  foodstuffs ( id_category ,  name ,  quantity ,  unit ,  id_user ,  expiration_date, price )" +
                    " VALUES (@id_category ,  @name ,  @quantity ,  @unit ,  @id_user ,  @expiration_date, @price)"
            };
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@id_category", food.IdCategory);
            cmd.Parameters.AddWithValue("@name", food.Name);
            cmd.Parameters.AddWithValue("@quantity", food.Quantity);
            cmd.Parameters.AddWithValue("@unit", food.Unit);
            cmd.Parameters.AddWithValue("@id_user", userId);
            cmd.Parameters.AddWithValue("@expiration_date", food.ExpirationDate);
            cmd.Parameters.AddWithValue("@price", food.Price);
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            int id = int.Parse(cmd.ExecuteScalar().ToString());
            _connection.Close();
            return id;
        }

        public void Update(Food food, int userId)
        {
            _connection.Open();
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = _connection,
                CommandText = "UPDATE  foodstuffs  SET  id_category = @id_category" +
                              ", name = @name" +
                              ", quantity = @quantity" +
                              ", unit = @unit" +
                              ", expiration_date = @expiration_date" +
                              ", price = @price" +
                              " WHERE id =  @id AND id_user = @id_user"
            };
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@id_category", food.IdCategory);
            cmd.Parameters.AddWithValue("@name", food.Name);
            cmd.Parameters.AddWithValue("@quantity", food.Quantity);
            cmd.Parameters.AddWithValue("@unit", food.Unit);
            cmd.Parameters.AddWithValue("@expiration_date", food.ExpirationDate);
            cmd.Parameters.AddWithValue("@price", food.Price);
            cmd.Parameters.AddWithValue("@id", food.Id);
            cmd.Parameters.AddWithValue("@id_user", userId);
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public void Delete(Food food, int userId)
        {
            _connection.Open();
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = _connection,
                CommandText = "DELETE FROM foodstuffs WHERE id = @id and id_user = @id_user"
            };
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@id", food.Id);
            cmd.Parameters.AddWithValue("@id_user", userId);
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }
}