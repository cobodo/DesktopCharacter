using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.ViewModel.SettingTab;

namespace DesktopCharacter.ViewModel
{
    class SettingViewModel : Livet.ViewModel
    {
        public LauncherSettingViewModel LauncherSetting { set; private get; }
        public TalkSettingViewModel TalkSetting { set; private get; }
        public TwitterSettingViewModel TwitterSetting { set; private get; }

        public SettingViewModel()
        {

        }

        public void ClosedEvent()
        {
            TwitterSetting.OnClose();

            ServiceLocator.Instance.ClearConfigBaseContext();
        }
    }
}
