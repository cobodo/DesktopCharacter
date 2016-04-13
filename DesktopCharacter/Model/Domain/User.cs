using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Domain
{
    [Table("user")]
    class User
    {
        [Key]
        [Column("user_id")]
        public int Id { get; set; }
        [Column("user_name")]
        public string Name { get; set;  }
    }
}
