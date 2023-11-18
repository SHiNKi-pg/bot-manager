using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common
{
    /// <summary>
    /// サービス提供Botインターフェース
    /// </summary>
    /// <typeparam name="TClient"><seealso cref="IServiceClient"/>を実装したクラスまたはインターフェース</typeparam>
    public interface IServiceBot<TClient> : IBot
        where TClient : IServiceClient
    {
        /// <summary>
        /// サービスクライアントオブジェクトを取得します。
        /// </summary>
        TClient Client { get; }
    }
}
