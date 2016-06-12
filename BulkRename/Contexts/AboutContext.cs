using BulkRename.Components.Net;
using BulkRename.ViewModels;
using System;

namespace BulkRename.Contexts
{
    public class AboutContext
    {
        private readonly string NL = Environment.NewLine;

        public AboutViewModel ViewModel { get; }
        private UpdatesComponent _updates;

        public AboutContext(
            AboutViewModel viewModel,
            UpdatesComponent updates)
        {
            ViewModel = viewModel;
            _updates = updates;
        }

        public async void CheckForUpdates()
        {
            ViewModel.UpdateData = "Checking for updates..." + NL;
            await _updates.CheckForUpdates(line =>
            {
                if (_updates.ContainsVersionData(line))
                {
                    var version = _updates.ParseVersion(line);
                    var getVersion = _updates.MakeGetVersionUrl(version);
                    var versionNumber = _updates.VersionToNumber(version);
                    var currentVersion = UpdatesComponent.CurrentVersion;
                    var currentNumber = _updates.VersionToNumber(currentVersion);
                    var comparison = versionNumber > currentNumber ? "+" : versionNumber == currentNumber ? "=" : "-";
                    ViewModel.UpdateData += $"{comparison} {version} {getVersion}{NL}";
                }
            });
            ViewModel.UpdateData += "READY" + NL;
        }
    }
}
