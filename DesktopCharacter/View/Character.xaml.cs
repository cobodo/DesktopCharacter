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
using WPFLive2DOpenGL.Graphics;

namespace DesktopCharacter.View
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Character : Window
    {
        System.Drawing.Point mScreenSize;

        WriteableBitmap mBitmapSource;
        Int32Rect mSourceRect;

        RenderTarget mRenderTarget;
        Shader mComputeShader;
        SSBObject mSSBObject;

        WPFLive2DOpenGL.Live2D.Manager mLive2DManager = new WPFLive2DOpenGL.Live2D.Manager();

        public Character()
        {
            InitializeComponent();
            var userVM = this.menuItem.DataContext as ViewModel.Menu.MenuItemViewModel;
            userVM.CharacterVM = this.DataContext as CharacterViewModel;
            var characterVM = this.DataContext as ViewModel.CharacterViewModel;
            characterVM.mTalkViewModel = this.TalkView.DataContext as ViewModel.TalkViewModel;

            mScreenSize = new System.Drawing.Point { X = (int)this.Width, Y = (int)this.Height };
            mBitmapSource = new WriteableBitmap(mScreenSize.X, mScreenSize.Y, 96, 96, PixelFormats.Bgra32, null);
            mSourceRect = new Int32Rect(0, 0, mScreenSize.X, mScreenSize.Y);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;
            GraphicsManager Manager = GraphicsManager.Instance;
            Manager.Device = args.OpenGL;

            Manager.Begin();
            {
                mLive2DManager.Update(mScreenSize.X, mScreenSize.Y);
            }
            Manager.End();

            Manager.SetEffect(mComputeShader);
            Manager.Uniform("Width", (float)mScreenSize.X);
            Manager.Uniform("Height", (float)mScreenSize.Y);
            Manager.UniformTexture(mRenderTarget.TextureID, 0);
            Manager.UniformBuffer(mSSBObject.Buffer, 1, OpenGL.GL_SHADER_STORAGE_BUFFER);
            Manager.Dispatch((uint)mScreenSize.X / 16, (uint)mScreenSize.Y / 16, 1);
            {
                var stride = this.mScreenSize.Y * 8 * 0.5;
                IntPtr ptr = Manager.Map(OpenGL.GL_SHADER_STORAGE_BUFFER, mSSBObject.Buffer);
                mBitmapSource.WritePixels(
                    mSourceRect,
                    ptr,
                    (int)stride * mScreenSize.Y,
                    (int)stride);
                Manager.UnMap(OpenGL.GL_SHADER_STORAGE_BUFFER);
            }
            this.Image.Source = mBitmapSource;
        }



        /// <summary>
        /// Handles the OpenGLInitialized event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            GraphicsManager.Instance.Device = args.OpenGL;
            mLive2DManager.Load("Res/koharu", "koharu.model.json");

            mRenderTarget = new RenderTarget { Width = (uint)mScreenSize.X, Height = (uint)mScreenSize.Y };
            mRenderTarget.Create();

            mSSBObject = new SSBObject();
            mSSBObject.Create(1000000 * sizeof(uint));

            mComputeShader = new Shader();
            mComputeShader.CreateShader("Res/computeShader.fx", Shader.Type.ComputeShader);
            mComputeShader.Attach();

            GraphicsManager.Instance.SetRenderTarget(mRenderTarget);
        }
    }
}
