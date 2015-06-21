namespace Tesseract.Droid
{
    public static class PageIteratorLevel
    {
        /// <summary>
        /// Block of text/image/separator line.
        /// </summary>
        public const int Block = 0;

        /// <summary>
        /// Paragraph within a block.
        /// </summary>
        public const int Paragraph = 1;

        /// <summary>
        /// Line within a paragraph.
        /// </summary>
        public const int Textline = 2;

        /// <summary>
        /// Word within a text line.
        /// </summary>
        public const int Word = 3;

        /// <summary>
        /// Symbol/character within a word.
        /// </summary>
        public const int Symbol = 4;
    };
}