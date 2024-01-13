using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BotManager.Tests.Database.Entities
{
    public class UserTest
    {
        private readonly ITestOutputHelper output;

        public UserTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "MST_USER SELECT")]
        public void Select()
        {
            using (var db = BotManager.Database.BotDatabase.Connect())
            {
                var users = db.Users;
                foreach(var user in users)
                {
                    output.WriteLine($"{user.Id.ToString()}: {user.Name}");
                }
            }
        }

        [Fact(DisplayName = "MST_USER INSERT")]
        public async Task Insert()
        {
            using (var db = BotManager.Database.BotDatabase.Connect())
            {
                var user = await db.Users.AddAsync(new()
                {
                    Name = "テスト"
                });
                output.WriteLine(user.Entity.Id.ToString());
                await db.SaveChangesAsync();
            }
        }
    }
}
