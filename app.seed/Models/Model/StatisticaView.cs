using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class StatisticaView
    {
        public StatisticaView() { }
        public StatisticaView(List<double> values, string name) { this.values = values;  this.name = name; }

        public List<double> values = new List<double>();

        public string name { get; set; }

        public double Min => Statistica.Min(values);

        public double Max => Statistica.Max(values);

        public double Mean => Statistica.Mean(values);

        public double Range => Statistica.Range(values);

        public double Median => Statistica.Median(values);

        public double Variance => Statistica.Variance(values);

        public double StandartDeriation => Statistica.StandartDeriation(values);

        public double Skewnes => Statistica.Skewnes(values);

        public double Kurtosis => Statistica.Kurtosis(values);


    }
}
