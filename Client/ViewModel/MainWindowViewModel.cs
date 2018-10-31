using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.AuthenticationServiceReference;
using Client.FoodManagerServiceReference;
using Client.Model;

namespace Client.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        #region private fields
        private ObservableCollection<Recipe> listRecipes;
        private ObservableCollection<Food> listFoods;
        private User user;

        private UserServiceClient userServiceClient = new UserServiceClient();
        private FoodManagerServiceClient foodManagerServiceClient = new FoodManagerServiceClient();

        #endregion

        #region Services
        public UserServiceClient UserServiceClient
        {
            get => userServiceClient;
            set => userServiceClient = value;
        }
        public FoodManagerServiceClient FoodManagerServiceClient
        {
            get => foodManagerServiceClient;
            set => foodManagerServiceClient = value;
        }
        #endregion

        #region Data
        public User User
        {
            get => user;
            set => user = value;
        }
        public ObservableCollection<Recipe> ListRecipes
        {
            get => listRecipes;
            set => listRecipes = value;
        }
        public ObservableCollection<Food> ListFoods
        {
            get => listFoods;
            set => listFoods = value;
        }
        #endregion

        public MainWindowViewModel()
        {
            
        }
    }
}
