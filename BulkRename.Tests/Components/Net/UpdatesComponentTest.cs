using BulkRename.Components.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BulkRename.Tests.Components.Net
{
    [TestClass]
    public class UpdatesComponentTest
    {
        private const string _version = "<a href=\"/nikitazu/BulkRename/tree/v0.1-alpha\" class=\"css-truncate\">";
        private UpdatesComponent _updates;

        [TestInitialize]
        public void Init()
        {
            _updates = new UpdatesComponent();
        }

        [TestMethod]
        public void UpdatesComponentContainsVersionDataShouldDetectVersionDataInString()
        {
            Assert.IsTrue(_updates.ContainsVersionData(_version));
        }

        [TestMethod]
        public void UpdatesComponentParseVersionShouldReturnVersion()
        {
            Assert.AreEqual("v0.1-alpha", _updates.ParseVersion(_version));
        }

        [TestMethod]
        public void UpdatesComponentMakeGetVersionUrlShouldReturnUrl()
        {
            Assert.AreEqual(
                "https://github.com/nikitazu/BulkRename/tree/v0.1-alpha",
                _updates.MakeGetVersionUrl("v0.1-alpha"));
        }

        [TestMethod]
        public void UpdatesComponentVersionToNumberShouldReturnNumber()
        {
            Assert.AreEqual(1234567L, _updates.VersionToNumber("v123.4567"), "v123.4567");
            Assert.AreEqual(0000001L, _updates.VersionToNumber("v0.1-alpha"), "v0.1-alpha");
            Assert.AreEqual(0000001L, _updates.VersionToNumber("v0.1-beta"), "v0.1-beta");
            Assert.AreEqual(0000001L, _updates.VersionToNumber("v0.1-rc"), "v0.1-rc");
            Assert.AreEqual(0000001L, _updates.VersionToNumber("v0.1"), "v0.1");
            Assert.AreEqual(0000002L, _updates.VersionToNumber("v0.2"), "v0.2");
            Assert.AreEqual(0000010L, _updates.VersionToNumber("v0.10"), "v0.10");
            Assert.AreEqual(0001000L, _updates.VersionToNumber("v0.1000"), "v0.1000");
            Assert.AreEqual(0010000L, _updates.VersionToNumber("v1.0"), "v1.0");
            Assert.AreEqual(1000050L, _updates.VersionToNumber("v100.50"), "v100.50");
        }
    }
}
