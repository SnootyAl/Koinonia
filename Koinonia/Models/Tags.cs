using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

using Xamarin.Forms;

namespace Koinonia.Models
{
    public class Tags
    {
        [PrimaryKey, AutoIncrement]
        public int TagsID { get; set; }
        public string TagName { get; set; }
    }
}