using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Messaging
{
    /// <summary>
    /// メッセージ作成者インターフェース
    /// </summary>
    public interface IAuthor
    {
        /// <summary>
        /// メッセージを作成したユーザーの名前
        /// </summary>
        string AuthorName { get; }
    }
    
    /// <summary>
    /// メッセージ作成者インターフェース（ID付き）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAuthorId<out T> : IAuthor
    {
        /// <summary>
        /// メッセージを作成したユーザーのID
        /// </summary>
        T AuthorId { get; }
    }
}
