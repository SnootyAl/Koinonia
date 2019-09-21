using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Koinonia.Droid;

namespace koinonia.Droid
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        ISharedPreferences prefs;
        Switch swSwitch;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            // Create your application here

            SetContentView(Resource.Layout.activity_settings);

            //need to change it to swSwitch once the project compiles.
            swSwitch = (Switch)FindViewById(Resource.Id.tvSettings);

            Boolean isDarkModeAlreadyEnabled = prefs.GetBoolean("DARK_MODE", false);

            if (isDarkModeAlreadyEnabled)
            {
                swSwitch.Checked = true;
            }
            else
            {
                swSwitch.Checked = false;
            }
        }

        public void SwitchMode(View v)
        {
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutBoolean("DARK_MODE", swSwitch.Checked);
            editor.Commit();   
        }

        public void ShowPrivacyPolicy(View v)
        {
            Xamarin.Forms.Device.OpenUri(new Uri("https://www.freeprivacypolicy.com/privacy/view/53f05bfebffd86f34f74d081923951bc"));
        }

        public void ShowTermsAndConditions(View v)
        {
            Xamarin.Forms.Device.OpenUri(new Uri("https://www.termsandconditionsgenerator.com/live.php?token=6QBYD6I2Q8Vs7a8D7KdXiRtVjMxkAzxW"));
        }
        
    }
}