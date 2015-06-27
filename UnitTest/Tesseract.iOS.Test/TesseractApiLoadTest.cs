using System;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace Tesseract.iOS.Test
{
    [TestFixture]
    public class TesseractApiLoadTest
    {
        [Test]
        public async void TestWithDispiose ()
        {
            for (int i = 0; i < 20; i++) {
                using (ITesseractApi api = new TesseractApi ()) {
                    await api.Init ("eng");
                    using (var stream = TesseractApiRecogniseTest.LoadSample ("sample2.png")) {
                        var result = await api.SetImage (stream);
                        Assert.IsTrue (result);
                        Assert.AreEqual ("ABCDE FGHI\nJKLHN OPQR\nSTUVVJXYZ\n\n", api.Text);
                    }
                }
            }
        }

        [Test]
        public async void TestWithoutDispiose ()
        {
            ITesseractApi api = new TesseractApi ();
            await api.Init ("eng");
            for (int i = 0; i < 20; i++) {
                using (var stream = TesseractApiRecogniseTest.LoadSample ("sample2.png")) {
                    var result = await api.SetImage (stream);
                    Assert.IsTrue (result);
                    Assert.AreEqual ("ABCDE FGHI\nJKLHN OPQR\nSTUVVJXYZ\n\n", api.Text);
                }
            }
            api.Dispose ();
        }
    }
}

