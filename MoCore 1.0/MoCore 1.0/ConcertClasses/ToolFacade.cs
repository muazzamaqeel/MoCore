using MoCore_1_0.Interfaces;
using System;
using System.Threading.Tasks;

namespace MoCore_1_0.ConcertClasses
{
    public class ToolFacade
    {
        private readonly IToolBase _fileSorting;
        private readonly IToolBase _encryptionTool;
        private readonly IToolBase _performanceMonitor;

        public ToolFacade()
        {
            _fileSorting = new FileSorting();
            _encryptionTool = new EncryptionDecryption();
            _performanceMonitor = new PerformanceMonitor();
        }

        public async Task ExecuteFileSorting(string folderPath, IProgress<int> progress)
        {
            // Since Execute is static, call it using the class name
            await FileSorting.Execute(folderPath, progress);
        }

        public void ExecuteEncryption()
        {
            _encryptionTool.Execute();
        }

        public void ExecutePerformanceMonitoring()
        {
            _performanceMonitor.Execute();
        }
    }
}
