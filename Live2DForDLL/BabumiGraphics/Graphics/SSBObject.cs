using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabumiGraphics.Graphics
{
    public class SSBObject
    {
        /// <summary>
        /// Buffer本体
        /// </summary>
        public uint Buffer { private set; get; }

        /// <summary>
        /// デバイス
        /// </summary>
        private OpenGL Device;

        public SSBObject()
        {
            Device = GraphicsManager.Instance.Device;
        }

        public void Create(int bufferSize)
        {
            uint[] temp = new uint[1];
            Device.GenBuffers(1, temp);
            Device.BindBuffer(OpenGL.GL_SHADER_STORAGE_BUFFER, temp[0]);
            Device.BufferData(OpenGL.GL_SHADER_STORAGE_BUFFER, bufferSize, IntPtr.Zero, OpenGL.GL_DYNAMIC_DRAW);
            Device.BindBuffer(OpenGL.GL_SHADER_STORAGE_BUFFER, 0);
            Buffer = temp[0];
        }

        public void Destory()
        {
            uint[] obj = new uint[1];
            obj[0] = Buffer;
            Device.DeleteBuffers(1, obj);
        }
    }
}
