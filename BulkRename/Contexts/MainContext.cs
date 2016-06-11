using BulkRename.Components;
using BulkRename.Components.IO;
using BulkRename.Extensions;
using BulkRename.ViewModels;
using System;
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

        public void OpenHomeDirectory()
        {
            ViewModel.Path = _directorySearch.GetHomeDir();
        }

        public ActionResult ListFiles() =>
            InDirectory(path =>
                WithRegex(regex =>
                {
                    var fileNames = _directorySearch.ListFiles(path);

                    ViewModel.SourceItems =
                        string.IsNullOrWhiteSpace(ViewModel.Filter)
                            ? fileNames.ToList()
                            : _filter.Filter(regex, fileNames).ToList();

                    ViewModel.TargetItems =
                        string.IsNullOrWhiteSpace(ViewModel.Template)
                            ? ViewModel.SourceItems.ToList()
                            : _renamer.Rename(ViewModel.Template, ViewModel.SourceItems).ToList();

                    return ActionResult.Ok;
                }));

        public ActionResult ListOnlyTemplates() =>
            InDirectory(path =>
                WithRegex(regex =>
                {
                    ViewModel.TargetItems =
                        string.IsNullOrWhiteSpace(ViewModel.Template)
                            ? ViewModel.SourceItems.ToList()
                            : _renamer.Rename(ViewModel.Template, ViewModel.SourceItems).ToList();

                    return ActionResult.Ok;
                }));

        public ActionResult Autocomplete(bool isDown)
        {
            ViewModel.Path = _autocomplete.Autocomplete(ViewModel.Path, isDown);
            return ActionResult.Ok;
        }

        public ActionResult RenameFiles() =>
            InDirectory(path =>
                _fileRename.RenameFiles(path, ViewModel.SourceItems, ViewModel.TargetItems) == FileRenameResult.Ok ?
                    ActionResult.Ok :
                    ActionResult.Error("File names are in conflict, operation cancelled"));

        public ActionResult CancelLastRename() =>
            _fileRename.Cancel() == FileRenameCancelResult.Ok ?
            ActionResult.Ok :
            ActionResult.Error("Cancel operation failed");

        private ActionResult InDirectory(Func<string, ActionResult> action)
        {
            var path = ViewModel.Path;
            if (_directorySearch.DirectoryExists(path))
            {
                return action(path);
            }
            else
            {
                return ActionResult.Error($"Path not exists: {path}");
            }
        }

        private ActionResult WithRegex(Func<Regex, ActionResult> action)
        {
            Regex regex = ViewModel.Filter.TryParseRegex();
            if (regex != null)
            {
                return action(regex);
            }
            else
            {
                return ActionResult.Error($"Incorrect regular expression: {ViewModel.Filter}");
            }
        }
    }
}
