using System.Linq;
using System.Text.RegularExpressions;
using BulkRename.Components;
using BulkRename.Components.IO;
using BulkRename.ViewModels;

namespace BulkRename.Contexts
{
    public class MainContext
    {
        private readonly FilterComponent _filter = new FilterComponent();
        private readonly RenamerComponent _renamer = new RenamerComponent();
        private readonly DirectorySearchComponent _directorySearch = new DirectorySearchComponent();
        private readonly FileRenameComponent _fileRename = new FileRenameComponent();

        public MainViewModel ViewModel { get; }

        public MainContext(MainViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public void ListFiles()
        {
            if (_directorySearch.DirectoryExists(ViewModel.Path))
            {
                var fileNames = _directorySearch.ListFiles(ViewModel.Path);
                ViewModel.SourceItems =
                    string.IsNullOrWhiteSpace(ViewModel.Filter)
                        ? fileNames.ToList()
                        : _filter.Filter(new Regex(ViewModel.Filter), fileNames).ToList();
                ViewModel.TargetItems =
                    string.IsNullOrWhiteSpace(ViewModel.Template)
                        ? ViewModel.SourceItems.ToList()
                        : _renamer.Rename(ViewModel.Template, ViewModel.SourceItems).ToList();
            }
        }

        public void RenameFiles()
        {
            if (_directorySearch.DirectoryExists(ViewModel.Path))
            {
                _fileRename.RenameFiles(ViewModel.Path, ViewModel.SourceItems, ViewModel.TargetItems);
            }
        }
    }
}
