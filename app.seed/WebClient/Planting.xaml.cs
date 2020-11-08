using Server.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WebClient
{
    public partial class Planting : Page
    {
        public delegate Point GetPosition(IInputElement element);
        int rowIndex = -1;

        public string imgPath = @"..\..\..\Images\{0}.png";

        List<Plant> Plants = new List<Plant>();
        List<Plant> SavedPlants = new List<Plant>();
        List<Plant> PlantedSeeds = new List<Plant>();

        public Planting()
        {
            InitializeComponent();
            
            Plants = MainWindow.channel.GetListPlants();
            plantsGrid.ItemsSource = Plants;

            PlantedSeeds = MainWindow.channel.GetPlantedSeeds();
            PlantedSeeds.ForEach(plant => { SetImageToGrid(plant.IconName, plant.X, plant.Y); });

            plantsGrid.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(seedsGrid_PreviewMouseLeftButtonDown);
            Field.Drop += new DragEventHandler(savedSeedGrid_Drop);
            
        }


       
        private void savedSeedGrid_Drop(object sender, DragEventArgs e)
        { 
            if (rowIndex < 0)
                return;

            Plant changedPlant = Plants[rowIndex];
          
            var element = (UIElement)e.Source;
            int row = Grid.GetRow(element);
            int column = Grid.GetColumn(element);

            bool check = false;
            PlantedSeeds.ForEach(plant =>
            {
                if (plant.X == row && plant.Y == column)
                {
                    check = true;
                }
            });

            if (!check)
            {
                changedPlant.X = row;
                changedPlant.Y = column;

                TextBox item = new TextBox();
                item.Text = changedPlant.Name;

                SetImageToGrid(changedPlant.IconName, row, column);
                SavedPlants.Add(changedPlant);

            }
        }


        void SetImageToGrid(string iconName, int row, int col)
        {
            string imageLocation = string.Format(imgPath, iconName);

            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(imageLocation, UriKind.Relative));

            Rectangle rect = new Rectangle();
            rect.Fill = imgBrush;

            Grid.SetColumn(rect, col);
            Grid.SetRow(rect, row);

            Field.Children.Add(rect);

        }

        private void seedsGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            rowIndex = GetCurrentRowIndex(e.GetPosition);
            if (rowIndex < 0)
                return;
            plantsGrid.SelectedIndex = rowIndex;

            Plant selectedSeed = plantsGrid.Items[rowIndex] as Plant;
            if (selectedSeed == null)
                return;
            DragDropEffects dragdropeffects = DragDropEffects.Move;


            if (DragDrop.DoDragDrop(plantsGrid, selectedSeed, dragdropeffects)
                                != DragDropEffects.None)
            {
                plantsGrid.SelectedItem = selectedSeed;
            }
        }

        private bool GetMouseTargetRow(Visual theTarget, GetPosition position)
        {
            Rect rect = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point point = position((IInputElement)theTarget);
            return rect.Contains(point);
        }

        private int GetCurrentRowIndex(GetPosition pos)
        {
            int curIndex = -1;
            for (int i = 0; i < plantsGrid.Items.Count; i++)
            {
                DataGridRow itm = GetRowItem(i);
                if (GetMouseTargetRow(itm, pos))
                {
                    curIndex = i;
                    break;
                }
            }
            return curIndex;
        }

        private DataGridRow GetRowItem(int index)
        {
            if (plantsGrid.ItemContainerGenerator.Status
                    != GeneratorStatus.ContainersGenerated)
                return null;
            return plantsGrid.ItemContainerGenerator.ContainerFromIndex(index)
                                                            as DataGridRow;
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.channel.SetPlants(SavedPlants);
        }

    
    }
}