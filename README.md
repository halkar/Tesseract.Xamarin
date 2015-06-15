# Xamarin.Tesseract
Xamarin.Tesseract is a wrapper for [Tesseract OCR](https://code.google.com/p/tesseract-ocr/) library.
For Android [tess-two](https://github.com/rmtheis/tess-two) is used and for iOS implementation from [gali8](https://github.com/gali8/Tesseract-OCR-iOS) (v.3.4.0) is used.
##Utilisation
Best way to use Xamarin.Tesseract is to add [Nuget package](https://www.nuget.org/packages/Xamarin.Tesseract/) to your project.

    //Android
    TesseractApi api = new TesseractApi (context);
    //iOS
    TesseractApi api = new TesseractApi ();
    await api.Init ("eng");
    await api.SetImage("image_path");
    string text = api.Text;
You will also need [tessdata](https://code.google.com/p/tesseract-ocr/downloads/list) files for the languges you need.
In Android application `tessdata` folder should be in your assets directory and files should be marked as `AndroidAssets`. In iOS project `testate` should be in `Resources` and files should be marked as `BundleResource`. You can add more than one language in the same folder. In this case use comma-separated list of languages to initialise `TesseractApi`.