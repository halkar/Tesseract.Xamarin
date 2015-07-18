namespace Tesseract
{
    public enum OcrEngineMode
    {
        /// <summary>
        /// Run Tesseract only - fastest
        /// </summary>
        TesseractOnly,
        /// <summary>
        /// Run Cube only - better accuracy, but slower
        /// </summary>
        CubeOnly,
        /// <summary>
        /// Run both and combine results - best accuracy
        /// </summary>
        TesseractCubeCombined
    }
}