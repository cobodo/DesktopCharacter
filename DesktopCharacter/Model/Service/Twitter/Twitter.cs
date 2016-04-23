using System;
using System.Data;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Windows.Threading;
using CoreTweet.Streaming;
using DesktopCharacter.Model.Database.Domain;
using DesktopCharacter.Model.Locator;
using DesktopCharacter.Model.Repository;

namespace DesktopCharacter.Model.Service.Twitter
{
    public class Twitter: IDisposable
    {
        public const string ConsumerKey = "KYylblbCVKZgnTMUhgFCkLrkK";
        public const string ConsumerSecret = "xsVZvuuvBFFhUtlhKgi7YW5XqfFqLme1hJUzhU9XCJdYNzCNBp";
        public TwitterUser TwitterUser { get; set; }
        public string ScreenName { get; private set; }

        private IDisposable _stream;
        private Subject<StreamingMessage> _streamingSubject = new Subject<StreamingMessage>();

        //StreamingObservable Sample
        //
        //StreamingObservable
        //      .Where(message => message.Type == MessageType.Limit)
        //      .Cast<LimitMessage>()
        //      .Subscribe((LimitMessage m) => { });
        public IObservable<StreamingMessage> StreamingObservable => _streamingSubject;

        public Twitter(TwitterUser twitterUser)
        {
            TwitterUser = twitterUser;
        }

        public void Initialize()
        {
            var tokens = CoreTweet.Tokens.Create(ConsumerKey, ConsumerSecret, TwitterUser.Token, TwitterUser.Secret, TwitterUser.UserId);
            var observable = tokens.Streaming.UserAsObservable();

            //ScreenNameは変更可能なので毎回認証時に取得
            var userResponse = tokens.Users.Show(TwitterUser.UserId, null);
            ScreenName = userResponse.ScreenName;
            TwitterUser.ScreenName = ScreenName;

            _streamingSubject = new Subject<StreamingMessage>();
            //エラー時に10秒待って再接続
            observable.Catch(observable.DelaySubscription(TimeSpan.FromSeconds(10)).Retry());
            _stream = observable.Subscribe(message => _streamingSubject.OnNext(message));
        }

        public void Dispose()
        {
            _stream?.Dispose();
            _streamingSubject.OnCompleted();
        }
    }
}
