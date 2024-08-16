using System;
using System.Windows;
using MoCore_1_0.ConcertClasses;
using MoCore_1_0.Views.ConcertClasses_Views; // Corrected namespace for views

namespace MoCore_1_0.Views
{
    public partial class DashboardWindow : Window
    {
        private readonly ToolFacade _toolFacade;

        public DashboardWindow()
        {
            InitializeComponent();
            _toolFacade = new ToolFacade();
        }

        private void FileSorting_Click(object sender, RoutedEventArgs e)
        {
            FileSortingWindow fileSortingWindow = new FileSortingWindow(_toolFacade);
            fileSortingWindow.Show();
            this.Close();
        }

        private void EncryptionDecryption_Click(object sender, RoutedEventArgs e)
        {
            _toolFacade.ExecuteEncryption();
            MessageBox.Show("Encryption/Decryption completed!");
        }

        private void PerformanceMonitor_Click(object sender, RoutedEventArgs e)
        {
            // Open the PerformanceMonitorWindow instead of just executing the tool
            PerformanceMonitorWindow performanceMonitorWindow = new PerformanceMonitorWindow();
            performanceMonitorWindow.Show();
            this.Close();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
