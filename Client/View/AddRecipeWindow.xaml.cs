﻿using System;
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
        public AddRecipeWindow()
        {
            InitializeComponent();
            DataContext = new AddRecipeWindowViewModel(null);
        }

        public AddRecipeWindow(Recipe recipe)
        {
            InitializeComponent();
            DataContext = new AddRecipeWindowViewModel(recipe);
        }

        public new Recipe ShowDialog()
        {
            base.ShowDialog();
            return ((AddRecipeWindowViewModel) DataContext).Output;
        }
    }
}
