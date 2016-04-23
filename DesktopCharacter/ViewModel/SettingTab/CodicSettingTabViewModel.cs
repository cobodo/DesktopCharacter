using DesktopCharacter.Model.Domain;
using DesktopCharacter.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Database.Domain;


namespace DesktopCharacter.ViewModel.SettingTab
{
    class CodicSettingTabViewModel : Livet.ViewModel
    {

        private string _tokenTextBox;
        public string TokenTextBox
        {
            get { return _tokenTextBox; }
            set
            {
                _tokenTextBox = value;
                this.RaisePropertyChanged("TokenTextBox");
            }
        }

        public CodicSettingTabViewModel()
        {
            
        }

        private Livet.Commands.ViewModelCommand _codicPageOpenCommand;
        public Livet.Commands.ViewModelCommand CodicPageOpenCommand
        {
            get
            {
                return _codicPageOpenCommand ?? (_codicPageOpenCommand = new Livet.Commands.ViewModelCommand(() => 
                {
                    System.Diagnostics.Process.Start("https://codic.jp/login");
                }));
            }
        }

        private Livet.Commands.ViewModelCommand _codicTokenRegister;
        public Livet.Commands.ViewModelCommand CodicTokenRegister
        {
            get
            {
                return _codicTokenRegister ?? (_codicTokenRegister = new Livet.Commands.ViewModelCommand(() =>
                {
                    if(TokenTextBox == null) { return; }
                    CodicUser user = new CodicUser
                    {
                        Token = TokenTextBox,
                    };
                    CodicRepository.Instance.Save(user);
                }));
            }
        }
    }
}
