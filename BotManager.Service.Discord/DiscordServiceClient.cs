using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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
        #endregion

        #region Method
        /// <summary>
        /// 開始してログインします
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            await client.StartAsync();
            await client.LoginAsync(TokenType.Bot, token);
        }

        /// <summary>
        /// ログアウトして終了します
        /// </summary>
        /// <returns></returns>
        public async Task EndAsync()
        {
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
