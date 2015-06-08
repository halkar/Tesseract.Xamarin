using UIKit;
using Foundation;

namespace Tesseract.iOS
{
	public class TesseractApi : ITesseract
	{
		private Tesseract.Binding.iOS.Tesseract _api;

	    public void Init(string tessDataPath, string language)
	    {
			_api = new Tesseract.Binding.iOS.Tesseract(tessDataPath, language);
			_api.Init ();
	    }

		public void SetImage(byte[] data)
	    {
			_api.Image = new UIImage (NSData.FromArray (data));
	    }

	    public void Dispose()
	    {
			_api.Dispose ();
	    }
	}
}

