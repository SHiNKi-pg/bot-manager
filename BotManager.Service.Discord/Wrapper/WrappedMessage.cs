using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Wrapper
{
    /// <summary>
    /// ラッパーメッセージインターフェース
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWrappedMessage<out T> : IWrapped<T> where T : IMessage
    {
        /// <summary>
        /// メッセージID
        /// </summary>
        ulong Id { get; }

        /// <summary>
        /// メッセージ本文
        /// </summary>
        string Content { get; }

        /// <summary>
        /// このメッセージを削除します。
        /// </summary>
        /// <returns></returns>
        Task DeleteAsync();

        /// <summary>
        /// このメッセージの参照を表すオブジェクト。このメッセージに対して返信する際に使用されます。
        /// </summary>
        MessageReference Reference { get; }

        /// <summary>
        /// このメッセージにリアクションを追加します。
        /// </summary>
        /// <param name="emote"></param>
        /// <returns></returns>
        Task AddReaction(IWrappedEmote<IEmote> emote);
    } 
    internal class WrappedMessage<T> : IWrappedMessage<T> where T : IMessage
    {
        #region Protected Fields
        protected readonly T _value;
        #endregion

        #region Constructor
        public WrappedMessage(T message)
        {
            _value = message;
        }
        #endregion

        public T Native => _value;

        public ulong Id => _value.Id;

        public string Content => _value.Content;

        public MessageReference Reference => _value.Reference;

        public async Task AddReaction(IWrappedEmote<IEmote> emote)
        {
            await _value.AddReactionAsync(emote.Native);
        }

        public async Task DeleteAsync()
        {
            await _value.DeleteAsync();
        }
    }
}
