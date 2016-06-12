using System.ComponentModel;

namespace BulkRename.ViewModels
{
    public class DownloadableVersionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _version;
        private string _url;

        public string Version
        {
            get { return _version; }
            set
            {
                if (_version != value)
                {
                    _version = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Version)));
                }
            }
        }

        public string Url
        {
            get { return _url; }
            set
            {
                if (_url != value)
                {
                    _url = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Url)));
                }
            }
        }

        public DownloadableVersionViewModel(string version, string url)
        {
            Version = version;
            Url = url;
        }
    }
}
