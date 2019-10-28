using System.IO;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public interface IFileService
    {
        Task SaveFile(string path, byte[] fileBytes);
        Task<byte[]> OpenImage(string path);
        string CombineFilePath(string filename);
        string GenerateImageFilename();
        string GenerateImagePath();
    }
}
