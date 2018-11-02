using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.FoodManagerServiceReference;
using Client.Model;

namespace Client.ViewModel.MainWindowSubViewModel
{
    public class MyFoodstockSubViewModel:MainWindowSubViewModelBase
    {
        private ObservableCollection<Food> _listFoods;
        private List<FoodCategoryAndSubs> _listFoodCategoryAndSubs;

        public List<FoodCategoryAndSubs> ListFoodCategoryAndSubs
        {
            get => _listFoodCategoryAndSubs;
            set => _listFoodCategoryAndSubs = value;
        }

        public ObservableCollection<Food> ListFoods
        {
            get => _listFoods;
            set => _listFoods = value;
        }
    }
}
