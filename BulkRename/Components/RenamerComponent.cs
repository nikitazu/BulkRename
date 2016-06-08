using System.Collections.Generic;
using System.Linq;

namespace BulkRename.Components
{
    public class RenamerComponent
    {
        private const char Hash = '#';

        public IEnumerable<string> Rename(string template, IEnumerable<string> inputItems)
        {
            return inputItems.Select((item, index) =>
            {
                string result = template;
                while (ReplaceHashWithNumber(result, result, index + 1, out result))
                {
                    //empty
                }
                return result;
            });
        }

        private static bool ReplaceHashWithNumber(string template, string input, int number, out string result)
        {
            result = input;
            var hashStartIndex = template.IndexOf(Hash);
            var hashIsPresent = hashStartIndex > -1;
            if (hashIsPresent)
            {
                var hashEndIndex = hashStartIndex;
                while (hashEndIndex < template.Length && template[++hashEndIndex] == Hash)
                {
                    // empty
                }
                var hashLength = hashEndIndex - hashStartIndex;
                var hashes = template.Substring(hashStartIndex, hashLength);
                result = input.Replace(hashes, number.ToString("D" + hashLength));
            }
            return hashIsPresent;
        }
    }
}
