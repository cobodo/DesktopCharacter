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
using DesktopCharacter.Model.Service.Template;
using DesktopCharacter.ViewModel;

namespace DesktopCharacter.Model.Service.Twitter
{
    public class TwitterService: IDisposable, IInitializable
    {
        private List<Twitter> _twitterList;
        private ITemplateService _templateService;

        public void Dispose()
        {
            foreach (var twitter in _twitterList)
            {
                twitter.Dispose();
            }
        }

        public void Initialize()
        {
            _templateService = ServiceLocator.Instance.GetInstance<ITemplateService>();
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
                            var str = _templateService.ProcessTemplate(TemplateVariables.BABUMI_NAMESPACE,
                                TemplateVariables.TWITTER_FAVORITE, new {Event = em, User = user});
                            CharacterNotify.Instance.Talk(str);
                        }
                        break;
                    case EventCode.Unfavorite:
                        if (user.Filter.Unfavorite)
                        {
                            var str = _templateService.ProcessTemplate(TemplateVariables.BABUMI_NAMESPACE,
                                TemplateVariables.TWITTER_UNFAVORITE, new { Event = em, User = user });
                            CharacterNotify.Instance.Talk(str);
                        }
                        break;
                    case EventCode.Follow:
                        if (user.Filter.Follow)
                        {
                            var str = _templateService.ProcessTemplate(TemplateVariables.BABUMI_NAMESPACE,
                                TemplateVariables.TWITTER_FOLLOW, new { Event = em, User = user });
                            CharacterNotify.Instance.Talk(str);
                        }
                        break;

                    case EventCode.Unfollow:
                        if (user.Filter.Unfollow)
                        {
                            var str = _templateService.ProcessTemplate(TemplateVariables.BABUMI_NAMESPACE,
                                TemplateVariables.TWITTER_UNFOLLOW, new { Event = em, User = user });
                            CharacterNotify.Instance.Talk(str);
                        }
                        break;

                    case EventCode.Block:
                        if (user.Filter.Block)
                        {
                            var str = _templateService.ProcessTemplate(TemplateVariables.BABUMI_NAMESPACE,
                                TemplateVariables.TWITTER_BLOCK, new { Event = em, User = user });
                            CharacterNotify.Instance.Talk(str);
                        }
                        break;

                    case EventCode.Unblock:
                        if (user.Filter.Unblock)
                        {
                            var str = _templateService.ProcessTemplate(TemplateVariables.BABUMI_NAMESPACE,
                                TemplateVariables.TWITTER_UNBLOCK, new { Event = em, User = user });
                            CharacterNotify.Instance.Talk(str);
                        }
                        break;
                    case EventCode.ListMemberAdded:
                        if (user.Filter.ListAdded)
                        {
                            var str = _templateService.ProcessTemplate(TemplateVariables.BABUMI_NAMESPACE,
                                TemplateVariables.TWITTER_MEMBER_ADDED, new { Event = em, User = user });
                            CharacterNotify.Instance.Talk(str);
                        }
                        break;
                    case EventCode.ListMemberRemoved:
                        if (user.Filter.ListRemoved)
                        {
                            var str = _templateService.ProcessTemplate(TemplateVariables.BABUMI_NAMESPACE,
                                TemplateVariables.TWITTER_MEMBER_REMOVED, new { Event = em, User = user });
                            CharacterNotify.Instance.Talk(str);
                        }
                        break;
                }
            }
            if (m.Type == MessageType.Create)
            {
                var sm = (StatusMessage)m;
                if (IsMentionStatus(sm, user))
                {
                    var str = _templateService.ProcessTemplate(TemplateVariables.BABUMI_NAMESPACE,
                                TemplateVariables.TWITTER_MENTION, new { Event = sm, User = user });
                    CharacterNotify.Instance.Talk(str);
                }
                if (IsRetweet(sm, user))
                {
                    var str = _templateService.ProcessTemplate(TemplateVariables.BABUMI_NAMESPACE,
                                TemplateVariables.TWITTER_RT, new { Event = sm, User = user });
                    CharacterNotify.Instance.Talk(str);
                }
            }
            if (m.Type == MessageType.DirectMesssage)
            {
                var dm = (DirectMessageMessage)m;
                if (dm.DirectMessage.Sender.Id != user.UserId && user.Filter.DirectMessage)
                {
                    var str = _templateService.ProcessTemplate(TemplateVariables.BABUMI_NAMESPACE,
                                TemplateVariables.TWITTER_DM, new { Event = dm, User = user });
                    CharacterNotify.Instance.Talk(str);
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
