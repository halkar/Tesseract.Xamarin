using ObjCRuntime;

namespace Tesseract.Binding.iOS
{
	[Native]
	public enum G8PageSegmentationMode : ulong
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

	[Native]
	public enum G8OCREngineMode : ulong
	{
		TesseractOnly,
		CubeOnly,
		TesseractCubeCombined
	}

	[Native]
	public enum G8PageIteratorLevel : ulong
	{
		Block,
		Paragraph,
		Textline,
		Word,
		Symbol
	}

	[Native]
	public enum G8Orientation : ulong
	{
		Up,
		Right,
		Down,
		Left
	}

	[Native]
	public enum G8WritingDirection : ulong
	{
		LeftToRight,
		RightToLeft,
		TopToBottom
	}

	[Native]
	public enum G8TextlineOrder : ulong
	{
		LeftToRight,
		RightToLeft,
		TopToBottom
	}
}