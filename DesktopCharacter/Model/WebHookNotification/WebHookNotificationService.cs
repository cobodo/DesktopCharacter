using EventSource4Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesktopCharacter.Model.WebHookNotification
{
    /// <summary>
    /// WebHookNotificationからの通知を提供するサービス
    /// </summary>
    class WebHookNotificationService
    {
        private readonly string host;
        /// <summary>
        /// Slackからのメッセージ
        /// </summary>
        public Subject<SlackMessage> SlackMessage { get; } =  new Subject<SlackMessage>();

        /// <param name="host">接続先のホスト</param>
        public WebHookNotificationService(string host)
        {
            this.host = host;
        }

        /// <summary>
        /// WebHookNotificationとの接続を確立します
        /// </summary>
        public void Initialize()
        {
            var cts = new CancellationTokenSource();
            var es = new EventSource4Net.EventSource(new Uri("http://" + host + "/streaming/slack"), int.MaxValue);
            es.StateChanged += new EventHandler<StateChangedEventArgs>((o, e) => {
                Console.WriteLine("New state: " + e.State.ToString());
            });
            es.EventReceived += new EventHandler<ServerSentEventReceivedEventArgs>(OnSlackMessageReceived);
            es.Start(cts.Token);
        }

        private void OnSlackMessageReceived(object o, ServerSentEventReceivedEventArgs e)
        {
            var jsonData = e.Message.Data;
            var serializer = new DataContractJsonSerializer(typeof(SlackMessage));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData)))
            {
                var data = (SlackMessage)serializer.ReadObject(stream);
                SlackMessage.OnNext(data);
            }
        }
    }

    /// <summary>
    /// Slackからのコマンド通知
    /// </summary>
    [DataContract]
    class SlackMessage
    {
        /// <summary>
        /// APIのトークン
        /// 例: KJkjLT0RH6zb6ydgSzRd70kV
        /// </summary>
        [DataMember()]
        public string token = "";
        /// <summary>
        /// チームのID
        /// 例: T0001
        /// </summary>
        [DataMember]
        public string team_id = "";
        /// <summary>
        /// チームのドメイン
        /// 例: example
        /// </summary>
        [DataMember]
        public string team_domain = "";
        /// <summary>
        /// チャンネルのID
        /// 例: C2147483705
        /// </summary>
        [DataMember]
        public string channel_id = "";
        /// <summary>
        /// チャンネルの名前
        /// 例: test
        /// </summary>
        [DataMember]
        public string channel_name = "";
        /// <summary>
        /// ユーザーID
        /// 例: U2147483697
        /// </summary>
        [DataMember]
        public string user_id = "";
        /// <summary>
        /// ユーザー名
        /// 例: Steve
        /// </summary>
        [DataMember]
        public string user_name = "";
        /// <summary>
        /// 実行されたコマンド
        /// 例: /weather
        /// </summary>
        [DataMember]
        public string command = "";
        /// <summary>
        /// コマンドに続くテキスト
        /// 例: 94070
        /// </summary>
        [DataMember]
        public string text = "";
        /// <summary>
        /// レスポンスURL
        /// 例: https://hooks.slack.com/commands/1234/5678
        /// </summary>
        [DataMember]
        public string response_url = "";
    }
}
