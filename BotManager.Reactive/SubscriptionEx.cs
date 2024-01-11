using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Reactive
{
    /// <summary>
    /// サブスクリプション拡張メソッド
    /// </summary>
    public static class SubscriptionEx
    {
        #region SubscribeUntilDispose
        /// <summary>
        /// 値が通知されると指定した処理を実行するようにします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <param name="onNext">値が通知されると実行される処理。第2引数はこのメソッドの <see cref="IDisposable.Dispose"/>が呼ばれると通知されるオブジェクト。</param>
        /// <returns></returns>
        public static IDisposable InterlockedSubscribe<T>(this IObservable<T> observable, Action<T, IObservable<Unit>> onNext)
        {
            CompositeDisposable disposables = new();
            var notifier = disposables.ToNotifiable();
            disposables.Add(observable.Subscribe(t => onNext(t, notifier)));
            return notifier;
        }

        /// <summary>
        /// 値が通知されると指定した処理を実行するようにします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <param name="onNext">値が通知されると実行される処理。第2引数はこのメソッドの <see cref="IDisposable.Dispose"/>が呼ばれると通知されるオブジェクト。</param>
        /// <param name="onError">エラーが通知された時に実行する処理</param>
        /// <returns></returns>
        public static IDisposable InterlockedSubscribe<T>(this IObservable<T> observable, Action<T, IObservable<Unit>> onNext, Action<Exception> onError)
        {
            CompositeDisposable disposables = new();
            var notifier = disposables.ToNotifiable();
            disposables.Add(observable.Subscribe(t => onNext(t, notifier), onError));
            return notifier;
        }

        /// <summary>
        /// 値が通知されると指定した処理を実行するようにします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <param name="onNext">値が通知されると実行される処理。第2引数はこのメソッドの <see cref="IDisposable.Dispose"/>が呼ばれると通知されるオブジェクト。</param>
        /// <param name="onCompleted">値の発行が完了した時に実行する処理</param>
        /// <returns></returns>
        public static IDisposable InterlockedSubscribe<T>(this IObservable<T> observable, Action<T, IObservable<Unit>> onNext, Action onCompleted)
        {
            CompositeDisposable disposables = new();
            var notifier = disposables.ToNotifiable();
            disposables.Add(observable.Subscribe(t => onNext(t, notifier), onCompleted));
            return notifier;
        }

        /// <summary>
        /// 値が通知されると指定した処理を実行するようにします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <param name="onNext">値が通知されると実行される処理。第2引数はこのメソッドの <see cref="IDisposable.Dispose"/>が呼ばれると通知されるオブジェクト。</param>
        /// <param name="onError">エラーが通知された時に実行する処理</param>
        /// <param name="onCompleted">値の発行が完了した時に実行する処理</param>
        /// <returns></returns>
        public static IDisposable InterlockedSubscribe<T>(this IObservable<T> observable, Action<T, IObservable<Unit>> onNext, Action<Exception> onError, Action onCompleted)
        {
            CompositeDisposable disposables = new();
            var notifier = disposables.ToNotifiable();
            disposables.Add(observable.Subscribe(t => onNext(t, notifier), onError, onCompleted));
            return notifier;
        }
        #endregion
    }
}
