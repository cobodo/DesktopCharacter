using System;
using System.ComponentModel;
using System.Windows;
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
        private string _message = "ブラウザで表示されたPINコードを入力";
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        private bool _isProcessing;
        public bool IsProcessing
        {
            get
            {
                return _isProcessing;
            }
            set
            {
                _isProcessing = value;
                RaisePropertyChanged("IsProcessing");
            }
        }

        private ViewModelCommand _submitCommand;
        public ViewModelCommand SubmitCommand => _submitCommand ?? (_submitCommand = new ViewModelCommand(OnSubmit));
        public Action CloseAction { private get; set; }

        private readonly OAuth.OAuthSession _oAuthSession;
        public delegate void Callback(TwitterUser result);
        private readonly Callback _callback;

        public TwitterSignInViewModel(Callback callback)
        {
            if (!(bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                this._callback = callback;
                _oAuthSession = CoreTweet.OAuth.Authorize(Twitter.ConsumerKey, Twitter.ConsumerSecret);
                System.Diagnostics.Process.Start(_oAuthSession.AuthorizeUri.ToString());
            }
        }

        private async void OnSubmit()
        {
            IsProcessing = true;
            this.Message = "認証中";

            var tokensAsync = _oAuthSession.GetTokensAsync(PinCode);
            try
            {
                await tokensAsync;

                var tokens = tokensAsync.Result;
                if (tokens == null) throw new NullReferenceException("token is null.");

                var twitterUser = new TwitterUser(tokens);
                var twitterRepository = ServiceLocator.Instance.GetInstance<TwitterRepository>();
                twitterRepository.Save(twitterUser);
                _callback.Invoke(twitterUser);
                CloseAction();
            }
            catch(TwitterException)
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
