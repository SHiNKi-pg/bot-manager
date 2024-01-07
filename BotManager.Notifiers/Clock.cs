using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Notifiers
{
    /// <summary>
    /// 現在の時刻の通知を行うタイマークラス
    /// </summary>
    public class Clock : IObservable<DateTime>, IDisposable
    {
        private readonly IConnectableObservable<DateTime> timer;
        private readonly IDisposable timerConnection;

        #region Constructor
        /// <summary>
        /// <see cref="Clock"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="offsetSeconds">実際の時刻からずらす秒数</param>
        public Clock(double offsetSeconds)
        {
            timer = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Select(_ => DateTime.Now.AddSeconds(offsetSeconds))
                .Publish();
            timerConnection = timer.Connect();
        }

        /// <summary>
        /// 現在時刻を通知する<see cref="Clock"/>オブジェクトを作成します。
        /// </summary>
        public Clock() : this(0) { }
        #endregion

        /// <summary>
        /// 時刻の通知を停止します。
        /// </summary>
        public void Dispose()
        {
            timerConnection.Dispose();
        }

        /// <summary>
        /// 時刻の通知を購読します。
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<DateTime> observer)
        {
            return timer.Subscribe(observer);
        }
    }
}
