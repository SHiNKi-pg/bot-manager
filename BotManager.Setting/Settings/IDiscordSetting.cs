using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Setting.Settings
{
    /// <summary>
    /// DiscordBot設定スキーマ
    /// </summary>
    public interface IDiscordSetting
    {
        /// <summary>
        /// 認証
        /// </summary>
        IDiscordCertification Certificate { get; }
    }

    /// <summary>
    /// DiscordBot認証スキーマ
    /// </summary>
    public interface IDiscordCertification
    {
        /// <summary>
        /// アクセストークン
        /// </summary>
        string Token { get; }
    }

#pragma warning disable 8618
    internal class DiscordSetting : IDiscordSetting
    {
        public DiscordCertification Certification { get; set; }

        public IDiscordCertification Certificate { get => Certification; }
    }

    internal class DiscordCertification : IDiscordCertification
    {
        public string Token { get; set; }
    }
#pragma warning restore
}
