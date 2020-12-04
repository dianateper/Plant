using System.Windows;
using WebClient.EditedItem;

namespace WebClient
{
    /// <summary>
    /// Логика взаимодействия для AddingItemsPage.xaml
    /// </summary>
    public partial class AddingItemsPage : Window
    {
        public AddingItemsPage()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var newForm = new AddPlantPage();
            newForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var newForm = new SoilFetilizerAddPage();
            newForm.Show();
            this.Close();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            var newForm = new MainWindow();
            newForm.Show();
            this.Close();
        }
    }
}
