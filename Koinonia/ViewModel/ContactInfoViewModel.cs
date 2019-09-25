﻿using Koinonia.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Koinonia.ViewModel
{
    public class ContactInfoViewModel : BaseViewModel
    {

        private readonly IPageService _pageService;
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

        private bool _editDisabled = true;
        public bool EditDisabled
        {
            get { return _editDisabled; }
            set
            {
                if (_editDisabled == value)
                {
                    return;
                }
                //Throw some stuff here for a dynamic save/edit button label probably
                _editDisabled = value;
                OnPropertyChanged(nameof(EditDisabled));
            }
        }
        public ICommand EditCommand { get; private set; }

        private string editButtonText = "Edit";
        public string EditButtonText
        {
            get { return editButtonText; }
            set
            {
                if (editButtonText == value)
                {
                    return;
                }
                //Throw some stuff here for a dynamic save/edit button label probably
                editButtonText = value;
                OnPropertyChanged(nameof(EditButtonText));
            }
        }


        public ContactInfoViewModel(IPageService pageService, Contact _contact)
        {
            SelectedContact = _contact;
            _pageService = pageService;
            EditCommand = new Command(Edit);      
            
        }


        /*Save and edit functionality with if statement. Definitely want to break this out into separate
        functions at some point soon.*/
         private async void Edit()
        {
            EditDisabled = (!EditDisabled);

            //'Save' has just been pressed. Save contact info.
            if (EditDisabled)
            {
                EditButtonText = "Edit";
                await App.Database.SaveContactAsync(SelectedContact);
                //MessagingCenter.Send(this, "ContactUpdated", SelectedContact);
                
            }

            //Edit button has just been pressed. Fields are now editable
            else
            {
                EditButtonText = "Save";
            }
        }
    }
}
