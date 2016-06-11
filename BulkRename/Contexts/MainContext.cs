using BulkRename.Components;
using BulkRename.Components.IO;
using BulkRename.Extensions;
using BulkRename.ViewModels;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BulkRename.Contexts
{
    public struct ActionResult
    {
        public ActionResultType Type { get; set; }
        public string ErrorMessage { get; set; }

        public static readonly ActionResult Ok =
            new ActionResult { Type = ActionResultType.Ok };

        public static ActionResult Error(string message)
        {
            return new ActionResult { Type = ActionResultType.Error, ErrorMessage = message };
        }
    }

    public enum ActionResultType
    {
        Ok,
        Error
    }

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

        public ActionResult ListFiles()
        {
            return InDirectory(path =>
            {
                var fileNames = _directorySearch.ListFiles(path);
                Regex regex = ViewModel.Filter.TryParseRegex();
                if (regex == null)
                {
                    return ActionResult.Error($"Incorrect regular expression: {ViewModel.Filter}");
                }

                ViewModel.SourceItems =
                    string.IsNullOrWhiteSpace(ViewModel.Filter)
                        ? fileNames.ToList()
                        : _filter.Filter(regex, fileNames).ToList();
                ViewModel.TargetItems =
                    string.IsNullOrWhiteSpace(ViewModel.Template)
                        ? ViewModel.SourceItems.ToList()
                        : _renamer.Rename(ViewModel.Template, ViewModel.SourceItems).ToList();

                return ActionResult.Ok;
            });
        }

        public ActionResult Autocomplete(bool isDown)
        {
            ViewModel.Path = _autocomplete.Autocomplete(ViewModel.Path, isDown);
            return ActionResult.Ok;
        }

        public ActionResult RenameFiles()
        {
            return InDirectory(path =>
            {
                _fileRename.RenameFiles(path, ViewModel.SourceItems, ViewModel.TargetItems);
                return ActionResult.Ok;
            });
        }

        private ActionResult InDirectory(Func<string, ActionResult> action)
        {
            var path = ViewModel.Path;
            if (_directorySearch.DirectoryExists(path))
            {
                return action(path);
            }
            else
            {
                return ActionResult.Error($"Path not exists: {ViewModel.Path}");
            }
        }
    }
}
