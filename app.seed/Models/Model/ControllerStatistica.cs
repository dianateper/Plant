using System.Collections.Generic;

namespace Models.Model
{
    public class ControllerStatistica
    {

        public List<ControllerHistory> controllerHistories = new List<ControllerHistory>();
        public StatisticaView statisticaTemperature;
        public StatisticaView statisticaHumidity;

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

            statisticaTemperature = new StatisticaView(temperatures,"Temperature");
            statisticaHumidity = new StatisticaView(humidities, "Humidity");

        }

    }
}
