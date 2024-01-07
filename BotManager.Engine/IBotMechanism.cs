using BotManager.Common.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Engine
{
    /// <summary>
    /// BotManager機関インターフェース
    /// </summary>
    public interface IBotMechanism<SubscriptionArguments> : IDisposable
        where SubscriptionArguments : ISubscriptionArguments, new()
    {
        /// <summary>
        /// Bot機能を開始します。
        /// </summary>
        Task Start();

        /// <summary>
        /// <seealso cref="ISubscription.SubscribeFrom(ISubscriptionArguments)"/>の引数を設定します。
        /// </summary>
        /// <param name="settingArguments">引数設定。 <seealso cref="ISubscriptionArguments.BotManager"/>は設定する必要はありません。</param>
        void SetSubscriptionArgument(Action<SubscriptionArguments> settingArguments);
    }
}
