using MoCore_1_0.Interfaces;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Diagnostics;
using System.Management;
using System.Threading.Tasks;

namespace MoCore_1_0.ConcertClasses
{
    public class PerformanceMonitor : IToolBase
    {
        private LineSeries cpuSeries;
        private LineSeries memorySeries;
        private LineSeries diskSeries;
        private LineSeries gpuSeries;
        private PerformanceCounter cpuCounter;
        private PerformanceCounter memoryCounter;
        private PerformanceCounter diskCounter;
        private bool monitoring;

        public PlotModel PlotModel { get; private set; }

        public PerformanceMonitor()
        {
            PlotModel = new PlotModel { Title = "System Performance", TextColor = OxyColors.White };

            cpuSeries = new LineSeries { Title = "CPU Usage (%)", MarkerType = MarkerType.None, StrokeThickness = 2, Color = OxyColors.Green };
            memorySeries = new LineSeries { Title = "Memory Usage (%)", MarkerType = MarkerType.None, StrokeThickness = 2, Color = OxyColors.Blue };
            diskSeries = new LineSeries { Title = "Disk Usage (%)", MarkerType = MarkerType.None, StrokeThickness = 2, Color = OxyColors.Red };
            gpuSeries = new LineSeries { Title = "GPU Usage (%)", MarkerType = MarkerType.None, StrokeThickness = 2, Color = OxyColors.Yellow };

            PlotModel.Series.Add(cpuSeries);
            PlotModel.Series.Add(memorySeries);
            PlotModel.Series.Add(diskSeries);
            PlotModel.Series.Add(gpuSeries);

            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            memoryCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            diskCounter = new PerformanceCounter("LogicalDisk", "% Disk Time", "_Total");

            monitoring = false;
        }

        public async Task StartMonitoringAsync()
        {
            monitoring = true;
            int time = 0;

            while (monitoring)
            {
                double cpuUsage = cpuCounter.NextValue();
                double memoryUsage = memoryCounter.NextValue();
                double diskUsage = diskCounter.NextValue();
                double gpuUsage = GetGpuUsage();

                cpuSeries.Points.Add(new DataPoint(time, cpuUsage));
                memorySeries.Points.Add(new DataPoint(time, memoryUsage));
                diskSeries.Points.Add(new DataPoint(time, diskUsage));
                gpuSeries.Points.Add(new DataPoint(time, gpuUsage));

                if (cpuSeries.Points.Count > 60) // Keep the graph within a 1-minute window
                {
                    cpuSeries.Points.RemoveAt(0);
                    memorySeries.Points.RemoveAt(0);
                    diskSeries.Points.RemoveAt(0);
                    gpuSeries.Points.RemoveAt(0);
                }

                time++;
                await Task.Delay(500); // Delay between each data point to avoid overwhelming the UI
            }
        }

        public void StopMonitoring()
        {
            monitoring = false;
        }

        private double GetGpuUsage()
        {
            double gpuLoad = 0;
            using (var searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_GPUPerformanceCounters_GPUEngine"))
            {
                foreach (var obj in searcher.Get())
                {
                    gpuLoad += Convert.ToDouble(obj["UtilizationPercentage"]);
                }
            }
            return gpuLoad;
        }

        public void Execute()
        {
            StartMonitoringAsync().Wait();
            Console.WriteLine("Performance monitoring executed.");
        }
    }
}
