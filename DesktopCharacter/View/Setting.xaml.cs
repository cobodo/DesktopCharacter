using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DesktopCharacter.Model.Locator;
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
            vm.TalkSetting = TalkTab.DataContext as TalkSettingViewModel;
            vm.TwitterSetting = TwitterSettingsTab.DataContext as TwitterSettingViewModel;
            vm.CodicSettingTab =  CodicSettingTab.DataContext as CodicSettingTabViewModel;
            
        }
    }
}
