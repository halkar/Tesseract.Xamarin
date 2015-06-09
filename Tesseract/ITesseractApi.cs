using System;
using System.Threading.Tasks;
using System.IO;

namespace Tesseract
{
	public interface ITesseractApi : IDisposable
	{
		Task<bool> Init(string tessDataPath, string lang);
		Task<bool> Init(string lang);
		void SetImage(string path);
		void SetImage(byte[] data);
		void SetImage (Stream stream);
		string Text { get; }
	}
}

