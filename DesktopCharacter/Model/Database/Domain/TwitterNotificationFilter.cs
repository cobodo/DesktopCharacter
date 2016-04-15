using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Database.Domain
{
    [Table("twitter_notification_filter")]
    public class TwitterNotificationFilter
    {
        [Key]
        [Column("filter_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FilterId
        {
            get;
            set;
        }

        [Column("favorite"), Required]
        public bool Favorite
        {
            get;
            set;
        }
        [Column("unfavorite"), Required]
        public bool Unfavorite { get; set; }
        [Column("retweet"), Required]
        public bool Retweet { get; set; }
        [Column("mention"), Required]
        public bool Mention { get; set; }
        [Column("follow"), Required]
        public bool Follow { get; set; }
        [Column("unfollow"), Required]
        public bool Unfollow { get; set; }
        [Column("block"), Required]
        public bool Block { get; set; }
        [Column("unblock"), Required]
        public bool Unblock { get; set; }
        [Column("list_added"), Required]
        public bool ListAdded { get; set; }
        [Column("list_removed"), Required]
        public bool ListRemoved { get; set; }
        [Column("direct_message"), Required]
        public bool DirectMessage { get; set; }
    }
}
