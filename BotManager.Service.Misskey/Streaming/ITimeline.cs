using BotManager.Service.Misskey.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Streaming
{
    /// <summary>
    /// タイムラインインターフェース
    /// </summary>
    public interface ITimeline
    {
        /// <summary>
        /// 新しいノートが追加されたときに発生します。
        /// </summary>
        IObservable<Note> Notes { get; }
    }
}
