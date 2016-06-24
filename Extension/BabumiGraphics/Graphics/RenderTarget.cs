using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabumiGraphics.Graphics
{
    public class RenderTarget
    {
        /// <summary>
        /// DepthBuffer
        /// </summary>
        public uint DepthBufferID { private set; get; }
        /// <summary>
        /// RebderTarget
        /// </summary>
        public uint FrameBufferID { private set; get; }
        /// <summary>
        /// テクスチャー(描画された情報をここに格納される)
        /// </summary>
        public uint TextureID { private set; get; }
        /// <summary>
        /// レンダーターゲット幅
        /// </summary>
        public uint Width { set; get; }
        /// <summary>
        /// レンダーターゲット高さ
        /// </summary>
        public uint Height { set; get; }
        /// <summary>
        /// デバイス
        /// </summary>
        private OpenGL Device;

        public RenderTarget()
        {
            Device = GraphicsManager.Instance.Device;
        }

        public void Create()
        {
            uint[] texture = new uint[1];
            Device.GenTextures(1, texture);
            TextureID = texture[0];
            Device.BindTexture(OpenGL.GL_TEXTURE_2D, TextureID);
            Device.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_CLAMP_TO_EDGE);
            Device.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_CLAMP_TO_EDGE);
            Device.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
            Device.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            Device.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_RGBA8, (int)Width, (int)Height, 0, OpenGL.GL_RGBA, OpenGL.GL_UNSIGNED_BYTE, null);

            uint[] frameID = new uint[1];
            Device.GenFramebuffersEXT(1, frameID);
            FrameBufferID = frameID[0];
            Device.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, FrameBufferID);
            Device.FramebufferTexture2DEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_TEXTURE_2D, TextureID, 0);

            uint[] dephtID = new uint[1];
            Device.GenRenderbuffersEXT(1, dephtID);
            DepthBufferID = dephtID[0];
            Device.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, DepthBufferID);
            Device.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER_EXT, OpenGL.GL_DEPTH_COMPONENT24, (int)Width, (int)Height);

            Device.FramebufferRenderbufferEXT(
                OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT,
                OpenGL.GL_RENDERBUFFER, DepthBufferID);

            Device.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, 0);
        }

        public void Destory()
        {
            uint[] frameBuffer = new uint[1];
            frameBuffer[0] = FrameBufferID;
            Device.DeleteBuffers(1, frameBuffer);

            uint[] depthBuffer = new uint[1];
            depthBuffer[0] = DepthBufferID;
            Device.DeleteBuffers(1, depthBuffer);

            uint[] texture = new uint[1];
            texture[0] = TextureID;
            Device.DeleteTextures( 1, texture );
        }
    }
}
