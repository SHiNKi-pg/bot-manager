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
        /// <param name="cancellationToken">処理がキャンセルされていない場合はtrue、それ以外はfalse。</param>
        /// <returns></returns>
        public static async Task<bool> WaitAsync(string mutexName, Func<Task> action, CancellationToken cancellationToken)
        {
            await using IAsyncMutex mutex = new AsyncMutex(mutexName);

            try
            {
                await mutex.AcquireAsync(cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                {
                    return false;
                }
                await action();
                return true;
            }
            catch (TaskCanceledException)
            {
                return false;
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
        /// <param name="cancellationToken"></param>
        /// <returns>処理がキャンセルされていない場合はtrue、それ以外はfalse。</returns>
        public static async Task<bool> WaitAsync(string mutexName, Func<CancellationToken, Task> action, CancellationToken cancellationToken)
        {
            await using IAsyncMutex mutex = new AsyncMutex(mutexName);

            try
            {
                await mutex.AcquireAsync(cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                {
                    return false;
                }
                await action(cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                {
                    return false;
                }
                return true;
            }
            catch (TaskCanceledException)
            {
                return false;
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
        /// <returns>処理がキャンセルされていない場合はtrue、それ以外はfalse。</returns>
        public static Task<bool> WaitAsync(string mutexName, Func<Task> action)
        {
            return WaitAsync(mutexName, action, CancellationToken.None);
        }

        /// <summary>
        /// ミューテックスが解放されるまで待機します。
        /// </summary>
        /// <param name="mutexName">ミューテックス名</param>
        /// <param name="cancellationToken"></param>
        /// <returns>途中でキャンセルが要求された場合は true、それ以外はfalse。</returns>
        public static Task<bool> WaitAsync(string mutexName, CancellationToken cancellationToken)
        {
            return WaitAsync(mutexName, () => Task.CompletedTask, cancellationToken);
        }
    }
}
