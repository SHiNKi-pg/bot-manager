using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        /// <summary>
        /// 現在日時の要素がそれぞれの条件を満たすかどうか返します。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="month">月の条件</param>
        /// <param name="day">日の条件</param>
        /// <param name="hour">時間の条件</param>
        /// <param name="minute">分の条件</param>
        /// <param name="second">秒の条件</param>
        /// <returns></returns>
        public static bool EveryYear(this DateTime dateTime, Func<int, bool> month, Func<int, bool> day, Func<int, bool> hour, Func<int, bool> minute, Func<int, bool> second)
        {
            return month(dateTime.Hour) && day(dateTime.Day) && hour(dateTime.Hour) && minute(dateTime.Minute) && second(dateTime.Second);
        }

        /// <summary>
        /// 現在日時が指定した日と時刻かどうか返します。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="day">日</param>
        /// <param name="hours">時間(0～23)</param>
        /// <param name="minutes">分(0～59)</param>
        /// <param name="seconds">秒(0～59)</param>
        /// <returns></returns>
        public static bool EveryMonth(this DateTime dateTime, int day, int hours, int minutes, int seconds)
        {
            return dateTime.Day == day
                && dateTime.Hour == hours
                && dateTime.Minute == minutes
                && dateTime.Second == seconds;
        }

        /// <summary>
        /// 現在日時の要素がそれぞれの条件を満たすかどうか返します。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="day">日の条件</param>
        /// <param name="hour">時間の条件</param>
        /// <param name="minute">分の条件</param>
        /// <param name="second">秒の条件</param>
        /// <returns></returns>
        public static bool EveryMonth(this DateTime dateTime, Func<int, bool> day, Func<int, bool> hour, Func<int, bool> minute, Func<int, bool> second)
        {
            return day(dateTime.Day) && hour(dateTime.Hour) && minute(dateTime.Minute) && second(dateTime.Second);
        }

        /// <summary>
        /// 現在日時が指定した時刻かどうか返します。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="hours">時間(0～23)</param>
        /// <param name="minutes">分(0～59)</param>
        /// <param name="seconds">秒(0～59)</param>
        /// <returns></returns>
        public static bool EveryDay(this DateTime dateTime, int hours, int minutes, int seconds)
        {
            return dateTime.Hour == hours
                && dateTime.Minute == minutes
                && dateTime.Second == seconds;
        }

        /// <summary>
        /// 現在時刻の要素がそれぞれの条件を満たすかどうか返します。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="hour">時間の条件</param>
        /// <param name="minute">分の条件</param>
        /// <param name="second">秒の条件</param>
        /// <returns></returns>
        public static bool EveryDay(this DateTime dateTime, Func<int, bool> hour, Func<int, bool> minute, Func<int, bool> second)
        {
            return hour(dateTime.Hour) && minute(dateTime.Minute) && second(dateTime.Second);
        }

        /// <summary>
        /// 現在時刻が指定した分秒かどうか返します。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="minutes">分(0～59)</param>
        /// <param name="seconds">秒(0～59)</param>
        /// <returns></returns>
        public static bool EveryHour(this DateTime dateTime, int minutes, int seconds)
        {
            return dateTime.Minute == minutes
                && dateTime.Second == seconds;
        }

        /// <summary>
        /// 現在時刻の要素がそれぞれの条件を満たすかどうか返します。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="minute">分の条件</param>
        /// <param name="second">秒の条件</param>
        /// <returns></returns>
        public static bool EveryHour(this DateTime dateTime, Func<int, bool> minute, Func<int, bool> second)
        {
            return minute(dateTime.Minute) && second(dateTime.Second);
        }

        /// <summary>
        /// 現在時刻が指定した秒かどうか返します。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="seconds">秒(0～59)</param>
        /// <returns></returns>
        public static bool EveryMinute(this DateTime dateTime, int seconds)
        {
            return dateTime.Minute == seconds;
        }

        /// <summary>
        /// 現在時刻の要素がそれぞれの条件を満たすかどうか返します。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="second">秒の条件</param>
        /// <returns></returns>
        public static bool EveryMinute(this DateTime dateTime, Func<int, bool> second)
        {
            return second(dateTime.Second);
        }
    }
}
