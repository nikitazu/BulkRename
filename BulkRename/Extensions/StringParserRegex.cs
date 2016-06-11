using System;
using System.Text.RegularExpressions;

namespace BulkRename.Extensions
{
    public static class StringParserRegex
    {
        public static Regex TryParseRegex(this string s)
        {
            try
            {
                return new Regex(s);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
