using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DesktopCharacter.ViewModel;

namespace DesktopCharacter.View
{
    /// <summary>
    /// Talk.xaml の相互作用ロジック
    /// </summary>
    public partial class Talk : UserControl
    {

        public Talk()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            var storyboard = (Storyboard) TryFindResource("MessageTextFade");
            var talkViewModel = DataContext as TalkViewModel;
            talkViewModel.StoryBoard = storyboard;
        }
    }
}
