using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet.Streaming;
using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;

namespace DesktopCharacter.Model.Service.Twitter
{
    public class TwitterService: IDisposable, IInitializable
    {
        private List<Twitter> _twitterList;

        public void Dispose()
        {
            Console.WriteLine("TwitterService Dispose");
            foreach (var twitter in _twitterList)
            {
                twitter.Dispose();
            }
        }

        public void Initialize()
        {
            var twitterRepository = ServiceLocator.Instance.GetInstance<TwitterRepository>();
            var twitterUsers = twitterRepository.FindAll();
            _twitterList = twitterUsers.Select(user => new Twitter(user)).ToList();
            Console.WriteLine("TwitterService Initialzied");
            foreach (var twitter in _twitterList)
            {
                Console.WriteLine(twitter.ScreenName);
                twitter.Initialize();
                twitter.StreamingObservable
                    .Subscribe(m => OnTwitterMessage(m, twitter.TwitterUser));
            }
        }

        private void OnTwitterMessage(StreamingMessage m, TwitterUser user)
        {
            if (m.Type == MessageType.Event)
            {
                var em = (EventMessage)m;
                //ソースが自分なら無視
                if (em.Source.Id == user.UserId)
                {
                    return;
                }
                switch (em.Event)
                {
                    case EventCode.Favorite:
                        if (user.Filter.Favorite)
                        {
                            //TODO お気に入り通知
                        }
                        break;
                    case EventCode.Unfavorite:
                        if (user.Filter.Unfavorite)
                        {
                            //TODO あんふぁぼ通知
                        }
                        break;
                    case EventCode.Follow:
                        if (user.Filter.Follow)
                        {
                            //TODO フォロー通知
                        }
                        break;

                    case EventCode.Unfollow:
                        if (user.Filter.Unfollow)
                        {
                            //TODO アンフォロー通知
                        }
                        break;

                    case EventCode.Block:
                        if (user.Filter.Block)
                        {
                            //TODO ブロック通知
                        }
                        break;

                    case EventCode.Unblock:
                        if (user.Filter.Unblock)
                        {
                            //TODO アンブロック通知
                        }
                        break;
                    case EventCode.ListMemberAdded:
                        if (user.Filter.ListAdded)
                        {
                            //TODO リストメンバー追加通知
                        }
                        break;
                    case EventCode.ListMemberRemoved:
                        if (user.Filter.ListRemoved)
                        {
                            //TODO リストメンバー削除通知
                        }
                        break;
                }
            }
            if (m.Type == MessageType.Create)
            {
                var sm = (StatusMessage)m;
                if (IsMentionStatus(sm, user))
                {
                    //TODO メンション通知
                }
                if (IsRetweet(sm, user))
                {
                    //TODO リツイート通知
                }
            }
            if (m.Type == MessageType.DirectMesssage)
            {
                var dm = (DirectMessageMessage)m;
                if (dm.DirectMessage.Sender.Id != user.UserId && user.Filter.DirectMessage)
                {
                    //TODO ダイレクトメッセージ通知
                }
            }
        }

        private bool IsRetweet(StatusMessage sm, TwitterUser user)
        {
            //普通のツイートは無視
            if (sm.Status.RetweetedStatus == null)
            {
                return false;
            }
            return sm.Status.RetweetedStatus.User.Id == user.UserId && user.Filter.Retweet;
        }

        private bool IsMentionStatus(StatusMessage sm, TwitterUser user)
        {
            //リツイートなら無視
            if (sm.Status.RetweetedStatus != null)
            {
                return false;
            }
            var mentions = sm.Status.Entities.UserMentions;
            return mentions.ToObservable().Any(e => e.Id == user.UserId).Wait() && user.Filter.Mention;
        }
    }
}
