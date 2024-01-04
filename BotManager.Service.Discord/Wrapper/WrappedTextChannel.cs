using BotManager.Service.Discord.Extensions;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Wrapper
{
    /// <summary>
    /// ラッパーテキストチャンネルインターフェース
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWrappedTextChannel<out T> : IWrappedChannel<T> where T : ITextChannel
    {
        /// <summary>
        /// このメソッドから返されるオブジェクトの<see cref="IDisposable.Dispose"/>が呼ばれるまで、入力インジケーターを表示します。
        /// </summary>
        /// <returns></returns>
        IDisposable EnterTypingState();

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isTTS"></param>
        /// <param name="embed"></param>
        /// <param name="options"></param>
        /// <param name="allowedMentions"></param>
        /// <param name="messageReference"></param>
        /// <param name="components"></param>
        /// <param name="stickers"></param>
        /// <param name="embeds"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        Task<IWrappedMessage<IUserMessage>> SendMessageAsync(string? text = null, bool isTTS = false, Embed? embed = null, RequestOptions? options = null, AllowedMentions? allowedMentions = null, MessageReference? messageReference = null, MessageComponent? components = null, ISticker[]? stickers = null, Embed[]? embeds = null, MessageFlags flags = MessageFlags.None);
    }

    internal class WrappedTextChannel<T> : WrappedChannel<T>, IWrappedTextChannel<T> where T : ITextChannel
    {
        #region Constructor
        public WrappedTextChannel(T channel) : base(channel) { }
        #endregion

        public IDisposable EnterTypingState()
        {
            return Native.EnterTypingState();
        }

        public async Task<IWrappedMessage<IUserMessage>> SendMessageAsync(string? text = null, bool isTTS = false, Embed? embed = null, RequestOptions? options = null, AllowedMentions? allowedMentions = null, MessageReference? messageReference = null, MessageComponent? components = null, ISticker[]? stickers = null, Embed[]? embeds = null, MessageFlags flags = MessageFlags.None)
        {
            var message = await Native.SendMessageAsync(text, isTTS, embed, options, allowedMentions, messageReference, components, stickers, embeds, flags);
            return message.ToWrappedMessage();
        }
    }
}
