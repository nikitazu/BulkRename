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

        [TestMethod]
        public void ListDirectoriesGetsDirectoriesByMask()
        {
            Directory.CreateDirectory("foo");
            Directory.CreateDirectory("foo\\dir1");
            Directory.CreateDirectory("foo\\dir2");
            var dirs = _search.ListDirs("foo", "di").ToList();
            Assert.AreEqual(2, dirs.Count);
            Assert.AreEqual("dir1", dirs[0]);
            Assert.AreEqual("dir2", dirs[1]);
        }

        [TestMethod]
        public void GetParentDirectoryPathShouldReturnParentDirectoryPath()
        {
            Assert.AreEqual("C:\\foo\\bar", _search.GetParentDirPath("C:\\foo\\bar\\yob"));
        }

        [TestMethod]
        public void GetDirectoryNameShouldReturnDirectoryName()
        {
            Assert.AreEqual("yob", _search.GetDirName("C:\\foo\\bar\\yob"));
        }
    }
}
