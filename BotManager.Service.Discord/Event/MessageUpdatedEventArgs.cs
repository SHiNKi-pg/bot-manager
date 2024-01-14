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
    /// <seealso cref="IDiscordEventNotifier.MessageUpdated"/>から通知されるオブジェクトのクラス。
    /// </summary>
    public sealed class MessageUpdatedEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public Cacheable<IMessage, ulong> CacheableMessage { get; }

        /// <summary>
        /// メッセージ
        /// </summary>
        public SocketMessage Message { get; }

        /// <summary>
        /// チャンネル
        /// </summary>
        public ISocketMessageChannel Channel { get; }

        internal MessageUpdatedEventArgs(Cacheable<IMessage, ulong> cacheableMessage, SocketMessage message, ISocketMessageChannel channel)
        {
            CacheableMessage = cacheableMessage;
            Message = message;
            Channel = channel;
        }
    }
}
