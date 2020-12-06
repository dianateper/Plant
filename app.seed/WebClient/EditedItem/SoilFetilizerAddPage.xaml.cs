using Models.Model;
using System;
using System.Windows;

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
                MessageBox.Show(soilNameText.Text + " added!");
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
            try
            {
                fertilizer.Name = fetilizerNameText.Text;
                fertilizer.Count = int.Parse(fetilizerPriceText.Text);

                MainWindow.channel.AddFertilizer(fertilizer);
                MessageBox.Show(fetilizerNameText.Text + " added!");
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
