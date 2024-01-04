using BotManager.Common;
using BotManager.Service.Discord.Wrapper;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord
{
    /// <summary>
    /// Bot用Discordクライアント
    /// </summary>
    public interface IDiscordServiceClient : IServiceClient, IDiscordEventNotifier
    {
        /// <summary>
        /// 指定したIDのサーバーのオブジェクトを返します。
        /// </summary>
        /// <param name="guildId">サーバーID</param>
        /// <returns></returns>
        IWrappedGuild<SocketGuild> GetGuild(ulong guildId);
    }
}
