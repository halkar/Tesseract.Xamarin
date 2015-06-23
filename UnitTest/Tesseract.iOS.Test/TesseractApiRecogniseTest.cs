using System;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace Tesseract.iOS.Test
{
    [TestFixture]
    public class TesseractApiRecogniseTest
    {
        private ITesseractApi _api;

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

        [Test]
        public async void Sample1Jpg ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample1.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("The quick brown fox\njumped over the 5\nlazy dogs!\n\n", _api.Text);
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
                Assert.AreEqual ("ABCDE FGHI\nJKLHN OPQR\nSTUVVJXYZ\n\n", _api.Text);
            }
        }

        [Test]
        public async void Sample3Png ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample3.png")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("the quick brown fox\njumps over the lazy dog-\n\nTHE QUICK BROlLIN FOX\nJUMPS OVER THE LAZY DOG.\n\n", _api.Text);
            }
        }

        [Test]
        public async void Sample4Jpg ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample4.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
                Assert.AreEqual ("Good font for the OCR\nmﬁzufrfomﬁn rﬁe DC’R\nm sml|l mm m cm\n\nGood 60m size for ocn\n\n", _api.Text);
            }
        }

        [Test]
        public async void Sample6BigFile ()
        {
            await _api.Init ("eng");
            using (var stream = LoadSample ("sample6.jpg")) {
                var result = await _api.SetImage (stream);
                Assert.IsTrue (result);
            }
        }

        private Stream LoadSample (string name)
        {
            var assembly = Assembly.GetAssembly (typeof(TesseractApiRecogniseTest));
            return assembly.GetManifestResourceStream ("Tesseract.iOS.Test.samples." + name);
        }
    }
}

