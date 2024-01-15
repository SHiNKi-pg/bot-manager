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
    /// MST_MISSKEY_USERテーブル
    /// </summary>
    [Table("MST_MISSKEY_USER")]
    [PrimaryKey(nameof(Id))]
    public class MisskeyUser
    {
        /// <summary>
        /// MisskeyユーザーID
        /// </summary>
        [Column("MISSKEYUSERID")]
        public required string Id { get; set; }

        /// <summary>
        /// Misskeyユーザー名
        /// </summary>
        [Column("USERNAME")]
        public required string Name { get; set; }

        /// <summary>
        /// メインユーザーID
        /// </summary>
        [Column("USERID")]
        public ulong? UserId { get; set; }
    }
}
