using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Scripting
{
    /// <summary>
    /// サブスクリプションオプション引数インターフェース
    /// </summary>
    public interface ISubscriptionArguments
    {
        /// <summary>
        /// Botマネージャ
        /// </summary>
        IBotManager BotManager { get; }
    }
}
