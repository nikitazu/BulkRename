using BulkRename.Components.IO;
using BulkRename.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulkRename.Contexts
{
    public class OpenFolderDialogContext
    {
        public OpenFolderDialogViewModel ViewModel { get; }
        private readonly DirectorySearchComponent _directorySearch;

        public OpenFolderDialogContext(
            OpenFolderDialogViewModel viewModel,
            DirectorySearchComponent directorySearch)
        {
            ViewModel = viewModel;
            _directorySearch = directorySearch;
        }

        public void ListDirectory()
        {
            var dirs = new List<OpenFolderViewModel>();
            var curDir = new DirectoryInfo(ViewModel.Directory);
            if (curDir.Parent == null)
            {
                foreach (var drive in Environment.GetLogicalDrives())
                {
                    dirs.Add(new OpenFolderViewModel(drive, new DirectoryInfo(drive)));
                }
            }
            else
            {
                dirs.Add(new OpenFolderViewModel("..", curDir.Parent));
            }
            dirs.AddRange(_directorySearch.ListDirs(ViewModel.Directory, string.Empty)
                .Select(d => new OpenFolderViewModel(d.Name, d)));
            ViewModel.Directories = dirs;
        }

        public void NavigateIntoDirectoryByIndex(int index)
        {
            var dirs = ViewModel.Directories;
            if (dirs.Count > 0 && index > 0 && index < dirs.Count)
            {
                ViewModel.Directory = dirs[index].Directory.FullName;
                ListDirectory();
            }
            else if (dirs.Count > 0)
            {
                ViewModel.Directory = dirs[0].Directory.FullName;
                ListDirectory();
            }
        }
    }
}
