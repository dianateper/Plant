using Models.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

            if (Console.ReadLine().Equals("1"))
            {
                pr.ReadFile();
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

        void SetParameter(DateTime date, double temperature, double humidity)
        {

            double minT = 0.1;
            double maxT = 1.1;

            double minH = 1;
            double maxH = 3;

            controllers.ForEach(c =>
            {
                c.temperature = Math.Round(temperature  + minT + rnd.NextDouble() * (maxT - minT), 1);
                c.humidity = Math.Round(humidity + minH + rnd.NextDouble() * (maxH - minH),2);
            });

            ShowController();
            channel.SendControllers(date, controllers);
        }


        void ReadFile()
        {
            int idx = 1;
     
            using (var rd = new StreamReader("../../weather_data_kiev_jun_jul_2020.csv"))
            {
                rd.ReadLine();
                while (!rd.EndOfStream && idx != 6)
                {
                    var splits = rd.ReadLine().Split(',');
                    idx++;
                
                    DateTime date = DateTime.ParseExact(splits[0], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    double humidity = double.Parse(splits[1], CultureInfo.InvariantCulture);
                    double temperature = double.Parse(splits[2], CultureInfo.InvariantCulture);

                    SetParameter(date, temperature, humidity);
                }
            }
        }


    }
}
