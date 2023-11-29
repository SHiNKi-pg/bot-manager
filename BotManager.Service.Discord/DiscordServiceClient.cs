using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
