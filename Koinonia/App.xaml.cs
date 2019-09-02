using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Koinonia.Data;
using Xamarin.Essentials;
using SQLite;


namespace Koinonia
{
    public partial class App : Application
    {

        static Database contactDB;

        public static Database Database
        {
            get
            {
                if (contactDB == null)
                {
                    contactDB = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Contacts.db3"));
                }
                return contactDB;
            }
        }       

        public App()
        {
            InitializeComponent();

            if(Preferences.Get("ProfileExists", false))
            {
                MainPage = new NavigationPage(new ContactList());
            }
            else
            {
                MainPage = new NavigationPage(new WelcomePage());
            }

            
            
            
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
