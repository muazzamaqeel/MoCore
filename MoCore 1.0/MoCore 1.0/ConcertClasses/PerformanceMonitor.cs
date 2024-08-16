using MoCore_1_0.Interfaces;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MoCore_1_0.ConcertClasses
{
    public class PerformanceMonitor : IToolBase
    {
        private LineSeries cpuSeries;
        private LineSeries memorySeries;
        private LineSeries diskSeries;
        private PerformanceCounter cpuCounter;
        private PerformanceCounter memoryCounter;
        private PerformanceCounter diskCounter;
        private bool monitoring;

        public PlotModel PlotModel { get; private set; }

        public PerformanceMonitor()
        {
            PlotModel = new PlotModel { Title = "System Performance" };

            cpuSeries = new LineSeries { Title = "CPU Usage (%)", MarkerType = MarkerType.None, StrokeThickness = 2 };
            memorySeries = new LineSeries { Title = "Memory Usage (%)", MarkerType = MarkerType.None, StrokeThickness = 2 };
            diskSeries = new LineSeries { Title = "Disk Usage (%)", MarkerType = MarkerType.None, StrokeThickness = 2 };

            PlotModel.Series.Add(cpuSeries);
            PlotModel.Series.Add(memorySeries);
            PlotModel.Series.Add(diskSeries);

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

                cpuSeries.Points.Add(new DataPoint(time, cpuUsage));
                memorySeries.Points.Add(new DataPoint(time, memoryUsage));
                diskSeries.Points.Add(new DataPoint(time, diskUsage));

                if (cpuSeries.Points.Count > 60) // Keep the graph within a 1-minute window
                {
                    cpuSeries.Points.RemoveAt(0);
                    memorySeries.Points.RemoveAt(0);
                    diskSeries.Points.RemoveAt(0);
                }

                PlotModel.InvalidatePlot(true);
                time++;

                await Task.Delay(500); // Reduced delay for faster updates
            }
        }

        public void StopMonitoring()
        {
            monitoring = false;
        }

        public void Execute()
        {
            StartMonitoringAsync().Wait();
            Console.WriteLine("Performance monitoring executed.");
        }
    }
}
