using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermalOperations
{
    public static class DataConverting
    {
        public static List<int[,]> RawDataToArray(List<string> raw)
        {
            Trace.WriteLine(raw.Count);
            List<int[,]> temperatureData = DeclareTemperatureData(raw.Count);
            bool exception = false;
            Parallel.For(0, raw.Count, (i, loopstate) =>
            {
                String[] rows = raw[i].Split(' ');
                int index = 0;
                for (int x = 0; x < 120; x++)
                {
                    for (int y = 0; y < 160; y++)
                    {
                        try
                        {
                            temperatureData[i][x, y] = int.Parse(rows[index]);
                            index++;
                        }
                        catch (Exception ex)
                        {
                            exception = true;
                            WriteExceptionInfos(ex);
                            break;
                        }
                        if (exception) break;
                    }
                    if (exception) break;
                }
                if (exception) loopstate.Stop();
            }
            );
            if (exception) return null;
            return temperatureData;
        }

        static private List<int[,]> DeclareTemperatureData(int count)
        {
            List<int[,]> temperatureData = new List<int[,]>();
            for (int i = 0; i < count; i++)
            {
                temperatureData.Add(new int[120, 160]);
            }
            return temperatureData;
        }
        static private void WriteExceptionInfos(Exception ex)
        {
            Trace.WriteLine("");
            Trace.WriteLine("Exception");
            Trace.WriteLine("Source: " + ex.Source);
            Trace.WriteLine("Target: " + ex.TargetSite);
            Trace.WriteLine("Inner: " + ex.InnerException);
            Trace.WriteLine("Message: " + ex.Message);
            Trace.WriteLine("HResult " + ex.HResult);
            Trace.WriteLine("Stack: " + ex.StackTrace);
            Trace.WriteLine("");
        }


        static public ThermalFile CreateThermalImages(ThermalFile thermalFile)
        {
            thermalFile.count = thermalFile.temperatureData.Count;
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 160, 120);
            if (thermalFile.temperatureData != null)
            {
                Emgu.CV.CvInvoke.UseOpenCL = false;
                List<Emgu.CV.UMat> ThermalImage = declareThermalImage(thermalFile);
                List<Emgu.CV.Matrix<int>> intMatrices = ScaleIntensity(thermalFile);
                double minTemperature = thermalFile.minTemperature;
                double maxTemperature = thermalFile.maxTemperature;
                Parallel.For(0, thermalFile.count, i =>
                {
                    Emgu.CV.UMat ColorImg = new Emgu.CV.UMat();
                    Emgu.CV.UMat UMatFloatImg;
                    Emgu.CV.UMat UMatFloatImg2 = new Emgu.CV.UMat();

                    intMatrices[i] = (intMatrices[i] - minTemperature);
                    UMatFloatImg = new Emgu.CV.UMat(intMatrices[i].ToUMat(), rect);
                    UMatFloatImg.ConvertTo(UMatFloatImg, Emgu.CV.CvEnum.DepthType.Cv8U, (1 / maxTemperature) * 255);
                    Emgu.CV.CvInvoke.ApplyColorMap(UMatFloatImg, UMatFloatImg2, Emgu.CV.CvEnum.ColorMapType.Hot);
                    ThermalImage[i] = UMatFloatImg2.Clone();
                });
                thermalFile.images = ThermalImage;
                return thermalFile;
            }
            return null;
        }

        static private List<Emgu.CV.UMat> declareThermalImage(ThermalFile thermalFile)
        {
            Trace.WriteLine("Images: " + thermalFile.count);
            List<Emgu.CV.UMat> ThermalImages = new List<Emgu.CV.UMat>();
            for (int i = 0; i < thermalFile.count; i++)
            {
                ThermalImages.Add(new Emgu.CV.UMat());
            }
            return ThermalImages;
        }
        static private List<Emgu.CV.Matrix<int>> ScaleIntensity(ThermalFile thermalFile)
        {
            List<Emgu.CV.Matrix<int>> intMatrices = new List<Emgu.CV.Matrix<int>>();
            List<double> minTemps = new List<double>();
            List<double> maxTemps = new List<double>();
            for (int i = 0; i < thermalFile.count; i++)
            {
                intMatrices.Add(new Emgu.CV.Matrix<int>(new System.Drawing.Size(160, 120)));
                minTemps.Add(new double());
                maxTemps.Add(new double());
            }
            Parallel.For(0, thermalFile.count, i =>
            {
                intMatrices[i] = new Emgu.CV.Matrix<int>(thermalFile.temperatureData[i]);
                double minT;
                double maxT;
                System.Drawing.Point minTPoint;
                System.Drawing.Point maxTPoint;
                intMatrices[i].MinMax(out minT, out maxT, out minTPoint, out maxTPoint);
                minTemps[i] = minT;
                maxTemps[i] = maxT;

            });

            double minTemp = ((from l in minTemps select l).Min());
            double maxTemp = ((from l in maxTemps select l).Max());

            maxTemp = maxTemp - ((maxTemp - minTemp) * 0.75);
            Trace.WriteLine("min: " + minTemp);
            Trace.WriteLine("max: " + maxTemp);
            thermalFile.minTemperature = minTemp;
            thermalFile.maxTemperature = maxTemp;
            return intMatrices;
        }
    }
}
