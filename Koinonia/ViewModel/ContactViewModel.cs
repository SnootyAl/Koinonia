using Koinonia.Models;
using Koinonia.Views;
using MvvmHelpers;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

/// <summary>
/// First main 'landing page' after profile creation. Has basic search, as well as an options button down the
/// bottom to facilitate navigation to other parts of the app. This button was essentially meant to be a temporaru
/// button to be broken up later, however time became a restriction and it was left as a working solution.
/// Future versions will have the different functions broken out into a more user intuitivae interface
/// </summary>
namespace Koinonia.ViewModel
{
    public class ContactViewModel : BaseViewModel
    {
        public ICommand TempButtonCommand { get; private set; }
        public ICommand ContactSelectedCommand { get; set; }
        private readonly IPageService _pageService;

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

        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                if (_selectedContact == value)
                {
                    return;
                }
                _selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));
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


        public ICommand GotoProfileCommand { get; private set; }
        public ICommand GotoHexCommand { get; private set; }
        public ICommand AddNewContactCommand { get; private set; }

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
            ContactSelectedCommand = new Command(ContactSelected);

            GotoProfileCommand = new Command(GotoProfile);
            GotoHexCommand = new Command(GotoHex);
            AddNewContactCommand = new Command(AddNewContact);

            

            _pageService = pageService;
            SetContactCollection();   
        }

        private async void CheckPermissions()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Contacts);
            if(status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Contacts))
                {
                    await _pageService.DisplayAlert("Import Contacts", "Please allow access to " +
                        "contacts for Koinonia to import contact list", "OK");
                }
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Contacts);
                await _pageService.DisplayAlert("Hello", "Hello", "Hello");


                if (results.ContainsKey(Permission.Contacts))
                {
                    status = results[Permission.Contacts];
                }
            }

            if (status == PermissionStatus.Granted)
            {
                ImportContacts();
            }
        }

        private async void ImportContacts()
        {
            var contacts = await Plugin.ContactService.CrossContactService.Current.GetContactListAsync();
            ObservableCollection<Contact> contactCollection = new ObservableCollection<Contact>();
            for (int i = 0; i < contacts.Count(); i++)
            {
                //Bug: Splits with space, wont work for last names with more than one word
                //Found at https://alexdunn.org/2017/09/08/xamarin-tip-read-all-contacts-in-android/
                string[] names = contacts[i].Name.Split(' ');
                string firstName = names[0];
                string lastName = "";
                if (names.Length > 1)
                { 
                    //Both first and last name present                    
                    lastName = names[1];
                }
                Contact tempcontact = new Contact
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = contacts[i].Email,
                    PhoneNumber = contacts[i].Number
                };
                await App.Database.SaveContactAsync(tempcontact);
                Contacts.Add(tempcontact);
            }
        }


        /*I believe this is pretty inefficient, some way to optimise?
        CUrrently, Addnewcontact page adds to the database, then calls this function.
        This function creates an entirely new ObservableCollection rather than adding to the list.
        Works. call it 'AGILE methodology' --Alex   */
        public async void SetContactCollection()
        {

            Contacts = new ObservableCollection<Contact>(await App.Database.GetContactsAsync());

            //FilteredContacts for search function
            FilteredContacts = Contacts;
            
            

            /*Uncomment to add dummy contact buttons to see how the grid scales. Note the buttons are not
           clickable by design as they only exist locally on this page and do not persist (and thus dont
           play nicely with the ContactSelected function) Will also dissapear if you go into an info page and
           back out. Cannot stress enough that this is essentially a demonstration thing*/

            //AddDummyContacts(20);
        }

        public void AddContact(Contact newContact)
        {
            Contacts.Add(newContact);
        }

        //Dummy Contacts to showcase the listView. While editable, edited details will not persist as they are not
        //written to the database by design.
        private void AddDummyContacts(int numberOfContacts)
        {
            for (int i = 0; i < numberOfContacts; i++)
            {
                Contact temp = new Contact()
                {
                    FirstName = i.ToString(),
                    LastName = i.ToString(),
                    PhoneNumber = i.ToString()
                };
                Contacts.Add(temp);
            }

        }

        public void UpdateContact(Contact updatedContact)
        {
            Contact contact = Contacts.Where(c => c.ContactID == updatedContact.ContactID).Single<Contact>();
            int index = Contacts.IndexOf(contact);
            Contacts[index] = updatedContact;
        }

        async void ContactSelected()
        {
            //Kind of ugly way to deal with deselecting SelectedContact to remove selection on list screen
            if (SelectedContact != null)
            {
                await _pageService.PushAsync(new ContactInfoPage(SelectedContact));
                SelectedContact = null;
            }
        }

        public void OnAppearing()
        {
            SetContactCollection();
        }

        private async void GotoProfile()
        {
            await _pageService.PushAsync(new ProfilePage());
        }

        private async void GotoHex()
        {
            await _pageService.PushAsync(new HexPage());
        }

        private async void AddNewContact()
        {
            await _pageService.PushAsync((new NewContactPage(this)));
        }

        /*This button was always intended to be temporary to allow developer navigation in the app. Fell under the
        radar of 'things that needed to be fixed but ran out of time'. Per ContactViewModel summary, this will
        likely be broken out into more pleasing ui elements.*/
        private async void TempButtonPressed()
        {

            var response = await _pageService.DisplayActionSheet("Options", "Cancel", null, "Import", "Settings", "Clear", "Tags", "New Reminder", "Sign");

            switch (response)
            {

                //**BUG** Title back button persists for a moment after pressing, allowing user to spam button and return to SignupPage
                //Navigate to Profile Info Page               
                
                //Import all Phone contacts
                case "Import":
                    CheckPermissions();
                    break;
                    
                //Navigate to SettingsPage
                case "Settings":
                    await _pageService.PushAsync(new SettingsPage());
                    break;
                    
                //Clear all contacts
                case "Clear":

                    await App.Database.DeleteAllContactsAsync();
                    Contacts.Clear();
                    break;

                //Navigate to Tags page
                case "Tags":

                    await _pageService.PushAsync(new TagsPage());
                    break;

                // Navigate to Reminders 
                case "New Reminder":
                    await _pageService.PushAsync(new ReminderSetupPage());
                    break;
                    
                case "Sign":
                    await _pageService.PushAsync(new SignupPage());
                    break;
                
                //Check these cases, possible merge bug. Uneccesary?

                case "Profile":

                    await _pageService.PushAsync(new ProfilePage());
                    break;
                    
                case "New Contact":
                    await _pageService.PushAsync((new NewContactPage(this)));
                    break;

            }
        }
    }


}
