﻿using AccTrade.Model.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AccTrade.View.AdminView
{
    public partial class UpdProduct : Window
    {
        public int ProductID { get; set; }  
        public UpdProduct(int id)
        {
            InitializeComponent();
            ProductID = id;
        }
        byte[] imageByte;
        MediaPlayer mediaPlayer = new MediaPlayer();
        string filename;
        private void open_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Png photo (*.png)|*.png|Jpeg photo (*.jpeg)|*.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {

                imageByte = File.ReadAllBytes(openFileDialog.FileName);
                filename = openFileDialog.FileName;
                mediaPlayer.Open(new Uri(filename));
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(imageByte);
                image.EndInit();
                img.Source = image;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppContext())
            {
                var products = context.Forms.Where(p => p.Id == ProductID).ToList();
                if (products.Any())
                {
                    foreach (var product in products)
                    {
                        product.username = User_cb.Text;
                        product.title= Title_tb.Text;
                        product.Describe = Describe_tb.Text;
                        product.GameCategory  = gamecategory_cb.Text;
                        product.Price = Int32.Parse(Price_tb.Text);
                        product.ImageData = imageByte;


                    }
                    MessageBox.Show("update successful");
                    context.SaveChanges();
                    Close();
                }
                else
                {
                    MessageBox.Show("There wasn't this record.");
                }
            }
        }

        private void Price_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex inputRegex = new Regex(@"\d");
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 4 || !match.Success)
            {
                e.Handled = true;
            }

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            using (var context = new AppContext())
            {
                var product = context.Forms.First(p => p.Id == ProductID);
                var login = context.Logins.ToList();
                var game = context.Categories.ToList();
                if (product != null)
                {
                    gamecategory_cb.ItemsSource = game;
                    gamecategory_cb.DisplayMemberPath = "CategoryName";
                    User_cb.ItemsSource = login;
                    User_cb.DisplayMemberPath = "Username";
                    User_cb.Text = product.username;
                    gamecategory_cb.Text = product.GameCategory;
                    Title_tb.Text = product.title;
                    User_cb.Text = product.username;
                    Price_tb.Text = product.Price.ToString();
                    Describe_tb.Text = product.Describe;
                    imageByte = product.ImageData;
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = new MemoryStream(imageByte);
                    image.EndInit();
                    img.Source = image;
                }
            }

        }

        private void Price_tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                e.Handled = true;
            }

        }
    }
}
