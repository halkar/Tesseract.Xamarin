using UIKit;
using Foundation;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;
using ObjCRuntime;
using System.Linq;
using Tesseract.Binding.iOS;
using CoreImage;
using CoreGraphics;

namespace Tesseract.iOS
{
    public class TesseractApi : ITesseractApi
    {
        private Tesseract.Binding.iOS.G8Tesseract _api;

        private volatile bool _busy;

        private CGSize _size;

        public event EventHandler<ProgressEventArgs> Progress;

        public bool Initialized { get; private set; }

        public async Task<bool> Init (string language, OcrEngineMode? mode = null)
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

        public void SetVariable (string key, string value)
        {
            CheckIfInitialized ();
            _api.SetVariableValue (value, key);
        }

        public async Task<bool> SetImage (byte[] data)
        {
            CheckIfInitialized ();
            if (data == null)
                throw new ArgumentNullException ("data");
            using (var uiImage = new UIImage (NSData.FromArray (data))) {
                return await Recognise (uiImage);
            }
        }

        public async Task<bool> SetImage (Stream stream)
        {
            CheckIfInitialized ();
            if (stream == null)
                throw new ArgumentNullException ("stream");
            using (var uiImage = new UIImage (NSData.FromStream (stream))) {
                return await Recognise (uiImage);
            }
        }

        public async Task<bool> SetImage (string path)
        {
            CheckIfInitialized ();
            if (path == null)
                throw new ArgumentNullException ("path");
            using (var uiImage = new UIImage (path)) {
                return await Recognise (uiImage);
            }
        }

        public async Task<bool> Recognise (CIImage image)
        {
            CheckIfInitialized ();
            if (image == null)
                throw new ArgumentNullException ("image");
            if (_busy)
                return false;
            _busy = true;
            try {
                return await Task.Run (() => {
                    using (var blur = new CIGaussianBlur ())
                    using (var context = CIContext.Create ()) {
                        blur.SetDefaults ();
                        blur.Image = image;
                        blur.Radius = 0;
                        using (var outputCiImage = context.CreateCGImage (blur.OutputImage, image.Extent))
                        using (var newImage = new UIImage (outputCiImage)) {
                            _size = newImage.Size;
                            _api.Image = newImage;
                            _api.Recognize ();
                            return true;
                        }
                    }

                });
            } finally {
                _busy = false;
            }
        }

        public async Task<bool> Recognise (CGImage image)
        {
            CheckIfInitialized ();
            using (var ciImage = new CIImage (image)) {
                return await Recognise (ciImage);
            }
        }

        public async Task<bool> Recognise (UIImage image)
        {   
            CheckIfInitialized ();
            return await Recognise (image.CGImage);
        }

        public string Text {
            get {
                CheckIfInitialized ();
                return _api.RecognizedText;
            }
        }

        public int ProgressValue {
            get {
                CheckIfInitialized ();
                return (int)_api.Progress;
            }
        }

        public void SetWhitelist (string whitelist)
        {
            CheckIfInitialized ();
            _api.CharWhitelist = whitelist;
        }

        public void SetBlacklist (string blacklist)
        {
            CheckIfInitialized ();
            _api.CharBlacklist = blacklist;
        }

        public void SetRectangle (Tesseract.Rectangle rect)
        {
            CheckIfInitialized ();
            _api.Rect = new CGRect (rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public void Dispose ()
        {
            if (_api != null) {
                _api.Dispose ();
                _api = null;
            }
        }

        public void Clear ()
        {
            Tesseract.Binding.iOS.G8Tesseract.ClearCache ();
        }

        public void SetOcrEngineMode (Tesseract.OcrEngineMode mode)
        {
            switch (mode) {
            case OcrEngineMode.CubeOnly:
                _api.EngineMode = G8OCREngineMode.CubeOnly;
                break;
            case OcrEngineMode.TesseractCubeCombined:
                _api.EngineMode = G8OCREngineMode.TesseractCubeCombined;
                break;
            case OcrEngineMode.TesseractOnly:
                _api.EngineMode = G8OCREngineMode.TesseractOnly;
                break;
            }
        }

        public void SetPageSegmentationMode (Tesseract.PageSegmentationMode mode)
        {
            switch (mode) {
            case PageSegmentationMode.Auto:
                _api.PageSegmentationMode = G8PageSegmentationMode.Auto;
                break;
            case PageSegmentationMode.AutoOnly:
                _api.PageSegmentationMode = G8PageSegmentationMode.AutoOnly;
                break;
            case PageSegmentationMode.AutoOsd:
                _api.PageSegmentationMode = G8PageSegmentationMode.AutoOSD;
                break;
            case PageSegmentationMode.CircleWord:
                _api.PageSegmentationMode = G8PageSegmentationMode.CircleWord;
                break;
            case PageSegmentationMode.OsdOnly:
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
            case PageSegmentationMode.SparseTextOsd:
                _api.PageSegmentationMode = G8PageSegmentationMode.SparseTextOSD;
                break;
            }
        }

        public List<Result> Results (Tesseract.PageIteratorLevel level)
        {
            var pageIterationLevel = GetPageIteratorLevel (level);
            return this._api.RecognizedBlocksByIteratorLevel (pageIterationLevel)
                .Select (r => ConvertToResult (r))
                .ToList ();
        }

        private G8PageIteratorLevel GetPageIteratorLevel (Tesseract.PageIteratorLevel level)
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

        private Result ConvertToResult (G8RecognizedBlock r)
        {
            return new Result {
                Confidence = (float)r.Confidence,
                Text = r.Text,
                Box = new Rectangle (
                    (float)(_size.Width * r.BoundingBox.X),
                    (float)(_size.Height * r.BoundingBox.Y),
                    (float)(_size.Width * r.BoundingBox.Width),
                    (float)(_size.Height * r.BoundingBox.Height)
                )
            };
        }

        private void CheckIfInitialized ()
        {
            if (!Initialized)
                throw new InvalidOperationException ("Call Init first");
        }
    }
}