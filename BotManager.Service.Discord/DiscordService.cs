using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord
{
    /// <summary>
    /// Discordサービスクラス
    /// </summary>
    public static class DiscordService
    {
        /// <summary>
        /// Discordクライアントを返します。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IDiscordServiceClient Create(string token, DiscordSocketConfig config)
        {
            return new DiscordServiceClient(token, config);
        }

        /// <summary>
        /// Discordクライアントを返します。
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static IDiscordServiceClient Create(string token)
        {
            return Create(token, new()
            {
                GatewayIntents = global::Discord.GatewayIntents.All,
            });
        }
    }
}
