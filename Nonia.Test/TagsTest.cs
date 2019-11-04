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
    public class TagsTest: BaseViewModel
    {
       [TestMethod]
       // test adding tag with a name
        public void AddCommandTest()
        {
            var vm = new TagsViewModel(new PageService());

            vm.Tagname.TagNames = "test";

            vm.CreateTags.CanExecute(null);

            Assert.AreEqual("test", vm.Tagname.TagNames);
            
        }

        [TestMethod]
        
        // test adding tag with no name
        public void Addcommandwithnull()
        {
            var vm = new TagsViewModel(new PageService());

            vm.Tagname.TagNames = null;

            vm.CreateTags.CanExecute(null);

           // Assert.AreEqual("", vm.Tagname.TagNames);
            Assert.IsNull(vm.Tagname.TagNames);
            
        }
    }
}
