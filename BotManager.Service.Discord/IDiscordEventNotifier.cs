using BotManager.Common;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord
{
    /// <summary>
    /// Discordイベント通知者インターフェース
    /// </summary>
    public interface IDiscordEventNotifier : IEventNotifier
    {
        /// <summary>
        /// メッセージを受信した時に通知されます。
        /// </summary>
        IObservable<SocketMessage> MessageReceived { get; }

        /// <summary>
        /// ログインした時に通知されます。
        /// </summary>
        IObservable<Unit> LoggedIn { get; }

        /// <summary>
        /// ログアウトした時に通知されます。
        /// </summary>
        IObservable<Unit> LoggedOut { get; }
    }
}
