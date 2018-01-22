using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Android.App;
using NUnit.Framework;

namespace Tesseract.Droid.Test
{
    [TestFixture]
    public class TesseractApiRecogniseTest
    {
        [SetUp]
        public void Setup ()
        {
            _api = new TesseractApi (Application.Context, AssetsDeployment.OncePerInitialization);
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
            var assembly = Assembly.GetAssembly (typeof(TesseractApiRecogniseTest));
            return assembly.GetManifestResourceStream ("Tesseract.Droid.Test.samples." + name);
        }

        [Test]
        public async void Sample1Jpg ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample1.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("The quick brown fox\njumped over the 5\nlazy dogs!", _api.Text);
                var data = _api.Results (Tesseract.PageIteratorLevel.Block).ToList ();
                Assert.AreEqual (1, data.Count);
                Assert.AreEqual ("The quick brown fox\njumped over the 5\nlazy dogs!\n\n", data [0].Text);
                data = _api.Results (Tesseract.PageIteratorLevel.Paragraph).ToList ();
                Assert.AreEqual (1, data.Count);
                Assert.AreEqual ("The quick brown fox\njumped over the 5\nlazy dogs!\n\n", data [0].Text);
                data = _api.Results (Tesseract.PageIteratorLevel.Symbol).ToList ();
                Assert.AreEqual (39, data.Count);
                data = _api.Results (Tesseract.PageIteratorLevel.Textline).ToList ();
                Assert.AreEqual (3, data.Count);
                Assert.AreEqual ("The quick brown fox\n", data [0].Text);
                Assert.AreEqual ("jumped over the 5\n", data [1].Text);
                Assert.AreEqual ("lazy dogs!\n\n", data [2].Text);
                Assert.AreEqual (new Rectangle (37, 233, 415, 89), data [2].Box);
                data = _api.Results (Tesseract.PageIteratorLevel.Word).ToList ();
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
                Assert.AreEqual ("The quick brown fox", _api.Text);
                var data = _api.Results (Tesseract.PageIteratorLevel.Block).ToList ();
                Assert.AreEqual (1, data.Count);
                Assert.AreEqual ("The quick brown fox\n\n", data [0].Text);
                data = _api.Results (Tesseract.PageIteratorLevel.Paragraph).ToList ();
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
                Assert.AreEqual ("ABCDE FGHI\nJKLHN OPQR\nSTUVVJXYZ", _api.Text);
            }
        }

        [Test]
        public async void Sample3AllModes ()
        {
            foreach (var engineMode in Enum.GetValues(typeof (OcrEngineMode))) {
                Console.WriteLine ("Engine mode: {0}", engineMode);
                await _api.Init ("eng", (OcrEngineMode)engineMode);
                foreach (var segmentationMode in Enum.GetValues(typeof (PageSegmentationMode))) {
                    _api.SetPageSegmentationMode ((PageSegmentationMode)segmentationMode);
                    using (var stream = LoadSample ("sample3.png")) {
                        var result = await _api.SetImage (stream);
                        Assert.IsTrue (result);
                        Console.WriteLine ("Segmentation mode: {0}, result: \"{1}\"", segmentationMode,
                            _api.Text.Replace ("\n", " "));
                    }
                }
            }
        }

        [Test]
        public async void Sample3CubeOnlyPng ()
        {
            await _api.Init ("eng", OcrEngineMode.CubeOnly);
            using (var stream = LoadSample ("sample3.png")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual (
                    "the quick brtngn fax\njumps i3Vi2r' the lazy dog.\n\nTHE ttuICK BR()l1N FOX\nJunps ()NIER THE LAZY DOG.",
                    _api.Text);
            }
        }

        [Test]
        public async void Sample3Png ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample3.png")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual (
                    "the quick brown fox\njumps over the lazy dog-\n\nTHE QUICK BROlLIN FOX\nJUMPS OVER THE LAZY DOG.",
                    _api.Text);
            }
        }

        [Test]
        public async void Sample3TesseractCubeCombinedPng ()
        {
            await _api.Init ("eng", OcrEngineMode.TesseractCubeCombined);
            using (var stream = LoadSample ("sample3.png")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual (
                    "the quick brown fax\njumps over the lazy dog.\n\nTHE QUICK BROlLIN FOX\nJUMPS OVER THE LAZY DOG.",
                    _api.Text);
            }
        }

        [Test]
        public async void Sample3TesseractOnlyPng ()
        {
            await _api.Init ("eng", OcrEngineMode.TesseractOnly);
            using (var stream = LoadSample ("sample3.png")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual (
                    "the quick brown fox\njumps over the lazy dog-\n\nTHE QUICK BROlLIN FOX\nJUMPS OVER THE LAZY DOG.",
                    _api.Text);
            }
        }

        [Test]
        public async void Sample4AllModes ()
        {
            foreach (var engineMode in Enum.GetValues(typeof (OcrEngineMode))) {
                Console.WriteLine ("Engine mode: {0}", engineMode);
                await _api.Init ("eng", (OcrEngineMode)engineMode);
                foreach (var segmentationMode in Enum.GetValues(typeof (PageSegmentationMode))) {
                    _api.SetPageSegmentationMode ((PageSegmentationMode)segmentationMode);
                    using (var stream = LoadSample ("sample4.jpg")) {
                        var result = await _api.SetImage (stream);
                        Assert.IsTrue (result);
                        Console.WriteLine ("Segmentation mode: {0}, result: \"{1}\"", segmentationMode,
                            _api.Text.Replace ("\n", " "));
                    }
                }
            }
        }

        [Test]
        public async void Sample4CubeOnlyJpg ()
        {
            await _api.Init ("eng", OcrEngineMode.CubeOnly);
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.IsTrue (_api.Text.Contains ("Good font for the OCR"));
            }
        }

        [Test]
        public async void Sample4Jpg ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.IsTrue (_api.Text.Contains ("Good font for the OCR"));
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
                Assert.IsTrue (_api.Text.Contains ("Good font for the OCR"));
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
                Assert.IsTrue (_api.Text.Contains ("Good font for the OCR"));
            }
        }

        [Test]
        public async void Sample4TesseractCubeCombinedJpg ()
        {
            await _api.Init ("eng", OcrEngineMode.TesseractCubeCombined);
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.IsTrue (_api.Text.Contains ("Good font for the OCR"));
            }
        }

        [Test]
        public async void Sample4TesseractOnlyJpg ()
        {
            await _api.Init ("eng", OcrEngineMode.TesseractOnly);
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.IsTrue (_api.Text.Contains ("Good font for the OCR"));
            }
        }

        [Test]
        public async void Sample4HOCRText ()
        {
            await _api.Init ("eng", OcrEngineMode.TesseractOnly);
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                var htmlResult = _api.GetHOCRText (0);
                Assert.IsTrue (htmlResult.Contains ("Good font for the OCR"));
            }
        }

        [Test]
        [Ignore]
        public async void Sample6BigFile ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample6.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
            }
        }
    }
}