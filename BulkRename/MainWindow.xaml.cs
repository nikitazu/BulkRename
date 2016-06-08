using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BulkRename.Components;
using BulkRename.ViewModels;
using Path = System.IO.Path;

namespace BulkRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FilterComponent _filter = new FilterComponent();
        private RenamerComponent _renamer = new RenamerComponent();

        private MainViewModel ViewModel => (MainViewModel) DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel
            {
                Path = @"C:\",
                Filter = "",
                Template = "",
                SourceItems = new List<string>(),
                TargetItems = new List<string>(),
            };
        }

        private void OnPathTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                ListFiles();
            }
        }

        private void OnFilterTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                ListFiles();
            }
        }

        private void OnTemplateTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                ListFiles();
            }
        }

        private void OnRenameFilesButtonClick(object sender, RoutedEventArgs e)
        {
            RenameFiles();
        }

        private void ListFiles()
        {
            if (Directory.Exists(ViewModel.Path))
            {
                var dir = new DirectoryInfo(ViewModel.Path);
                var fileNames = dir.GetFiles().Select(f => f.Name);
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

        private void RenameFiles()
        {
            if (Directory.Exists(ViewModel.Path) && ViewModel.TargetItems.Count > 0)
            {
                for (int i = 0; i < ViewModel.SourceItems.Count; i++)
                {
                    var sourceFileName = ViewModel.SourceItems[i];
                    var targetFileName = ViewModel.TargetItems[i];

                    var dir = new DirectoryInfo(ViewModel.Path);
                    File.Move(
                        Path.Combine(dir.FullName, sourceFileName),
                        Path.Combine(dir.FullName, targetFileName));
                }
            }
        }
    }
}
