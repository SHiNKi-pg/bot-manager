using BotManager.Reactive;
using Microsoft.Reactive.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Notifiers.Test
{
    public class ReactiveTest
    {
        private readonly ITestOutputHelper output;

        public ReactiveTest(ITestOutputHelper helper)
        {
            this.output = helper;
        }

        [Fact(DisplayName = "Interlocked Disposal Subscription")]
        public async Task InterlockDisposingTest()
        {
            var numobs = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .GroupBy(n => n % 10)
                ;

            var subscription = numobs.InterlockedSubscribe((g, d) =>
            {
                // Disposeされたことを受け取れるかテスト
                g
                .TakeUntil(d)
                .Finally(() => output.WriteLine("Disposing"))
                .Subscribe(n => output.WriteLine("{0}-{1}", g.Key, n));
            });
            await Task.Delay(25000);
            subscription.Dispose();
        }
    }
}
