using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using BotManager.Common.Messaging;
using BotManager.Service.Discord.Extensions;
using BotManager.Service.Discord.Messaging;
using BotManager.Service.Discord.Wrapper;
using Discord;
using Discord.WebSocket;

namespace BotManager.Service.Discord
{
    /// <summary>
    /// Bot用 Discordクライアントクラス
    /// </summary>
    internal class DiscordServiceClient : IDiscordServiceClient
    {
        #region Private Fields
        private readonly string token;
        private readonly DiscordSocketClient client;

        private readonly IConnectableObservable<IReplyableMessageWithId<ulong>> messageReceived;
        private readonly CompositeDisposable subscriptions;
        #endregion

        #region Constructor
        /// <summary>
        /// <see cref="DiscordServiceClient"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="token">アクセストークン</param>
        /// <param name="config">設定</param>
        public DiscordServiceClient(string token, DiscordSocketConfig config)
        {
            this.token = token;
            client = new(config);

            this.subscriptions = new();

            // Event
            this.messageReceived = MessageReceived
                .Select(m => new DiscordMessage(this, m))
                .Publish();

            subscriptions.Add(this.messageReceived.Connect());
        }
        #endregion

        #region Events
        /// <summary>
        /// メッセージを受信した時に通知されます。
        /// </summary>
        public IObservable<SocketMessage> MessageReceived =>
            Observable.FromEvent<Func<SocketMessage, Task>, SocketMessage>(
                h => e => { h(e); return Task.CompletedTask; },
                h => client.MessageReceived += h,
                h => client.MessageReceived -= h
                );

        /// <summary>
        /// ログインした時に通知されます。
        /// </summary>
        public IObservable<Unit> LoggedIn =>
            Observable.FromEvent<Func<Task>, Unit>(
                h => () => { h(Unit.Default); return Task.CompletedTask; },
                h => client.LoggedIn += h,
                h => client.LoggedIn -= h
                );

        /// <summary>
        /// ログアウトした時に通知されます。
        /// </summary>
        public IObservable<Unit> LoggedOut =>
            Observable.FromEvent<Func<Task>, Unit>(
                h => () => { h(Unit.Default); return Task.CompletedTask; },
                h => client.LoggedOut += h,
                h => client.LoggedOut -= h
                );

        /// <summary>
        /// サーバーデータが取得できた時に通知されます。
        /// </summary>
        public IObservable<Unit> Ready =>
            Observable.FromEvent<Func<Task>, Unit>(
                h => () => { h(Unit.Default); return Task.CompletedTask; },
                h => client.Ready += h,
                h => client.Ready -= h
                );
        #endregion

        #region Properties
        /// <summary>
        /// 現在のオンライン状態を取得します。
        /// </summary>
        public UserStatus Status => client.Status;

        IObservable<IReplyableMessageWithId<ulong>> IMessageReceived<IReplyableMessageWithId<ulong>>.MessageReceived { get => messageReceived; }
        #endregion

        #region Method
        /// <summary>
        /// 開始してログインします
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            using (ReplaySubject<Unit> subject = new(1))
            {
                Ready.Take(1).Subscribe(subject);
                await client.StartAsync();
                await client.LoginAsync(TokenType.Bot, token);
                // 準備完了するまで待機する
                await subject;
            }
        }

        /// <summary>
        /// ログアウトして終了します
        /// </summary>
        /// <returns></returns>
        public async Task EndAsync()
        {
            await client.SetStatusAsync(UserStatus.Invisible);
            await client.LogoutAsync();
            await client.StopAsync();
        }

        /// <summary>
        /// 指定したIDのサーバーのオブジェクトを返します。
        /// </summary>
        /// <param name="guildId">サーバーID</param>
        /// <returns></returns>
        public IWrappedGuild<SocketGuild> GetGuild(ulong guildId)
        {
            return client.GetGuild(guildId).ToWrappedGuild();
        }

        /// <summary>
        /// 指定したサーバーのチャンネルを返します。
        /// </summary>
        /// <param name="guildId">サーバーID</param>
        /// <param name="channelId">テキストチャンネルID</param>
        /// <returns></returns>
        public IWrappedTextChannel<SocketTextChannel> GetTextChannelInGuild(ulong guildId, ulong channelId)
        {
            var guild = client.GetGuild(guildId);
            return guild.GetTextChannel(channelId).ToWrappedTextChannel();
        }

        /// <summary>
        /// オンライン状態を変更します。
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task SetUserStatus(UserStatus status)
        {
            await client.SetStatusAsync(status);
        }
        #endregion

        #region Disposal
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            subscriptions.Dispose();
            client.Dispose();
        }
        #endregion
    }
}
