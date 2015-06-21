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
        Task<bool> Init(string lang, OcrEngineMode? mode = null);

        /// <summary>
        /// Recognise image.
        /// </summary>
        Task<bool> SetImage(string path);

        /// <summary>
        /// Recognise image.
        /// </summary>
        Task<bool> SetImage(byte[] data);

        /// <summary>
        /// Recognise image.
        /// </summary>
        Task<bool> SetImage(Stream stream);

        /// <summary>
        /// Get all recognised text in one block.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Get all results.
        /// </summary>
        /// <param name="level">Block, paragraph, line, word, symbol</param>
        List<Result> Results(PageIteratorLevel level);

        /// <summary>
        /// Is library initialised.
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        /// Set page segmentation mode.
        /// </summary>
        void SetPageSegmentationMode(PageSegmentationMode mode);
    }
}

