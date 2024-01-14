using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Utility
{
    /// <summary>
    /// <see cref="Embed"/>ユーティリティクラス
    /// </summary>
    public static class EmbedUtility
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
    }
}
