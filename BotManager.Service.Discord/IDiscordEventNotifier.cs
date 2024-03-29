﻿using BotManager.Common;
using BotManager.Service.Discord.Event;
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

        /// <summary>
        /// サーバーデータが取得できた時に通知されます。
        /// </summary>
        IObservable<Unit> Ready { get; }

        /// <summary>
        /// メッセージにリアクションが付いた時に通知されます。
        /// </summary>
        IObservable<ReactionAddedEventArgs> ReactionAdded { get; }

        /// <summary>
        /// コンポーネントのボタンが押された時に通知されます。
        /// </summary>
        IObservable<SocketMessageComponent> ButtonExecuted { get; }

        /// <summary>
        /// メッセージが削除されると通知されます。
        /// </summary>
        IObservable<MessageDeletedEventArgs> MessageDeleted { get; }

        /// <summary>
        /// メッセージが更新されると通知されます。
        /// </summary>
        IObservable<MessageUpdatedEventArgs> MessageUpdated { get; }

        /// <summary>
        /// モーダルが送信されると通知されます。
        /// </summary>
        IObservable<SocketModal> ModalSubmitted { get; }

        /// <summary>
        /// ドロップダウンリストからデータを選択した時に通知されます。
        /// </summary>
        IObservable<SocketMessageComponent> SelectMenuExecuted { get; }

        /// <summary>
        /// サーバーにユーザーが参加すると通知されます。
        /// </summary>
        IObservable<SocketGuildUser> UserJoined { get; }

        /// <summary>
        /// ユーザーがサーバーから離脱すると通知されます。
        /// </summary>
        IObservable<UserLeftEventArgs> UserLeft { get; }
    }
}
