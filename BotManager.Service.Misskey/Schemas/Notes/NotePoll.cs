using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Schemas.Notes
{
    /// <summary>
    /// 投票
    /// </summary>
    [JsonObject("poll")]
    public sealed class Poll
    {
        /// <summary>
        /// 選択肢
        /// </summary>
        [JsonProperty("choices")]
        public IEnumerable<string> Choices { get; private set; } = Enumerable.Empty<string>();

        /// <summary>
        /// trueにすると、複数選択を許容します。
        /// </summary>
        [JsonProperty("multiple")]
        public bool Multiple { get; private set; } = false;

        /// <summary>
        /// 投票の締め切り。エポック秒で指定します。
        /// </summary>
        [JsonProperty("expiresAt")]
        public int? ExpiresAt { get; private set; }

        /// <summary>
        /// 指定すると、ノート作成からexpiredAfter秒後に投票を締め切ります。expiresAtと併せて指定した場合、expiresAtが優先されます。
        /// </summary>
        [JsonProperty("expiredAfter")]
        public int? ExpiredAfter { get; private set; }

        /// <summary>
        /// <see cref="Poll"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="choices">選択肢</param>
        /// <param name="multiple">trueにすると、複数選択を許容します。</param>
        /// <param name="expiresAt">投票の締め切り。エポック秒で指定します。</param>
        /// <param name="expiredAfter">指定すると、ノート作成からexpiredAfter秒後に投票を締め切ります。expiresAtと併せて指定した場合、expiresAtが優先されます。</param>
        public Poll(IEnumerable<string> choices, bool multiple, int? expiresAt = null, int? expiredAfter = null)
        {
            Choices = choices;
            Multiple = multiple;
            ExpiresAt = expiresAt;
            ExpiredAfter = expiredAfter;
        }
    }
}
