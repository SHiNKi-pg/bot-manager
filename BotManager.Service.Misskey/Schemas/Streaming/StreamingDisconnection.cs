using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Schemas.Streaming
{
    /// <summary>
    /// ストリーミングAPIでチャンネルから切断するためのクラス
    /// </summary>
    [JsonObject]
    internal class StreamingDisconnectionBody
    {
        [JsonProperty("id")]
        public required string Id { get; init; }
    }
}
