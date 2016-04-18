﻿using System;
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

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            var twitterSettingViewModel = TwitterSettingsTab.DataContext as TwitterSettingViewModel;
            twitterSettingViewModel?.OnClose();

            //設定が変更されたのでConfigBaseContextをクリア
            ServiceLocator.Instance.ClearConfigBaseContext();
        }
    }
}
