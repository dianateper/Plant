using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Statistica;

namespace Server.Model
{
    class StatisticaView
    {

        public List<double> array { get; set; }

        public double Min => Statistica.Statistica.Min(array);

        public double Max => Statistica.Statistica.Max(array);

        public double Mean => Statistica.Statistica.Mean(array);

        public double Median => Statistica.Statistica.Median(array);

        public double Range => Statistica.Statistica.Range(array);

        public double Variance => Statistica.Statistica.Variance(array);

        public double StandartDeriviation => Statistica.Statistica.StandartDeriation(array);

        public double Skewnes => Statistica.Statistica.Skewnes(array);

        public double Kurtosis => Statistica.Statistica.Kurtosis(array);


    }
}
