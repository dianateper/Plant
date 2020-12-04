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
                //TODO update MachineList
                if (selectedMachine == null)
                {
                    selectedMachine = value;
                    OnPropertyChanged();
                    ShowNewMachinePosition(selectedMachine);
                }
                else
                {
                    Console.WriteLine("selma" + selectedMachine.Str());
                    Console.WriteLine("value" + value.Str());

                    if (!selectedMachine.IsEqual(value))
                    {
                        Console.WriteLine("!!!!!!!!!!!selectedMachine.IsEqual(value) is working");
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
                        OnPropertyChanged();
                        ShowNewMachinePosition(selectedMachine);
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
                if (optimalRoute != value)
                {
                    if (optimalRoute != null)
                    {
                        HideOldOptimalRoute(optimalRoute);
                    }
                    optimalRoute = value;
                    OnPropertyChanged();
                    ShowNewOptimalRoute(optimalRoute);
                }
            }
        }

        #endregion


        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = this;
        }



        private void HideOldMachinePosition(Machine machine)
        {
            Console.WriteLine("!!!HideOldMachinePosition" + machine.Str() + ", color = " + Color.Blue.ToString());
            ChangeCellColorPositionsGrid(machine.X, machine.Y, colorHidePath);
        }

        public void ShowNewMachinePosition(Machine machine)
        {
            Console.WriteLine("!!!ShowNewMachinePosition" + machine.Str() + ", color = " + colorSelectedMachine.ToString());
            ChangeCellColorPositionsGrid(machine.X, machine.Y, colorSelectedMachine);
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


        private void HideOldOptimalRoute(LinkedList<Position> optimalRoute)
        {
            Color color = Color.Blue;
            if (light_mode) { }
            else
            if (dark_mode)
            {
                Style = Resources["ButtonGridStyle"] as Style;

                foreach (Setter s in Style.Setters)
                {
                    if (s.Property.Equals("BackgroundColor"))
                    {
                        color = (Color)s.Value;
                    }
                }
            }

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

        #region controls grid1

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

        #region controls grid2

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

        #region controls grid3 

        private void play_image_button_Clicked(object sender, EventArgs e)
        {

        }

        private void pause_image_button_Clicked(object sender, EventArgs e)
        {

        }

        private void execute_action_image_button_Clicked(object sender, EventArgs e)
        {

        }

        #endregion

        #region controls grid4 (move)

        private void left_image_button_Clicked(object sender, EventArgs e)
        {
            Machine machine = SelectedMachine.ToClone();
            SelectedMachine = machine.MoveMachineLeft(channel);
        }

        private void right_image_button_Clicked(object sender, EventArgs e)
        {
            Machine machine = SelectedMachine.ToClone();
            SelectedMachine = machine.MoveMachineRight(channel);
        }

        private void up_image_button_Clicked(object sender, EventArgs e)
        {
            Machine machine = SelectedMachine.ToClone();
            SelectedMachine = machine.MoveMachineUp(channel);
        }

        private void down_image_button_Clicked(object sender, EventArgs e)
        {
            Machine machine = SelectedMachine.ToClone();
            SelectedMachine = machine.MoveMachineDown(channel);
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
