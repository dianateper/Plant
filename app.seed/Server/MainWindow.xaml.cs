using Server.Repository;
using Server.Services;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Documents;

namespace Server
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceHost serviceWeb;
        ServiceHost serviceController;
        ServiceHost serviceMachine;
        ServiceHost serviceXam;

        DBManager db = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (serviceController == null || serviceWeb == null || serviceMachine == null || serviceXam == null)
                {
                    serviceWeb = new ServiceHost(typeof(ServiceWeb));
                    serviceController = new ServiceHost(typeof(ServiceController));
                    serviceMachine = new ServiceHost(typeof(ServiceMachine));
                    serviceXam = new ServiceHost(typeof(ServiceXam));

                    serviceWeb.Open();
                    serviceController.Open();
                    serviceMachine.Open();
                    serviceXam.Open();

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
                if ( serviceController != null && serviceWeb != null && serviceMachine != null)
                {
                    serviceWeb.Close();
                    serviceController.Close();
                    serviceMachine.Close();
                    serviceXam.Close();

                    serviceWeb = null;
                    serviceController = null;
                    serviceMachine = null;
                    serviceXam = null;

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
