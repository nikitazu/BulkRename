using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace BulkRename.ViewModels
{
    public class OpenFolderDialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title;
        private string _directory;
        private List<OpenFolderViewModel> _directories;

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

        public string Directory
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

        public List<OpenFolderViewModel> Directories
        {
            get { return _directories; }
            set
            {
                if (_directories != value)
                {
                    _directories = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Directories)));
                }
            }
        }

        public OpenFolderDialogViewModel()
        {
            Title = "";
            Directory = "";
            Directories = new List<OpenFolderViewModel>();
        }
    }
}
