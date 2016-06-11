using BulkRename.Components.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace BulkRename.Tests.Components.IO
{
    [TestClass]
    public class FileRenameComponentTest
    {
        private FileRenameComponent _rename;
        private List<string> _source;
        private List<string> _target;
        private List<string> _conflictingTarget;

        [TestInitialize]
        public void Init()
        {
            _rename = new FileRenameComponent();
            _source = new List<string>
            {
                "file1", "file2"
            };
            _target = new List<string>
            {
                "file01", "file02"
            };
            _conflictingTarget = new List<string>
            {
                "file01", "file01"
            };

            if (Directory.Exists("foo"))
            {
                Directory.Delete("foo", true);
            }
        }

        [TestMethod]
        public void FileRenameCanRenameFiles()
        {
            Directory.CreateDirectory("foo");
            File.WriteAllText("foo\\file1", "1");
            File.WriteAllText("foo\\file2", "2");
            var result = _rename.RenameFiles("foo", _source, _target);
            Assert.AreEqual(FileRenameResult.Ok, result);
            Assert.IsTrue(File.Exists("foo\\file01"));
            Assert.IsTrue(File.Exists("foo\\file02"));
            Assert.IsFalse(File.Exists("foo\\file1"));
            Assert.IsFalse(File.Exists("foo\\file2"));
        }

        [TestMethod]
        public void FileRenamePreventsNameCollistion()
        {
            Directory.CreateDirectory("foo");
            File.WriteAllText("foo\\file1", "1");
            File.WriteAllText("foo\\file2", "2");
            Assert.AreEqual(FileRenameResult.Conflict, _rename.RenameFiles("foo", _source, _conflictingTarget));
            Assert.IsTrue(File.Exists("foo\\file1"));
            Assert.IsTrue(File.Exists("foo\\file2"));
            Assert.IsFalse(File.Exists("foo\\file01"));
        }

        [TestMethod]
        public void FileRenameCanBeCancelled()
        {
            Directory.CreateDirectory("foo");
            File.WriteAllText("foo\\file1", "1");
            File.WriteAllText("foo\\file2", "2");
            _rename.RenameFiles("foo", _source, _target);
            Assert.IsTrue(_rename.CanCancel());
            Assert.AreEqual(FileRenameCancelResult.Ok, _rename.Cancel());
            Assert.IsFalse(File.Exists("foo\\file01"));
            Assert.IsFalse(File.Exists("foo\\file02"));
            Assert.IsTrue(File.Exists("foo\\file1"));
            Assert.IsTrue(File.Exists("foo\\file2"));
        }

        [TestMethod]
        public void FileRenameCanNotBeCancelledIfNoRenamePerformed()
        {
            Assert.IsFalse(_rename.CanCancel());
            Assert.AreEqual(FileRenameCancelResult.NothingToCancel, _rename.Cancel());
        }

        [TestMethod]
        public void FileRenameCanNotBeCancelledTwice()
        {
            Directory.CreateDirectory("foo");
            File.WriteAllText("foo\\file1", "1");
            File.WriteAllText("foo\\file2", "2");
            _rename.RenameFiles("foo", _source, _target);
            Assert.IsTrue(_rename.CanCancel());
            Assert.AreEqual(FileRenameCancelResult.Ok, _rename.Cancel());
            Assert.IsFalse(_rename.CanCancel());
            Assert.AreEqual(FileRenameCancelResult.NothingToCancel, _rename.Cancel());
            Assert.IsFalse(File.Exists("foo\\file01"));
            Assert.IsFalse(File.Exists("foo\\file02"));
            Assert.IsTrue(File.Exists("foo\\file1"));
            Assert.IsTrue(File.Exists("foo\\file2"));
        }
    }
}
