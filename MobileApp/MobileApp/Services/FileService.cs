using System;
using System.IO;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    class FileService : IFileService
    {
        public async Task SaveFile(string path, byte[] fileBytes)
        {
            await File.WriteAllBytesAsync(path, fileBytes);
        }

        public async Task<byte[]> OpenImage(string path)
        {
            return await File.ReadAllBytesAsync(path);
        }

        public string CombineFilePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), filename);
        }

        public string GenerateImageFilename()
        {
            var dateNow = DateTime.Now;
            return $"IMG_{dateNow.Year}{dateNow.Month}{dateNow.Day}_{dateNow.Hour}{dateNow.Minute}{dateNow.Second}.jpg";
        }

        public string GenerateImagePath()
        {
            return CombineFilePath(GenerateImageFilename());
        }
    }
}
