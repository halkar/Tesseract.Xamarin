using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace Tesseract
{
	public interface ITesseractApi : IDisposable
	{
		Task<bool> Init(string lang);
		Task<bool> Init(string lang, OCREngineMode mode);
		Task SetImage(string path);
		Task SetImage(byte[] data);
		Task SetImage(Stream stream);
		string Text { get; }
		List<Result> Results (PageIteratorLevel level);
		bool Initialized { get; }
		void SetPageSegmentationMode (PageSegmentationMode mode);
	}

	public class Result
	{
		/// <summary>
		/// For Android Box is in pixels, in iOS it's in fractions.
		/// </summary>
		public Rectangle Box { get; set; }
		public string Text { get; set; }
		public float Confidence { get; set; }
	}

	public enum PageIteratorLevel
	{
		Block,
		Paragraph,
		Textline,
		Word,
		Symbol
	}

	public enum PageSegmentationMode
	{
		OSDOnly,
		AutoOSD,
		AutoOnly,
		Auto,
		SingleColumn,
		SingleBlockVertText,
		SingleBlock,
		SingleLine,
		SingleWord,
		CircleWord,
		SingleChar,
		SparseText,
		SparseTextOSD
	}

	public enum OCREngineMode
	{
		TesseractOnly,
		CubeOnly,
		TesseractCubeCombined
	}
}

