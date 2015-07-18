using System;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace Tesseract.Droid.Test
{
    [TestFixture]
    public class TesseractApiRecogniseTest
    {
        private ITesseractApi _api;

        [SetUp]
        public void Setup ()
        {
            _api = new TesseractApi (Android.App.Application.Context);
        }

        
        [TearDown]
        public void Tear ()
        {
            _api.Dispose ();
            _api = null;
        }

        [Test]
        public async void Sample1Jpg ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample1.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("The quick brown fox\njumped over the 5\nlazy dogs!", _api.Text);
                var data = _api.Results (Tesseract.PageIteratorLevel.Block);
                Assert.AreEqual (1, data.Count);
                Assert.AreEqual ("The quick brown fox\njumped over the 5\nlazy dogs!\n\n", data [0].Text);
                data = _api.Results (Tesseract.PageIteratorLevel.Paragraph);
                Assert.AreEqual (1, data.Count);
                Assert.AreEqual ("The quick brown fox\njumped over the 5\nlazy dogs!\n\n", data [0].Text);
                data = _api.Results (Tesseract.PageIteratorLevel.Symbol);
                Assert.AreEqual (39, data.Count);
                data = _api.Results (Tesseract.PageIteratorLevel.Textline);
                Assert.AreEqual (3, data.Count);
                Assert.AreEqual ("The quick brown fox\n", data [0].Text);
                Assert.AreEqual ("jumped over the 5\n", data [1].Text);
                Assert.AreEqual ("lazy dogs!\n\n", data [2].Text);
                data = _api.Results (Tesseract.PageIteratorLevel.Word);
                Assert.AreEqual (10, data.Count);
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
        public async void Sample3Png ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample3.png")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("the quick brown fox\njumps over the lazy dog-\n\nTHE QUICK BROlLIN FOX\nJUMPS OVER THE LAZY DOG.", _api.Text);
            }
        }

        [Test]
        public async void Sample4Jpg ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("Good font for the OCR\nmﬁzufrfomﬁv rﬁe DC’R\nm m“ {mu mom\n\nGood font size for ocn", _api.Text);
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

        [Test]
        public async void Sample3TesseractOnlyPng ()
        {
            await _api.Init ("eng", OcrEngineMode.TesseractOnly);
            using (var stream = LoadSample ("sample3.png")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("the quick brown fox\njumps over the lazy dog-\n\nTHE QUICK BROlLIN FOX\nJUMPS OVER THE LAZY DOG.", _api.Text);
            }
        }

        [Test]
        public async void Sample4TesseractOnlyJpg ()
        {
            await _api.Init ("eng", OcrEngineMode.TesseractOnly);
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("Good font for the OCR\nmﬁzufrfomﬁv rﬁe DC’R\nm m“ {mu mom\n\nGood font size for ocn", _api.Text);
            }
        }

        [Test]
        public async void Sample3CubeOnlyPng ()
        {
            await _api.Init ("eng", OcrEngineMode.CubeOnly);
            using (var stream = LoadSample ("sample3.png")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("the quick brtngn fax\njumps i3Vi2r' the lazy dog.\n\nTHE ttuICK BR()l1N FOX\nJunps ()NIER THE LAZY DOG.", _api.Text);
            }
        }

        [Test]
        public async void Sample4CubeOnlyJpg ()
        {
            await _api.Init ("eng", OcrEngineMode.CubeOnly);
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("Good font for the OCR\n1hfkuftforrtfor tke JC'R\nToo small font mr0CR\n\nGood font size for OCR", _api.Text);
            }
        }

        [Test]
        public async void Sample3TesseractCubeCombinedPng ()
        {
            await _api.Init ("eng", OcrEngineMode.TesseractCubeCombined);
            using (var stream = LoadSample ("sample3.png")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("the quick brown fax\njumps over the lazy dog.\n\nTHE QUICK BROlLIN FOX\nJUMPS OVER THE LAZY DOG.", _api.Text);
            }
        }

        [Test]
        public async void Sample4TesseractCubeCombinedJpg ()
        {
            await _api.Init ("eng", OcrEngineMode.TesseractCubeCombined);
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("Good font for the OCR\n1hfkuftforrtfor tke DC’R\nm small font mom\n\nGood font size for OCR", _api.Text);
            }
        }

        [Test]
        public async void Sample4AllModes ()
        {
            using (_api = new TesseractApi (Android.App.Application.Context)) {
                foreach (var engineMode in Enum.GetValues(typeof(OcrEngineMode))) {
                    Console.WriteLine ("Engine mode: {0}", engineMode);
                    await _api.Init ("eng", (OcrEngineMode)engineMode);
                    foreach (var segmentationMode in Enum.GetValues(typeof(PageSegmentationMode))) {
                        _api.SetPageSegmentationMode ((PageSegmentationMode)segmentationMode);
                        using (var stream = LoadSample ("sample4.jpg")) {
                            var result = await _api.SetImage (stream);
                            Assert.IsTrue (result);
                            Console.WriteLine ("Segmentation mode: {0}, result: \"{1}\"", segmentationMode, _api.Text.Replace ("\n", " "));
                        }
                    }
                }
            }
        }

        [Test]
        public async void Sample3AllModes ()
        {
            using (_api = new TesseractApi (Android.App.Application.Context)) {
                foreach (var engineMode in Enum.GetValues(typeof(OcrEngineMode))) {
                    Console.WriteLine ("Engine mode: {0}", engineMode);
                    await _api.Init ("eng", (OcrEngineMode)engineMode);
                    foreach (var segmentationMode in Enum.GetValues(typeof(PageSegmentationMode))) {
                        _api.SetPageSegmentationMode ((PageSegmentationMode)segmentationMode);
                        using (var stream = LoadSample ("sample3.png")) {
                            var result = await _api.SetImage (stream);
                            Assert.IsTrue (result);
                            Console.WriteLine ("<strong>{0}:</strong>\n{1}", segmentationMode, _api.Text.Replace ("\n", " "));
                        }
                    }
                }
            }
        }

        public static Stream LoadSample (string name)
        {
            var assembly = Assembly.GetAssembly (typeof(TesseractApiRecogniseTest));
            return assembly.GetManifestResourceStream ("Tesseract.Droid.Test.samples." + name);
        }
    }
}

