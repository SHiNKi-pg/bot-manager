using BotManager.Service.Misskey.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Api
{
    /// <summary>
    /// ノート関連のAPI
    /// </summary>
    internal class Notes : MisskeyApiBase, INotes
    {
        #region Constructor
        internal Notes(MisskeyApi misskeyApi) : base(misskeyApi)
        {
        }
        #endregion

        #region notes
        /// <summary>
        /// ノート一覧を取得します。
        /// </summary>
        /// <param name="local">ローカルで作成されたノートのみを取得します。</param>
        /// <param name="reply">`true` にすると返信だけを、 `false` にすると返信以外を取得します。値を設定しなければ返信であるかそうでないかに関係なくノートを取得します。</param>
        /// <param name="renote">`true` にするとリノートだけを、 `false` にするとリノート以外を取得します。値を設定しなければリノートであるかそうでないかに関係なくノートを取得します。</param>
        /// <param name="withFiles">`true` にすると添付ファイルのあるノートだけを、 `false` にすると添付ファイルがないノートだけを取得します。値を設定しなければ添付ファイルの有無にかかわらずノートを取得します。</param>
        /// <param name="poll">`true` にすると投票を含むノートだけを、 `false` にすると含まないノートだけを取得します。値を設定しなければ投票の有無にかかわらずノートを取得します。</param>
        /// <param name="limit">取得するノートの最大数を指定します。</param>
        /// <param name="sinceId">指定すると、idがその値よりも大きいノートを返します。</param>
        /// <param name="untilId">指定すると、idがその値よりも小さいノートを返します。</param>
        /// <returns></returns>
        public async Task<IEnumerable<Note>> GetNotesAsync(
            bool? local = false,
            bool? reply = false,
            bool? renote = false,
            bool? withFiles = false,
            bool? poll = false,
            int? limit = 10,
            string? sinceId = null,
            string? untilId = null
            )
        {
            return await base.PostAsync<IEnumerable<Note>>("notes", new
            {
                i = misskeyApi.AccessToken,
                detail = false,
                local,
                reply,
                renote,
                withFiles,
                poll,
                limit,
                sinceId,
                untilId
            });
        }
        #endregion

        public async Task<Note> CreateNote(string visibility = "public", 
            IEnumerable<string>? visibleUserIds = null, 
            string? text = null, 
            string? cw = null, 
            bool localOnly = false, 
            bool noExtractMentions = false, 
            bool noExtractHashtags = false, 
            bool noExtractEmojis = false, 
            IEnumerable<string>? fileIds = null, 
            IEnumerable<string>? mediaIds = null, 
            string? replyId = null, 
            string? renoteId = null, 
            string? channelId = null, 
            NotePoll? poll = null)
        {
            return await base.PostAsync<Note>("notes/create", new
            {
                i = misskeyApi.AccessToken,
                detail = false,
                visibility, visibleUserIds, text, cw, localOnly,
                noExtractMentions, noExtractHashtags, noExtractEmojis, fileIds, mediaIds, replyId, renoteId, channelId, poll
            });
        }

        public async Task<IEnumerable<Note>> GetChildren(string noteId, int? limit = 10, string? sinceId = null, string? untilId = null)
        {
            return await base.PostAsync<IEnumerable<Note>>("notes/children", new
            {
                i = misskeyApi.AccessToken,
                detail = false,
                noteId, limit, sinceId, untilId
            });
        }
    }
}
