using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using SkiaSharp;

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

        public async Task<byte[]> OpenFile(string path)
        {
            return await File.ReadAllBytesAsync(path);
        }

        public async Task DeleteFile(string path)
        {
            await Task.Run(() => { File.Delete(path); });
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
                throw new NotSupportedException("Picking photo is not supported.");
            }

            return await CrossMedia.Current.PickPhotoAsync();
        }

        public async Task SaveImage(string path, byte[] imageBytes, int compression = 100)
        {
            using var ms = new SKMemoryStream(imageBytes);
            using var original = SKBitmap.Decode(ms);
            var resizedHeight = original.Height > 2500 ? original.Height / 2 : original.Height;
            var resizedWidth = original.Width > 2500 ? original.Width / 2 : original.Width;
            var imageInfo = new SKImageInfo(resizedWidth, resizedHeight);
            var resizedBitmap = new SKBitmap(imageInfo);
            original.ScalePixels(resizedBitmap, SKFilterQuality.High);
            using var image = SKImage.FromBitmap(resizedBitmap);
            await using var output = File.OpenWrite(path);
            image.Encode(SKEncodedImageFormat.Jpeg, compression).SaveTo(output);
        }
    }
}
