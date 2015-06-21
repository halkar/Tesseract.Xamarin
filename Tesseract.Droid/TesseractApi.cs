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
        private TessBaseAPI _api;

		private volatile bool _busy;

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

        public async Task<bool> Init(string language, Tesseract.OCREngineMode? mode = null)
        {
            var path = await CopyAssets();
            var result = mode.HasValue
                ? _api.Init(path, language, GetOcrEngineMode(mode.Value))
                : _api.Init(path, language);
			Initialized = result;
			return result;
        }

        private static BitmapFactory.Options GetOptions()
        {
            return new BitmapFactory.Options {InSampleSize = 4};
        }

		public async Task<bool> SetImage(byte[] data)
        {
			using (var bitmap = await BitmapFactory.DecodeByteArrayAsync (data, 0, data.Length, GetOptions ())) {
				return await Recognise (bitmap);
			}
        }

		public async Task<bool> SetImage(string path)
		{
			using (Bitmap bitmap = await BitmapFactory.DecodeFileAsync (path, GetOptions ())) {
				return await Recognise (bitmap);
			}
		}

		public async Task<bool> SetImage(Stream stream)
        {
			using (Bitmap bitmap = await BitmapFactory.DecodeStreamAsync (stream)) {
				return await Recognise (bitmap);
			}
        }

		private async Task<bool> Recognise (Bitmap bitmap)
		{
			if (_busy)
				return false;
			_busy = true;
			try {
				await Task.Run (() => _api.SetImage (bitmap));
				return true;
			} finally {
				_busy = false;
			}
		}

		int GetOcrEngineMode (OCREngineMode mode)
		{
			switch (mode) {
				case OCREngineMode.CubeOnly:
					return OcrMode.OEM_CUBE_ONLY;
				case OCREngineMode.TesseractCubeCombined:
					return OcrMode.OEM_TESSERACT_CUBE_COMBINED;
				case OCREngineMode.TesseractOnly:
					return OcrMode.OEM_TESSERACT_ONLY;
				default:
					return OcrMode.OEM_CUBE_ONLY;
			}
		}

		public void SetPageSegmentationMode(Tesseract.PageSegmentationMode mode)
		{
			switch (mode)
			{
			case PageSegmentationMode.Auto:
				_api.SetPageSegMode (PageSegMode.PSM_AUTO);
				break;
			case PageSegmentationMode.AutoOnly:
				_api.SetPageSegMode (PageSegMode.PSM_AUTO_ONLY);
				break;
			case PageSegmentationMode.AutoOSD:
				_api.SetPageSegMode (PageSegMode.PSM_AUTO_OSD);
				break;
			case PageSegmentationMode.CircleWord:
				_api.SetPageSegMode (PageSegMode.PSM_CIRCLE_WORD);
				break;
			case PageSegmentationMode.OSDOnly:
				_api.SetPageSegMode (PageSegMode.PSM_OSD_ONLY);
				break;
			case PageSegmentationMode.SingleBlock:
				_api.SetPageSegMode (PageSegMode.PSM_SINGLE_BLOCK);
				break;
			case PageSegmentationMode.SingleBlockVertText:
				_api.SetPageSegMode (PageSegMode.PSM_SINGLE_BLOCK_VERT_TEXT);
				break;
			case PageSegmentationMode.SingleChar:
				_api.SetPageSegMode (PageSegMode.PSM_SINGLE_CHAR);
				break;
			case PageSegmentationMode.SingleColumn:
				_api.SetPageSegMode (PageSegMode.PSM_SINGLE_COLUMN);
				break;
			case PageSegmentationMode.SingleLine:
				_api.SetPageSegMode (PageSegMode.PSM_AUTO);
				break;
			case PageSegmentationMode.SingleWord:
				_api.SetPageSegMode (PageSegMode.PSM_SINGLE_WORD);
				break;
			case PageSegmentationMode.SparseText:
				_api.SetPageSegMode (PageSegMode.PSM_SPARSE_TEXT);
				break;
			case PageSegmentationMode.SparseTextOSD:
				_api.SetPageSegMode (PageSegMode.PSM_SPARSE_TEXT_OSD);
				break;
			}
		}

        public string Text
        {
            get { return _api.UTF8Text; }
        }

        public int ProgressValue { get; private set; }

		public void Clear()
		{
			_api.Clear ();
		}

        public void Dispose()
        {
			if (_api != null) {
				_api.Dispose ();
				_api = null;
			}
        }

		public List<Result> Results(Tesseract.PageIteratorLevel level)
		{
			int pageIteratorLevel = GetPageIteratorLevel(level);
			int[] boundingBox;
			var results = new List<Result> ();
			var iterator = _api.ResultIterator;
			iterator.Begin ();
			do {
				boundingBox = iterator.GetBoundingBox (pageIteratorLevel);
				var result = new Result {
					Confidence = iterator.Confidence (pageIteratorLevel),
					Text = iterator.GetUTF8Text (pageIteratorLevel),
					Box = new Rectangle(boundingBox[0], boundingBox[1], boundingBox[2], boundingBox[3])
				};
				results.Add (result);
			} while (iterator.Next (pageIteratorLevel));
			return results;
		}

		int GetPageIteratorLevel (Tesseract.PageIteratorLevel level)
		{
			switch (level) {
				case Tesseract.PageIteratorLevel.Block:
					return PageIteratorLevel.RIL_BLOCK;
				case Tesseract.PageIteratorLevel.Paragraph:
					return PageIteratorLevel.RIL_PARA;
				case Tesseract.PageIteratorLevel.Symbol:
					return PageIteratorLevel.RIL_SYMBOL;
				case Tesseract.PageIteratorLevel.Textline:
					return PageIteratorLevel.RIL_TEXTLINE;
				case Tesseract.PageIteratorLevel.Word:
					return PageIteratorLevel.RIL_WORD;
				default:
					return PageIteratorLevel.RIL_WORD;
			}
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

	public static class PageSegMode {
		/** Orientation and script detection only. */
		public static int PSM_OSD_ONLY = 0;

		/** Automatic page segmentation with orientation and script detection. (OSD) */
		public static int PSM_AUTO_OSD = 1;

		/** Fully automatic page segmentation, but no OSD, or OCR. */
		public static int PSM_AUTO_ONLY = 2;

		/** Fully automatic page segmentation, but no OSD. */
		public static int PSM_AUTO = 3;

		/** Assume a single column of text of variable sizes. */
		public static int PSM_SINGLE_COLUMN = 4;

		/** Assume a single uniform block of vertically aligned text. */
		public static int PSM_SINGLE_BLOCK_VERT_TEXT = 5;

		/** Assume a single uniform block of text. (Default.) */
		public static int PSM_SINGLE_BLOCK = 6;

		/** Treat the image as a single text line. */
		public static int PSM_SINGLE_LINE = 7;

		/** Treat the image as a single word. */
		public static int PSM_SINGLE_WORD = 8;

		/** Treat the image as a single word in a circle. */
		public static int PSM_CIRCLE_WORD = 9;

		/** Treat the image as a single character. */
		public static int PSM_SINGLE_CHAR = 10;

		/** Find as much text as possible in no particular order. */
		public static int PSM_SPARSE_TEXT = 11;

		/** Sparse text with orientation and script detection. */
		public static int PSM_SPARSE_TEXT_OSD = 12;

		/** Number of enum entries. */
		public static int PSM_COUNT = 13;
	}

	public static class OcrMode {
		/** Run Tesseract only - fastest */
		public static int OEM_TESSERACT_ONLY = 0;

		/** Run Cube only - better accuracy, but slower */
		public static int OEM_CUBE_ONLY = 1;

		/** Run both and combine results - best accuracy */
		public static int OEM_TESSERACT_CUBE_COMBINED = 2;

		/** Default OCR engine mode. */
		public static int OEM_DEFAULT = 3;
	}
}

