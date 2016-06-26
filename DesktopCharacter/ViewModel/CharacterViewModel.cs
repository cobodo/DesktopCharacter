using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.Commands;
using Livet.Messaging.Windows;
using Reactive.Bindings;
using System.Windows.Input;
using System.Reactive.Linq;
using Livet.Messaging;
using System.Collections.ObjectModel;
using DesktopCharacter.Model.Locator;
using NLog;
using System.Windows.Media.Imaging;
using System.Windows;
using BabumiGraphics.Graphics;
using BabumiGraphics.Live2D;
using System.IO;
using DesktopCharacter.Model.Repository;
using DesktopCharacter.Model.Database.Domain;
using SharpGL;
using System.Windows.Media;
using DesktopCharacter.Model;
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
