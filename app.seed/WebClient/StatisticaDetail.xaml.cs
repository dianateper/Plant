using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WebClient
{
    /// <summary>
    /// Логика взаимодействия для StatisticaDetail.xaml
    /// </summary>
    public partial class StatisticaDetail : Window
    {
        public Func<double, string> Formatter { get; set; }
        ControllerStatistica statistica;
        public SeriesCollection TemperatureCollection { get; set; }
        public SeriesCollection HumidityCollection { get; set; }

        public List<Plant> historyPlants { get; set; }


        public StatisticaDetail(int X, int Y)
        {
            InitializeComponent();
            statistica = MainWindow.channel.GetControllerStatistica(X, Y);
            historyPlants = MainWindow.channel.GetPlantsHistoryByPosition(X,Y);

            SetGridsVariable();
            SetChartsVariable();  
        }

        public void SetGridsVariable()
        {
            ShowDetail(statistica.statisticaTemperature, Temperature);
            ShowDetail(statistica.statisticaHumidity, Humidity);
            PlantsHistoryGrid.ItemsSource = historyPlants;
        }

        public void SetChartsVariable()
        {
            TemperatureCollection = new SeriesCollection
            {
                new  LineSeries
                {
                    Title = "Temperature",
                    Values = RawDataSeriesTemperature,
                    Fill = new SolidColorBrush(Colors.Bisque)
                }
            };

            HumidityCollection = new SeriesCollection
            {
                new  LineSeries
                {
                    Title = "Humidity",
                    Values = RawDataSeriesHumidity,
                    Fill = new SolidColorBrush(Colors.Bisque)
                }
            };

            GraphTemperatureAxisX.LabelFormatter = value => new DateTime((long)value).ToString("MM/dd/yy");
            GraphHumidityAxisX.LabelFormatter = value => new DateTime((long)value).ToString("MM/dd/yy");

            DataContext = this;
            GraphTemperature.Series = TemperatureCollection;
            GraphHumidity.Series = HumidityCollection;
        }

        public void ShowDetail(StatisticaView view, System.Windows.Controls.DataGrid grid)
        {
            grid.Items.Add(new { Property = "Max", Value = view.Max });
            grid.Items.Add(new { Property = "Min", Value = view.Min });
            grid.Items.Add(new { Property = "Median", Value = view.Median });
            grid.Items.Add(new { Property = "Mean", Value = view.Mean });
            grid.Items.Add(new { Property = "Range", Value = view.Range });
            grid.Items.Add(new { Property = "Variance", Value = view.Variance });
            grid.Items.Add(new { Property = "StandartDeriation", Value = view.StandartDeriation });
            grid.Items.Add(new { Property = "Kurtosis", Value = view.Kurtosis });
            grid.Items.Add(new { Property = "Skewnes", Value = view.Skewnes });
        }


        public ChartValues<DateTimePoint> RawDataSeriesTemperature
        {
            get
            {
                ChartValues<DateTimePoint> Values = new ChartValues<DateTimePoint>();
                foreach (ControllerHistory history in statistica.controllerHistories)
                {
                    Values.Add(new DateTimePoint(history.datetime, history.temperature));
                }
                return Values;
            }
        }

        public ChartValues<DateTimePoint> RawDataSeriesHumidity
        {
            get
            {
                ChartValues<DateTimePoint> Values = new ChartValues<DateTimePoint>();
                foreach (ControllerHistory history in statistica.controllerHistories)
                {
                    Values.Add(new DateTimePoint(history.datetime, history.humidity));
                }
                return Values;
            }
        }


    }


}
