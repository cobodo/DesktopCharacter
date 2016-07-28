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
        /// 初期化済みかどうか
        /// </summary>
        public bool Initialized { get; private set; } = false;
        /// <summary>
        /// スクリーンサイズ
        /// </summary>
        private Util.Math.Point _screenSize;
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
            CharacterNotify.Instance.CharacterLoadSubject.Subscribe(CharacterLoad);
            CharacterNotify.Instance.SetAnimationSubject.Subscribe(SetAnimation);
            var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
            _babumiConfig = repo.GetConfig();
        }

        /// <summary>
        /// キャラクターのロード
        /// </summary>
        /// <param name="name">キャラクター名</param>
        public void CharacterLoad( string modelJsonPath )
        {
            _live2DManager.DeleteModel();
            _live2DManager.Load(modelJsonPath);
            var repo = ServiceLocator.Instance.GetInstance<BabumiConfigRepository>();
        }

        /// <summary>
        /// アニメーションセット
        /// </summary>
        /// <param name="name">アニメーションの名前(完全一致しなくてもよい、空文字の場合完全ランダム)</param>
        public void SetAnimation(string name)
        {
            _live2DManager.SetAnimation(name, true);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="name">キャラクター名</param>
        /// <param name="screenSize">スクリーンサイズ</param>
        public void Initialize(Util.Math.Point screenSize)
        {
            //!< 初期化
            Initialized = true;
            //!< スクリーンサイズ設定
            _screenSize = screenSize;
            //!< キャラクターをロードする
            CharacterLoad(_babumiConfig.ModelJsonPath);
            //!< Debugの時だけバージョンチェックをする
            if (_babumiConfig.RequiredVersion > GraphicsManager.Instance.GetVersion())
            {
                return; //!< GL4.3以下なので処理をしない
            }
            _bitmapSource = new WriteableBitmap((int)_screenSize.X, (int)_screenSize.Y, 96, 96, PixelFormats.Bgra32, null);
            _sourceRect = new Int32Rect(0, 0, (int)_screenSize.X - 20, (int)_screenSize.Y - 20);

            _renderTarget = new RenderTarget { Width = (uint)_screenSize.X, Height = (uint)_screenSize.Y };
            _renderTarget.Create();

            _sssbObject = new SSBObject();
            _sssbObject.Create((int)_screenSize.X * (int)_screenSize.Y * sizeof(uint));

            _computeShader = new Shader();
            _computeShader.CreateShader("Res/computeShader.fx", Shader.Type.ComputeShader);
            _computeShader.Attach();

            GraphicsManager.Instance.SetRenderTarget(_renderTarget);
        }

        /// <summary>
        /// グラフィックスオブジェクト削除
        /// 再初期化ができる
        /// </summary>
        public void Destory()
        {
            Initialized = false;
            _sssbObject?.Destory();
            _renderTarget?.Destory();
            _computeShader?.Destory();
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
                _live2DManager.Update((int)_screenSize.X, (int)_screenSize.Y);
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
                    (int)stride * (int)_screenSize.Y,
                    (int)stride);
                Manager.UnMap(OpenGL.GL_SHADER_STORAGE_BUFFER);
            }
            return _bitmapSource;
        }
    }
}
