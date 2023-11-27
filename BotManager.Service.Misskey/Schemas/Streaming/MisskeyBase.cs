using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Schemas.Streaming
{
    [JsonObject]
    internal class MisskeyBase<T>
    {
        [JsonProperty("type")]
        public required string Type { get; set; }

        [JsonProperty("body")]
        public required T Body { get; set; }

        /// <summary>
        /// このオブジェクトをJSON文字列として返します。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
