using DesktopCharacter.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Database.Domain;

namespace DesktopCharacter.ViewModel.SettingTab
{
    class CodicSettingTabViewModel : Livet.ViewModel
    {
        private CodicUser _codicUser;

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

        private string _guideTextBox;
        public string GuideTextBox
        {
            get { return _guideTextBox; }
            set { _guideTextBox = value; this.RaisePropertyChanged("GuideTextBox"); }
        }

        private string _casingItem;
        public string CasingItem
        {
            get { return _casingItem; }
            set { _casingItem = value; this.RaisePropertyChanged("ComboxItem"); }
        }

        public CodicSettingTabViewModel()
        {
            var coidcRepository = ServiceLocator.Instance.GetInstance<CodicRepository>();
            _codicUser = coidcRepository.Load();
            GuideTextBox = "Token貼り付けて";
            CasingItem = _codicUser?.Casing ?? "";
            TokenTextBox = _codicUser?.Token ?? "";
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

        public void OnClose()
        {
            if(TokenTextBox != string.Empty)
            {
                var coidcRepository = ServiceLocator.Instance.GetInstance<CodicRepository>();
                coidcRepository.Save(new CodicUser { Token = TokenTextBox, Casing = CasingItem });
            }
        }
    }
}
