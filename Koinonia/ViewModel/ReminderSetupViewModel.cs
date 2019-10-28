using MvvmHelpers;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Koinonia.ViewModel
{
    public class ReminderSetupViewModel: BaseViewModel
    {
        public ReminderSetupViewModel()
        {
            SaveReminderCommand = new Command(() =>
            {
                var stringDate = SelectedReminderDate.ToString("dd/MM/yyyy HH:mm:ss");
                var reminderDateTime = DateTime.ParseExact(stringDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture) + SelectedReminderTime;
                CrossLocalNotifications.Current.Show(EventName, EventDescription, GenerateRandomId(), reminderDateTime);
                EventName = String.Empty;
                EventDescription = String.Empty;
            });
            MinimumDate = DateTime.Now;
            SelectedReminderDate = DateTime.Today;
        }

        public string _eventName;
        public string _eventDescription;
        public DateTime _minimumDate;
        public DateTime _selectedReminderDate;
        public TimeSpan _selectedReminderTime;
        public ICommand SaveReminderCommand { get; }

        public string EventName
        {
            get => _eventName;
            set
            {
                _eventName = value;
                OnPropertyChanged("EventName");
            }
        }

        public string EventDescription
        {
            get => _eventDescription;
            set
            {
                _eventDescription = value;
                OnPropertyChanged("EventDescription");
            }
        }

        public DateTime MinimumDate
        {
            get => _minimumDate;
            set
            {
                _minimumDate = value;
                OnPropertyChanged("MinimumDate");
            }
        }

        public DateTime SelectedReminderDate
        {
            get => _selectedReminderDate;
            set
            {
                _selectedReminderDate = value;
                OnPropertyChanged("SelectedReminderDate");
            }
        }

        public TimeSpan SelectedReminderTime
        {
            get => _selectedReminderTime;
            set
            {
                _selectedReminderTime = value;
                OnPropertyChanged("SelectedReminderTime");
            }
        }

        public int GenerateRandomId()
        {
            var random = new Random();
            return random.Next(100, 999);
        } 
    }
}
