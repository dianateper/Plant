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

        Uri address = new Uri("https://20f2f6c1403f.ngrok.io");
        BasicHttpBinding binding = new BasicHttpBinding();
        ChannelFactory<IContractXam> factory = null;
        IContractXam channel = null;

        #endregion

        #region fields style

        public static readonly Color colorHidePath = Color.White;
        public static readonly Color colorSelectedMachine = Color.LightCoral;
        public static readonly Color colorOptimalRoute = Color.Lime;
        public static readonly Color colorGridButtonDefault = Color.MistyRose;
        public static readonly Color colorGridButtonTarget = Color.LimeGreen;

        public static int MinX = 0;
        public static int MaxX = 9;
        public static int MinY = 0;
        public static int MaxY = 19;

        #endregion

        #region fields

        List<Machine> machineList;
        Machine selectedMachinePicker;
        Machine selectedMachine;
        Position start_position;
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

                if (TargetPosition != null)
                {
                    StartPosition = GetPositionInListByXY(
                        SelectedMachine.X,
                        SelectedMachine.Y
                        );

                    GetOptimalRoute();
                }
            }
        }

        public Position StartPosition
        {
            get { return start_position; }
            set
            {
                start_position = value;
                OnPropertyChanged();
            }
        }

        public Position TargetPosition
        {
            get { return targetPosition; }
            set
            {
                targetPosition = value;
                OnPropertyChanged();
            }
        }

        public List<Position> PositionsList
        {
            get { return positionsList; }
            set
            {
                positionsList = value;
                OnPropertyChanged();
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
            foreach (Position p in optimalRoute)
            {
                ChangeCellColorPositionsGrid(p.X, p.Y, colorHidePath);
            }

            ChangeCellColorPositionsGrid(
                optimalRoute.First.Value.X,
                optimalRoute.First.Value.Y,
                colorHidePath);


            ChangeCellColorPositionsGrid(
                optimalRoute.Last.Value.X,
                optimalRoute.Last.Value.Y,
                colorGridButtonDefault);

            ShowNewMachinePosition(selectedMachine);
        }

        private void ShowNewOptimalRoute(LinkedList<Position> optimalRoute)
        {
            foreach (Position p in optimalRoute)
            {
                ChangeCellColorPositionsGrid(p.X, p.Y, colorOptimalRoute);
            }

            ChangeCellColorPositionsGrid(
                optimalRoute.First.Value.X,
                optimalRoute.First.Value.Y,
                colorGridButtonTarget);


            ChangeCellColorPositionsGrid(
                optimalRoute.Last.Value.X,
                optimalRoute.Last.Value.Y,
                colorGridButtonTarget);

            ShowNewMachinePosition(selectedMachine);
        }

        private void ChangeCellColorPositionsGrid(int x, int y, Color color)
        {
            if (IsButton(x, y))
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

        private View GetView(int col, int row)
        {
            foreach (View v in positions_grid.Children)
                if ((col == Grid.GetColumn(v)) && (row == Grid.GetRow(v)))
                    return v;
            return null;
        }

        private bool IsButton(Position position)
        {
            if (position.X % 2 == 0 && position.Y % 2 == 0)
            {
                return true;
            }

            return false;
        }

        private bool IsButton(int x, int y)
        {
            if (x % 2 == 0 && y % 2 == 0)
            {
                return true;
            }

            return false;
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

        private void SetControllerValuesManyQueries()
        {
            int row = positions_grid.RowDefinitions.Count;
            int col = positions_grid.ColumnDefinitions.Count;

            for (int i = 0; i < col; i += 2)
            {
                for (int j = 0; j < row; j += 2)
                {
                    var button = (Button)GetView(i, j);
                    Controller controller = channel.GetControllerByPosition(i, j);
                    Console.WriteLine("controller.PositionId={0}, controller.temperature={1}, controller.humidity={2}", controller.PositionId, controller.temperature, controller.humidity);
                    button.FontSize = 8;
                    button.Text = i + "" + j + "\nT: " + string.Format("{0}\u00B0C", controller.temperature) + "\nH: " + controller.humidity + "%";
                }
            }
        }

        List<Controller> controllers;
        private void SetControllerValues()
        {
            controllers = channel.GetAllControllers();

            int row = positions_grid.RowDefinitions.Count;
            int col = positions_grid.ColumnDefinitions.Count;

            for (int i = 0; i < col; i += 2)
            {
                for (int j = 0; j < row; j += 2)
                {
                    var button = (Button)GetView(i, j);
                    Position position = GetPositionInListByXY(i, j);
                    Controller controller = FindControllerByPositionId(position.PositionId);

                    button.FontSize = 7;
                    button.Text = "T: " + string.Format("{0}\u00B0C", controller.temperature) + "\nH: " + controller.humidity + "%";
                    //button.Text = i/2 + "" + j/2 + "\nT: " + string.Format("{0}\u00B0C", controller.temperature) + "\nH: " + controller.humidity + "%";
                }
            }
        }

        private void change_mod_image_button_Clicked(object sender, EventArgs e)
        {
            SetControllerValues();
        }

        #endregion

        #region grid positions

        private void grid_position_button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var row = Grid.GetRow(button);
            var column = Grid.GetColumn(button);

            StartPosition = GetPositionInListByXY(
                SelectedMachine.X,
                SelectedMachine.Y
                );
            TargetPosition = GetPositionInListByXY(column, row);

            GetOptimalRoute();
        }

        private void GetOptimalRoute()
        {
            OptimalRoute = channel.GetOptimalRoute(StartPosition, TargetPosition);
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
            foreach (Position p in positionsList)
            {
                if (p.X == x && p.Y == y)
                {
                    return p;
                }
            }

            return null;
        }

        public Controller FindControllerByPositionId(int position_id)
        {
            foreach (Controller c in controllers)
            {
                if (c.PositionId == position_id)
                {
                    return c;
                }
            }

            return null;
        }






    }
}
