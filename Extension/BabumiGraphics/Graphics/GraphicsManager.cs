using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabumiGraphics.Graphics
{
    public class GraphicsManager
    {
        static public GraphicsManager Instance = new GraphicsManager();

        /// <summary>
        /// デバイス
        /// </summary>
        public OpenGL Device { get; set; }
        /// <summary>
        /// ベンダー情報
        /// </summary>
        public string mVender { get; set; }
        /// <summary>
        /// レンダー情報
        /// </summary>
        public string mRender { get; set; }
        /// <summary>
        /// バージョン情報
        /// </summary>
        public string mVersion { get; set; }

        private RenderTarget mRenderTarget;

        Shader mShader;

        public void Initialize()
        {
            mVender = Device.GetString(OpenGL.GL_VENDOR);
            mRender = Device.GetString(OpenGL.GL_RENDERER);
            mVersion = Device.GetString(OpenGL.GL_VERSION);
        }

        public double GetVersion()
        {
            return double.Parse(Device.GetString(OpenGL.GL_VERSION).Substring(0, 3));
        }

        public void Begin()
        {
            Device.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, mRenderTarget.FrameBufferID);

            uint[] target = new uint[1];
            target[0] = OpenGL.GL_COLOR_ATTACHMENT0_EXT;
            Device.DrawBuffers(1, target);

            Device.ClearColor(1.0f, 1.0f, 1.0f, 0.0f);
            Device.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
            Device.LoadIdentity();
            Device.Viewport(0, 0, (int)mRenderTarget.Width, (int)mRenderTarget.Height);
        }

        public void End()
        {
            Device.Flush();
            Device.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, 0);
        }

        public void Draw()
        {
            //!< TODO
        }

        public void DrawIndexed()
        {
            //!< TODO
        }

        public void Dispatch(uint x, uint y, uint z)
        {
            Device.DispatchCompute( x, y, z );
        }

        public void SetEffect( Shader effect )
        {
            mShader = effect;
            Device.UseProgram(effect.ShaderLink);
        }

        public void SetRenderTarget( RenderTarget renderTarget )
        {
            mRenderTarget = renderTarget;
        }

        public void Uniform(string name, float param)
        {
            Device.Uniform1(Device.GetUniformLocation(mShader.ShaderLink, name), param);
        }

        public void UniformTexture(uint textureID, uint index)
        {
            Device.BindImageTexture(index, textureID, 0, OpenGL.GL_FALSE, 0, OpenGL.GL_READ_ONLY, OpenGL.GL_RGBA8);
        }

        public void UniformBuffer(uint bufferID, uint index, uint type)
        {
            Device.BindBufferBase(type, index, bufferID);
        }

        public IntPtr Map( uint type, uint buffer )
        {
            Device.UnmapBuffer(type);
            Device.BindBuffer(type, buffer);
            return Device.MapBuffer(type, OpenGL.GL_READ_ONLY);
        }

        public void UnMap( uint type )
        {
            Device.UnmapBuffer(type);
        }
    }
}
