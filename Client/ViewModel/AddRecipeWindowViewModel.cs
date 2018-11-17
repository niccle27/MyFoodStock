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
            }
            else
            {
                _recipe = new Recipe()
                {
                    TextXml = System.IO.File.ReadAllText(@"C:\Users\cleme\source\repos\MyFoodStock\Client\Ressources\XML\baseRecipe.xml")
            };
            }
        }
        #endregion

        #region Private Fields
        private Recipe _recipe;
        private Recipe _output;

        #endregion

        #region Properties
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
        /// Gets the MyCommand.
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
