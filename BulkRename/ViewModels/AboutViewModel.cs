using System.ComponentModel;

namespace BulkRename.ViewModels
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title;
        private string _updateData;

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

        public AboutViewModel()
        {
            Title = "";
            UpdateData = "";
        }
    }
}
