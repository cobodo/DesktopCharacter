using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;
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

namespace DesktopCharacter.View.Menu
{
    /// <summary>
    /// LauncherMenu.xaml の相互作用ロジック
    /// </summary>
    public partial class LauncherMenu : Window
    {
        public LauncherMenu()
        {
            InitializeComponent();
            //!< 保存したPositionを取り出し設定する
            {
                var repo = ServiceLocator.Instance.GetInstance<WindowPositionRepository>();
                var pos = repo.FetchPosition();
                Top = pos.PosY;
                Left = pos.PosX;
            }
        }
    }
}
