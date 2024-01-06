using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BotManager.Tests.Database
{
    public class DatabaseTest
    {
        private readonly ITestOutputHelper output;

        public DatabaseTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "データベース接続")]
        public async Task ConnectionTest()
        {
            using(var db = BotManager.Database.Database.Connect())
            {
                using(var tran = await db.BeginTransactionAsync())
                {
                    output.WriteLine("トランザクション開始");
                    await Task.Delay(3000);

                    tran.Commit();
                    output.WriteLine("トランザクション コミット");
                }
                output.WriteLine("トランザクション終了");
            }
        }
    }
}
