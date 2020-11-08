using System;
using System.Runtime.Serialization;

namespace Server.Model
{
  
    [DataContract(Namespace = "WebClient")]
    public class Plant
    {
        int plantId;

        string name;

        string iconName;

        [DataMember]
        public int PlantId { 
            get { return plantId; } 
            set { plantId = value; }
        }

        [DataMember]
        public string Name {
            get { return name; }
            set { name = value; }
        }

        [DataMember(IsRequired = false)]
        public string IconName {
            get { return iconName; }
            set { iconName = value; }
        }

        [DataMember(IsRequired = false)]
        public int X { get; set; }

        [DataMember(IsRequired = false)]
        public int Y { get; set; }

    }
}
