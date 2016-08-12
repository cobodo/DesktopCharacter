using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DesktopCharacter.Util.Math;
using System.Xml.Serialization;

namespace DesktopCharacter.Model.Database.Domain
{
    [Serializable]
    public class BabumiConfig
    {   
        public string ModelJsonPath { get; set; }
        public double RequiredVersion { get; set; }
        public string Live2DResourceDir { get; set; }
        public bool Topmost { get; set; }
        public int ZoomLevel { get; set; }
        public List<LauncherSettingsDataSet> Dataset { get; set; } = new List<LauncherSettingsDataSet>();
        [XmlIgnore]
        public Point WindowSizeOrigin { get; private set; } = new Point { X = 400, Y = 400 };
        [XmlIgnore]
        public int ZoomLevelMax { get; set; } = 3;
        [XmlIgnore]
        public int ZoomLevelMin { get; set; } = -3;

        public BabumiConfig()
        {
        }

        public BabumiConfig(string modelJsonPath, double requiredVersion, string live2DResourceDir, string live2DModelDir, bool topmost, int zoomLevel)
        {
            ModelJsonPath = modelJsonPath;
            RequiredVersion = requiredVersion;
            Live2DResourceDir = live2DResourceDir;
            Topmost = topmost;
            ZoomLevel = zoomLevel;
        }

        public void AddZoomLevel( int param )
        {
            ZoomLevel += param;
            ZoomLevel = Function.Clamp(ZoomLevel, ZoomLevelMin, ZoomLevelMax);
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
                        Topmost = true,
                        ZoomLevel = 0,
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
