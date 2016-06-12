using System.Windows;
using System.Windows.Input;

namespace BulkRename.Views.Base
{
    public abstract class DialogView : Window
    {
        internal DialogView()
        {
            // for XAML designer
        }

        public DialogView(Window owner)
        {
            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    DialogResult = false;
                    Close();
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }
    }
}
