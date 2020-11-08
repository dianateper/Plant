using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Machines
{
    class Program
    {
        static Uri address = new Uri("http://127.0.0.1:4000/IContractMachine");
        static BasicHttpBinding binding = new BasicHttpBinding();
        static ChannelFactory<IContractMachine> factory = null;
        public static IContractMachine channel = null;

        static void Main(string[] args)
        {
            try
            {
                if (factory == null)
                {
                    factory = new ChannelFactory<IContractMachine>(binding, new EndpointAddress(address));
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
