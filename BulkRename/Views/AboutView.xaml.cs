using BulkRename.Contexts;
using BulkRename.Views.Base;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace BulkRename.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : DialogView
    {
        private AboutContext _context;

        public AboutView(Window owner, AboutContext context) : base(owner)
        {
            InitializeComponent();
            _context = context;
            DataContext = _context.ViewModel;
            _context.CheckForUpdates();
        }

        private void OnOpenInBrowserVersionRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            _context.OpenUrlInBrowser(e.Uri);
        }

        private void OnDownloadVersionRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ((Hyperlink)sender).IsEnabled = false;
            _context.DownloadVersion(e.Uri);
        }
    }
}
