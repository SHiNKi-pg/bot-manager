using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Schemas
{
    /// <summary>
    /// ノート
    /// </summary>
    [JsonObject("note")]
    public sealed class Note
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; private set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("createdAt")]
        public string CreatedAt { get; private set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("text")]
        public string? Text { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("cw")]
        public string? CW { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("user")]
        public User? User { get; private set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; private set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("visibility")]
        public string Visibility { get; private set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("replyId")]
        public string? ReplyId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("renoteId")]
        public string? RenoteId { get; private set; }

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
