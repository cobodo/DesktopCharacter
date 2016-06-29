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

        protected bool Equals(BabumiConfig other)
        {
            return ModelJsonPath == other.ModelJsonPath
                && RequiredVersion == other.RequiredVersion
                && Live2DResourceDir == other.Live2DResourceDir;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BabumiConfig)obj);
        }

        public override int GetHashCode()
        {
            return ModelJsonPath.GetHashCode();
        }
    }
}
