using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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

        public MainPage()
        {
           
            BindingContext = this;
        }

        void Greeting(object sender, EventArgs args)
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
                    //Message.Text = channel.Greeting();
                }
            }
            catch (Exception)
            {
                //Message.Text = "Error";
            }
        }

    }
}
