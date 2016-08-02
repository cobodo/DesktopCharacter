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

        public SlackNotificationFilter Filter { get; set; }

        protected bool Equals(SlackUser other)
        {
            return string.Equals(AccessToken, other.AccessToken);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SlackUser) obj);
        }

        public override int GetHashCode()
        {
            return AccessToken?.GetHashCode() ?? 0;
        }
    }
}
