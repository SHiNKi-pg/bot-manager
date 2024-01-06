using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Reactive
{
    /// <summary>
    /// リアクティブユーティリティ拡張メソッド定義クラス
    /// </summary>
    public static class RxUtility
    {
        /// <summary>
        /// 流れてくる値が null でなければその値を後続に流します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IObservable<T> IsNotNull<T>(this IObservable<T?> source)
        {
            return Observable.Create<T>(observer =>
            {
                return source.Subscribe(value =>
                {
                    if (value is not null)
                        observer.OnNext(value);
                },
                observer.OnError,
                observer.OnCompleted);
            });
        }

        /// <summary>
        /// 流れてくる値が null でなければその値を後続に流します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IObservable<T> IsNotNull<T>(this IObservable<T?> source) where T : struct
        {
            return Observable.Create<T>(observer =>
            {
                return source.Subscribe(value =>
                {
                    if (value.HasValue)
                        observer.OnNext(value.Value);
                },
                observer.OnError,
                observer.OnCompleted);
            });
        }
    }
}
