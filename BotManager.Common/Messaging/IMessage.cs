using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Messaging
{
    /// <summary>
    /// メッセージインターフェース
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// メッセージ内容
        /// </summary>
        string Content { get; }

        /// <summary>
        /// 受信日時
        /// </summary>
        DateTime ReceivedTime { get; }
    }

    /// <summary>
    /// 返信可能メッセージインターフェース
    /// </summary>
    public interface IReplyableMessage : IMessage
    {
        /// <summary>
        /// このメッセージに対して返信します。
        /// </summary>
        /// <param name="content">返信メッセージ</param>
        /// <returns></returns>
        Task<IReplyableMessage> Reply(string content);
    }

    /// <summary>
    /// 削除可能メッセージインターフェース
    /// </summary>
    public interface IDeletableMessage : IMessage
    {
        /// <summary>
        /// このメッセージを削除します。
        /// </summary>
        /// <returns></returns>
        Task Delete();
    }
}
