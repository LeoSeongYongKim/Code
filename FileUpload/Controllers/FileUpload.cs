using FileUpload.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace FileUpload.Controllers
{

    public class FileUpload : ControllerBase
    {
        private readonly FileUploadQueue _queue;

        public FileUpload(FileUploadQueue queue)
        {
            _queue = queue;
        }
        [HttpPost("/upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {

            var fileName = file == null || file.Length == 0 ? "FakeName" : file.FileName;
            var request = new FileUploadRequest
            {
                FileName = fileName,
                Status = "Pending",
                FilePath = Path.Combine("jincl/fileupload", fileName) // Save file path
            };
            var jobId = _queue.Enqueue(request);
            
            return Accepted(new { jobId });
        }

        [HttpGet("/status/{jobId}")]
        public IActionResult GetJobStatus(Guid jobId)
        {
            var status = _queue.GetStatus(jobId);
            if (status == null)
                return NotFound();

            return Ok(status);

        }

    }
}
