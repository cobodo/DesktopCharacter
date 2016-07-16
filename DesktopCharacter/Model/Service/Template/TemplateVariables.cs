using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.Service.Template
{
    class TemplateVariables
    {
        private TemplateVariables()
        {
            throw new NotImplementedException();
        }

        //namespace
        public const string BABUMI_NAMESPACE = "babumi";

        //timesignal prefix
        public const string TIME_SIGNAL_PREFIX = "timesignal.";

        //id prefix
        public const string TWITTER_PREFIX = "twitter.";

        //template id
        //Twitter
        public const string TWITTER_FAVORITE = TWITTER_PREFIX + "favorite";
        public const string TWITTER_UNFAVORITE = TWITTER_PREFIX + "unfavorite";
        public const string TWITTER_FOLLOW = TWITTER_PREFIX + "follow";
        public const string TWITTER_UNFOLLOW = TWITTER_PREFIX + "unfollow";
        public const string TWITTER_BLOCK = TWITTER_PREFIX + "block";
        public const string TWITTER_UNBLOCK = TWITTER_PREFIX + "unblock";
        public const string TWITTER_MEMBER_ADDED = TWITTER_PREFIX + "member-added";
        public const string TWITTER_MEMBER_REMOVED = TWITTER_PREFIX + "member-removed";
        public const string TWITTER_RT = TWITTER_PREFIX + "rt";
        public const string TWITTER_MENTION = TWITTER_PREFIX + "mention";
        public const string TWITTER_DM = TWITTER_PREFIX + "dm";
        //TimeSignal
        public const string TIME_SIGNAL = TIME_SIGNAL_PREFIX + "timesignal";
    }
}
