using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common
{
    /// <summary>
    /// 汎用 <see cref="IAsyncDisposable"/>
    /// </summary>
    public class AsyncDisposable
    {
        #region Creation
        /// <summary>
        /// <see cref="IAsyncDisposable.DisposeAsync"/>を呼び出したときに特定の処理を実行するようにします。
        /// </summary>
        /// <param name="disposeAsync"><see cref="IAsyncDisposable.DisposeAsync"/>を呼び出したときに実行する処理</param>
        /// <returns></returns>
        public static IAsyncDisposable Create(Func<ValueTask> disposeAsync)
        {
            return new AnonymousAsyncDisposable(disposeAsync);
        }

        /// <summary>
        /// <see cref="IAsyncDisposable.DisposeAsync"/>を呼び出したときに特定の処理を実行するようにします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="state"></param>
        /// <param name="disposeAsync"><see cref="IAsyncDisposable.DisposeAsync"/>を呼び出したときに実行する処理</param>
        /// <returns></returns>
        public static IAsyncDisposable Create<T>(T state, Func<T, ValueTask> disposeAsync)
        {
            return new AnonymousAsyncDisposable<T>(state, disposeAsync);
        }
        #endregion

        #region Private Class
        private class AnonymousAsyncDisposable<T> : IAsyncDisposable
        {
            private T state;
            private Func<T, ValueTask> disposeAsync;

            public AnonymousAsyncDisposable(T state, Func<T, ValueTask> disposeAsync)
            {
                this.state = state;
                this.disposeAsync = disposeAsync;
            }

            public ValueTask DisposeAsync()
            {
                return disposeAsync(state);
            }
        }

        private class AnonymousAsyncDisposable : IAsyncDisposable
        {
            private Func<ValueTask> disposableAsync;

            public AnonymousAsyncDisposable(Func<ValueTask> disposableAsync)
            {
                this.disposableAsync = disposableAsync;
            }

            public ValueTask DisposeAsync()
            {
                return disposableAsync();
            }
        }
        #endregion
    }
}
