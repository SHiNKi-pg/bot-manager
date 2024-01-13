using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BotManager.Tests.Database.Entities
{
    public class DiscordBotTest
    {
        private readonly ITestOutputHelper output;

        public DiscordBotTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "MST_DISCORD_USER SELECT")]
        public void Select()
        {
            using (var db = BotManager.Database.BotDatabase.Connect())
            {
                var users = db.DiscordUsers;
                foreach(var user in users)
                {
                    output.WriteLine($"{user.Id.ToString()}: {user.Name}, {user.MentionString}");
                }
            }
        }

        [Fact(DisplayName = "MST_DISCORD_USER INSERT")]
        public async Task Insert()
        {
            using (var db = BotManager.Database.BotDatabase.Connect())
            {
                var user = await db.DiscordUsers.AddAsync(new()
                {
                    Id = 0,
                    Name = "テスト",
                    MentionString = "Mention"
                });
                await db.SaveChangesAsync();
            }
        }
    }
}
