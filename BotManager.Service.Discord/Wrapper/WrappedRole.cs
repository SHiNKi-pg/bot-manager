using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Wrapper
{
    /// <summary>
    /// ラッパーロールインターフェース
    /// </summary>
    public interface IWrappedRole<out T> : IWrapped<T> where T : IRole
    {
        /// <summary>
        /// ロールID
        /// </summary>
        ulong Id { get; }

        /// <summary>
        /// ロール名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// このロールにメンションする場合の文字列を返します。
        /// </summary>
        string Mention { get; }
    }
    internal class WrappedRole<T> : IWrappedRole<T> where T : IRole
    {
        #region Protected Fields
        protected readonly T _value;
        #endregion

        #region Constructor
        public WrappedRole(T role)
        {
            _value = role;
        }
        #endregion

        public T Native => _value;

        public ulong Id => _value.Id;

        public string Name => _value.Name;

        public string Mention => _value.Mention;
    }
}
