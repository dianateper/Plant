using LiveCharts;
using LiveCharts.Wpf;
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

        Dictionary<Plant, int> fieldStatistic = new Dictionary<Plant, int>();
        public SeriesCollection PlantsCollection;
        public List<string> Labels = new List<string>();
      
        public StatisticaPage()
        {
            InitializeComponent();
            SetGridValues();
            SetStatisticaValues();
        }

        public void SetGridValues()
        {

            int row = Field.RowDefinitions.Count;
            int col = Field.ColumnDefinitions.Count;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Controller controller = MainWindow.channel.GetControllerByPosition(i * 2, j * 2);

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = "\nT: " + controller.temperature + "\nH: " + controller.humidity;

                    Grid.SetColumn(textBlock, j);
                    Grid.SetRow(textBlock, i);

                    Field.Children.Add(textBlock);

                }
            }
        }
       
        public void SetStatisticaValues()
        { 
            ChartValues<double> values = new ChartValues<double>();
            fieldStatistic = MainWindow.channel.GetFieldStatistic();
            List<string> names = new List<string>();

            foreach (KeyValuePair<Plant, int> value in fieldStatistic)
            {
                values.Add(value.Value);
                names.Add(value.Key.Name);
            }

            PlantsCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Plants",
                    Values = values
                }
            };

            GraphFrequencyPlant.AxisX.Add(new Axis
            {
                Labels = names,
                Separator = new LiveCharts.Wpf.Separator
                {
                    Step = 1,
                    IsEnabled = false 
                },
                LabelsRotation = 15
            });

            DataContext = this;
            GraphFrequencyPlant.Series = PlantsCollection;

            
        }

        private void Field_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement)e.Source;
            int row = Grid.GetRow(element);
            int column = Grid.GetColumn(element);

                   
            var detail = new StatisticaDetail(row*2, column*2);
            detail.Show();

        }


    }
}
