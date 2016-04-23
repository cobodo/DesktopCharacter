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
using Livet;
using Livet.Commands;
using Livet.Messaging;

namespace DesktopCharacter.ViewModel.SettingTab
{
    class TwitterSettingViewModel : Livet.ViewModel
    {
        private ViewModelCommand _createAccount;

        public ViewModelCommand CreateAccount => 
            _createAccount ?? (_createAccount = new ViewModelCommand(OpenCreateAccount));

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
            TwitterUsers = new ObservableCollection<TwitterUser>();
            var users = twitterRepository.FindAll();
            oldTwitterUsersList = users;
            
            users.ForEach(user => TwitterUsers.Add(user));
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

        public void OpenCreateAccount()
        {
            var twitterSignInDialog = new TwitterSignInDialog();
            var showDialog = twitterSignInDialog.ShowDialog();

            if (showDialog == null || !showDialog.Value) return;
            var resultUser = twitterSignInDialog.AuthTwitterUser;

            // ReSharper disable once SimplifyLinqExpression
            if (TwitterUsers.Any(user => resultUser.UserId == user.UserId)) return;
            TwitterUsers.Add(resultUser);
        }
    }
}
