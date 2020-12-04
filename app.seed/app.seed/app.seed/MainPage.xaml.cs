using Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using Xamarin.Forms;

namespace app.seed
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        #region fields connection

        Uri address = new Uri("https://763059102637.ngrok.io");
        BasicHttpBinding binding = new BasicHttpBinding();
        ChannelFactory<IContractXam> factory = null;
        IContractXam channel = null;

        #endregion

        #region fields style

        public static readonly Color colorHidePath = Color.White;
        public static readonly Color colorSelectedMachine = Color.Red;
        public static readonly Color colorOptimalRoute = Color.Green;

        public static int MinX = 0;
        public static int MaxX = 9;
        public static int MinY = 0;
        public static int MaxY = 19;

        bool light_mode = false;
        bool dark_mode = true;

        #endregion

        #region fields

        List<Machine> machineList;
        Machine selectedMachinePicker;
        Machine selectedMachine;
        Position targetPosition;
        List<Position> positionsList;
        LinkedList<Position> optimalRoute;

        #endregion

        #region properties
        
        public List<Machine> MachineList
        {
            get { return machineList; }
            set
            {
                machineList = value;
                OnPropertyChanged();
            }
        }

        public Machine SelectedMachinePicker
        {
            get { return selectedMachinePicker; }
            set
            {
                selectedMachinePicker = value;
                SelectedMachine = value;
                OnPropertyChanged();
            }
        }

        public Machine SelectedMachine
        {
            get { return selectedMachine; }
            set
            {
                if (selectedMachine == null)
                {
                    selectedMachine = value;
                    ShowNewMachinePosition(selectedMachine);

                    OnPropertyChanged();
                }
                else
                {
                    if (!selectedMachine.IsEqual(value))
                    {
                        HideOldMachinePosition(selectedMachine);

                        MachineList.ForEach(i =>
                        {
                            if (i.machineId == selectedMachine.machineId)
                            {
                                i.X = value.X;
                                i.Y = value.Y;
                            }
                        });

                        selectedMachine = value;
                        ShowNewMachinePosition(selectedMachine);

                        OnPropertyChanged();
                    }
                }
            }
        }

        public Position TargetPosition
        {
            get { return targetPosition; }
            set
            {
                if (targetPosition != value)
                {
                    targetPosition = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Position> PositionsList
        {
            get { return positionsList; }
            set
            {
                if (positionsList != value)
                {
                    positionsList = value;
                    OnPropertyChanged();
                }
            }
        }

        public LinkedList<Position> OptimalRoute
        {
            get { return optimalRoute; }
            set
            {
                if (optimalRoute != null)
                {
                    HideOldOptimalRoute(optimalRoute);
                }
                optimalRoute = value;
                foreach(Position p in optimalRoute)
                {
                    Console.WriteLine("Optimal route: x={0}, y={1}", p.X, p.Y);
                }
                OnPropertyChanged();
                ShowNewOptimalRoute(optimalRoute);
            }
        }

        #endregion


        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = this;
        }

        #region view methods

        private void HideOldMachinePosition(Machine machine)
        {
            ChangeCellColorPositionsGrid(machine.X, machine.Y, colorHidePath);
        }

        public void ShowNewMachinePosition(Machine machine)
        {
            ChangeCellColorPositionsGrid(machine.X, machine.Y, colorSelectedMachine);
        }

        private void HideOldOptimalRoute(LinkedList<Position> optimalRoute)
        {
            Color color = Color.Blue;

            foreach (Position p in optimalRoute)
            {
                ChangeCellColorPositionsGrid(p.X, p.Y, color);
            }
        }

        private void ShowNewOptimalRoute(LinkedList<Position> optimalRoute)
        {
            foreach (Position p in optimalRoute)
            {
                ChangeCellColorPositionsGrid(p.X, p.Y, colorOptimalRoute);
            }
        }

        private void ChangeCellColorPositionsGrid(int x, int y, Color color)
        {
            if (x % 2 == 0 && y % 2 == 0)
            {
                var button = (Button)GetView(x, y);
                button.BackgroundColor = color;
            }
            else
            {
                var boxview = (BoxView)GetView(x, y);
                boxview.Color = color;
            }

            OnPropertyChanged();
        }

        public View GetView(int col, int row)
        {
            foreach (View v in positions_grid.Children)
                if ((col == Grid.GetColumn(v)) && (row == Grid.GetRow(v)))
                    return v;
            return null;
        }

        #endregion

        #region grid header

        void Connect()
        {

            try
            {
                if (factory == null)
                {
                    factory = new ChannelFactory<IContractXam>(binding, new EndpointAddress(address));
                    channel = factory.CreateChannel();
                }

                if (factory != null && channel != null)
                {
                    //TODO picker
                    MachineList = channel.GetAllMachines();
                    DisplayAlert("", $"Got machines list", "OK");

                    PositionsList = channel.GetAllPositions();
                    DisplayAlert("", $"Got positions list", "OK");
                }
            }
            catch (Exception) { }

        }

        private void connect_button_Clicked(object sender, EventArgs e)
        {
            Connect();
        }

        private void change_mod_image_button_Clicked(object sender, EventArgs e)
        {

        }

        #endregion

        #region grid positions

        private void grid_position_button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var row = Grid.GetRow(button);
            var column = Grid.GetColumn(button);

            TargetPosition = GetPositionInListByXY(column, row);

            DisplayAlert("", $"Target position: {row} {column}", "OK");

            OptimalRoute = channel.GetOptimalRoute(
                GetPositionInListByXY(
                    SelectedMachine.X,
                    SelectedMachine.Y
                    ),
                TargetPosition);

        }

        #endregion

        #region controls grid move

        private void left_image_button_Clicked(object sender, EventArgs e)
        {
            SelectedMachine = SelectedMachine.MoveMachineLeft(channel);
        }

        private void right_image_button_Clicked(object sender, EventArgs e)
        {
            SelectedMachine = SelectedMachine.MoveMachineRight(channel);
        }

        private void up_image_button_Clicked(object sender, EventArgs e)
        {
            SelectedMachine = SelectedMachine.MoveMachineUp(channel);
        }

        private void down_image_button_Clicked(object sender, EventArgs e)
        {
            SelectedMachine = SelectedMachine.MoveMachineDown(channel);
        }
        
        private void execute_action_image_button_Clicked(object sender, EventArgs e)
        {

        }

        #endregion






        public Position GetPositionInListByXY(int x, int y)
        {
            Position position = new Position();
            positionsList.ForEach(i =>
            {
                if (i.X == x && i.Y == y)
                {
                    position = i;
                }

            });

            return position;
        }




    }
}
