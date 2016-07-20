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

        public SettingViewModel()
        {
            
        }

        public void ClosedEvent()
        {
            TwitterSetting.OnClose();
            CodicSettingTab.OnClose();

            ServiceLocator.Instance.ClearConfigBaseContext();
        }
    }
}
