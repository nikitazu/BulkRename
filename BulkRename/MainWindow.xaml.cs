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
        }

        private void OnPathTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    _context.ListFiles();
                    break;

                case Key.Down:
                    _context.Autocomplete(true);
                    _context.ListFiles();
                    break;

                case Key.Up:
                    _context.Autocomplete(false);
                    _context.ListFiles();
                    break;
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
