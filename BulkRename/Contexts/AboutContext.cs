using BulkRename.Components.IPC;
using BulkRename.Components.Net;
using BulkRename.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BulkRename.Contexts
{
    public class AboutContext
    {
        private readonly string NL = Environment.NewLine;

        public AboutViewModel ViewModel { get; }
        private readonly UpdatesComponent _updates;
        private readonly ProcessComponent _process;

        public AboutContext(
            AboutViewModel viewModel,
            UpdatesComponent updates,
            ProcessComponent process)
        {
            ViewModel = viewModel;
            _updates = updates;
            _process = process;
        }

        public async void CheckForUpdates()
        {
            var currentVersion = _updates.CurrentVersion;
            ViewModel.UpdateData = $"Current version is {currentVersion}{NL}Checking for updates...{NL}";
            await _updates.CheckForUpdates(line =>
            {
                if (_updates.ContainsVersionData(line))
                {
                    var version = _updates.ParseVersion(line);
                    var versionUrl = _updates.MakeGetVersionUrl(version);
                    var versionNumber = _updates.VersionToNumber(version);
                    var currentNumber = _updates.VersionToNumber(currentVersion);
                    var comparison = versionNumber > currentNumber ? "+" : versionNumber == currentNumber ? "=" : "-";
                    var downloadableVersion = new DownloadableVersionViewModel(version, versionUrl);
                    ViewModel.DownloadableVersions.Add(downloadableVersion);
                    ViewModel.DownloadableVersions = ViewModel.DownloadableVersions.ToList();
                    ViewModel.UpdateData += $"{comparison} {version} {versionUrl}{NL}";
                }
            });
            ViewModel.UpdateData += "READY" + NL;
        }

        public async void DownloadVersion(Uri uri)
        {
            var path = await _updates.DownloadVersion(uri.ToString());
            _process.OpenFileInNavigator(path);
        }

        public void OpenUrlInBrowser(Uri uri)
        {
            _process.OpenUrlInBrowser(uri.ToString());
        }
    }
}
