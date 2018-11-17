using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Client.FoodManagerServiceReference;
using Client.Helper;
using Client.Model;

namespace Client.ViewModel.MainWindowSubViewModel
{
    public class MyFoodstockSubViewModel:MainWindowSubViewModelBase
    {
        #region Ctor
        /// <summary>
        /// associate ListFoods and ListFoodCategoryAndSubs and the SelectedCategory
        /// and SelectedCategory
        /// </summary>
        /// <param name="listFoods">list of foods from mainwindow</param>
        /// <param name="listFoodCategoryAndSubs">created from xml </param>
        public MyFoodstockSubViewModel(ObservableCollection<Food> listFoods, List<FoodCategoryAndSubs> listFoodCategoryAndSubs)
        {
            ListFoods = listFoods;
            ListFoodCategoryAndSubs = listFoodCategoryAndSubs;
            SelectedCategory = ListFoodCategoryAndSubs.First();
            SelectedSubCategory = SelectedCategory.SubCategory.First();
        }

        #endregion

        #region Private Fields

        private FoodCategoryAndSubs _selectedCategory;
        private KeyValuePair<string,int> _selectedSubCategory;
        private Food _selectedFood;

        private ObservableCollection<Food> _listFoods;
        private List<FoodCategoryAndSubs> _listFoodCategoryAndSubs;

        #endregion


        #region Properties

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

        public Food SelectedFood
        {
            get => _selectedFood;
            set
            {
                _selectedFood = value;
                OnPropertyChanged(nameof(SelectedFood));
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

        #endregion

        #region RelayCommands

        private RelayCommand _removeFoodCommand;
        private RelayCommand _updateFoodCommand;

        public RelayCommand UpdateFoodCommand
        {
            get => _updateFoodCommand;
            set => _updateFoodCommand = value;
        }

        public RelayCommand RemoveFoodCommand
        {
            get => _removeFoodCommand;
            set => _removeFoodCommand = value;
        }

        #endregion

        #region Filter
        /// <summary>
        /// apply filtering to the view based on the combobox
        /// </summary>
        /// <param name="listFoods"></param>
        private void ApplyFilterOnView(ObservableCollection<Food> listFoods)
        {
            CollectionView view = (CollectionView) CollectionViewSource.GetDefaultView(listFoods);
            view.Filter = FoodFilter;
        }
        private bool FoodFilter(object item)
        {
            if (item is Food food)
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
