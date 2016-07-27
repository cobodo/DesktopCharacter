using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Codeplex.Data;

namespace DesktopCharacter.Model.Service.Slack
{
    public class SlackClient
    {
        private readonly string _accessToken;
        private ClientWebSocket _ws = null;

        public Subject<string> Message { get; private set; } = new Subject<string>();

        internal SlackClient(string accessToken)
        {
            _accessToken = accessToken;
        }

        /// <summary>
        /// ストリーミングに接続します
        /// </summary>
        public async void ConnectStreaming()
        {
            using (var client = new HttpClient())
            {
                var url = $"https://slack.com/api/rtm.start?token={_accessToken}";
                client.Timeout = TimeSpan.FromSeconds(10);
                var response = await client.GetStringAsync(url);

                var json = DynamicJson.Parse(response);
                if (!json.ok)
                {
                    throw new SlackAuthException(json.error);
                }

                if (_ws == null)
                {
                    _ws = new ClientWebSocket();
                }

                if (_ws.State != WebSocketState.Open)
                {
                    await _ws.ConnectAsync(new Uri(json.url), CancellationToken.None);

                    while (_ws?.State == WebSocketState.Open)
                    {
                        var buff = new ArraySegment<byte>(new byte[1024]);
                        var message = await _ws.ReceiveAsync(buff, CancellationToken.None);
                        var messageStr = (new UTF8Encoding()).GetString(buff.Take(message.Count).ToArray());
                        if (messageStr.Length == 0)
                        {
                            continue;
                        }
                        var messageJson = DynamicJson.Parse(messageStr);

                        if (messageJson.type == "message")
                        {
                            Message.OnNext(messageJson.text);
                        }
                    }
                }

            }
        }

        public void CloseStream()
        {
            if (_ws != null && (_ws.State == WebSocketState.Open || _ws.State == WebSocketState.Connecting))
            {
                _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
            _ws = null;
        }
    }
}
