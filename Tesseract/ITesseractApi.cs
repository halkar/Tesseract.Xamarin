using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace Tesseract
{
    /// <summary>
    /// Base interface for accessing all Tesseract OCR functionality.
    /// </summary>
    public interface ITesseractApi : IDisposable
    {
        /// <summary>
        /// Initialise Tesseract OCR. This method should be called before using Tesseract.
        /// </summary>
        Task<bool> Init (string lang, OcrEngineMode? mode = null);

        /// <summary>
        /// Recognise image.
        /// </summary>
        Task<bool> SetImage (string path);

        /// <summary>
        /// Recognise image.
        /// </summary>
        Task<bool> SetImage (byte[] data);

        /// <summary>
        /// Recognise image.
        /// </summary>
        Task<bool> SetImage (Stream stream);

        /// <summary>
        /// Get all recognised text in one block.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Get all results.
        /// </summary>
        /// <param name="level">Block, paragraph, line, word, symbol</param>
        List<Result> Results (PageIteratorLevel level);

        /// <summary>
        /// Is library initialised.
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        /// Set page segmentation mode.
        /// </summary>
        void SetPageSegmentationMode (PageSegmentationMode mode);

        /// <summary>
        /// Sets the whitelist.
        /// </summary>
        void SetWhitelist (string whitelist);

        /// <summary>
        /// Sets the blacklist.
        /// </summary>
        void SetBlacklist (string blacklist);

        /// <summary>
        /// Frees up recognition results and any stored image data, without actually
        /// freeing any recognition data that would be time-consuming to reload.
        /// Afterwards, you must call SetImage before getting any
        /// Text or Results.
        /// </summary>
        void Clear ();

        /// <summary>
        /// Restricts recognition to a sub-rectangle of the image. Call after
        /// SetImage. Each SetRectangle clears the recognition results so multiple
        /// rectangles can be recognized with the same image.
        /// </summary>
        void SetRectangle (Tesseract.Rectangle rect);
    }
}

