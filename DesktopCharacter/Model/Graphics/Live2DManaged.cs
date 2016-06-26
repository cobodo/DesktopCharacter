using BabumiGraphics.Graphics;
using BabumiGraphics.Live2D;
using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;
using SharpGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopCharacter.Model.Graphics
{
    class Live2DManaged
    {
        /// <summary>
        /// スクリーンサイズ
        /// </summary>
        private System.Drawing.Point _screenSize;
        /// <summary>
        /// xamlに渡すための書き込み先ビットマップ
        /// </summary>
        private WriteableBitmap _bitmapSource;
        /// <summary>
        /// 書き込み範囲（newの節約のためここでインスタンス化させる）
        /// </summary>
        private Int32Rect _sourceRect;
        /// <summary>
        /// レンダリングターゲット（テクスチャーに描画させるため）
        /// </summary>
        private RenderTarget _renderTarget;
        /// <summary>
        /// Texture -> Bitmapのカラー情報
        /// </summary>
        private Shader _computeShader;
        /// <summary>
        /// GLSLで使うBitmapのカラー情報格納先
        /// </summary>
        private SSBObject _sssbObject;
        /// <summary>
        /// Live2Dの管理クラス
        /// </summary>
        private Manager _live2DManager = new Manager();
        /// <summary>
        /// ばぶみのコンフィグ
        /// </summary>
        private BabumiConfig _babumiConfig;

        public Live2DManaged()
        {
            CharacterPropertyNotify.Instance.CharacterLoadSubject.Subscribe(CharacterLoad);
            CharacterPropertyNotify.Instance.SetAnimationSubject.Subscribe(SetAnimation);
            var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
            _babumiConfig = repo.GetConfig();
        }

        /// <summary>
        /// キャラクターのロード
        /// </summary>
        /// <param name="name">キャラクター名</param>
        public void CharacterLoad( string name )
        {
            Object thisLock = new Object();
            lock (thisLock)
            {
                _live2DManager.DeleteModel();
                _live2DManager.Load(_babumiConfig.Live2DResourceDir, _babumiConfig.Live2DModelDir, name, string.Format("{0}.model.json", name));
                var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
                //!< 次回からこちらをロードする
                _babumiConfig.Name = name;
                repo.Save(_babumiConfig);
            }
        }

        /// <summary>
        /// アニメーションセット
        /// </summary>
        /// <param name="name">アニメーションの名前</param>
        public void SetAnimation(string name)
        {
            _live2DManager.SetAnimation(name, true);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="name">キャラクター名</param>
        /// <param name="screenSize">スクリーンサイズ</param>
        public void Initialize(System.Drawing.Point screenSize)
        {
            //!< スクリーンサイズ設定
            _screenSize = screenSize;
            //!< キャラクターをロードする
            CharacterLoad(_babumiConfig.Name);
            //!< Debugの時だけバージョンチェックをする
            if (_babumiConfig.RequiredVersion > GraphicsManager.Instance.GetVersion())
            {
                return; //!< GL4.3以下なので処理をしない
            }
            _bitmapSource = new WriteableBitmap(_screenSize.X, _screenSize.Y, 96, 96, PixelFormats.Bgra32, null);
            _sourceRect = new Int32Rect(0, 0, _screenSize.X, _screenSize.Y);

            _renderTarget = new RenderTarget { Width = (uint)_screenSize.X, Height = (uint)_screenSize.Y };
            _renderTarget.Create();

            _sssbObject = new SSBObject();
            _sssbObject.Create(1000000 * sizeof(uint));

            _computeShader = new Shader();
            _computeShader.CreateShader("Res/computeShader.fx", Shader.Type.ComputeShader);
            _computeShader.Attach();

            GraphicsManager.Instance.SetRenderTarget(_renderTarget);
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <returns></returns>
        public WriteableBitmap Draw()
        {
            //!< Debugの時だけバージョンチェックをする
            if (_babumiConfig.RequiredVersion > GraphicsManager.Instance.GetVersion())
            {
                return null; //!< GL4.3以下なので処理をしない
            }

            GraphicsManager Manager = GraphicsManager.Instance;

            Manager.Begin();
            {
                _live2DManager.Update(_screenSize.X, _screenSize.Y);
            }
            Manager.End();

            Manager.SetEffect(_computeShader);
            Manager.Uniform("Width", (float)_screenSize.X);
            Manager.Uniform("Height", (float)_screenSize.Y);
            Manager.UniformTexture(_renderTarget.TextureID, 0);
            Manager.UniformBuffer(_sssbObject.Buffer, 1, OpenGL.GL_SHADER_STORAGE_BUFFER);
            Manager.Dispatch((uint)_screenSize.X / 16, (uint)_screenSize.Y / 16, 1);
            {
                var stride = this._screenSize.Y * 8 * 0.5;
                IntPtr ptr = Manager.Map(OpenGL.GL_SHADER_STORAGE_BUFFER, _sssbObject.Buffer);
                _bitmapSource.WritePixels(
                    _sourceRect,
                    ptr,
                    (int)stride * _screenSize.Y,
                    (int)stride);
                Manager.UnMap(OpenGL.GL_SHADER_STORAGE_BUFFER);
            }
            return _bitmapSource;
        }
    }
}
