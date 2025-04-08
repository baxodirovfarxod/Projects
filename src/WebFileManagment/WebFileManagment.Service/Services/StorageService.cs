
using WebFileManagment.StorageBroker.Services;

namespace WebFileManagment.Service.Services;

public class StorageService : IStorageService
{
    public readonly IStorageBrokerService storageBrokerService;

    public StorageService(IStorageBrokerService storageBrokerService)
    {
        this.storageBrokerService = storageBrokerService;
    }

    public async Task CopyFileChunkAsync(string filePath, string newFileName)
    {
        await storageBrokerService.CopyFileChunkAsync(filePath, newFileName);
    }

    public async Task CreateDirectoryAsync(string directoryPath)
    {
        await storageBrokerService.CreateDirectoryAsync(directoryPath);
    }

    public async Task DeleteDirectoryAsync(string directoryPath)
    {
        await storageBrokerService.DeleteDirectoryAsync(directoryPath);
    }

    public async Task DeleteFileAsync(string filePath)
    {
        await storageBrokerService.DeleteFileAsync(filePath);
    }

    public async Task<Stream> DownloadFileAsync(string filePath)
    {
       return await storageBrokerService.DownloadFileAsync(filePath);
    }

    public async Task<Stream> DownloadFolderAsZipAsync(string directoryPath)
    {
        return await storageBrokerService.DownloadFolderAsZipAsync(directoryPath);  
    }

    public async Task<List<string>> GetAllFilesAndDirectoriesAsync(string directoryPath)
    {
        return await storageBrokerService.GetAllFilesAndDirectoriesAsync(directoryPath);
    }

    public async Task UploadFileAsync(string filePath, Stream stream)
    {
        return await storageBrokerService.UploadFileAsync(filePath, stream);
    }
}
