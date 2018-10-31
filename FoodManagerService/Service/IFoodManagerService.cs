using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FoodManagerService.Modele;

namespace FoodManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IFoodManagerService
    {
        #region food CRUD

        [OperationContract]
        List<Food> GetFoodList(string userToken);
        [OperationContract]
        int? CreateFood(Food food, string userToken);
        [OperationContract]
        bool? UpdateFood(Food food, string userToken);
        [OperationContract]
        bool? DeleteFood(Food food, string userToken);

        #endregion

        #region recipe CRUD
        [OperationContract]
        int? CreateRecipe(Recipe recipe, string userToken);
        [OperationContract]
        List<Recipe> GetRecipesList(string userToken);
        [OperationContract]
        bool? UpdateRecipe(Recipe recipe, string userToken);
        [OperationContract]
        bool? DeleteRecipe(Recipe recipe, string userToken);
        #endregion
    }
}
