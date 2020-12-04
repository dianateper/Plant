using System.Collections.Generic;
using Extreme.Statistics.TimeSeriesAnalysis;
using Extreme.Mathematics;
using System.Linq;
using IronPython.Hosting;
using System.IO;
using System;
using System.Windows;
using Microsoft.Scripting.Hosting;
using System.Diagnostics;
using System.Globalization;

namespace Server.Arima
{
    public class Arima
    {

        private double[] data;
        private int predictionSize;

        private int p;
        private int d;
        private int q;

        
        private readonly double[][] diff_seasonal;

        public Arima(double[] data, int predictionSize, int p, int d, int q)
        {
            this.predictionSize = predictionSize;
            this.data = data;
            this.p = p;
            this.d = d;
            this.q = q;
        }

        public Arima(int q, int d, int p)
        {
            this.p = p;
            this.d = d;
            this.q = q;
            diff_seasonal = new double[d][];
        }

        public List<double> MakeArimaPrediction()
        {
            ArimaModel model = new ArimaModel(data, p, d, q);
            model.Fit();
            Vector<double> nextValues = model.Forecast(predictionSize);

            return new List<double>(nextValues).Select(x => System.Math.Round(x, 2)).ToList();
        }


        public List<double> MakePredition()
        {
            List<double> forecast = new List<double>();/*
            try
            {
                var psi = new ProcessStartInfo();
                psi.FileName = @"C:\Users\admin\AppData\Local\Programs\Python\Python39\python.exe";
                var script = @"..\..\Arima\ArimaScript.py";
               
                var df = string.Join(",", data);
                psi.Arguments = $"\"{script}\" \"{predictionSize}\" \"{p}\" \"{d}\" \"{q}\" \"{df}\"";

                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;

                var results = "";
                var errors = "";

                using (var process = Process.Start(psi))
                {
                    errors = process.StandardError.ReadToEnd();
                    results = process.StandardOutput.ReadToEnd();
                }
                foreach(string res in results.Split(','))
                {
                    forecast.Add(double.Parse(res, CultureInfo.InvariantCulture));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
            return forecast;

        }



        

    }
}
