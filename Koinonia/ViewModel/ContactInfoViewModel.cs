using Koinonia.Models;
using MvvmHelpers;
using Plugin.Messaging;
using System.Windows.Input;
using Xamarin.Forms;

/// <summary>
/// Shown when a contact is selected in the app to display relevant info about the contact. Also allows
/// editing of the contact, and saves back to the database.
/// </summary>
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

        public ICommand MessageCommand { get; private set; }
        private string messageButtonText = "SMS";
        public string MessageButtonText
        {
            get { return messageButtonText; }
            set
            {
                if (messageButtonText == value)
                {
                    return;
                }
                //Throw some stuff here for a dynamic save/edit button label probably
                messageButtonText = value;
                OnPropertyChanged(nameof(MessageButtonText));
            }
        }

        public ICommand CallCommand { get; private set; }
        private string callButtonText = "Call";
        public string CallButtonText
        {
            get { return callButtonText; }
            set
            {
                if (callButtonText == value)
                {
                    return;
                }
                //Throw some stuff here for a dynamic save/edit button label probably
                callButtonText = value;
                OnPropertyChanged(nameof(CallButtonText));
            }
        }


        public ContactInfoViewModel(IPageService pageService, Contact _contact)
        {
            SelectedContact = _contact;
            _pageService = pageService;
            EditCommand = new Command(Edit);
            MessageCommand = new Command(OpenMessenger);
            CallCommand = new Command(OpenDialer);
        }


        /*Save and edit functionality with if statement. Definitely want to break this out into separate
        functions at some point.*/
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


        //Messenger and Calling found at https://www.c-sharpcorner.com/article/xamarin-forms-messaging-app/
        private void OpenMessenger()
        {
            var smsMessanger = CrossMessaging.Current.SmsMessenger;

            if (smsMessanger.CanSendSms)
            {
                smsMessanger.SendSms(SelectedContact.PhoneNumber, "");
            }
        }

        private void OpenDialer()
        {
            var phoneDial = CrossMessaging.Current.PhoneDialer;

            if (phoneDial.CanMakePhoneCall)
            {
                phoneDial.MakePhoneCall(SelectedContact.PhoneNumber, SelectedContact.FirstName);
            }
        }
    }
}
