using DesktopCharacter.ViewModel;
using SharpGL;
using SharpGL.SceneGraph;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BabumiGraphics.Graphics;
using BabumiGraphics.Live2D;
using BabumiGraphics;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;
using DesktopCharacter.Model.Database.Domain;
using System.IO;

namespace DesktopCharacter.View
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Character : Window
    {
        /// <summary>
        /// デスクトップマスコットを利用するために必要なバージョン
        /// </summary>
        static double RequiredVersion = 4.3;

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
            var characterVM = this.DataContext as ViewModel.CharacterViewModel;
            characterVM.ScreenSize = new System.Drawing.Point((int)this.Width, (int)this.Height);
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

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //!< Debugの時だけバージョンチェックをする
            if (RequiredVersion > GraphicsManager.Instance.GetVersion())
            {
                //!< GLのバージョンを表示してアプリケーションを終了する
                MessageBox.Show(string.Format(
                    "[ ERROR ]\nGL_VENDOR: {0} \nGL_RENDERER : {1} \nGL_VERSION : {2} \nOpenGLのバージョンが4.3以下です！コンピュートシェーダに対応していないため終了します",
                    GraphicsManager.Instance.mVender,
                    GraphicsManager.Instance.mRender,
                    GraphicsManager.Instance.mVersion));
                //!< アプリケーションを終了する
                this.Close();
            }
        }
    }
}
