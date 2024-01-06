using BotManager.Service.Misskey.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Schemas.Notes
{
    /// <summary>
    /// 作成されたノート
    /// </summary>
    [JsonObject("createdNote")]
    public class CreatedNote
    {
        /// <summary>
        /// <seealso cref="INotes.CreateNote(string, IEnumerable{string}?, string?, string?, bool, bool, bool, bool, IEnumerable{string}?, IEnumerable{string}?, string?, string?, string?, Poll?)"/>
        /// で作成されたノート
        /// </summary>
        [JsonProperty("createdNote")]
        public required Note Note { get; init; }
    }
}
