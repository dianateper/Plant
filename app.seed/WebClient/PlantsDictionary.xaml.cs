﻿using Models.Model;
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

namespace WebClient
{
    /// <summary>
    /// Логика взаимодействия для PlantsDictionary.xaml
    /// </summary>
    public partial class PlantsDictionary : Window
    {

        List<Plant> plants = new List<Plant>();
        public PlantsDictionary()
        {
            InitializeComponent();
            plants = MainWindow.channel.GetListFullPlants();
            plantsGrid.ItemsSource = plants;
            ConditionGrid.ItemsSource = plants;
          
        }

        private void plantsGrid_Selected(object sender, RoutedEventArgs e)
        {
            Plant plant = (Plant)plantsGrid.SelectedItem;

            feritilizerldGrid.ItemsSource = MainWindow.channel.GetFeritilizerByPlantId(plant.PlantId);

            ConditionGrid.ItemsSource = new List<Plant> { plant };
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            plants = MainWindow.channel.GetListFullPlants();
            plantsGrid.ItemsSource = plants;
            ConditionGrid.ItemsSource = plants;
            feritilizerldGrid.ItemsSource = null;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            var newForm = new MainWindow();
            newForm.Show();
            this.Close();
        }
    }
}
