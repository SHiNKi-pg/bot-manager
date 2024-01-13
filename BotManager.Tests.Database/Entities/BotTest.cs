using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BotManager.Tests.Database.Entities
{
    public class BotTest
    {
        private readonly ITestOutputHelper output;

        public BotTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "MST_BOT SELECT")]
        public void Select()
        {
            using (var db = BotManager.Database.BotDatabase.Connect())
            {
                var bots = db.Bots;
                foreach(var bot in bots)
                {
                    output.WriteLine($"{bot.Id.ToString()}: {bot.Name}");
                }
            }
        }

        [Fact(DisplayName = "MST_BOT INSERT")]
        public async Task Insert()
        {
            using (var db = BotManager.Database.BotDatabase.Connect())
            {
                var user = await db.Bots.AddAsync(new()
                {
                    Id = "test",
                    Name = "テスト"
                });
                await db.SaveChangesAsync();
            }
        }
    }
}
