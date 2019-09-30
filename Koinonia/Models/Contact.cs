using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

/// <summary>
/// Contact Class containing different possible fields. AutoIncrementing ContactID for use with Database.
/// </summary>
namespace Koinonia.Models
{
    public class Contact : INotifyPropertyChanged
    {

        [PrimaryKey, AutoIncrement]
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; } 
        public string Status { get; set; }
        public string ImageURL { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    
}
