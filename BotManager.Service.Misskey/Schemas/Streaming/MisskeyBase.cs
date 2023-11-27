using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Schemas.Streaming
{
    /// <summary>
    /// Misskey Base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [JsonObject]
    public class MisskeyBase<T> : IObjectType
    {
        /// <summary>
        /// タイプ
        /// </summary>
        [JsonProperty("type")]
        public required string Type { get; set; }

        /// <summary>
        /// ボディ部
        /// </summary>
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
