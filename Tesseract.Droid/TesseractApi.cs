using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Com.Googlecode.Tesseract.Android;
using File = Java.IO.File;
using Object = Java.Lang.Object;

namespace Tesseract.Droid
{
    public class TesseractApi : ITesseractApi
    {
        private readonly Context _context;
        private readonly ProgressHandler _progressHandler = new ProgressHandler ();
        private TessBaseAPI _api;
        private volatile bool _busy;

        /// <summary>
        /// Whitelist of characters to recognize.
        /// </summary>
        public const string VAR_CHAR_WHITELIST = "tessedit_char_whitelist";

        /// <summary>
        /// Blacklist of characters to not recognize.
        /// </summary>
        public const string VAR_CHAR_BLACKLIST = "tessedit_char_blacklist";

        public string Text { get; private set; }

        public TesseractApi (Context context)
        {
            _context = context;
            _progressHandler.Progress += (sender, e) => {
                OnProgress (e.Progress);
            };
            _api = new TessBaseAPI (_progressHandler);
        }

        public int ProgressValue { get; private set; }

        public bool Initialized { get; private set; }

        public async Task<bool> Init (string language, OcrEngineMode? mode = null)
        {
            if (string.IsNullOrEmpty (language))
                return false;
            var path = await CopyAssets ();
            var result = mode.HasValue
                ? _api.Init (path, language, GetOcrEngineMode (mode.Value))
                : _api.Init (path, language);
            Initialized = result;
            return result;
        }

        public async Task<bool> SetImage (byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException ("data");
            using (var bitmap = await BitmapFactory.DecodeByteArrayAsync (data, 0, data.Length, GetOptions ())) {
                return await Recognise (bitmap);
            }
        }

        public async Task<bool> SetImage (string path)
        {
            if (path == null)
                throw new ArgumentNullException ("path");
            using (var bitmap = await BitmapFactory.DecodeFileAsync (path, GetOptions ())) {
                return await Recognise (bitmap);
            }
        }

        public async Task<bool> SetImage (Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException ("stream");
            using (var bitmap = await BitmapFactory.DecodeStreamAsync (stream)) {
                return await Recognise (bitmap);
            }
        }

        public void SetWhitelist (string whitelist)
        {
            _api.SetVariable (VAR_CHAR_WHITELIST, whitelist);
        }

        public void SetBlacklist (string blacklist)
        {
            _api.SetVariable (VAR_CHAR_BLACKLIST, blacklist);
        }

        public void SetRectangle (Tesseract.Rectangle rect)
        {
            _api.SetRectangle ((int)rect.Left, (int)rect.Top, (int)rect.Width, (int)rect.Height);
        }

        public void SetPageSegmentationMode (PageSegmentationMode mode)
        {
            switch (mode) {
            case PageSegmentationMode.Auto:
                _api.SetPageSegMode (PageSegMode.Auto);
                break;
            case PageSegmentationMode.AutoOnly:
                _api.SetPageSegMode (PageSegMode.AutoOnly);
                break;
            case PageSegmentationMode.AutoOsd:
                _api.SetPageSegMode (PageSegMode.AutoOsd);
                break;
            case PageSegmentationMode.CircleWord:
                _api.SetPageSegMode (PageSegMode.CircleWord);
                break;
            case PageSegmentationMode.OsdOnly:
                _api.SetPageSegMode (PageSegMode.OsdOnly);
                break;
            case PageSegmentationMode.SingleBlock:
                _api.SetPageSegMode (PageSegMode.SingleBlock);
                break;
            case PageSegmentationMode.SingleBlockVertText:
                _api.SetPageSegMode (PageSegMode.SingleBlockVertText);
                break;
            case PageSegmentationMode.SingleChar:
                _api.SetPageSegMode (PageSegMode.SingleChar);
                break;
            case PageSegmentationMode.SingleColumn:
                _api.SetPageSegMode (PageSegMode.SingleColumn);
                break;
            case PageSegmentationMode.SingleLine:
                _api.SetPageSegMode (PageSegMode.Auto);
                break;
            case PageSegmentationMode.SingleWord:
                _api.SetPageSegMode (PageSegMode.SingleWord);
                break;
            case PageSegmentationMode.SparseText:
                _api.SetPageSegMode (PageSegMode.SparseText);
                break;
            case PageSegmentationMode.SparseTextOsd:
                _api.SetPageSegMode (PageSegMode.SparseTextOsd);
                break;
            }
        }

        public void Dispose ()
        {
            if (_api != null) {
                _api.Dispose ();
                _api = null;
            }
        }

