using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Reactive.Distribution
{
    /// <summary>
    /// 実行条件付きオブザーバーインターフェース
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDistributee<in T> : IObserver<T>
    {
        /// <summary>
        /// 実行優先度。値が高いほど優先度が上がります。
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// この処理を実行した後に他に条件を満たす<seealso cref="IDistributee{T}"/>を実行させないかどうか取得します。
        /// </summary>
        bool NoContinuous { get; }

        /// <summary>
        /// 他の処理を実行する場合、この処理の実行を禁止するかどうか取得します。
        /// </summary>
        bool DisallowAnotherExecution { get; }

        /// <summary>
        /// 実行条件
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Conditions(T value);
    }
}
