using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Messaging
{
    /// <summary>
    /// IDプロパティを持つメッセージインターフェース
    /// </summary>
    /// <typeparam name="T">IDの型</typeparam>
    public interface IMessageId<out T> : IMessaging
    {
        /// <summary>
        /// メッセージID
        /// </summary>
        T Id { get; }
    }
}
