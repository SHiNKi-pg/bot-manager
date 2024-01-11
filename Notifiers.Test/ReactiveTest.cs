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

        [Fact(DisplayName = "Dispose内部通知/外部でDisposeする")]
        public async Task InterlockDisposingTest()
        {
            var numobs = Observable
                .Interval(TimeSpan.FromSeconds(0.01))
                .GroupBy(n => n % 10)
                ;

            bool disposed = false;

            var subscription = numobs.InterlockedSubscribe((g, d) =>
            {
                // Disposeされたことを受け取れるかテスト
                g
                .Subscribe(n => output.WriteLine("{0}-{1}", g.Key, n))
                .Then(() =>
                {
                    disposed = true;
                    output.WriteLine("Key:{0} Disposed", g.Key);
                })
                .DisposeOn(d)
                ;
            });
            await Task.Delay(120);
            subscription.Dispose();

            Assert.True(disposed);  // 内部のSubscribeがDisposeされていればOK
        }

        [Fact(DisplayName = "Dispose内部通知/外部でDisposeしない")]
        public async Task InterlockDisposingNoDisposeTest()
        {
            var numobs = Observable
                .Interval(TimeSpan.FromSeconds(0.01))
                .GroupBy(n => n % 10)
                ;

            var subscription = numobs.InterlockedSubscribe((g, d) =>
            {
                // Disposeされたことを受け取れるかテスト
                g
                .Subscribe(n => output.WriteLine("{0}-{1}", g.Key, n))
                .Then(() => Assert.Fail($"Key : {g.Key} Disposed")) // 外部でDisposeされていないのにも関わらず呼ばれたら失敗
                .DisposeOn(d)
                ;
            });
            await Task.Delay(120);
        }
    }
}
