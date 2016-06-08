using BulkRename.Components.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace BulkRename.Tests.Components.IO
{
    [TestClass]
    public class DirectorySearchComponentTest
    {
        private DirectorySearchComponent _search;

        [TestInitialize]
        public void Init()
        {
            _search = new DirectorySearchComponent();

            if (Directory.Exists("foo"))
            {
                Directory.Delete("foo", true);
            }
            if (Directory.Exists("bar"))
            {
                Directory.Delete("bar", true);
            }
        }

        [TestMethod]
        public void DirectoryExistsIfCreated()
        {
            Directory.CreateDirectory("bar");
            Assert.IsTrue(_search.DirectoryExists("bar"));
        }

        [TestMethod]
        public void DirectoryNotExistsIfNotCreated()
        {
            Assert.IsFalse(_search.DirectoryExists("foo"));
        }

        [TestMethod]
        public void ListFilesGetsAllFilesInDirectory()
        {
            Directory.CreateDirectory("foo");
            File.WriteAllText("foo\\file1", "1");
            File.WriteAllText("foo\\file2", "2");
            var files = _search.ListFiles("foo").ToList();
            Assert.AreEqual(2, files.Count);
            Assert.AreEqual("file1", files[0]);
            Assert.AreEqual("file2", files[1]);
        }
    }
}
