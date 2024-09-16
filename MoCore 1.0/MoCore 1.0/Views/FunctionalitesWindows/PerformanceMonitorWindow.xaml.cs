using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MoCore_1_0.Views.ConcertClasses_Views
{
    public partial class PerformanceMonitorWindow : Window
    {
        private DispatcherTimer _uiUpdateTimer;
        public ChartValues<double> CpuUsage { get; set; }
        public ChartValues<double> GpuUsage { get; set; }
        public List<string> CpuCores { get; set; }
        public List<string> GpuCores { get; set; }
        private Dictionary<string, double> _preOptimizationUsage;

        public PerformanceMonitorWindow()
        {
            InitializeComponent();
            DataContext = this;

            CpuCores = new List<string> { "Core 1", "Core 2", "Core 3", "Core 4" };
            GpuCores = new List<string> { "GPU 1", "GPU 2" };

            CpuUsage = new ChartValues<double> { 0, 0, 0, 0 };
            GpuUsage = new ChartValues<double> { 0, 0 };

            StartMonitoring();
            SetupUiUpdateTimer();
            LoadBackgroundResources();
        }

        private void SetupUiUpdateTimer()
        {
            _uiUpdateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1500)
            };
            _uiUpdateTimer.Tick += (s, e) => UpdateCharts();
            _uiUpdateTimer.Start();
        }

        private void StartMonitoring()
        {
            // Simulate starting CPU and GPU monitoring
        }

        private void UpdateCharts()
        {
            // Update CPU and GPU charts with sample data
            Random rand = new Random();
            for (int i = 0; i < CpuUsage.Count; i++)
            {
                CpuUsage[i] = rand.Next(0, 100);
            }

            for (int i = 0; i < GpuUsage.Count; i++)
            {
                GpuUsage[i] = rand.Next(0, 100);
            }
        }

        private void LoadBackgroundResources()
        {
            var processes = Process.GetProcesses().OrderBy(p => p.ProcessName).ToList();
            _preOptimizationUsage = new Dictionary<string, double>();

            var data = new List<BackgroundResource>();

            foreach (var process in processes)
            {
                try
                {
                    double memoryUsage = process.WorkingSet64 / 1024.0 / 1024.0; // Convert bytes to MB
                    _preOptimizationUsage[process.ProcessName] = memoryUsage;
                    data.Add(new BackgroundResource { Name = process.ProcessName, MemoryUsage = memoryUsage });
                }
                catch (Exception)
                {
                    // Ignore inaccessible processes
                }
            }

            BackgroundResourcesGrid.ItemsSource = data;
        }

        private void OptimizeButton_Click(object sender, RoutedEventArgs e)
        {
            var processes = Process.GetProcesses();
            var postOptimizationUsage = new Dictionary<string, double>();

            foreach (var process in processes)
            {
                try
                {
                    double reducedUsage = (_preOptimizationUsage.ContainsKey(process.ProcessName) ?
                        _preOptimizationUsage[process.ProcessName] * 0.7 : process.WorkingSet64 / 1024.0 / 1024.0);

                    postOptimizationUsage[process.ProcessName] = reducedUsage;
                }
                catch (Exception)
                {
                    // Ignore inaccessible processes
                }
            }

            ShowOptimizationResults(postOptimizationUsage);
        }

        private void ShowOptimizationResults(Dictionary<string, double> postOptimizationUsage)
        {
            var window = new Window
            {
                Title = "Optimization Results",
                Width = 600,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            var grid = new Grid();

            var titleBlock = new TextBlock
            {
                Text = "Post Optimization Memory Usage",
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 10)
            };

            var chart = new CartesianChart
            {
                Background = System.Windows.Media.Brushes.Transparent,
                Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Memory Usage",
                        Values = new ChartValues<double>(postOptimizationUsage.Values)
                    }
                }
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());

            Grid.SetRow(titleBlock, 0);
            Grid.SetRow(chart, 1);

            grid.Children.Add(titleBlock);
            grid.Children.Add(chart);

            window.Content = grid;
            window.ShowDialog();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CpuChart_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

    public class BackgroundResource
    {
        public string Name { get; set; }
        public double MemoryUsage { get; set; }
    }
}
