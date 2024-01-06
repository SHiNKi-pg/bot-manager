using BotManager.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Database
{
    /// <summary>
    /// データベースクラス
    /// </summary>
    public static class BotDatabase
    {
        /// <summary>
        /// 予め設定されたデータベースへ接続します。
        /// </summary>
        /// <returns></returns>
        public static IDatabaseContent Connect()
        {
            var connectionString = AppSettings.Database.ConnectionString;
            var db = new DatabaseContext(connectionString);
            return db;
        }
    }
}
