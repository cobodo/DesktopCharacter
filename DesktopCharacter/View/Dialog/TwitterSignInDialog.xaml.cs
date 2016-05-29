using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CoreTweet;
using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;
using DesktopCharacter.Model.Service.Twitter;

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
