using Koinonia.Models;
using Koinonia.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koinonia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactInfoPage : ContentPage
    {
        public ContactInfoPage(Contact _contact)
        {
            InitializeComponent();
            BindingContext = new ContactInfoViewModel(new PageService(), _contact);
        }
    }
}