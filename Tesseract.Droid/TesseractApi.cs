using Android.Content;
using Android.Graphics;
using Com.Googlecode.Tesseract.Android;
using Java.IO;
using Android.Content.Res;
using Android.Util;
using System.IO;
using System.Threading.Tasks;
using System;

namespace Tesseract.Droid
{
	public class TesseractApi : ITesseractApi
	{
		private readonly TessBaseAPI _api = new TessBaseAPI();

		private readonly Context _context;

		public TesseractApi(Context context) {
			_context = context;
		}

		public async Task<bool> Init(string tessDataPath, string language)
	    {
			return _api.Init(tessDataPath, language);
	    }

		public async Task<bool> Init(string language)
		{
			var path = await CopyAssets ();
			return _api.Init(path, language);
		}

		public void SetImage(byte[] data)
		{
			BitmapFactory.Options options = new BitmapFactory.Options();
			options.InSampleSize = 4;

			Bitmap bitmap = BitmapFactory.DecodeByteArray(data, 0, data.Length, options);
			_api.SetImage(bitmap);
		}

		public void SetImage(Stream stream)
		{
			BitmapFactory.Options options = new BitmapFactory.Options();
			options.InSampleSize = 4;

			Bitmap bitmap = BitmapFactory.DecodeStream (stream);
			_api.SetImage(bitmap);
		}

		public void SetImage(string path)
	    {
			BitmapFactory.Options options = new BitmapFactory.Options();
			options.InSampleSize = 4;

			Bitmap bitmap = BitmapFactory.DecodeFile(path, options);
			_api.SetImage(bitmap);
	    }

		public string Text
		{
			get { return _api.UTF8Text; }
		}

	    public void Dispose()
	    {
			_api.Dispose ();
	    }

		private async Task<string> CopyAssets() {
			try {
				AssetManager assetManager = _context.Assets;
				string[] files = assetManager.List ("tessdata");
				var file = _context.GetExternalFilesDir (null);
				var tessdata = new Java.IO.File (_context.GetExternalFilesDir (null), "tessdata");
				if (!tessdata.Exists ()) {
					tessdata.Mkdir ();
				}

				foreach (string filename in files) {
					using (var inStream = assetManager.Open ("tessdata/" + filename)) {
						Java.IO.File outFile = new Java.IO.File (tessdata, filename);
						if (outFile.Exists ()) {
							outFile.Delete ();
						}
						using (var outStream = new FileStream (outFile.AbsolutePath, FileMode.Create)) {
							await inStream.CopyToAsync (outStream);
							await outStream.FlushAsync();
						}
					}
				}
				return file.AbsolutePath;
			} catch (Exception ex) {
				Log.Error ("[TesseractApi]", ex.Message);
			}
			return null;
		}
	}
}

