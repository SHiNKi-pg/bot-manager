using BotManager.Setting.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Engine.Settings
{
#pragma warning disable 8618
    /// <summary>
    /// 設定ファイルメインスキーマ
    /// </summary>
    internal class AppSetting
    {
        public Database Database { get; set; } = new();

        public Script Script { get; set; } = new();

        public IEnumerable<BotSetting> Bots { get; set; }
    }
#pragma warning restore
}
