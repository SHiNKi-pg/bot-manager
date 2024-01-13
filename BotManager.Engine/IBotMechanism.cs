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
    /// BotManager機関インターフェース
    /// </summary>
    public interface IBotMechanism<SubscriptionArguments> : IDisposable
        where SubscriptionArguments : ISubscriptionArguments, new()
    {
        /// <summary>
        /// ソースをコンパイルし、成功したらBotスクリプトを開始します。
        /// </summary>
        Task CompileSources();

        /// <summary>
        /// コンパイラを取得します。これは通常、スクリプトがコンパイルできるかどうかチェックするために使用されます。
        /// </summary>
        IPrecompilable PreCompiler { get; }
    }
}
