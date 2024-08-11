using MoCore_1_0.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MoCore_1_0.ConcertClasses
{
    public class FileSorting : IToolBase
    {
        public static async Task Execute(string folderPath, IProgress<int> progress)
        {
            // Run the file sorting logic on a background thread
            await Task.Run(() =>
            {
                string outputFolder = Path.Combine(folderPath, "Mo-FileSorting-Output");

                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                var files = Directory.GetFiles(folderPath);
                int totalFiles = files.Length;
                int filesProcessed = 0;

                foreach (var file in files)
                {
                    string extension = Path.GetExtension(file).TrimStart('.').ToUpperInvariant();
                    string destinationFolder = Path.Combine(outputFolder, extension);

                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }

                    string fileName = Path.GetFileName(file);
                    string destinationPath = Path.Combine(destinationFolder, fileName);

                    if (!File.Exists(destinationPath))
                    {
                        File.Move(file, destinationPath);
                    }

                    filesProcessed++;
                    progress.Report((filesProcessed * 100) / totalFiles);
                }
            });
        }

        // Explicit interface implementation
        void IToolBase.Execute()
        {
            // If you want this method to trigger the file sorting process,
            // you can call the static Execute method with default parameters or throw an exception.
            throw new NotImplementedException("Please use the static Execute method with parameters.");
        }
    }
}
