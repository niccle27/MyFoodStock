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
        /// <summary>
        /// in case of update
        /// deep copy foodOutputReference into _food
        /// hydrate listFoodCategoryAndSubs
        /// set validationButtonContent to update
        /// set current category and sub
        /// in case of create
        /// create a food and set expiration date as today
        /// set the category and sub to first value each
        /// </summary>
        /// <param name="listFoodCategoryAndSubs"></param>
        /// <param name="foodOutputReference"></param>
        public AddFoodWindowViewModel(List<FoodCategoryAndSubs> listFoodCategoryAndSubs, Food foodOutputReference)
        {
            _listFoodCategoryAndSubs = listFoodCategoryAndSubs;
            if (foodOutputReference != null)
            {
                _food = new Food()
                {
                    IdCategory = foodOutputReference.IdCategory,
                    Id = foodOutputReference.Id,
                    IdSubCategory = foodOutputReference.IdSubCategory,
                    ExpirationDate = foodOutputReference.ExpirationDate,
                    Quantity = foodOutputReference.Quantity,
                    Price = foodOutputReference.Price,
                    Name = foodOutputReference.Name,
                    Unit = foodOutputReference.Unit
                };
                ButtonValidateContent = "Update";
                _food.ExpirationDate = foodOutputReference.ExpirationDate;
                SelectedCategory = (from category in listFoodCategoryAndSubs
                    where category.Id == foodOutputReference.IdCategory
                    select category).First();
                SelectedSubCategory = (from subCategory in SelectedCategory.SubCategory
                    where subCategory.Value == foodOutputReference.IdSubCategory
                    select subCategory).First();
            }
            else
            {
                _food = new Food();
                ButtonValidateContent = "Create";
                _food.ExpirationDate = DateTime.Now;
                SelectedCategory = ListFoodCategoryAndSubs.First();
                SelectedSubCategory = SelectedCategory.SubCategory.First();
            }

        }
        #endregion

        #region Private Fields

        private List<FoodCategoryAndSubs> _listFoodCategoryAndSubs;

        private FoodCategoryAndSubs _selectedCategory;
        private KeyValuePair<string, int> _selectedSubCategory;

        private Food _output;
        private Food _food;

        private string _buttonValidateContent;


        #endregion

        #region Properties

        public string ButtonValidateContent
        {
            get => _buttonValidateContent;
            set => _buttonValidateContent = value;
        }
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
        /// action set output to food created by the window and then close the window
        /// canexecute check whether each field is not empty
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
