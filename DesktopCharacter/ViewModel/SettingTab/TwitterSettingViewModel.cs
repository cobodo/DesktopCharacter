using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Database;
using DesktopCharacter.Model.Database.Domain;
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

        public ViewModelCommand CreateAccount
        {
            get
            {
                if (_createAccount == null)
                {
                    _createAccount = new ViewModelCommand(OpenCreateAccount);
                }
                return _createAccount;
            }
        }

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

        public TwitterSettingViewModel()
        {
            var twitterRepository = new TwitterRepository();
            TwitterUsers = new ObservableCollection<TwitterUser>();
            twitterRepository.Load().ForEach(user =>
            {
                TwitterUsers.Add(user);
            });
        }

        public void OnClose()
        {
            var twitterRepository = new TwitterRepository();
            twitterRepository.Save(TwitterUsers.ToList());
        }

        public void OpenCreateAccount()
        {
            var twitterSignInDialog = new TwitterSignInDialog();
            var showDialog = twitterSignInDialog.ShowDialog();
            if (showDialog != null && showDialog.Value)
            {
                TwitterUsers.Add(twitterSignInDialog.AuthTwitterUser);
            }
        }
    }
}
