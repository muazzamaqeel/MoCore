using MoCore_1_0.ConcertClasses;
using System.Windows;

namespace MoCore_1_0.Views.ConcertClasses_Views
{
    public partial class PerformanceMonitorWindow : Window
    {
        private readonly PerformanceMonitor _performanceMonitor;

        public PerformanceMonitorWindow()
        {
            InitializeComponent();
            _performanceMonitor = new PerformanceMonitor();
            PlotView.Model = _performanceMonitor.PlotModel;

            StartMonitoring();
        }

        private async void StartMonitoring()
        {
            await _performanceMonitor.StartMonitoringAsync();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _performanceMonitor.StopMonitoring();
        }
    }
}
