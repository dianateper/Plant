using System.Runtime.Serialization;

namespace Models.Model
{
    
    class Condition
    {
     
        public int ConditionId { get; set; }

       
        public string Soil { get; set; }

      
        public double MinTmp { get; set; }

        public double MaxTmp { get; set; }

       
        public double MinHumidity { get; set; }

        public double MaxHumidity { get; set; }

    }
}
