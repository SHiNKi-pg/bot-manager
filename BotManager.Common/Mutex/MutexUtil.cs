using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Mutex
{
    /// <summary>
    /// ミューテックスユーティリティクラス
    /// </summary>
    public static class MutexUtility
    {
        /// <summary>
        /// 指定した処理を排他的に実行します。
        /// </summary>
        /// <param name="mutexName">ミューテックス名</param>
        /// <param name="action">処理</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task WaitAsync(string mutexName, Func<Task> action, CancellationToken cancellationToken)
        {
            await using IAsyncMutex mutex = new AsyncMutex(mutexName);

            try
            {
                await mutex.AcquireAsync(cancellationToken);
                await action();
            }
            finally
            {
                await mutex.ReleaseAsync();
            }
        }

        /// <summary>
        /// 指定した処理を排他的に実行します。
        /// </summary>
        /// <param name="mutexName">ミューテックス名</param>
        /// <param name="action">処理</param>
        /// <returns></returns>
        public static Task WaitAsync(string mutexName, Func<Task> action)
        {
            return WaitAsync(mutexName, action, CancellationToken.None);
        }
    }
}
