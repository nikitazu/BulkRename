using System.Windows;
using System.Windows.Input;

namespace BulkRename.Views.Base
{
    public abstract class DialogView : Window
    {
        public DialogView(Window owner = null)
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
