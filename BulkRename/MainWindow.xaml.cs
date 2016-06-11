using BulkRename.Components;
using BulkRename.Components.IO;
using BulkRename.Contexts;
using BulkRename.ViewModels;
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

            _context.ListFiles();
        }

        private void OnPathTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            ActionResult result = ActionResult.Ok;

            switch (e.Key)
            {
                case Key.Down:
                    _context.Autocomplete(true);
                    result = _context.ListFiles();
                    break;

                case Key.Up:
                    _context.Autocomplete(false);
                    result = _context.ListFiles();
                    break;
            }

            HandleResult(result);
        }

        private void OnPathTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            ActionResult result = ActionResult.Ok;

            switch (e.Key)
            {
                case Key.Enter:
                    result = _context.ListFiles();
                    break;
            }

            HandleResult(result);
        }

        private void OnFilterTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            ActionResult result = ActionResult.Ok;

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                result = _context.ListFiles();
            }

            HandleResult(result);
        }

        private void OnTemplateTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            ActionResult result = ActionResult.Ok;

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                result = _context.ListFiles();
            }

            HandleResult(result);
        }

        private void OnRenameFilesButtonClick(object sender, RoutedEventArgs e)
        {
            ActionResult result = ActionResult.Ok;

            result = _context.RenameFiles();

            HandleResult(result);
        }

        private void HandleResult(ActionResult result)
        {
            if (result.Type == ActionResultType.Error)
            {
                MessageBox.Show(result.ErrorMessage, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
