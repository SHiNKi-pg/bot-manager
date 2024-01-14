using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Event
{
    /// <summary>
    /// <seealso cref="IDiscordEventNotifier.UserLeft"/>から通知されるオブジェクトのクラス
    /// </summary>
    public sealed class UserLeftEventArgs
    {
        /// <summary>
        /// ユーザーが離脱したサーバー
        /// </summary>
        public SocketGuild Guild { get; }

        /// <summary>
        /// 離脱したユーザー
        /// </summary>
        public SocketUser User { get; }

        internal UserLeftEventArgs(SocketGuild guild, SocketUser user)
        {
            Guild = guild;
            User = user;
        }
    }
}
