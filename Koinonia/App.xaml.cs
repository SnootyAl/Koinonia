using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Koinonia.Data;


namespace Koinonia
{
    public partial class App : Application
    {

        static ContactDatabase contactDB;

        public static ContactDatabase ContactDatabase
        {
            get
            {
                if (contactDB == null)
                {
                    contactDB = new ContactDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Contacts.db3"));
                }
                return contactDB;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new WelcomePage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
