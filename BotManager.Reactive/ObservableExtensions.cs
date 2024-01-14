using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
