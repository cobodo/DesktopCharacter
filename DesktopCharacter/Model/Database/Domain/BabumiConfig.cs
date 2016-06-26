using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesktopCharacter.Model.Database.Domain
{
    [Table("babumi_config")]
    public class BabumiConfig
    {   
        [Key()]
        [Column("character_name")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        [Column("opengl_required_version")]
        public double RequiredVersion { get; set; }
        [Column("live2d_resoruce_dir")]
        public string Live2DResourceDir { get; set; }
        [Column("live2d_model_dir")]
        public string Live2DModelDir { get; set; }

        public BabumiConfig()
        {
        }

        public BabumiConfig(string name, double requiredVersion, string live2DResourceDir, string live2DModelDir)
        {
            Name = name;
            RequiredVersion = requiredVersion;
            Live2DResourceDir = live2DResourceDir;
            Live2DModelDir = live2DModelDir;
        }

        protected bool Equals(BabumiConfig other)
        {
            return Name == other.Name
                && RequiredVersion == other.RequiredVersion
                && Live2DResourceDir == other.Live2DResourceDir
                && Live2DModelDir == other.Live2DModelDir;
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
            return Name.GetHashCode();
        }
    }
}
