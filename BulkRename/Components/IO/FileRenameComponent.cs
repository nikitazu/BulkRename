using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulkRename.Components.IO
{
    public enum FileRenameResult
    {
        Ok,
        Conflict
    }

    public class FileRenameComponent
    {
        public FileRenameResult RenameFiles(string path, List<string> sourceItems, List<string> targetItems)
        {
            if (!AreUnique(targetItems))
            {
                return FileRenameResult.Conflict;
            }
            if (targetItems.Count > 0)
            {
                for (int i = 0; i < sourceItems.Count; i++)
                {
                    var sourceFileName = sourceItems[i];
                    var targetFileName = targetItems[i];

                    var dir = new DirectoryInfo(path);
                    File.Move(
                        Path.Combine(dir.FullName, sourceFileName),
                        Path.Combine(dir.FullName, targetFileName));
                }
            }
            return FileRenameResult.Ok;
        }

        private bool AreUnique(IEnumerable<string> targetItems)
        {
            var set = new SortedSet<string>();
            return !targetItems.Any(item =>
            {
                bool contains = set.Contains(item);
                if (!contains) { set.Add(item); }
                return contains;
            });
        }
    }
}
