using BotManager.Engine;
using BotManager.Service.Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Tests;
using Xunit.Abstractions;

namespace Discord.Tests
{
    public class CertificationTest : TestBase
    {
        public CertificationTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact(DisplayName = "ログインとログアウトのテスト")]
        public async Task LoginTest()
        {
            await client.StartAsync();
            Assert.True(client.Status == UserStatus.Online);
            await client.EndAsync();
        }

        [Fact(DisplayName = "Bot設定取得")]
        public void GetBotsTest()
        {
            var bots = AppSettings.Bots;
            foreach (var bot in bots)
            {
                output.WriteLine("BotId : {0}", bot.Id);
                var discord = bot.DiscordSetting;
                if (discord != null)
                {
                    output.WriteLine("Token : {0}", discord.Certificate.Token);
                    output.WriteLine("Main Guild Id : {0}", discord.MainGuildId.ToString());
                    output.WriteLine("Test Channel Id : {0}", discord.TestChannelId.ToString());
                    output.WriteLine("");
                }
            }
        }
    }
}
