using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BotManager.Reactive.Distribution
{
    /// <summary>
    /// 非同期値分配インターフェース
    /// </summary>
    public interface IAsyncDistributor<T> : IDisposable, IAsyncObserver<T>
    {
        /// <summary>
        /// <seealso cref="IAsyncDistributee{T}"/>オブジェクトを追加します。
        /// </summary>
        /// <param name="distributee"></param>
        /// <returns>購読を停止するオブジェクト</returns>
        IDisposable Add(IAsyncDistributee<T> distributee);
    }

    /// <summary>
    /// 非同期値分配クラス
    /// </summary>
    public static class AsyncDistributor
    {
        /// <summary>
        /// この値通知を分配します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static IAsyncDistributor<T> ToAsyncDistributor<T>(this IObservable<T> observable, CancellationToken cancellationToken)
        {
            var distributor = new _AsyncDistributor<T>(observable, cancellationToken);
            return distributor;
        }

        /// <summary>
        /// この値通知を分配します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <returns></returns>
        public static IAsyncDistributor<T> ToAsyncDistributor<T>(this IObservable<T> observable)
        {
            return observable.ToAsyncDistributor(CancellationToken.None);
        }

        #region Private Class
        private sealed class _AsyncDistributor<T> : IAsyncDistributor<T>
        {
            private readonly List<IAsyncDistributee<T>> distributees;
            private readonly IDisposable subscription;

            public _AsyncDistributor(IObservable<T> observable, CancellationToken cancellationToken)
            {
                distributees = new();
                subscription = observable.Subscribe(
                    async n => await OnNextAsync(n, cancellationToken),
                    async e => await OnErrorAsync(e, cancellationToken),
                    async () => await OnCompletedAsync(cancellationToken)
                    );
            }

            public IDisposable Add(IAsyncDistributee<T> distributee)
            {
                AddDistributee(distributee);
                return Disposable.Create(this, d => d.RemoveDistributee(distributee));
            }

            private void AddDistributee(IAsyncDistributee<T> distributee)
            {
                lock (distributees)
                {
                    distributees.Add(distributee);
                }
            }

            private void RemoveDistributee(IAsyncDistributee<T> distributee)
            {
                lock (distributees)
                {
                    distributees.Remove(distributee);
                }
            }

            public void Dispose()
            {
                subscription.Dispose();
                distributees.Clear();
            }

            public async ValueTask OnCompletedAsync(CancellationToken cancellationToken)
            {
                foreach (var d in distributees.ToList())
                {
                    await d.OnCompletedAsync(cancellationToken);
                }
                distributees.Clear();
            }

            public async ValueTask OnErrorAsync(Exception error, CancellationToken cancellationToken)
            {
                foreach (var d in distributees.ToList())
                {
                    await d.OnErrorAsync(error, cancellationToken);
                }
            }

            public async ValueTask OnNextAsync(T value, CancellationToken cancellationToken)
            {
                var query = distributees
                        .Where(d => d.Conditions(value))
                        .OrderByDescending(d => d.Priority)
                        .ToList()
                        ;

                var disallowAnothers = query.Where(d => d.DisallowAnotherExecution);
                if (disallowAnothers.Any())
                {
                    if (disallowAnothers.Count() >= 2)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }
                        // 他処理実行禁止が複数ある場合は実行禁止ではないものを実行する
                        foreach (var distributee in query.Where(d => !d.DisallowAnotherExecution))
                        {
                            await distributee.OnNextAsync(value, cancellationToken);
                            if (distributee.NoContinuous || cancellationToken.IsCancellationRequested)
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }
                        // 他処理実行禁止1つだけの場合はそれだけ実行する
                        foreach (var distributee in disallowAnothers)
                        {
                            await distributee.OnNextAsync(value, cancellationToken);
                            if (distributee.NoContinuous || cancellationToken.IsCancellationRequested)
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                    // 他処理実行禁止がない場合は全て実行
                    foreach (var distributee in query)
                    {
                        await distributee.OnNextAsync(value, cancellationToken);
                        if (distributee.NoContinuous || cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
