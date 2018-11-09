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
        public AddFoodWindowViewModel(List<FoodCategoryAndSubs> listFoodCategoryAndSubs, Food foodOutputReference)
        {
            _listFoodCategoryAndSubs = listFoodCategoryAndSubs;
            if (foodOutputReference != null)
            {
                Food = foodOutputReference;
            }
            else
            {
                Food=new Food();
            }
            food.ExpirationDate = DateTime.Now;
            SelectedCategory = ListFoodCategoryAndSubs.First();
            SelectedSubCategory = SelectedCategory.SubCategory.First();
        }

        private List<FoodCategoryAndSubs> _listFoodCategoryAndSubs;

        private FoodCategoryAndSubs _selectedCategory;

        private KeyValuePair<string, int> _selectedSubCategory;
        private Food food;

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

        public AddFoodWindowViewModel()
        {
            
        }


        public string Name
        {
            get => food.Name;
            set
            {
                food.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Quantity
        {
            get => food.Quantity;
            set
            {
                food.Quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public DateTime ExpirationDate
        {
            get => food.ExpirationDate;
            set
            {
                food.ExpirationDate = value;
                OnPropertyChanged(nameof(ExpirationDate));
            }
        }

        public int Price
        {
            get => food.Price;
            set
            {
                food.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string Unit
        {
            get => food.Unit;
            set
            {
                food.Unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }

        public int IdCategory
        {
            get => food.IdCategory;
            set
            {
                food.IdCategory = value;
                OnPropertyChanged(nameof(IdCategory));
            }
        }
        public int IdSubCategory
        {
            get => food.IdSubCategory;
            set
            {
                food.IdSubCategory = value;
                OnPropertyChanged(nameof(IdSubCategory));
            }
        }


        public Food Food
        {
            get => food;
            set => food = value;
        }

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
    }
}
