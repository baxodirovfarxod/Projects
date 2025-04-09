using System.IO.Compression;

namespace WebFileManagment.StorageBroker.Services
{
    public class LocalStorageBrokerService : IStorageBrokerService
    {
        private string _dataPath;
        public LocalStorageBrokerService()
        {
            _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            if (!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
            }
        }
        public async Task CopyFileChunkAsync(string filePath, string newFileName)
        {
            filePath = Path.Combine(_dataPath, filePath);
            var newFilePath = Path.Combine(_dataPath, newFileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Source file '{filePath}' not found.");
            }

            var parentDirectory = Path.GetDirectoryName(newFilePath);
            if (!Directory.Exists(parentDirectory))
            {
                Directory.CreateDirectory(parentDirectory);
            }

            using (var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var destinationStream = new FileStream(newFilePath, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[8192];
                    int bytesRead;


                    while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await destinationStream.WriteAsync(buffer, 0, bytesRead);
                    }
                }
            }
        }

        public async Task CreateDirectoryAsync(string directoryPath)
        {
            await Task.Run(() =>
            {
                directoryPath = Path.Combine(_dataPath, directoryPath);
                ValidateDirectoryPath(directoryPath);
                Directory.CreateDirectory(directoryPath);
            });
        }

        public async Task DeleteDirectoryAsync(string directoryPath)
        {
            await Task.Run(() =>
            {

                directoryPath = Path.Combine(_dataPath, directoryPath);

                if (!Directory.Exists(directoryPath))
                {
                    throw new Exception(directoryPath + " does not exist");
                }

                Directory.Delete(directoryPath, true);

            });
        }
        public async Task DeleteFileAsync(string filePath)
        {
            await Task.Run(() =>
            {
                filePath = Path.Combine(_dataPath, filePath);

                if (!File.Exists(filePath))
                {
                    throw new Exception(filePath + " does not exist");
                }
                File.Delete(filePath);
            });
        }

        public async Task<Stream> DownloadFileAsync(string filePath)
        {

            filePath = Path.Combine(_dataPath, filePath);

            if (!File.Exists(filePath))
            {
                throw new Exception("File not found to download");
            }

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);

            return stream;

        }

        public async Task<Stream> DownloadFolderAsZipAsync(string directoryPath)
        {
            if (Path.GetExtension(directoryPath) != string.Empty)
            {
                throw new Exception("DirectoryPath is not directory");
            }

            directoryPath = Path.Combine(_dataPath, directoryPath);
            if (!Directory.Exists(directoryPath))
            {
                throw new Exception("Directory not found to download");
            }

            var zipPath = directoryPath + ".zip";

            ZipFile.CreateFromDirectory(directoryPath, zipPath);

            var stream = new FileStream(zipPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);

            return stream;
        }

        public async Task<List<string>> GetAllFilesAndDirectoriesAsync(string directoryPath)
        {
            return await Task.Run(() =>
            {
                directoryPath = Path.Combine(_dataPath, directoryPath);

                var parentPath = Directory.GetParent(directoryPath);

                if (!Directory.Exists(parentPath.FullName))
                {
                    throw new Exception("Parent folder path not found");
                }

                var allFilesAndFolders = Directory.GetFileSystemEntries(directoryPath).ToList();

                allFilesAndFolders = allFilesAndFolders.Select(p => p.Remove(0, directoryPath.Length + 1)).ToList();

                return allFilesAndFolders;
            });
        }

        public async Task UploadFileAsync(string filePath, Stream stream)
        {
            filePath = Path.Combine(_dataPath, filePath);
            var parentPath = Directory.GetParent(filePath);

            if (!Directory.Exists(parentPath.FullName))
            {
                throw new Exception("Parent folder path not found");
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    await stream.CopyToAsync(fileStream);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while uploading file", ex);
                }
            }
        }

        private void ValidateDirectoryPath(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                throw new Exception(directoryPath + " already exists");
            }
            var parentPath = Directory.GetParent(directoryPath);
            if (!Directory.Exists(parentPath.FullName))
            {
                throw new Exception(parentPath.FullName + " does not exist");
            }

        }
    }
}
