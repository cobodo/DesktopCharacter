using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Database;
using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;
using DesktopCharacter.Model.Service.Twitter;
using DesktopCharacter.View.Dialog;
using DesktopCharacter.ViewModel.Dialog;
using Livet;
using Livet.Commands;
using Livet.Messaging;

namespace DesktopCharacter.ViewModel.SettingTab
{
    class TwitterSettingViewModel : Livet.ViewModel, SettingViewModelBase
    {
        private ViewModelCommand _createAccount;
        public ViewModelCommand CreateAccount
        {
            get
            {
                if (_createAccount == null)
                {
                    _createAccount = new ViewModelCommand(() =>
                    {
                        var vm = new TwitterSignInViewModel((TwitterUser resultUser) =>
                        {
                            if (TwitterUsers.Any(user => resultUser.UserId == user.UserId)) return;
                            TwitterUsers.Add(resultUser);
                        });
                        Messenger.Raise(new TransitionMessage(typeof(TwitterSignInDialog), vm, TransitionMode.Modal, "SignIn"));
                    });
                }
                return _createAccount;
            }
        }

        private ViewModelCommand _deleteAccount;
        public ViewModelCommand DeleteAccount => _deleteAccount ??
            (_deleteAccount = new ViewModelCommand(() =>
            {
                _twitterUsers.Remove(_selectedTwitterUser);
            }));


        private ObservableCollection<TwitterUser> _twitterUsers;
        public ObservableCollection<TwitterUser> TwitterUsers
        {
            get
            {
                return _twitterUsers;
            }
            set
            {
                _twitterUsers = value;
                this.RaisePropertyChanged("TwitterUsers");
            }
        }

        private TwitterUser _selectedTwitterUser;

        public TwitterUser SelectedTwitterUser
        {
            get { return _selectedTwitterUser; }
            set
            {
                _selectedTwitterUser = value;
                this.RaisePropertyChanged("SelectedTwitterUser");
            }
        }

        private readonly List<TwitterUser> oldTwitterUsersList;

        public TwitterSettingViewModel()
        {
            var twitterRepository = ServiceLocator.Instance.GetInstance<TwitterRepository>();
            var twitterService = ServiceLocator.Instance.GetInstance<TwitterService>();

            TwitterUsers = new ObservableCollection<TwitterUser>();
            var users = twitterRepository.FindAll();
            oldTwitterUsersList = users;
            
            users.ForEach(user =>
            {
                var findById = twitterService.FindById(user.UserId);
                //おそらく無いと思うが、未認証のユーザーがいれば一度認証してスクリーンネームを取得する
                if (findById == null)
                {
                    Console.WriteLine(@"User not found: " + user.UserId);
                    var twitter = new Twitter(user);
                    twitter.Initialize();
                    findById = twitter;
                }
                TwitterUsers.Add(findById.TwitterUser);
            });
        }

        public void OnClose()
        {
            //現在の状態を保存
            var twitterRepository = ServiceLocator.Instance.GetInstance<TwitterRepository>();
            twitterRepository.Save(TwitterUsers.ToList());

            //差分からアカウントを削除
            var diff = new List<TwitterUser>(oldTwitterUsersList);
            foreach (var twitterUser in TwitterUsers)
            {
                diff.Remove(twitterUser);
            }
            twitterRepository.Delete(diff);
        }
    }
}
