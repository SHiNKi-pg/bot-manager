using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Reactive
{
    /// <summary>
    /// <see cref="IObservable{T}"/>拡張メソッド定義
    /// </summary>
    public static class ObservableExtensions
    {
        #region Once
        /// <summary>
        /// オブザーバブルからの通知を1度だけ購読します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <returns></returns>
        public static IObservable<T> Once<T>(this IObservable<T> observable)
        {
            return observable.Take(1);
        }
        #endregion

        #region WaitCompleteAsync
        /// <summary>
        /// オブザーバブルの通知の完了を待ちます。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <returns></returns>
        public static async Task WaitCompleteAsync<T>(this IObservable<T> observable)
        {
            await observable.Count();
        }
        #endregion

        #region GetCompleteObservable
        /// <summary>
        /// オブザーバブルの通知が完了したことを通知するオブザーバブルを返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <returns></returns>
        public static IObservable<Unit> GetCompleteObservable<T>(this IObservable<T> observable)
        {
            return Observable.Create<Unit>(observer =>
            {
                return observable.Subscribe(_ => { },
                    observer.OnError,
                    () =>
                    {
                        observer.OnNext(Unit.Default);
                        observer.OnCompleted();
                    }
                );
            });
        }
        #endregion

        #region TimeoutThenComplete
        /// <summary>
        /// 指定した時間が経過した場合、強制的に完了させます。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <param name="dueTime">タイムアウト期間</param>
        /// <returns></returns>
        public static IObservable<T> TimeoutThenComplete<T>(this IObservable<T> observable, TimeSpan dueTime)
        {
            return observable.Timeout(dueTime, Observable.Empty<T>());
        }
        #endregion
    }
}
