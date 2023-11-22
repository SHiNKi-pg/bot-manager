using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Schemas
{
    /// <summary>
    /// ユーザー
    /// </summary>
    [JsonObject]
    public sealed class User
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
        /// ユーザー名
        /// </summary>
        [JsonProperty("username")]
        public string UserName { get; private set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("host")]
        public string? Host { get; private set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("onlineStatus")]
        public string OnlineStatus { get; private set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get; private set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("avatarBlurhash")]
        public string AvatarBlurhash { get; private set; } = "";
    }
}
