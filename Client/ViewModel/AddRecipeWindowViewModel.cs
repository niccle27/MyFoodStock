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
        private Recipe _recipe = new Recipe();
        private Recipe _output;

        private string _author;
        private int _id;
        private string _textXml;
        private string _title;
        private string _imagePath;

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
            get => _author;
            set
            {
                _author = value;
                Recipe.Author = _author;
                OnPropertyChanged(nameof(Author));
            }
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string TextXml
        {
            get => _textXml;
            set
            {
                _textXml = value;
                Recipe.TextXml = _textXml;
                OnPropertyChanged(nameof(TextXml));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Recipe.Title = _title;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                Recipe.ImagePath = _imagePath;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

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
