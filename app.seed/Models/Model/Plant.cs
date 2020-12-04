using System;
using System.Runtime.Serialization;

namespace Models.Model
{

    public class Plant
    {
        public int PlantId { get; set; }

        public string Name { get; set; }

        public string IconName { get; set; }

        public DateTime datetime { get; set; }

        public double Price { get; set; }

        public int X { get; set; }
    
        public int Y { get; set; }

        public double minTemperature { get; set; }

        public double maxTemperature { get; set; }

        public double maxHumidity { get; set; }

        public double minHumidity { get; set; }

        public double temperature { get; set; }

        public double humidity { get; set; }

        public double phMin { get; set; }

        public double phMax { get; set; }

        public string soil { get; set; }

        public Plant() { 
        }

        public Plant(Plant plant)
        {
            this.PlantId = plant.PlantId;
            this.Name = plant.Name;
            this.IconName = plant.IconName;
            this.X = plant.X;
            this.Y = plant.Y;
        }
    }
}
