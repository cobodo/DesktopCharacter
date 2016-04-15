using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Model.Service.Twitter;
using Livet;

namespace DesktopCharacter.ViewModel.SettingTab
{
    class TwitterSettingViewModel : Livet.ViewModel
    {
        private ObservableCollection<Model> _twitterUsers;

        public ObservableCollection<Model> TwitterUsers
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

        private Model _selectedTwitterUser;

        public Model SelectedTwitterUser
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
            TwitterUsers = new ObservableCollection<Model>();
            twitterRepository.Load().ForEach(user =>
            {
                TwitterUsers.Add(new Model()
                {
                    DisplayName = user.UserId.ToString(),
                    Favorite = user.Filter.Favorite
                });
            });
        }

        public void OnClose()
        {
            Console.WriteLine("");
        }

        internal class Model : NotificationObject
        {
            private string _displayName;

            public string DisplayName
            {
                get { return _displayName; }
                set
                {
                    if(_displayName == value) return;
                    _displayName = value;
                    RaisePropertyChanged("DisplayName");
                }
            }

            private bool _favorite;

            public bool Favorite
            {
                get { return _favorite;}
                set
                {
                    if (_favorite == value)
                    {
                        return;
                    }
                    _favorite = value;
                    RaisePropertyChanged("Favorite");
                }
            }
        }
    }
}
