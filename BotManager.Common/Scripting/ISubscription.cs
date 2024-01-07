using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Scripting
{
    /// <summary>
    /// サブスクリプションインターフェース
    /// </summary>
    public interface ISubscription<Args> : INamed where Args : ISubscriptionArguments
    {
        /// <summary>
        /// イベントを購読します。
        /// </summary>
        /// <param name="args">通知元のオブジェクト等を含むオブジェクト</param>
        /// <returns></returns>
        IDisposable SubscribeFrom(Args args);
    }
}
