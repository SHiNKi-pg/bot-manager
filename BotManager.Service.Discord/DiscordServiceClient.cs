﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using BotManager.Service.Discord.Extensions;
using BotManager.Service.Discord.Wrapper;
using Discord;
using Discord.WebSocket;

namespace BotManager.Service.Discord
{
    /// <summary>
    /// Bot用 Discordクライアントクラス
    /// </summary>
    public class DiscordServiceClient : IDiscordServiceClient
    {
        #region Private Fields
        private readonly string token;
        private readonly DiscordSocketClient client;
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
        }

        /// <summary>
        /// <see cref="DiscordServiceClient"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="token">アクセストークン</param>
        public DiscordServiceClient(string token) : this(token, new()
        {
            GatewayIntents = GatewayIntents.All
        })
        { }
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
        #endregion

        #region Properties
        /// <summary>
        /// 現在のオンライン状態を取得します。
        /// </summary>
        public UserStatus Status => client.Status;
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
                LoggedIn.Take(1).Subscribe(subject);
                await client.StartAsync();
                await client.LoginAsync(TokenType.Bot, token);
                await subject;
            }
            // 認証してから3秒間は待機しないとなぜかサーバー取得に失敗する
            await Task.Delay(3000);
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
            client.Dispose();
        }
        #endregion
    }
}
