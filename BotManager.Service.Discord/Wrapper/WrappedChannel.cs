using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Wrapper
{
    /// <summary>
    /// ラッパーチャネルインターフェース
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWrappedChannel<out T> : IWrapped<T> where T : IChannel
    {
        /// <summary>
        /// チャンネルID
        /// </summary>
        ulong Id { get; }

        /// <summary>
        /// チャンネル名
        /// </summary>
        string Name { get; }
    }
    internal class WrappedChannel<T> : IWrappedChannel<T> where T : IChannel
    {
        #region Protected Fields
        protected readonly T _channel;
        #endregion

        #region Constructor
        public WrappedChannel(T channel)
        {
            _channel = channel;
        }
        #endregion

        public T Native => _channel;

        public ulong Id => Native.Id;

        public string Name => Native.Name;
    }
}
