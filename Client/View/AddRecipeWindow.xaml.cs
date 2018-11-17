using System;
using System.Collections.Generic;
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
using Client.ViewModel;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        /// <summary>
        /// create a window in order to create a Recipe
        /// </summary>
        public AddRecipeWindow()
        {
            InitializeComponent();
            DataContext = new AddRecipeWindowViewModel(null);
        }
        /// <summary>
        /// create a window in order to update a recipe
        /// </summary>
        /// <param name="recipe">recipe to update</param>
        public AddRecipeWindow(Recipe recipe)
        {
            InitializeComponent();
            DataContext = new AddRecipeWindowViewModel(recipe);
        }
        /// <summary>
        /// hide the default ShowDialog method in order to return the recipe created or updated
        /// </summary>
        /// <returns>recipe created or updated</returns>
        public new Recipe ShowDialog()
        {
            base.ShowDialog();
            return ((AddRecipeWindowViewModel) DataContext).Output;
        }
    }
}
