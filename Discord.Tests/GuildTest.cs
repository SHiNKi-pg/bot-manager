using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Tests;
using Xunit.Abstractions;

namespace Discord.Tests
{
    public class GuildTest : TestBase
    {
        public GuildTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact(DisplayName = "メッセージ投稿＆削除テスト")]
        public async Task SendMessageTest()
        {
            // 認証
            await client.StartAsync();

            // メインギルド取得
            var guild = client.GetGuild(setting.MainGuildId);

            // チャンネル取得
            var channel = await guild.GetTextChannelAsync(setting.TestChannelId);

            // メッセージ投稿
            var response = await channel.SendMessageAsync(text: "メッセージ投稿テスト");
            output.WriteLine("投稿メッセージID : {0}", response.Id.ToString());

            await Task.Delay(3000);

            // メッセージ削除
            await response.DeleteAsync();

            // 終了
            await client.EndAsync();
        }
    }
}
