using BotManager.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common
{
    /// <summary>
    /// メッセージ受信可能機能付きBot
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReceivableMessageBot<out T> : IBot
        where T : IMessaging
    {
        /// <summary>
        /// メッセージを受信すると通知するオブジェクトを生成します。
        /// </summary>
        /// <returns></returns>
        IObservable<T> CreateMessageNotifier();
    }
}
