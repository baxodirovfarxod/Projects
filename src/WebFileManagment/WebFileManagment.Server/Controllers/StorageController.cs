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
            throw new NotImplementedException();
        }

        [HttpPost("uploadFiles")]
        public async Task UploadFiles(List<IFormFile> files, string? directoryPath)
        {
            throw new NotImplementedException();
        }

        [HttpPost("createFolder")]
        public async Task CreateFolder(string folderPath)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getAll")]
        public async Task<List<string>> GetAllInFolderPath(string? directoryPath)
        {
            throw new NotImplementedException();
        }

        [HttpGet("downloadFile")]
        public async Task<FileStreamResult> DownloadFile(string filePath)
        {
            throw new NotImplementedException();
        }

        [HttpGet("downloadFolderAsZip")]
        public async Task<FileStreamResult> DownloadFolderAsZip(string directoryPath)
        {  
            throw new NotImplementedException();
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
