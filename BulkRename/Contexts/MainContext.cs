using BulkRename.Components;
using BulkRename.Components.IO;
using BulkRename.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;

namespace BulkRename.Contexts
{
    public class MainContext
    {
        private readonly FilterComponent _filter;
        private readonly RenamerComponent _renamer;
        private readonly DirectorySearchComponent _directorySearch;
        private readonly FileRenameComponent _fileRename;
        private readonly DirectoryAutocompleteComponent _autocomplete;

        public MainViewModel ViewModel { get; }

        public MainContext(
            MainViewModel viewModel,
            FilterComponent filter,
            RenamerComponent renamer,
            DirectorySearchComponent directorySearch,
            FileRenameComponent fileRename,
            DirectoryAutocompleteComponent autocomplete)
        {
            ViewModel = viewModel;
            _filter = filter;
            _renamer = renamer;
            _directorySearch = directorySearch;
            _fileRename = fileRename;
            _autocomplete = autocomplete;
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

        public void Autocomplete(bool isDown)
        {
            ViewModel.Path = _autocomplete.Autocomplete(ViewModel.Path, isDown);
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
