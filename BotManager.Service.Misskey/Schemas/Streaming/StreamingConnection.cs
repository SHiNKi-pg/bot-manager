using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Schemas.Streaming
{
    /// <summary>
    /// ストリームAPIに接続するための情報の一部を保有するクラス
    /// </summary>
    [JsonObject]
    internal class StreamingConnectionBody
    {
        [JsonProperty("channel")]
        public required string Channel { get; init; }

        [JsonProperty("id")]
        public required string Id { get; init; }
    }

    /// <summary>
    /// ストリームAPIに接続するための情報の一部を保有するクラス
    /// </summary>
    [JsonObject]
    internal class StreamingConnectionBody<TParam> : StreamingConnectionBody
    {
        [JsonProperty("params")]
        public required TParam Parameter { get; init; }
    }
}
