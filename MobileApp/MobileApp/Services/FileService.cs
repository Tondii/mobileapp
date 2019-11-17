using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;

namespace MobileApp.Services
{
    class FileService : IFileService
    {
        private readonly IPermissionService _permissionService;

        public FileService(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

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
            return $"IMG_{dateNow.Year}{dateNow.Month}{dateNow.Day}_{dateNow.Hour}{dateNow.Minute}{dateNow.Second}{dateNow.Millisecond}.jpg";
        }

        public string GenerateImagePath()
        {
            return CombineFilePath(GenerateImageFilename());
        }

        public async Task<MediaFile> PickImageFromGallery()
        {
            await _permissionService.CheckAndRequestPermission(Permission.Storage);

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {

            }

            return await CrossMedia.Current.PickPhotoAsync();
        }
    }
}
