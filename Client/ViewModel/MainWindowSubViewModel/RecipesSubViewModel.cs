using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Client.FoodManagerServiceReference;
using Client.Helper;
using Client.View;

namespace Client.ViewModel.MainWindowSubViewModel
{
    public class RecipesSubViewModel:MainWindowSubViewModelBase
    {
        #region Private Fields

        private Recipe _selectedRecipe;
        private ObservableCollection<Recipe> _listRecipes;

        #endregion

        #region Properties

        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged(nameof(SelectedRecipe));
            }
        }

        public ObservableCollection<Recipe> ListRecipes
        {
            get
            {
                return _listRecipes;
            }
            set
            {
                _listRecipes = value;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListRecipes);
                view.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
            }
        }

        #endregion

        #region RelayCommand

        private RelayCommand _openRecipeCommand;
        private RelayCommand _updateRecipeCommand;
        private RelayCommand _deleteRecipeCommand;

        public RelayCommand UpdateRecipeCommand
        {
            get => _updateRecipeCommand;
            set => _updateRecipeCommand = value;
        }

        public RelayCommand DeleteRecipeCommand
        {
            get => _deleteRecipeCommand;
            set => _deleteRecipeCommand = value;
        }

        public RelayCommand OpenRecipeCommand
        {
            get => _openRecipeCommand;
            set => _openRecipeCommand = value;
        }

        #endregion
    }
}
