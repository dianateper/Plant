using System.Collections.Generic;
using Extreme.Statistics.TimeSeriesAnalysis;
using Extreme.Mathematics;
using System.Linq;

namespace Server.Arima
{
    public class Arima
    {

        private double[] data;
        private int predictionSize;

        private int p;

        private int d;

        private int q;

        public Arima(double[] data, int predictionSize, int p, int d, int q)
        {
            this.predictionSize = predictionSize;
            this.data = data;
            this.p = p;
            this.d = d;
            this.q = q;
        }

        public List<double> MakeArimaPrediction()
        {
            ArimaModel model = new ArimaModel(data, p, d, q);
            model.Fit();
            Vector<double> nextValues = model.Forecast(predictionSize);

            return new List<double>(nextValues).Select(x => System.Math.Round(x, 2)).ToList();
        }

      
    }
}
