using Android.App;
using Android.Content.PM;
using Android.OS;
using Tesseract.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XLabs.Platform.Services.Media;
using XLabs.Platform.Device;

namespace Tesseract.Forms.Test.Droid
{
    [Activity(Label = "Tesseract.Forms.Test", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
			var device = AndroidDevice.CurrentDevice;
            DependencyService.Register<ITesseract, TesseractApi>();
            DependencyService.Register<IMediaPicker, MediaPicker>();
            LoadApplication(new App());
        }
    }
}

