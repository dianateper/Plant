using System.Runtime.Serialization;

namespace Server.Model
{
    [DataContract]
    class Position
    {
        [DataMember]
        public int PositionId { get; set; }

        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

    }
}