        public List<Result> Results (Tesseract.PageIteratorLevel level)
        {	
            var pageIteratorLevel = GetPageIteratorLevel (level);
            int[] boundingBox;
            var results = new List<Result> ();
            var iterator = _api.ResultIterator;
            if (iterator == null)
                return new List<Result> ();
            iterator.Begin ();
            do {
                boundingBox = iterator.GetBoundingBox (pageIteratorLevel);
                var result = new Result {
                    Confidence = iterator.Confidence (pageIteratorLevel),
                    Text = iterator.GetUTF8Text (pageIteratorLevel),
                    Box = new Rectangle (boundingBox [0], boundingBox [1], boundingBox [2], boundingBox [3])
                };
                results.Add (result);
            } while (iterator.Next (pageIteratorLevel));
            return results;
        }

        public event EventHandler<ProgressEventArgs> Progress;

        public async Task<bool> Init (string tessDataPath, string language)
        {
            var result = _api.Init (tessDataPath, language);
            Initialized = result;
            return result;
        }

        private static BitmapFactory.Options GetOptions ()
        {
            return new BitmapFactory.Options { InSampleSize = 4 };
        }

        public async Task<bool> Recognise (Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException ("bitmap");
            if (_busy)
                return false;
            _busy = true;
            try {
                await Task.Run (() => {
                    var grayscaleImage = changeBitmapContrastBrightness (bitmap, 10, -100);
                    _api.SetImage (grayscaleImage);
                    Text = _api.UTF8Text;
                });
                return true;
            } finally {
                _busy = false;
            }
        }

        public static Bitmap changeBitmapContrastBrightness (Bitmap bmp, float contrast, float brightness)
        {
            ColorMatrix cm = new ColorMatrix (new float[] {
                contrast, 0, 0, 0, brightness,
                0, contrast, 0, 0, brightness,
                0, 0, contrast, 0, brightness,
                0, 0, 0, 1, 0
            });
            cm.SetSaturation (0);

            Bitmap ret = Bitmap.CreateBitmap (bmp.Width, bmp.Height, bmp.GetConfig ());

            Canvas canvas = new Canvas (ret);

            Paint paint = new Paint ();
            paint.SetColorFilter (new ColorMatrixColorFilter (cm));
            canvas.DrawBitmap (bmp, 0, 0, paint);

            return ret;
        }

        private int GetOcrEngineMode (OcrEngineMode mode)
        {
            switch (mode) {
            case OcrEngineMode.CubeOnly:
                return OcrMode.CubeOnly;
            case OcrEngineMode.TesseractCubeCombined:
                return OcrMode.TesseractCubeCombined;
            case OcrEngineMode.TesseractOnly:
                return OcrMode.TesseractOnly;
            default:
                return OcrMode.CubeOnly;
            }
        }

        public void Clear ()
        {
            _api.Clear ();
        }

        private int GetPageIteratorLevel (Tesseract.PageIteratorLevel level)
        {
            switch (level) {
            case Tesseract.PageIteratorLevel.Block:
                return PageIteratorLevel.Block;
            case Tesseract.PageIteratorLevel.Paragraph:
                return PageIteratorLevel.Paragraph;
            case Tesseract.PageIteratorLevel.Symbol:
                return PageIteratorLevel.Symbol;
            case Tesseract.PageIteratorLevel.Textline:
                return PageIteratorLevel.Textline;
            case Tesseract.PageIteratorLevel.Word:
                return PageIteratorLevel.Word;
            default:
                return PageIteratorLevel.Word;
            }
        }

        public void OnProgressValues (TessBaseAPI.ProgressValues progress)
        {
            OnProgress (progress.Percent);
        }

        private async Task<string> CopyAssets ()
        {
            try {
                var assetManager = _context.Assets;
                var files = assetManager.List ("tessdata");
                var file = _context.GetExternalFilesDir (null);
                var tessdata = new File (_context.GetExternalFilesDir (null), "tessdata");
                if (!tessdata.Exists ()) {
                    tessdata.Mkdir ();
                }

                foreach (var filename in files) {
                    using (var inStream = assetManager.Open ("tessdata/" + filename)) {
                        var outFile = new File (tessdata, filename);
                        if (outFile.Exists ()) {
                            outFile.Delete ();
                        }
                        using (var outStream = new FileStream (outFile.AbsolutePath, FileMode.Create)) {
                            await inStream.CopyToAsync (outStream);
                            await outStream.FlushAsync ();
                        }
                    }
                }
                return file.AbsolutePath;
            } catch (Exception ex) {
                Log.Error ("[TesseractApi]", ex.Message);
            }
            return null;
        }

        protected virtual void OnProgress (int progress)
        {
            ProgressValue = progress;
            var handler = Progress;
            if (handler != null)
                handler (this, new ProgressEventArgs (progress));
        }

        private class ProgressHandler : Object, TessBaseAPI.IProgressNotifier
        {
            public void OnProgressValues (TessBaseAPI.ProgressValues progress)
            {
                OnProgress (progress.Percent);
            }

            internal event EventHandler<ProgressEventArgs> Progress;

            private void OnProgress (int progress)
            {
                var handler = Progress;
                if (handler != null)
                    handler (this, new ProgressEventArgs (progress));
            }
        }
    }
}