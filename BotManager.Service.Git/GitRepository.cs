using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Git
{
    /// <summary>
    /// Gitリポジトリインターフェース
    /// </summary>
    public interface IGitRepositry : IDisposable
    {
        /// <summary>
        /// ローカルリポジトリのディレクトリを取得します。
        /// </summary>
        IDirectory LocalDirectory { get; }

        /// <summary>
        /// このリポジトリで git pullを実行します。
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <param name="emailAddress">ユーザーのメールアドレス</param>
        MergeStatus Pull(string userName, string emailAddress);
    }
    internal sealed class GitRepository : IGitRepositry
    {
        #region Private Fields
        private Repository repository;
        private IDirectory localDirectory;
        #endregion

        #region Constructor
        public GitRepository(string repositryPath, string directory) : this(new Repository(repositryPath), directory)
        {
        }

        public GitRepository(Repository repository, string directory)
        {
            this.repository = repository;
            this.localDirectory = new GitDirectory(directory);
        }
        #endregion

        #region Proparty
        public IDirectory LocalDirectory => this.localDirectory;
        #endregion

        #region Method
        public void Dispose()
        {
            repository.Dispose();
        }

        public MergeStatus Pull(string userName, string emailAddress)
        {
            PullOptions options = new();
            Signature signature = new(userName, emailAddress, DateTimeOffset.Now);
            var mergeResult = Commands.Pull(repository, signature, options);
            return mergeResult.Status;
        }
        #endregion
    }
}
