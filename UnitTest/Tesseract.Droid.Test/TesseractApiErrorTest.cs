using System;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using Android.Graphics;

namespace Tesseract.Droid.Test
{
    [TestFixture]
    public class TesseractApiErrorTest
    {
        private TesseractApi _api;

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
        [ExpectedException (typeof(ArgumentNullException))]
        public async void NullStringTest ()
        {
            await _api.Init ("eng");
            await _api.SetImage ((string)null);
        }

        [Test]
        [ExpectedException (typeof(ArgumentNullException))]
        public async void NullStreamTest ()
        {
            await _api.Init ("eng");
            await _api.SetImage ((Stream)null);
        }

        [Test]
        [ExpectedException (typeof(ArgumentNullException))]
        public async void NullByteArrayTest ()
        {
            await _api.Init ("eng");
            await _api.SetImage ((byte[])null);
        }

        [Test]
        [ExpectedException (typeof(ArgumentNullException))]
        public async void NullBitmapTest ()
        {
            await _api.Init ("eng");
            await _api.Recognise ((Bitmap)null);
        }

        [Test]
        [ExpectedException (typeof(InvalidOperationException))]
        public async void NotInitializedTest ()
        {
            await _api.Recognise ((Bitmap)null);
        }
    }
}

