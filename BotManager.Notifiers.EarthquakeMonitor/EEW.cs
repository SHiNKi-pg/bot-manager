using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Notifiers.EarthquakeMonitor
{
    /// <summary>
    /// 緊急地震速報通知
    /// </summary>
    public static class EEWNotifier
    {
        /// <summary>
        /// 緊急地震速報を通知するオブジェクトを作成します。
        /// </summary>
        /// <param name="datetimeObservable"></param>
        /// <returns></returns>
        public static IEEWMonitor Create(IObservable<DateTime> datetimeObservable)
        {
            return new EEWMonitor(datetimeObservable);
        }
    }
}
