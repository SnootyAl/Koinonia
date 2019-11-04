using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Koinonia.ViewModel;
using MvvmHelpers;
using System.Threading.Tasks;

namespace Nonia.Test
{ 
    [TestClass]
    public class SignUptests: BaseViewModel
    {
       [TestMethod]
       // test adding users first name with a string
        public void Addfirstname()
        {
            var vm = new SignupViewModel(new PageService());

            vm.Profile.FirstName = "John";

            vm.NextButtonCommand.CanExecute(null);

            Assert.AreEqual("John", vm.Profile.FirstName);
            
        }

        // test adding users last name with a string
        [TestMethod]
        public void Addlastname()
        {
            var vm = new SignupViewModel(new PageService());

            vm.Profile.LastName = "Smith";

            vm.NextButtonCommand.CanExecute(null);

            Assert.AreEqual("Smith", vm.Profile.LastName);

        }

        // test adding user email
        [TestMethod]
        public void addemailladress()
        {
            var vm = new SignupViewModel(new PageService());

            vm.Profile.Email = "johnsmith@nonia.com";

            vm.NextButtonCommand.CanExecute(null);

            Assert.AreEqual("johnsmith@nonia.com", vm.Profile.Email);
        }

        // test adding user phone number 
        [TestMethod]
        public void addphonenumber()
        {
            var vm = new SignupViewModel(new PageService());

            vm.Profile.PhoneNumber = "923842834";

            vm.NextButtonCommand.CanExecute(null);

            Assert.AreEqual("923842834", vm.Profile.PhoneNumber);

        }

        // add the users postion
        [TestMethod]
        public void addposition()
        {
            var vm = new SignupViewModel(new PageService());

            vm.Profile.Position = "Developer";

            vm.NextButtonCommand.CanExecute(null);

            Assert.AreEqual("Developer", vm.Profile.Position);
        }

        // add the users location
        [TestMethod]
        public void addlocation()
        {
            var vm = new SignupViewModel(new PageService());

            vm.Profile.Location = "Brisbane";

            vm.NextButtonCommand.CanExecute(null);

            Assert.AreEqual("Brisbane", vm.Profile.Location);
        }

        // add the users Work
        [TestMethod]
        public void addWork()
        {
            var vm = new SignupViewModel(new PageService());

            vm.Profile.Work = "Nonia";

            vm.NextButtonCommand.CanExecute(null);

            Assert.AreEqual("Nonia", vm.Profile.Work);
        }
    }
}
