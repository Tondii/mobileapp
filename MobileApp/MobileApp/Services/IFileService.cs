using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace MobileApp.Services
{
    public interface IFileService
    {
        Task SaveFile(string path, byte[] fileBytes);
        Task<byte[]> OpenFile(string path);
        Task DeleteFile(string path);
        string CombineFilePath(string filename);
        string GenerateImageFilename();
        string GenerateImagePath();
        Task<MediaFile> PickImageFromGallery();
        Task SaveImage(string path, byte[] imageBytes, int compression);
    }
}
