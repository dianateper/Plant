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
                    Controller controller = MainWindow.channel.GetControllerByPosition(i*2, j*2);
                  
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = "\nT: " + controller.temperature + "\nH: " + controller.humidity;


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

                   
            var detail = new StatisticaDetail(row*2, column*2);
            detail.Show();

        }
    }
}
