namespace Tesseract.Droid
{
    public static class OcrMode
    {
        /// <summary>
        /// Run Tesseract only - fastest
        /// </summary>
        public const int TesseractOnly = 0;

        /// <summary>
        /// Run Cube only - better accuracy, but slower
        /// </summary>
        public const int CubeOnly = 1;

        /// <summary>
        /// Run both and combine results - best accuracy
        /// </summary>
        public const int TesseractCubeCombined = 2;

        /// <summary>
        /// Default OCR engine mode.
        /// </summary>
        public const int Default = 3;
    }
}