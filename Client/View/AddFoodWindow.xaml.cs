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
        /// <summary>
        /// create a FoodWindow in order to create a Food poco
        /// </summary>
        /// <param name="listFoodCategoryAndSubs">listFoodCategoryAndSubs from MainWindowViewModel in order to bind to the combobox</param>
        public AddFoodWindow(List<FoodCategoryAndSubs> listFoodCategoryAndSubs)
        {
            InitializeComponent();
            DataContext = new AddFoodWindowViewModel(listFoodCategoryAndSubs,null);
        }
        /// <summary>
        /// create a FoodWindow hydrated with an existing Food poco in order to update it
        /// </summary>
        /// <param name="listFoodCategoryAndSubs">listFoodCategoryAndSubs from MainWindowViewModel in order to bind to the combobox</param>
        /// <param name="food">Food poco to update</param>
        public AddFoodWindow(List<FoodCategoryAndSubs> listFoodCategoryAndSubs, Food food)
        {
            InitializeComponent();
            DataContext = new AddFoodWindowViewModel(listFoodCategoryAndSubs, food);
        }
        /// <summary>
        /// explicit hiding of the ShowDialog method in order to expose the food create or updated via this window
        /// </summary>
        /// <returns>the food create or updated via this window</returns>
        public new Food ShowDialog()
        {
            base.ShowDialog();
            return ((AddFoodWindowViewModel) DataContext).Output;
        }
    }
}
