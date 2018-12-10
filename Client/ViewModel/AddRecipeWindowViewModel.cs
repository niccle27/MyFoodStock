using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;
using Client.FoodManagerServiceReference;
using Client.Helper;

namespace Client.ViewModel
{
    public class AddRecipeWindowViewModel:ViewModelBase
    {
        #region ctor
        /// <summary>
        /// deep copy recipeOutputReference into _recipe
        /// </summary>
        /// <param name="recipeOutputReference"></param>
        public AddRecipeWindowViewModel(Recipe recipeOutputReference)
        {
            if (recipeOutputReference != null)
            {
                _recipe = new Recipe()
                {
                    Id = recipeOutputReference.Id,
                    TextXml = recipeOutputReference.TextXml,
                    Author =  recipeOutputReference.Author,
                    ImagePath = recipeOutputReference.ImagePath,
                    Title = recipeOutputReference.Title
                };
                _buttonValidateContent = "update";
            }
            else
            {
                _recipe = new Recipe()
                {
                    TextXml = System.IO.File.ReadAllText(@"C:\Users\cleme\source\repos\MyFoodStock\Client\Ressources\XML\baseRecipe.xml")//should be relative path
                };
                _buttonValidateContent = "add";
            }
        }
        #endregion

        #region Private Fields
        private Recipe _recipe;
        private Recipe _output;
        private string _buttonValidateContent;
        #endregion

        #region Properties
        public string ButtonValidateContent
        {
            get => _buttonValidateContent;
            set => _buttonValidateContent = value;
        }
        public Recipe Output
        {
            get => _output;
            set => _output = value;
        }
        public Recipe Recipe
        {
            get => _recipe;
            set => _recipe = value;
        }
        public string Author
        {
            get => Recipe.Author;
            set
            {
                _recipe.Author = value;
                OnPropertyChanged(nameof(Author));
            }
        }
        public int Id
        {
            get => Recipe.Id;
            set => _recipe.Id = value;
        }
        public string TextXml
        {
            get => _recipe.TextXml;
            set
            {
                _recipe.TextXml = value;
                OnPropertyChanged(nameof(TextXml));
            }
        }public string Title
        {
            get => _recipe.Title;
            set
            {
                _recipe.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string ImagePath
        {
            get => _recipe.ImagePath;
            set
            {
                _recipe.ImagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }
        #endregion

        #region relayCommands

        private RelayCommand _createRecipeCommand;

        /// <summary>
        /// action : set output to Recipe and close the window
        /// canexecute : check whether all fields aren't empty
        /// </summary>
        public RelayCommand CreateRecipeCommand
        {
            get
            {
                return _createRecipeCommand
                    ?? (_createRecipeCommand = new RelayCommand(
                    (o) =>
                    {
                        Output = Recipe;
                        CloseWindow();
                    },
                    (o) =>
                    {
                        if (!string.IsNullOrWhiteSpace(Title) &&
                            !string.IsNullOrWhiteSpace(TextXml) &&
                            !string.IsNullOrWhiteSpace(ImagePath))
                        {
                            return true;
                        }
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
