using Models.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WebClient
{
    /// <summary>
    /// Логика взаимодействия для StatisticaPage.xaml
    /// </summary>
    public partial class StatisticaPage : Window
    {

        public static List<ControllerStatistica> controllersStatistica = new List<ControllerStatistica>();

        public StatisticaPage()
        {
            InitializeComponent();
            GetStatisticaValues();
            
        }


        public void GetStatisticaValues()
        {
            int row = Field.RowDefinitions.Count;
            int col = Field.ColumnDefinitions.Count;

            for (int i = 0; i<row; i++)
            {
                for(int j = 0; j<col-9; j++)
                {
                    ControllerStatistica c = MainWindow.channel.GetControllerStatistica(i, j);
                    controllersStatistica.Add(c);

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = "\n t:\nMin: " + c.statisticaTemperature.Min + "\nMax: " + c.statisticaTemperature.Max;


                    Grid.SetColumn(textBlock, j);
                    Grid.SetRow(textBlock, i);

                    Field.Children.Add(textBlock);

                }
            }

        }

        private void Field_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement)e.Source;
            int row = Grid.GetRow(element);
            int column = Grid.GetColumn(element);

                   
            var detail = new StatisticaDetail(row, column);
            detail.Show();

        }
    }
}
