using BotManager.Engine;
using BotManager.Service.Git;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Git.Tests
{
    public class GitTest
    {
        private readonly ITestOutputHelper output;

        public GitTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "設定ファイルから情報取得")]
        public void GetInfoFromSettingFileTest()
        {
            var gitSetting = AppSettings.Script;
            output.WriteLine("リポジトリ : {0}", gitSetting.RepositoryUrl);
            output.WriteLine("ディレクトリ : {0}", gitSetting.Path);
            output.WriteLine("ユーザー名 : {0}", gitSetting.UserName);
            output.WriteLine("メールアドレス : {0}", gitSetting.Email);
        }

        [Fact(DisplayName = "git clone")]
        public void CloneTest()
        {
            var gitSetting = AppSettings.Script;
            // クローン
            using (var repos = BotManager.Service.Git.Git.Clone(gitSetting.RepositoryUrl, gitSetting.Path))
            {
                // ディレクトリ名
                string directoryPath = repos.LocalDirectory.Path;
                output.WriteLine("クローン先ディレクトリ : {0}", directoryPath);
                Assert.False(string.IsNullOrEmpty(directoryPath));

                // ディレクトリ削除
                repos.LocalDirectory.Delete();
            }
        }
    }
}
