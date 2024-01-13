using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Database.Entities
{
    /// <summary>
    /// MV_EMOTIONマテリアライズドビュー
    /// </summary>
    [Table("MV_EMOTION")]
    [Keyless]
    public class EmotionTotal
    {
        /// <summary>
        /// Bot ID
        /// </summary>
        [Column("BOTID")]
        public string BotId { get; init; } = "";

        /// <summary>
        /// ユーザーID
        /// </summary>
        [Column("USERID")]
        public ulong UserId { get; init; }

        /// <summary>
        /// 感情の合計値
        /// </summary>
        [Column("EMOTIONSUM")]
        public int TotalValue { get; init; }
    }
}
