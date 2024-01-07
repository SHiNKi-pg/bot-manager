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
    /// Botマネージャー機関
    /// </summary>
    public static class Core
    {
        /// <summary>
        /// Botマネージャー機関インスタンスを作成します。
        /// </summary>
        /// <param name="assembly">アセンブリ名</param>
        /// <param name="gettingSubscription">サブスクリプションの引数に設定するオブジェクトを作成する関数</param>
        /// <returns></returns>
        public static IBotMechanism<SubscriptionArguments> Create<SubscriptionArguments>(string assembly, Func<IBotManager, SubscriptionArguments> gettingSubscription)
            where SubscriptionArguments : ISubscriptionArguments, new()
        {
            return new BotMechanism<SubscriptionArguments>(assembly, gettingSubscription);
        }
    }
}
