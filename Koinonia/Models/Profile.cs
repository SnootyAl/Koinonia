using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Koinonia.Models
{
    public class Profile
    {
        [PrimaryKey]
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImageURL { get; set; }
        public string PhoneNumber { get; set; }
    }
}
