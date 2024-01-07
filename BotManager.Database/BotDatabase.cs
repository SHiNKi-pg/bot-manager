using BotManager.Common;
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
        private static readonly ILog Logger = Log.GetLogger("DB");

        /// <summary>
        /// 予め設定されたデータベースへ接続します。
        /// </summary>
        /// <returns></returns>
        public static IDatabaseContent Connect()
        {
            Logger.Debug("Connect Start");
            var connectionString = AppSettings.Database.ConnectionString;
            var db = new DatabaseContext(connectionString);
            Logger.Debug("Connect End");
            return db;
        }
    }
}
