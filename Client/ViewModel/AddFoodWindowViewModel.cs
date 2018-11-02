using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.FoodManagerServiceReference;
using Client.Helper;

namespace Client.ViewModel
{
    public class AddFoodWindowViewModel : ViewModelBase//TODO finish this after updated dao food
    {
        private Food _foodOutputReference;

        public Food FoodOutputReference
        {
            get => _foodOutputReference;
            set => _foodOutputReference = value;
        }

        /*public AddFoodWindowViewModel(Food mainViewModelFood)
        {
            MainViewModelFood = mainViewModelFood;
        }*/
        private Food food = new Food();

        public AddFoodWindowViewModel()
        {
            food.ExpirationDate=DateTime.Now;
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

                    },
                    (o) =>
                    {
                        if(String.IsNullOrWhiteSpace(Name) &&
                          Quantity != 0 &&
                          ExpirationDate < DateTime.Now &&
                          Price !=0 &&
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
