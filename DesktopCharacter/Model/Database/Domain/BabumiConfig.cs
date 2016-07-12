using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesktopCharacter.Model.Database.Domain
{
    [Serializable]
    public class BabumiConfig
    {   
        public string ModelJsonPath { get; set; }
        public double RequiredVersion { get; set; }
        public string Live2DResourceDir { get; set; }

        public BabumiConfig()
        {
        }

        public BabumiConfig(string modelJsonPath, double requiredVersion, string live2DResourceDir, string live2DModelDir)
        {
            ModelJsonPath = modelJsonPath;
            RequiredVersion = requiredVersion;
            Live2DResourceDir = live2DResourceDir;
        }

        public static BabumiConfig DefaultConfig()
        {
            try
            {
                var fileList = Util.File.DirectoryUtility.GetFileList("Res/Live2D", ".model.json");
                if (fileList.Count != 0)
                {
                    //!< デフォルトのコンフィグを作成する
                    return new BabumiConfig
                    {
                        RequiredVersion = 4.2,
                        Live2DResourceDir = "Res/Live2D",
                        ModelJsonPath = fileList.FirstOrDefault(),
                    };
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
