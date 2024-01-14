using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Utility
{
    /// <summary>
    /// <see cref="Random"/>拡張メソッド定義
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// 指定したコレクションの中からランダムで要素を1つ選択して返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static T Choice<T>(this Random random, params T[] items)
        {
            int count = items.Length;
            return items[random.Next(0, count)];
        }

        /// <summary>
        /// 指定したコレクションの中からランダムで要素を1つ選択して返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static T Choice<T>(this Random random, IEnumerable<T> items)
        {
            return random.Choice(items.ToArray());
        }
    }
}
