using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Setting.Settings
{
    /// <summary>
    /// Botスキーマインターフェース
    /// </summary>
    public interface IBotSetting
    {
        /// <summary>
        /// Bot ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Bot名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Bot名正規表現パターン
        /// </summary>
        [StringSyntax(StringSyntaxAttribute.Regex)]
        string NamePattern { get; }

        /// <summary>
        /// Discord Bot設定
        /// </summary>
        IDiscordSetting? DiscordSetting { get; }

        /// <summary>
        /// Twitter Bot設定
        /// </summary>
        ITwitterSetting? TwitterSetting { get; }

        /// <summary>
        /// Misskey Bot設定
        /// </summary>
        IMisskeySetting? MisskeySetting { get; }
    }

#pragma warning disable 8618
    internal class BotSetting : IBotSetting
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string NamePattern { get; set; }

        public DiscordSetting? Discord { get; set; }

        public TwitterSetting? Twitter { get; set; }

        public MisskeySetting? Misskey { get; set; }

        public IDiscordSetting? DiscordSetting { get => Discord; }

        public ITwitterSetting? TwitterSetting { get => Twitter; }

        public IMisskeySetting? MisskeySetting { get => Misskey; }
    }
#pragma warning restore
}
