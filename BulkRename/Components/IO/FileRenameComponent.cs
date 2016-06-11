using System;
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

    public enum FileRenameCancelResult
    {
        Ok,
        Failed,
        NothingToCancel
    }

    public class FileRenameComponent
    {
        private string _lastPath;
        private List<string> _lastSourceItems;
        private List<string> _lastTargetItems;

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
                _lastPath = path;
                _lastSourceItems = sourceItems.ToList();
                _lastTargetItems = targetItems.ToList();
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

        public FileRenameCancelResult Cancel()
        {
            if (!CanCancel())
            {
                return FileRenameCancelResult.NothingToCancel;
            }

            var result = RenameFiles(_lastPath, _lastTargetItems, _lastSourceItems);
            _lastPath = null;
            _lastSourceItems = null;
            _lastTargetItems = null;

            return result == FileRenameResult.Ok ? FileRenameCancelResult.Ok : FileRenameCancelResult.Failed;
        }

        public bool CanCancel() =>
            _lastPath != null && _lastTargetItems != null && _lastSourceItems != null;
    }
}
