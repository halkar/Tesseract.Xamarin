namespace Tesseract.Droid
{
    public static class PageSegMode
    {
        /// <summary>
        /// Orientation and script detection only.
        /// </summary>
        public const int OsdOnly = 0;

        /// <summary>
        /// Automatic page segmentation with orientation and script detection. (OSD)
        /// </summary>
        public const int AutoOsd = 1;

        /// <summary>
        /// Fully automatic page segmentation, but no OSD, or OCR.
        /// </summary>
        public const int AutoOnly = 2;

        /// <summary>
        /// Fully automatic page segmentation, but no OSD.
        /// </summary>
        public const int Auto = 3;

        /// <summary>
        /// Assume a single column of text of variable sizes.
        /// </summary>
        public const int SingleColumn = 4;

        /// <summary>
        /// Assume a single uniform block of vertically aligned text.
        /// </summary>
        public const int SingleBlockVertText = 5;

        /// <summary>
        /// Assume a single uniform block of text. (Default.)
        /// </summary>
        public const int SingleBlock = 6;

        /// <summary>
        /// Treat the image as a single text line.
        /// </summary>
        public const int SingleLine = 7;

        /// <summary>
        /// Treat the image as a single word.
        /// </summary>
        public const int SingleWord = 8;

        /// <summary>
        /// Treat the image as a single word in a circle.
        /// </summary>
        public const int CircleWord = 9;

        /// <summary>
        /// Treat the image as a single character.
        /// </summary>
        public const int SingleChar = 10;

        /// <summary>
        /// Find as much text as possible in no particular order.
        /// </summary>
        public const int SparseText = 11;

        /// <summary>
        /// Sparse text with orientation and script detection.
        /// </summary>
        public const int SparseTextOsd = 12;

        /// <summary>
        /// Number of enum entries.
        /// </summary>
        public const int Count = 13;
    }
}