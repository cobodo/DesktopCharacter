using System;
using System.Windows;
using DesktopCharacter.ViewModel.Dialog;

namespace DesktopCharacter.View.Dialog
{
    /// <summary>
    /// Interaction logic for TwitterSignInDialog.xaml
    /// </summary>
    public partial class TwitterSignInDialog : Window
    {

        public TwitterSignInDialog()
        {
            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            var vm = DataContext as TwitterSignInViewModel;
            if (vm != null)
            {
                vm.CloseAction = new Action(Close);
            }
        }

        private void PinCode_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var dataObject = Clipboard.GetDataObject();
            if (dataObject != null && dataObject.GetDataPresent(DataFormats.Text))
            {
                PinCode.Text = (string)dataObject.GetData(DataFormats.Text);
            }
        }
    }
}
