using Microsoft.AspNetCore.Mvc;
using WebFileManagment.Service.Services;

namespace WebFileManagment.Server.Controllers
{
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService storageService;
        public StorageController(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        [HttpPost("uploadFile")]
        public async Task UploadFile(IFormFile file, string? directoryPath)
        {
            directoryPath = directoryPath ?? string.Empty;
            directoryPath = Path.Combine(directoryPath, file.FileName);

            using (var stream = file.OpenReadStream())
            {
                await storageService.UploadFileAsync(directoryPath, stream);
            }
        }

        [HttpPost("uploadFiles")]
        public async Task UploadFiles(List<IFormFile> files, string? directoryPath)
        {
            directoryPath = directoryPath ?? string.Empty;
            var mainPath = directoryPath;
            if (files == null || files.Count == 0)
            {
                throw new Exception("files is empty or null");
            }

            foreach (var file in files)
            {
                directoryPath = Path.Combine(mainPath, file.FileName);

                using (var stream = file.OpenReadStream())
                {
                    await storageService.UploadFileAsync(directoryPath, stream);
                }
            }
        }

        [HttpPost("createFolder")]
        public async Task CreateFolder(string folderPath)
        {
            await storageService.CreateDirectoryAsync(folderPath);
        }

        [HttpGet("getAll")]
        public async Task<List<string>> GetAllInFolderPath(string? directoryPath)
        {
            directoryPath = directoryPath ?? string.Empty;
            var all = await storageService.GetAllFilesAndDirectoriesAsync(directoryPath);
            return all;
        }

        [HttpGet("downloadFile")]
        public async Task<FileStreamResult> DownloadFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("Error");
            }

            var fileName = Path.GetFileName(filePath);

            var stream = await storageService.DownloadFileAsync(filePath);


            var res = new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = fileName,
            };

            return res;
        }

        [HttpGet("downloadFolderAsZip")]
        public async Task<FileStreamResult> DownloadFolderAsZip(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new Exception("Error");
            }

            var directoryName = Path.GetFileName(directoryPath);

            var stream = await storageService.DownloadFolderAsZipAsync(directoryPath);

            var res = new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = directoryName + ".zip",
            };

            return res;
        }

        [HttpDelete("deleteFile")]
        public async Task DeleteFile(string filePath)
        {
            await storageService.DeleteFileAsync(filePath);
        }

        [HttpDelete("deleteDirectory")]
        public async Task DeleteDirectory(string directoryPath)
        {
            await storageService.DeleteDirectoryAsync(directoryPath);
        }
    }
}
