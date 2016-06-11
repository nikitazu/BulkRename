using System.ComponentModel;
using System.IO;

namespace BulkRename.ViewModels
{
    public class OpenFolderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title;
        private DirectoryInfo _directory;

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

        public DirectoryInfo Directory
        {
            get { return _directory; }
            set
            {
                if (_directory != value)
                {
                    _directory = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Directory)));
                }
            }
        }

        public OpenFolderViewModel(string title, DirectoryInfo directory)
        {
            Title = title;
            Directory = directory;
        }

        public override string ToString() => Title;
    }
}
