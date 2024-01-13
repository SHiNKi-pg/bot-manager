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
    /// MST_BOTテーブル
    /// </summary>
    [Table("MST_BOT")]
    [PrimaryKey(nameof(Id))]
    public class Bot
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column("BOTID")]
        public required string Id { get; set; }

        /// <summary>
        /// Bot名
        /// </summary>
        [Column("BOTNAME")]
        public required string Name { get; set; }
    }
}
