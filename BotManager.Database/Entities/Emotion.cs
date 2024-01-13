using BotManager.Database.Entities;
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
    /// TBL_EMOTIONテーブル
    /// </summary>
    [Table("TBL_EMOTION")]
    [PrimaryKey(nameof(Id))]
    public class Emotion
    {
        /// <summary>
        /// 感情増分ID
        /// </summary>
        [Column("EMOTIONID")]
        public ulong Id { get; }

        /// <summary>
        /// Bot ID
        /// </summary>
        [Column("BOTID")]
        public required string BotId { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        [Column("USERID")]
        public required ulong UserId { get; set; }

        /// <summary>
        /// 感情値の増分
        /// </summary>
        [Column("EMOTIONVALUE")]
        public required int Value { get; set; }

        /// <summary>
        /// データ作成日時
        /// </summary>
        [Column("CREATEDAT")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Bot
        /// </summary>
        [ForeignKey(nameof(BotId))]
        public Bot? Bot { get; init; }

        /// <summary>
        /// User
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public User? User { get; init; }
    }
}
