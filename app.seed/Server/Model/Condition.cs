using System.Runtime.Serialization;

namespace Server.Model
{
    [DataContract]
    class Condition
    {
        [DataMember]
        public int ConditionId { get; set; }

        [DataMember]
        public string Soil { get; set; }

        [DataMember]
        public double MinTmp { get; set; }

        [DataMember]
        public double MaxTmp { get; set; }

        [DataMember]
        public double MinHumidity { get; set; }

        [DataMember]
        public double MaxHumidity { get; set; }

    }
}
