using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord
{
    /// <summary>
    /// Discord用ユーティリティクラス
    /// </summary>
    public static class DiscordUtility
    {
        /// <summary>
        /// 埋め込みメッセージを作成します。
        /// </summary>
        /// <param name="builder">ビルダ</param>
        /// <returns></returns>
        public static Embed CreateEmbed(Action<EmbedBuilder> builder)
        {
            EmbedBuilder embedBuilder = new();
            builder(embedBuilder);
            return embedBuilder.Build();
        }

        /// <summary>
        /// コンポーネントを作成します。
        /// </summary>
        /// <param name="builder">コンポーネントビルダ</param>
        /// <returns></returns>
        public static MessageComponent CreateComponent(Action<ComponentBuilder> builder)
        {
            ComponentBuilder componentBuilder = new();
            builder(componentBuilder);
            return componentBuilder.Build();
        }
    }
}
