using System;
using System.Threading.Tasks;

namespace Tesseract
{
	public interface ITesseractApi : IDisposable
	{
		Task<bool> Init(string tessDataPath, string lang);
		Task<bool> Init(string lang);
		void SetImage(string path);
		void SetImage(byte[] data);
		string Text { get; }
	}
}

