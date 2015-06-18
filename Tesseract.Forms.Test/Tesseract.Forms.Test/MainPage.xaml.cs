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
			await Recognise (result);
        }

		private async void GetPhotoButton_OnClicked(object sender, EventArgs e)
		{
			var result = await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions());
			await Recognise (result);
		}

		async Task Recognise (MediaFile result)
		{
			if (result.Source == null)
				return;
			activityIndicator.IsRunning = true;
			if (!_tesseract.Initialized) {
				var initialised = await _tesseract.Init ("eng");
				if (!initialised)
					return;
			}
			await _tesseract.SetImage (result.Source);
			activityIndicator.IsRunning = false;
			TextLabel.Text = _tesseract.Text;
			var words = _tesseract.Results (PageIteratorLevel.Word);
			var symbols = _tesseract.Results (PageIteratorLevel.Symbol);
			var blocks = _tesseract.Results (PageIteratorLevel.Block);
			var paragraphs = _tesseract.Results (PageIteratorLevel.Paragraph);
			var lines = _tesseract.Results (PageIteratorLevel.Textline);
		}
	}
}
