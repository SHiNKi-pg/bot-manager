using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Schemas.Streaming
{
    /// <summary>
    /// 受信メッセージボディ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [JsonObject]
    public class ReceivedBody<T> : IObjectType
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public required string Id { get; init; }

        /// <summary>
        /// タイプ
        /// </summary>
        [JsonProperty("type")]
        public required string Type { get; init; }

        /// <summary>
        /// ボディ部
        /// </summary>
        [JsonProperty("body")]
        public required T Body { get; init; }
    }
}
