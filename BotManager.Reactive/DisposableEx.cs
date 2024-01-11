using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
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

        /// <summary>
        /// このオブジェクトの <see cref="IDisposable.Dispose"/>が呼び出されると通知されるオブジェクトを作成します。
        /// </summary>
        /// <param name="baseDisposable"></param>
        /// <returns></returns>
        public static IDisposalNotifier ToNotifiable(this IDisposable baseDisposable)
        {
            return new DisposalNotifier(baseDisposable);
        }

        #region Then

        /// <summary>
        /// <see cref="IDisposable.Dispose"/>を呼び出したときに実行する処理を登録します。
        /// </summary>
        /// <param name="baseDisposable"></param>
        /// <param name="dispose">実行する処理</param>
        /// <returns></returns>
        public static IDisposable Then(this IDisposable baseDisposable, Action dispose)
        {
            CompositeDisposable disposables = new();
            disposables.Add(baseDisposable);
            disposables.Add(Disposable.Create(dispose));
            return disposables;
        }

        /// <summary>
        /// <see cref="IDisposable.Dispose"/>を呼び出したときに実行する処理を登録します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="disposable"></param>
        /// <param name="dispose">実行する処理</param>
        /// <returns></returns>
        public static IDisposable Then<T>(this T disposable, Action<T> dispose) where T : IDisposable
        {
            CompositeDisposable disposables = new();
            disposables.Add(disposable);
            disposables.Add(Disposable.Create(disposable, dispose));
            return disposables;
        }
        #endregion
    }
}
