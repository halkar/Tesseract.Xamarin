using UIKit;
using Foundation;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;
using ObjCRuntime;
using System.Linq;

namespace Tesseract.iOS
{
    public class TesseractApi : ITesseractApi
    {
        private Tesseract.Binding.iOS.Tesseract _api;

        public event EventHandler<ProgressEventArgs> Progress;

        public Task<bool> Init(string tessDataPath, string language)
        {
            _api = new Tesseract.Binding.iOS.Tesseract(tessDataPath, language);
            _api.Init();
            return Task.FromResult(true);
        }

        public Task<bool> Init(string language)
        {
            _api = new Tesseract.Binding.iOS.Tesseract(language);
            _api.Init();
            return Task.FromResult(true);
        }

        public async Task SetImage(byte[] data)
        {
            _api.Image = new UIImage(NSData.FromArray(data));
            _api.Recognize();
        }

        public async Task SetImage(Stream stream)
        {
            _api.Image = new UIImage(NSData.FromStream(stream));
            _api.Recognize();
        }

        public async Task SetImage(string path)
        {
            _api.Image = new UIImage(path);
            _api.Recognize();
        }

        public string Text
        {
            get { return _api.RecognizedText; }
        }

        public int ProgressValue
        {
            get { return _api.Progress; }
        }

        public void Dispose()
        {
            _api.Dispose();
        }

		public List<Result> Results()
		{
			return this._api.GetConfidenceByWord
				.Select (r => ConvertToResult (r))
				.ToList ();
		}

		Result ConvertToResult (NSDictionary r)
		{
			var rect = (NSValue)r ["boundingbox"];
			return new Result {
				Confidence = ((NSNumber)r ["confidence"]).FloatValue,
				Text = ((NSMutableString)r ["text"]).ToString (),
				Box = new int[] { 
					(int)rect.RectangleFValue.X, 
					(int)rect.RectangleFValue.Y,
					(int)rect.RectangleFValue.Width, 
					(int)rect.RectangleFValue.Height
				}
			};
		}
    }
}

