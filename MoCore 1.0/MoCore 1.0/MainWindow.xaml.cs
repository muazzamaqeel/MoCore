using System;
using System.Diagnostics;
using System.Management;
using System.Windows;
using System.Windows.Threading;

namespace MoCore_1_0
{
    public partial class MainWindow : Window
    {
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            InitializePerformanceCounters();
            InitializeTimer();
        }

        private void InitializePerformanceCounters()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdatePerformanceData();
        }

        private void UpdatePerformanceData()
        {
            float cpuUsage = cpuCounter.NextValue();
            float availableMemory = ramCounter.NextValue();
            float totalMemory = GetTotalMemoryInMB();

            CpuUsageText.Text = $"CPU Usage: {cpuUsage:F1}%";
            MemoryUsageText.Text = $"Memory Usage: {totalMemory - availableMemory:F1} MB / {totalMemory:F1} MB";
        }

        private float GetTotalMemoryInMB()
        {
            var query = new ObjectQuery("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
            using (var searcher = new ManagementObjectSearcher(query))
            {
                foreach (var item in searcher.Get())
                {
                    return Convert.ToSingle(item["TotalPhysicalMemory"]) / (1024 * 1024);
                }
            }
            return 0;
        }

        private void StartMonitoring_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
    }
}
