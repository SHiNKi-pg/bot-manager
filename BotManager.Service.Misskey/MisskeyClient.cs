using BotManager.Service.Misskey.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;
using System.Net.WebSockets;
using BotManager.Reactive.Json;
using BotManager.Reactive;
using BotManager.Service.Misskey.Schemas.Streaming;
using Newtonsoft.Json;
using System.Reactive.Disposables;

namespace BotManager.Service.Misskey
{
    /// <summary>
    /// Misskeyクライアントクラス
    /// </summary>
    internal class MisskeyClient : IMisskeyServiceClient
    {
        #region Private Field
        private WebsocketClient websocketClient;
        #endregion

        #region Constructor
        /// <summary>
        /// <see cref="MisskeyClient"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="host">ホスト名</param>
        /// <param name="token">アクセストークン</param>
        public MisskeyClient(string host, string token)
        {
            websocketClient = new(new($"wss://{host}/streaming&i={token}"));
        }
        #endregion

        #region Method

        private IObservable<Note> GetTimeline(string channelName, string id)
        {
            return Observable.Create<Note>(async observer =>
            {
                // チャンネル接続
                var connectionData = new MisskeyBase<StreamingConnectionBody>()
                { 
                    Type = "connect",
                    Body = new()
                    {
                        Channel = channelName,
                        Id = id
                    }
                };
                await websocketClient.SendInstant(JsonConvert.SerializeObject(connectionData));

                // データ購読
                var subscription = websocketClient
                    .MessageReceived
                    .Where(mr => mr.MessageType == WebSocketMessageType.Text)
                    .Select(mr => mr.Text)
                    .IsNotNull()
                    .WhereIs<MisskeyBase<Note>>()
                    .Where(mes => mes.Type == "channel" && mes.Body.Id == id)
                    .Select(mes => mes.Body)
                    .Subscribe(observer)
                    ;

                // Dispose時にチャンネルから切断
                var disconnection = Disposable.Create(() =>
                {
                    var disconnectionData = new MisskeyBase<StreamingDisconnectionBody>()
                    {
                        Type = "disconnect",
                        Body = new()
                        {
                            Id = id
                        },
                    };
                    websocketClient.Send(JsonConvert.SerializeObject(disconnectionData));
                });

                return StableCompositeDisposable.Create(subscription, disconnection);
            });
        }

        public Task StartAsync()
        {
            return Task.CompletedTask;
        }

        public Task EndAsync()
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Disposal
        public void Dispose()
        {
            websocketClient.Dispose();
        }
        #endregion
    }
}
