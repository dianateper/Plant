using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Server.Model;

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
        public List<string> MachinesNames { get; set; }
        public int selected_machine;

        public string ImagePlayPath
        {
            get
            {
                return "..\\..\\..\\..\\Images\\play.png";
            }
        }
        public string ImageUpPath
        {
            get
            {
                return "..\\..\\..\\..\\Images\\up.png";
            }
        }
        public string ImagePausePath
        {
            get
            {
                return "..\\..\\..\\..\\Images\\pause.png";
            }
        }
        public string ImageLeftPath
        {
            get
            {
                return "..\\..\\..\\..\\Images\\left.png";
            }
        }
        public string ImageDownPath
        {
            get
            {
                return "..\\..\\..\\..\\Images\\down.png";
            }
        }
        public string ImageRightPath
        {
            get
            {
                return "..\\..\\..\\..\\Images\\right.png";
            }
        }



        public MainPage()
        {
            MachinesNames.Add("Jnt");
            MachinesNames.Add("Two");


            InitializeComponent();
            Connect();

            BindingContext = this;
        }

        void Connect()
        {
            /*
            try
            {
                if (factory == null)
                {
                    factory = new ChannelFactory<IContractXam>(binding, new EndpointAddress(address));
                    channel = factory.CreateChannel();
                }

                if (factory != null && channel != null)
                {
                    Machines = channel.GetListMachines();
                    GetMachinesNames();

                }
            }
            catch (Exception)
            {


            }
            */
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
    }
}
