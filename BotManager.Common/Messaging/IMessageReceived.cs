using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Messaging
{
    /// <summary>
    /// 返信可能メッセージ受信可能インターフェース
    /// </summary>
    public interface IMessageReceived<out T> where T : IMessage
    {
        /// <summary>
        /// メッセージを受信したことを通知します。
        /// </summary>
        IObservable<T> MessageReceived { get; }
    }
}
