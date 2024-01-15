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
    /// MST_USER_NAMEテーブル
    /// </summary>
    [Table("MST_USER_NAME")]
    [PrimaryKey("USERID")]
    public class UserName
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Column("USERID")]
        public ulong Id { get; set; }

        /// <summary>
        /// 左側の名前
        /// </summary>
        [Column("LEFTNAME")]
        public string LeftName { get; set; } = "";

        /// <summary>
        /// 中央の名前
        /// </summary>
        [Column("MIDDLENAME")]
        public string MiddleName { get; set; } = "";

        /// <summary>
        /// 右側の名前
        /// </summary>
        [Column("RIGHTNAME")]
        public string RightName { get; set; } = "";

        /// <summary>
        /// <seealso cref="LeftName"/>と <seealso cref="MiddleName"/>、 <see cref="RightName"/>を組み合わせた名称を取得します。
        /// </summary>
        public string Name => LeftName + MiddleName + RightName;
    }
}
