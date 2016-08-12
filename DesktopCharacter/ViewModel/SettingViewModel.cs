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
        public CharacterSettingViewModel CharacterSetting { set; private get; }
        public TwitterSettingViewModel TwitterSetting { set; private get; }
        public CodicSettingTabViewModel CodicSettingTab { set; private get; }
        public SlackSettingViewModel SlackSetting { set; private get; }

        public SettingViewModel()
        {
            
        }

        public void ClosedEvent()
        {
            TwitterSetting.OnClose();
            CodicSettingTab.OnClose();
            CharacterSetting.OnClose();
            SlackSetting.OnClose();
            LauncherSetting.OnClose();

            ServiceLocator.Instance.ClearConfigBaseContext();
        }
    }
}
