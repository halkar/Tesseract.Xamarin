using System;
using System.IO;
using Xamarin.Forms;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;
using XLabs.Ioc;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tesseract.Forms.Test
{
    public partial class MainPage : ContentPage
    {
        private readonly IMediaPicker _mediaPicker;
        private readonly ITesseractApi _tesseract;
        public MainPage()
        {
            InitializeComponent();
			_mediaPicker = Resolver.Resolve<IMediaPicker>();
			_tesseract = Resolver.Resolve<ITesseractApi>();
        }

        private async void LoadImageButton_OnClicked(object sender, EventArgs e)
        {
			var result = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions());
			if(result.Source == null) 
                return;
			activityIndicator.IsRunning = true;
			var initialised = await _tesseract.Init ("eng");
			if (!initialised)
				return;
			await _tesseract.SetImage(result.Source);
			activityIndicator.IsRunning = false;
			TextLabel.Text = _tesseract.Text;
        }

		private async void GetPhotoButton_OnClicked(object sender, EventArgs e)
		{
			try {
			var result = await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions());
			if(result.Source == null) 
				return;
			activityIndicator.IsRunning = true;
			var initialised = await _tesseract.Init ("eng");
			if (!initialised)
				return;
			await _tesseract.SetImage(result.Source);
			activityIndicator.IsRunning = false;
			TextLabel.Text = _tesseract.Text;
			}
			catch(Exception ex) {
				Debug.WriteLine (ex.Message);
			}
		}
	}
}
