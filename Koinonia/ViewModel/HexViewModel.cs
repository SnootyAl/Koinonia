using Koinonia.HexLayouts;
using Koinonia.Models;
using Koinonia.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Koinonia.ViewModel
{
    public class HexViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;
        public ICommand contactSelected { get; private set; }
        public int hexColumns, hexRows;
        private int contactColumns, contactRows;

        private HexLayout displayHexGrid { get; set; }
        public HexLayout DisplayHexGrid
        {
            get { return displayHexGrid; }
            set
            {
                if (displayHexGrid == value)
                {
                    return;
                }
                displayHexGrid = value;
                OnPropertyChanged(nameof(DisplayHexGrid));
            }
        }

        private double boundingHeight { get; set; }
        public double BoundingHeight
        {
            get { return boundingHeight; }
            set
            {
                if (boundingHeight == value)
                {
                    return;
                }
                boundingHeight = value;
                OnPropertyChanged(nameof(BoundingHeight));
            }
        }

        private double boundingWidth { get; set; }
        public double BoundingWidth
        {
            get { return boundingWidth; }
            set
            {
                if (boundingWidth == value)
                {
                    return;
                }
                boundingWidth = value;
                OnPropertyChanged(nameof(BoundingWidth));
            }
        }

        private double outerFrame { get; set; }
        public double OuterFrame
        {
            get { return outerFrame; }
            set
            {
                if (outerFrame == value)
                {
                    return;
                }
                outerFrame = value;
                OnPropertyChanged(nameof(OuterFrame));
            }
        }

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
                Console.WriteLine("Contacts List update fired");
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
        private Contact _selectedContact { get; set; }
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


        private double ButtonDiameter;
        private double BoundingFrameLength;
        private double ScalingFactor;
        



        public HexViewModel(IPageService pageService)
        {
            _pageService = pageService;
            contactSelected = new Command(ContactSelected);
            SetScaling();
            _pageService.DisplayAlert("Dimensions", BoundingHeight + "x" + BoundingWidth, "OK");
            ScreenSetup();
            //CreateAndShowGrid();
            
        }

        /*All Visual elements are bound/scaled to the size of a button, which in turn is one third the width of the screen
          by default. This lends itself to pinch to zoom in the future, as binding this change to the button size will
          (Hopefully) scale the rest of the UI accordingly.*/
        private void SetScaling()
        {
            ButtonDiameter = App.screenWidth / 3;
            //Why does Pi always end up in the weirdest spots?
            BoundingHeight = ButtonDiameter * 6.28;
            //BoundingHeight = App.screenHeight;
            BoundingWidth = BoundingHeight;
            OuterFrame = BoundingHeight * 2;
            
        }
        public async void ScreenSetup()
        {
            Contacts = new CustomObservableCollection<Contact>(await App.Database.GetContactsAsync());
            FilteredContacts = Contacts;

            //Will not save to database, just in ObserableCollection. Purely to test scaling of HexLayout
            AddDummyContacts(15);

            contactRows = (int)Math.Round(Math.Sqrt(Contacts.Count()) + 0.5);
            contactColumns = contactRows;            
            CreateAndShowGrid();

            
        }

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

        public void CreateAndShowGrid()
        {
            
            DisplayHexGrid = new HexLayout
            {
                RowCount = contactRows + 1,
                ColumnCount = contactColumns + 1,                
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.FromHex("#17A7B2")     
            };

            int contactIndex = 0;
            int hex_row = 0;
            int hex_column = 0;
            foreach (Contact contact in Contacts)
            {
                Button contactButton = CreateContactButton(contact);
                HexLayout.SetRow(contactButton, hex_row);
                HexLayout.SetColumn(contactButton, hex_column);
                DisplayHexGrid.Children.Add(contactButton);
                if (hex_column == contactColumns - 1)
                {
                    hex_column = 0;
                    hex_row++;
                }
                else
                {
                    hex_column++;
                }

            }
        }

        private Button CreateContactButton(Contact contact)
        {
            var tempButton = new HexButton
            {
                Text = contact.FirstName,
                BackgroundColor = Color.FromRgb(255, 255, 255),
                HeightRequest = ButtonDiameter,
                WidthRequest = ButtonDiameter,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 75,
                ContactID = contact.ContactID,
                CommandParameter = contact.ContactID
            };
            tempButton.SetBinding(Button.CommandProperty, new Binding("contactSelected"));
            return tempButton;
        }

        public async void ContactSelected(object param)
        {
            //Index becomes ContactID of requested Contact
            int index = Convert.ToInt32(param);

            //Find associated contact object in Contacts ObservableCollection
            SelectedContact = Contacts.Where(c => c.ContactID == index).Single<Contact>();

            //Set next page to push to
            

            //MessagingCenter.Subscribe<ContactInfoViewModel, Contact>(this, "ContactUpdated", UpdateContact);

            //Push new page with this returned contact as the parameter
            await _pageService.PushAsync(new ContactInfoPage(SelectedContact));

            //await _pageService.DisplayAlert("Hello", index.ToString(), "Ok") ;
        }

        public void UpdateContact(ContactInfoViewModel source, Contact updatedContact)
        {
            Contact contact = Contacts.Where(c => c.ContactID == updatedContact.ContactID).Single<Contact>();
            int index = Contacts.IndexOf(contact);
            Contacts[index] = updatedContact;
            
        }

        private async void UpdateContacts()
        {
            Contacts = new ObservableCollection<Contact>(await App.Database.GetContactsAsync());
        }



        /*OnAppearing()
         * 
         * Fairly resource intensive, redraws the buttons every time the screen is shown with updated contacts.
           Could be improved by Binding the text of the buttons to the ObservableCollection rather than hard coding them,
           But need to figure out how to do this programatically CreateContactButton() */
        public void OnAppearing()
        {
            UpdateContacts();
            CreateAndShowGrid();
        }
    }
}