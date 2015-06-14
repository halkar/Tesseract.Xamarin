using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace Tesseract
{
	public interface ITesseractApi : IDisposable
	{
		Task<bool> Init(string tessDataPath, string lang);
		Task<bool> Init(string lang);
		Task SetImage(string path);
		Task SetImage(byte[] data);
		Task SetImage(Stream stream);
		string Text { get; }
		int ProgressValue{ get; }
	    event EventHandler<ProgressEventArgs> Progress;
		List<Result> Results ();
		bool Initialized { get; }
	}

	public class Result
	{
		public Rectangle Box { get; set; }
		public string Text { get; set; }
		public float Confidence { get; set; }
	}
}

