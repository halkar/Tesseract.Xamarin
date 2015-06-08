using System;
using System.IO;
using Xamarin.Forms;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace Tesseract.Forms.Test
{
    public partial class MainPage : ContentPage
    {
        private readonly IMediaPicker _mediaPicker;
        private readonly ITesseract _tesseract;
        public MainPage()
        {
            InitializeComponent();
            _mediaPicker = DependencyService.Get<IMediaPicker>();
            _tesseract = DependencyService.Get<ITesseract>();
			_tesseract.Init ("", "eng");
        }

        private async void LoadImageButton_OnClicked(object sender, EventArgs e)
        {
            var result = await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions());
            if(result.Source == null) 
                return;
            using (var ms = new MemoryStream())
            {
                await result.Source.CopyToAsync(ms);
                _tesseract.SetImage(ms.ToArray());
            }
            
        }
    }
}
