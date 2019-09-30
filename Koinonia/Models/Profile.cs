using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

/// <summary>
/// Property Class similar to the Contact Class. Has various fields to be entered by the user.
/// </summary>
namespace Koinonia.Models
{
    public class Profile : INotifyPropertyChanged
    {

        private int _ContactID;
        [PrimaryKey]
        public int ContactID
        {
            get { return _ContactID; }
            set
            {
                _ContactID = value;
                OnPropertyChanged();
            }
        }
        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImageURL { get; set; }
        public string PhoneNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
