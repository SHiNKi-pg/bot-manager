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

        [Fact]
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

        [Fact]
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
    }
}
