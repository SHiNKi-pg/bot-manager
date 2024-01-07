using BotManager.Service.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Compiler.Tests
{
    public class NormalCompileTest : TestBase
    {
        public NormalCompileTest(ITestOutputHelper testOutput) : base("test_compiler1.dll", testOutput) { }

        [Fact(DisplayName = "何もないソース")]
        public void NoSourceTest()
        {
            compiler.ClearSources();
            compiler.AddSource("");

            compiler.Compile();
        }

        [Fact(DisplayName = "Bot属性付与クラス")]
        public void BotAttributeTest()
        {
            compiler.ClearSources();
            compiler.AddSource("""
                using BotManager.Common.Scripting.Attributes;

                [Bot]
                public class Class1 {

                }
                """);

            compiler.Compile();
        }

        [Fact(DisplayName = "サブスクリプションソース")]
        public void SubscriptionSourceTest()
        {
            compiler.ClearSources();
            compiler.AddSource("""
                using System;
                using System.Reactive.Disposables;
                using BotManager.Common.Scripting;
                using BotManager.Common.Scripting.Attributes;

                [Action]
                public class SubscriptionTest : ISubscription
                {
                    public IDisposable SubscribeFrom(ISubscriptionArguments args)
                    {
                        return Disposable.Empty;
                    }
                }
                """);

            compiler.Compile();
        }
    }
}
