using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Client.AuthenticationServiceReference;
using Client.FoodManagerServiceReference;
using Client.Helper;
using Client.Model;
using Client.View;
using Client.ViewModel.MainWindowSubViewModel;
using UserService.Modele;

namespace Client.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        #region Ctor
        /// <summary>
        /// hydrate the user value
        /// create retryManager
        /// hydrade listFoodCategoryAndSubs from xml
        /// hydrate ObservableListFoods
        /// hydrate ObservableListFoods
        /// create Subviews
        /// </summary>
        /// <param name="mUser"></param>
        public MainWindowViewModel(User mUser)
        {
            User defaultUser = new User()//todo delete en production 
            {
                Login = "admin",
                Password = "admin",
                Token = "35476D40AD60D59A21A600B450C084DE"
            };
            User = mUser ?? defaultUser;

            retryManager = new RetryManager(userServiceClient,foodManagerServiceClient);
            ListFoodCategoryAndSubs = FoodCategoriesAndSubsLoader.GetCategoriesList(
                XElement.Load(@"C:\Users\cleme\source\repos\MyFoodStock\Client\Ressources\XML\Categories.xml"));

            ObservableListFoods = new ObservableCollection<Food>(retryManager.RetryGetFoodList(User));
            ObservableListRecipes = new ObservableCollection<Recipe>(retryManager.RetryGetRecipesList(User));
            SubViewDictionary = new Dictionary<string, MainWindowSubViewModelBase>()
            {
                {
                    "MyFoodstock",new MyFoodstockSubViewModel(ObservableListFoods,ListFoodCategoryAndSubs)
                    {
                        RemoveFoodCommand = DeleteFoodCommand,
                        UpdateFoodCommand = UpdateFoodCommand
                    }
                },
                {
                    "Recipes", new RecipesSubViewModel()
                    {
                        ListRecipes = ObservableListRecipes,
                        SelectedRecipe = ObservableListRecipes.First(),
                        OpenRecipeCommand = OpenRecipeCommand,
                        DeleteRecipeCommand = DeleteRecipeCommand,
                        UpdateRecipeCommand = UpdateRecipeCommand
                    }
                }
            };
        }
        #endregion

        #region Private Fields
        private User user;

        private ObservableCollection<Recipe> _listRecipes;
        private ObservableCollection<Food> _listFoods;
        private List<FoodCategoryAndSubs> _listFoodCategoryAndSubs;

        private Dictionary<string, MainWindowSubViewModelBase> _subViewDictionary;

        private readonly UserServiceClient userServiceClient = new UserServiceClient();
        private readonly FoodManagerServiceClient foodManagerServiceClient = new FoodManagerServiceClient();
        private readonly RetryManager retryManager;
        #endregion

        #region Properties

        #region SubView
        public Dictionary<string, MainWindowSubViewModelBase> SubViewDictionary
        {
            get => _subViewDictionary;
            set => _subViewDictionary = value;
        }
        #endregion

        #region Data
        public User User
        {
            get => user;
            set => user = value;
        }
        public ObservableCollection<Recipe> ObservableListRecipes
        {
            get => _listRecipes;
            set => _listRecipes = value;
        }
        public ObservableCollection<Food> ObservableListFoods
        {
            get => _listFoods;
            set => _listFoods = value;
        }
        public List<FoodCategoryAndSubs> ListFoodCategoryAndSubs
        {
            get => _listFoodCategoryAndSubs;
            set => _listFoodCategoryAndSubs = value;
        }
        #endregion

        #endregion

        #region RelayCommands

        private RelayCommand _createRecipeCommand;
        private RelayCommand _createFoodCommand;
        private RelayCommand _showAboutWindowCommand;
        private RelayCommand _deleteFoodCommand;
        private RelayCommand _deleteRecipeCommand;
        private RelayCommand _updateRecipeCommand;
        private RelayCommand _updateFoodCommand;
        private RelayCommand _openRecipeCommand;

        /// <summary>
        /// action : show Recipe window
        /// </summary>
        public RelayCommand OpenRecipeCommand
        {
            get
            {
                return _openRecipeCommand
                       ?? (_openRecipeCommand = new RelayCommand(
                           (o) =>
                           {
                               Recipe recipe = (Recipe)o;
                               new ShowRecipeWindow(recipe.TextXml).ShowDialog();
                           },
                           (o) => true));
            }
        }
        /// <summary>
        /// action : create addFoodWindow and retrieve food
        /// check if it succeed and add the food to collection and bdd
        /// </summary>
        public RelayCommand CreateFoodCommand
        {
            get
            {
                return _createFoodCommand
                       ?? (_createFoodCommand = new RelayCommand(
                           (o) =>
                           {
                               Food food = new AddFoodWindow(ListFoodCategoryAndSubs, null).ShowDialog();
                               if (food != null)
                               {
                                   int? id;
                                   if ((id=retryManager.RetryCreateFood(food, User)) != null)
                                   {
                                       food.Id = (int) id;
                                       ObservableListFoods.Add(food);
                                   }
                               }

                           },
                           (o) => true));
            }
        }
        /// <summary>
        /// create addRecipeWindow and retrieve recipe
        /// check if it succeed and add the recipe to collection and bdd
        /// </summary>
        public RelayCommand CreateRecipeCommand
        {
            get
            {
                return _createRecipeCommand
                    ?? (_createRecipeCommand = new RelayCommand(
                           (o) =>
                           {
                               Recipe recipe = new AddRecipeWindow().ShowDialog();
                               if (recipe != null)
                               {
                                   int? id;
                                   if ((id = retryManager.RetryCreateRecipe(recipe, User)) != null)
                                   {
                                       recipe.Id=(int)id;
                                       recipe.Author = User.Login;
                                       ObservableListRecipes.Add(recipe);
                                   }
                               }
                           },
                    (o) => true));
            }
        }
        /// <summary>
        /// show aboutWindow
        /// </summary>
        public RelayCommand ShowAboutWindowCommand
        {
            get
            {
                return _showAboutWindowCommand
                    ?? (_showAboutWindowCommand = new RelayCommand(
                           (o) =>
                           {
                               MessageBox.Show("This software was develloped as" +
                                               " a MA1 project for the \"technique de programmation\" course\n" +
                                               "©2018 Copyrights Clément Nicolas  all rights reserved ");
                           },
                    (o) => true));
            }
        }
        /// <summary>
        /// Delete Food from collection and bdd
        /// </summary>
        public RelayCommand DeleteFoodCommand
        {
            get
            {
                return _deleteFoodCommand
                    ?? (_deleteFoodCommand = new RelayCommand(
                    (o) =>
                    {
                        Food food =  (Food)o;
                        retryManager.RetryDeleteFood(food, User);
                        ObservableListFoods.Remove(food);
                    },
                    (o) => true));
            }
        }
        /// <summary>
        /// delete Recipe from collection and bdd
        /// </summary>
        public RelayCommand DeleteRecipeCommand
        {
            get
            {
                return _deleteRecipeCommand
                       ?? (_deleteRecipeCommand = new RelayCommand(
                           (o) =>
                           {
                               Recipe recipe = (Recipe)o;
                               retryManager.RetryDeleteRecipe(recipe, User);
                               ObservableListRecipes.Remove(recipe);
                               ((RecipesSubViewModel)SubViewDictionary["Recipes"]).SelectedRecipe =
                                   ObservableListRecipes.First();
                           },
                           (o) =>
                           {
                               return ((RecipesSubViewModel)SubViewDictionary["Recipes"]).SelectedRecipe.Author == user.Login;
                           }));
            }
        }
        /// <summary>
        /// action : update recipe from collection and bdd
        /// canexecute : only if the author is the user 
        /// </summary>
        public RelayCommand UpdateRecipeCommand
        {
            get
            {
                return _updateRecipeCommand
                    ?? (_updateRecipeCommand = new RelayCommand(
                    (o) =>
                    {
                        Recipe recipeSelected = (Recipe) o;
                        Recipe recipeFromWindow = new AddRecipeWindow(recipeSelected).ShowDialog();
                        if(recipeFromWindow != null)
                        {
                            if (retryManager.RetryUpdateRecipe(recipeFromWindow, user)==true)
                            {
                                ObservableListRecipes.Remove(ObservableListRecipes.First(x => x.Id == recipeSelected.Id));
                                ObservableListRecipes.Add(recipeFromWindow);
                                ((RecipesSubViewModel) SubViewDictionary["Recipes"]).SelectedRecipe = recipeFromWindow;
                            }
                        }
                    },
                    (o) =>
                    {
                        if(((RecipesSubViewModel)SubViewDictionary["Recipes"]).SelectedRecipe!=null)
                        {
                            return ((RecipesSubViewModel)SubViewDictionary["Recipes"]).SelectedRecipe.Author ==
                                   user.Login;
                        }

                        return false;
                    }));
            }
        }
        /// <summary>
        /// action : update food in the collection and in the bdd
        /// </summary>
        public RelayCommand UpdateFoodCommand
        {
            get
            {
                return _updateFoodCommand
                    ?? (_updateFoodCommand = new RelayCommand(
                    (o) =>
                    {
                        Food foodSelected = (Food)o;
                        Food foodFromWindow = new AddFoodWindow(ListFoodCategoryAndSubs,foodSelected).ShowDialog();
                        if (foodFromWindow != null)
                        {
                            if (retryManager.RetryUpdateFood(foodFromWindow, user) == true)
                            {
                                ObservableListFoods.Remove(ObservableListFoods.First(x => x.Id == foodSelected.Id));
                                ObservableListFoods.Add(foodFromWindow);
                            }
                        }
                    },
                    (o) => true));
            }
        }
        #endregion

        #region Nested class
        /// <summary>
        /// static class to store the function in order to load the list FoodCategoryAndSubs from XML
        /// </summary>
        private static class FoodCategoriesAndSubsLoader
        {
            public static List<FoodCategoryAndSubs> GetCategoriesList(XElement categoriesXML)
            {
                List<FoodCategoryAndSubs> categoriesList = new List<FoodCategoryAndSubs>();
                List<XElement> categoriesXElement = (from category in categoriesXML.Descendants() orderby category.Value select category).ToList();
                foreach (var category in categoriesXElement)
                {
                    Dictionary<string, int> subCategories = new Dictionary<string, int>();
                    var subCategoriesXElement = from subCategory in category.Attributes()
                        orderby subCategory.Name.ToString()
                        where subCategory.Name != "Id"
                        select subCategory;
                    foreach (var attribute in subCategoriesXElement)
                    {
                        subCategories.Add(attribute.Name.ToString(), int.Parse(attribute.Value));
                    }
                    categoriesList.Add(new FoodCategoryAndSubs()
                    {
                        Name = category.Value,
                        Id = int.Parse(category.Attribute("Id").Value.ToString()),
                        SubCategory = subCategories
                    });
                }
                return categoriesList;
            }
        }
        /// <summary>
        /// creation of a retry manager in order to make it transparent for the user when the token become invalid
        /// if the token is invalid it goes ask for a new one and retry
        /// </summary>
        private  class RetryManager
        {
            public RetryManager(UserServiceClient userServiceClient, FoodManagerServiceClient foodManagerServiceClient)
            {
                this.userServiceClient = userServiceClient;
                this.foodManagerServiceClient = foodManagerServiceClient;
            }

            private UserServiceClient userServiceClient;
            private FoodManagerServiceClient foodManagerServiceClient;

            #region food CRUD
            public Food[] RetryGetFoodList(User user)
            {
                var result = foodManagerServiceClient.GetFoodList(user.Token);
                if (result.Length==0)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result = foodManagerServiceClient.GetFoodList(user.Token);
                }
                return result;
            }

            public int? RetryCreateFood(Food food, User user)
            {
                var result = foodManagerServiceClient.CreateFood(food, user.Token);
                if (result == 0)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result= foodManagerServiceClient.CreateFood(food, user.Token);
                }
                return result;
            }

            public bool? RetryUpdateFood(Food food, User user)
            {
                var result = foodManagerServiceClient.UpdateFood(food, user.Token);
                if (result == false)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result = foodManagerServiceClient.UpdateFood(food, user.Token);
                }
                return result;
            }

            public bool? RetryDeleteFood(Food food, User user)
            {
                var result = foodManagerServiceClient.DeleteFood(food, user.Token);
                if (result == false)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result = foodManagerServiceClient.DeleteFood(food, user.Token);
                }
                return result;
            }

            #endregion

            #region recipe CRUD

            public int? RetryCreateRecipe(Recipe recipe, User user)
            {
                var result = foodManagerServiceClient.CreateRecipe(recipe, user.Token);
                if (result == 0)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result = foodManagerServiceClient.CreateRecipe(recipe, user.Token);
                }
                return result;
            }

            public Recipe[] RetryGetRecipesList(User user)
            {
                var result = foodManagerServiceClient.GetRecipesList(user.Token);
                if (result.Length == 0)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result = foodManagerServiceClient.GetRecipesList(user.Token);
                }
                return result;
            }

            public bool? RetryUpdateRecipe(Recipe recipe, User user)
            {
                var result = foodManagerServiceClient.UpdateRecipe(recipe, user.Token);
                if (result == false)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result = foodManagerServiceClient.UpdateRecipe(recipe, user.Token);
                }
                return result;
            }

            public bool? RetryDeleteRecipe(Recipe recipe, User user)
            {
                var result = foodManagerServiceClient.DeleteRecipe(recipe, user.Token);
                if (result == false)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result = foodManagerServiceClient.DeleteRecipe(recipe, user.Token);
                }
                return result;
            }
            #endregion
        }
        #endregion
    }
}
