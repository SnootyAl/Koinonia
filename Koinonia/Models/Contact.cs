using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Models
{
    public class Contact
    {

        [PrimaryKey, AutoIncrement]
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Status { get; set; }
        public string ImageURL { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        
    }

    
}
