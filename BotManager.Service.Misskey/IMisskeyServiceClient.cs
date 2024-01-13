using BotManager.Common;
using BotManager.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey
{
    /// <summary>
    /// Misskey Bot用クライアントインターフェース
    /// </summary>
    public interface IMisskeyServiceClient : IServiceClient, IMisskeyEventNotifier, ISendable, IMessageReceived<IReplyableMessageWithId<string>>
    {
        /// <summary>
        /// Misskey HTTP APIにアクセスするオブジェクトを取得します。
        /// </summary>
        /// <returns></returns>
        IMisskeyApi Api { get; }
    }
}
