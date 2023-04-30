﻿using AccTrade.View.AdminView;
using Microsoft.EntityFrameworkCore;
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

namespace AccTrade.View.AppView
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            

        }

        private void Update_btn_Click(object sender, RoutedEventArgs e) => Window_Loaded(sender, e);

        private void User_btn_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new UserPage());
        }

        private void Product_btn_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new ProductPage());
        }
    }
}
