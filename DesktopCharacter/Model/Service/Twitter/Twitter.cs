using System;
using System.Data;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using CoreTweet.Streaming;
using DesktopCharacter.Model.Database.Domain;

namespace DesktopCharacter.Model.Service.Twitter
{
    class Twitter: IDisposable
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
            Initialize();
        }

        public void Initialize()
        {
            var tokens = CoreTweet.Tokens.Create(ConsumerKey, ConsumerSecret, TwitterUser.Token, TwitterUser.Secret);
            var observable = tokens.Streaming.UserAsObservable();
            ScreenName = tokens.ScreenName;

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
