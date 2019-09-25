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

        private int screenHeight { get; set; }
        public int ScreenHeight
        {
            get { return screenHeight; }
            set
            {
                if (screenHeight == value)
                {
                    return;
                }
                screenHeight = value;
                OnPropertyChanged(nameof(ScreenHeight));
            }
        }

        private int screenWidth { get; set; }
        public int ScreenWidth
        {
            get { return screenWidth; }
            set
            {
                if (screenWidth == value)
                {
                    return;
                }
                screenWidth = value;
                OnPropertyChanged(nameof(ScreenWidth));
            }
        }

        private CustomObservableCollection<Contact> _contacts { get; set; }
        public CustomObservableCollection<Contact> Contacts
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
        private CustomObservableCollection<Contact> _filteredContacts { get; set; }
        public CustomObservableCollection<Contact> FilteredContacts
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

        



        public HexViewModel(IPageService pageService)
        {
            _pageService = pageService;
            contactSelected = new Command(ContactSelected);            
            ScreenHeight = 1000;
            ScreenWidth = 1000;
            ScreenSetup();
            //CreateAndShowGrid();
            
        }
        public async void ScreenSetup()
        {
            Contacts = new CustomObservableCollection<Contact>(await App.Database.GetContactsAsync());
            FilteredContacts = Contacts;
            contactRows = (int)Math.Round(Math.Sqrt(Contacts.Count()) + 0.5);
            contactColumns = contactRows;            
            CreateAndShowGrid();
        }

      


        //Screen dimensions are platform specific. Will need to work on this.
        public async void GetScreenDimensions()
        {
            
        }

        public void CreateAndShowGrid()
        {
            
            DisplayHexGrid = new HexLayout
            {
                RowCount = contactRows + 1,
                ColumnCount = contactColumns + 1,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.FromHex("#17A7B2"),

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





                /*for (int i = 0; i < hexRows; i++)
                {
                    for (int j = 0; j < hexColumns; j++)
                    {

                        //Button contactButton = CreateContactButton(Contacts[contactIndex]);
                        Button contactButton = new Button
                        {
                            Text = (i + "," + j),
                            HeightRequest = 300,
                            WidthRequest = 300
                        };
                        HexLayout.SetRow(contactButton, i);
                        HexLayout.SetColumn(contactButton, j);
                        DisplayHexGrid.Children.Add(contactButton);
                        contactIndex += 1;

                    }
                }*/
            }
        }

        private Button CreateContactButton(Contact contact)
        {
            var tempButton = new HexButton
            {
                Text = contact.FirstName,
                BackgroundColor = Color.FromRgb(255, 255, 255),
                HeightRequest = 150,
                WidthRequest = 150,
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
            

            MessagingCenter.Subscribe<ContactInfoViewModel, Contact>(this, "ContactUpdated", UpdateContact);

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

        /*private async void UpdateContacts(ContactInfoViewModel source)
        {
            Contacts = new ObservableCollection<Contact>(await App.Database.GetContactsAsync());
        }*/
    }
}