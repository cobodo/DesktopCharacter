using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesktopCharacter.Model.Database.Domain
{
    [Table("character_list")]
    class CharacterData
    {   
        [Key()]
        [Column("character_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }

        public CharacterData()
        {
        }

        public CharacterData(string name)
        {
            Name = name;
        }

        protected bool Equals(CharacterData other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CharacterData)obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
