using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Database.Domain
{
    [Table("slack_notification_filter")]
    public class SlackNotificationFilter
    {
        [Key]
        [Column("filter_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FilterId { get; set; }
        [Column("message"), Required]
        public bool Message { get; set; }
    }
}
