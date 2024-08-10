using MoCore_1_0.Interfaces;
using MoCore_1_0.ConcertClasses;
using System;
using System.Windows;

namespace MoCore_1_0.Views
{
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
        }

        // Factory Method to create tools based on a string identifier
        public IToolBase CreateTool(string toolType)
        {
            switch (toolType)
            {
                case "PerformanceMonitor":
                    return new PerformanceMonitor();
                case "FileSorting":
                    return new FileSorting();
                case "Encryption":
                    return new EncryptionDecryption();
                default:
                    throw new ArgumentException("Invalid tool type");
            }
        }

        // Example usage of the factory method within the Dashboard class
        public void UseTool(string toolType)
        {
            IToolBase tool = CreateTool(toolType);
            tool.Execute();
        }
    }
}
