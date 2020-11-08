using Server.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Controllers
{
    class Program
    {
        static Uri address = new Uri("http://127.0.0.1:4000/IContractWeb");
        static BasicHttpBinding binding = new BasicHttpBinding();
        static ChannelFactory<IContractControllers> factory = null;
        public static IContractControllers channel = null;

        public List<Controller> controllers = new List<Controller>();

        static void Main(string[] args)
        {
            try
            {
                if (factory == null)
                {
                    factory = new ChannelFactory<IContractControllers>(binding, new EndpointAddress(address));
                    channel = factory.CreateChannel();
                }
                if (factory != null && channel != null)
                {
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }
        }
    }
}
