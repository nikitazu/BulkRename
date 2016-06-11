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
            var startIndex = template.IndexOf(Hash);
            var hashIsPresent = startIndex > -1;
            if (hashIsPresent)
            {
                var endIndex = startIndex;
                while (endIndex < template.Length && template[endIndex] == Hash)
                {
                    endIndex += 1;
                }
                var delta = endIndex - startIndex;
                var hashLength = delta == 0 ? 1 : delta;
                var hashes = template.Substring(startIndex, hashLength);
                result = input.Replace(hashes, number.ToString("D" + hashLength));
            }
            return hashIsPresent;
        }
    }
}
