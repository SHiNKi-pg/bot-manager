using BotManager.Engine;
using BotManager.Service.Git;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Compiler.Tests
{
    public class ServiceCompileTest : TestBase
    {
        public ServiceCompileTest(ITestOutputHelper testOutput) : base("test_compiler2.dll", testOutput) { }

        [Fact(DisplayName = "Botスクリプトコンパイルテスト")]
        public async Task RepositoryCompileTest()
        {
            // Git Clone or Git Pull
            var setting = AppSettings.Script;
            var repositry = Git.GetOrClone(setting.RepositoryUrl, setting.Path);

            // ソース設定
            compiler.ClearSources();
            await compiler.CompileFrom(repositry.LocalDirectory.DirectoryInfo, "*.cs");
        }
    }
}
