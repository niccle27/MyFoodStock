using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
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

        public MyFoodstockSubViewModel(ObservableCollection<Food> listFoods, List<FoodCategoryAndSubs> listFoodCategoryAndSubs)
        {
            ListFoods = listFoods;
            ListFoodCategoryAndSubs = listFoodCategoryAndSubs;
            SelectedCategory = ListFoodCategoryAndSubs.First();
            SelectedSubCategory = SelectedCategory.SubCategory.First();
        }
        //TODO filtering : https://dzone.com/articles/filtering-mvvm-architecture
        public FoodCategoryAndSubs SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                ApplyFilterOnView(ListFoods);
            }
        }

        public KeyValuePair<string, int> SelectedSubCategory
        {
            get => _selectedSubCategory;
            set
            {
                _selectedSubCategory = value;
                ApplyFilterOnView(ListFoods);
            }
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

        #region Filter

        private void ApplyFilterOnView(ObservableCollection<Food> listFoods)
        {
            CollectionView view = (CollectionView) CollectionViewSource.GetDefaultView(listFoods);
            view.Filter = FoodFilter;
        }
        private bool FoodFilter(object item)
        {
            var food = item as Food;
            if (food != null)
            {
                if (food.IdCategory == SelectedCategory.Id && food.IdSubCategory == SelectedSubCategory.Value)
                    return true;
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
