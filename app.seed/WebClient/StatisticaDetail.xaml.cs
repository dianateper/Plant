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
    /// Логика взаимодействия для StatisticaDetail.xaml
    /// </summary>
    public partial class StatisticaDetail : Window
    {
        ControllerStatistica statistica; 

        public StatisticaDetail(int X, int Y)
        {
            InitializeComponent();
            statistica = MainWindow.channel.GetControllerStatistica(X, Y);
        }

        public void ShowDetail()
        {
            
        }

    }
}
