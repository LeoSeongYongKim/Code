using System.Threading;

namespace FileUpload.Services
{
    public class FileProcessService
    {
        private readonly FileUploadQueue _queue;
        public FileProcessService(FileUploadQueue queue)
        {
            _queue = queue;
        }

        public async Task FileUploadSimulation(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested){
                var request = _queue.Dequeue();
                if (request != null)
                {
                    request.Status = "Processing";
                    // Simulate file upload processing 10초
                    await Task.Delay(10000);

                    request.Status = "Completed";
                }
                await Task.Delay(1000); 
            }
        }
    }
}
