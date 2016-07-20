using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesktopCharacter.Model.Database.Domain
{
    [Table("twitter_user")]
    public class TwitterUser
    {
        [Key()]
        [Column("twitter_user_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //自動採番無効化
        public long UserId { get; set; }
        [Column("access_token"), Required]
        public string Token { get; set; }
        [Column("access_token_secret"), Required]
        public string Secret { get; set; }
        [NotMapped]
        public string ScreenName { get; set; }

        public TwitterNotificationFilter Filter { get; set; }

        public TwitterUser()
        {
            
        }

        public TwitterUser(CoreTweet.Tokens tokens)
        {
            UserId = tokens.UserId;
            Token = tokens.AccessToken;
            Secret = tokens.AccessTokenSecret;
            ScreenName = tokens.ScreenName;
            Filter = new TwitterNotificationFilter();
        }

        protected bool Equals(TwitterUser other)
        {
            return UserId == other.UserId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TwitterUser) obj);
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }
    }
}
