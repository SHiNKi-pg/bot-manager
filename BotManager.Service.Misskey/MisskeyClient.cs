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
using BotManager.Service.Misskey.Schemas.Streaming.Captures;
using BotManager.Common.Messaging;
using BotManager.Service.Misskey.Messaging;
using System.Reactive.Subjects;
using BotManager.Common;
using System.Reactive;

namespace BotManager.Service.Misskey
{
    /// <summary>
    /// Misskeyクライアントクラス
    /// </summary>
    internal partial class MisskeyClient : IMisskeyServiceClient
    {
        #region Private Field
        private string host;
        private string token;
        private WebsocketClient websocketClient;

        private readonly IConnectableObservable<IReplyableMessageWithId<string, string>> messageReceived;
        private readonly CompositeDisposable subscriptions;
        private readonly ILog logger;

        private readonly ReplaySubject<Unit> readyobs;
        #endregion

        #region Constructor
        /// <summary>
        /// <see cref="MisskeyClient"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="host">ホスト名</param>
        /// <param name="token">アクセストークン</param>
        public MisskeyClient(string host, string token)
        {
            logger = Log.GetLogger("misskey-ws");
            readyobs = new(1);
            this.host = host;
            this.token = token;
            this.subscriptions = new();
            websocketClient = new(new($"wss://{host}/streaming&i={token}"), () => new()
            {
                Options =
                {
                    KeepAliveInterval = TimeSpan.Zero,
                }
            });

            subscriptions.Add(
                websocketClient.DisconnectionHappened
                .Subscribe(d => logger.Warn(d.CloseStatusDescription ?? "Websocket connection disconnected"))
                );

            subscriptions.Add(
                websocketClient.ReconnectionHappened
                .Subscribe(r => logger.Info("Websocket reconnection:" + r.Type.ToString()))
                );

            subscriptions.Add(
                websocketClient.MessageReceived
                .Where(rm => rm.MessageType == WebSocketMessageType.Text)
                .Subscribe(rm => logger.Trace("Message Received: " + rm.Text)));

            this.Api = new MisskeyApi(host, token);

            subscriptions.Add(websocketClient.MessageReceived
                .Retry()
                .Where(rm => rm.MessageType == WebSocketMessageType.Text)
                .Select(rm => rm.Text)
                .Subscribe(t => Console.WriteLine(t))
                );

            // Event
            this.messageReceived = GetHomeTimeline(GetType().FullName + "_home_timeline")
                .Select(note => new MisskeyMessage(this, note))
                .Publish();
            subscriptions.Add(messageReceived.Connect());
        }
        #endregion

        #region Property
        public IMisskeyApi Api { get; }

        public IObservable<IReplyableMessageWithId<string, string>> MessagingReceived => messageReceived;
        #endregion

        #region Streaming Timeline

        private IObservable<Note> GetTimeline(string channelName, string id)
        {
            return Observable.Create<Note>(async observer =>
            {
                logger.Debug($"{channelName}-{id} connection : Wait for ready");
                // スタートするまで待機する
                await readyobs.Take(1).Count();
                logger.Debug("Misskey Ready.");

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
                    .Retry()
                    .Where(mr => mr.MessageType == WebSocketMessageType.Text)
                    .Select(mr => mr.Text)
                    .IsNotNull()
                    .WhereIs<MisskeyBase<ReceivedBody<Note>>>()
                    .Where(mes => mes.Type == "channel" && mes.Body.Id == id)
                    .Select(mes => mes.Body.Body)
                    .Subscribe(observer)
                    ;

                // Dispose時にチャンネルから切断
                var disconnection = Disposable.Create(() =>
                {
                    logger.Debug($"{channelName}-{id} connection : disconnection");
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
        /// グローバルタイムラインを取得します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IObservable<Note> GetGlobalTimeline(string id) => GetTimeline("globalTimeline", id);

        /// <summary>
        /// ホームタイムラインを取得します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IObservable<Note> GetHomeTimeline(string id) => GetTimeline("homeTimeline", id);

        /// <summary>
        /// ソーシャルタイムラインを取得します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IObservable<Note> GetHybridTimeline(string id) => GetTimeline("hybridTimeline", id);

        /// <summary>
        /// ローカルタイムラインを取得します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IObservable<Note> GetLocalTimeline(string id) => GetTimeline("localTimeline", id);

        // TODO: Mainタイムライン取得メソッドの追加
        #endregion

        #region Streaming Capture
        /// <summary>
        /// 投稿をキャプチャします
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public IObservable<MisskeyBase<ReceivedBody<dynamic>>> CaptureNote(string noteId)
        {
            return Observable.Create<MisskeyBase<ReceivedBody<dynamic>>>(async observer =>
            {
                // ノートのキャプチャ
                var connectionData = new MisskeyBase<NoteSubscriptionBody>()
                {
                    Type = "subNote",
                    Body = new()
                    {
                        NoteId = noteId
                    }
                };

                string connectionJson = JsonConvert.SerializeObject(connectionData);
                if (websocketClient.IsRunning)
                    await websocketClient.SendInstant(connectionJson);

                // データ購読
                var subscription = websocketClient
                    .MessageReceived
                    .Where(mr => mr.MessageType == WebSocketMessageType.Text)
                    .Select(mr => mr.Text)
                    .IsNotNull()
                    .WhereIs<MisskeyBase<ReceivedBody<dynamic>>>()
                    .Where(mes => mes.Body.Id == noteId)
                    .Subscribe(observer)
                    ;

                // Dispose時にキャプチャを解除
                var disconnection = Disposable.Create(() =>
                {
                    var disconnectionData = new MisskeyBase<NoteSubscriptionBody>()
                    {
                        Type = "unsubNote",
                        Body = new()
                        {
                            NoteId = noteId
                        },
                    };
                    string disconnectionJson = JsonConvert.SerializeObject(disconnectionData);
                    if (websocketClient.IsRunning)
                        websocketClient.Send(disconnectionJson);
                });

                return StableCompositeDisposable.Create(subscription, disconnection);
            });
        }
        #endregion

        #region Method
        /// <summary>
        /// MisskeyClientを開始します。
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            await websocketClient.Start();
            readyobs.OnNext(Unit.Default);
            logger.Info("Misskey Started");
        }

        /// <summary>
        /// MisskeyClientを終了します。
        /// </summary>
        /// <returns></returns>
        public async Task EndAsync()
        {
            await websocketClient.Stop(WebSocketCloseStatus.NormalClosure, "stop websockets");
            logger.Info("Misskey End");
        }

        public async Task<IMessaging> Send(string content)
        {
            var createdNote = await Api.Notes.CreateNote(text: content);
            return new Messaging.MisskeyMessage(this, createdNote.Note);
        }
        #endregion

        #region Disposal
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            subscriptions.Dispose();
            websocketClient.Dispose();
            readyobs.Dispose();
            logger.Debug("Misskey Client Disposed");
        }
        #endregion
    }
}
