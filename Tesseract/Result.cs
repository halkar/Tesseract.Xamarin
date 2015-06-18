namespace Tesseract
{
    public class Result
    {
        /// <summary>
        /// For Android Box is in pixels, in iOS it's in fractions.
        /// </summary>
        public Rectangle Box { get; set; }
        public string Text { get; set; }
        public float Confidence { get; set; }
    }
}