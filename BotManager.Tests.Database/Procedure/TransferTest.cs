using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BotManager.Tests.Database.Procedure
{
    public class TransferTest
    {
        private readonly ITestOutputHelper output;

        public TransferTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "PKG_DISCORD.TRANSFER_FROM_MISSKEY")]
        public async Task TransferFromMisskeyTest()
        {
            using (var db = BotManager.Database.BotDatabase.Connect())
            {
                await db.TransferFromMisskeyAsync(20, "testmis");
            }
        }

        [Fact(DisplayName = "PKG_MISSKEY.TRANSFER_FROM_DISCORD")]
        public async Task TransferFromDiscordTest()
        {
            using (var db = BotManager.Database.BotDatabase.Connect())
            {
                await db.TransferFromDiscordAsync(21, 100);
            }
        }

    }
}
