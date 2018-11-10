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
        public MainWindowViewModel(User mUser)
        {
            User defaultUser = new User()//todo delete en production 
            {
                Login = "admin",
                Password = "admin",
                Token = "5FB1DB6AC20D391183AFDAD68E1E74D0"
            };
            User = (mUser==null)?defaultUser:mUser;
            ListFoodCategoryAndSubs = _foodCategoriesAndSubsLoader.GetCategoriesList(
                XElement.Load(@"C:\Users\cleme\source\repos\MyFoodStock\Client\Ressources\XML\Categories.xml"));

            ObservableListFoods = new ObservableCollection<Food>(_retryManager.RetryGetFoodList(User,FoodManagerServiceClient,UserServiceClient));
            ObservableListRecipes = new ObservableCollection<Recipe>(_retryManager.RetryGetRecipesList(User, FoodManagerServiceClient, UserServiceClient));
            SubViewDictionary = new Dictionary<string, MainWindowSubViewModelBase>()
            {
                {
                    "MyFoodstock",new MyFoodstockSubViewModel(ObservableListFoods,ListFoodCategoryAndSubs)
                },
                {
                    "Recipes", new RecipesSubViewModel()
                    {
                        ListRecipes = ObservableListRecipes
                    }
                }
            };
        }
        #region private fields
        FoodCategoriesAndSubsLoader _foodCategoriesAndSubsLoader = new FoodCategoriesAndSubsLoader();

        private User user;

        private UserServiceClient _userServiceClient = new UserServiceClient();
        private FoodManagerServiceClient _foodManagerServiceClient = new FoodManagerServiceClient();

        private ObservableCollection<Recipe> _listRecipes;
        private ObservableCollection<Food> _listFoods;
        private List<FoodCategoryAndSubs> _listFoodCategoryAndSubs;

        private Dictionary<string, MainWindowSubViewModelBase> _subViewDictionary;

        private RetryManager _retryManager = new RetryManager();
        #endregion

        #region Services
        public UserServiceClient UserServiceClient
        {
            get => _userServiceClient;
            set => _userServiceClient = value;
        }
        public FoodManagerServiceClient FoodManagerServiceClient
        {
            get => _foodManagerServiceClient;
            set => _foodManagerServiceClient = value;
        }
        #endregion

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

        #region RelayCommand

        private RelayCommand createRecipeCommand;
        private RelayCommand createFoodCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand CreateFoodCommand
        {
            get
            {
                return createFoodCommand
                       ?? (createFoodCommand = new RelayCommand(
                           (o) =>
                           {
                               Food food = new AddFoodWindow(ListFoodCategoryAndSubs, null).ShowDialog();
                               if (food != null)
                               {
                                   int? id;
                                   if ((id=_retryManager.RetryCreateFood(food, User, FoodManagerServiceClient,
                                           UserServiceClient)) != null)
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
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand CreateRecipeCommand
        {
            get
            {
                return createRecipeCommand
                    ?? (createRecipeCommand = new RelayCommand(
                           (o) =>
                           {
                               Recipe recipe = new AddRecipeWindow().ShowDialog();
                               if (recipe != null)
                               {
                                   int? id;
                                   if ((id = _retryManager.RetryCreateRecipe(recipe, User, FoodManagerServiceClient,
                                           UserServiceClient)) != null)
                                   {
                                       recipe.Id=(int)id;
                                       ObservableListRecipes.Add(recipe);
                                   }
                               }
                           },
                    (o) => true));
            }
        }

        private RelayCommand showAboutWindowCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand ShowAboutWindowCommand
        {
            get
            {
                return showAboutWindowCommand
                    ?? (showAboutWindowCommand = new RelayCommand(
                           (o) =>
                           {
                               MessageBox.Show("This software was develloped as" +
                                               " a MA1 project for the \"technique de programmation\" course\n" +
                                               "©2018 Copyrights Clément Nicolas  all rights reserved ");
                           },
                    (o) => true));
            }
        }
        #endregion

        #region nested class

        private  class FoodCategoriesAndSubsLoader
        {
            public List<FoodCategoryAndSubs> GetCategoriesList(XElement categoriesXML)
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

        private  class RetryManager
        {
            #region food CRUD
            public Food[] RetryGetFoodList(User user, FoodManagerServiceClient foodManagerServiceClient, UserServiceClient userServiceClient)
            {
                var result = foodManagerServiceClient.GetFoodList(user.Token);
                if (result.Length==0)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result = foodManagerServiceClient.GetFoodList(user.Token);
                }

                return result;
            }

            public int? RetryCreateFood(Food food, User user, FoodManagerServiceClient foodManagerServiceClient, UserServiceClient userServiceClient)
            {
                var result = foodManagerServiceClient.CreateFood(food, user.Token);
                if (result == 0)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result= foodManagerServiceClient.CreateFood(food, user.Token);
                }

                return result;
            }

            public bool? RetryUpdateFood(Food food, User user, FoodManagerServiceClient foodManagerServiceClient,
                UserServiceClient userServiceClient)
            {
                var result = foodManagerServiceClient.UpdateFood(food, user.Token);
                if (result == false)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result = foodManagerServiceClient.UpdateFood(food, user.Token);
                }

                return result;
            }

            public bool? RetryDeleteFood(Food food, User user, FoodManagerServiceClient foodManagerServiceClient,
                UserServiceClient userServiceClient)
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

            public int? RetryCreateRecipe(Recipe recipe, User user, FoodManagerServiceClient foodManagerServiceClient,
                UserServiceClient userServiceClient)
            {
                var result = foodManagerServiceClient.CreateRecipe(recipe, user.Token);
                if (result == 0)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result = foodManagerServiceClient.CreateRecipe(recipe, user.Token);
                }

                return result;
            }

            public Recipe[] RetryGetRecipesList(User user, FoodManagerServiceClient foodManagerServiceClient,
                UserServiceClient userServiceClient)
            {
                var result = foodManagerServiceClient.GetRecipesList(user.Token);
                if (result.Length == 0)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result = foodManagerServiceClient.GetRecipesList(user.Token);
                }

                return result;
            }

            public bool? RetryUpdateRecipe(Recipe recipe, User user, FoodManagerServiceClient foodManagerServiceClient,
                UserServiceClient userServiceClient)
            {
                var result = foodManagerServiceClient.UpdateRecipe(recipe, user.Token);
                if (result == false)
                {
                    user.Token = userServiceClient.Connect(user.Login, user.Password);
                    result = foodManagerServiceClient.UpdateRecipe(recipe, user.Token);
                }

                return result;
            }

            public bool? RetryDeleteRecipe(Recipe recipe, User user, FoodManagerServiceClient foodManagerServiceClient,
                UserServiceClient userServiceClient)
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
