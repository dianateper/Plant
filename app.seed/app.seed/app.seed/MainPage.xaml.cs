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
        Uri address = new Uri("https://c8724f944704.ngrok.io");
        BasicHttpBinding binding = new BasicHttpBinding();
        ChannelFactory<IContractXam> factory = null;
        IContractXam channel = null;


        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = this;//.rootModel;
        }

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
                    //rootModel.MachineList = channel.GetAllMachines();
                    MachineList = channel.GetAllMachines();
                    DisplayAlert("", $"Got machines list", "OK");

                    //rootModel.PositionsList = channel.GetAllPositions();
                    PositionsList = channel.GetAllPositions();
                    DisplayAlert("", $"Got positions list", "OK");
                }
            }
            catch (Exception) { }

        }



        #region controls grid1

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

            channel.GetOptimalRoute(
                GetPositionInListByXY(
                    SelectedMachine.X,
                    SelectedMachine.Y
                    ),
                TargetPosition);

            //ShowMachinePosition();
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
            SelectedMachine.MoveMachineLeft(channel);
        }

        private void right_image_button_Clicked(object sender, EventArgs e)
        {
            SelectedMachine.MoveMachineRight(channel);
        }

        private void up_image_button_Clicked(object sender, EventArgs e)
        {
            SelectedMachine.MoveMachineUp(channel);
        }

        private void down_image_button_Clicked(object sender, EventArgs e)
        {
            SelectedMachine.MoveMachineDown(channel);
        }

        #endregion


        #region fields

        #region style
        
        public static readonly Color colorSelectedMachine = Color.Red;

        public static int MinX = 0;
        public static int MaxX = 9;
        public static int MinY = 0;
        public static int MaxY = 19;

        #endregion

        List<Machine> machineList;
        Machine selectedMachine;
        Position targetPosition;
        List<Position> positionsList;

        #endregion

        #region properties
        public List<Machine> MachineList
        {
            get { return machineList; }
            set
            {
                if (machineList != value)
                {
                    machineList = value;
                    OnPropertyChanged();
                }
            }
        }

        public Machine SelectedMachine
        {
            get { return selectedMachine; }
            set
            {
                if (selectedMachine != value)
                {
                    selectedMachine = value;
                    OnPropertyChanged();
                    ShowMachinePosition();
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

        /*
        public void ShowMachinePosition()
        {
            var boxview = (BoxView)PositionsGrid.GetChildElements(new Xamarin.Forms.Point(SelectedMachine.X, SelectedMachine.Y));
            boxview.Color = colorSelectedMachine;
            OnPropertyChanged();
        }
        */
        

        public void ShowMachinePosition()
        {
            var boxview = (BoxView) GetView(SelectedMachine.X, SelectedMachine.Y);
            boxview.Color = colorSelectedMachine;
            
            //DisplayAlert("", $"Got machines list", "OK");

            OnPropertyChanged();
        }


        public View GetView(int col, int row)
        {
            foreach (View v in positions_grid.Children)
                if ((col == Grid.GetColumn(v)) && (row == Grid.GetRow(v)))
                    return v;
            return null;
        }


    }
}
