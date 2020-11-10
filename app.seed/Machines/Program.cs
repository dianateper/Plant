using Models.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Machines
{
    class Program
    {
        static Uri address = new Uri("http://127.0.0.1:4000/IContractMachine");
        static BasicHttpBinding binding = new BasicHttpBinding();
        static ChannelFactory<IContractMachine> factory = null;
        public static IContractMachine channel = null;
   
        public static List<Machine> machines = new List<Machine>();

        static void Main(string[] args)
        {
            Program pr = new Program();
            Console.ReadLine();
            try
            {
                if (factory == null)
                {
                    factory = new ChannelFactory<IContractMachine>(binding, new EndpointAddress(address));
                    channel = factory.CreateChannel();
                }
                if (factory != null && channel != null)
                {
                    machines = channel.GetAllMachines();
                    pr.ShowMachines();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        
            Console.ReadLine();

        }

        void ShowMachines()
        {
            machines.ForEach(m => {
                Console.WriteLine(m.machineId + " " + m.Name + " " + m.Type + " " + m.X + " " + m.Y);
            });
        }

    
    }
}
