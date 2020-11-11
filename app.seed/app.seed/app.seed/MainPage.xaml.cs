using Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using Xamarin.Forms;

namespace app.seed
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Uri address = new Uri("http://10.0.2.2:4000/IContractXam");
        BasicHttpBinding binding = new BasicHttpBinding();
        ChannelFactory<IContractXam> factory = null;
        IContractXam channel = null;

        public List<Machine> Machines { get; set; }
        public List<string> MachinesNames = new List<string>();
        public int selected_machine;

        public MainPage()
        {
            MachinesNames.Add("Jnt");
            MachinesNames.Add("Two");
            MachinesNames.Add("Tрwo");


            InitializeComponent();
         

            BindingContext = this;
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
                    Machines = channel.GetAllMachines();
                    GetMachinesNames();

                    Machines.ForEach(m =>
                    {
                        DisplayAlert(m.Name, "ok", "ok");
                    });

                }
            }
            catch (Exception)
            {


            }
            
        }



        void GetMachinesNames()
        {
            Machines.ForEach(i =>
            {
                MachinesNames.Add(i.Name);
            });
        }

        private void machineListPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_machine = machineListPicker.SelectedIndex;
        }

        private void change_mod_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Connect();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void down_image_button_Clicked(object sender, EventArgs e)
        {

        }

        private void left_image_button_Clicked(object sender, EventArgs e)
        {

        }

        private void right_image_button_Clicked(object sender, EventArgs e)
        {

        }

        private void pause_image_button_Clicked(object sender, EventArgs e)
        {

        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {

        }

        private void ImageButton_Clicked_2(object sender, EventArgs e)
        {

        }

        private void catch_position_image_button_Clicked(object sender, EventArgs e)
        {

        }

        private void up_image_button_Clicked(object sender, EventArgs e)
        {

        }

        private void change_mod_image_button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
