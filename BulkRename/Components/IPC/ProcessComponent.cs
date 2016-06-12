using System.Diagnostics;

namespace BulkRename.Components.IPC
{
    public class ProcessComponent
    {
        public void OpenUrlInBrowser(string url)
        {
            Process.Start(url);
        }

        public void OpenFileInNavigator(string filePath)
        {
            Process.Start(filePath);
        }
    }
}
