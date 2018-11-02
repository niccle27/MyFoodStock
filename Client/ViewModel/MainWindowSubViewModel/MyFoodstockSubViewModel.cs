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
        private FoodCategoryAndSubs _selectedCategory;
        private KeyValuePair<string,int> _selectedSubCategory;
        private ObservableCollection<Food> _listFoods;
        private List<FoodCategoryAndSubs> _listFoodCategoryAndSubs;
        //TODO filtering : https://dzone.com/articles/filtering-mvvm-architecture
        public FoodCategoryAndSubs SelectedCategory
        {
            get => _selectedCategory;
            set => _selectedCategory = value;
        }

        public KeyValuePair<string, int> SelectedSubCategory
        {
            get => _selectedSubCategory;
            set => _selectedSubCategory = value;
        }


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
