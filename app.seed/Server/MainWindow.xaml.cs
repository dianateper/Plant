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
        ServiceHost service;
        DBManager db = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (service == null)
                {
                    service = new ServiceHost(typeof(ServiceWeb));
                    service.Open();

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
                if (service != null)
                {
                    service.Close();
                    service = null;

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
