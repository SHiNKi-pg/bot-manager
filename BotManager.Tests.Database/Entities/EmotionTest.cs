using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BotManager.Tests.Database.Entities
{
    public class EmotionTest
    {
        private readonly ITestOutputHelper output;

        public EmotionTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "TBL_EMOTION INSERT")]
        public async Task InsertEmotionTest()
        {
            using (var db = BotManager.Database.BotDatabase.Connect())
            {
                await db.Emotions.AddAsync(new()
                {
                    BotId = "test",
                    UserId = 1,
                    Value = 5
                });
                await db.Emotions.AddAsync(new()
                {
                    BotId = "test",
                    UserId = 1,
                    Value = 7
                });
                await db.SaveChangesAsync();
            }
        }

        [Fact(DisplayName = "MV_EMOTION SELECT")]
        public async Task FetchMVEmotionTest()
        {
            using (var db = BotManager.Database.BotDatabase.Connect())
            {
                var emotions = db.EmotionTotals.AsAsyncEnumerable();
                await foreach(var emotion in emotions)
                {
                    output.WriteLine("[Bot:{0}, User{1}]: {2}", emotion.BotId, emotion.UserId, emotion.TotalValue);
                }
            }
        }
    }
}
