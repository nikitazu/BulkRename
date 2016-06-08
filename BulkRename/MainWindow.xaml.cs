using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using BulkRename.Components;
using BulkRename.Components.IO;
using BulkRename.Contexts;
using BulkRename.ViewModels;
using Path = System.IO.Path;

namespace BulkRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new MainContext(new MainViewModel
            {
                Path = @"C:\",
                Filter = "",
                Template = "",
                SourceItems = new List<string>(),
                TargetItems = new List<string>(),
            });
            DataContext = _context.ViewModel;
        }

        private void OnPathTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                _context.ListFiles();
            }
        }

        private void OnFilterTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                _context.ListFiles();
            }
        }

        private void OnTemplateTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                _context.ListFiles();
            }
        }

        private void OnRenameFilesButtonClick(object sender, RoutedEventArgs e)
        {
            _context.RenameFiles();
        }
    }
}
