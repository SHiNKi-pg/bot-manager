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
    public class MisskeyClient : IMisskeyServiceClient
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

                string connectionJson = JsonConvert.SerializeObject(connectionData);
                if(websocketClient.IsRunning)
                    await websocketClient.SendInstant(connectionJson);

                // データ購読
                var subscription = websocketClient
                    .MessageReceived
                    .Where(mr => mr.MessageType == WebSocketMessageType.Text)
                    .Select(mr => mr.Text)
                    .IsNotNull()
                    .WhereIs<MisskeyBase<IReceivedBody<Note>>>()
                    .Where(mes => mes.Type == "channel" && mes.Body.Id == id)
                    .Select(mes => mes.Body.Body)
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
                    var disconnectionJson = JsonConvert.SerializeObject(disconnectionData);
                    if(websocketClient.IsRunning)
                        websocketClient.Send(disconnectionJson);
                });

                return StableCompositeDisposable.Create(subscription, disconnection);
            });
        }

        /// <summary>
        /// Globalタイムラインを取得します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IObservable<Note> GetGlobalTimeline(string id) => GetTimeline("globalTimeline", id);

        /// <summary>
        /// MisskeyClientを開始します。
        /// </summary>
        /// <returns></returns>
        public Task StartAsync()
        {
            return websocketClient.Start();
        }

        /// <summary>
        /// MisskeyClientを終了します。
        /// </summary>
        /// <returns></returns>
        public async Task EndAsync()
        {
            await websocketClient.Stop(WebSocketCloseStatus.NormalClosure, "stop websockets");
        }
        #endregion

        #region Disposal
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            websocketClient.Dispose();
        }
        #endregion
    }
}
