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
    /// MST_USERテーブル
    /// </summary>
    [Table("MST_USER")]
    [PrimaryKey(nameof(Id))]
    public class User
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Column("USERID")]
        public ulong Id { get; set; }

        /// <summary>
        /// ユーザー名
        /// </summary>
        [Column("USERNAME")]
        public required string Name { get; set; }

        /// <summary>
        /// ユーザー作成日時
        /// </summary>
        [Column("CREATEDAT")]
        public DateTime CreatedAt {  get; }
    }
}
