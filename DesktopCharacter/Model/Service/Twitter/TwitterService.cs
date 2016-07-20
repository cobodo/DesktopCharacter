using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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
            foreach (var twitter in _twitterList)
            {
                twitter.Initialize();
                twitter.StreamingObservable
                    .Subscribe(m => OnTwitterMessage(m, twitter.TwitterUser));
            }
        }

        /// <summary>
        /// IDに一致するTwitterを返す
        /// </summary>
        /// <param name="id">UserID</param>
        /// <returns></returns>
        public Twitter FindById(long id)
        {
            return _twitterList.FirstOrDefault(twitter => twitter.TwitterUser.UserId == id);
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
                            CharacterNotify.Instance.Talk(em.Source.Name + "にふぁぼられました");
                        }
                        break;
                    case EventCode.Unfavorite:
                        if (user.Filter.Unfavorite)
                        {
                            CharacterNotify.Instance.Talk(em.Source.Name + "にあんふぁぼされました");
                        }
                        break;
                    case EventCode.Follow:
                        if (user.Filter.Follow)
                        {
                            CharacterNotify.Instance.Talk(em.Source.Name + "にフォローされました");
                        }
                        break;

                    case EventCode.Unfollow:
                        if (user.Filter.Unfollow)
                        {
                            CharacterNotify.Instance.Talk(em.Source.Name + "にフォロー解除されました");
                        }
                        break;

                    case EventCode.Block:
                        if (user.Filter.Block)
                        {
                            CharacterNotify.Instance.Talk(em.Source.Name + "にブロックされました");
                        }
                        break;

                    case EventCode.Unblock:
                        if (user.Filter.Unblock)
                        {
                            CharacterNotify.Instance.Talk(em.Source.Name + "にブロック解除されました");
                        }
                        break;
                    case EventCode.ListMemberAdded:
                        if (user.Filter.ListAdded)
                        {
                            CharacterNotify.Instance.Talk(em.Source.Name + "にリスト追加されました");
                        }
                        break;
                    case EventCode.ListMemberRemoved:
                        if (user.Filter.ListRemoved)
                        {
                            CharacterNotify.Instance.Talk(em.Source.Name + "にリストから外されました");
                        }
                        break;
                }
            }
            if (m.Type == MessageType.Create)
            {
                var sm = (StatusMessage)m;
                if (IsMentionStatus(sm, user))
                {
                    CharacterNotify.Instance.Talk(sm.Status.User.Name + "からメンション");
                }
                if (IsRetweet(sm, user))
                {
                    CharacterNotify.Instance.Talk(sm.Status.User.Name + "にRTされました");
                }
            }
            if (m.Type == MessageType.DirectMesssage)
            {
                var dm = (DirectMessageMessage)m;
                if (dm.DirectMessage.Sender.Id != user.UserId && user.Filter.DirectMessage)
                {
                    CharacterNotify.Instance.Talk(dm.DirectMessage.Sender.Name + "からDM");
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
