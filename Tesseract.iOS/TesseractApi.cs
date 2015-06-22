using UIKit;
using Foundation;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;
using ObjCRuntime;
using System.Linq;
using Tesseract.Binding.iOS;
using MonoTouch.GpuImage;

namespace Tesseract.iOS
{
    public class TesseractApi : ITesseractApi
    {
        private Tesseract.Binding.iOS.G8Tesseract _api;

        private volatile bool _busy;

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

        public async Task<bool> SetImage (byte[] data)
        {
            using (var uIImage = new UIImage (NSData.FromArray (data))) {
                return await Recognise (uIImage);
            }
        }

        public async Task<bool> SetImage (Stream stream)
        {
            using (var uIImage = new UIImage (NSData.FromStream (stream))) {
                return await Recognise (uIImage);
            }
        }

        public async Task<bool> SetImage (string path)
        {
            using (var uIImage = new UIImage (path)) {
                return await Recognise (uIImage);
            }
        }

        private async Task<bool> Recognise (UIImage image)
        {
            if (_busy)
                return false;
            _busy = true;
            try {
                using (var filter = new GPUImageAdaptiveThresholdFilter ()) {
                    filter.BlurSize = 4;
                    using (var filteredImage = filter.ImageByFilteringImage (image)) {
                        _api.Image = filteredImage;
                        return await Task.Run (() => _api.Recognize ());
                    }
                }
            } finally {
                _busy = false;
            }
        }

        public string Text {
            get { return _api.RecognizedText; }
        }

        public int ProgressValue {
            get { return (int)_api.Progress; }
        }

        public void Dispose ()
        {
            if (_api != null) {
                _api.Dispose ();
                _api = null;
            }
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
                    (float)r.BoundingBox.X,
                    (float)r.BoundingBox.Y,
                    (float)r.BoundingBox.Width,
                    (float)r.BoundingBox.Height
                )
            };
        }
    }
}