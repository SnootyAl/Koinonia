﻿using Koinonia.HexLayouts;
using Koinonia.Models;
using Koinonia.Views;
using MvvmHelpers;
using System;
using System.Collections;
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
        public ICommand profileSelected { get; private set; }
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

        private Profile _profile { get; set; }
        public Profile UserProfile
        {
            get { return _profile; }
            set
            {
                if (_profile == value)
                {
                    return;
                }
                _profile = value;
                OnPropertyChanged(nameof(UserProfile));
            }
        }


        private double ButtonDiameter;
        private double BoundingFrameLength;
        private double ScalingFactor;

        public ArrayList coords;
        



        public HexViewModel(IPageService pageService)
        {
            _pageService = pageService;
            contactSelected = new Command(ContactSelected);
            profileSelected = new Command(ProfileSelected);
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
            ButtonDiameter = App.screenWidth / 4;
            //Why does Pi always end up in the weirdest spots?
            BoundingHeight = ButtonDiameter * 6.28;
            //BoundingHeight = App.screenHeight;
            BoundingWidth = BoundingHeight;
            OuterFrame = BoundingHeight * 2;
            
        }
        public async void ScreenSetup()
        {
            UserProfile = await App.Database.GetProfileAsync(0);
            Contacts = new CustomObservableCollection<Contact>(await App.Database.GetContactsAsync());            
            FilteredContacts = Contacts;

            //Will not save to database, just in ObserableCollection. Purely to test scaling of HexLayout
            AddDummyContacts(50);
            

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
            //How many contact buttons to place per 'hop' (leg of spiral)
            int hopLength = 1;
            /*hopDirection maps to the following directions:
             0: UP
             1: Right
             2: Down
             3: Left
             
             Will cycle between these values and loop back to 0 to perform a clockwise spiral*/
            int hopDirection = 0;
            int numberOfContacts = Contacts.Count();
            int contactIndex = 0;

            //Create empty hex grid with enough columns/rows to accommodate all contacts with buffer
            DisplayHexGrid = new HexLayout
            {
                RowCount = contactRows + 1,
                ColumnCount = contactColumns + 1,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.FromHex("#17A7B2")
            };


            //Place initial button position at the center(ish) of the HexGrid
            int hex_row = (int)Math.Round((contactRows / 2) + 0.5);
            int hex_column = (int)Math.Round((contactColumns / 2) + 0.5);
            bool breakLoops = false;

            //Place all contacts.
            while (!breakLoops)
            {
                //perform each side twice for a given hopLength, in order to create clockwise spiral outwards.
                for (int i = 0; i < 2; i++)
                {
                    //place a button for every single hop, defined by hopLength. hopLength increments by 1 per two 'right turns'
                    for (int j = 0; j < hopLength; j++)
                    {
                        if (contactIndex == numberOfContacts - 1)
                        {
                            breakLoops = true;
                            break;
                        }

                        switch (hopDirection)
                        {                            
                            case 0:
                                hex_row -= 1;                                
                                break;

                            case 1:
                                hex_column += 1;
                                break;

                            case 2:
                                hex_row += 1;
                                break;

                            case 3:
                                hex_column -= 1;
                                break;                                                        
                        }

                        Button contactButton = CreateContactButton(Contacts[contactIndex]);
                        HexLayout.SetRow(contactButton, hex_row);
                        HexLayout.SetColumn(contactButton, hex_column);
                        DisplayHexGrid.Children.Add(contactButton);
                        contactIndex++;

                    }

                    if (breakLoops)
                    {
                        break;
                    }

                    if (hopDirection == 3)
                    {
                        hopDirection = 0;
                    }
                    else
                    {
                        hopDirection++;
                    }
                    

                    
                }

                

                hopLength++;
                
            }


            /*int contactIndex = 0;
            int hex_row = 0;
            int hex_column = 0;
            Button profileButton = CreateProfileButton(UserProfile);
            HexLayout.SetRow(profileButton, hex_row);
            HexLayout.SetColumn(profileButton, hex_column);
            DisplayHexGrid.Children.Add(profileButton);
            hex_row++;*/


            /*foreach (Contact contact in Contacts)
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
            }*/
        }

        private void ClockwiseSpiral()
        {
            
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

        private Button CreateProfileButton(Profile profile)
        {
            var tempButton = new HexButton
            {
                Text = profile.FirstName,
                BackgroundColor = Color.FromRgb(255, 100, 255),
                HeightRequest = ButtonDiameter,
                WidthRequest = ButtonDiameter,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 75,
                ContactID = profile.ContactID,
                CommandParameter = profile.ContactID
            };
            tempButton.SetBinding(Button.CommandProperty, new Binding("profileSelected"));
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
        public async void ProfileSelected()
        {
            await _pageService.PushAsync(new ProfilePage());
        }

        private async void UpdateContacts()
        {
            Contacts = new ObservableCollection<Contact>(await App.Database.GetContactsAsync());
        }
        private async void UpdateProfile()
        {
            UserProfile = await App.Database.GetProfileAsync(0);
        }



        /*OnAppearing()
         * 
         * Fairly resource intensive, redraws the buttons every time the screen is shown with updated contacts.
           Could be improved by Binding the text of the buttons to the ObservableCollection rather than hard coding them,
           But need to figure out how to do this programatically CreateContactButton() */
        public void OnAppearing()
        {                       
            UpdateProfile();
            UpdateContacts();

            if (Contacts != null && UserProfile != null)
            {
                CreateAndShowGrid();
            }
        }
    }
}