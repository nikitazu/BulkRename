using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BulkRename.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BulkRename.Tests.Components
{
    [TestClass]
    public class FilterComponentTest
    {
        private FilterComponent _filter;
        private List<string> _sourceItems;
        private Regex _regex;

        [TestInitialize]
        public void Init()
        {
            _filter = new FilterComponent();
            _sourceItems = new List<string>()
            {
                "[ololo-subs] oh my goddes [720p] - 01.avi",
                "[ololo-subs] oh my goddes [720p] - 01.srt",
                "[ololo-subs] oh my goddes [720p] - 02.avi",
                "[ololo-subs] oh my goddes [720p] - 02.srt",
            };
            _regex = new Regex("avi$");
        }

        [TestMethod]
        public void FilterWithRegexFilters()
        {
            var targetItems = _filter.Filter(_regex, _sourceItems).ToList();
            Assert.AreEqual(2, targetItems.Count);
            Assert.AreEqual("[ololo-subs] oh my goddes [720p] - 01.avi", targetItems[0]);
            Assert.AreEqual("[ololo-subs] oh my goddes [720p] - 02.avi", targetItems[1]);
        }
    }
}
