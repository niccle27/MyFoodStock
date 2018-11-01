using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.FoodManagerServiceReference;

namespace Client.ViewModel.MainWindowSubViewModel
{
    public class RecipesSubViewModel:MainWindowSubViewModelBase
    {
        private ObservableCollection<Recipe> _listRecipes;

        public ObservableCollection<Recipe> ListRecipes
        {
            get => _listRecipes;
            set => _listRecipes = value;
        }
    }
}
