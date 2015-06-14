using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Autofac;
using XLabs.Platform.Services.Media;
using Tesseract.iOS;
using XLabs.Ioc;
using XLabs.Ioc.Autofac;

namespace Tesseract.Nuget.Test.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			var containerBuilder = new Autofac.ContainerBuilder();

			containerBuilder.RegisterType<MediaPicker> ().As<IMediaPicker> ();
			containerBuilder.RegisterType<TesseractApi> ().As<ITesseractApi> ();

			Resolver.SetResolver(new AutofacResolver(containerBuilder.Build()));

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

