using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Reactive
{
    /// <summary>
    /// <see cref="IDisposable"/>拡張メソッド定義用クラス
    /// </summary>
    public static class DisposableEx
    {
        /// <summary>
        /// 指定したストリームからデータが通知された場合に このオブジェクトの<seealso cref="IDisposable.Dispose"/>を呼び出すように登録します。
        /// </summary>
        /// <typeparam name="TSignal"></typeparam>
        /// <param name="disposable"></param>
        /// <param name="observable">このオブジェクトの <see cref="IDisposable.Dispose"/>を呼び出すタイミング</param>
        /// <returns><see cref="DisposeOn{TSignal}(IDisposable, IObservable{TSignal})"/>で登録した破棄予約を解除するためのオブジェクト。</returns>
        public static IDisposable DisposeOn<TSignal>(this IDisposable disposable, IObservable<TSignal> observable)
        {
            return observable
                .Take(1)
                .Subscribe(_ => disposable.Dispose());
        }
    }
}
