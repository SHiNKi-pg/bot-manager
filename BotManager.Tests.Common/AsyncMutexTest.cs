using BotManager.Common.Mutex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BotManager.Tests.Common
{
    public class AsyncMutexTest
    {
        private readonly ITestOutputHelper output;

        public AsyncMutexTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "ミューテックス名同名（排他制御）")]
        public async Task MultiThread_SameName()
        {
            List<int> list = new();

            var task1 = MutexUtility.WaitAsync("test_A", async () =>
            {
                output.WriteLine("Task1 Start");
                list.Add(1);
                await Task.Delay(2000);
                list.Add(2);
                output.WriteLine("Task1 End");
            });

            var task2 = MutexUtility.WaitAsync("test_A", async () =>
            {
                output.WriteLine("Task2 Start");
                list.Add(3);
                await Task.Delay(1000);
                list.Add(4);
                output.WriteLine("Task2 End");
            });

            output.WriteLine("Task Await Start");
            await Task.WhenAll(task1, task2);
            output.WriteLine("Task Await End");
            Assert.Equal(list.AsEnumerable(), new int[] { 1, 2, 3, 4});
        }

        [Fact(DisplayName = "ミューテックス名別名")]
        public async Task MultiThread()
        {
            List<int> list = new();

            var task1 = MutexUtility.WaitAsync("test_2_A", async () =>
            {
                output.WriteLine("Task1 Start");
                await Task.Delay(2000);
                list.Add(1);
                output.WriteLine("Task1 End");
            });

            var task2 = MutexUtility.WaitAsync("test_2_B", async () =>
            {
                output.WriteLine("Task2 Start");
                await Task.Delay(1000);
                list.Add(2);
                output.WriteLine("Task2 End");
            });

            output.WriteLine("Task Await Start");
            await Task.WhenAll(task1, task2);
            output.WriteLine("Task Await End");
            Assert.Equal(list.AsEnumerable(), new int[] { 2, 1});
        }

        [Fact(DisplayName = "途中キャンセル")]
        public async Task CancelThread()
        {
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                var task1 = MutexUtility.WaitAsync("test_3_A", async ct =>
                {
                    output.WriteLine("Task1 Start");
                    await Task.Delay(2000);
                    output.WriteLine("Task1 End");
                }, cts.Token);

                var task2 = MutexUtility.WaitAsync("test_3_A", async ct =>
                {
                    output.WriteLine("Task2 Start");
                    await Task.Delay(1000);

                    // キャンセル
                    cts.Cancel();
                    output.WriteLine("Task2 End");
                }, cts.Token);

                var task3 = MutexUtility.WaitAsync("test_3_A", async ct =>
                {
                    Assert.Fail("キャンセルしているので実行されないはず");
                    output.WriteLine("Task3 Start");
                    await Task.Delay(3000);
                    output.WriteLine("Task3 End");
                }, cts.Token);

                output.WriteLine("Task Await Start");
                await Task.WhenAll(task1, task2, task3);
                output.WriteLine("Task Await End");

                Assert.True(await task1, "Task1は実行されている");
                Assert.False(await task2, "Task2は途中でキャンセルしているのでFalseになるはず");
                Assert.False(await task3, "Task3は実行されていないはず");
            }
        }

        [Fact(DisplayName = "Disposable排他制御テスト")]
        public async Task LockAsyncTest()
        {
            List<int> list = new();

            var task = MutexUtility.WaitAsync("test_4", async () =>
            {
                output.WriteLine("Task1 Start");
                await Task.Delay(2000);
                list.Add(1);
                output.WriteLine("Task1 End");
            });

            await using(await MutexUtility.LockAsync("test_4", CancellationToken.None))
            {
                output.WriteLine("Task2 Start");
                await Task.Delay(1000);
                list.Add(2);
                output.WriteLine("Task2 End");
            }

            output.WriteLine("Task Await Start");
            await task;
            output.WriteLine("Task Await End");
            Assert.Equal(list.AsEnumerable(), new int[] { 1,2  });
        }

        [Fact(DisplayName = "ミューテックス待機テスト")]
        public async Task WaitMutexTest()
        {
            // ここでは待機しないはず
            await MutexUtility.WaitAsync("test_5", CancellationToken.None);

            // 3秒間待機
            bool endFlag = false;
            var task = MutexUtility.WaitAsync("test_5", async () =>
            {
                output.WriteLine("Task Start");
                await Task.Delay(3000);
                endFlag = true;
                output.WriteLine("Task End");
            });

            // 待機
            Assert.False(endFlag, "ここでは終了していないはず");
            output.WriteLine("test_5 mutex waiting");
            await MutexUtility.WaitAsync("test_5", CancellationToken.None);
            output.WriteLine("test_5 mutex waiting end");
            Assert.True(endFlag, "このタイミングでは終了しているはず");

            await task;
        }
    }
}
