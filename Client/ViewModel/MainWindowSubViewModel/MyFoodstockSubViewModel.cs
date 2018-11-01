using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.FoodManagerServiceReference;

namespace Client.ViewModel.MainWindowSubViewModel
{
    public class MyFoodstockSubViewModel:MainWindowSubViewModelBase
    {
        private ObservableCollection<Food> _listFoods;

        public ObservableCollection<Food> ListFoods
        {
            get => _listFoods;
            set => _listFoods = value;
        }
    }
}
