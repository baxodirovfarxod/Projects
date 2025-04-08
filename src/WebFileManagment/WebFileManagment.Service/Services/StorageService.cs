
namespace WebFileManagment.Service.Services;

public class StorageService : IStorageService
{
    public Task CopyFileChunkAsync(string filePath, string newFileName)
    {
        throw new NotImplementedException();
    }

    public Task CreateDirectoryAsync(string directoryPath)
    {
        throw new NotImplementedException();
    }

    public Task DeleteDirectoryAsync(string directoryPath)
    {
        throw new NotImplementedException();
    }

    public Task DeleteFileAsync(string filePath)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> DownloadFileAsync(string filePath)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> DownloadFolderAsZipAsync(string directoryPath)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetAllFilesAndDirectoriesAsync(string directoryPath)
    {
        throw new NotImplementedException();
    }

    public Task UploadFileAsync(string filePath, Stream stream)
    {
        throw new NotImplementedException();
    }
}
