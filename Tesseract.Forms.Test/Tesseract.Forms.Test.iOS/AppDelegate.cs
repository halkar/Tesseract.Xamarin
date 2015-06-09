using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Autofac;
using XLabs.Platform.Services.Media;
using Tesseract.iOS;
using XLabs.Ioc.Autofac;
using XLabs.Ioc;
using Xamarin;

namespace Tesseract.Forms.Test.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			// Code to start the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Calabash.Start();
			#endif

			Xamarin.Forms.Forms.Init();

			var containerBuilder = new Autofac.ContainerBuilder();

			containerBuilder.RegisterType<MediaPicker> ().As<IMediaPicker> ();
			containerBuilder.RegisterType<TesseractApi> ().As<ITesseractApi> ();

			Resolver.SetResolver(new AutofacResolver(containerBuilder.Build()));

			LoadApplication (new App ());

			return base.FinishedLaunching(application, launchOptions);

		}
	}
}


