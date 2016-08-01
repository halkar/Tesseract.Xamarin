# Xamarin.Tesseract
Xamarin.Tesseract is a wrapper for [Tesseract OCR](https://code.google.com/p/tesseract-ocr/) library.
For Android [tess-two](https://github.com/rmtheis/tess-two) is used and for iOS implementation from [gali8](https://github.com/gali8/Tesseract-OCR-iOS) (v.4.0.0) is used.
##Documantation
[Blog post](http://shamsutdinov.net/2015/07/01/tesseract-orc-xamarin-part-1/)
##Utilisation
Best way to use Xamarin.Tesseract is to add [Nuget package](https://www.nuget.org/packages/Xamarin.Tesseract/) to your project.

    //Android
    TesseractApi api = new TesseractApi (context, AssetsDeployment.OncePerVersion);
    //iOS
    TesseractApi api = new TesseractApi ();
    await api.Init ("eng");
    await api.SetImage("image_path");
    string text = api.Text;
You will also need [tessdata](https://github.com/tesseract-ocr/tessdata) files for the languges you need.
In Android application `tessdata` folder should be in your assets directory and files should be marked as `AndroidAssets`. In iOS project `testate` should be in `Resources` and files should be marked as `BundleResource`. You can add more than one language in the same folder. In this case use "+"-separated list of languages to initialise `TesseractApi`.
##License
Tesseract.Xamarin is distributed under the MIT license (see LICENSE).
Tesseract, maintained by Google (http://code.google.com/p/tesseract-ocr/), is distributed under the Apache 2.0 license (see http://www.apache.org/licenses/LICENSE-2.0).
Tesseract OCR iOS is distributed under MIT license (https://github.com/gali8/Tesseract-OCR-iOS/blob/master/LICENSE.md).
tess-two is distributed under Apache 2.0 license (https://github.com/rmtheis/tess-two/blob/master/COPYING).