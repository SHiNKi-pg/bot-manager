using BotManager.Engine.Settings;
using BotManager.Setting.Settings;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Engine
{
    /// <summary>
    /// アプリケーション設定ファイル内容
    /// </summary>
    public static class AppSettings
    {
        #region コンストラクタ(static)
        static AppSettings()
        {
            const string APPSETTINGS_JSON_FILE = "appsettings.json";

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(APPSETTINGS_JSON_FILE)
                .Build();

            var appSettings = configuration.Get<AppSetting>();
            if(appSettings == null)
            {
                throw new FileLoadException(APPSETTINGS_JSON_FILE);
            }

            Database = appSettings.Database;
            Script = appSettings.Script;
            Bots = appSettings.Bots;
        }
        #endregion

        #region static properties
        /// <summary>
        /// データベース設定ファイル
        /// </summary>
        public static IDatabase Database { get; }

        public static IScript Script { get; }

        public static IEnumerable<IBotSetting> Bots { get; }

        #endregion
    }
}
