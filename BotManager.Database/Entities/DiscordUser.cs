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
    /// MST_DISCORD_USERテーブル
    /// </summary>
    [Table("MST_DISCORD_USER")]
    [PrimaryKey(nameof(Id))]
    public class DiscordUser
    {
        /// <summary>
        /// DiscordユーザーID
        /// </summary>
        [Column("DISCORDUSERID")]
        public required ulong Id { get; set; }

        /// <summary>
        /// Discordユーザー名
        /// </summary>
        [Column("USERNAME")]
        public required string Name { get; set; }

        /// <summary>
        /// このユーザーにメンションする際に使用される文字列
        /// </summary>
        [Column("MENTION")]
        public required string MentionString { get; set; }
    }
}
