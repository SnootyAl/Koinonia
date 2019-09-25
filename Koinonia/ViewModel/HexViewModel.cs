﻿using Koinonia.HexLayouts;
using Koinonia.Models;
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
        public ICommand helloWorld { get; private set; }
        public int hexColumns, hexRows;

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




        public HexViewModel(IPageService pageService)
        {
            _pageService = pageService;
            helloWorld = new Command(HelloWorld);
            hexRows = 7;
            hexColumns = 7;
            ScreenHeight = 500;
            ScreenWidth = 500;
            SetContactCollection();
            //CreateAndShowGrid();
            
        }

        public async void SetContactCollection()
        {
            Contacts = new ObservableCollection<Contact>(await App.Database.GetContactsAsync());
            FilteredContacts = Contacts;
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
                RowCount = hexRows,
                ColumnCount = hexColumns,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.FromHex("#17A7B2"),

            };

            int contactIndex = 0;

            for (int i = 0; i < hexRows; i++)
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
            tempButton.SetBinding(Button.CommandProperty, new Binding("helloWorld"));
            return tempButton;
        }

        public async void HelloWorld(object param)
        {
            int index = Convert.ToInt32(param);
            await _pageService.DisplayAlert("Hello", index.ToString(), "Ok") ;
        }
    }
}