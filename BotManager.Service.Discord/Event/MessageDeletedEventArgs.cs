using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Event
{
    /// <summary>
    /// <seealso cref="IDiscordEventNotifier.MessageDeleted"/>から通知されるオブジェクトのクラス。
    /// </summary>
    public sealed class MessageDeletedEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public Cacheable<IMessage, ulong> Message { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public Cacheable<IMessageChannel, ulong> Channel { get; init; }

        internal MessageDeletedEventArgs(Cacheable<IMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel)
        {
            Message = message;
            Channel = channel;
        }
    }
}
