using Server.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Controllers
{
    class Program
    {
        static Uri address = new Uri("http://127.0.0.1:4000/IContractControllers");
        static BasicHttpBinding binding = new BasicHttpBinding();
        static ChannelFactory<IContractControllers> factory = null;
        public static IContractControllers channel = null;
        Random rnd = new Random();
        public static List<Controller> controllers = new List<Controller>();

        static void Main(string[] args)
        {
            Program pr = new Program();
            Console.ReadLine();
            try
            {
                if (factory == null)
                {
                    factory = new ChannelFactory<IContractControllers>(binding, new EndpointAddress(address));
                    channel = factory.CreateChannel();
                }
                if (factory != null && channel != null)
                {
                    controllers = channel.GetAllControllers();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }

            pr.ShowMenu();
            string action = Console.ReadLine();
            while (action.Equals("1"))
            {
                pr.SetParameter();
                pr.ShowController();
                pr.ShowMenu();
                action = Console.ReadLine();
            }

            Console.ReadLine();
           
        }

        void ShowController()
        {
            controllers.ForEach(c => {
                Console.WriteLine(c.ControllerId + " " + c.PositionId + " " + c.temperature + " " + c.humidity);
            });
        }

        void ShowMenu()
        {
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Get temperature and humidity");
            Console.WriteLine("2) Exit");
            Console.Write("\r\nSelect an option: ");
        }

        void SetParameter()
        {

            int temperature = rnd.Next(15, 31);
            int humidity = rnd.Next(50, 81);


            double minT = 0.3;
            double maxT = 1.7;

            double minH = 1;
            double maxH = 3;

            controllers.ForEach(c =>
            {
                c.temperature = Math.Round(temperature  + minT + rnd.NextDouble() * (maxT - minT), 1);
                c.humidity = Math.Round(humidity + minH + rnd.NextDouble() * (maxH - minH),2);
            });
        }
    }
}
