using BotManager.Common;
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
    }
}
