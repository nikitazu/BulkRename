using BulkRename.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BulkRename.Views
{
    /// <summary>
    /// Interaction logic for OpenFolderDialogView.xaml
    /// </summary>
    public partial class OpenFolderDialogView : Window
    {
        private OpenFolderDialogContext _context;

        public OpenFolderDialogView(Window owner, OpenFolderDialogContext context)
        {
            Owner = owner;
            InitializeComponent();
            _context = context;
            DataContext = _context.ViewModel;
            _context.ListDirectory();
        }

        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OnDirectoriesMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _context.NavigateIntoDirectoryByIndex(((ListView)sender).SelectedIndex);
        }
    }
}
