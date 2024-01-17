using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Reactive.Distribution
{
    /// <summary>
    /// 値分配インターフェース
    /// </summary>
    public interface IDistributor<T> : IObserver<T>, IDisposable
    {
        /// <summary>
        /// <seealso cref="IDistributee{T}"/>オブジェクトを追加します。
        /// </summary>
        /// <param name="distributee"></param>
        /// <returns></returns>
        IDisposable Add(IDistributee<T> distributee);
    }
}
