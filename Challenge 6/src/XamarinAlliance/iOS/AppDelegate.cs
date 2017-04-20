
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;

namespace XamarinAllianceApp.iOS
{
    [Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// Initialize Xamarin Forms
			global::Xamarin.Forms.Forms.Init ();

            CurrentPlatform.Init();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

