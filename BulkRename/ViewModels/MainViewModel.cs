using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BulkRename.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _path;
        private string _filter;
        private string _template;
        private List<string> _sourceItems;
        private List<string> _targetItems;

        public string Path
        {
            get { return _path; }
            set
            {
                if (_path != value)
                {
                    _path = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Path)));
                }
            }
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
                }
            }
        }

        public string Template
        {
            get { return _template; }
            set
            {
                if (_template != value)
                {
                    _template = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Template)));
                }
            }
        }

        public List<string> SourceItems
        {
            get { return _sourceItems; }
            set
            {
                if (_sourceItems != value)
                {
                    _sourceItems = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SourceItems)));
                }
            }
        }

        public List<string> TargetItems
        {
            get { return _targetItems; }
            set
            {
                if (_targetItems != value)
                {
                    _targetItems = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TargetItems)));
                }
            }
        }
    }
}
