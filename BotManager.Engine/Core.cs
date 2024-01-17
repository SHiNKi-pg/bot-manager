using BotManager.Common;
using BotManager.Common.Scripting;
using BotManager.Service.Compiler;
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
        /// <param name="compiler">ソースのコンパイルで使用するコンパイラオブジェクト</param>
        /// <param name="gettingSubscription">サブスクリプションの引数に設定するオブジェクトを作成する関数。第1引数はサブスクリプションの IDと名称を表します。</param>
        /// <returns></returns>
        public static IBotMechanism<SubscriptionArguments> Create<SubscriptionArguments>(IPrecompilableCompiler compiler, Func<INamed, IBotManager, CancellationToken, SubscriptionArguments> gettingSubscription)
            where SubscriptionArguments : ISubscriptionArguments, new()
        {
            return new BotMechanism<SubscriptionArguments>(compiler, gettingSubscription);
        }
    }
}
