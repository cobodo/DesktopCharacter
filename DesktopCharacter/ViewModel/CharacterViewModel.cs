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
using DesktopCharacter.Util.Messenger.Message;

namespace DesktopCharacter.ViewModel
{
    class CharacterViewModel : Livet.ViewModel
    {
        /// <summary>
        /// 初期化フラグ
        /// </summary>
        private bool _initialized = false;
        /// <summary>
        /// スクリーンサイズ
        /// </summary>
        private System.Drawing.Point _screenSize;
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
            CharacterNotify.Instance.TopMostMessageSubject.Subscribe(TopMostMessageSend);
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
                        //!< キャラクターVMの初期化が終わるまで遅延させる
                        if (!_initialized)
                        {
                            return;
                        }
                        if (!_model.Initialized)
                        {
                            _model.Initialize(_screenSize);
                        }
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
                        //!< ここでモデルの初期化はしない
                        //!< 描画コマンドの実行時にはスクリーンサイズが取得できているのでそちらで行う
                        //!< コマンドとして定義している理由は、ビヘイビアでDeviceの初期化を行っているため
                        return; 
                    });
                }
                return mInitializeCommand;
            }
        }

        public void Initialize()
        {
            //!< 初期化フラグOn
            _initialized = true;
            //!< コンフィグファイルを読み込む
            {
                var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
                var setting = repo.GetConfig();
                //!< Windowsサイズを設定する
                _screenSize = setting.WindowSize;
                //!< Windowの最前面かどうかをコンフィグから設定
                TopMostMessageSend(setting.Topmost);
                //!< 起動時に例外処理をしているので必ずnullではないと思うのだけど...
                if (setting == null)
                {
                    //!< GLのバージョンを表示してアプリケーションを終了する
                    Messenger.Raise(new InformationMessage(
                        "Configファイルを正しく読み込めてない可能性があるため終了します",
                        "Information",
                        MessageBoxImage.Information,
                        "InfoMessage"));
                    //!< アプリケーションを終了する
                    Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
                }
                //!< OpenGLのバージョンチェック
                if (setting.RequiredVersion > GraphicsManager.Instance.GetVersion())
                {
                    //!< GLのバージョンを表示してアプリケーションを終了する
                    Messenger.Raise(new InformationMessage(
                        string.Format("GL_VENDOR: {0} \nGL_RENDERER : {1} \nGL_VERSION : {2} \nOpenGLのバージョンが4.3以下です！\nコンピュートシェーダに対応していないため終了します",
                        GraphicsManager.Instance.mVender,
                        GraphicsManager.Instance.mRender,
                        GraphicsManager.Instance.mVersion),
                        "Error",
                        MessageBoxImage.Error,
                        "InfoMessage"));
                    //!< アプリケーションを終了する
                    Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
                }
            }
        }

        public void TopMostMessageSend( bool topmost )
        {
            //!< Windowの最前面かどうかをコンフィグから設定
            Messenger.Raise(new TopmostMessage("TopmostMessage", topmost));
        }
    }
}
