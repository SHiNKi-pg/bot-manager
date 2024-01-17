using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Reactive.Distribution
{
    /// <summary>
    /// <seealso cref="IDistributor{T}"/>拡張メソッド定義
    /// </summary>
    public static class DistributorExtensions
    {
        #region GenerateObservable
        /// <summary>
        /// <seealso cref="IDistributor{T}"/>から<see cref="IObservable{T}"/>オブジェクトを生成します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributor"></param>
        /// <param name="priority">優先度</param>
        /// <param name="conditions">値を流す条件</param>
        /// <param name="noContinuous">値を通知した後、これ以降の<seealso cref="IDistributee{T}"/>を処理しないか</param>
        /// <param name="disallowAnotherExecution">値を通知する際、他の<seealso cref="IDistributee{T}"/>を実行する場合は値の通知をしないかどうか</param>
        /// <returns></returns>
        public static IObservable<T> GenerateObservable<T>(this IDistributor<T> distributor, int priority, Func<T, bool> conditions, bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            return Observable.Create<T>(observer =>
            {
                var distributee = new AnonymousDistributee<T>()
                { 
                    Priority = priority,
                    NoContinuous = noContinuous,
                    DisallowAnotherExecution = disallowAnotherExecution,
                    Condition = conditions,
                    onNext = observer.OnNext,
                    onError = observer.OnError,
                    onCompleted = observer.OnCompleted,
                };
                return distributor.Add(distributee);
            });
        }

        /// <summary>
        /// <seealso cref="IDistributor{T}"/>から<see cref="IObservable{T}"/>オブジェクトを生成します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributor"></param>
        /// <param name="priority">優先度</param>
        /// <param name="noContinuous">値を通知した後、これ以降の<seealso cref="IDistributee{T}"/>を処理しないか</param>
        /// <param name="disallowAnotherExecution">値を通知する際、他の<seealso cref="IDistributee{T}"/>を実行する場合は値の通知をしないかどうか</param>
        /// <returns></returns>
        public static IObservable<T> GenerateObservable<T>(this IDistributor<T> distributor, int priority,  bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            return distributor.GenerateObservable(priority, _ => true, noContinuous, disallowAnotherExecution);
        }
        #endregion

        #region Add
        /// <summary>
        /// <see cref="IObserver{T}"/>に分配するように設定します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributor"></param>
        /// <param name="observer">通知を受け取るオブザーバー</param>
        /// <param name="priority">実行優先度</param>
        /// <param name="condition">値の通知を受ける条件</param>
        /// <param name="noContinuous">値を通知した後、これ以降の<seealso cref="IDistributee{T}"/>を処理しないか</param>
        /// <param name="disallowAnotherExecution">値を通知する際、他の<seealso cref="IDistributee{T}"/>を実行する場合は値の通知をしないかどうか</param>
        /// <returns></returns>
        public static IDisposable Add<T>(this IDistributor<T> distributor, Func<T, bool> condition, IObserver<T> observer, int priority, bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            return distributor.Add(new AnonymousDistributee<T>()
            {
                Priority = priority,
                Condition = condition,
                NoContinuous = noContinuous,
                DisallowAnotherExecution = disallowAnotherExecution,
                onNext = observer.OnNext,
                onCompleted = observer.OnCompleted,
                onError = observer.OnError
            });
        }

        /// <summary>
        /// <see cref="IObserver{T}"/>に分配するように設定します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributor"></param>
        /// <param name="observer">通知を受け取るオブザーバー</param>
        /// <param name="priority">実行優先度</param>
        /// <param name="noContinuous">値を通知した後、これ以降の<seealso cref="IDistributee{T}"/>を処理しないか</param>
        /// <param name="disallowAnotherExecution">値を通知する際、他の<seealso cref="IDistributee{T}"/>を実行する場合は値の通知をしないかどうか</param>
        /// <returns></returns>
        public static IDisposable Add<T>(this IDistributor<T> distributor, IObserver<T> observer, int priority, bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            return distributor.Add(_ => true, observer, priority, noContinuous, disallowAnotherExecution);
        }

        /// <summary>
        /// 値を受け取った時に特定の処理を実行するように設定します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributor"></param>
        /// <param name="onNext">値の通知を受け取った時に実行する処理</param>
        /// <param name="priority">実行優先度</param>
        /// <param name="condition">値の通知を受ける条件</param>
        /// <param name="noContinuous">値を通知した後、これ以降の<seealso cref="IDistributee{T}"/>を処理しないか</param>
        /// <param name="disallowAnotherExecution">値を通知する際、他の<seealso cref="IDistributee{T}"/>を実行する場合は値の通知をしないかどうか</param>
        /// <returns></returns>
        public static IDisposable Add<T>(this IDistributor<T> distributor, Func<T, bool> condition, Action<T> onNext,
            int priority, bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            return distributor.Add(new AnonymousDistributee<T>()
            {
                Priority = priority,
                Condition = condition,
                NoContinuous = noContinuous,
                DisallowAnotherExecution = disallowAnotherExecution,
                onNext = onNext,
            });
        }

        /// <summary>
        /// 値を受け取った時に特定の処理を実行するように設定します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributor"></param>
        /// <param name="onNext">値の通知を受け取った時に実行する処理</param>
        /// <param name="priority">実行優先度</param>
        /// <param name="noContinuous">値を通知した後、これ以降の<seealso cref="IDistributee{T}"/>を処理しないか</param>
        /// <param name="disallowAnotherExecution">値を通知する際、他の<seealso cref="IDistributee{T}"/>を実行する場合は値の通知をしないかどうか</param>
        /// <returns></returns>
        public static IDisposable Add<T>(this IDistributor<T> distributor, Action<T> onNext,
            int priority, bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            return distributor.Add(_ => true, onNext, priority, noContinuous, disallowAnotherExecution);
        }

        /// <summary>
        /// 値を受け取った時に特定の処理を実行するように設定します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributor"></param>
        /// <param name="onNext">値の通知を受け取った時に実行する処理</param>
        /// <param name="onError">エラー通知を受け取った時に実行する処理</param>
        /// <param name="priority">実行優先度</param>
        /// <param name="condition">値の通知を受ける条件</param>
        /// <param name="noContinuous">値を通知した後、これ以降の<seealso cref="IDistributee{T}"/>を処理しないか</param>
        /// <param name="disallowAnotherExecution">値を通知する際、他の<seealso cref="IDistributee{T}"/>を実行する場合は値の通知をしないかどうか</param>
        /// <returns></returns>
        public static IDisposable Add<T>(this IDistributor<T> distributor, Func<T, bool> condition, Action<T> onNext, Action<Exception> onError,
            int priority, bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            return distributor.Add(new AnonymousDistributee<T>()
            {
                Priority = priority,
                Condition = condition,
                NoContinuous = noContinuous,
                DisallowAnotherExecution = disallowAnotherExecution,
                onNext = onNext,
                onError = onError,
            });
        }

        /// <summary>
        /// 値を受け取った時に特定の処理を実行するように設定します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributor"></param>
        /// <param name="onNext">値の通知を受け取った時に実行する処理</param>
        /// <param name="onError">エラー通知を受け取った時に実行する処理</param>
        /// <param name="priority">実行優先度</param>
        /// <param name="noContinuous">値を通知した後、これ以降の<seealso cref="IDistributee{T}"/>を処理しないか</param>
        /// <param name="disallowAnotherExecution">値を通知する際、他の<seealso cref="IDistributee{T}"/>を実行する場合は値の通知をしないかどうか</param>
        /// <returns></returns>
        public static IDisposable Add<T>(this IDistributor<T> distributor, Action<T> onNext, Action<Exception> onError,
            int priority, bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            return distributor.Add(_ => true, onNext, onError, priority, noContinuous, disallowAnotherExecution);
        }

        /// <summary>
        /// 値を受け取った時に特定の処理を実行するように設定します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributor"></param>
        /// <param name="onNext">値の通知を受け取った時に実行する処理</param>
        /// <param name="onCompleted">値の通知が完了した時に実行する処理</param>
        /// <param name="priority">実行優先度</param>
        /// <param name="condition">値の通知を受ける条件</param>
        /// <param name="noContinuous">値を通知した後、これ以降の<seealso cref="IDistributee{T}"/>を処理しないか</param>
        /// <param name="disallowAnotherExecution">値を通知する際、他の<seealso cref="IDistributee{T}"/>を実行する場合は値の通知をしないかどうか</param>
        /// <returns></returns>
        public static IDisposable Add<T>(this IDistributor<T> distributor, Func<T, bool> condition, Action<T> onNext, Action onCompleted,
            int priority, bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            return distributor.Add(new AnonymousDistributee<T>()
            {
                Priority = priority,
                Condition = condition,
                NoContinuous = noContinuous,
                DisallowAnotherExecution = disallowAnotherExecution,
                onNext = onNext,
                onCompleted = onCompleted,
            });
        }

        /// <summary>
        /// 値を受け取った時に特定の処理を実行するように設定します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributor"></param>
        /// <param name="onNext">値の通知を受け取った時に実行する処理</param>
        /// <param name="onCompleted">値の通知が完了した時に実行する処理</param>
        /// <param name="priority">実行優先度</param>
        /// <param name="noContinuous">値を通知した後、これ以降の<seealso cref="IDistributee{T}"/>を処理しないか</param>
        /// <param name="disallowAnotherExecution">値を通知する際、他の<seealso cref="IDistributee{T}"/>を実行する場合は値の通知をしないかどうか</param>
        /// <returns></returns>
        public static IDisposable Add<T>(this IDistributor<T> distributor, Action<T> onNext, Action onCompleted,
            int priority, bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            return distributor.Add(_ => true, onNext, onCompleted, priority, noContinuous, disallowAnotherExecution);
        }

        /// <summary>
        /// 値を受け取った時に特定の処理を実行するように設定します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributor"></param>
        /// <param name="onNext">値の通知を受け取った時に実行する処理</param>
        /// <param name="onError">エラー通知を受け取った時に実行する処理</param>
        /// <param name="onCompleted">値の通知が完了した時に実行する処理</param>
        /// <param name="priority">実行優先度</param>
        /// <param name="condition">値の通知を受ける条件</param>
        /// <param name="noContinuous">値を通知した後、これ以降の<seealso cref="IDistributee{T}"/>を処理しないか</param>
        /// <param name="disallowAnotherExecution">値を通知する際、他の<seealso cref="IDistributee{T}"/>を実行する場合は値の通知をしないかどうか</param>
        /// <returns></returns>
        public static IDisposable Add<T>(this IDistributor<T> distributor, Func<T, bool> condition, Action<T> onNext, Action<Exception> onError, Action onCompleted,
            int priority, bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            return distributor.Add(new AnonymousDistributee<T>()
            {
                Priority = priority,
                Condition = condition,
                NoContinuous = noContinuous,
                DisallowAnotherExecution = disallowAnotherExecution,
                onNext = onNext,
                onError = onError,
                onCompleted = onCompleted,
            });
        }

        /// <summary>
        /// 値を受け取った時に特定の処理を実行するように設定します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="distributor"></param>
        /// <param name="onNext">値の通知を受け取った時に実行する処理</param>
        /// <param name="onError">エラー通知を受け取った時に実行する処理</param>
        /// <param name="onCompleted">値の通知が完了した時に実行する処理</param>
        /// <param name="priority">実行優先度</param>
        /// <param name="noContinuous">値を通知した後、これ以降の<seealso cref="IDistributee{T}"/>を処理しないか</param>
        /// <param name="disallowAnotherExecution">値を通知する際、他の<seealso cref="IDistributee{T}"/>を実行する場合は値の通知をしないかどうか</param>
        /// <returns></returns>
        public static IDisposable Add<T>(this IDistributor<T> distributor, Action<T> onNext, Action<Exception> onError, Action onCompleted,
            int priority, bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            return distributor.Add(_ => true, onNext, onError, onCompleted, priority, noContinuous, disallowAnotherExecution);
        }
        #endregion

        #region Private Class

        private class AnonymousDistributee<T> : IDistributee<T>
        {
            public required int Priority { get; init; }

            public bool NoContinuous { get; init; } = false;

            public bool DisallowAnotherExecution { get; init; } = false;

            public required Func<T, bool> Condition { get; init; }

            public required Action<T> onNext { get; init; }

            public Action<Exception>? onError { get; init; }

            public Action? onCompleted { get; init; }

            public bool Conditions(T value)
            {
                return Condition(value);
            }

            public void OnCompleted()
            {
                if(onCompleted is not null)
                    onCompleted();
            }

            public void OnError(Exception error)
            {
               if(onError is not null)
                    onError(error);
            }

            public void OnNext(T value)
            {
                OnNext(value);
            }
        }
        #endregion
    }
}
