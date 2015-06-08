using System;

namespace Tesseract
{
	public interface ITesseract : IDisposable
	{
		void Init(string tessDataPath, string lang);
        void SetImage(byte[] data);
	}
}

