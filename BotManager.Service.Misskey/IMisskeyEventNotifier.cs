using BotManager.Common;
using BotManager.Service.Misskey.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey
{
    /// <summary>
    /// Misskeyイベント通知クライアントインターフェース
    /// </summary>
    public interface IMisskeyEventNotifier : IEventNotifier
    {
        /// <summary>
        /// グローバルタイムラインを取得します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IObservable<Note> GetGlobalTimeline(string id);

        /// <summary>
        /// ホームタイムラインを取得します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IObservable<Note> GetHomeTimeline(string id);

        /// <summary>
        /// ソーシャルタイムラインを取得します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IObservable<Note> GetHybridTimeline(string id);

        /// <summary>
        /// ローカルタイムラインを取得します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IObservable<Note> GetLocalTimeline(string id);
    }
}
