using Server.Repository;
using Server.Services;
using System;
using System.ServiceModel;
using System.Windows;

namespace Server
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceHost serviceWeb;
        ServiceHost serviceController;
        DBManager db = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (serviceController == null || serviceWeb == null)
                {
                    serviceWeb = new ServiceHost(typeof(ServiceWeb));
                    serviceController = new ServiceHost(typeof(ServiceController));

                    serviceWeb.Open();
                    serviceController.Open();

                    textBox1.Text += "Сервер запущен.         " + DateTime.Now + Environment.NewLine;
                    db = new DBManager();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( serviceController != null && serviceWeb != null)
                {
                    serviceWeb.Close();
                    serviceController.Close();

                    serviceWeb = null;
                    serviceController = null;

                    textBox1.Text += "Сервер остановлен.    " + DateTime.Now + Environment.NewLine;
                    db.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
