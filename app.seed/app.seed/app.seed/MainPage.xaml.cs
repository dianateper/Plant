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
        Uri address = new Uri("https://8e8412410a01.ngrok.io");
        BasicHttpBinding binding = new BasicHttpBinding();
        ChannelFactory<IContractXam> factory = null;
        IContractXam channel = null;

        RootModel rootModel = new RootModel();

        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = this.rootModel;
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
                    rootModel.MachineList = channel.GetAllMachines();
                    DisplayAlert("", $"Got machines list", "OK");

                    rootModel.PositionsList = channel.GetAllPositions();
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

            rootModel.TargetPosition = rootModel.GetPositionInListByXY(column, row);

            DisplayAlert("", $"Target position: {row} {column}", "OK");

            channel.GetOptimalRoute(
                rootModel.GetPositionInListByXY(
                    rootModel.SelectedMachine.X,
                    rootModel.SelectedMachine.Y
                    ),
                rootModel.TargetPosition);

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
            rootModel.SelectedMachine.MoveMachineLeft(channel);
        }

        private void right_image_button_Clicked(object sender, EventArgs e)
        {
            rootModel.SelectedMachine.MoveMachineRight(channel);
        }

        private void up_image_button_Clicked(object sender, EventArgs e)
        {
            rootModel.SelectedMachine.MoveMachineUp(channel);
        }

        private void down_image_button_Clicked(object sender, EventArgs e)
        {
            rootModel.SelectedMachine.MoveMachineDown(channel);
        }

        #endregion

    }
}
