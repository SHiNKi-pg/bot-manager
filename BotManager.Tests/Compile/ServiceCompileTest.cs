using BotManager.Engine;
using BotManager.Service.Compiler;
using BotManager.Service.Git;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BotManager.Tests.Compile
{
    public class ServiceCompileTest : TestBase
    {
        public ServiceCompileTest(ITestOutputHelper testOutput) : base("test_compiler2.dll", testOutput) { }

        [Fact(DisplayName = "Botスクリプトコンパイルテスト")]
        public async Task RepositoryCompileTest()
        {
            // ブランチ切替
            var setting = AppSettings.Script;
            var repositry = Git.GetOrClone(setting.RepositoryUrl, setting.Path);
            repositry.Checkout(setting.BranchName);

            // ファイル名出力
            var files = repositry.LocalDirectory.DirectoryInfo.EnumerateFiles("*.cs", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                output.WriteLine(file.Name);
            }

            // ソース設定
            compiler.ClearSources();
            await compiler.CompileFrom(files);
        }
    }
}
