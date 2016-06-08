using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulkRename.Components.IO
{
    public class DirectorySearchComponent
    {
        public IEnumerable<string> ListFiles(string path)
        {
            if (Directory.Exists(path))
            {
                var dir = new DirectoryInfo(path);
                var fileNames = dir.GetFiles().Select(f => f.Name);
                return fileNames;
            }
            return new List<string>();
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
