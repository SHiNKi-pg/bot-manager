using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common
{
    /// <summary>
    /// 一括送信機能付きBot
    /// </summary>
    public interface ISendableBot : IBot
    {
        /// <summary>
        /// メッセージを投稿します。
        /// </summary>
        /// <param name="context">メッセージ本文</param>
        /// <returns></returns>
        Task Send(string context);
    }
}
