using System;

namespace Models.Model
{
    public class Controller
    {
        int controllerId;

        int positionId;

       
        public int ControllerId { get { return controllerId; } set { controllerId = value; } }

    
        public double temperature { get; set; }

        public double humidity { get; set; }

       
        public DateTime date { get; set; }

      
        public int PositionId { get { return positionId; } set { positionId = value; } }
    }
}
