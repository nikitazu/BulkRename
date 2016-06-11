using BulkRename.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BulkRename.Tests.Extensions
{
    [TestClass]
    public class StringParserRegexTest
    {
        [TestMethod]
        public void TryParseRegextShouldParseCorrectRegex()
        {
            Assert.IsNotNull("foo".TryParseRegex());
        }

        [TestMethod]
        public void TryParseRegextShouldReturnNullForIncorrectRegex()
        {
            Assert.IsNull("*.foo".TryParseRegex());
        }
    }
}
