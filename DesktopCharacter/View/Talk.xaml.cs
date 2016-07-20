using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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
