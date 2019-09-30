using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
/// <summary>
/// HexButton implements Button, while adding ContactID as a parameter. Turns out its uneccesary but its referenced
/// at the moment, and dont have the time to delete it and potentially deal with the bugs that arise :(
/// </summary>
namespace Koinonia.Models
{
    class HexButton : Button
    {
        public int ContactID { get; set; }
    }
}
