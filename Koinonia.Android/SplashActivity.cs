using System;
using Android.App;
using Android.OS;
using Android.Content.PM;

namespace Koinonia.Droid
{
    [Activity(Label = "Koinonia", Icon = "@mipmap/icon", NoHistory = true, Theme = "@style/SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));

        }
    }
}