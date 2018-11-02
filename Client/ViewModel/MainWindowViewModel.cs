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
using Client.ViewModel.MainWindowSubViewModel;
using UserService.Modele;

namespace Client.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {

        #region private fields
        FoodCategoriesAndSubsLoader _foodCategoriesAndSubsLoader = new FoodCategoriesAndSubsLoader();

        private User user;

        private UserServiceClient _userServiceClient = new UserServiceClient();
        private FoodManagerServiceClient _foodManagerServiceClient = new FoodManagerServiceClient();

        private ObservableCollection<Recipe> _listRecipes;
        private ObservableCollection<Food> _listFoods;
        private List<FoodCategoryAndSubs> _listFoodCategoryAndSubs;



        private Dictionary<string, MainWindowSubViewModelBase> _subViewDictionary;

        public MainWindowViewModel(User mUser)
        {
            User=new User()
            {
                Login = "admin",
                Password = "admin",
                Token = "D9812AB5CCF25FC28FC4985BB9D75685"
            };
            ListFoodCategoryAndSubs = _foodCategoriesAndSubsLoader.GetCategoriesList(
                XElement.Load(@"C:\Users\cleme\source\repos\MyFoodStock\Client\Ressources\XML\Categories.xml"));

            var list = FoodManagerServiceClient.GetRecipesList(User.Token);
            ObservableListFoods = new ObservableCollection<Food>(FoodManagerServiceClient.GetFoodList(User.Token));
            ObservableListRecipes = new ObservableCollection<Recipe>(list);
            SubViewDictionary=new Dictionary<string, MainWindowSubViewModelBase>()
            {
                {
                    "MyFoodstock",new MyFoodstockSubViewModel()
                    {
                        ListFoods = ObservableListFoods,
                        ListFoodCategoryAndSubs = ListFoodCategoryAndSubs
                    }
                },
                {
                    "Recipes", new RecipesSubViewModel()
                    {
                        ListRecipes = ObservableListRecipes
                    }
                }
            };
        }
        //Dictionary<string, InterfaceViewModel> _interfaceList = new Dictionary<string, InterfaceViewModel>();

        
        
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
                               MessageBox.Show("CreateFoodCommand");

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
                               MessageBox.Show("CreateRecipeCommand");

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
                List<XElement> categoriesXElement = (from category in categoriesXML.Descendants() select category).ToList();
                foreach (var category in categoriesXElement)
                {
                    Dictionary<string, int> subCategories = new Dictionary<string, int>();
                    var subCategoriesXElement = from subCategory in category.Attributes()
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

        #endregion
    }
}
