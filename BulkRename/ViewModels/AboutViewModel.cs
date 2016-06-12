using System.Collections.Generic;
using System.ComponentModel;

namespace BulkRename.ViewModels
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title;
        private string _updateData;
        private List<DownloadableVersionViewModel> _downloadableVersions;

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
                }
            }
        }

        public string UpdateData
        {
            get { return _updateData; }
            set
            {
                if (_updateData != value)
                {
                    _updateData = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UpdateData)));
                }
            }
        }

        public List<DownloadableVersionViewModel> DownloadableVersions
        {
            get { return _downloadableVersions; }
            set
            {
                if (_downloadableVersions != value)
                {
                    _downloadableVersions = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DownloadableVersions)));
                }
            }
        }

        public AboutViewModel()
        {
            Title = "";
            UpdateData = "";
            DownloadableVersions = new List<DownloadableVersionViewModel>();
        }
    }
}
