using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Client.FoodManagerServiceReference;
using Client.Model;
using Client.ViewModel;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for AddFoodWindow.xaml
    /// </summary>
    public partial class AddFoodWindow : Window
    {
        public AddFoodWindow()
        {
            InitializeComponent();
            DataContext = new AddFoodWindowViewModel();
        }

        public AddFoodWindow(List<FoodCategoryAndSubs> listFoodCategoryAndSubs, Food food)
        {
            InitializeComponent();
            DataContext = new AddFoodWindowViewModel(listFoodCategoryAndSubs, food);
        }

        public Food ShowDialog()
        {
            base.ShowDialog();
            return ((AddFoodWindowViewModel) DataContext).Food;
        }
    }
}
