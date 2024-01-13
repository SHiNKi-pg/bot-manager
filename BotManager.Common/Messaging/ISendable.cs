using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Messaging
{
    /// <summary>
    /// メッセージ送信可能インターフェース
    /// </summary>
    public interface ISendable
    {
        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="content">投稿メッセージ</param>
        /// <returns></returns>
        Task<IMessage> Send(string content);
    }
}
