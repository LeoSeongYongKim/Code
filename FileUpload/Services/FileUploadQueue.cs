using FileUpload.Controllers;
using System.Collections.Concurrent;

namespace FileUpload.Services
{
    public class FileUploadQueue
    {
        private readonly ConcurrentQueue<FileUploadRequest> _queue = new();
        private readonly Dictionary<Guid, FileUploadRequest> _processingTasks = new();

        public Guid Enqueue(FileUploadRequest request)
        {
            var id = Guid.NewGuid();
            _queue.Enqueue(request);
            _processingTasks[id] = request;
            return id;
        }

        public FileUploadRequest Dequeue()
        {
            if (_queue.TryDequeue(out var request))
            {
                return request;
            }
            return null;
        }

        public FileUploadRequest GetStatus(Guid id)
        {
            _processingTasks.TryGetValue(id, out var request);
            return request;
        }
    }


    public class FileUploadRequest
    {
        public string FileName { get; set; }
        public string Status { get; set; }
        public string FilePath { get; set; }
    }
}
