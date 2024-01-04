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
    /// Discord拡張メソッド定義クラス
    /// </summary>
    public static class DiscordExtensions
    {
        #region ToWrapped
        /// <summary>
        /// ラッピングされたオブジェクトを返します。
        /// </summary>
        /// <param name="guild"></param>
        /// <returns></returns>
        public static IWrappedGuild<T> ToWrappedGuild<T>(this T guild) where T : IGuild
        {
            return new WrappedGuild<T>(guild);
        }

        /// <summary>
        /// ラッピングされたオブジェクトを返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static IWrappedChannel<T> ToWrappedChannel<T>(this T channel) where T : IChannel
        {
            return new WrappedChannel<T>(channel);
        }

        /// <summary>
        /// ラッピングされたオブジェクトを返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static IWrappedTextChannel<T> ToWrappedTextChannel<T>(this T channel) where T : ITextChannel
        {
            return new WrappedTextChannel<T>(channel);
        }

        /// <summary>
        /// ラッピングされたオブジェクトを返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IWrappedUser<T> ToWrappedUser<T>(this T user) where T : IUser
        {
            return new WrappedUser<T>(user);
        }

        /// <summary>
        /// ラッピングされたオブジェクトを返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="role"></param>
        /// <returns></returns>
        public static IWrappedRole<T> ToWrappedRole<T>(this T role) where T : IRole
        {
            return new WrappedRole<T>(role);
        }

        /// <summary>
        /// ラッピングされたオブジェクトを返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IWrappedMessage<T> ToWrappedMessage<T>(this T message) where T : IMessage
        {
            return new WrappedMessage<T>(message);
        }

        /// <summary>
        /// ラッピングされたオブジェクトを返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="emote"></param>
        /// <returns></returns>
        public static IWrappedEmote<T> ToWrappedEmote<T>(this T emote) where T : IEmote
        {
            return new WrappedEmote<T>(emote);
        }
        #endregion
    }
}
