using BulkRename.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BulkRename.Tests.Components
{
    [TestClass]
    public class RenamerComponentTest
    {
        private RenamerComponent _renamer;
        private List<string> _sourceItems;

        [TestInitialize]
        public void Init()
        {
            _renamer = new RenamerComponent();
            _sourceItems = new List<string>()
            {
                "[ololo-subs] oh my goddes [720p] - 01.avi",
                "[ololo-subs] oh my goddes [720p] - 02.avi",
                "[ololo-subs] oh my goddes [720p] - 03.avi",
            };
        }

        [TestMethod]
        public void RenameWithTwoHashesShouldAddTwoDigits()
        {
            var targetItems = _renamer.Rename("oh_my_goddes_##.avi", _sourceItems).ToList();
            Assert.AreEqual("oh_my_goddes_01.avi", targetItems[0]);
            Assert.AreEqual("oh_my_goddes_02.avi", targetItems[1]);
            Assert.AreEqual("oh_my_goddes_03.avi", targetItems[2]);
        }

        [TestMethod]
        public void RenameWithOneHashShouldAddOneDigit()
        {
            var targetItems = _renamer.Rename("oh_my_goddes_#.avi", _sourceItems).ToList();
            Assert.AreEqual("oh_my_goddes_1.avi", targetItems[0]);
            Assert.AreEqual("oh_my_goddes_2.avi", targetItems[1]);
            Assert.AreEqual("oh_my_goddes_3.avi", targetItems[2]);
        }

        [TestMethod]
        public void RenameWithNHashesShouldAddNDigits()
        {
            var targetItems = _renamer.Rename("oh_my_goddes_#####.avi", _sourceItems).ToList();
            Assert.AreEqual("oh_my_goddes_00001.avi", targetItems[0]);
            Assert.AreEqual("oh_my_goddes_00002.avi", targetItems[1]);
            Assert.AreEqual("oh_my_goddes_00003.avi", targetItems[2]);
        }

        [TestMethod]
        public void HashCanBeAtTheEndOfTemplate()
        {
            var targetItems = _renamer.Rename("oh_my_goddes_#", _sourceItems).ToList();
            Assert.AreEqual("oh_my_goddes_1", targetItems[0]);
            Assert.AreEqual("oh_my_goddes_2", targetItems[1]);
            Assert.AreEqual("oh_my_goddes_3", targetItems[2]);
        }

        [TestMethod]
        public void HashCanBeAtTheTemplate()
        {
            var targetItems = _renamer.Rename("#", _sourceItems).ToList();
            Assert.AreEqual("1", targetItems[0]);
            Assert.AreEqual("2", targetItems[1]);
            Assert.AreEqual("3", targetItems[2]);
        }

        [TestMethod]
        public void RenameWithoutHashesShouldNotAddAnything()
        {
            var targetItems = _renamer.Rename("ololo", _sourceItems).ToList();
            Assert.AreEqual("ololo", targetItems[0]);
            Assert.AreEqual("ololo", targetItems[1]);
            Assert.AreEqual("ololo", targetItems[2]);
        }

        [TestMethod]
        public void RenameWithMultipleHashesShouldAddDigitsInMultiplePlaces()
        {
            var targetItems = _renamer.Rename("oh_###_my_goddes_##.avi", _sourceItems).ToList();
            Assert.AreEqual("oh_001_my_goddes_01.avi", targetItems[0]);
            Assert.AreEqual("oh_002_my_goddes_02.avi", targetItems[1]);
            Assert.AreEqual("oh_003_my_goddes_03.avi", targetItems[2]);
        }
    }
}
