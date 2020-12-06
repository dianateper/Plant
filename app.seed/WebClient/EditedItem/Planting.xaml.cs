using Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WebClient
{
    public partial class Planting : Window
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
            PlantedSeeds.ForEach(plant => { SetImageToGrid(plant.IconName, plant.X/2, plant.Y/2); });

            plantsGrid.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(seedsGrid_PreviewMouseLeftButtonDown);
            Field.Drop += new DragEventHandler(savedSeedGrid_Drop);
            
        }


       
        private void savedSeedGrid_Drop(object sender, DragEventArgs e)
        { 
            if (rowIndex < 0)
                return;

            var element = (UIElement)e.Source;
            int row = Grid.GetRow(element);
            int column = Grid.GetColumn(element);

            bool check = false;
            PlantedSeeds.ForEach(plant =>
            {
                if (plant.X == row*2 && plant.Y == column*2)
                {
                    check = true;
                }
            });

            if (!check)
            {
                Plant changedPlant = new Plant(Plants[rowIndex]);
             
                changedPlant.X = row*2;
                changedPlant.Y = column*2;


                TextBox item = new TextBox();
                item.Text = changedPlant.Name;

                SetImageToGrid(changedPlant.IconName, row, column);
                SavedPlants.Add(changedPlant);

            }
        }


        void SetImageToGrid(string iconName, int row, int col)
        {
            string imageLocation = string.Format(imgPath, iconName);

            bool fileExist = File.Exists(imageLocation);

            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(File.Exists(imageLocation)? imageLocation: @"..\..\..\Images\seeds.png", UriKind.Relative));

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
            try
            {
                MainWindow.channel.SetPlants(SavedPlants);
                MessageBox.Show("Seeds added!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            var newForm = new MainWindow();
            newForm.Show();
            this.Close();
        }

      
    }
}