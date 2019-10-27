using System;
using System.IO;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    class FileService : IFileService
    {
        public async Task SaveFile(string path, Stream data)
        {
            await using var ms = new MemoryStream();
            data.CopyTo(ms);
            await File.WriteAllBytesAsync(path, ms.ToArray());
        }

        public async Task<Stream> OpenImage(string path)
        {
            var dataBytes = await File.ReadAllBytesAsync(path);
            return new MemoryStream(dataBytes);
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
