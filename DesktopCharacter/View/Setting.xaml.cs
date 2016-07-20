using System;
using System.Windows;
using DesktopCharacter.View.SettingTab;
using DesktopCharacter.ViewModel;
using DesktopCharacter.ViewModel.SettingTab;

namespace DesktopCharacter.View
{
    /// <summary>
    /// Setting.xaml の相互作用ロジック
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var vm = DataContext as SettingViewModel;
            vm.LauncherSetting = LauncehrTab.DataContext as LauncherSettingViewModel;
            vm.CharacterSetting = CharacterTab.DataContext as CharacterSettingViewModel;
            vm.TwitterSetting = TwitterSettingsTab.DataContext as TwitterSettingViewModel;
            vm.CodicSettingTab =  CodicSettingTab.DataContext as CodicSettingTabViewModel;
            
        }
    }
}
