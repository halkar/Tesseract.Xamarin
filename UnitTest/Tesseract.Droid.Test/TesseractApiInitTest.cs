using System;
using NUnit.Framework;

namespace Tesseract.Droid.Test
{
    [TestFixture]
    public class TesseractApiInitTest
    {
        private ITesseractApi _api;

        [SetUp]
        public void Setup ()
        {
            _api = new TesseractApi (Android.App.Application.Context, AssetsDeployment.OncePerInitialization);
        }

        
        [TearDown]
        public void Tear ()
        {
            _api.Dispose ();
            _api = null;
        }

        [Test]
        public async void InitOneLang ()
        {
            var result = await _api.Init ("eng");
            Assert.IsTrue (result);
        }

        [Test]
        public async void InitOneWithOcrEngineModeLang ()
        {
            var result = await _api.Init ("eng", OcrEngineMode.CubeOnly);
            Assert.IsTrue (result);
        }

        [Test]
        public async void InitTwoLangs ()
        {
            var result = await _api.Init ("eng+rus");
            Assert.IsTrue (result);
        }

        [Test]
        public async void InitAbsentLang ()
        {
            var result = await _api.Init ("spa");
            Assert.IsFalse (result);
        }

        [Test]
        public async void InitEmptyLang ()
        {
            var result = await _api.Init ("");
            Assert.IsFalse (result);
        }

        [Test]
        public async void InitNullLang ()
        {
            var result = await _api.Init (null);
            Assert.IsFalse (result);
        }
    }
}

