using Koinonia.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koinonia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactList : ContentPage
    {
        protected ContactViewModel vm;
        public ContactList()
        {
            InitializeComponent();
            BindingContext = vm = new ContactViewModel(new PageService());
        }

        protected override void OnAppearing()
        {
            vm.OnAppearing();
            base.OnAppearing();
        }

        //override Android back button to avoid returning to signup screen.
        // **** Explore XAMARIN for possibility of setting this screen to the new 'home' or 'base' screen? Bottom of stack? - A
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        

        
    }
}