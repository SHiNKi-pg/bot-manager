using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Utility
{
    /// <summary>
    /// <see cref="DateTime"/>拡張メソッド定義
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 日時が指定した日付と時刻ちょうどかどうか返します。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="year">年</param>
        /// <param name="month">月(1～12)</param>
        /// <param name="day">日</param>
        /// <param name="hours">時間(0～23)</param>
        /// <param name="minutes">分(0～59)</param>
        /// <param name="seconds">秒(0～59)</param>
        /// <returns></returns>
        public static bool IsJust(this DateTime dateTime, int year, int month, int day, int hours, int minutes, int seconds)
        {
            return dateTime.Year == year
                && dateTime.Month == month
                && dateTime.Day == day
                && dateTime.Hour == hours
                && dateTime.Minute == minutes
                && dateTime.Second == seconds;
        }

        /// <summary>
        /// 現在日時が指定した月日と時刻かどうか返します。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="month">月(1～12)</param>
        /// <param name="day">日</param>
        /// <param name="hours">時間(0～23)</param>
        /// <param name="minutes">分(0～59)</param>
        /// <param name="seconds">秒(0～59)</param>
        /// <returns></returns>
        public static bool EveryYear(this DateTime dateTime, int month, int day, int hours, int minutes, int seconds)
        {
            return dateTime.Month == month
                && dateTime.Day == day
                && dateTime.Hour == hours
                && dateTime.Minute == minutes
                && dateTime.Second == seconds;
        }
    }
}
