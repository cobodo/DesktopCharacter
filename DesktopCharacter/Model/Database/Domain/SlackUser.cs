using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Database.Domain
{
    [Table("slack_users")]
    public class SlackUser
    {
        [Key()]
        [Column("access_token")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AccessToken { get; set; }
        [Column("team_name")]
        public string TeamName { get; set; }
    }
}
