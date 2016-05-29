using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreTweet;
using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;
using DesktopCharacter.Model.Service.Twitter;
using Livet.Commands;

namespace DesktopCharacter.ViewModel.Dialog
{
    class TwitterSignInViewModel : Livet.ViewModel
    {
        public string PinCode { get; set; } = "";
        public string Message { get; set; } = "";
        public bool IsProcessing { get; private set; }
        public ViewModelCommand SubmitCommand { get; private set; }

        private readonly OAuth.OAuthSession _oAuthSession;
        public TwitterUser AuthTwitterUser { get; private set; }


        public TwitterSignInViewModel()
        {
            SubmitCommand = new ViewModelCommand(OnSubmit);

            _oAuthSession = CoreTweet.OAuth.Authorize(Twitter.ConsumerKey, Twitter.ConsumerSecret);
            System.Diagnostics.Process.Start(_oAuthSession.AuthorizeUri.ToString());
        }

        private async void OnSubmit()
        {
            IsProcessing = true;

            var tokensAsync = _oAuthSession.GetTokensAsync(PinCode);
            try
            {
                await tokensAsync;

                var tokens = tokensAsync.Result;
                if (tokens == null) throw new NullReferenceException("token is null.");

                var twitterUser = new TwitterUser(tokens);
                var twitterRepository = ServiceLocator.Instance.GetInstance<TwitterRepository>();
                twitterRepository.Save(twitterUser);
                AuthTwitterUser = twitterUser;
            }
            catch
            {
                PinCode = "";
                Message= "認証失敗";
            }
            finally
            {
                IsProcessing = false;
            }
        }
    }
}
