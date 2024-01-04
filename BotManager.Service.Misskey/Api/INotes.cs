using BotManager.Service.Misskey.Consts;
using BotManager.Service.Misskey.Schemas;
using BotManager.Service.Misskey.Schemas.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Api
{
    /// <summary>
    /// NoteAPIインターフェース
    /// </summary>
    public interface INotes
    {
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
        Task<IEnumerable<Note>> GetNotesAsync(
            bool? local = false,
            bool? reply = false,
            bool? renote = false,
            bool? withFiles = false,
            bool? poll = false,
            int? limit = 10,
            string? sinceId = null,
            string? untilId = null
            );

        #endregion

        #region notes/children
        /// <summary>
        /// ノートへのリプライや引用を取得します。引用なしのRenoteは取得されません。
        /// </summary>
        /// <param name="noteId">ノートのid。</param>
        /// <param name="limit">取得するノートの最大数。</param>
        /// <param name="sinceId">指定すると、idがその値よりも大きいノートを返します。</param>
        /// <param name="untilId">指定すると、idがその値よりも小さいノートを返します。</param>
        /// <returns></returns>
        Task<IEnumerable<Note>> GetChildren(
            string noteId,
            int? limit = 10,
            string? sinceId = null,
            string? untilId = null
            );
        #endregion

        #region notes/create
        /// <summary>
        /// ノートを作成します。返信やRenoteもこのAPIで行います。
        /// </summary>
        /// <param name="visibility">ノートの公開範囲。<seealso cref="MisskeyVisibility"/>クラスの定数から選択できます。</param>
        /// <param name="visibleUserIds">ノートを閲覧可能なユーザーのidのリスト。visibilityがspecifiedの場合のみ適用されます。</param>
        /// <param name="text">ノートの本文。</param>
        /// <param name="cw">ノートのCW。</param>
        /// <param name="localOnly">trueにすると、ローカルのみに投稿されます。</param>
        /// <param name="noExtractMentions">trueにすると、本文からメンションを展開しません。</param>
        /// <param name="noExtractHashtags">trueにすると、本文からハッシュタグを展開しません。</param>
        /// <param name="noExtractEmojis">trueにすると、本文から絵文字を展開しません。</param>
        /// <param name="fileIds">添付するファイルのid。</param>
        /// <param name="mediaIds">fileIds を使用してください。fileIds と mediaIds が指定された場合、 mediaIds は無視されます。</param>
        /// <param name="replyId">返信先のノートのid。</param>
        /// <param name="renoteId">Renote対象のノートのid。</param>
        /// <param name="channelId">投稿先のチャンネルのid。</param>
        /// <param name="poll">投票に関するパラメータ。</param>
        /// <returns>作成されたノート</returns>
        Task<CreatedNote> CreateNote(
            string visibility = MisskeyVisibility.PUBLIC,
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
            Poll? poll = null
            );
        #endregion

        #region notes/delete
        /// <summary>
        /// ノートを削除します。
        /// </summary>
        /// <param name="noteId">削除するノートのID</param>
        /// <returns></returns>
        Task DeleteNote(string noteId);
        #endregion

        #region notes/show
        /// <summary>
        /// ノートを取得します。
        /// </summary>
        /// <param name="noteId">ノートのid。</param>
        /// <returns></returns>
        Task<Note> ShowNote(string noteId);
        #endregion
    }
}
