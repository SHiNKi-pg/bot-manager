using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BotManager.Tests.Database.Entities
{
    public class MisskeyUserTest
    {
        private readonly ITestOutputHelper output;

        public MisskeyUserTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "MST_MISSKEY_USER SELECT")]
        public void Select()
        {
            using (var db = BotManager.Database.BotDatabase.Connect())
            {
                var users = db.MisskeyUsers;
                foreach(var user in users)
                {
                    output.WriteLine($"{user.Id.ToString()}: {user.Name}");
                }
            }
        }

        [Fact(DisplayName = "MST_MISSKEY_USER INSERT")]
        public async Task Insert()
        {
            using (var db = BotManager.Database.BotDatabase.Connect())
            {
                var user = await db.MisskeyUsers.AddAsync(new()
                {
                    Id = "test",
                    Name = "テスト"
                });
                await db.SaveChangesAsync();
            }
        }
    }
}
