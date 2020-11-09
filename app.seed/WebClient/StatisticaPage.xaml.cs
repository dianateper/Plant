using Server.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebClient
{
    /// <summary>
    /// Логика взаимодействия для StatisticaPage.xaml
    /// </summary>
    public partial class StatisticaPage : Window
    {

        public List<ControllerStatistica> controllerStatisticas = new List<ControllerStatistica>();

        public StatisticaPage()
        {
            InitializeComponent();
        }


        public void GetStatisticaValues()
        {
           
        }

    }
}
