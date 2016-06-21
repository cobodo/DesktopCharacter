using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Database.Domain
{
    [Table("window_position")]
    public class WindowPosition
    {
        [Key()]
        [Column("window_position_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Column("pos_x"), Required]
        public int PosX { get; set; }
        [Column("pos_y"), Required]
        public int PosY { get; set; }

        public WindowPosition()
        {
        }

        public WindowPosition(int posX, int posY)
        {
            Id = 1;
            PosX = posX;
            PosY = posY;
        }

        protected bool Equals(WindowPosition other)
        {
            return PosX == other.PosX && PosY == other.PosY;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((WindowPosition) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (PosX*397) ^ PosY;
            }
        }
    }
}
