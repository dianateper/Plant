using Models.Model;
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

namespace WebClient.EditedItem
{
    /// <summary>
    /// Логика взаимодействия для SoilFetilizerAddPage.xaml
    /// </summary>
    public partial class SoilFetilizerAddPage : Window
    {
        public SoilFetilizerAddPage()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //add soil

            Soil soil = new Soil();
            soil.Name = soilNameText.Text;

            try
            {
                MainWindow.channel.AddSoil(soil);
                MessageBox.Show(soilNameText.Text + " added.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //add fertilizer
            Fertilizer fertilizer = new Fertilizer();
            fertilizer.Name = fetilizerNameText.Text;
            fertilizer.Count = int.Parse(fetilizerPriceText.Text);

            try
            {
                MainWindow.channel.AddFertilizer(fertilizer);
                MessageBox.Show(fetilizerNameText.Text + " added.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var newForm = new AddingItemsPage();
            newForm.Show();
            this.Close();
        }
    }
}
