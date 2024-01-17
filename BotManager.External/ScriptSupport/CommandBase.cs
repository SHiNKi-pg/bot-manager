using BotManager.Reactive.Distribution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.External.ScriptSupport
{
    /// <summary>
    /// <seealso cref="IDistributor{T}"/>から通知を受け取った時に実行する処理を設定する機能を持つクラス。
    /// 例えば受け取ったメッセージの内容によって処理を分配する時などに使用されます。
    /// </summary>
    /// <typeparam name="T">受け取る型</typeparam>
    public abstract class CommandBase<T> : IAsyncDistributee<T>
    {
        #region Property
        /// <summary>
        /// 実行優先度。高い数値であるほど実行順序は上の方になります。
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// この処理を実行した後、このイベント通知について他の処理を実行させないかどうかを取得します。
        /// </summary>
        public bool NoContinuous { get; }

        /// <summary>
        /// このイベント通知について、他の処理を実行する場合はこの処理の実行を許可しないかどうかを取得します。
        /// </summary>
        public bool DisallowAnotherExecution { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// <see cref="CommandBase{T}"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="priority">実行優先度。高い数値であるほど実行順序は上の方になります。</param>
        /// <param name="noContinuous">この処理を実行した後、このイベント通知について他の処理を実行させないかどうかを取得します。</param>
        /// <param name="disallowAnotherExecution">このイベント通知について、他の処理を実行する場合はこの処理の実行を許可しないかどうかを取得します。</param>
        public CommandBase(int priority, bool noContinuous = false, bool disallowAnotherExecution = false)
        {
            this.Priority = priority;
            this.NoContinuous = noContinuous;
            this.DisallowAnotherExecution = disallowAnotherExecution;
        }
        #endregion

        #region Method
        /// <summary>
        /// <seealso cref="OnNextAsync(T, CancellationToken)"/>を実行する条件
        /// </summary>
        /// <param name="value">受け取った値</param>
        /// <returns></returns>
        public abstract bool Conditions(T value);

        /// <summary>
        /// 値を受け取った時に実行する処理
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract ValueTask OnNextAsync(T value, CancellationToken cancellationToken);

        /// <summary>
        /// エラー通知を受け取った時に実行する処理
        /// </summary>
        /// <param name="error"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual ValueTask OnErrorAsync(Exception error, CancellationToken cancellationToken)
        {
            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// 値の通知が完了した時に実行する処理
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual ValueTask OnCompletedAsync(CancellationToken cancellationToken)
        {
            return ValueTask.CompletedTask;
        }
        #endregion
    }
}
