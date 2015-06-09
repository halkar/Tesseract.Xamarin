using UIKit;
using Foundation;
using System.Threading.Tasks;
using System.IO;

namespace Tesseract.iOS
{
	public class TesseractApi : ITesseractApi
	{
		private Tesseract.Binding.iOS.Tesseract _api;

		public async Task<bool> Init(string tessDataPath, string language)
	    {
			_api = new Tesseract.Binding.iOS.Tesseract(tessDataPath, language);
			_api.Init ();
			return true;
	    }

		public async Task<bool> Init(string language)
		{
			_api = new Tesseract.Binding.iOS.Tesseract(language);
			_api.Init ();
			return true;
		}

		public void SetImage(byte[] data)
		{
			_api.Image = new UIImage (NSData.FromArray(data));
		}

		public void SetImage(Stream stream)
		{
			_api.Image = new UIImage (NSData.FromStream(stream));
		}

		public void SetImage(string path)
	    {
			_api.Image = new UIImage (path);
	    }

		public string Text
		{
			get { return _api.RecognizedText; }
		}

	    public void Dispose()
	    {
			_api.Dispose ();
	    }
	}
}

