using Koinonia.Models;
using Koinonia.Views;
using MvvmHelpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;


namespace Koinonia.ViewModel
{
    public class ContactViewModel : BaseViewModel
    {
        public ICommand TempButtonCommand { get; private set; }
        public ICommand SearchTextChangedCommand { get; private set; }
        private readonly IPageService _pageService;

         void OnAppearing()
        {
            throw new NotImplementedException();
        }

        private SQLiteAsyncConnection _connection;

        private ObservableCollection<Contact> _contacts { get; set; }
        public ObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
            set
            {
                if (_contacts == value)
                {
                    return;
                }
                _contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }
        }

        private ObservableCollection<Contact> _filteredContacts { get; set; }
        public ObservableCollection<Contact> FilteredContacts
        {
            get { return _filteredContacts; }
            set
            {
                if (_filteredContacts == value)
                {
                    return;
                }
                _filteredContacts = value;
                OnPropertyChanged(nameof(FilteredContacts));
            }
        }

        


        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                SearchUserText(_searchText);
                OnPropertyChanged(nameof(SearchText));
                
            }
        }
        
        private void SearchUserText(string _searchText)
        {
            var searchText = _searchText.Trim();
            if (searchText.Length <= 0)
            {
                FilteredContacts = _contacts;
                return;
            }

            FilteredContacts = new ObservableCollection<Contact>(_contacts.Where(name => name.FirstName.ToLower().Contains(searchText.ToLower())));
        }


        public ContactViewModel(IPageService pageService)
        {
            TempButtonCommand = new Command(TempButtonPressed);
            _pageService = pageService;
            SetContactCollection();
              
        }
        /*I believe this is pretty inefficient, some way to optimise?
        CUrrently, Addnewcontact page adds to the database, then calls this function.
        This function creates an entirely new ObservableCollection rather than adding to the list.
        Works. call it 'AGILE methodology' --Alex   */
        public async void SetContactCollection()
        {            
            Contacts = new ObservableCollection<Contact>(await App.Database.GetContactsAsync());
            FilteredContacts = Contacts;
        }

        public void AddContact(Contact newContact)
        {
            Contacts.Add(newContact);
        }
        
       


        async void TempButtonPressed()
        {

            var response = await _pageService.DisplayActionSheet("Options", "Cancel", null, "Profile", "Settings", "New Contact", "Clear", "Debug", "Tags");

            switch (response)
            {

                //**BUG** Title back button persists for a moment after pressing, allowing user to spam button and return to SignupPage
                case "Profile":
                    await _pageService.PushAsync(new ProfilePage());
                    break;

                case "Settings":
                    await _pageService.PushAsync(new SettingsPage());
                    break;

                //Temporary add new contact for list debugging and search. Will delete once proper add contact implemented.
                /*case "New Contact":
                    var newContact = new Contact { FirstName = "Added" + DateTime.Now.Ticks };
                    await _connection.InsertAsync(newContact);
                    _contacts.Add(newContact);
                    //await Navigation.PushAsync(new NewContactPage());
                    break;*/

                case "New Contact":

                    //****Bug? Table ID continues to increment? Probably not an issue, saves conflicts in future.

                    await _pageService.PushAsync((new NewContactPage(this)));                    
                    break;

                case "Clear":

                    await App.Database.DeleteAllContactsAsync();
                    Contacts.Clear();
                    break;

                case "Debug":

                    //await Navigation.PushAsync(new DebugPage());
                    break;


                case "Tags":
                    await _pageService.PushAsync(new TagsPage());
                    break;


            }
        }
    }

 
}
