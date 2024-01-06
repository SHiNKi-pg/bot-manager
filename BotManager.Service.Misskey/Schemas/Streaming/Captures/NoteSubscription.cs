using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Schemas.Streaming.Captures
{
    /// <summary>
    /// 投稿キャプチャボディ
    /// </summary>
    [JsonObject]
    internal class NoteSubscriptionBody
    {
        /// <summary>
        /// ノートID
        /// </summary>
        [JsonProperty("id")]
        public required string NoteId { get; init; }
    }
}
