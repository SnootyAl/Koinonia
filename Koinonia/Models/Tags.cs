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
        // Create a primary key of int type called TagsID
        public int TagsID { get; set; }

        // Create a string called Tagnames
        public string TagNames { get; set; }
    }
}