using System.IO;
using System.Linq;
using System.Reflection;
using CoreImage;
using Foundation;
using NUnit.Framework;
using UIKit;

namespace Tesseract.iOS.Test
{
    [TestFixture]
    public class TesseractApiRecogniseTest
    {
        [SetUp]
        public void Setup ()
        {
            _api = new TesseractApi ();
        }


        [TearDown]
        public void Tear ()
        {
            _api.Dispose ();
            _api = null;
        }

        private TesseractApi _api;


        public static Stream LoadSample (string name)
        {
            var assembly = Assembly.GetAssembly (typeof (TesseractApiRecogniseTest));
            return assembly.GetManifestResourceStream ("Tesseract.iOS.Test.samples." + name);
        }

        [Test]
        public async void Sample1Jpg ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample1.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("The quick brown fox\njumped over the 5\nlazy dogs!\n\n", _api.Text);
                var data = _api.Results (PageIteratorLevel.Block).ToList ();
                Assert.AreEqual (1, data.Count);
                Assert.AreEqual ("The quick brown fox\njumped over the 5\nlazy dogs!\n\n", data [0].Text);
                data = _api.Results (PageIteratorLevel.Paragraph).ToList ();
                Assert.AreEqual (1, data.Count);
                Assert.AreEqual ("The quick brown fox\njumped over the 5\nlazy dogs!\n\n", data [0].Text);
                data = _api.Results (PageIteratorLevel.Symbol).ToList ();
                Assert.AreEqual (39, data.Count);
                data = _api.Results (PageIteratorLevel.Textline).ToList ();
                Assert.AreEqual (3, data.Count);
                Assert.AreEqual ("The quick brown fox\n", data [0].Text);
                Assert.AreEqual ("jumped over the 5\n", data [1].Text);
                Assert.AreEqual ("lazy dogs!\n\n", data [2].Text);
                Assert.AreEqual (new Rectangle (37, 233, 415, 89), data [2].Box);
                data = _api.Results (PageIteratorLevel.Word).ToList ();
                Assert.AreEqual (10, data.Count);
            }
        }

        [Test]
        public async void Sample1JpgRectangle ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample1.jpg")) {
                _api.SetRectangle (new Rectangle (0, 0, 900, 100));
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("The quick brown fox\n\n", _api.Text);
                var data = _api.Results (PageIteratorLevel.Block).ToList ();
                Assert.AreEqual (1, data.Count);
                Assert.AreEqual ("The quick brown fox\n\n", data [0].Text);
                data = _api.Results (PageIteratorLevel.Paragraph).ToList ();
                Assert.AreEqual (1, data.Count);
                Assert.AreEqual ("The quick brown fox\n\n", data [0].Text);
            }
        }

        [Test]
        public async void Sample2Png ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample2.png")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("ABCDE FGHI\nJKLHN OPQR\nSTUVVJXYZ\n\n", _api.Text);
            }
        }

        [Test]
        public async void Sample3Png ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample3.png"))
            using (var image = new CIImage (NSData.FromStream (stream)))
            using (var blur = new CIGaussianBlur ())
            using (var context = CIContext.Create ()) {
                blur.SetDefaults ();
                blur.Image = image;
                blur.Radius = 0;
                using (var outputCiImage = context.CreateCGImage (blur.OutputImage, image.Extent))
                using (var newImage = new UIImage (outputCiImage)) {
                    var result = await ((TesseractApi)_api).Recognise (newImage);
                    Assert.IsTrue (result);
                    Assert.AreEqual (
                        "the quick brown fox\njumps over the lazy dog-\n\nTHE QUICK BROlLIN FOX\nJUMPS OVER THE LAZY DOG.\n\n",
                        _api.Text);
                }
            }
        }


        [Test]
        [Ignore]
        public async void Sample3PngPerformance ()
        {
            await _api.Init ("eng");
            for (var i = 0; i < 10000; i++) {
                using (var stream = LoadSample ("sample3.png")) {
                    var result = await _api.SetImage (stream);
                    Assert.IsTrue (result);
                }
            }
        }

        [Test]
        public async void Sample4Jpg ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual (
                    "Good font for the OCR\nmﬁzufrfomﬁn rﬁe DC’R\nm sml|l mm m cm\n\nGood 60m size for ocn\n\n",
                    _api.Text);
            }
        }

        [Test]
        public async void Sample4JpgWithSetVariable ()
        {
            await _api.Init ("eng");
            _api.SetVariable ("tessedit_char_whitelist", "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual (
                    "Good font for the OCR\nDingufrfom n Me am\nhe mm mm m cm\n\nGood 60m size for ocn\n\n", _api.Text);
            }
        }

        [Test]
        public async void Sample4JpgWithWhitelist ()
        {
            await _api.Init ("eng");
            _api.SetWhitelist ("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual (
                    "Good font for the OCR\nDingufrfom n Me am\nhe mm mm m cm\n\nGood 60m size for ocn\n\n", _api.Text);
            }
        }

        [Test]
        public async void Sample6BigFile ()
        {
            await _api.Init ("eng");
            _api.MaximumRecognitionTime = 1;
            using (var stream = LoadSample ("sample6.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
            }
        }
    }
}