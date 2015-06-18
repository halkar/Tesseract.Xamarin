using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Com.Googlecode.Tesseract.Android;
using File = Java.IO.File;
using Object = Java.Lang.Object;
using System.Drawing;
using System.Collections.Generic;

namespace Tesseract.Droid
{
    public class TesseractApi : ITesseractApi
    {
        private readonly TessBaseAPI _api;

        private readonly Context _context;

        public event EventHandler<ProgressEventArgs> Progress;

        private readonly ProgressHandler _progressHandler = new ProgressHandler();

		public bool Initialized { get; private set; }

        public TesseractApi(Context context)
        {
            _context = context;
            _progressHandler.Progress += (sender, e) => { OnProgress(e.Progress); };
            _api = new TessBaseAPI(_progressHandler);
        }

		public async Task<bool> Init(string tessDataPath, string language)
        {
			var result = _api.Init (tessDataPath, language);
			Initialized = result;
            return result;
        }

        public async Task<bool> Init(string language)
        {
            var path = await CopyAssets();
			var result = _api.Init (path, language);
			Initialized = result;
			return result;
        }

        private static BitmapFactory.Options GetOptions()
        {
            return new BitmapFactory.Options {InSampleSize = 4};
        }

        public async Task SetImage(byte[] data)
        {
            var bitmap = await BitmapFactory.DecodeByteArrayAsync(data, 0, data.Length, GetOptions());
			await Task.Run (() => _api.SetImage (bitmap));
        }

        public async Task SetImage(string path)
        {
            Bitmap bitmap = await BitmapFactory.DecodeFileAsync(path, GetOptions());
			await Task.Run (() => _api.SetImage (bitmap));
        }

        public async Task SetImage(Stream stream)
        {
            Bitmap bitmap = await BitmapFactory.DecodeStreamAsync(stream);
			await Task.Run (() => _api.SetImage (bitmap));
        }

        public string Text
        {
            get { return _api.UTF8Text; }
        }

        public int ProgressValue { get; private set; }

        public void Dispose()
        {
            _api.Dispose();
        }

		public List<Result> Results()
		{
			int[] boundingBox;
			var results = new List<Result> ();
			var iterator = _api.ResultIterator;
			iterator.Begin ();
			do {
				boundingBox = iterator.GetBoundingBox (PageIteratorLevel.RIL_WORD);
				var result = new Result {
					Confidence = iterator.Confidence (PageIteratorLevel.RIL_WORD),
					Text = iterator.GetUTF8Text (PageIteratorLevel.RIL_WORD),
					Box = new Rectangle(boundingBox[0], boundingBox[1], boundingBox[2], boundingBox[3])
				};
				results.Add (result);
			} while (iterator.Next (PageIteratorLevel.RIL_WORD));
			return results;
		}

        public void OnProgressValues(TessBaseAPI.ProgressValues progress)
        {
            OnProgress(progress.Percent);
        }

        private async Task<string> CopyAssets()
        {
            try
            {
                AssetManager assetManager = _context.Assets;
                string[] files = assetManager.List("tessdata");
                var file = _context.GetExternalFilesDir(null);
                var tessdata = new File(_context.GetExternalFilesDir(null), "tessdata");
                if (!tessdata.Exists())
                {
                    tessdata.Mkdir();
                }

                foreach (string filename in files)
                {
                    using (var inStream = assetManager.Open("tessdata/" + filename))
                    {
                        File outFile = new File(tessdata, filename);
                        if (outFile.Exists())
                        {
                            outFile.Delete();
                        }
                        using (var outStream = new FileStream(outFile.AbsolutePath, FileMode.Create))
                        {
                            await inStream.CopyToAsync(outStream);
                            await outStream.FlushAsync();
                        }
                    }
                }
                return file.AbsolutePath;
            }
            catch (Exception ex)
            {
                Log.Error("[TesseractApi]", ex.Message);
            }
            return null;
        }

        protected virtual void OnProgress(int progress)
        {
            ProgressValue = progress;
            var handler = Progress;
            if (handler != null) handler(this, new ProgressEventArgs(progress));
        }

        private class ProgressHandler : Object, TessBaseAPI.IProgressNotifier
        {
            internal event EventHandler<ProgressEventArgs> Progress;

            public void OnProgressValues(TessBaseAPI.ProgressValues progress)
            {
                OnProgress(progress.Percent);
            }

            private void OnProgress(int progress)
            {
                var handler = Progress;
                if (handler != null) handler(this, new ProgressEventArgs(progress));
            }
        }
    }

	public static class PageIteratorLevel {
		/** Block of text/image/separator line. */
		public const int RIL_BLOCK = 0;

		/** Paragraph within a block. */
		public const int RIL_PARA = 1;

		/** Line within a paragraph. */
		public const int RIL_TEXTLINE = 2;

		/** Word within a text line. */
		public const int RIL_WORD = 3;

		/** Symbol/character within a word. */
		public const int RIL_SYMBOL = 4;
	};
}

