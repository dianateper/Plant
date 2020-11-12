using System;
using System.Runtime.Serialization;

namespace Models.Model
{

    public class Plant
    {
        int plantId;

        string name;

        string iconName;

        public int PlantId { 
            get { return plantId; } 
            set { plantId = value; }
        }

    
        public string Name {
            get { return name; }
            set { name = value; }
        }

     
        public string IconName {
            get { return iconName; }
            set { iconName = value; }
        }

        public DateTime datetime { get; set; }

     
        public int X { get; set; }

    
        public int Y { get; set; }

        public double minTemperature { get; set; }

        public double maxTemperature { get; set; }

        public double maxHumidity { get; set; }

        public double minHumidity { get; set; }

        public double temperature { get; set; }

        public double humidity { get; set; }

        public Plant() { 
        }

        

        public Plant(Plant plant)
        {
            this.plantId = plant.plantId;
            this.name = plant.name;
            this.iconName = plant.iconName;
            this.X = plant.X;
            this.Y = plant.Y;
        }

    }
}
