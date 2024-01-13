using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Messaging
{
    /// <summary>
    /// 返信待機可能メッセージ
    /// </summary>
    /// <typeparam name="T">受信するメッセージの型</typeparam>
    public interface IWaitableMessage<T> : IMessage where T : IMessage
    {
        /// <summary>
        /// このメッセージに対して返信されたメッセージを通知するオブジェクトを生成します。
        /// </summary>
        IObservable<T> CreateReceiveNotifier();
    }
}
