using System.Runtime.Serialization;

namespace Models.Model
{

    public class Position
    {
        
        public int PositionId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Position() { }

        public Position(int PositionId, int X, int Y)
        {
            this.PositionId = PositionId;
            this.X = X;
            this.Y = Y;
        }

    }
}
