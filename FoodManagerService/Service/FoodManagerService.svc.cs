using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FoodManagerService.Database;
using FoodManagerService.Modele;
using MySql.Data.MySqlClient;

namespace FoodManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FoodManagerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FoodManagerService.svc or FoodManagerService.svc.cs at the Solution Explorer and start debugging.
    public class FoodManagerService : IFoodManagerService
    {
        readonly FoodDAO _foodDao = new FoodDAO(DbConnection.CreateConnection());
        readonly RecipeDAO _recipeDao = new RecipeDAO(DbConnection.CreateConnection());
        readonly ValidatorCRUD<Food> _foodValidatorCrud = new ValidatorCRUD<Food>();
        readonly ValidatorCRUD<Recipe> _recipeValidatorCrud = new ValidatorCRUD<Recipe>();
        /// <summary>
        /// get the userId that match the token, return null if not found,0 if token outdated
        /// </summary>
        /// <param name="userToken"></param>
        /// <returns>return the userId that match the token, return null if not found,0 if token outdated</returns>
        static int? GetUserId(string userToken)//Todo intégré ceci à un objet ou utiliser le service
        {
            int? userId = null;
            MySqlConnection connection = DbConnection.CreateConnection();
            connection.Open();
            MySqlCommand cmd = new MySqlCommand()
            {
                Connection = connection,
                CommandText = "SELECT id, tokenValidTimeStamp FROM users where token = @token"
            };
            cmd.Parameters.AddWithValue("@token", userToken);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                userId = reader.GetDateTime("tokenValidTimeStamp") > DateTime.Now ? reader.GetInt32("id") : 0;
            }
            connection.Close();
            return userId;
        }

        #region Food CRUD
        /// <summary>
        /// insert food into database if usertoken is valid
        /// </summary>
        /// <param name="food"></param>
        /// <param name="userToken"></param>
        /// <returns>return id if succeed , 0 if token outdated and null if fake token</returns>
        public int? CreateFood(Food food, string userToken)
        {
            int? userId = GetUserId(userToken);
            return _foodValidatorCrud.ValidCreate(food, userId, _foodDao);

        }
        /// <summary>
        /// get food list if userid linked to that token
        /// </summary>
        /// <param name="userToken"></param>
        /// <returns>list food if usertoken match and is valid, 0 if token outdated and null if fake token</returns>
        public List<Food> GetFoodList(string userToken)
        {
            int? userId = GetUserId(userToken);
            return _foodValidatorCrud.ValidGetList(userId, _foodDao);
        }
        /// <summary>
        /// update food 
        /// </summary>
        /// <param name="food"></param>
        /// <param name="userToken"></param>
        /// <returns>return true if succeed, false if token outdated and null if faketoken or token don't match food userid</returns>
        public bool? UpdateFood(Food food, string userToken)
        {
            int? userId = GetUserId(userToken);
            return _foodValidatorCrud.ValidUpdate(food, userId, _foodDao);
        }
        /// <summary>
        /// delete food
        /// </summary>
        /// <param name="food"></param>
        /// <param name="userToken"></param>
        /// <returns>true if succeed, 0 if token outdated and null if fake token or token don't match userid</returns>
        public bool? DeleteFood(Food food, string userToken)
        {
            int? userId = GetUserId(userToken);
            return _foodValidatorCrud.ValidDelete(food, userId, _foodDao);
        }

        #endregion

        #region Recipe CRUD
        /// <summary>
        /// insert Recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="userToken"></param>
        /// <returns>id if succeed, 0 if token outdated , null if fake token</returns>
        public int? CreateRecipe(Recipe recipe, string userToken)
        {
            int? userId = GetUserId(userToken);
            return _recipeValidatorCrud.ValidCreate(recipe, userId, _recipeDao);
        }
        /// <summary>
        /// get recipeList
        /// </summary>
        /// <param name="userToken"></param>
        /// <returns>return id if succeed, 0 if token outdated and null if fake token</returns>
        public List<Recipe> GetRecipesList(string userToken)
        {
            int? userId = GetUserId(userToken);
            return _recipeValidatorCrud.ValidGetList(userId, _recipeDao);
        }
        /// <summary>
        /// update recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="userToken"></param>
        /// <returns>return true if succeed, false if token outdated and null if fake token</returns>
        public bool? UpdateRecipe(Recipe recipe, string userToken)
        {
            int? userId = GetUserId(userToken);
            return _recipeValidatorCrud.ValidUpdate(recipe, userId, _recipeDao);
        }
        /// <summary>
        /// delete recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="userToken"></param>
        /// <returns>return true if succeed, false if token outdated and null if fake token</returns>
        public bool? DeleteRecipe(Recipe recipe, string userToken)
        {
            int? userId = GetUserId(userToken);
            return _recipeValidatorCrud.ValidDelete(recipe, userId, _recipeDao);
        }



        #endregion

        #region Nested Class
        /// <summary>
        /// validate if crud action succeed or not
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class ValidatorCRUD<T> where  T : new()
        {
            public int? ValidCreate(T item, int? userId, IDao<T> dao)
            {
                switch (userId)
                {
                    case 0:
                        return userId;
                    case null:
                        return null;
                    default:
                        return dao.Create(item, (int)userId);
                }

            }

            public List<T> ValidGetList(int? userId, IDao<T> dao)
            {
                switch (userId)
                {
                    case 0:
                        return new List<T>();
                    case null:
                        return null;
                    default:
                        return dao.GetList((int)userId);
                }
            }

            public bool? ValidUpdate(T item, int? userId, IDao<T> dao)
            {
                switch (userId)
                {
                    case 0:
                        return false;
                    case null:
                        return null;
                    default:
                        dao.Update(item, (int)userId);
                        return true;
                }
            }

            public bool? ValidDelete(T item, int? userId, IDao<T> dao)
            {
                switch (userId)
                {
                    case 0:
                        return false;
                    case null:
                        return null;
                    default:
                        dao.Delete(item, (int)userId);
                        return true;
                }
            }
        }
        #endregion
        
    }
}
