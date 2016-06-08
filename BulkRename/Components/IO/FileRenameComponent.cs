using System.Collections.Generic;
using System.IO;

namespace BulkRename.Components.IO
{
    public class FileRenameComponent
    {
        public void RenameFiles(string path, List<string> sourceItems, List<string> targetItems)
        {
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
        }
    }
}
