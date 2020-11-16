using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.AzurePushNotification;
using SalesWorkforce.MobileApp.Common.Constants;

namespace SalesWorkforce.MobileApp.Droid
{
    [Application(
        Theme = "@style/MainTheme"
        )]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Xamarin.Essentials.Platform.Init(this);

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                AzurePushNotificationManager.DefaultNotificationChannelId = "SalesWorkforce";
                AzurePushNotificationManager.DefaultNotificationChannelName = "SalesWorkforce";
            }

            //AzurePushNotificationManager.IconResource = MyOSMv2.Droid.Resource.Drawable.logo_notification;

#if DEBUG
            AzurePushNotificationManager.Initialize(this, AppConstants.APNHubConnectionString, AppConstants.APNHubName, resetToken: false, autoRegistration: false);
#else
            AzurePushNotificationManager.Initialize(this, AppConstants.APNHubConnectionString,AppConstants.APNHubName, resetToken: false, autoRegistration: false);
#endif
            CrossAzurePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

            };
        }
    }
}
