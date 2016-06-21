using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BabumiGraphics.Graphics
{
    public class Shader
    {
        /// <summary>
        /// Shaderリンクオブジェクト
        /// </summary>
        public uint ShaderLink { private set; get; }

        /// <summary>
        /// 作成したシェーダのコンテナ
        /// </summary>
        private Dictionary<Type, uint> ShaderObjectDict = new Dictionary<Type, uint>();

        /// <summary>
        /// デバイス
        /// </summary>
        private OpenGL Device;

        public Shader()
        {
            Device = GraphicsManager.Instance.Device;
        }

        public void CreateShader(string filePath, Type type)
        {
            if (ShaderLink == 0)
            {
                ShaderLink = Device.CreateProgram();
            }

            //!< Shader作成
            uint shader = CreateShader(type);
            
            //!< ファイルを読み込む
            string source = LoadSource(filePath);

            //!< コンパイル
            Device.ShaderSource(shader, source);
            Device.CompileShader(shader);

            int[] parameters = new int[] { 0 };
            Device.GetShader(shader, OpenGL.GL_COMPILE_STATUS, parameters);
            if (parameters[0] == OpenGL.GL_FALSE)
            {
                return;
            }

            //!< Handleを入れる
            ShaderObjectDict.Add(type, shader);
        }

        public void Attach()
        {
            //!< 作成したシェーダをアタッチ
            foreach( var obj in ShaderObjectDict)
            {
                Device.AttachShader(ShaderLink, obj.Value);
            }
            //!< リンクすることでGPUで使えるようになる
            Device.LinkProgram(ShaderLink);

            Device.UseProgram(ShaderLink);
        }

        public void Destory()
        {
            uint[] shader = new uint[ShaderObjectDict.Count];
            foreach ( var s in ShaderObjectDict.Values.ToArray())
            {
                Device.DeleteShader(s);
            }
        }

        private string LoadSource(string path)
        {
            string source = "";
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                StreamReader reader = new StreamReader(stream);
                source = reader.ReadToEnd();
                reader.Close();
            }
            return source;
        }

        private uint CreateShader(Type type)
        {
            switch (type)
            {
                case Type.VertexShader:
                    return Device.CreateShader(OpenGL.GL_VERTEX_SHADER);
                case Type.PixelShader:
                    return Device.CreateShader(OpenGL.GL_FRAGMENT_SHADER);
                case Type.ComputeShader:
                    return Device.CreateShader(OpenGL.GL_COMPUTE_SHADER);
            }
            return 0;
        }

        public enum Type
        {
            VertexShader,
            PixelShader,
            ComputeShader,
        }
    }
}
