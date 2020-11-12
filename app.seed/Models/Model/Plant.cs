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

     
        public int X { get; set; }

    
        public int Y { get; set; }

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
