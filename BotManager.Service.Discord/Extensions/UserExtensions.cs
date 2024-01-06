using BotManager.Service.Discord.Wrapper;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Extensions
{
    /// <summary>
    /// ユーザー拡張メソッド定義
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// このユーザーが所持しているロールのIDを列挙します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IEnumerable<ulong> GetRoleIds<T>(this IWrappedUser<T> user) where T : IGuildUser
        {
            return user.Native.RoleIds;
        }
    }
}
