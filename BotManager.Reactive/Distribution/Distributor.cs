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
    /// 値分配インターフェース
    /// </summary>
    public interface IDistributor<T> : IObserver<T>, IDisposable
    {
        /// <summary>
        /// <seealso cref="IDistributee{T}"/>オブジェクトを追加します。
        /// </summary>
        /// <param name="distributee"></param>
        /// <returns>購読を停止するオブジェクト</returns>
        IDisposable Add(IDistributee<T> distributee);
    }

    /// <summary>
    /// 値分配クラス
    /// </summary>
    public static class Distributor
    {
        /// <summary>
        /// この値通知を分配します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <returns></returns>
        public static IDistributor<T> ToDistributor<T>(this IObservable<T> observable)
        {
            var distributor = new _Distributor<T>(observable);
            return distributor;
        }

        #region Private Class
        private sealed class _Distributor<T> : IDistributor<T>
        {
            private readonly List<IDistributee<T>> distributees;
            private readonly IDisposable subscription;

            public _Distributor(IObservable<T> observable)
            {
                distributees = new();
                subscription = observable.Subscribe(this);
            }

            public IDisposable Add(IDistributee<T> distributee)
            {
                AddDistributee(distributee);
                return Disposable.Create(this, d => d.RemoveDistributee(distributee));
            }

            private void AddDistributee(IDistributee<T> distributee)
            {
                lock (distributees)
                {
                    distributees.Add(distributee);
                }
            }

            private void RemoveDistributee(IDistributee<T> distributee)
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

            public void OnCompleted()
            {
                lock (distributees)
                {
                    foreach(var d in distributees)
                    {
                        d.OnCompleted();
                    }
                    distributees.Clear();
                }
            }

            public void OnError(Exception error)
            {
                lock (distributees)
                {
                    foreach (var d in distributees)
                    {
                        d.OnError(error);
                    }
                }
            }

            public void OnNext(T value)
            {
                lock (distributees)
                {
                    var query = distributees
                        .Where(d => d.Conditions(value))
                        .OrderByDescending(d => d.Priority)
                        ;

                    var disallowAnothers = query.Where(d => d.DisallowAnotherExecution);
                    if (disallowAnothers.Any())
                    {
                        if(disallowAnothers.Count() >= 2)
                        {
                            // 他処理実行禁止が複数ある場合は実行禁止ではないものを実行する
                            foreach (var distributee in query.Where(d => !d.DisallowAnotherExecution))
                            {
                                distributee.OnNext(value);
                                if (distributee.NoContinuous)
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            // 他処理実行禁止1つだけの場合はそれだけ実行する
                            foreach(var distributee in disallowAnothers)
                            {
                                distributee.OnNext(value);
                                if (distributee.NoContinuous)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        // 他処理実行禁止がない場合は全て実行
                        foreach(var distributee in query)
                        {
                            distributee.OnNext(value);
                            if (distributee.NoContinuous)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
