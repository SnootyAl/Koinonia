using Koinonia.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Koinonia.ViewModel
{
    class ContactInfoViewModel : BaseViewModel
    {

        private readonly IPageService _pageService;
        private Contact _currentContact;
        public Contact CurrentContact
        {
            get { return _currentContact; }
            set
            {
                if (_currentContact == value)
                {
                    return;
                }
                _currentContact = value;
                OnPropertyChanged(nameof(CurrentContact));

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

        public ContactInfoViewModel(IPageService pageService, Contact selectedContact)
        {
            CurrentContact = selectedContact;
            _pageService = pageService;
            EditCommand = new Command(Edit);
        }

        private void Edit()
        {
            EditDisabled = (!EditDisabled);
        }
    }
}
