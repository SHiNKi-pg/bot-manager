using BotManager.Engine;
using BotManager.Service.Discord;
using BotManager.Setting.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Twitter.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected readonly DiscordServiceClient client;
        protected readonly ITestOutputHelper output;
        protected readonly IDiscordSetting setting;

        public TestBase(ITestOutputHelper output)
        {
            this.setting = AppSettings.GetBotDictionary()["test"].DiscordSetting!;
            var certification = setting.Certificate!;
            this.output = output;
            client = new(certification.Token, new());
        }

        public virtual void Dispose()
        {
            client.Dispose();
        }
    }
}
