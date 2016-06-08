using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BulkRename.Components
{
    public class FilterComponent
    {
        public IEnumerable<string> Filter(Regex re, IEnumerable<string> sourceItems)
        {
            return sourceItems.Where(item => re.IsMatch(item));
        }
    }
}
