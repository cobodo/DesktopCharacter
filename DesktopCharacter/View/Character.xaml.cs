using DesktopCharacter.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;
using BabumiGraphics.Graphics;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;

namespace DesktopCharacter.View
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Character : Window
    {
        public Character()
        {
            InitializeComponent();
            //!< 保存したPositionを取り出し設定する
            {
                var repo = ServiceLocator.Instance.GetInstance<WindowPositionRepository>();
                var pos = repo.FetchPosition();
                Top = pos.PosY;
                Left = pos.PosX;
            }

            var userVM = this.menuItem.DataContext as ViewModel.Menu.MenuItemViewModel;
            userVM.CharacterVM = this.DataContext as CharacterViewModel;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            var repo = ServiceLocator.Instance.GetInstance<WindowPositionRepository>();
            repo.Save((int)Left, (int)Top);
        }
    }
}
