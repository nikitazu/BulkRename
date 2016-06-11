using BulkRename.Components;
using BulkRename.Components.IO;
using BulkRename.Contexts;
using BulkRename.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

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
            var ds = new DirectorySearchComponent();
            _context = new MainContext(
                new MainViewModel(),
                new FilterComponent(),
                new RenamerComponent(),
                ds,
                new FileRenameComponent(),
                new DirectoryAutocompleteComponent(ds));
            DataContext = _context.ViewModel;

            _context.OpenHomeDirectory();
            _context.ListFiles();
        }

        private void OnPathTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            HandleResult(ok =>
            {
                switch (e.Key)
                {
                    case Key.Down:
                        _context.Autocomplete(true);
                        return _context.ListFiles();

                    case Key.Up:
                        _context.Autocomplete(false);
                        return _context.ListFiles();

                    default:
                        return ok;
                }
            });
        }

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            HandleResult(ok =>
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        return _context.ListFiles();

                    default:
                        return ok;
                }
            });
        }

        private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            _context.ListFiles();
        }

        private void OnTemplateTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            HandleResult(ok =>
            {
                return _context.ListOnlyTemplates();
            });
        }

        private void OnRenameFilesButtonClick(object sender, RoutedEventArgs e)
        {
            HandleResult(ok =>
            {
                return string.IsNullOrWhiteSpace(_context.ViewModel.Template) ?
                    ActionResult.Error("Template is empty, nothing to rename") :
                    _context.RenameFiles();
            });
        }

        private void OnCancelLastRenameButtonClick(object sender, RoutedEventArgs e)
        {
            HandleResult(ok => _context.CancelLastRename());
        }

        private void HandleResult(Func<ActionResult, ActionResult> action)
        {
            var result = action(ActionResult.Ok);
            if (result.Type == ActionResultType.Error)
            {
                MessageBox.Show(result.ErrorMessage, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
