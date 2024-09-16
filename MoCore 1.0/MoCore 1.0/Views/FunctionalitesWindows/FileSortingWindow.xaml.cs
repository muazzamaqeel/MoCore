using MoCore_1_0.ConcertClasses;
using System;
using System.Windows;
using System.Windows.Forms;

namespace MoCore_1_0.Views
{
    public partial class FileSortingWindow : Window
    {
        private readonly ToolFacade _toolFacade;

        internal FileSortingWindow(ToolFacade toolFacade) // Make internal to match ToolFacade accessibility
        {
            InitializeComponent();
            _toolFacade = toolFacade;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            using var folderDialog = new FolderBrowserDialog();
            {
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedFolderPath = folderDialog.SelectedPath;
                    ProgressBar.Visibility = Visibility.Visible;
                    StatusText.Text = "Sorting files...";

                    var progress = new Progress<int>(value => ProgressBar.Value = value);

                    await _toolFacade.ExecuteFileSorting(selectedFolderPath, progress);

                    StatusText.Text = "File sorting completed!";
                    ShowStats(selectedFolderPath);
                }
            }
        }

        private void ShowStats(string outputFolder)
        {
            var directories = System.IO.Directory.GetDirectories(System.IO.Path.Combine(outputFolder, "Mo-FileSorting-Output"));
            int totalFilesMoved = 0;

            foreach (var dir in directories)
            {
                totalFilesMoved += System.IO.Directory.GetFiles(dir).Length;
            }

            string message = $"Sorting Complete!\nTotal Files Moved: {totalFilesMoved}\n";
            foreach (var dir in directories)
            {
                string folderName = System.IO.Path.GetFileName(dir);
                int fileCount = System.IO.Directory.GetFiles(dir).Length;
                message += $"{folderName}: {fileCount} files\n";
            }

            System.Windows.MessageBox.Show(message, "Sorting Stats", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
