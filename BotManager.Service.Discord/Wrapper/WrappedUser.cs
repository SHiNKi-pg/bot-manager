using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Wrapper
{
    /// <summary>
    /// ラッパーユーザーインターフェース
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWrappedUser<out T> : IWrapped<T> where T : IUser
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        ulong Id { get; }

        /// <summary>
        /// ユーザー名
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// このユーザーがBotかどうか取得します。
        /// </summary>
        bool IsBot { get; }

        /// <summary>
        /// このユーザーにメンションする場合の文字列を取得します。
        /// </summary>
        string Mention { get; }

        /// <summary>
        /// このユーザーのオンライン状態を取得します。
        /// </summary>
        UserStatus Status { get; }
    }
    internal class WrappedUser<T> : IWrappedUser<T> where T : IUser
    {
        #region Protected Fields
        protected readonly T _value;
        #endregion

        #region Constructor
        public WrappedUser(T user)
        {
            _value = user;
        }
        #endregion

        public T Native => _value;

        public ulong Id => _value.Id;

        public string UserName => _value.Username;

        public bool IsBot => _value.IsBot;

        public string Mention => _value.Mention;

        public UserStatus Status => _value.Status;
    }
}
