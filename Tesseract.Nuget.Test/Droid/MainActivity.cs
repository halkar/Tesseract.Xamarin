using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Autofac;
using XLabs.Platform.Services.Media;
using Tesseract.Droid;
using XLabs.Ioc;
using XLabs.Ioc.Autofac;

namespace Tesseract.Nuget.Test.Droid
{
	[Activity (Label = "Tesseract.Nuget.Test.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			var containerBuilder = new Autofac.ContainerBuilder();

			containerBuilder.Register(c => this).As<Context>();
			containerBuilder.RegisterType<MediaPicker> ().As<IMediaPicker> ();
			containerBuilder.RegisterType<TesseractApi> ().As<ITesseractApi> ();

			Resolver.SetResolver(new AutofacResolver(containerBuilder.Build()));


			LoadApplication (new App ());
		}
	}
}

