using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesktopCharacter.Model.Database.Domain
{
    [Table("codic_user")]
    class CodicUser
    {
        [Key]
        [Column("codic_access_token"), Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Token { get; set; }
        [Column("codic_casing")]
        public string Casing { get; set; }

        public CodicUser()
        {

        }

        public CodicUser( string token, string casing )
        {
            Token = token;
            Casing = casing;
        }
    }
}
