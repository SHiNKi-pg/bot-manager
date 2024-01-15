using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Extensions
{
    /// <summary>
    /// 値通知インターフェース
    /// </summary>
    /// <typeparam name="TSender"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IValueSender<out TSender, out TValue> : IValue<TValue>
    {
        /// <summary>
        /// 値を通知したオブジェクトを取得します。
        /// </summary>
        TSender Sender { get; }
    }
}
