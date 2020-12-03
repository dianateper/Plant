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

namespace WebClient
{
    /// <summary>
    /// Логика взаимодействия для AddPlantPage.xaml
    /// </summary>
    public partial class AddPlantPage : Window
    {

        List<Soil> soils = new List<Soil>();
        List<Fertilizer> fertilizers = new List<Fertilizer>();

        public AddPlantPage()
        {
            InitializeComponent();

            soils = MainWindow.channel.GetAllSoils();
            fertilizers = MainWindow.channel.GetAllFertilizer();

            soilComboBox.ItemsSource = soils;
            fertilizerComboBox.ItemsSource = fertilizers;
            
            soilComboBox.DisplayMemberPath = "Name";
            fertilizerComboBox.DisplayMemberPath = "Name";

            DataContext = this;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            Soil selectedSoil = soilComboBox.SelectedItem as Soil;
            Fertilizer selectedFertilizer = fertilizerComboBox.SelectedItem as Fertilizer;
           
            try
            {
                selectedFertilizer.Count = int.Parse(FertilizerCountText.Text);
                Models.Model.Condition condition = new Models.Model.Condition();
                condition.MinTmp = double.Parse(plantMinTempText.Text);
                condition.MaxTmp = double.Parse(plantMaxTempText.Text);
                condition.MinHumidity = double.Parse(plantHumidityMinText.Text);
                condition.MaxHumidity = double.Parse(plantHumidityMaxText.Text);
                condition.phMin = double.Parse(plantPhminText.Text);
                condition.phMax = double.Parse(plantPhmaxText.Text);

                Plant plant = new Plant();
                plant.Name = plantNameText.Text;
                plant.Price = double.Parse(plantPriceText.Text);
                if (selectedFertilizer != null || selectedSoil != null || condition.MinTmp < condition.MaxTmp 
                    || condition.MinHumidity < condition.MaxHumidity || condition.phMin < condition.phMax 
                    || plant.Price != 0 || !string.IsNullOrEmpty(plant.Name))
                {
                    MainWindow.channel.AddPlant(plant, condition, selectedSoil, selectedFertilizer);
                }

                MessageBox.Show(plantNameText.Text + " added.");
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var newForm = new AddingItemsPage();
            newForm.Show();
            this.Close();
        }
    }
}
