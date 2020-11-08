using System;
using System.Runtime.Serialization;

namespace Server.Model
{
    [DataContract(Namespace = "Controllers")]
    public class Controller
    {
        int controllerId;

        int positionId;

        [DataMember]
        public int ControllerId { get { return controllerId; } set { controllerId = value; } }

        [DataMember(IsRequired = false)]
        public double temperature { get; set; }

        [DataMember(IsRequired = false)]
        public double humidity { get; set; }

        [DataMember(IsRequired = false)]
        public DateTime date { get; set; }

        [DataMember]
        public int PositionId { get { return positionId; } set { positionId = value; } }
    }
}
