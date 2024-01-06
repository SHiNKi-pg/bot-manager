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
    /// サブスクリプション引数
    /// </summary>
    internal class SubscriptionArguments : ISubscriptionArguments
    {
        public required IBotManager BotManager { get; init; }
    }
}
