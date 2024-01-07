using BotManager.Common;
using BotManager.Common.Scripting;
using BotManager.Notifiers;
using BotManager.Notifiers.EarthquakeMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.External
{
    /// <summary>
    /// サブスクリプション引数クラス
    /// </summary>
    public class SubscriptionArguments : ISubscriptionArguments
    {
#pragma warning disable 8618
        /// <summary>
        /// Botマネージャー
        /// </summary>
        public required IBotManager BotManager { get; init; }

        /// <summary>
        /// 現在時刻を通知する機構
        /// </summary>
        public required IObservable<DateTime> Clock { get; init; }

        /// <summary>
        /// 緊急地震速報通知
        /// </summary>
        public required IEEWMonitor EEWMonitor { get; init; }

        // TODO: 新しい引数を追加したい場合はここに記載していく
#pragma warning restore
    }
}
