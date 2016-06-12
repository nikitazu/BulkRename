using BulkRename.Contexts;
using BulkRename.Views.Base;
using System.Windows;

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
    }
}
