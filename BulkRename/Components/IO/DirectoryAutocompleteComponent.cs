using System.IO;
using System.Linq;

namespace BulkRename.Components.IO
{
    public class DirectoryAutocompleteComponent
    {
        private readonly DirectorySearchComponent _search;

        public DirectoryAutocompleteComponent(DirectorySearchComponent search)
        {
            _search = search;
        }

        public string Autocomplete(string path, bool isDown)
        {
            var parent = _search.GetParentDirPath(path);
            var mask = _search.GetDirName(path);
            if (_search.DirectoryExists(path))
            {
                if (path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    var suggestion = _search.ListDirs(path, string.Empty).FirstOrDefault();
                    return suggestion != null ? Path.Combine(path, suggestion) : path;
                }
                else
                {
                    var suggestions = _search.ListDirs(parent, string.Empty);
                    bool foundCurrent = false;
                    foreach (var suggestion in isDown ? suggestions : suggestions.Reverse())
                    {
                        if (foundCurrent)
                        {
                            return Path.Combine(parent, suggestion);
                        }
                        foundCurrent = suggestion == mask;
                    }
                    return path;
                }
            }
            else
            {
                var suggestion = _search.ListDirs(parent, mask).FirstOrDefault();
                return suggestion != null ? Path.Combine(parent, suggestion) : path;
            }
        }
    }
}
