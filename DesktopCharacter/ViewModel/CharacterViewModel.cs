using Livet.Commands;
using System.Windows.Media;
using DesktopCharacter.Model.Graphics;

namespace DesktopCharacter.ViewModel
{
    class CharacterViewModel : Livet.ViewModel
    {
        /// <summary>
        /// TalkVMのインスタンス
        /// </summary>
        public TalkViewModel TalkViewModel { private get; set; }
        /// <summary>
        /// スクリーンサイズ
        /// </summary>
        public System.Drawing.Point ScreenSize { private get; set; }
        /// <summary>
        /// モデル描画をまとめたModel層
        /// </summary>
        private Live2DManaged _model = new Live2DManaged();
        /// <summary>
        /// マスコット画像のイメージ
        /// </summary>
        private ImageSource _source;
        public ImageSource Source
        {
            get { return _source; }
            set
            {
                _source = value;
                this.RaisePropertyChanged("Source");
            }
        }

        public CharacterViewModel()
        {

        }

        private ViewModelCommand mDrawCommand;
        public ViewModelCommand DrawCommand
        {
            get
            {
                if (mDrawCommand == null)
                {
                    mDrawCommand = new ViewModelCommand( () =>
                    {
                        Source = _model.Draw();
                    });
                }
                return mDrawCommand;
            }
        }

        private ViewModelCommand mInitializeCommand;
        public ViewModelCommand InitializeCommand
        {
            get
            {
                if (mInitializeCommand == null)
                {
                    mInitializeCommand = new ViewModelCommand(() =>
                    {
                        _model.Initialize(ScreenSize);
                    });
                }
                return mInitializeCommand;
            }
        }
    }
}
