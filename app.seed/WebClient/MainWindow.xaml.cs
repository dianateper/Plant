using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Windows;

namespace WebClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Uri address = new Uri("http://127.0.0.1:4000/IContractWeb");
        BasicHttpBinding binding = new BasicHttpBinding();
        ChannelFactory<IContractWeb> factory = null;
        public static IContractWeb channel = null;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void StatisticaPageBnt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (factory == null)
                {
                    factory = new ChannelFactory<IContractWeb>(binding, new EndpointAddress(address));
                    channel = factory.CreateChannel();
                }
                if (factory != null && channel != null)
                {

                    Planting planting = new Planting();


                    var newForm = new StatisticaPage();
                    newForm.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PlantingPageBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (factory == null)
                {
                    factory = new ChannelFactory<IContractWeb>(binding, new EndpointAddress(address));
                    channel = factory.CreateChannel();
                }
                if (factory != null && channel != null)
                {

                    Planting planting = new Planting();


                    var newForm = new Planting();
                    newForm.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
