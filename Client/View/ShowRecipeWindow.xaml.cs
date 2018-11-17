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
using Client.ViewModel;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for ShowRecipeWindow.xaml
    /// </summary>
    public partial class ShowRecipeWindow : Window
    {
        /// <summary>
        /// create a window showing a well presented recipe tutorial
        /// </summary>
        /// <param name="textXml">xml containing all the information to be parsed</param>
        public ShowRecipeWindow(string textXml)
        {
            InitializeComponent();
            DataContext = new ShowRecipeWindowViewModel(textXml);
        }
    }
}
