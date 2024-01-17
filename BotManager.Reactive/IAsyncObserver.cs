using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Reactive
{
    /// <summary>
    /// 非同期オブザーバーインターフェース
    /// </summary>
    public interface IAsyncObserver<in T>
    {
        /// <summary>
        /// 値を受け取った時に実行する処理
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask OnNextAsync(T value, CancellationToken cancellationToken);

        /// <summary>
        /// エラー通知を受け取った時に実行する処理
        /// </summary>
        /// <param name="error"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask OnErrorAsync(Exception error, CancellationToken cancellationToken);

        /// <summary>
        /// 値の通知が完了した時に実行する処理
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask OnCompletedAsync(CancellationToken cancellationToken);
    }
}
