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
using DesktopCharacter.Model.Service.Twitter;

namespace DesktopCharacter.View.Dialog
{
    /// <summary>
    /// Interaction logic for TwitterSignInDialog.xaml
    /// </summary>
    public partial class TwitterSignInDialog : Window
    {
        private readonly OAuth.OAuthSession _oAuthSession;
        public TwitterUser AuthTwitterUser { get; private set; }

        public TwitterSignInDialog()
        {
            InitializeComponent();
            _oAuthSession = CoreTweet.OAuth.Authorize(Twitter.ConsumerKey, Twitter.ConsumerSecret);
            System.Diagnostics.Process.Start(_oAuthSession.AuthorizeUri.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthButton.IsEnabled = false;
            Message.Text = "認証中";

            //非同期処理にしたい
            var tokens = _oAuthSession.GetTokens(PinCode.Text);
            if (tokens != null)
            {
                var twitterUser = new TwitterUser(tokens);
                var twitterRepository = new TwitterRepository();
                twitterRepository.Save(twitterUser);
                AuthTwitterUser = twitterUser;
                DialogResult = true;
                return;
            }

            AuthButton.IsEnabled = true;
            Message.Text = "認証中";
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
