using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XLabs.Platform.Services.Media;
using Autofac;
using XLabs.Ioc;
using XLabs.Ioc.Autofac;
using Tesseract.iOS;

namespace Tesseract.Forms.Test.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();
            
			var containerBuilder = new Autofac.ContainerBuilder();

			containerBuilder.RegisterType<MediaPicker> ().As<IMediaPicker> ();
			containerBuilder.RegisterType<TesseractApi> ().As<ITesseractApi> ();

			Resolver.SetResolver(new AutofacResolver(containerBuilder.Build()));

            return base.FinishedLaunching(app, options);
        }
    }
}
