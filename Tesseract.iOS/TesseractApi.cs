using UIKit;
using Foundation;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;
using ObjCRuntime;
using System.Linq;
using Tesseract.Binding.iOS;

namespace Tesseract.iOS
{
    public class TesseractApi : ITesseractApi
    {
		private Tesseract.Binding.iOS.G8Tesseract _api;

		private volatile bool _busy;

        public event EventHandler<ProgressEventArgs> Progress;

		public bool Initialized { get; private set; }

        public async Task<bool> Init(string language, OCREngineMode? mode = null)
		{
			try {
				_api = new Tesseract.Binding.iOS.G8Tesseract (language);
				_api.Init ();
				if (mode.HasValue)
					SetOcrEngineMode (mode.Value);
				Initialized = true;
			} catch {
				Initialized = false;

			}
			return Initialized;
		}

		public async Task<bool> SetImage(byte[] data)
        {
			using (var uIImage = new UIImage (NSData.FromArray (data))) {
				return Recognise (uIImage);
			}
        }

		public async Task<bool> SetImage(Stream stream)
        {
			using (var uIImage = new UIImage (NSData.FromStream (stream))) {
				return Recognise (uIImage);
			}
        }

		public async Task<bool> SetImage(string path)
        {
			using (var uIImage = new UIImage (path)) {
				return Recognise (uIImage);
			}
        }

		private bool Recognise (UIImage image)
		{
			if (_busy)
				return false;
			_busy = true;
			try {
				_api.Image = image;
				_api.Recognize ();
				return true;
			} finally {
				_busy = false;
			}
		}

        public string Text
        {
            get { return _api.RecognizedText; }
        }

        public int ProgressValue
        {
			get { return (int)_api.Progress; }
        }

        public void Dispose()
        {
			if (_api != null) {
				_api.Dispose ();
				_api = null;
			}
        }

		public void SetOcrEngineMode(Tesseract.OCREngineMode mode)
		{
			switch (mode)
			{
				case OCREngineMode.CubeOnly:
					_api.EngineMode = G8OCREngineMode.CubeOnly;
					break;
				case OCREngineMode.TesseractCubeCombined:
					_api.EngineMode = G8OCREngineMode.TesseractCubeCombined;
					break;
				case OCREngineMode.TesseractOnly:
					_api.EngineMode = G8OCREngineMode.TesseractOnly;
					break;
			}
		}

		public void SetPageSegmentationMode(Tesseract.PageSegmentationMode mode)
		{
			switch (mode)
			{
				case PageSegmentationMode.Auto:
					_api.PageSegmentationMode = G8PageSegmentationMode.Auto;
					break;
				case PageSegmentationMode.AutoOnly:
					_api.PageSegmentationMode = G8PageSegmentationMode.AutoOnly;
					break;
				case PageSegmentationMode.AutoOSD:
					_api.PageSegmentationMode = G8PageSegmentationMode.AutoOSD;
					break;
				case PageSegmentationMode.CircleWord:
					_api.PageSegmentationMode = G8PageSegmentationMode.CircleWord;
					break;
				case PageSegmentationMode.OSDOnly:
					_api.PageSegmentationMode = G8PageSegmentationMode.OSDOnly;
					break;
				case PageSegmentationMode.SingleBlock:
					_api.PageSegmentationMode = G8PageSegmentationMode.SingleBlock;
					break;
				case PageSegmentationMode.SingleBlockVertText:
					_api.PageSegmentationMode = G8PageSegmentationMode.SingleBlockVertText;
					break;
				case PageSegmentationMode.SingleChar:
					_api.PageSegmentationMode = G8PageSegmentationMode.SingleChar;
					break;
				case PageSegmentationMode.SingleColumn:
					_api.PageSegmentationMode = G8PageSegmentationMode.SingleColumn;
					break;
				case PageSegmentationMode.SingleLine:
					_api.PageSegmentationMode = G8PageSegmentationMode.SingleLine;
					break;
				case PageSegmentationMode.SingleWord:
					_api.PageSegmentationMode = G8PageSegmentationMode.SingleWord;
					break;
				case PageSegmentationMode.SparseText:
					_api.PageSegmentationMode = G8PageSegmentationMode.SparseText;
					break;
				case PageSegmentationMode.SparseTextOSD:
					_api.PageSegmentationMode = G8PageSegmentationMode.SparseTextOSD;
					break;
			}
		}

		public List<Result> Results(Tesseract.PageIteratorLevel level)
		{
			var pageIterationLevel = GetPageIteratorLevel(level);
			return this._api.RecognizedBlocksByIteratorLevel (pageIterationLevel)
				.Select (r => ConvertToResult (r))
				.ToList ();
		}

		G8PageIteratorLevel GetPageIteratorLevel (Tesseract.PageIteratorLevel level)
		{
			switch (level) {
				case Tesseract.PageIteratorLevel.Block:
					return G8PageIteratorLevel.Block;
				case Tesseract.PageIteratorLevel.Paragraph:
					return G8PageIteratorLevel.Paragraph;
				case Tesseract.PageIteratorLevel.Symbol:
					return G8PageIteratorLevel.Symbol;
				case Tesseract.PageIteratorLevel.Textline:
					return G8PageIteratorLevel.Textline;
				case Tesseract.PageIteratorLevel.Word:
					return G8PageIteratorLevel.Word;
				default:
					return G8PageIteratorLevel.Word;
			}
		}

		Result ConvertToResult (G8RecognizedBlock r)
		{
			return new Result {
				Confidence = (float)r.Confidence,
				Text = r.Text,
				Box = new Rectangle ( 
					(float)r.BoundingBox.X, 
					(float)r.BoundingBox.Y,
					(float)r.BoundingBox.Width, 
					(float)r.BoundingBox.Height
				)
			};
		}
    }
}

