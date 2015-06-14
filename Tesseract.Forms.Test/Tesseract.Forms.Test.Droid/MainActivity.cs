using Android.App;
using Android.Content.PM;
using Android.OS;
using Tesseract.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XLabs.Platform.Services.Media;
using XLabs.Platform.Device;
using XLabs.Ioc;
using Autofac;
using XLabs.Ioc.Autofac;
using Android.Content;
using Android.Util;

namespace Tesseract.Forms.Test.Droid
{
    [Activity(Label = "Tesseract.Forms.Test", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

			var containerBuilder = new Autofac.ContainerBuilder();

			containerBuilder.Register(c => this).As<Context>();
			containerBuilder.RegisterType<MediaPicker> ().As<IMediaPicker> ();
			containerBuilder.RegisterType<TesseractApi> ().As<ITesseractApi> ();

			Resolver.SetResolver(new AutofacResolver(containerBuilder.Build()));

            LoadApplication(new App());
        }
    }
}

