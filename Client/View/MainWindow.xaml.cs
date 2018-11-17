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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.ViewModel;
using UserService.Modele;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// create the mainWindow using the authenticated user
        /// </summary>
        /// <param name="user">authenticated user from connected or register window</param>
        public MainWindow(User user)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(user);
        }
        /// <summary>
        /// create the mainwindow using the default user, should be disable in production
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(null);
        }
    }
}
