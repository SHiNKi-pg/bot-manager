using BotManager.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BotManager.Setting.Test
{
    public class ReadSetting
    {
        private readonly ITestOutputHelper output;

        public ReadSetting(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void DatabaseSchema()
        {
            try
            {
                output.WriteLine(AppSettings.Database.ConnectionString);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Fact]
        public void LoadBots()
        {
            try
            {
                var bots = AppSettings.Bots;
                foreach(var bot in bots)
                {
                    output.WriteLine("{0}: {1}", bot.Id, bot.Name);
                    output.WriteLine("Discord Setting : " + (bot.DiscordSetting != null));
                    output.WriteLine("Twitter Setting : " + (bot.TwitterSetting != null));
                    output.WriteLine("Misskey Setting : " + (bot.MisskeySetting != null));
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
