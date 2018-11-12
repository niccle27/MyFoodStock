using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.FoodManagerServiceReference;
using Client.Helper;
using Client.Model;

namespace Client.ViewModel
{
    public class AddFoodWindowViewModel : ViewModelBase
    {

        #region Ctor

        public AddFoodWindowViewModel(List<FoodCategoryAndSubs> listFoodCategoryAndSubs, Food foodOutputReference)
        {
            _listFoodCategoryAndSubs = listFoodCategoryAndSubs;
            if (foodOutputReference != null)
            {
                _food = foodOutputReference;
            }
            else
            {
                _food = new Food();
            }
            _food.ExpirationDate = DateTime.Now;
            SelectedCategory = ListFoodCategoryAndSubs.First();
            SelectedSubCategory = SelectedCategory.SubCategory.First();
        }
        public AddFoodWindowViewModel()
        {

        }

        #endregion

        #region Private Fields

        private List<FoodCategoryAndSubs> _listFoodCategoryAndSubs;

        private FoodCategoryAndSubs _selectedCategory;

        private KeyValuePair<string, int> _selectedSubCategory;

        private Food _output;
        private Food _food;

        #endregion

        #region Properties

        public Food Output
        {
            get => _output;
            set => _output = value;
        }

        public List<FoodCategoryAndSubs> ListFoodCategoryAndSubs
        {
            get => _listFoodCategoryAndSubs;
            set => _listFoodCategoryAndSubs = value;
        }

        public FoodCategoryAndSubs SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                IdCategory = _selectedCategory.Id;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public KeyValuePair<string, int> SelectedSubCategory
        {
            get => _selectedSubCategory;
            set
            {
                _selectedSubCategory = value;
                IdSubCategory = _selectedSubCategory.Value;
                OnPropertyChanged(nameof(SelectedSubCategory));
            }
        }
        public string Name
        {
            get => _food.Name;
            set
            {
                _food.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Quantity
        {
            get => _food.Quantity;
            set
            {
                _food.Quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public DateTime ExpirationDate
        {
            get => _food.ExpirationDate;
            set
            {
                _food.ExpirationDate = value;
                OnPropertyChanged(nameof(ExpirationDate));
            }
        }
        public int Price
        {
            get => _food.Price;
            set
            {
                _food.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public string Unit
        {
            get => _food.Unit;
            set
            {
                _food.Unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }
        public int IdCategory
        {
            get => _food.IdCategory;
            set
            {
                _food.IdCategory = value;
                OnPropertyChanged(nameof(IdCategory));
            }
        }
        public int IdSubCategory
        {
            get => _food.IdSubCategory;
            set
            {
                _food.IdSubCategory = value;
                OnPropertyChanged(nameof(IdSubCategory));
            }
        }

        #endregion

        #region RelayCommands

        private RelayCommand _createFood;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand CreateFoodCommand
        {
            get
            {
                return _createFood
                       ?? (_createFood = new RelayCommand(
                           (o) =>
                           {
                               Output = _food;
                               CloseWindow();
                           },
                           (o) =>
                           {
                               if(!String.IsNullOrWhiteSpace(Name) &&
                                  Quantity != 0 &&
                                  ExpirationDate >= DateTime.Now &&
                                  //Price !=0 && //TODO décommenter si on veut ajouter les prix
                                  IdCategory != 0 &&
                                  IdSubCategory != 0)return true;
                               else
                               {
                                   return false;
                               }
                           }));
            }
        }

        #endregion
    }
}
