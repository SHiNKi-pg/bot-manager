using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Mutex
{
    /// <summary>
    /// 非同期ミューテックスインターフェース
    /// </summary>
    public interface IAsyncMutex : IAsyncDisposable
    {
        /// <summary>
        /// ミューテックスを取得します。
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AcquireAsync(CancellationToken cancellationToken);

        /// <summary>
        /// ミューテックスを解放します。
        /// </summary>
        /// <returns></returns>
        Task ReleaseAsync();
    }

    /// <summary>
    /// 非同期ミューテックスクラス
    /// </summary>
    internal sealed class AsyncMutex : IAsyncMutex
    {
        #region Fields
        private readonly string _name;
        private Task? _mutexTask;
        private ManualResetEventSlim? _releaseEvent;
        private CancellationTokenSource? _cancellationTokenSource;
        #endregion

        #region Constructor
        /// <summary>
        /// <see cref="AsyncMutex"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="name"></param>
        public AsyncMutex(string name)
        {
            _name = name;
        }
        #endregion

        #region Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AcquireAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            TaskCompletionSource taskCompletionSource = new();

            _releaseEvent = new ManualResetEventSlim();
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            _mutexTask = Task.Factory.StartNew(
                state =>
                {
                    try
                    {
                        CancellationToken ct = _cancellationTokenSource.Token;
                        using var mutex = new System.Threading.Mutex(false, _name);
                        try
                        {
                            if (WaitHandle.WaitAny(new[] { mutex, ct.WaitHandle }) != 0)
                            {
                                taskCompletionSource.SetCanceled(ct);
                                return;
                            }
                        }
                        catch (AbandonedMutexException)
                        {

                        }

                        taskCompletionSource.SetResult();

                        _releaseEvent.Wait();
                        mutex.ReleaseMutex();
                    }
                    catch (OperationCanceledException)
                    {
                        taskCompletionSource.TrySetCanceled(cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        taskCompletionSource.TrySetException(ex);
                    }
                },
                state: null,
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task ReleaseAsync()
        {
            _releaseEvent?.Set();

            if (_mutexTask != null)
            {
                await _mutexTask;
            }
        }
        #endregion

        #region DisposeAsync
        public async ValueTask DisposeAsync()
        {
            _cancellationTokenSource?.Cancel();

            await ReleaseAsync();

            _releaseEvent?.Dispose();
            _cancellationTokenSource?.Dispose();
        }
        #endregion
    }
}
