using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.FoodManagerServiceReference;
using Client.Helper;

namespace Client.ViewModel.MainWindowSubViewModel
{
    public class RecipesSubViewModel:MainWindowSubViewModelBase
    {
        #region Private Fields

        private ObservableCollection<Recipe> _listRecipes;

        #endregion

        #region Properties

        public ObservableCollection<Recipe> ListRecipes
        {
            get => _listRecipes;
            set => _listRecipes = value;
        }

        #endregion

        private RelayCommand _openRecipeCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand OpenRecipeCommand
        {
            get
            {
                return _openRecipeCommand
                    ?? (_openRecipeCommand = new RelayCommand(
                    (o) =>
                    {

                    },
                    (o) => true));
            }
        }
    }
}
