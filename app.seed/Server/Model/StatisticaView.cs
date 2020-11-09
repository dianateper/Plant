using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Statistica;

namespace Server.Model
{
    public class StatisticaView
    {
        public StatisticaView() { }
        public StatisticaView(List<double> values, string name) { this.values = values;  this.name = name; }

        public List<double> values = new List<double>();

        public string name { get; set; }

        public double Min => Statistica.Statistica.Min(values);

        public double Max => Statistica.Statistica.Max(values);

        public double Mean => Statistica.Statistica.Mean(values);

        public double Range => Statistica.Statistica.Range(values);

        public double Median => Statistica.Statistica.Median(values);

        public double Variance => Statistica.Statistica.Variance(values);

        public double StandartDeriation => Statistica.Statistica.StandartDeriation(values);

        public double Skewnes => Statistica.Statistica.Skewnes(values);

        public double Kurtosis => Statistica.Statistica.Kurtosis(values);


    }
}
