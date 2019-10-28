﻿using System;
using System.IO;
using System.Windows.Input;
using MobileApp.Database.DTO;
using MobileApp.Navigation;
using MobileApp.Services;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class CameraResultViewModel : BaseViewModel, IParameterized<byte[]>
    {
        private byte[] _imageBytes;
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly IFileService _fileService;
        public ImageSource Image => ImageSource.FromStream(() => new MemoryStream(_imageBytes));

        public CameraResultViewModel(INavigationService navigationService, IDataService dataService, IFileService fileService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _fileService = fileService;
        }

        public byte[] ImageBytes
        {
            get => _imageBytes;
            set
            {
                _imageBytes = value;
                RaisePropertyChanged();
            }
        }

        public void HandleParameter(byte[] parameter)
        {
            ImageBytes = parameter;
        }

        public ICommand AddNewReceipt =>
            new Command(async () =>
            {
                var receipt = new Receipt()
                {
                    BruttoSummary = 0,
                    Company = new Company(),
                    CreateDateTime = DateTime.Now,
                    PicturePath = _fileService.GenerateImagePath()
                };
                await _fileService.SaveFile(receipt.PicturePath, _imageBytes);
                await _dataService.AddReceiptAsync(receipt);
                await _navigationService.NavigateTo(new MainPage());
            });
    }
}
