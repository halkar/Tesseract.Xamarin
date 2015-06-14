using System;
using System.IO;
using Xamarin.Forms;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;
using XLabs.Ioc;
using System.Threading.Tasks;

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
			var init = await _tesseract.Init ("eng");
			await _tesseract.SetImage(result.Source);
			TextLabel.Text = _tesseract.Text;
        }
	}
}
