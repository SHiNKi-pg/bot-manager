using BotManager.Common;
using BotManager.Common.Messaging;
using BotManager.Service.Discord.Wrapper;
using Discord;
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
    public interface IDiscordServiceClient : IServiceClient, IDiscordEventNotifier, IMessageReceived<IReplyableMessage>
    {
        /// <summary>
        /// 指定したIDのサーバーのオブジェクトを返します。
        /// </summary>
        /// <param name="guildId">サーバーID</param>
        /// <returns></returns>
        IWrappedGuild<SocketGuild> GetGuild(ulong guildId);

        /// <summary>
        /// 指定したサーバーのチャンネルを返します。
        /// </summary>
        /// <param name="guildId">サーバーID</param>
        /// <param name="channelId">テキストチャンネルID</param>
        /// <returns></returns>
        IWrappedTextChannel<SocketTextChannel> GetTextChannelInGuild(ulong guildId, ulong channelId);

        /// <summary>
        /// 現在のオンライン状態を取得します。
        /// </summary>
        UserStatus Status { get; }

        /// <summary>
        /// オンライン状態を変更します。
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        Task SetUserStatus(UserStatus status);
    }
}
