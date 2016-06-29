using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Live2DWrap;
using System.Drawing;
using BabumiGraphics.Graphics;
using System.IO;
using System.Runtime.InteropServices;

namespace BabumiGraphics.Live2D
{
    public class Model
    {
        public Live2Model ModelObject = new Live2Model();

        private List<Texture2D> _cacheTextureList = new List<Texture2D>();

        public bool IsLoadComplete { get; set; }

        public void LoadModel( string prefix, dynamic modelPath, dynamic textures )
        {
            //!< 2byte文字をLive2D側が読み込めない様子...
            using (var fs = new FileStream(Path.Combine(prefix, modelPath), FileMode.Open))
            {
                byte[] readBuffer = new byte[fs.Length];
                fs.Read(readBuffer, 0, (int)fs.Length);
                IntPtr dest = Marshal.AllocHGlobal((int)fs.Length);
                Marshal.Copy(readBuffer, 0, dest, (int)fs.Length);
                ModelObject.craeteModel(dest, (int)fs.Length);
            }

            //!< テクスチャー読み込み
            for (int i = 0; i < ((object[])textures).Length; ++i)
            {
                using (Bitmap bitmap = new Bitmap(Path.Combine( prefix, textures[ i ])))
                {
                    //  Create a new texture and bind it.
                    var tex = new Texture2D();
                    tex.Create();
                    tex.Bind();
                    ModelObject.setTexture(i, tex.SetImage(bitmap));
                    _cacheTextureList.Add(tex);
                }
            }
            ModelObject.setPremultipliedAlpha(false);
            IsLoadComplete = true;
        }

        public void Delete()
        {
            ModelObject.deleteModel();
            //!< テクスチャーを解放
            foreach( var tex in _cacheTextureList)
            {
                tex.Delete();
            }
            IsLoadComplete = false;
        }

        public void Update(int Width, int Height )
        {
            float aspect = (float)Width / (float)Height;

            float sx = 2.0f / ModelObject.getCanvasWidth();
            float sy = -2.0f / ModelObject.getCanvasWidth() * aspect;
            float x = -1;
            float y = 1;

            ModelObject.setMatrix(
                sx, 0, 0, 0,
                0, sy, 0, 0,
                0, 0, 1, 0,
                x, y, 0, 1);
            ModelObject.update();
            ModelObject.draw();
        }
    }
}
