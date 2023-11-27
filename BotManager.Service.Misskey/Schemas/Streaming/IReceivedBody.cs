using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Schemas.Streaming
{
    [JsonObject]
    internal class IReceivedBody<T>
    {
        [JsonProperty("id")]
        public required string Id { get; init; }

        [JsonProperty("type")]
        public required string Type { get; init; }

        [JsonProperty("body")]
        public required T Body { get; init; }
    }
}
