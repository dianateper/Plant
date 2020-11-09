using System.Collections.Generic;

namespace Server.Model
{
    public class ControllerStatistica
    {

        public List<ControllerHistory> controllerHistories = new List<ControllerHistory>();
        public List<StatisticaView> statisticaViews = new List<StatisticaView>();
       
        public ControllerStatistica()
        {
            
        }

        public ControllerStatistica(List<ControllerHistory> controllerHistories)
        {
            this.controllerHistories = controllerHistories;
            List<double> temperatures = new List<double>();
            List<double> humidities = new List<double>();

            this.controllerHistories.ForEach(c=>
            {
                temperatures.Add(c.temperature);
                humidities.Add(c.humidity);
            });

            StatisticaView temperatureStatistica = new StatisticaView(temperatures,"Temperature");
            StatisticaView humidityStatistica = new StatisticaView(humidities, "Humidity");

            statisticaViews.Add(temperatureStatistica);
            statisticaViews.Add(humidityStatistica);

        }

    }
}
