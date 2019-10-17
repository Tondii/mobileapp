using System;
using System.Collections.Generic;
using System.Text;
using MobileApp.Navigation;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class CameraResultViewModel : BaseViewModel, IParameterized<ImageSource>
    {
        private ImageSource _image;

        public ImageSource Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged();
            }
        }

        public void HandleParameter(ImageSource parameter)
        {
            Image = parameter;
        }
    }
}
