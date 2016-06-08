using BulkRename.Components.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BulkRename.Tests.Components.IO
{
    [TestClass]
    public class DirectoryAutocompleteComponentTest
    {
        private DirectoryAutocompleteComponent _autocomplete;

        [TestInitialize]
        public void Init()
        {
            _autocomplete = new DirectoryAutocompleteComponent(new DirectorySearchComponent());
            if (Directory.Exists("foo"))
            {
                Directory.Delete("foo", true);
            }
        }

        [TestMethod]
        public void AutocompletePartialInput()
        {
            Directory.CreateDirectory("foo");
            Directory.CreateDirectory("foo\\dir1");
            Directory.CreateDirectory("foo\\dir2");
            Assert.AreEqual("foo\\dir1", _autocomplete.Autocomplete("foo\\di", true));
            Assert.AreEqual("foo\\dir1", _autocomplete.Autocomplete("foo\\di", false));
        }

        [TestMethod]
        public void AutocompleteDown()
        {
            Directory.CreateDirectory("foo");
            Directory.CreateDirectory("foo\\dir1");
            Directory.CreateDirectory("foo\\dir2");
            Assert.AreEqual("foo\\dir2", _autocomplete.Autocomplete("foo\\dir1", true));
        }

        [TestMethod]
        public void AutocompleteUp()
        {
            Directory.CreateDirectory("foo");
            Directory.CreateDirectory("foo\\dir1");
            Directory.CreateDirectory("foo\\dir2");
            Assert.AreEqual("foo\\dir1", _autocomplete.Autocomplete("foo\\dir2", false));
        }

        [TestMethod]
        public void AutocompleteInside()
        {
            Directory.CreateDirectory("foo");
            Directory.CreateDirectory("foo\\dir1");
            Directory.CreateDirectory("foo\\dir2");
            Assert.AreEqual("foo\\dir1", _autocomplete.Autocomplete("foo\\", true));
            Assert.AreEqual("foo\\dir1", _autocomplete.Autocomplete("foo\\", false));
        }
    }
}
