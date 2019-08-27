using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Models
{
    public class Contact
    {
        public string firstName { get; set; }
        public string lastName { get; set; } 
        public string Status { get; set; }
        public string ImageURL { get; set; }

        public void setfirstName(String name)
        {
            this.firstName = name;
        }
    }

    
}
