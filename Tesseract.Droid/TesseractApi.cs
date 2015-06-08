using Android.Content;
using Android.Graphics;
using Com.Googlecode.Tesseract.Android;

namespace Tesseract.Droid
{
	public class TesseractApi : ITesseract
	{
		private readonly TessBaseAPI _api = new TessBaseAPI();

	    public void Init(string tessDataPath, string language)
	    {
			_api.Init(tessDataPath, language);
	    }

	    public void SetImage(byte[] data)
	    {
			_api.SetImage(BitmapFactory.DecodeByteArray(data, 0, data.Length));
	    }

	    public void Dispose()
	    {
			_api.Dispose ();
	    }
	}
}

