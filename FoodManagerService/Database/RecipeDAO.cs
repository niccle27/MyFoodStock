using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodManagerService.Modele;
using MySql.Data.MySqlClient;

namespace FoodManagerService.Database
{
    // ReSharper disable once InconsistentNaming
    public class RecipeDAO:IDao<Recipe>
    {
        private readonly MySqlConnection _connection;

        public RecipeDAO(MySqlConnection connection)
        {
            _connection = connection;
        }
        public List<Recipe> GetList(int idUser)
        {
            List<Recipe> outputList = new List<Recipe>();
            _connection.Open();
            MySqlCommand cmd = new MySqlCommand()
            {
                Connection = _connection,
                CommandText = "SELECT recipes.id AS id," +
                              "title," +
                              "text_xml," +
                              "picture_path," +
                              "login AS author" +
                              " FROM recipes LEFT JOIN users ON recipes.id_user = users.id"
            };
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                outputList.Add(new Recipe()
                {
                    Author = reader.GetString("author"),
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    ImagePath = reader.GetString("picture_path"),
                    TextXml = reader.GetString("text_xml")
                });
            }
            _connection.Close();
            return outputList;
        }

        public int Create(Recipe recipe, int idUser)
        {
            _connection.Open();
            MySqlCommand cmd = new MySqlCommand()
            {
                Connection = _connection,
                CommandText = "INSERT INTO recipes(title, text_xml, picture_path, id_user)" +
                              " VALUES (@title, @text_xml, @picture_path, @id_user)"
            };
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@title", recipe.Title);
            cmd.Parameters.AddWithValue("@text_xml", recipe.TextXml);
            cmd.Parameters.AddWithValue("@picture_path", recipe.ImagePath);
            cmd.Parameters.AddWithValue("@id_user", idUser);
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            int id = int.Parse(cmd.ExecuteScalar().ToString());
            _connection.Close();
            return id;
        }

        public void Update(Recipe recipe,int idUser)
        {
            _connection.Open();
            MySqlCommand cmd = new MySqlCommand()
            {
                Connection = _connection,
                CommandText = "UPDATE recipes SET " +
                              "title = @title" +
                              "text_xml = @text_xml" +
                              ",picture_path = @picture_path" +
                              " WHERE id = @id AND id_user = @id_user"
            };
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@title", recipe.Title);
            cmd.Parameters.AddWithValue("@text_xml", recipe.TextXml);
            cmd.Parameters.AddWithValue("@picture_path", recipe.ImagePath);
            cmd.Parameters.AddWithValue("@id", recipe.Id);
            cmd.Parameters.AddWithValue("@id_user", idUser);
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public void Delete(Recipe recipe, int idUser)
        {
            _connection.Open();
            MySqlCommand cmd = new MySqlCommand()
            {
                Connection = _connection,
                CommandText = "DELETE FROM recipes WHERE id = @id AND id_user = @id_user"
            };
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@id", recipe.Id);
            cmd.Parameters.AddWithValue("@id_user", idUser);
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }
}