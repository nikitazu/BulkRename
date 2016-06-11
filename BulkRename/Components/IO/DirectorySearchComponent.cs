using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulkRename.Components.IO
{
    public class DirectorySearchComponent
    {
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public string GetHomeDir() =>
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public IEnumerable<string> ListFiles(string path)
        {
            try
            {
                return Directory.Exists(path) ?
                new DirectoryInfo(path).GetFiles().Select(f => f.Name) :
                new List<string>();
            }
            catch (UnauthorizedAccessException)
            {
                return new List<string>();
            }
        }

        public IEnumerable<DirectoryInfo> ListDirs(string path, string startsWith)
        {
            try
            {
                return Directory.Exists(path) ?
                    new DirectoryInfo(path).GetDirectories(startsWith + "*") :
                    new DirectoryInfo[] { };
            }
            catch (UnauthorizedAccessException)
            {
                return new DirectoryInfo[] { };
            }
        }

        public string GetParentDirPath(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public string GetDirName(string path)
        {
            return Path.GetFileName(path);
        }
    }
}
