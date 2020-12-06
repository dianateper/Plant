using System.Collections.Generic;
using Extreme.Statistics.TimeSeriesAnalysis;
using Extreme.Mathematics;
using System.Linq;
using System;
using System.Windows;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Win32;

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

        public Arima(int q, int d, int p)
        {
            this.p = p;
            this.d = d;
            this.q = q;
           
        }

     

        public List<double> MakePredition()
        {
            List<double> forecast = new List<double>();
            try
            {
                var psi = new ProcessStartInfo();
                //psi.FileName = @"C:\Users\admin\AppData\Local\Programs\Python\Python39\python.exe";
                psi.FileName = GetPythonPath();
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
            }
            return forecast;

        }



        public List<double> MakeArimaPrediction()
        {
            ArimaModel model = new ArimaModel(data, p, d, q);
            model.Fit();
            Vector<double> nextValues = model.Forecast(predictionSize);
            return new List<double>(nextValues).Select(x => System.Math.Round(x, 2)).ToList();
        }

        private static string GetPythonPath(string requiredVersion = "", string maxVersion = "")
        {
            string[] possiblePythonLocations = new string[3] {
                @"HKLM\SOFTWARE\Python\PythonCore\",
                @"HKCU\SOFTWARE\Python\PythonCore\",
                @"HKLM\SOFTWARE\Wow6432Node\Python\PythonCore\"
            };

            
            Dictionary<string, string> pythonLocations = new Dictionary<string, string>();

            foreach (string possibleLocation in possiblePythonLocations)
            {
                string regKey = possibleLocation.Substring(0, 4), actualPath = possibleLocation.Substring(5);
               
                using (var theKey = RegistryKey.OpenBaseKey(regKey == "HKLM" ? RegistryHive.LocalMachine : RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    RegistryKey theValue = theKey.OpenSubKey(actualPath);

                    foreach (var v in theValue.GetSubKeyNames())
                    {
                        RegistryKey productKey = theValue.OpenSubKey(v);
                        if (productKey != null)
                        {
                            try
                            {
                                if (productKey.OpenSubKey("InstallPath") != null && productKey.OpenSubKey("InstallPath").GetValue("ExecutablePath")!=null)
                                {
                                    string pythonExePath = productKey.OpenSubKey("InstallPath").GetValue("ExecutablePath").ToString();
                                    if (pythonExePath != null && pythonExePath != "")
                                    {

                                        pythonLocations.Add(v.ToString(), pythonExePath);
                                    }
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            if (pythonLocations.Count > 0)
            {
                System.Version desiredVersion = new System.Version(requiredVersion == "" ? "0.0.1" : requiredVersion),
                    maxPVersion = new System.Version(maxVersion == "" ? "999.999.999" : maxVersion);

                string highestVersion = "", highestVersionPath = "";

                foreach (KeyValuePair<string, string> pVersion in pythonLocations)
                {
                  
                    int index = pVersion.Key.IndexOf("-"); 
                    string formattedVersion = index > 0 ? pVersion.Key.Substring(0, index) : pVersion.Key;

                    System.Version thisVersion = new System.Version(formattedVersion);
                    int comparison = desiredVersion.CompareTo(thisVersion),
                        maxComparison = maxPVersion.CompareTo(thisVersion);

                    if (comparison <= 0)
                    {
                        if (maxComparison >= 0)
                        {
                            desiredVersion = thisVersion;

                            highestVersion = pVersion.Key;
                            highestVersionPath = pVersion.Value;
                        }
                        else
                        {
                           
                        }
                    }
                    else
                    {
                       
                    }
                }

                return highestVersionPath;
            }

            return "";
        }
    }
}
