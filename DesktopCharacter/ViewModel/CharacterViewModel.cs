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
        /// xamlに渡すための書き込み先ビットマップ
        /// </summary>
        WriteableBitmap mBitmapSource;
        /// <summary>
        /// 書き込み範囲（newの節約のためここでインスタンス化させる）
        /// </summary>
        Int32Rect mSourceRect;
        /// <summary>
        /// レンダリングターゲット（テクスチャーに描画させるため）
        /// </summary>
        RenderTarget mRenderTarget;
        /// <summary>
        /// Texture -> Bitmapのカラー情報
        /// </summary>
        Shader mComputeShader;
        /// <summary>
        /// GLSLで使うBitmapのカラー情報格納先
        /// </summary>
        SSBObject mSSBObject;
        /// <summary>
        /// Live2Dの管理クラス
        /// </summary>
        Manager mLive2DManager = new Manager();
        /// <summary>
        /// デスクトップマスコットを利用するために必要なバージョン
        /// </summary>
        static double RequiredVersion = 4.3;
        /// <summary>
        /// Live2Dのリソースディレクトリパス
        /// </summary>
        static string Live2DResoruceDir = "Res/Live2D";
        /// <summary>
        /// ロードするデータ名
        /// </summary>
        public string LoadDataName { private get; set; }

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
            //!< Live2Dディレクトリにランダムから選出する
            //!< フォルダーがもしない場合はそのこと通知して終了する
            {
                var repo = ServiceLocator.Instance.GetInstance<CharacterDataRepository>();
                var loadDataName = repo.GetDataName();
                if (loadDataName == null)
                {
                    string[] dirs = Directory.GetDirectories(Live2DResoruceDir);
                    if (dirs.Length == 0)
                    {                 //!< GLのバージョンを表示してアプリケーションを終了する
                        MessageBox.Show(string.Format("[ ERROR ]\n{0}のパスにLive2Dのモデルデータが入っていない可能性があります。確認してみてください。", Live2DResoruceDir));
                        //!< アプリケーションを終了する
                        //this.Close();
                    }
                    loadDataName = System.IO.Path.GetFileName(dirs[new System.Random().Next(dirs.Length - 1)]);
                    //!< 次回からこちらをロードする
                    repo.Save(new CharacterData(loadDataName));
                }
                LoadDataName = loadDataName;
            }
        }

        private ViewModelCommand mDrawCommand;
        public ViewModelCommand DrawCommand
        {
            get
            {
                if (mDrawCommand == null)
                {
                    mDrawCommand = new ViewModelCommand(OpenGLDrawCommand);
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
                    mInitializeCommand = new ViewModelCommand(OpenGLInitializeCommand);
                }
                return mInitializeCommand;
            }
        }

        private void OpenGLDrawCommand()
        {
            //!< Debugの時だけバージョンチェックをする
            if (RequiredVersion > GraphicsManager.Instance.GetVersion())
            {
                return; //!< GL4.3以下なので処理をしない
            }

            GraphicsManager Manager = GraphicsManager.Instance;

            Manager.Begin();
            {
                mLive2DManager.Update(ScreenSize.X, ScreenSize.Y);
            }
            Manager.End();

            Manager.SetEffect(mComputeShader);
            Manager.Uniform("Width", (float)ScreenSize.X);
            Manager.Uniform("Height", (float)ScreenSize.Y);
            Manager.UniformTexture(mRenderTarget.TextureID, 0);
            Manager.UniformBuffer(mSSBObject.Buffer, 1, OpenGL.GL_SHADER_STORAGE_BUFFER);
            Manager.Dispatch((uint)ScreenSize.X / 16, (uint)ScreenSize.Y / 16, 1);
            {
                var stride = this.ScreenSize.Y * 8 * 0.5;
                IntPtr ptr = Manager.Map(OpenGL.GL_SHADER_STORAGE_BUFFER, mSSBObject.Buffer);
                mBitmapSource.WritePixels(
                    mSourceRect,
                    ptr,
                    (int)stride * ScreenSize.Y,
                    (int)stride);
                Manager.UnMap(OpenGL.GL_SHADER_STORAGE_BUFFER);
            }
            Source = mBitmapSource;
        }

        private void OpenGLInitializeCommand()
        {
            //!< Debugの時だけバージョンチェックをする
            if (RequiredVersion > GraphicsManager.Instance.GetVersion())
            {
                return; //!< GL4.3以下なので処理をしない
            }
            mBitmapSource = new WriteableBitmap(ScreenSize.X, ScreenSize.Y, 96, 96, PixelFormats.Bgra32, null);
            mSourceRect = new Int32Rect(0, 0, ScreenSize.X, ScreenSize.Y);

            mLive2DManager.Load(Live2DResoruceDir, LoadDataName, string.Format("{0}.model.json", LoadDataName));

            mRenderTarget = new RenderTarget { Width = (uint)ScreenSize.X, Height = (uint)ScreenSize.Y };
            mRenderTarget.Create();

            mSSBObject = new SSBObject();
            mSSBObject.Create(1000000 * sizeof(uint));

            mComputeShader = new Shader();
            mComputeShader.CreateShader("Res/computeShader.fx", Shader.Type.ComputeShader);
            mComputeShader.Attach();

            GraphicsManager.Instance.SetRenderTarget(mRenderTarget);
        }

        /// <summary>
        /// 終了時に呼ばれるイベント
        /// </summary>
        public void ClosedEvent()
        {

        }
    }
}
