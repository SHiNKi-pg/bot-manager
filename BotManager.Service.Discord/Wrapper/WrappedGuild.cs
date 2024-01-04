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
    /// ラッパーギルドインターフェース
    /// </summary>
    public interface IWrappedGuild<out T> : IWrapped<T> where T : IGuild
    {
        /// <summary>
        /// サーバーID
        /// </summary>
        ulong Id { get; }

        /// <summary>
        /// サーバー名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// このサーバーのメンバー全員にメンションする時の文字列を取得します。
        /// </summary>
        IWrappedRole<IRole> EveryoneRole { get; }

        /// <summary>
        /// このサーバーの指定したIDのチャンネルを返します。
        /// </summary>
        /// <param name="channelId">チャンネルID</param>
        /// <returns></returns>
        Task<IWrappedChannel<IGuildChannel>> GetChannelAsync(ulong channelId);

        /// <summary>
        /// このサーバーの指定したIDのテキストチャンネルを返します。
        /// </summary>
        /// <param name="channelId">チャンネルID</param>
        /// <returns></returns>
        Task<IWrappedTextChannel<ITextChannel>> GetTextChannelAsync(ulong channelId);

        /// <summary>
        /// 指定したIDのロールを返します。
        /// </summary>
        /// <param name="roleId">ロールID</param>
        /// <returns></returns>
        IWrappedRole<IRole> GetRole(ulong roleId);

        /// <summary>
        /// このサーバーに所属するユーザーを全て返します。
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IWrappedUser<IGuildUser>>> GetUsersAsync();

        /// <summary>
        /// 指定したIDのユーザーを返します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <returns></returns>
        Task<IWrappedUser<IGuildUser>> GetUserAsync(ulong userId);

        /// <summary>
        /// このサーバーの絵文字を全て列挙します。
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IWrappedEmote<GuildEmote>>> GetEmotesAsync();

        /// <summary>
        /// このサーバーの指定したIDの絵文字を返します。
        /// </summary>
        /// <param name="emoteId"></param>
        /// <returns></returns>
        Task<IWrappedEmote<GuildEmote>> GetEmoteAsync(ulong emoteId);
    }

    internal class WrappedGuild<T> : IWrappedGuild<T> where T : IGuild
    {
        #region Private Fields
        private T _guild;
        #endregion

        #region Constructor
        public WrappedGuild(T guild)
        {
            this._guild = guild;
        }
        #endregion

        public T Native => this._guild;

        public ulong Id => _guild.Id;

        public string Name => _guild.Name;

        public IWrappedRole<IRole> EveryoneRole => _guild.EveryoneRole.ToWrappedRole();

        public async Task<IWrappedChannel<IGuildChannel>> GetChannelAsync(ulong channelId)
        {
            var channel = await _guild.GetChannelAsync(channelId);
            return channel.ToWrappedChannel();
        }

        public async Task<IWrappedEmote<GuildEmote>> GetEmoteAsync(ulong emoteId)
        {
            var emote = await _guild.GetEmoteAsync(emoteId);
            return emote.ToWrappedEmote();
        }

        public async Task<IEnumerable<IWrappedEmote<GuildEmote>>> GetEmotesAsync()
        {
            var emotes = await _guild.GetEmotesAsync();
            return emotes.Select(emoji => emoji.ToWrappedEmote());
        }

        public IWrappedRole<IRole> GetRole(ulong roleId)
        {
            var role = _guild.GetRole(roleId);
            return role.ToWrappedRole();
        }

        public async Task<IWrappedTextChannel<ITextChannel>> GetTextChannelAsync(ulong channelId)
        {
            var channel = await _guild.GetTextChannelAsync(channelId);
            return channel.ToWrappedTextChannel();
        }

        public async Task<IWrappedUser<IGuildUser>> GetUserAsync(ulong userId)
        {
            var user = await _guild.GetUserAsync(userId);
            return user.ToWrappedUser();
        }

        public async Task<IEnumerable<IWrappedUser<IGuildUser>>> GetUsersAsync()
        {
            var users = await _guild.GetUsersAsync();
            return users.Select(user => user.ToWrappedUser());
        }
    }
}
