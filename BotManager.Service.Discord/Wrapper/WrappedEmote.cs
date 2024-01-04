using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Wrapper
{
    /// <summary>
    /// ラッパー絵文字インターフェース
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWrappedEmote<out T> : IWrapped<T> where T : IEmote
    {
        /// <summary>
        /// 絵文字名
        /// </summary>
        string Name { get; }
    }
    internal class WrappedEmote<T> : IWrappedEmote<T> where T : IEmote
    {
        #region Protected Fields
        protected readonly T _value;
        #endregion

        #region Constructor
        public WrappedEmote(T emote)
        {
            _value = emote;
        }
        #endregion

        public T Native => _value;

        public string Name => _value.Name;
    }
}
