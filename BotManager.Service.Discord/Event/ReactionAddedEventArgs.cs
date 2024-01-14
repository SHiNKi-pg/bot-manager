using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Event
{
    /// <summary>
    /// <seealso cref="IDiscordEventNotifier.ReactionAdded"/>から通知されるオブジェクトのクラス。
    /// </summary>
    public class ReactionAddedEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public Cacheable<IUserMessage, ulong> Message { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public Cacheable<IMessageChannel, ulong> Channel { get; init; }

        /// <summary>
        /// リアクション
        /// </summary>
        public SocketReaction Reaction { get; init; }

        internal ReactionAddedEventArgs(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
        {
            Message = message;
            Channel = channel;
            Reaction = reaction;
        }
    }
}
