using BotManager.Service.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BotManager.Tests.Compile
{
    public abstract class TestBase : IDisposable
    {
        protected readonly ITestOutputHelper output;
        protected readonly TestCompiler compiler;

        public TestBase(string assemblyName, ITestOutputHelper output)
        {
            this.output = output;
            compiler = new TestCompiler(assemblyName);

            compiler.CompileFailed.Subscribe(_ =>
            {
                output.WriteLine("コンパイル失敗");
                Assert.Fail("コンパイル失敗");
            });

            compiler.AssemblyCreated.Subscribe(_ =>
            {
                output.WriteLine("コンパイル成功");
            });

            compiler.CompileError.Subscribe(errors =>
            {
                foreach (var error in errors)
                {
                    output.WriteLine("[{0}]{1} Line:{2} | {3}",
                        error.Severity.ToString(),
                        error.Id,
                        error.Location.GetLineSpan().StartLinePosition.Line.ToString(),
                        error.GetMessage()
                        );
                }
            });
        }

        public void Dispose()
        {
            compiler.Dispose();
        }
    }
}
